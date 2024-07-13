using Meguri.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Meguri.Authorization {
    public class WorkIsOwnerAuthorizationHandler
        : AuthorizationHandler<OperationAuthorizationRequirement, Work> {
        UserManager<IdentityUser> _userManager;

        public WorkIsOwnerAuthorizationHandler(
            UserManager<IdentityUser> userManager
        ) {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            Work resource
        ) {
            if (context.User == null || resource == null) {
                return Task.CompletedTask;
            }

            // If not asking for CRUD permission, return.
            if (requirement.Name != Constants.CreateOperationName &&
                requirement.Name != Constants.ReadOperationName &&
                requirement.Name != Constants.UpdateOperationName &&
                requirement.Name != Constants.DeleteOperationName) {
                return Task.CompletedTask;
            }

            if (resource.UserId == _userManager.GetUserId(context.User)) {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
