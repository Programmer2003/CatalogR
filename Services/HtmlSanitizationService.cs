using Ganss.Xss;

namespace CatalogR.Services
{
    public class HtmlSanitizationService
    {
        private readonly IHtmlSanitizer _htmlSanitizer;

        public HtmlSanitizationService()
        {
            _htmlSanitizer = new HtmlSanitizer();
        }

        public string Sanitize(string? unsanitizedHtml)
        {
            if (string.IsNullOrWhiteSpace(unsanitizedHtml)) return string.Empty;

            return _htmlSanitizer.Sanitize(unsanitizedHtml);
        }
    }
}
