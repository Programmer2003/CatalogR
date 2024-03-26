using CatalogR.Data;
using CatalogR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CatalogR.Hubs
{
    public class LikesHub : ItemHub
    {
        public LikesHub(ApplicationDbContext context, UserManager<User> userManager) : base(context, userManager) { }

        public async Task LikeItem(string userId, int itemId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return;

            var like = await CreateLike(userId, itemId);
            like.User = user;
            await Clients.GroupExcept(itemId.ToString(), Context.ConnectionId).SendAsync("Liked");
        }

        private async Task<Like> CreateLike(string userId, int itemId)
        {
            var like = new Like() { UserId = userId, ItemId = itemId };
            await _context.Likes.AddAsync(like);
            await _context.SaveChangesAsync();
            return like;
        }

        public async Task DislikeItem(string userId, int itemId)
        {
            if (await DeleteLike(userId, itemId))
            {
                await Clients.GroupExcept(itemId.ToString(), Context.ConnectionId).SendAsync("Disliked");
            }
        }

        private async Task<bool> DeleteLike(string userId, int itemId)
        {
            var like = await _context.Likes.FirstOrDefaultAsync(l => l.UserId == userId && l.ItemId == itemId);
            if (like == null) return false;

            _context.Likes.Remove(like);

            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
