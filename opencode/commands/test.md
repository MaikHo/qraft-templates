---
description: Run the complete .NET testing pipeline
---

# Testing Pipeline (.NET)

Run this sequence:

1. `dotnet restore`
2. `dotnet build`
3. `dotnet test`
4. Report failures
5. Fix failures
6. Re-run until green
7. Report success with affected test projects
