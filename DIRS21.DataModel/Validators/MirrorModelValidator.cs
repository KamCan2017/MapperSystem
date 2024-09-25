using FluentValidation;

namespace DIRS21.DataModel.Validators;

/// <summary>
/// The mirror model validator
/// </summary>
/// <typeparam name="T">The mirror model type</typeparam>
public class MirrorModelValidator<T> : BaseMirrorModelValidator<T> where T : MirrorModel
{
    public MirrorModelValidator()
    {
        ConfigureRules();
    }

    protected override void ConfigureRules()
    {
        RuleFor(r => r.Id).NotNull().NotEmpty();
    }
}


