using System.Collections;

namespace OpenCode.ToolUtils.Env;

public sealed class EnvLoaderOptions
{
    public IReadOnlyList<string>? SearchPaths { get; init; }
    public bool OverrideExisting { get; init; }
}

public static class EnvLoader
{
    private static readonly string[] DefaultSearchPaths =
    [
        ".env",
        "../.env",
        "../../.env",
        "../../../.env"
    ];

    public static IDictionary<string, string> Load(EnvLoaderOptions? options = null)
    {
        var loaded = new Dictionary<string, string>(StringComparer.Ordinal);
        var searchPaths = options?.SearchPaths ?? DefaultSearchPaths;
        var overrideExisting = options?.OverrideExisting ?? false;

        foreach (var path in searchPaths)
        {
            var fullPath = Path.GetFullPath(path);
            if (!File.Exists(fullPath))
            {
                continue;
            }

            foreach (var line in File.ReadLines(fullPath))
            {
                var trimmed = line.Trim();
                if (trimmed.Length == 0 || trimmed.StartsWith("#", StringComparison.Ordinal))
                {
                    continue;
                }

                var separatorIndex = trimmed.IndexOf('=');
                if (separatorIndex <= 0)
                {
                    continue;
                }

                var key = trimmed[..separatorIndex].Trim();
                var value = trimmed[(separatorIndex + 1)..].Trim().Trim('"', '\'');
                if (string.IsNullOrWhiteSpace(key))
                {
                    continue;
                }

                if (!overrideExisting && Environment.GetEnvironmentVariable(key) is not null)
                {
                    continue;
                }

                Environment.SetEnvironmentVariable(key, value);
                loaded[key] = value;
            }
        }

        return loaded;
    }

    public static string? Get(string key, EnvLoaderOptions? options = null)
    {
        var existing = Environment.GetEnvironmentVariable(key);
        if (!string.IsNullOrEmpty(existing))
        {
            return existing;
        }

        var loaded = Load(options);
        return loaded.GetValueOrDefault(key);
    }

    public static string GetRequired(string key, EnvLoaderOptions? options = null)
    {
        var value = Get(key, options);
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException($"Required environment variable '{key}' not found.");
        }

        return value;
    }

    public static IDictionary<string, string> GetRequiredMany(IEnumerable<string> keys, EnvLoaderOptions? options = null)
    {
        var result = new Dictionary<string, string>(StringComparer.Ordinal);
        Load(options);

        foreach (var key in keys)
        {
            var value = Environment.GetEnvironmentVariable(key);
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException($"Required environment variable '{key}' not found.");
            }

            result[key] = value;
        }

        return result;
    }

    public static string GetApiKey(string keyName, EnvLoaderOptions? options = null) =>
        GetRequired(keyName, options);
}
