using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SnippetShare.DataAccess.Entities
{
    public class Snippet
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DatePublished { get; set; }

        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserProfile User { get; set; }
    }
}