namespace SnippetShare.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class ShowVM
    {
        [HiddenInput]
        public int Id { get; set; }

        public string Title { get; set; }

        [UIHint("_SnippetContent")]
        public string Content { get; set; }

        public DateTime DatePublished { get; set; }

        [ScaffoldColumn(false)]
        public int? UserId { get; set; }

        [ScaffoldColumn(false)]
        public string UserName { get; set; }

        public string FriendlyUserName
        {
            get
            {
                if (string.IsNullOrEmpty(this.UserName))
                {
                    return "Anonymous";
                }
                else
                {
                    return this.UserName;
                }
            }
        }

        public string TitleToShow
        {
            get
            {
                if (string.IsNullOrEmpty(this.Title))
                {
                    return "Untitled";
                }
                else
                {
                    return this.Title;
                }
            }
        }
    }
}