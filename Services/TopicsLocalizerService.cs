using Microsoft.Extensions.Localization;

namespace CatalogR.Services
{
    public class TopicsLocalizerService
    {
        private readonly IStringLocalizer<TopicsLocalizerService> _localizer;

        public TopicsLocalizerService(IStringLocalizer<TopicsLocalizerService> localizer) => _localizer = localizer;

        public string GetString(string name) => _localizer[name];
    }
}
