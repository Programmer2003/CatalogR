using Microsoft.Extensions.Localization;

namespace CatalogR.Services
{
    public class TimeManagerService
    {
        private readonly IStringLocalizer<TimeManagerService> _localizer;

        public TimeManagerService(IStringLocalizer<TimeManagerService> localizer)
        {
            _localizer = localizer;
        }

        public string AsTimeAgo(DateTime dateTime)
        {
            TimeSpan timeSpan = DateTime.UtcNow.Subtract(dateTime);

            return timeSpan.TotalSeconds switch
            {
                <= 60 => $"{timeSpan.Seconds} {_localizer["seconds ago"]}",

                _ => timeSpan.TotalMinutes switch
                {
                    <= 1 => _localizer["about a minute ago"],
                    < 60 => $"{_localizer["about"]} {timeSpan.Minutes} {_localizer["minutes ago"]}",
                    _ => timeSpan.TotalHours switch
                    {
                        <= 1 => _localizer["about an hour ago"],
                        < 24 => $"{_localizer["about"]} {timeSpan.Hours} {_localizer["hours ago"]}",
                        _ => timeSpan.TotalDays switch
                        {
                            <= 1 => _localizer["yesterday"],
                            <= 30 => $"{_localizer["about"]} {timeSpan.Days} {_localizer["days ago"]}",

                            <= 60 => _localizer["about a month ago"],
                            < 365 => $"{_localizer["about"]} {timeSpan.Days / 30} {_localizer["months ago"]}",

                            <= 365 * 2 => "about a year ago",
                            _ => $"{_localizer["about"]} {timeSpan.Days / 365} {_localizer["years ago"]}"
                        }
                    }
                }
            };
        }
    }
}
