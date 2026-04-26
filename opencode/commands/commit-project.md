---
description: Create atomic project commits with .NET validation and push
---

# Commit Project (.NET)

## Validation

Run before committing:

- `dotnet restore`
- `dotnet format --verify-no-changes`
- `dotnet build`
- `dotnet test`

If validation fails, report and request direction if fixes are non-trivial.

## Commit Strategy

- Keep commits atomic by intent.
- Separate refactor/formatting from behavior changes.
- Use Conventional Commits with optional scope.
- Push after successful commit sequence.

## Output

- Commands executed
- Commit hashes + subject lines
- Files included per commit
