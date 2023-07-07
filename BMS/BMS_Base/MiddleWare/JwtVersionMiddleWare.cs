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
        var pwd = context.Request.Headers["JwtVersion"];
     if(!string.IsNullOrEmpty(pwd)) Console.WriteLine($"JwtVersion={pwd}");
        await _next(context);

    }
}