namespace SnippetShare.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Snippet
    {
        public int Id { get; set; }

        [MaxLength(256)]
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime DatePublished { get; set; }

        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserProfile User { get; set; }
    }
}
