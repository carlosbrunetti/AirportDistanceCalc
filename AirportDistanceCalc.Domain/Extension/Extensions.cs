using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AirportDistanceCalc.Domain.Extension
{
    public static class Extensions
    {
        //TODO Criar DTO de retorno de erro
        public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
        {
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    modelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
        }

        public static string ConvertMetersToMiles(double value)
        {
            return (value / 1609).ToString("0.##");
        }

    }
}
