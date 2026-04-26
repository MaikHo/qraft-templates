---
description: Enforce full UI localization for .NET projects (Blazor/Avalonia/ASP.NET)
---

# Translate Command (.NET)

Goal: zero unresolved localization issues in user-facing UI.

## Localization Rules

- No hardcoded UI text in components/views.
- Use project localization abstractions (`IStringLocalizer`, generated resources, or project-specific wrappers).
- Keep resource keys semantic and stable.
- Keep locale resource files synchronized (e.g., `.resx` sets).

## Workflow

1. Discover localization resources and current usage.
2. Find hardcoded user-facing strings in changed scope (or whole repo if requested).
3. Replace hardcoded text with localization keys.
4. Add/update resource entries in all required locales.
5. Run project validation:
   - `dotnet build`
   - `dotnet test` (or relevant localization tests)
6. Report changed files, keys added/updated, and validation results.

## Done Criteria

- No known hardcoded UI strings remain in scope.
- All required locale resources contain matching keys.
- Build/tests pass for localization-related changes.
