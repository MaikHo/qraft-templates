# .NET UI Patterns (Blazor + AvaloniaUI)

## Purpose

Guidelines for building maintainable UI applications with Blazor and AvaloniaUI using Clean Code, KISS, and pragmatic design patterns.

## Core Principles

- Keep UI thin, keep domain logic out of UI layer
- Prefer explicit, readable state transitions
- Use dependency injection for services
- Separate responsibilities: rendering vs orchestration vs business logic
- Avoid framework lock-in in domain/application layers

## Blazor Patterns

- Use component parameters for explicit data flow
- Extract business workflows to services/use-cases
- Use `EditForm` and validators consistently
- Minimize logic in `.razor` markup files
- Use cancellation tokens for long-running async flows

## Avalonia Patterns

- Prefer MVVM
- Keep `ViewModel` free of UI framework specifics where possible
- Use commands for user actions
- Use bindings and templates over imperative UI manipulation
- Keep platform-specific details at the edges

## Design Pattern Guidance

- Strategy: interchangeable UI behavior/policies
- Adapter: integrate legacy or third-party APIs
- Factory: controlled creation of complex objects
- Decorator: extend behaviors without modifying base classes
- Mediator: coordinate decoupled interactions in larger modules

Use patterns only when they reduce complexity; otherwise keep direct and simple (KISS).

## Performance Basics

- Avoid unnecessary rerenders/state churn
- Debounce noisy inputs where needed
- Keep large lists virtualized/paginated
- Cache expensive lookups with clear invalidation rules

## Testing Focus

- Unit-test view models and services
- Component-test critical UI behavior
- Keep tests deterministic and independent of external systems
