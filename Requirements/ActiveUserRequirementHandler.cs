using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using CatalogR.Models;

namespace CatalogR.Data.Migrations
{
    public class ActiveUserRequirement : IAuthorizationRequirement { }
    public class ActiveUserRequirementHandler : AuthorizationHandler<ActiveUserRequirement>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public ActiveUserRequirementHandler(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ActiveUserRequirement requirement)
        {
            var userFromDb = await _userManager.GetUserAsync(context.User);


            if (userFromDb == null)
            {
                context.Succeed(requirement);
            }
            else if (!userFromDb.IsLocked)
            {
                context.Succeed(requirement);
            }
            else
            {
                await _signInManager.SignOutAsync();
                context.Fail();
            }
        }
    }
}
