# OpenCode Tool Utilities (.NET)

Dieses Verzeichnis enthält C#/.NET-Utilities zum Laden und Abrufen von
Umgebungsvariablen.

## Projekte

- [OpenCode.ToolUtils.csproj](/Users/mhoffmann/github/qraft-templates/opencode/utilities/tool-utils/OpenCode.ToolUtils.csproj)
- [OpenCode.ToolUtils.Tests.csproj](/Users/mhoffmann/github/qraft-templates/opencode/utilities/tool-utils/OpenCode.ToolUtils.Tests/OpenCode.ToolUtils.Tests.csproj)

## Implementierung

- [EnvLoader.cs](/Users/mhoffmann/github/qraft-templates/opencode/utilities/tool-utils/Env/EnvLoader.cs)

Enthaltene APIs:

- `EnvLoader.Load(options?)`
- `EnvLoader.Get(key, options?)`
- `EnvLoader.GetRequired(key, options?)`
- `EnvLoader.GetRequiredMany(keys, options?)`
- `EnvLoader.GetApiKey(keyName, options?)`

## Build

```bash
cd opencode/utilities/tool-utils
dotnet build
```

## Tests

```bash
cd opencode/utilities/tool-utils
dotnet test
```
