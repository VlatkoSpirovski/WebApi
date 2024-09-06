using WebApi.Models;

namespace WebApi.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync();
}