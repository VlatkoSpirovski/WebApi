using WebApi.Dtos.Comment;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync(QueryObjectComment query);
    Task<Comment?> GetByIdAsync(int id);
    Task<Comment> CreateAsync(Comment commentModel);
    Task<Comment?> UpdateAsync(int id, UpdateCommentDto commentDto);
    Task<Comment> DeleteAsync(int id);
}