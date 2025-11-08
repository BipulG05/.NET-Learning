using Microsoft.EntityFrameworkCore;
using WebApi2024.Data;
using WebApi2024.Interfaces;
using WebApi2024.Mappers;
using WebApi2024.Models;

namespace WebApi2024.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _Context;
        public CommentRepository(ApplicationDBContext context)
        {
            _Context = context;
            
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _Context.AddAsync(commentModel);
            await _Context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment> DeleteAsync(int id)
        {
            var commentModel = await _Context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (commentModel == null)
            {
                return null;
            }
            _Context.Comments.Remove(commentModel);
            await _Context.SaveChangesAsync();
            return commentModel;

        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _Context.Comments.Include(a => a.AppUser).ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment = await _Context.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.Id==id);
            return comment;
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var existingComment = await _Context.Comments.FindAsync(id);
            if (existingComment == null)
            {
                return null;
            }
            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;
            await _Context.SaveChangesAsync();
            return existingComment;
        }
    }
}
