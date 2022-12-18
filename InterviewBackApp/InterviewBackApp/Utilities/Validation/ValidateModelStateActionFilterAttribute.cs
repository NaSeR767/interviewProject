using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using InterviewBackApp.ViewModels.Result;

namespace InterviewBackApp.Utilities.Validation
{
    public class ValidateModelStateActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var result = new ResultViewModel();
                var errors = ModelStateValidation.GetErrors(context.ModelState);
                result.ErrorMessages.AddRange(errors);


                context.Result = new BadRequestObjectResult(result); // it returns 400 with the error
            }
        }
    }
}
