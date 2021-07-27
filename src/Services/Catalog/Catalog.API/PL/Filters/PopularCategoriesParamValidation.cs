using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Catalog.API.PL.Filters
{
    public class PopularCategoriesParamValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var popularCategoriesCountKey = "popularCategoriesCount";

            if (!context.ActionArguments.ContainsKey(popularCategoriesCountKey))
            {
                context.Result = new BadRequestResult();
                return;
            }

            if (context.ActionArguments[popularCategoriesCountKey] is not int popularCategoriesCount)
            {
                context.Result = new BadRequestResult();
                return;
            }

            if (popularCategoriesCount < 0)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    Error = "Number cannot be negative"
                });

                return;
            }

            base.OnActionExecuting(context);
        }
    }
}