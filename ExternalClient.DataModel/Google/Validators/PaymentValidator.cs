using FluentValidation;

namespace ExternalClient.DataModel.Google.Validators;

public class PaymentValidator : GoogleModelValidator<Payment>
{
    public PaymentValidator() : base()
    {
        RuleFor(r => r.ServiceName).NotNull().NotEmpty();
        RuleFor(r => r.Token).NotNull().NotEmpty();
    }
}
