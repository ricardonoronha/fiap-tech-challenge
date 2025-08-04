using Serilog.Events;
using Serilog.Formatting;
using System.Text.Json;
using System.Text.Json.Serialization;

public class CustomDatadogFormatter : ITextFormatter
{
    public void Format(LogEvent logEvent, TextWriter output)
    {
        var logObject = new Dictionary<string, object?>
        {
            ["Timestamp"] = logEvent.Timestamp.UtcDateTime,
            ["Level"] = logEvent.Level.ToString(),
            ["Message"] = logEvent.RenderMessage(),
            // ["MessageTemplate"] = logEvent.MessageTemplate.Text,
            // ["RenderedMessage"] = logEvent.RenderMessage()
        };

        // Campos que devem ir para a raiz com nome original (snake_case)
        var ddKeys = new HashSet<string>
        {
            "dd_env",
            "dd_service",
            "dd_version",
            "dd_trace_id",
            "dd_span_id"
        };

        var remainingProps = new Dictionary<string, object?>();

        foreach (var property in logEvent.Properties)
        {
            var value = Simplify(property.Value);

            if (ddKeys.Contains(property.Key))
            {
                // Coloca na raiz com mesmo nome
                logObject[property.Key] = value;
            }

            // Sempre inclui em Properties
            remainingProps[property.Key] = value;
        }

        logObject["Properties"] = remainingProps;

        if (logEvent.Exception is not null)
        {
            logObject["Exception"] = logEvent.Exception.ToString();
        }

        var json = JsonSerializer.Serialize(logObject, new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = false
        });

        output.WriteLine(json);
    }

    private object? Simplify(LogEventPropertyValue value)
    {
        return value switch
        {
            ScalarValue scalar => scalar.Value,
            SequenceValue sequence => sequence.Elements.Select(Simplify).ToArray(),
            StructureValue structure => structure.Properties.ToDictionary(p => p.Name, p => Simplify(p.Value)),
            DictionaryValue dict => dict.Elements.ToDictionary(
                kv => kv.Key.Value?.ToString() ?? string.Empty,
                kv => Simplify(kv.Value)
            ),
            _ => value.ToString()
        };
    }
}
