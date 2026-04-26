# Project Context (.NET / C#)

## Execution Rules

- Follow instructions exactly; do not bypass quality gates to force success.
- Prefer simple and explicit solutions (KISS).
- Keep architecture boundaries intact: UI -> Application -> Domain -> Infrastructure.
- Never expose secrets in code or logs.

## Default Tooling

- Restore: `dotnet restore`
- Build: `dotnet build`
- Test: `dotnet test`
- Format/Analyzers: `dotnet format --verify-no-changes`

If a command fails because of the current directory, run `pwd` and execute from the correct solution root.

## End-of-Task Checklist

Before handoff, run and fix where applicable:

- `dotnet build`
- `dotnet test`
- `dotnet format --verify-no-changes`

If a step is not runnable in the current repo, state that clearly in the handoff.

## Architecture Guidance

### Core Layers

- **Domain**: entities, value objects, domain services, business rules
- **Application**: use cases, orchestration, interfaces/contracts
- **Infrastructure**: DB, external APIs, filesystem, adapters
- **Presentation**: Blazor/Avalonia UI and interaction wiring

### Dependency Direction

- Presentation depends on Application
- Application depends on Domain abstractions
- Infrastructure implements Application/Domain contracts
- Domain should not depend on UI or infrastructure

## Blazor Guidance

- Keep components focused and reusable
- Move business logic to services/use-case handlers
- Use explicit parameters and predictable state updates
- Avoid uncontrolled rerender loops in lifecycle methods

## Avalonia Guidance

- Prefer MVVM
- Keep view models testable and framework-light
- Use commands and bindings instead of imperative UI logic
- Dispose subscriptions/resources properly

## Design and Quality

- Apply design patterns pragmatically (Strategy, Adapter, Factory, Decorator, Mediator)
- Avoid pattern overuse when simple code is clearer
- Follow Clean Code and SOLID principles
- Keep functions/classes small and intention-revealing
