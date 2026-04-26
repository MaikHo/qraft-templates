using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;

var argsMap = ParseArgs(args);
if (argsMap.ContainsKey("help"))
{
    PrintHelp();
    return;
}

LoadDotEnv();

var token = Environment.GetEnvironmentVariable("SUPABASE_ACCESS_TOKEN");
if (string.IsNullOrWhiteSpace(token))
{
    Console.Error.WriteLine("SUPABASE_ACCESS_TOKEN not found in environment or .env");
    Environment.Exit(1);
}

var project = GetArg(argsMap, "project");
var functionName = GetArg(argsMap, "function");
var type = GetArg(argsMap, "type") ?? "both";
var output = GetArg(argsMap, "output");
var limit = int.TryParse(GetArg(argsMap, "limit"), out var parsedLimit) ? parsedLimit : 200;
limit = Math.Clamp(limit, 1, 1000);
var duration = GetArg(argsMap, "last") ?? "30m";

var startUtc = DateTimeOffset.UtcNow - ParseDuration(duration);
var endUtc = DateTimeOffset.UtcNow;

project ??= await ResolveProjectFromApi(token);

using var http = new HttpClient { BaseAddress = new Uri("https://api.supabase.com") };
http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

var result = new JsonObject
{
    ["project"] = project,
    ["function"] = functionName ?? "all",
    ["timeRange"] = new JsonObject
    {
        ["start"] = startUtc.ToString("O"),
        ["end"] = endUtc.ToString("O")
    }
};

if (type is "runtime" or "both")
{
    var runtimeSql = BuildRuntimeLogsQuery(functionName, limit);
    result["runtime"] = await FetchLogs(http, project!, runtimeSql, startUtc, endUtc);
}

if (type is "edge" or "both")
{
    var edgeSql = BuildEdgeLogsQuery(functionName, limit);
    result["edge"] = await FetchLogs(http, project!, edgeSql, startUtc, endUtc);
}

var json = result.ToJsonString(new JsonSerializerOptions { WriteIndented = true });
if (!string.IsNullOrWhiteSpace(output))
{
    await File.WriteAllTextAsync(output, json);
    Console.WriteLine($"Saved logs to {output}");
}
else
{
    Console.WriteLine(json);
}

static Dictionary<string, string?> ParseArgs(string[] args)
{
    var map = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
    for (var i = 0; i < args.Length; i++)
    {
        var arg = args[i];
        if (!arg.StartsWith("--", StringComparison.Ordinal))
        {
            continue;
        }

        var key = arg[2..];
        if (key is "help" or "h")
        {
            map["help"] = "true";
            continue;
        }

        var value = i + 1 < args.Length && !args[i + 1].StartsWith("--", StringComparison.Ordinal)
            ? args[++i]
            : null;
        map[key] = value;
    }

    return map;
}

static string? GetArg(Dictionary<string, string?> argsMap, string key) =>
    argsMap.TryGetValue(key, out var value) ? value : null;

static TimeSpan ParseDuration(string input)
{
    if (string.IsNullOrWhiteSpace(input))
    {
        return TimeSpan.FromMinutes(30);
    }

    var unit = input[^1];
    if (!int.TryParse(input[..^1], out var value))
    {
        throw new ArgumentException($"Invalid duration: {input}");
    }

    return unit switch
    {
        'm' => TimeSpan.FromMinutes(value),
        'h' => TimeSpan.FromHours(value),
        'd' => TimeSpan.FromDays(value),
        _ => throw new ArgumentException($"Invalid duration unit in: {input}")
    };
}

static async Task<string> ResolveProjectFromApi(string token)
{
    using var http = new HttpClient { BaseAddress = new Uri("https://api.supabase.com") };
    http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    var response = await http.GetAsync("/v1/projects");
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();
    var projects = JsonNode.Parse(body)?.AsArray() ?? [];
    if (projects.Count == 0)
    {
        throw new InvalidOperationException("No Supabase projects found for token.");
    }

    if (projects.Count > 1)
    {
        throw new InvalidOperationException("Multiple projects found. Please pass --project <ref>.");
    }

    return projects[0]?["id"]?.GetValue<string>()
           ?? throw new InvalidOperationException("Project id missing from API response.");
}

static async Task<JsonArray> FetchLogs(
    HttpClient http,
    string projectRef,
    string sql,
    DateTimeOffset startUtc,
    DateTimeOffset endUtc)
{
    var url =
        $"/v1/projects/{projectRef}/analytics/endpoints/logs.all?sql={Uri.EscapeDataString(sql)}" +
        $"&iso_timestamp_start={Uri.EscapeDataString(startUtc.ToString("O"))}" +
        $"&iso_timestamp_end={Uri.EscapeDataString(endUtc.ToString("O"))}";

    var response = await http.GetAsync(url);
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();

    var parsed = JsonNode.Parse(body);
    if (parsed is JsonArray array)
    {
        return array;
    }

    if (parsed?["result"] is JsonArray resultArray)
    {
        return resultArray;
    }

    return [];
}

static string BuildRuntimeLogsQuery(string? functionName, int limit)
{
    var filter = string.IsNullOrWhiteSpace(functionName)
        ? ""
        : $"AND m.function_id IN (SELECT DISTINCT em.function_id FROM function_edge_logs AS el CROSS JOIN UNNEST(el.metadata) AS em CROSS JOIN UNNEST(em.request) AS er WHERE er.pathname = '/functions/v1/{functionName}')";

    return $"""
            SELECT
              datetime(t.timestamp) AS time,
              t.event_message,
              m.function_id,
              m.level,
              m.execution_id
            FROM function_logs AS t
              CROSS JOIN UNNEST(t.metadata) AS m
            WHERE m.event_type = 'Log'
              {filter}
            ORDER BY t.timestamp DESC
            LIMIT {limit}
            """;
}

static string BuildEdgeLogsQuery(string? functionName, int limit)
{
    var filter = string.IsNullOrWhiteSpace(functionName)
        ? "AND r.pathname LIKE '/functions/v1/%'"
        : $"AND r.pathname = '/functions/v1/{functionName}'";

    return $"""
            SELECT
              datetime(t.timestamp) AS time,
              r.method,
              r.pathname,
              r.search,
              res.status_code,
              m.execution_time_ms
            FROM function_edge_logs AS t
              CROSS JOIN UNNEST(t.metadata) AS m
              CROSS JOIN UNNEST(m.request) AS r
              CROSS JOIN UNNEST(m.response) AS res
            WHERE TRUE
              {filter}
            ORDER BY t.timestamp DESC
            LIMIT {limit}
            """;
}

static void LoadDotEnv()
{
    var envPath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
    if (!File.Exists(envPath))
    {
        return;
    }

    foreach (var rawLine in File.ReadLines(envPath))
    {
        var line = rawLine.Trim();
        if (line.Length == 0 || line.StartsWith("#", StringComparison.Ordinal))
        {
            continue;
        }

        var separator = line.IndexOf('=');
        if (separator <= 0)
        {
            continue;
        }

        var key = line[..separator].Trim();
        var value = line[(separator + 1)..].Trim().Trim('"', '\'');
        if (Environment.GetEnvironmentVariable(key) is null)
        {
            Environment.SetEnvironmentVariable(key, value);
        }
    }
}

static void PrintHelp()
{
    Console.WriteLine(
        """
        Supabase Logs Tool (.NET)

        Options:
          --project <ref>     20-char project ref
          --function <name>   Edge function name
          --last <duration>   15m, 30m, 1h, 2h, 6h, 12h, 24h
          --type <type>       runtime, edge, both (default: both)
          --output <file>     Write JSON output to file
          --limit <n>         Max rows per query (default: 200, max: 1000)
          --help              Show help
        """);
}
