using FluentValidation;

namespace ExternalClient.DataModel.Google.Validators;

public class RentalCarValidator : GoogleModelValidator<RentalCar>
{
    public RentalCarValidator() : base()
    {
        RuleFor(r => r.Name).NotNull().NotEmpty();
        RuleFor(r => r.Description).NotNull().NotEmpty();
    }
}