using Serilog.Core;
using Serilog.Events;
using Datadog.Trace;

public class DatadogTraceEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var span = Tracer.Instance?.ActiveScope?.Span;
        if (span == null) return;
        
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("dd.trace_id", span.TraceId.ToString()));
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("dd.span_id", span.SpanId.ToString()));
    }
}