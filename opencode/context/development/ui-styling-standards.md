# UI Styling Standards (.NET UI)

## Scope

Styling conventions for Blazor and AvaloniaUI projects.

## Principles

- Consistency over novelty
- Accessibility by default
- Clear visual hierarchy
- Reusable style tokens/components
- Keep styling simple (KISS)

## Tokens

- Define shared tokens for:
  - Color roles (primary, secondary, success, warning, danger, surface, text)
  - Typography scale
  - Spacing scale
  - Radius and elevation

## Blazor Guidance

- Prefer component-scoped styling and reusable CSS classes
- Avoid inline styles except for dynamic runtime-calculated values
- Keep theme variables centralized
- Ensure responsive behavior for desktop/tablet/mobile targets

## Avalonia Guidance

- Use Styles and ControlThemes consistently
- Centralize brushes, spacing, and font resources
- Prefer reusable templates over per-control one-off styles
- Keep XAML styles readable and modular

## Accessibility

- Maintain sufficient contrast
- Provide visible focus states
- Ensure keyboard navigation paths
- Use semantic labels/automation properties where applicable

## Validation

- Verify style changes in target views/screens
- Confirm no regressions in readability and interaction
