using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using NPlaylist.Persistence.DbModels;

namespace NPlaylist.Authorization
{
    public class AudioAuthorizationCrudHandler
        : AuthorizationHandler<OperationAuthorizationRequirement, Audio>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            Audio resource)
        {
            if (requirement.Name == Operations.Read.Name)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var userId = Guid.Parse(context.User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == resource.OwnerId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
