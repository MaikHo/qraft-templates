# Design Iteration Workflow (.NET UI)

## Purpose

Iterative UI design workflow for Blazor and Avalonia projects with clear review gates and maintainable implementation.

## Stages

1. **Discovery**
- Clarify user goals, screens, and workflows.
- Identify target framework: Blazor or Avalonia.

2. **Structure**
- Draft screen/component hierarchy.
- Define state ownership and data flow.

3. **Visual System**
- Define typography, spacing, color, and interaction tokens.
- Align with accessibility requirements.

4. **Implementation**
- Build UI in small increments.
- Keep business logic outside UI layer.

5. **Validation**
- Build and test:
  - `dotnet build`
  - relevant tests (`dotnet test`)
- Capture iteration notes and follow-up actions.

## Review Gates

- Gate A: structure approved
- Gate B: visual direction approved
- Gate C: behavior and validation approved

## Output Per Iteration

- Changed files
- UX/UI rationale
- Validation output
- Next iteration scope
