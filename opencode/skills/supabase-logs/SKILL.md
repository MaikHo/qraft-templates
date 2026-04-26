---
name: supabase-logs
description: >
  Fetches and analyzes Supabase Edge Function logs (runtime console output + HTTP edge logs)
  using a bundled C#/.NET CLI tool and the Supabase Management API. Use this skill whenever
  the user mentions errors, bugs, or unexpected behavior in a Supabase Edge Function, or
  asks to "check the logs", "what's happening in function X", "why is my function failing",
  "look at the logs for", or anything that involves debugging or understanding what an Edge
  Function is doing. Don't wait to be asked explicitly — if there's an Edge Function problem,
  proactively fetch the logs.

  Applies specifically to Supabase Edge Functions (Deno-based serverless functions invoked
  via HTTP). Does NOT apply to Supabase database logs, Realtime, Auth issues, Postgres
  triggers, or serverless functions on other platforms (Vercel, Cloudflare Workers, etc.).
---

This skill lets you fetch Supabase Edge Function logs directly from the terminal using the
Supabase Management API — no browser, no dashboard, no manual copying.

The bundled script fetches two types of logs for any Edge Function:
- **Runtime logs** (`function_logs`): output from `console.log`, `console.error`, etc. inside the function code
- **Edge logs** (`function_edge_logs`): HTTP-level metadata — method, path, status code, latency for each invocation

## Setup (first time only)

The script reads `SUPABASE_ACCESS_TOKEN` from the `.env` file in the repo root.

If the token isn't set yet, tell the user:
> "You need a Supabase personal access token. Go to https://supabase.com/dashboard/account/tokens,
> create one, and add this line to your `.env` file in the repo root:
> `SUPABASE_ACCESS_TOKEN=sbp_...`"

## Running the script

The CLI project is at: `.opencode/skills/supabase-logs/scripts/SupabaseLogsTool.csproj`

Run it with .NET from the repo root:

```bash
# Investigate a specific function (last 30 min, both log types)
dotnet run --project .opencode/skills/supabase-logs/scripts/SupabaseLogsTool.csproj -- --function <name>

# Wider time window for intermittent issues
dotnet run --project .opencode/skills/supabase-logs/scripts/SupabaseLogsTool.csproj -- --function <name> --last 2h

# Save as JSON for further analysis
dotnet run --project .opencode/skills/supabase-logs/scripts/SupabaseLogsTool.csproj -- --function <name> --last 1h --output /tmp/logs.json

# Specify a project explicitly (if you have multiple)
dotnet run --project .opencode/skills/supabase-logs/scripts/SupabaseLogsTool.csproj -- --project abcdefghijklmnopqrst --function <name>

# Only one log type
dotnet run --project .opencode/skills/supabase-logs/scripts/SupabaseLogsTool.csproj -- --function <name> --type runtime
dotnet run --project .opencode/skills/supabase-logs/scripts/SupabaseLogsTool.csproj -- --function <name> --type edge
```

### All options

| Option | Default | Description |
|--------|---------|-------------|
| `--function <name>` | all functions | Filter by Edge Function name |
| `--last <duration>` | `30m` | Time window: `15m`, `30m`, `1h`, `2h`, `6h`, `12h`, `24h` |
| `--project <ref>` | auto-detected | 20-char project ref |
| `--type <type>` | `both` | `runtime`, `edge`, or `both` |
| `--output <file>` | stdout | Save JSON to file |
| `--limit <n>` | `200` | Max rows per query (max 1000) |

## Project auto-detection (monorepo-aware)

If `--project` is not specified, the script tries in order:

1. If `--project` is given, it is used directly
2. Otherwise, the tool queries the Management API project list
3. If exactly one project exists, it is auto-selected
4. If multiple projects exist, pass `--project <ref>` explicitly

This means it works whether `supabase/` is at the root, inside `apps/saas/`, `packages/backend/`, or anywhere else up to 4 levels down.

## How to investigate an Edge Function error

When the user reports a problem with an Edge Function, follow this workflow:

1. **Run the script** to get recent logs — start with `--last 30m`, go wider if needed
2. **Look at runtime logs first** — errors, stack traces, and `console.log` output reveal what happened inside the function
3. **Cross-reference edge logs** — check HTTP status codes and latency. A 500 with high latency suggests a timeout; a 401/403 is auth; a 400 is bad input
4. **Identify the pattern**: Is it failing on every request? Only for certain inputs? After a recent deploy?
5. **Report findings** clearly: what the error is, when it started, which requests are affected

### What good log analysis looks like

- If you see `Error:` or stack traces in runtime logs → quote them exactly and explain the root cause
- If edge logs show mostly 500s → there's a runtime crash — look for the corresponding error in runtime logs
- If edge logs show 200s but user says something is broken → the function runs fine, issue is in the response data
- If no logs at all → either the function isn't being called, or it's a different time window — try `--last 6h` or `--last 24h`

## Time range guidance

- **`--last 30m`**: Good for "just happened" issues
- **`--last 2h`**: Good for investigating something noticed in the last couple hours  
- **`--last 24h`**: Good for understanding recurring patterns or when the issue started
- Max range is 24h per API call; for longer history, run multiple calls with explicit timestamps

## Handling the JSON output (for AI analysis)

When saving to `--output logs.json`, the structure is:
```json
{
  "project": "projectref",
  "function": "function-name",
  "timeRange": { "start": "...", "end": "..." },
  "runtime": [ { "time": "...", "event_message": "...", "level": "log", "function_id": "...", "execution_id": "..." } ],
  "edge": [ { "time": "...", "method": "POST", "pathname": "/functions/v1/my-fn", "status_code": 500, "execution_time_ms": 1230 } ]
}
```

Read this file and analyze it directly — the `event_message` field in runtime logs contains the actual console output.
