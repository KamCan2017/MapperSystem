using FluentValidation;

namespace DIRS21.DataModel.Validators;

public class ReservationValidator : BaseMirrorModelValidator<Reservation>
{
    public ReservationValidator()
    {
        RuleFor(r => r.CustomerId).NotNull().NotEmpty();
        RuleFor(r => r.ReservationId).Custom((value, context) =>
        {
            if (value == Guid.Empty)
                context.AddFailure($"The value of {nameof(Reservation.ReservationId)} property must not be empty.");
        });
        RuleFor(r => r.End).GreaterThan(x => x.Start);
        RuleFor(r => r.RentalCar).SetValidator(new RentalCarValidator()!);  
    }
}
