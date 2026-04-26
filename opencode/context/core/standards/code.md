<!-- Context: standards/code | Priority: critical | Version: 3.0 | Updated: 2026-04-26 -->
# Code Standards (.NET / C#)

## Quick Reference

**Core Philosophy**: Clean Code, KISS, SOLID, testable architecture  
**Primary Stack**: .NET, C#, Blazor, AvaloniaUI  
**Golden Rule**: If code is hard to understand or hard to test, simplify and refactor.

**Required Principles**:
- Keep classes focused (Single Responsibility)
- Prefer composition over inheritance
- Depend on abstractions (interfaces)
- Keep methods small and intention-revealing
- Favor immutable data where practical
- Use async/await correctly (no blocking `.Result` / `.Wait()`)

---

## Architecture Principles

- Use layered boundaries: UI -> Application -> Domain -> Infrastructure
- UI frameworks (Blazor/Avalonia) should orchestrate, not contain business logic
- Domain logic must be framework-agnostic
- External systems (DB, API, filesystem, clock) go behind interfaces
- Inject dependencies, do not instantiate infrastructure in business code

## C# Conventions

- Types/methods/properties: `PascalCase`
- Parameters/locals/fields: `camelCase`
- Private readonly fields: `_camelCase`
- Interfaces: prefix with `I`
- Async methods: suffix `Async`
- Constants: `PascalCase` for public constants, `_camelCase` for private readonly preferred

## File and Project Structure

```text
src/
  MyApp.Domain/
    Entities/
    ValueObjects/
    Services/
  MyApp.Application/
    UseCases/
    Contracts/
  MyApp.Infrastructure/
    Persistence/
    Integrations/
  MyApp.Presentation.Blazor/ or MyApp.Presentation.Avalonia/
tests/
  MyApp.UnitTests/
  MyApp.IntegrationTests/
```

## Clean Code and KISS

- One reason to change per class
- Prefer explicit, simple control flow over clever abstractions
- Keep methods generally under ~30 lines unless justified
- Avoid deep nesting; use guard clauses and early returns
- Use expressive names instead of comments where possible
- Add comments only for non-obvious constraints or decisions

## Blazor Guidelines

- Keep `.razor` components thin; move logic to services/view-model style classes
- Use `@code` blocks for UI state only, not domain logic
- Avoid heavy work in render paths
- Use cascading parameters and DI intentionally, not globally by default
- Validate forms with clear models and data annotations or FluentValidation integration

## AvaloniaUI Guidelines

- Keep code-behind minimal; prefer MVVM
- Bind UI to view models, not directly to infrastructure
- Use `INotifyPropertyChanged` (or toolkit helpers) consistently
- Keep commands focused and testable
- Avoid visual-tree hacks when a proper binding/template solution exists

## Design Patterns (Use Pragmatically)

- **Recommended**: Strategy, Factory, Adapter, Decorator, Mediator, Repository (when justified)
- **Avoid overengineering**: Do not apply patterns without a concrete problem
- Use CQRS selectively for complex domains, not by default

## Error Handling

- Fail fast at boundaries with clear validation messages
- Throw domain-specific exceptions only when needed
- Do not swallow exceptions silently
- Convert technical failures into user-meaningful responses at application/UI boundaries

```csharp
public sealed class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result<Order>> CreateAsync(CreateOrderCommand command, CancellationToken ct)
    {
        if (command.Items.Count == 0)
        {
            return Result<Order>.Failure("Order must contain at least one item.");
        }

        var order = Order.Create(command.CustomerId, command.Items);
        await _orderRepository.SaveAsync(order, ct);
        return Result<Order>.Success(order);
    }
}
```

## Testing Expectations in Code Design

- Design for testability: pure domain logic, injected dependencies
- Avoid static/global mutable state
- Minimize hidden side effects
- Prefer deterministic behavior (inject clock/random providers when needed)

## Anti-Patterns

- Fat UI components with embedded domain logic
- God classes/services
- Excessive inheritance hierarchies
- Premature abstraction
- Blocking async code
- Catch-all exceptions without action/context
- Pattern cargo-culting (patterns without real need)

## Done Criteria (Code Quality)

- Code is readable without long explanation
- Boundaries and responsibilities are clear
- Build passes with `dotnet build`
- Tests pass with `dotnet test`
- No obvious KISS/SOLID violations remain
