using FluentValidation;

namespace ExternalClient.DataModel.Google.Validators;

public class RoomValidator : GoogleModelValidator<Room>
{
    public RoomValidator() : base()
    {
        RuleFor(r => r.Category).NotNull().NotEmpty();
        RuleFor(r => r.NumberOfBad).GreaterThanOrEqualTo(0);
        RuleFor(r => r.NumberOfBed).GreaterThanOrEqualTo(1);
    }
}
