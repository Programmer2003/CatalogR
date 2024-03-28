using CatalogR.Data;
using CatalogR.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogR.Services
{
    public class AdminService
    {
        private readonly ApplicationDbContext _context;
        public AdminService(ApplicationDbContext context) => _context = context;

        public async Task<List<User>> GetUsersAsync() =>
            await _context.Users
                .OrderBy(u => u.Id)
                .AsNoTracking()
                .ToListAsync();

        public async Task ChangeBlockStatus(List<string> userIds, bool block)
        {
            var users = await _context.Users.Where(u => userIds.Contains(u.Id)).ToListAsync();
            users.ForEach(u => u.IsLocked = block);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeRole(List<string> userIds, bool admin)
        {
            var users = await _context.Users.Where(u => userIds.Contains(u.Id)).ToListAsync();
            users.ForEach(u => u.IsAdmin = admin);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUsers(List<string> userIds)
        {
            _context.Comments.RemoveRange(_context.Comments.Where(c => userIds.Contains(c.UserId!)));
            _context.Users.RemoveRange(_context.Users.Where(u => userIds.Contains(u.Id)));
            await _context.SaveChangesAsync();
        }
    }
}
