# Context Index

## Scope

Dieser Index listet ausschließlich Context-Dateien, die in diesem Repository
unter `opencode/context` tatsächlich vorhanden sind.

## Core Context

Pfad: `.opencode/context/core/{category}/{file}`

### Standards

- `code` -> `core/standards/code.md` (critical)
- `docs` -> `core/standards/docs.md` (critical)
- `tests` -> `core/standards/tests.md` (critical)
- `patterns` -> `core/standards/patterns.md` (high)
- `analysis` -> `core/standards/analysis.md` (high)

### Workflows

- `delegation` -> `core/workflows/delegation.md` (high)
- `review` -> `core/workflows/review.md` (high)
- `breakdown` -> `core/workflows/task-breakdown.md` (high)
- `design-iteration` -> `core/workflows/design-iteration.md` (high)
- `sessions` -> `core/workflows/sessions.md` (medium)

### System

- `context-guide` -> `core/system/context-guide.md` (medium)
- `context-bundle-schema` -> `core/system/context-bundle-schema.md` (high)

## Development Context

Pfad: `.opencode/context/development/{file}`

- `clean-code.md`
- `dotnet-ui-patterns.md`
- `react-patterns.md` (Kompatibilitätspfad, enthält .NET-UI-Patterns)
- `api-design.md`
- `animation-patterns.md`
- `design-systems.md`
- `ui-styling-standards.md`
- `design-assets.md`

## Project Context

Pfad: `.opencode/context/project/{file}`

- `project-context.md`

## Agent Mapping (aktuell vorhanden)

Verwendet durch Agenten unter `opencode/agents`:

- `openagent`
- `opencoder`
- `codebase-agent`
- `frontend-specialist`
- `supabase-specialist`
- `devops-specialist`

## Loading Hint

Für normale Entwicklungsaufgaben:

1. Lade `core/standards/code.md`
2. Ergänze bei Bedarf mit `core/standards/tests.md` und `core/standards/docs.md`
3. Für UI-Arbeit nutze `development/dotnet-ui-patterns.md` zusätzlich
