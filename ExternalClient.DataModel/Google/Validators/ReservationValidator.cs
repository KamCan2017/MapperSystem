using FluentValidation;

namespace ExternalClient.DataModel.Google.Validators;

public class ReservationValidator : GoogleModelValidator<Reservation>
{
    public ReservationValidator()
    {
        RuleFor(r => r.CustomerId).NotNull().NotEmpty();
        RuleFor(r => r.End).GreaterThan(x => x.Start);
        RuleFor(r => r.RentalCar).SetValidator(new RentalCarValidator()!);

    }
}
