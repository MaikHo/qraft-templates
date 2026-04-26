# API Design Patterns (.NET / ASP.NET Core)

**Category**: development  
**Purpose**: API design principles for REST-first .NET backends  
**Used by**: codebase-agent, devops-specialist

---

## REST Fundamentals

### Resource-Based Routing

```
GET    /users
POST   /users
GET    /users/{id}
PUT    /users/{id}
PATCH  /users/{id}
DELETE /users/{id}
```

### Status Codes

- `200 OK` read/update success
- `201 Created` create success
- `204 No Content` delete success
- `400 Bad Request` invalid request format
- `401 Unauthorized` missing/invalid auth
- `403 Forbidden` insufficient permissions
- `404 Not Found` resource missing
- `409 Conflict` state conflict
- `422 Unprocessable Entity` validation failure
- `500 Internal Server Error` unexpected server failure

## Consistent Response Contracts

### Success

```json
{
  "data": {
    "id": "123",
    "name": "Jane Doe"
  },
  "meta": {
    "requestId": "req-abc123"
  }
}
```

### Error

```json
{
  "error": {
    "code": "VALIDATION_ERROR",
    "message": "One or more fields are invalid.",
    "details": [
      {
        "field": "email",
        "message": "Email format is invalid."
      }
    ]
  },
  "meta": {
    "requestId": "req-abc123"
  }
}
```

## ASP.NET Core Example

```csharp
[ApiController]
[Route("api/users")]
public sealed class UsersController : ControllerBase
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var user = await _userService.GetByIdAsync(id, ct);
        if (user is null)
        {
            return NotFound();
        }

        return Ok(new { data = user, meta = new { requestId = HttpContext.TraceIdentifier } });
    }
}
```

## Validation and Error Handling

- Validate DTOs at API boundaries
- Use centralized exception handling middleware
- Return stable error codes for clients
- Avoid exposing internal exception details in production

## Security

- Enforce HTTPS
- Use JWT/OIDC for authentication
- Use policy-based authorization
- Apply rate limiting for sensitive endpoints
- Log security-relevant events with correlation ids

## Performance

- Prefer pagination for list endpoints
- Avoid N+1 queries in data access layer
- Use response caching where appropriate
- Make expensive endpoints asynchronous and cancellable

## Versioning

- Prefer URL versioning (`/api/v1/...`) or header versioning, but stay consistent
- Provide deprecation windows and migration guidance
- Keep old versions stable until sunset date
