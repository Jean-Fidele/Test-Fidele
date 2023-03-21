using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Test_Fidele.Systems;

public static class VersionMiddlewareExtensions
{
    public static IApplicationBuilder UseVersionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<VersionMiddleware>();
    }
}

public class VersionMiddleware
{
    private static readonly string Version = (Assembly.GetEntryAssembly() ?? throw new InvalidOperationException())
        .GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

    private readonly RequestDelegate _next;

    public VersionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context?.Response?.Headers != null)
        {
            if (context.Response.Headers.ContainsKey("Access-Control-Expose-Headers") == false)
            {
                context.Response.Headers.Add("Access-Control-Expose-Headers", "X-Application-Version");
            }
        }

        if (context?.Response?.Headers != null)
        {
            if (context.Response.Headers.ContainsKey("X-Application-Version") == false)
            {
                context.Response.Headers.Add("X-Application-Version", Version);
            }
        }

        await _next(context);
    }
}