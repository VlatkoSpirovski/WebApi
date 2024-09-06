using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos.Comment;
using WebApi.Interfaces;
using WebApi.Mappers;
using WebApi.Repository;

namespace WebApi.Controllers;

[Route("/api/comments")]
[ApiController]
public class CommentController :ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IStockRepository _stockRepository;
    public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
        _commentRepository = commentRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllComments()
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var comments = await _commentRepository.GetAllAsync();
        var commentsDto = comments.Select(s => s.ToCommentDto());
        return Ok(comments);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCommentById([FromRoute]int id)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var comments = await _commentRepository.GetByIdAsync(id);
        if (comments == null)
        {
            return NotFound();
        }
        return Ok(comments.ToCommentDto());
    }

    [HttpPost("{stockId:int}")]
    public async Task<IActionResult> Create([FromRoute] int stockId,[FromBody] CreateCommentDto commentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (!await _stockRepository.StockExist(stockId))
        {
            return BadRequest("Stock not found");
        }

        var commentModel = commentDto.ToCommentFromCreate(stockId);
        await _commentRepository.CreateAsync(commentModel);
        return CreatedAtAction(nameof(GetCommentById), new { id = commentModel.Id }, commentModel);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto commentDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var comment = await _commentRepository.UpdateAsync(id, commentDto);
        if (comment == null)
        {
            return NotFound();
        }
        return Ok(comment.ToCommentDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        await _commentRepository.DeleteAsync(id);
        return NoContent();
    }
}