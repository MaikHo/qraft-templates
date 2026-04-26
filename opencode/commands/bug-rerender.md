---
description: Investigate and fix repeated render / repeated resource loading bugs in Blazor or AvaloniaUI using codebase analysis and user observations
---

# bug-rerender (.NET UI)

Focus: repeated rendering, repeated API/resource loading, event-loop feedback bugs in Blazor/Avalonia.

## Triage Checklist

1. Identify target component/view/viewmodel.
2. Trace state updates that trigger rerender cycles.
3. Inspect async/event handlers for feedback loops.
4. Check subscriptions/timers and proper cleanup/disposal.
5. Confirm data-loading guards to avoid duplicate calls.

## Common Causes

- State updates triggered from render lifecycle without guard
- `PropertyChanged` or event handlers re-triggering themselves
- Missing debounce/throttle on fast UI events
- Repeated `OnParametersSetAsync`/`OnAfterRenderAsync` loads in Blazor
- Missing disposal/unsubscribe in Avalonia view models/services

## Fix Order

1. Add guard conditions and idempotency checks.
2. Stabilize async workflows and cancellation.
3. Separate initialization from reactive updates.
4. Add safe disposal for subscriptions/timers.
5. Re-test with user verification checklist.
