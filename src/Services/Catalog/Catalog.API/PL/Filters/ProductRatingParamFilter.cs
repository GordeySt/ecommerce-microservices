using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Catalog.API.PL.Filters
{
    public class ProductRatingParamFilter : ActionFilterAttribute
    {
        private const int MinimumRating = 0;
        private const int MaximumRating = 5;
        private const string InvalidNumberErrorMessage = "Number cannot be lower than 0 or higher than 5";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ratingCountKey = "ratingCount";

            if (!context.ActionArguments.ContainsKey(ratingCountKey))
            {
                SetBadRequestResultToContextResult(context);
                return;
            }

            if (context.ActionArguments[ratingCountKey] is not int ratingCount)
            {
                SetBadRequestResultToContextResult(context);
                return;
            }

            if (ratingCount < MinimumRating || ratingCount > MaximumRating)
            {
                SetBadRequestResultToContextResult(context, new
                {
                    Error = InvalidNumberErrorMessage
                });

                return;
            }

            OnActionExecuting(context);
        }

        private void SetBadRequestResultToContextResult(ActionExecutingContext context, object error = null)
        {
            context.Result = new BadRequestObjectResult(error);
        }
    }
}
