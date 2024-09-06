using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dtos.Comment;
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

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Comment> CreateAsync(Comment commentModel)
    {
        await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
        return commentModel;
    }

    public async Task<Comment?> UpdateAsync(int id, UpdateCommentDto commentDto)
    {
        {
            var existingId = await _context.Comments.FindAsync(id);
            if (existingId == null)
            {
                return null;
            }
            existingId.Title = commentDto.Title;
            existingId.Content = commentDto.Content;
            await _context.SaveChangesAsync();
            return existingId;
        }
    }

    public async Task<Comment> DeleteAsync(int id)
    {
        var existingId = await _context.Comments.FindAsync(id);
        if (existingId == null)
        {
            return null;
        }
        _context.Comments.Remove(existingId);
        await _context.SaveChangesAsync();
        return existingId;
    }
}