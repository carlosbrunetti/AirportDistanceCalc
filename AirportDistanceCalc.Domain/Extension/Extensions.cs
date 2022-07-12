using AirportDistanceCalc.Domain.Enum;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;

namespace AirportDistanceCalc.Domain.Extension
{
    public static class Extensions
    {
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

        public static string ConvertMetersTo(double value, UnitOfMeasureEnum unitOfMeasure)
        {
            switch (unitOfMeasure)
            {
                case UnitOfMeasureEnum.Miles:
                    return (value / 1609).ToString("0.##");
                case UnitOfMeasureEnum.Kilometers:
                    return (value / 1000).ToString("0.##");
                default:
                    throw new Exception("Unit of measure to conversion invalid.");
            }
        }

        public static string Description<T>(this T enumValue) where T : IConvertible
        {
            var descriptionAttribute = enumValue.GetType()
                .GetField(enumValue.ToString())
                .GetCustomAttributes(false)
                .SingleOrDefault(attr => attr.GetType() == typeof(DescriptionAttribute)) as DescriptionAttribute;

            return descriptionAttribute?.Description ?? "";
        }

    }
}
