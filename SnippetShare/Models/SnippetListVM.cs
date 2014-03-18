namespace SnippetShare.Models
{
    using System;

    public class SnippetListVM
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
}