using System;
using System.IO;
using System.Text;

namespace DocumentConverter
{
    public enum DocumentFormat { Markdown, HTML, PlainText }

    public class Converter
    {
        public string Convert(string input, DocumentFormat from, DocumentFormat to)
        {
            if (from == to) return input;

            string plainText = from switch
            {
                DocumentFormat.Markdown  => StripMarkdown(input),
                DocumentFormat.HTML      => StripHtml(input),
                DocumentFormat.PlainText => input,
                _ => throw new NotSupportedException()
            };

            return to switch
            {
                DocumentFormat.HTML      => ToHtml(plainText),
                DocumentFormat.Markdown  => $"# {plainText.Split('\n')[0]}\n\n{plainText}",
                DocumentFormat.PlainText => plainText,
                _ => throw new NotSupportedException()
            };
        }

        private string StripMarkdown(string md) =>
            md.Replace("#", "").Replace("**", "").Replace("*", "").Trim();

        private string StripHtml(string html) =>
            System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "").Trim();

        private string ToHtml(string text)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html><html><body>");
            foreach (var line in text.Split('\n'))
                sb.AppendLine($"<p>{line}</p>");
            sb.AppendLine("</body></html>");
            return sb.ToString();
        }
    }

    class Program
    {
        static void Main()
        {
            var converter = new Converter();
            string md = "# Hello World\n\nThis is a **bold** statement.";

            Console.WriteLine("=== Document Converter ===\n");
            Console.WriteLine("[INPUT - Markdown]");
            Console.WriteLine(md);

            string html = converter.Convert(md, DocumentFormat.Markdown, DocumentFormat.HTML);
            Console.WriteLine("\n[OUTPUT - HTML]");
            Console.WriteLine(html);

            string plain = converter.Convert(md, DocumentFormat.Markdown, DocumentFormat.PlainText);
            Console.WriteLine("[OUTPUT - PlainText]");
            Console.WriteLine(plain);
        }
    }
}
