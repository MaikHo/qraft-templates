# Validate Repository

Validate consistency of the local OpenCode template files in this repository.

## Usage

```bash
/validate-repo
```

## Validation Scope

1. Agent files under `.opencode/agents/`
2. Command files under `.opencode/commands/`
3. Context files under `.opencode/context/`
4. Skill files under `.opencode/skills/`
5. Tool definition files under `.opencode/tools/` (if used)
6. C# utility projects under `opencode/utilities/` (if used)
7. Manifest/config consistency

## Checks

- Referenced files exist
- No broken internal references in key docs
- Context index paths match actual files
- Command and agent docs avoid dead example paths
- Basic JSON/Markdown structure sanity

## Output

- ✅ Valid checks
- ⚠️ Warnings for potential drift
- ❌ Errors for missing/broken references

## Example Report Template

```markdown
# Repository Validation Report

Generated: <timestamp>

## Summary
- ✅ Passed checks: <n>
- ⚠️ Warnings: <n>
- ❌ Errors: <n>

## ✅ Validated
- Agent files resolve correctly
- Command files resolve correctly
- Context index points to existing paths

## ⚠️ Warnings
1. <warning item>

## ❌ Errors
1. <error item>

## Recommended Actions
1. Fix all errors first
2. Address warnings with highest impact
3. Re-run validation
```
