# Änderungslog

## 2026-04-26 11:16:51 CEST
- Initiale Einrichtung der Log-Datei `log.md` im Projekt-Root.
- Regel festgehalten: Jede zukünftige Änderung im Projekt wird direkt nach der Änderung als eigener Log-Eintrag dokumentiert.

## 2026-04-26 11:20:02 CEST
- `opencode/context/core/standards/code.md` vollständig auf C#/.NET umgestellt.
- Fokus ergänzt: Blazor, AvaloniaUI, Design Patterns, Clean Code, KISS sowie .NET-konforme Struktur- und Namensregeln.

## 2026-04-26 11:20:33 CEST
- `opencode/context/core/standards/tests.md` vollständig auf .NET-Teststandards umgestellt.
- Test-Workflow und Beispiele auf `dotnet test`, AAA, xUnit/NUnit und Mocking von Abhängigkeiten angepasst.

## 2026-04-26 11:20:54 CEST
- `opencode/context/core/standards/docs.md` auf .NET/C#-Dokustandard umgestellt.
- README-/Befehlsbeispiele auf `dotnet restore`, `dotnet build`, `dotnet run`, `dotnet test` angepasst.

## 2026-04-26 11:21:27 CEST
- Neues Kontextdokument `opencode/context/development/dotnet-ui-patterns.md` angelegt (Blazor + AvaloniaUI Patterns).
- `opencode/context/index.md` aktualisiert: Development-Mapping von `react-patterns` auf `dotnet-ui-patterns` umgestellt.

## 2026-04-26 11:22:35 CEST
- Agenten auf .NET/C#-Fokus umgestellt:
  - `opencode/agent/core/opencoder.md`
  - `opencode/agent/development/codebase-agent.md`
  - `opencode/agent/development/frontend-specialist.md` (neu als Blazor/Avalonia-Spezialist)
  - `opencode/agent/subagents/code/build-agent.md`
  - `opencode/agent/subagents/code/tester.md`
  - `opencode/agent/development/0-category.json`
- Validierungs-/Build-Logik auf `dotnet`-Kommandos und .NET-Workflows ausgerichtet.

## 2026-04-26 11:24:05 CEST
- Commands auf .NET/C#-Workflow umgestellt:
  - `opencode/command/test.md`
  - `opencode/command/lint.md`
  - `opencode/command/clean.md`
  - `opencode/command/commit.md`
  - `opencode/command/commit-project.md`
  - `opencode/command/bug-rerender.md`
  - `opencode/command/context.md` (Framework-/Toolchain-Beispiele angepasst)
- `opencode/manifest.json` aktualisiert (postInstall-Hinweis primär auf `dotnet restore` ausgerichtet).

## 2026-04-26 11:24:46 CEST
- `opencode/context/project/project-context.md` vollständig auf generischen .NET/C#-Projektkontext umgestellt.
- `opencode/command/optimize.md` Framework-Hinweise von React/Node auf Blazor/Avalonia/.NET angepasst.
- `opencode/agent/subagents/code/codebase-pattern-analyst.md` technische Kontextbeispiele auf Blazor/Avalonia/ASP.NET Core aktualisiert.

## 2026-04-26 11:25:19 CEST
- Weitere Referenz-Bereinigung für C#/.NET:
  - `opencode/command/clarify.md`
  - `opencode/context/core/essential-patterns.md`
  - `opencode/agent/subagents/core/context-retriever.md`
- Verbleibende Beispiele von React/Node auf Blazor/Avalonia/.NET angepasst.

## 2026-04-26 11:50:21 CEST
- `opencode/command/translate.md` von npm/JS-Übersetzungsworkflow auf .NET-Lokalisierungsworkflow (z. B. `.resx` / `IStringLocalizer`) umgestellt.

## 2026-04-26 11:50:45 CEST
- `opencode/context/core/workflows/design-iteration.md` vollständig auf Blazor/Avalonia-.NET-Design-Workflow umgestellt (ohne Tailwind/Flowbite-Abhängigkeit).

## 2026-04-26 11:51:03 CEST
- `opencode/context/development/ui-styling-standards.md` vollständig auf .NET-UI-Styling-Standards (Blazor/Avalonia) umgestellt.

## 2026-04-26 11:51:17 CEST
- `opencode/context/development/design-assets.md` von Web-CDN/Tailwind-Referenzen auf .NET-UI-Asset-Referenz umgestellt.

## 2026-04-26 11:51:32 CEST
- `opencode/context/development/react-patterns.md` inhaltlich auf .NET-UI-Interaktionsmuster umgestellt (Pfad aus Kompatibilitätsgründen beibehalten).

## 2026-04-26 11:52:42 CEST
- React-zentrierte Skill-Bibliotheken vollständig entfernt:
  - `opencode/skills/react-best-practices/`
  - `opencode/skills/composition-patterns/`
- Ziel: konsequente Ausrichtung des Templates auf C#/.NET.

## 2026-04-26 11:54:22 CEST
- Weitere Restbereinigung auf C#/.NET:
  - `opencode/agent/subagents/code/build-agent.md` auf reine .NET-Buildvalidierung eingeschränkt.
  - `opencode/manifest.json` Node/npm-Hinweis entfernt.
  - `opencode/command/context.md` Toolchain-Beispiel auf .NET/NuGet/MSBuild angepasst.
  - `opencode/command/tasks.md` Datei- und Testbeispiele auf `.cs`/`.razor` und `dotnet test` umgestellt.
  - `opencode/skills/readme/SKILL.md` vollständig durch .NET-orientierte README-Skilldefinition ersetzt.
  - `opencode/skills/supabase-postgres-best-practices/README.md` Befehlsbeispiele auf `dotnet`-Workflow angepasst.
  - `opencode/skills/supabase-postgres-best-practices/references/_contributing.md` Sprach-/Buildreferenzen auf C#/.NET angepasst, inklusive korrigierter C#-Codebeispiele.

## 2026-04-26 11:54:47 CEST
- `opencode/tool/tsconfig.json` bereinigt: `bun-types` entfernt, um verbleibende JS/Bun-spezifische Referenz zu eliminieren.

## 2026-04-26 12:04:20 CEST
- `opencode/tool/package.json` angepasst:
  - Beschreibung auf aktuelle Env-Utility-Funktionalität korrigiert.
  - Test-Scripts von `bun test.ts` auf `bun test` umgestellt (kein harter Verweis auf fehlende Datei).
  - `@opencode-ai/plugin`-Version auf `1.2.15` angeglichen.

## 2026-04-26 12:04:40 CEST
- `opencode/tool/README.md` vollständig ersetzt: alte Gemini-Command-Dokumentation entfernt, neue Doku auf tatsächlich vorhandene Env-Utilities ausgerichtet.

## 2026-04-26 12:04:55 CEST
- Smoke-Tests ergänzt: `opencode/tool/env/index.test.ts` erstellt, damit `bun run test` konkrete Tests im Tool-Paket ausführt.

## 2026-04-26 12:05:19 CEST
- `opencode/context/index.md` vollständig bereinigt:
  - tote Context-Bäume (`content`, `product`, `data`, `learning`, `openagents-repo`) entfernt.
  - Agent-Mapping auf tatsächlich vorhandene Agenten reduziert.
  - Index auf reale Dateien und .NET-orientierte Nutzungshinweise ausgerichtet.

## 2026-04-26 12:05:39 CEST
- `opencode/tool/package.json` Paketname von `opencode-gemini-tool` auf `opencode-tool-utils` geändert, um die aktuelle Funktionalität korrekt abzubilden.

## 2026-04-26 12:05:49 CEST
- `opencode/tool/index.ts` Header-Kommentar bereinigt: alte Gemini-Beschreibung entfernt, Env-Utility-Zweck dokumentiert.

## 2026-04-26 12:14:48 CEST
- `opencode/tool/package.json` entfernt, um Bun/Node-Tooling aus dem Tool-Bereich zu eliminieren (Start der vollständigen C#/.NET-Migration von `opencode/tool`).

## 2026-04-26 12:15:46 CEST
- `opencode/tool` von TypeScript auf C#/.NET migriert:
  - Entfernt: `tsconfig.json`, `index.ts`, `env/index.ts`, `env/index.test.ts`
  - Hinzugefügt: `OpenCode.ToolUtils.csproj`, `Env/EnvLoader.cs`
  - Hinzugefügt: Testprojekt `OpenCode.ToolUtils.Tests` mit `EnvLoaderTests.cs`

## 2026-04-26 12:16:13 CEST
- `opencode/tool/README.md` auf C#/.NET-Projektstruktur aktualisiert (`dotnet build`, `dotnet test`, C#-API-Referenzen).

## 2026-04-26 12:17:09 CEST
- `opencode/skills/supabase-logs/scripts` von Node.js auf C#/.NET migriert:
  - Entfernt: `fetch-logs.js`
  - Hinzugefügt: `SupabaseLogsTool.csproj`, `Program.cs` (CLI für Supabase-Logabfrage via Management API)

## 2026-04-26 12:17:36 CEST
- `opencode/skills/supabase-logs/SKILL.md` auf C#/.NET-CLI-Nutzung aktualisiert (`dotnet run --project ...` statt Node.js-Skript).

## 2026-04-26 12:17:54 CEST
- `opencode/skills/supabase-logs/evals/evals.json` auf C#/.NET-CLI-Referenzen aktualisiert (`SupabaseLogsTool.csproj` statt `fetch-logs.js`).

## 2026-04-26 12:18:06 CEST
- `opencode/context/development/clean-code.md` entfernt, um die JS-lastige Fassung durch eine C#/.NET-Version zu ersetzen.

## 2026-04-26 12:18:29 CEST
- `opencode/context/development/clean-code.md` neu als C#/.NET-Clean-Code-Richtlinie erstellt (inkl. C#-Codebeispiele).

## 2026-04-26 12:18:38 CEST
- `opencode/context/development/api-design.md` entfernt, um die JS-lastige Fassung durch eine C#/.NET-Version zu ersetzen.

## 2026-04-26 12:19:01 CEST
- `opencode/context/development/api-design.md` neu als C#/.NET-API-Designleitfaden erstellt (REST + ASP.NET-Core-Beispiele).

## 2026-04-26 12:19:13 CEST
- `opencode/agent/core/openagent.md` angepasst: Aufrufsyntax-Codeblock von `javascript` auf neutrales `text` geändert.

## 2026-04-26 12:19:25 CEST
- `opencode/agent/core/opencoder.md` angepasst: Aufrufsyntax-Codeblock von `javascript` auf neutrales `text` geändert.

## 2026-04-26 12:19:35 CEST
- `opencode/agent/development/codebase-agent.md` angepasst: Aufrufsyntax-Codeblock von `javascript` auf neutrales `text` geändert.

## 2026-04-26 12:19:47 CEST
- `opencode/agent/subagents/code/codebase-pattern-analyst.md` entfernt, um die JS-lastige Fassung durch eine .NET-Variante zu ersetzen.

## 2026-04-26 12:20:14 CEST
- `opencode/agent/subagents/code/codebase-pattern-analyst.md` neu als .NET/C#-Variante erstellt (inkl. C#-Beispielen und `rg`-basiertem Suchworkflow).

## 2026-04-26 12:20:28 CEST
- `opencode/agent/subagents/core/context-retriever.md` bereinigt: tote Beispielpfade (`style-guide.md`, `best-practices.md`) durch vorhandene Context-Dateien ersetzt.

## 2026-04-26 12:20:49 CEST
- `opencode/command/validate-repo.md` entfernt, um die OpenAgents-/Registry-lastige Altfassung mit toten Referenzen zu ersetzen.

## 2026-04-26 12:21:07 CEST
- `opencode/command/validate-repo.md` neu als template-konformer Repo-Validator ohne tote Beispielpfade erstellt.

## 2026-04-26 12:21:41 CEST
- `opencode/agent/subagents/core/context-retriever.md` weiter bereinigt: Beispielstruktur von nicht vorhandenen `style-guide.md`/`best-practices.md` auf vorhandene `tests.md`/`clean-code.md` umgestellt.

## 2026-04-26 12:21:52 CEST
- `opencode/skills/supabase-postgres-best-practices/references/conn-prepared-statements.md` aktualisiert: Node.js-spezifischer Hinweis durch .NET/Npgsql-orientierten Hinweis ersetzt.

## 2026-04-26 12:22:14 CEST
- `opencode/skills/supabase-logs/scripts/Program.cs` bereinigt: lokale Variable `node` in `projects` umbenannt (sauberer C#-Kontext, keine missverständlichen Suchtreffer).

## 2026-04-26 12:52:15 CEST
- Verzeichnisstruktur auf OpenCode-Pluralpfade umgestellt:
  - `opencode/agent` -> `opencode/agents`
  - `opencode/command` -> `opencode/commands`
- C#-Utility-Projekt aus Tool-Definitionspfad verschoben:
  - `opencode/tool` -> `opencode/utilities/tool-utils`
- Neues Verzeichnis `opencode/tools/` mit Hinweisdatei `README.md` angelegt (reserviert für OpenCode-Custom-Tools).

## 2026-04-26 12:53:30 CEST
- `opencode/commands/validate-repo.md` korrigiert: Pfade auf `.opencode/agents/` und `.opencode/commands/` gesetzt.
- `opencode/skills/readme/SKILL.md` korrigiert: `name` auf `readme` gesetzt (entspricht Verzeichnisname).
- `opencode/.gitignore` erweitert: .NET-Artefakte (`bin/`, `obj/`, `.vs/`, `*.user`, `*.suo`) ergänzt.
- `opencode/skills/frontend-design/SKILL.md` vollständig auf .NET-UI-Kontext (Blazor/Avalonia) umgestellt.
- `opencode/skills/supabase-postgres-best-practices/references/_contributing.md` bereinigt: Python-Language-Tag entfernt.

## 2026-04-26 12:54:14 CEST
- `opencode/commands/validate-repo.md` erweitert: Validierungsscope ergänzt um `.opencode/tools/` (Custom-Tools) und `opencode/utilities/` (C#-Hilfsprojekte).

## 2026-04-26 12:54:26 CEST
- Repo-weite Pfadreferenzen nach Strukturmigration aktualisiert:
  - `opencode/agent` -> `opencode/agents`
  - `opencode/command` -> `opencode/commands`
  - `opencode/tool` -> `opencode/utilities/tool-utils`
  - `.opencode/agent` -> `.opencode/agents`
  - `.opencode/command` -> `.opencode/commands`
  - `.opencode/tool` -> `.opencode/tools`

## 2026-04-26 13:07:31 CEST
- `opencode/agents/subagents/code/build-agent.md` aktualisiert: alle `dotnet`-Bash-Regeln von `allow` auf `ask` gesetzt.

## 2026-04-26 13:07:54 CEST
- `opencode/agents/subagents/core/documentation.md` aktualisiert: Markdown-Edit-Regeln von `allow` auf `ask` gesetzt.

## 2026-04-26 13:08:09 CEST
- Neue zentrale Policy-Datei `opencode/config.json` hinzugefügt:
  - globale Permissions auf `ask` gesetzt (`*`, `bash`, `edit`, `task`, `webfetch`)
  - Agent-Overrides für `build` und `build-agent` auf `ask` gesetzt.

## 2026-04-26 13:10:00 CEST
- Permission-Härtung in schreibenden Agenten umgesetzt (explizite `ask`-Wildcards ergänzt):
  - `opencode/agents/core/openagent.md`
  - `opencode/agents/core/opencoder.md`
  - `opencode/agents/development/codebase-agent.md`
  - `opencode/agents/development/frontend-specialist.md`
  - `opencode/agents/subagents/code/tester.md`
  - `opencode/agents/subagents/code/coder-agent.md`
  - `opencode/agents/subagents/core/task-manager.md`
  - `opencode/agents/subagents/core/documentation.md`
  - `opencode/agents/subagents/code/build-agent.md`
- Ergebnis-Check: keine `\"allow\"`-Regeln mehr unter `opencode/agents`.
