using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics.CodeAnalysis;

namespace Core.Utils.HealthCheck;

public static class UiResponseWriter
{
    private const string DefaultContentType = "application/json";

    private static readonly byte[] EmptyResponse = "{}"u8.ToArray();
    private static readonly Lazy<JsonSerializerOptions> Options = new(CreateJsonOptions);

    [SuppressMessage("Design", "CA1062:Validate arguments of public methods")]
    [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract")]
    public static async Task WriteHealthCheckUiResponse(HttpContext httpContext, HealthReport report)
    {
        if (report != null)
        {
            httpContext.Response.ContentType = DefaultContentType;

            var uiReport = UiHealthReport.CreateFrom(report);

            await JsonSerializer.SerializeAsync(httpContext.Response.Body, uiReport, Options.Value).ConfigureAwait(false);
        }
        else
        {
            await httpContext.Response.Body.WriteAsync(EmptyResponse).ConfigureAwait(false);
        }
    }

    private static JsonSerializerOptions CreateJsonOptions()
    {
        var options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        options.Converters.Add(new JsonStringEnumConverter());

        // for compatibility with older UI versions ( <3.0 ) we arrange
        // timespan serialization as s
        options.Converters.Add(new TimeSpanConverter());

        return options;
    }
}

internal sealed class TimeSpanConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return TimeSpan.Zero;
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}