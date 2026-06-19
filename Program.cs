using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/api/v1/convert", async (HttpContext context) => {
    using var doc = await JsonDocument.ParseAsync(context.Request.Body);
    var root = doc.RootElement;
    
    var content = root.TryGetProperty("content", out var c) ? c.GetString() ?? "" : "";
    var fromFormat = root.TryGetProperty("from", out var f) ? f.GetString() ?? "text" : "text";
    var toFormat = root.TryGetProperty("to", out var t) ? t.GetString() ?? "json" : "json";
    
    object converted = toFormat switch {
        "json" => new { content, format = "json", length = content.Length },
        "base64" => new { content = Convert.ToBase64String(Encoding.UTF8.GetBytes(content)), format = "base64" },
        "upper" => new { content = content.ToUpper(), format = "upper" },
        _ => new { content, format = fromFormat }
    };
    
    await context.Response.WriteAsJsonAsync(converted);
});

app.MapGet("/health", () => new { status = "healthy", version = "3.0.0" });

app.Run("http://0.0.0.0:8080");
