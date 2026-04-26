---
description: Create small, coherent conventional commits for .NET projects
---

# Commit Command (.NET)

1. Run pre-commit validation:
   - `dotnet build`
   - `dotnet test` (or impacted test projects)
2. Analyze `git status --porcelain` and `git diff --cached`.
3. Group changes by intent (do not mix unrelated concerns).
4. Stage only relevant files/hunks.
5. Commit using Conventional Commits (`feat|fix|docs|refactor|test|chore|style`).
6. Push branch and report commit hashes with short summaries.

Rules:
- Never auto-stage everything blindly.
- Split commits when intents differ.
- Keep subject line concise and imperative.
