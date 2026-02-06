using backend.services;

namespace backend.middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IJwtService jwtService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            var teacherId = jwtService.ValidateToken(token);
            if (teacherId != null)
            {
                // Attach teacher id to context on successful jwt validation
                context.Items["TeacherId"] = teacherId;
            }
        }

        await _next(context);
    }
}
