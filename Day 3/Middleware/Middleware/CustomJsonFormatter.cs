using Serilog.Events;
using Serilog.Formatting;

namespace Middleware;

public class CustomJsonFormatter : ITextFormatter
{
    public void Format(LogEvent logEvent, TextWriter output)
    {
        output.Write("{");
        output.Write("\"Timestamp\":\"");
        output.Write(logEvent.Timestamp.ToString("o"));
        output.Write("\",\"LogMessage\":\"");
        output.Write(logEvent.RenderMessage());
        output.Write("\"}");
        output.WriteLine();
    }
}