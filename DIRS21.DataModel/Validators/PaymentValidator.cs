using FluentValidation;

namespace DIRS21.DataModel.Validators;

public class PaymentValidator : MirrorModelValidator<Payment>
{
    public PaymentValidator() : base()
    {
        RuleFor(r => r.ServiceName).NotNull().NotEmpty();
        RuleFor(r => r.Token).NotNull().NotEmpty();
    }
}
