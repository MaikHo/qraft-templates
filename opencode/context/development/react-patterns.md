# .NET UI Interaction Patterns (Blazor/Avalonia)

> Legacy note: This file replaces the previous React-specific guidance to keep path compatibility for existing references.

## State Management

- Keep UI state local to component/viewmodel unless shared needs justify elevation.
- Keep business state in application/domain services.
- Prefer explicit state transitions over implicit side effects.

## Async Interaction

- Use async/await end-to-end; avoid blocking calls.
- Use cancellation tokens for user-triggered replaceable actions.
- Prevent duplicate in-flight requests for the same operation.

## Event Handling

- Keep handlers short; delegate business logic to services/use-cases.
- Debounce high-frequency user events where needed.
- Validate input at boundaries before invoking application services.

## Rendering and Updates

- Avoid repeated expensive computations in render/update loops.
- Cache deterministic expensive computations when justified.
- For list-heavy screens, use virtualization/paging patterns.

## Reusability

- Favor reusable components with clear parameters.
- Avoid god-components/god-viewmodels.
- Keep naming and behavior predictable.

## Testing Focus

- Unit test view model/service behavior.
- Component test critical interaction flows.
- Keep tests deterministic and framework-aware.
