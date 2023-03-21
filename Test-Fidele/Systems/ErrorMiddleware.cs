using System.Net;
using System.Text.Json;
using Domain.Exceptions;

namespace Test_Fidele.Systems;

public static class ErrorMiddlewareExtensions
{
    public static IApplicationBuilder UserErrorMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorMiddleware>();
    }
}

public class ErrorMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorMiddleware> _logger;
    private readonly IHostEnvironment _hostEnvironment;

    public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger, IHostEnvironment hostEnvironment)
    {
        _next = next;
        _logger = logger;
        _hostEnvironment = hostEnvironment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            //if (context.Request.ContentLength.HasValue && context.Request.ContentLength > 0)
            //{
            //    using (var reader = new StreamReader(
            //        context.Request.Body,
            //        encoding: Encoding.UTF8,
            //        detectEncodingFromByteOrderMarks: false,
            //        bufferSize: 1024,
            //        leaveOpen: true))
            //    {
            //        var body = await reader.ReadToEndAsync();
            //        // Do some processing with body

            //        // Get the body data    
            //        byte[] bodyData = Encoding.UTF8.GetBytes(body);

            //        // Put a new stream with that data in the body
            //        context.Request.Body = new MemoryStream(bodyData);

            //    }
            //}

            await _next(context);
        }
        catch (BadRequestException e)
        {
            _logger.LogInformation("Request: {@request}", StandardEnricher(context));
            _logger.LogWarning(e, e.Message);
            await SetResponse(context, HttpStatusCode.BadRequest, _hostEnvironment.IsProduction() ? "You don’t know the power of the dark side" : e.Message);
        }
        catch (DomainException e)
        {
            _logger.LogInformation("Request: {@request}", StandardEnricher(context));
            _logger.LogWarning(e, e.Message);
            await SetResponse(context, HttpStatusCode.BadRequest, _hostEnvironment.IsProduction() ? "You have controlled your fear. Now, release your anger" : e.Message);
        }
        catch (NotFoundException e)
        {
            _logger.LogInformation("Request: {@request}", StandardEnricher(context));
            _logger.LogWarning(e, e.Message);
            await SetResponse(context, HttpStatusCode.NotFound, _hostEnvironment.IsProduction() ? "No… I am your Father…" : e.Message);
        }
        catch (Exception e)
        {
            _logger.LogInformation("Request: {@request}", StandardEnricher(context));
            _logger.LogError(e, e.Message);
            await SetResponse(context, HttpStatusCode.InternalServerError, _hostEnvironment.IsProduction() ? "I find your lack of faith disturbing." : e.Message);
        }
    }

    public static object StandardEnricher(HttpContext ctx)
    {
        if (ctx == null) return null;

        var httpContextCache = new HttpContextCache
        {
            IpAddress = ctx.Connection.RemoteIpAddress.ToString(),
            Host = ctx.Request.Host.ToString(),
            Path = ctx.Request.Path.ToString(),
            IsHttps = ctx.Request.IsHttps,
            Scheme = ctx.Request.Scheme,
            Method = ctx.Request.Method,
            ContentType = ctx.Request.ContentType,
            Protocol = ctx.Request.Protocol,
            QueryString = ctx.Request.QueryString.ToString(),
            Query = ctx.Request.Query.ToDictionary(x => x.Key, y => y.Value.ToString()),
            Headers = ctx.Request.Headers.ToDictionary(x => x.Key, y => y.Value.ToString()),
            Cookies = ctx.Request.Cookies.ToDictionary(x => x.Key, y => y.Value.ToString())
        };

        return httpContextCache;
    }

    private async Task SetResponse(HttpContext context, HttpStatusCode statusCode, string msg)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) statusCode;

        try
        {
            var json = JsonSerializer.Serialize(new {error = msg}, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}