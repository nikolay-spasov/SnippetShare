using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SnippetShare.Models
{
    public class ShowVM
    {
        [HiddenInput]
        public int Id { get; set; }

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
    }
}