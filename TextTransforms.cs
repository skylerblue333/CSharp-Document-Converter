using System.Text;

namespace SkyDocumentConverter;

public static class TextTransforms
{
    public const int MaxInputCharacters = 100_000;
    public static readonly string[] SupportedOutputs = ["metadata", "base64", "upper"];

    public static TransformResult Transform(string? content, string? output)
    {
        if (content is null)
        {
            throw new ArgumentException("content is required");
        }
        if (content.Length > MaxInputCharacters)
        {
            throw new ArgumentException($"content exceeds {MaxInputCharacters} characters");
        }

        output = output?.Trim().ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(output) || !SupportedOutputs.Contains(output))
        {
            throw new ArgumentException("to must be one of: metadata, base64, upper");
        }

        return output switch
        {
            "metadata" => new TransformResult(
                Content: content,
                Format: "metadata",
                InputCharacters: content.Length,
                OutputBytes: Encoding.UTF8.GetByteCount(content)),
            "base64" => BuildBase64(content),
            "upper" => BuildUpper(content),
            _ => throw new InvalidOperationException("validated output was not handled")
        };
    }

    private static TransformResult BuildBase64(string content)
    {
        var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(content));
        return new TransformResult(
            Content: encoded,
            Format: "base64",
            InputCharacters: content.Length,
            OutputBytes: Encoding.UTF8.GetByteCount(encoded));
    }

    private static TransformResult BuildUpper(string content)
    {
        var upper = content.ToUpperInvariant();
        return new TransformResult(
            Content: upper,
            Format: "upper",
            InputCharacters: content.Length,
            OutputBytes: Encoding.UTF8.GetByteCount(upper));
    }
}

public sealed record TransformResult(
    string Content,
    string Format,
    int InputCharacters,
    int OutputBytes);
