using System.ComponentModel.DataAnnotations;

namespace WebApi2024.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5,ErrorMessage ="Title must be minimun 5 character !!")]
        [MaxLength(50, ErrorMessage = "Title can not be over 50 character !!")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(10, ErrorMessage = "Content must be minimun 10 character !!")]
        [MaxLength(500, ErrorMessage = "Content can not be over 500 character !!")]
        public string Content { get; set; } = string.Empty;
    }
}
