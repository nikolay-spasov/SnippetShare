namespace SnippetShare.Helpers
{
    using System;
    using System.Text;
    using System.Web.Mvc;
    using SnippetShare.Models;

    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
            PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            if (pagingInfo.CurrentPage - 1 > 0)
            {
                TagBuilder prevPage = new TagBuilder("a");
                prevPage.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage - 1));
                prevPage.InnerHtml = "&lt;";
                result.Append(prevPage.ToString());
            }

            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("selected");
                result.Append(tag.ToString());
            }

            if (pagingInfo.CurrentPage + 1 <= pagingInfo.TotalPages)
            {
                TagBuilder nextPage = new TagBuilder("a");
                nextPage.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage + 1));
                nextPage.InnerHtml = "&gt;";
                result.Append(nextPage.ToString());
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}