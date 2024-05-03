
namespace Middleware;
using Microsoft.AspNetCore.Http;
public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;
        var requestBodyStream = new MemoryStream();
        var originalRequestBody = request.Body;
        
        request.EnableBuffering();
        await request.Body.CopyToAsync(requestBodyStream);
        requestBodyStream.Seek(0, SeekOrigin.Begin);

        var requestBodyText = await new StreamReader(requestBodyStream).ReadToEndAsync();
        var logMessage = $"Schema: {request.Scheme} Host: {request.Host} Path: {request.Path} Query String: {request.QueryString} Body: {requestBodyText}";

        _logger.LogInformation(logMessage);

        requestBodyStream.Seek(0, SeekOrigin.Begin);
        request.Body = requestBodyStream;

        await _next(context);

        request.Body = originalRequestBody;
    }
    // public async Task<string> ReadAsync()
    // {
    //     var logFilePath = "log.txt";
    //     using var reader = File.OpenText(logFilePath);
    //     var logFileContent = await reader.ReadToEndAsync();
    //     return logFileContent;
    // }

    // private async Task WriteLogToFileAsync(string logMessage)
    // {
    //     var logFilePath = $"log{DateTime.Now:yyyyMMdd}.txt";
    //     await File.AppendAllTextAsync(logFilePath, logMessage + Environment.NewLine);
    // }
}