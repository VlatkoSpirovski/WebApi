using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos.Comment;

public class UpdateCommentDto
{
    [Required]
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters")]
    [MaxLength(250, ErrorMessage = "Title can't exceed 250 characters")]
    public string Title { get; set; } = String.Empty;
    [Required]
    [MinLength(5, ErrorMessage = "Content must be at least 5 characters")]
    [MaxLength(250, ErrorMessage = "Content can't exceed 250 characters")]
    public string Content { get; set; } = String.Empty;
}