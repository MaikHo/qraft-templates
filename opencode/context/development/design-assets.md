# Design Assets Reference (.NET UI)

## Purpose

Approved asset sources and integration guidance for Blazor and Avalonia projects.

## Icons

- Prefer consistent icon sets across the app.
- Typical options:
  - Fluent UI icons
  - Material Symbols
  - Font Awesome (if already in project)

## Typography

- Use one primary UI font family and one fallback stack.
- Keep heading/body scale consistent across screens.
- Avoid mixing many font families in one product.

## Images and Illustrations

- Use project-owned assets where possible.
- For placeholders in prototypes, use neutral placeholder services.
- Do not commit copyrighted assets without permission.

## Charts and Data Visualization

- Blazor: use established chart libraries already adopted in project.
- Avalonia: use reusable chart controls with clear style integration.

## Theme Assets

- Store colors/brushes as shared tokens/resources.
- Keep dark/light theme resources in dedicated files.
- Avoid hardcoded color literals in component code where possible.

## Quality Checklist

- Asset license verified
- Visual consistency validated
- Accessibility impact reviewed
- No dead/unused heavy asset bundles
