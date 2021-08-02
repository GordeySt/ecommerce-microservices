using Catalog.API.BL.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Catalog.API.PL.Filters.ResponseCaching
{
    [AttributeUsage(AttributeTargets.Method)]
    public class FlushCacheFilterAttribute : Attribute, IAsyncActionFilter
    { 
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

            await cacheService.FlushCachedResponsesAsync();
        }
    }
}

