namespace SnippetShare.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateSnippetVM
    {
        [MaxLength(256, ErrorMessage = "Title should be less than 256 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Snippet content is required")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}