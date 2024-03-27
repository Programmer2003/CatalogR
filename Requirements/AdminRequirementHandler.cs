using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using CatalogR.Models;

namespace CatalogR.Data.Migrations
{
    public class AdminRequirement : IAuthorizationRequirement { }
    public class AdminRequirementHandler : AuthorizationHandler<AdminRequirement>
    {
        private readonly UserManager<User> _userManager;

        public AdminRequirementHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
        {
            var userFromDb = await _userManager.GetUserAsync(context.User);

            if (userFromDb == null || !userFromDb.IsAdmin) context.Fail();
            else context.Succeed(requirement);
        }
    }
}
