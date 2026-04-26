<!-- Context: standards/docs | Priority: critical | Version: 3.0 | Updated: 2026-04-26 -->

# Documentation Standards (.NET / C#)

## Quick Reference

**Golden Rule**: If teammates ask the same question twice, document it.  
**Priorities**: Clarity, reproducibility, and maintainability.

**Document this**:
- Architecture decisions (why)
- Public APIs/contracts
- Setup, build, run, and test flows
- Non-obvious framework constraints (Blazor/Avalonia)

**Don't document this**:
- Obvious line-by-line code behavior
- Redundant comments that repeat code names

---

## README Baseline

```markdown
# Project Name
Short description

## Tech Stack
- .NET version
- C# version
- UI: Blazor and/or Avalonia

## Prerequisites
- .NET SDK (version)

## Setup
```bash
dotnet restore
dotnet build
```

## Run
```bash
dotnet run --project src/MyApp.Presentation.Blazor
```

## Test
```bash
dotnet test
```

## Architecture
[Short overview + links]
```

## API and Code Examples

```csharp
/// <summary>
/// Calculates final price including VAT.
/// </summary>
/// <param name="netPrice">Net amount.</param>
/// <param name="vatRate">VAT rate in decimal form (0.19 = 19%).</param>
/// <returns>Gross amount.</returns>
public static decimal CalculateGross(decimal netPrice, decimal vatRate)
{
    return netPrice * (1 + vatRate);
}
```

## What Good Docs Must Explain

- Why a design/pattern was chosen
- Expected input/output contracts
- Error handling behavior
- How to validate changes locally
- Known limitations and explicit trade-offs

## Blazor/Avalonia Notes

- Document component ownership and data flow
- Note where state is stored and how it is updated
- For Avalonia, describe MVVM bindings and command responsibilities
- For Blazor, document DI usage and component boundaries

## Best Practices

- Keep command examples copy/paste ready
- Keep docs close to code changes
- Prefer concrete examples over abstract descriptions
- Keep terms consistent across repo
- Remove stale docs immediately when behavior changes
