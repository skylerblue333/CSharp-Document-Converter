using SkyDocumentConverter;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/healthz", () => Results.Ok(new { status = "ok", service = "sky-text-transform" }));
app.MapGet("/readyz", () => Results.Ok(new
{
    status = "ready",
    maxInputCharacters = TextTransforms.MaxInputCharacters,
    supportedInput = "text",
    supportedOutputs = TextTransforms.SupportedOutputs
}));

app.MapPost("/v1/convert", (ConvertRequest request) =>
{
    try
    {
        if (!string.Equals(request.From, "text", StringComparison.OrdinalIgnoreCase))
        {
            return Results.UnprocessableEntity(new { error = "only 'text' input is supported" });
        }

        var result = TextTransforms.Transform(request.Content, request.To);
        return Results.Ok(result);
    }
    catch (ArgumentException exception)
    {
        return Results.UnprocessableEntity(new { error = exception.Message });
    }
});

app.Run();

public sealed record ConvertRequest(string? Content, string? From, string? To);
