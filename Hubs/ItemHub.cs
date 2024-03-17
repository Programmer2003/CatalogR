using CatalogR.Data;
using CatalogR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CatalogR.Hubs
{
    public class ItemHub : Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public ItemHub(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task AddToGroup(int itemId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, itemId.ToString());
        }

        public async Task AddComment(string userId, int itemId, string text)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return;

            var comment = await CreateComment(userId, itemId, text);
            comment.User = user;
            await Clients.Group(itemId.ToString()).SendAsync("CommentAdded", comment);
        }

        public async Task<Comment> CreateComment(string userId, int itemId, string text)
        {
            var comment = new Comment();
            comment.UserId = userId;
            comment.ItemId = itemId;
            comment.Text = text;
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return (await _context.Comments.AddAsync(comment)).Entity;
        }
    }
}
