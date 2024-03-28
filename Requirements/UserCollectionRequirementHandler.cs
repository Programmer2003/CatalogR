using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using CatalogR.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CatalogR.Data.Migrations
{
    public class UserCollectionRequirement : IAuthorizationRequirement { }
    public class UserCollectionRequirementHandler : AuthorizationHandler<UserCollectionRequirement>
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public UserCollectionRequirementHandler(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserCollectionRequirement requirement)
        {
            var user = await _userManager.GetUserAsync(context.User);

            if (context.Resource is HttpContext httpContext)
            {
                var collectionId = httpContext.GetRouteValue("id");
                if(collectionId == null)
                {
                    context.Succeed(requirement);
                    return;
                }

                if (!int.TryParse(collectionId.ToString(), out int id))
                {
                    context.Fail();
                    return;
                }

                var collection = await _context.Collections.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
                if (collection == null || user == null || (collection.UserId != user.Id && !user.IsAdmin)) context.Fail();
                else context.Succeed(requirement);
            }
        }
    }
}
