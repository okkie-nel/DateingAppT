using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Datingapp.API.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Datingapp.API.Helpers
{
    public class loguseractive : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultsContext = await next();
            
            var userId = int.Parse(resultsContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var rep = resultsContext.HttpContext.RequestServices.GetService<IDateingRepositry>();
            var user = await rep.GetUser(userId);
            user.LastActive = DateTime.Now;
            await rep.SaveAll();
        }
    }
}