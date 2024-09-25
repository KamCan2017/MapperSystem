using FluentValidation;

namespace DIRS21.DataModel.Validators;

public class RoomValidator : MirrorModelValidator<Room>
{
    public RoomValidator() : base()
    {
        RuleFor(r => r.Category).NotNull().NotEmpty();
        RuleFor(r => r.NumberOfBad).GreaterThanOrEqualTo(0);
        RuleFor(r => r.NumberOfBed).GreaterThanOrEqualTo(1);
    }
}
