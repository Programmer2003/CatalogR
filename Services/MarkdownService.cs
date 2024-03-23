namespace CatalogR.Services
{
    public static class MarkdownService
    {
        private static readonly HtmlSanitizationService _sanitizer = new HtmlSanitizationService();

        public static string Parse(string? text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;

            var html = Markdig.Markdown.ToHtml(text);
            return _sanitizer.Sanitize(html);
        }
    }
}
