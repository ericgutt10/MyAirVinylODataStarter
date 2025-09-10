using AirVinyContext.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Text.Json;

namespace App.ServerHost.CustomFilters
{
    public class ModelValidationFilter<TModel> : IEndpointFilter where TModel : class
    {
        private readonly IValidator<TModel> _validator;
        private readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web) { PropertyNameCaseInsensitive = true };


        public ModelValidationFilter(IValidator<TModel> validator)
        {
            _validator = validator;
        }

        async ValueTask<object?> IEndpointFilter.InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            try
            {
                TModel model = (TModel)context.Arguments.FirstOrDefault(a => a is TModel)!;

                var validationResult = await _validator.ValidateAsync(model);
                if (!validationResult.IsValid)
                {
                    // If Invalid then Read Error Messages
                    var errors = validationResult.Errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage);
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.HttpContext.Response.WriteAsJsonAsync(errors);
                }
            }


            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

            return await next(context);
        }
    }
}