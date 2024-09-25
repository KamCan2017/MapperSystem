using FluentValidation;

namespace ExternalClient.DataModel.Google.Validators;

/// <summary>
/// The googl model validator
/// </summary>
/// <typeparam name="T">The google model type</typeparam>
public class GoogleModelValidator<T> : AbstractValidator<T> where T : GoogleModel
{
    public GoogleModelValidator()
    {
        ConfigureRules();
    }

    protected virtual void ConfigureRules()
    {
        RuleFor(r => r.Id).NotNull().NotEmpty();
    }
}
