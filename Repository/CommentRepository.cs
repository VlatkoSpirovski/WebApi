using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDBContext _context;

    public CommentRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<Comment>> GetAllAsync()
    {
        return await _context.Comments.ToListAsync();
    }
}