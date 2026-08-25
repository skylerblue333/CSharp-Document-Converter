using SkyDocumentConverter;

static void Assert(bool condition, string message)
{
    if (!condition) throw new InvalidOperationException(message);
}

var metadata = TextTransforms.Transform("hello", "metadata");
Assert(metadata.Content == "hello", "metadata preserves content");
Assert(metadata.InputCharacters == 5, "metadata character count");
Assert(metadata.OutputBytes == 5, "metadata byte count");

var base64 = TextTransforms.Transform("hello", "base64");
Assert(base64.Content == "aGVsbG8=", "base64 must use UTF-8");
Assert(base64.Format == "base64", "base64 format label");

var upper = TextTransforms.Transform("Sky coin", "upper");
Assert(upper.Content == "SKY COIN", "uppercase transform");

try
{
    TextTransforms.Transform(null, "upper");
    throw new InvalidOperationException("null content was accepted");
}
catch (ArgumentException) { }

try
{
    TextTransforms.Transform("x", "pdf");
    throw new InvalidOperationException("unsupported output was accepted");
}
catch (ArgumentException) { }

try
{
    TextTransforms.Transform(new string('x', TextTransforms.MaxInputCharacters + 1), "upper");
    throw new InvalidOperationException("oversized content was accepted");
}
catch (ArgumentException) { }

Console.WriteLine("Sky Text Transform tests passed");
