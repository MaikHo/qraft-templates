<!-- Context: standards/tests | Priority: critical | Version: 3.0 | Updated: 2026-04-26 -->

# Testing Standards (.NET / C#)

## Quick Reference

**Golden Rule**: If you can't test it easily, refactor it.  
**Test Style**: Arrange -> Act -> Assert (AAA)  
**Primary Tooling**: `dotnet test`, xUnit/NUnit, Moq/NSubstitute/FakeItEasy

**Test this**:
- Business rules and domain logic
- Application use-cases and contracts
- Error and edge-case behavior

**Don't test this**:
- .NET framework internals
- Trivial getters/setters
- Third-party packages themselves

---

## Principles

- Test behavior, not implementation details
- Keep tests deterministic and isolated
- Prefer clear scenario names over short cryptic names
- One reason to fail per test
- No flaky time/network dependencies (mock or fake boundaries)

## AAA Example

```csharp
[Fact]
public void CalculateTotal_ReturnsSumOfItemPrices()
{
    // Arrange
    var items = new[] { 10m, 20m, 30m };
    var calculator = new PriceCalculator();

    // Act
    var result = calculator.CalculateTotal(items);

    // Assert
    Assert.Equal(60m, result);
}
```

## Coverage Goals

1. Critical domain/application logic: 100%
2. Public APIs and major user flows: >= 90%
3. Utilities and adapters: >= 80%
4. Boilerplate/config wrappers: case-by-case

## Dependency and Integration Testing

- Unit tests: mock external dependencies
- Integration tests: use real components for selected boundaries
- Keep integration tests explicit and slower; keep unit tests fast

```csharp
[Fact]
public async Task GetUserAsync_ReturnsUserFromRepository()
{
    // Arrange
    var repo = Substitute.For<IUserRepository>();
    repo.GetByIdAsync(42, Arg.Any<CancellationToken>())
        .Returns(new User(42, "Mia"));

    var service = new UserService(repo);

    // Act
    var user = await service.GetUserAsync(42, CancellationToken.None);

    // Assert
    Assert.NotNull(user);
    Assert.Equal("Mia", user!.Name);
}
```

## Naming Conventions

- `MethodName_Condition_ExpectedResult`
- or clear sentence-style names with `[Fact(DisplayName = "...")]`
- Keep names descriptive and scenario-specific

## Blazor and Avalonia Testing Notes

- Blazor components: use component tests (e.g., bUnit) for rendering and interaction logic
- Avalonia UI: keep VM logic unit-testable; test commands/state changes without full UI boot when possible
- UI snapshot/screenshot tests are optional and should not replace behavior tests

## Anti-Patterns

- Testing private methods directly
- Over-mocking every collaborator without intent
- Asserting irrelevant implementation details
- Sharing mutable fixture state across tests
- Sleeping/retrying blindly to pass unstable tests

## Done Criteria (Testing)

- Relevant unit/integration tests added or updated
- `dotnet test` passes
- New behavior has positive + negative coverage where applicable
- Tests are readable and maintainable
