using AirportDistanceCalc.Domain.Models.Request;
using FluentValidation;

namespace AirportDistanceCalc.Domain.Validations
{
    public class AirportCalcRequestValidation : AbstractValidator<AirportCalcRequest>
    {
        public AirportCalcRequestValidation()
        {
            RuleFor(x => x.Origin)
                .Must(x => !int.TryParse(x, out _)).WithMessage("IATA code must be alphanumeric.")
                .Length(3).WithMessage("IATA code must have 3 characters.");

            RuleFor(x => x.Destination)
                .Must(x => !int.TryParse(x, out _)).WithMessage("IATA code must be alphanumeric.")
                .Length(3).WithMessage("IATA code must have 3 characters.");

            RuleFor(x => x.Origin).Must((model, field) => !field.Equals(model.Destination)).WithMessage("The airports must be different.");

        }
    }
}
