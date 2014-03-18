using System.ComponentModel.DataAnnotations;
namespace SnippetShare.Models
{
    public class EditVM
    {
        public long Id { get; set; }

        [MaxLength(256, ErrorMessage = "Title should be less than 256 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Snippet content is required")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}