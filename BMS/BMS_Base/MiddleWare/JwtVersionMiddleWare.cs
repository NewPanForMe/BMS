using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Ys.Tools.Models;

namespace BMS_Base.MiddleWare;

public class JwtVersionMiddleWare
{
    private readonly RequestDelegate _next;

    public JwtVersionMiddleWare(RequestDelegate next, IMemoryCache memoryCache)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var findFirst = context.User.FindFirst(x=>x.Type==ClaimTypes.Version);
        if (!string.IsNullOrEmpty(findFirst?.Value))
        {
            Console.WriteLine($"JwtVersion={findFirst?.Value}");
        }
        await _next(context);
    }
}