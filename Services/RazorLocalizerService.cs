using Microsoft.Extensions.Localization;

namespace CatalogR.Services
{
    public class RazorLocalizerService
    {
        private readonly IStringLocalizer<RazorLocalizerService> _localizer;

        public RazorLocalizerService(IStringLocalizer<RazorLocalizerService> localizer) => _localizer = localizer;

        public string GetString(string name) => _localizer[name];
    }
}
