using FluentValidation;

namespace DIRS21.DataModel.Validators;

public class RentalCarValidator : BaseMirrorModelValidator<RentalCar>
{
    public RentalCarValidator() : base()
    {
        RuleFor(r => r.Name).NotNull().NotEmpty();
        RuleFor(r => r.CarId).NotNull().NotEmpty();
        RuleFor(r => r.Description).NotNull().NotEmpty();
    }
}