﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using CatalogR.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CatalogR.Data.Migrations
{
    public class CollectionAccessRequirement : IAuthorizationRequirement { }
    public class CollectionAccessRequirementHandler : AuthorizationHandler<CollectionAccessRequirement>
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public CollectionAccessRequirementHandler(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CollectionAccessRequirement requirement)
        {
            var userId = _userManager.GetUserId(context.User);

            if (context.Resource is HttpContext httpContext)
            {
                var collectionId = httpContext.GetRouteValue("collectionId");
                if (collectionId == null || !int.TryParse(collectionId.ToString(), out int id))
                {
                    context.Fail();
                    return;
                }

                var collection = await _context.Collections.FirstOrDefaultAsync(c => c.Id == id);
                if(collection == null || collection.UserId != userId) context.Fail();
                else context.Succeed(requirement);
            }
        }
    }
}