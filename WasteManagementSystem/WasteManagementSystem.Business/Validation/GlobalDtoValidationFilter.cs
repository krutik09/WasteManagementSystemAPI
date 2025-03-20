

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WasteManagementSystem.Business.Validation;

public class GlobalDtoValidationFilter : IActionFilter
{
    private readonly IServiceProvider _serviceProvider;

    public GlobalDtoValidationFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var arg in context.ActionArguments)
        {
            var dto = arg.Value;
            if (dto == null) continue;

            // Find the corresponding validator
            var validatorType = typeof(IValidateDto<>).MakeGenericType(dto.GetType());
            var validator = _serviceProvider.GetService(validatorType);

            if (validator == null) continue;

            // Invoke the Validate method dynamically
            var validateMethod = validatorType.GetMethod("Validate");
            var errors = (List<string>)validateMethod.Invoke(validator, new object[] { dto });
            if (errors.Any())
            {
                context.Result = new BadRequestObjectResult(errors);
                return;
            }

        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}

