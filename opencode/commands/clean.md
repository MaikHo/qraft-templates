---
description: Clean the codebase or current task using .NET formatting, analyzers, and build checks
---

# Code Quality Cleanup (.NET)

## Process

1. Analyze target scope from `$ARGUMENTS` or modified files.
2. Remove debug artifacts and dead/commented-out code.
3. Apply formatting with `dotnet format`.
4. Resolve analyzer warnings/errors where relevant.
5. Validate with `dotnet build`.
6. Run targeted `dotnet test` for changed areas when applicable.

## Output Format

- Files processed
- Actions taken
- Remaining manual issues
- Validation results (`dotnet format`, `dotnet build`, `dotnet test`)
