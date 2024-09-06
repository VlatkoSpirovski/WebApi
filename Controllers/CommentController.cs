using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;
using WebApi.Repository;

namespace WebApi.Controllers;

[Route("/api/comments")]
[ApiController]
public class CommentController :ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    public CommentController(CommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllComments()
    {
        var comments = await _commentRepository.GetAllAsync();
        return Ok(comments);
    }
}