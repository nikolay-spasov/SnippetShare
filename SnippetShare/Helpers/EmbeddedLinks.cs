namespace SnippetShare.Helpers
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web;
    using System;

    public static class EmbeddedLinks
    {
        public static MvcHtmlString ShowEmbeddedCode(this HtmlHelper html, string url)
        {
            TagBuilder tag = new TagBuilder("input");
            tag.Attributes["type"] = "text";
            tag.Attributes["value"] = string.Format(
                @"<iframe src=""{0}"" style=""width: 600px; height: 200px;""></iframe>", url);

            return MvcHtmlString.Create(tag.ToString());
        }
    }
}