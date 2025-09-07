using Microsoft.AspNetCore.Authorization;
using WebAPI_Template_Starter.Infrastructure.Annotation;
using WebAPI_Template_Starter.Infrastructure.Utils;

namespace WebAPI_Template_Starter.Infrastructure.Security.permission;

[Component]
public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        Console.WriteLine(CustomJson.json(requirement.permission, CustomJsonOptions.WriteIndented));
        if (context.User.HasClaim("permission", requirement.permission))
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}

public class PermissionRequirement : IAuthorizationRequirement
{
    public String permission { get; }

    public PermissionRequirement(String permission)
    {
        this.permission = permission;
    }
}
