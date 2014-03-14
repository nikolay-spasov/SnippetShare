namespace SnippetShare.HtmlHelpers
{
    using System;
    using System.Web.Mvc;

    public static class FriendlyDate
    {
        private const int Second = 1;
        private const int Minute = 60 * Second;
        private const int Hour = 60 * Minute;
        private const int Day = 24 * Hour;
        private const int Month = 30 * Day;

        public static MvcHtmlString ToFriendlyDate(this HtmlHelper html, DateTime date)
        {
            TagBuilder builder = new TagBuilder("span");
            builder.AddCssClass("friendly-date");
            builder.InnerHtml = FriendlyDateAsString(date);
            builder.Attributes["title"] = date.ToString("yyyy-MM-dd hh:mm:ss");

            return new MvcHtmlString(builder.ToString());
        }

        private static string FriendlyDateAsString(DateTime date)
        {
            DateTime now = DateTime.Now;
            if (date > now)
            {
                throw new ArgumentException("Future dates are not supported.");
            }

            TimeSpan ts = now.Subtract(date);
            long delta = (long)ts.TotalSeconds;

            if (delta < 0)
            {
                return "not yet";
            }

            if (delta < 1 * Minute)
            {
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
            }

            if (delta < 2 * Minute)
            {
                return "a minute ago";
            }

            if (delta < 45 * Minute)
            {
                return ts.Minutes + " minutes ago";
            }

            if (delta < 90 * Minute)
            {
                return "an hour ago";
            }

            if (delta < 24 * Hour)
            {
                return ts.Hours + " hours ago";
            }

            if (delta < 48 * Hour)
            {
                return "yesterday";
            }

            if (delta < 30 * Day)
            {
                return ts.Days + " days ago";
            }

            if (delta < 12 * Month)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }
        }
    }
}