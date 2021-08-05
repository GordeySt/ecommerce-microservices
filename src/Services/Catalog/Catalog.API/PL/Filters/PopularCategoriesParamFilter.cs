using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Catalog.API.PL.Filters
{
    public class PopularCategoriesParamFilter : ActionFilterAttribute
    {
        private const string InvalidNumberErrorMessage = "Number cannot be negative";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var popularCategoriesCountKey = "popularCategoriesCount";

            if (!context.ActionArguments.ContainsKey(popularCategoriesCountKey))
            {
                SetBadRequestResultToContextResult(context);
                return;
            }

            if (context.ActionArguments[popularCategoriesCountKey] is not int popularCategoriesCount)
            {
                SetBadRequestResultToContextResult(context);
                return;
            }

            if (popularCategoriesCount < 0)
            {
                SetBadRequestResultToContextResult(context, new
                {
                    Error = InvalidNumberErrorMessage
                });

                return;
            }

            base.OnActionExecuting(context);
        }

        private void SetBadRequestResultToContextResult(ActionExecutingContext context, object errors = null)
        {
            context.Result = new BadRequestObjectResult(errors);
        }
    }
}