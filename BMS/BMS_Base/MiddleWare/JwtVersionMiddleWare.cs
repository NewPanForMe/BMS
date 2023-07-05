using Microsoft.AspNetCore.Http;

namespace BMS_Base.MiddleWare;

public class JwtVersionMiddleWare
{
    private readonly RequestDelegate _next;
    public JwtVersionMiddleWare(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var pwd = context.Request.Query["password"];

        await _next(context);

    }
}