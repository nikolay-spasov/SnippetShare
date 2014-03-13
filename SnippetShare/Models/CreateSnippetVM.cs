using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SnippetShare.Models
{
    public class CreateSnippetVM
    {
        [Required(ErrorMessage = "Snippet content is required")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}