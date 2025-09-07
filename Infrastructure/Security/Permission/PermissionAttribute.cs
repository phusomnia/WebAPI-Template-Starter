using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI_Template_Starter.Infrastructure.Security;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class PermissionAttribute : AuthorizeAttribute
{
    public PermissionAttribute(String permissionName)
    {
        Policy = $"Permission:{permissionName}";
    }
}