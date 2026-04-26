# Clean Code Principles (.NET / C#)

**Category**: development  
**Purpose**: Core coding standards and best practices for maintainable C# code  
**Used by**: frontend-specialist, devops-specialist, codebase-agent

---

## Overview

Clean code is easy to read, test, and change. In C#/.NET this means clear
boundaries, small focused methods, explicit dependencies, and predictable
error handling.

## Core Principles

### 1. Meaningful Names

```csharp
// Bad
var d = DateTime.UtcNow;
var x = GetUserData();

// Good
var currentUtcTime = DateTime.UtcNow;
var activeUserProfile = GetUserData();
```

### 2. Single Responsibility

```csharp
// Better: orchestration method with focused collaborators
public async Task<User> ProcessUserAsync(User user, CancellationToken ct)
{
    var validatedUser = _validator.Validate(user);
    var savedUser = await _repository.SaveAsync(validatedUser, ct);
    await _notifier.NotifyAsync(savedUser, ct);
    return savedUser;
}
```

### 3. Avoid Deep Nesting

```csharp
public Result ProcessOrder(Order? order)
{
    if (order is null) return Result.Fail("Order is required.");
    if (order.Items.Count == 0) return Result.Fail("At least one item is required.");
    if (order.Total <= 0) return Result.Fail("Total must be greater than zero.");

    return Result.Ok();
}
```

### 4. DRY with Intent

- Extract shared logic only when duplication is meaningful
- Prefer composition over inheritance for reuse
- Keep abstractions practical, not speculative

### 5. Explicit Error Handling

```csharp
public async Task<UserProfile> FetchProfileAsync(Guid userId, CancellationToken ct)
{
    try
    {
        return await _apiClient.GetProfileAsync(userId, ct);
    }
    catch (HttpRequestException ex)
    {
        _logger.LogError(ex, "Failed to fetch user profile for {UserId}", userId);
        throw new ExternalDependencyException("Unable to retrieve user profile.", ex);
    }
}
```

## C#/.NET Best Practices

1. Use `async`/`await` end-to-end; avoid `.Result`/`.Wait()`
2. Depend on abstractions (`I...`) at boundaries
3. Keep methods small and intention-revealing
4. Prefer immutable models where practical
5. Validate inputs at boundaries
6. Use `CancellationToken` in async I/O operations
7. Write focused unit tests for business logic
8. Keep UI layers free of domain logic

## Anti-Patterns

- God services/classes
- Hidden dependencies / service locator usage
- Swallowing exceptions silently
- Overly generic utility classes without domain meaning
- Mutable global/shared state without clear ownership

## References

- Clean Code (Robert C. Martin)
- Framework Design Guidelines (.NET)
- ASP.NET Core and .NET architecture guidance
