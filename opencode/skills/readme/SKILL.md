---
name: readme
description: Create and improve high-quality README files for .NET/C# projects, including setup, build, test, architecture, and contribution guidance.
license: MIT
metadata:
  author: opencode
  version: "2.0.0"
---

# README Quality Skill (.NET)

Use this skill when creating or improving README documentation for C#/.NET repositories.

## Required README Sections

1. Project purpose (short and concrete)
2. Tech stack (.NET version, app type, key libraries)
3. Prerequisites (SDK/runtime/tooling)
4. Setup steps
5. Build and run commands
6. Test commands
7. Architecture overview
8. Contribution workflow
9. Troubleshooting notes

## Command Conventions

Use .NET-first commands in examples:

```bash
dotnet restore
dotnet build
dotnet test
dotnet run --project src/MyApp
```

## Writing Principles

- Prefer concise, actionable instructions
- Keep commands copy/paste ready
- Explain why key decisions exist
- Keep terminology consistent
- Update docs whenever behavior changes

## Quality Checklist

- README reflects current repo structure
- Setup instructions actually work
- Build/test commands are accurate
- Architecture section matches implementation boundaries
- No stale references to old tooling/frameworks
