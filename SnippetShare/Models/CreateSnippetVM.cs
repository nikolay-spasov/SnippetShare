namespace SnippetShare.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateSnippetVM
    {
        [Required(ErrorMessage = "Snippet content is required")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}