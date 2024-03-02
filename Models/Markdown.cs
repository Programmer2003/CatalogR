namespace CatalogR.Models
{
    public static class Markdown
    {
        public static string Parse(string? text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;

            return Markdig.Markdown.ToHtml(text);
        }
    }
}
