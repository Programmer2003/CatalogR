using CatalogR.Data;
using CatalogR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace CatalogR.Hubs
{
    public class ItemHub : Hub
    {
        protected readonly ApplicationDbContext _context;

        public ItemHub(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
        }

        public async Task AddToGroup(int itemId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, itemId.ToString());
        }
    }
}
