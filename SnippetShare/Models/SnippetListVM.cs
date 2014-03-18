namespace SnippetShare.Models
{
    using System;
    using System.Collections.Generic;

    public class SnippetListVM
    {
        public List<SnippetVM> Snippets { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }

    public class SnippetVM
    {
        private string title;

        public int Id { get; set; }
        public DateTime DatePublished { get; set; }
        public string Title
        {
            get
            {
                if (string.IsNullOrEmpty(this.title))
                {
                    return "Untitled";
                }
                else
                {
                    return this.title;
                }
            }

            set { this.title = value; }
        }
    }

    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
            }
        }
    }
}