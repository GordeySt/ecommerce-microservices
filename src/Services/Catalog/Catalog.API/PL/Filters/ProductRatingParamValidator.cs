using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Catalog.API.PL.Filters
{
    public class ProductRatingParamValidator : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ratingCountKey = "ratingCount";

            if (!context.ActionArguments.ContainsKey(ratingCountKey))
            {
                context.Result = new BadRequestResult();
                return;
            }

            if (context.ActionArguments[ratingCountKey] is not int ratingCount)
            {
                context.Result = new BadRequestResult();
                return;
            }

            if (ratingCount < 0 || ratingCount > 5)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    Error = "Number cannot be lower than 0 or higher than 5"
                });

                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
