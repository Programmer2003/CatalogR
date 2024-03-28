using CatalogR.Data;
using CatalogR.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CatalogR.Hubs
{
    public class CommentsHub : ItemHub
    {
        public CommentsHub(ApplicationDbContext context) : base(context) { }

        public async Task AddComment(string userId, int itemId, string text)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return;

            var comment = await CreateComment(userId, itemId, text);
            comment.User = user;
            await Clients.Group(itemId.ToString()).SendAsync("CommentAdded", comment);
        }

        private async Task<Comment> CreateComment(string userId, int itemId, string text)
        {
            var comment = new Comment() { UserId = userId, ItemId = itemId, Text = text };
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}
