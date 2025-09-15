using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI_Template_Starter.Infrastructure.Security.Authorization;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class PreAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
{
    private readonly string _expression;

    public PreAuthorizeAttribute(string expression)
    {
        _expression = expression;
    }

    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (!EvaluateExpression(user, _expression, context))
        {
            context.Result = new UnauthorizedObjectResult(new
            {
                message = "You do not have permission to access this resource."
            });
        }

        return Task.CompletedTask;
    }

    private bool EvaluateExpression(ClaimsPrincipal user, string expression, AuthorizationFilterContext context)
    {
        expression = ResolvePlaceholders(expression, context);
        Console.WriteLine(expression);
        
        var roleMatch = Regex.Match(expression, @"hasRole\('([^']+)'\)", RegexOptions.IgnoreCase);
        if (roleMatch.Success)
        {
            var role = roleMatch.Groups[1].Value;
            return user.IsInRole(role);
        }
        
        var authMatch = Regex.Match(expression, @"hasAuthority\('([^']+)'\)", RegexOptions.IgnoreCase);
        if (authMatch.Success)
        {
            var authority = authMatch.Groups[1].Value;
            return user.Claims.Any(c => c.Type == "permission" && c.Value == authority);
        }
        
        return false;
    }
    
    private string ResolvePlaceholders(string expression, AuthorizationFilterContext context)
    {
        var matches = Regex.Matches(expression, @"#(\w+)");
        foreach (Match match in matches)
        {
            Console.WriteLine(match);
            var paramName = match.Groups[1].Value;
            string? value = null;
            
            if (context.RouteData.Values.TryGetValue(paramName, out var routeValue))
            {
                value = routeValue?.ToString();
            }
            else if (context.HttpContext.Request.Query.TryGetValue(paramName, out var queryValue))
            {
                value = queryValue.ToString();
            }
            if (value != null)
            {
                expression = expression.Replace(match.Value, $"'{value}'");
            }
        }

        return expression;
    }
}