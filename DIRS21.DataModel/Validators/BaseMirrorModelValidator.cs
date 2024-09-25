using FluentValidation;

namespace DIRS21.DataModel.Validators;

/// <summary>
/// The base validator class for the datamodel.
/// </summary>
/// <typeparam name="T">The mirror model type</typeparam>
public class BaseMirrorModelValidator<T> : AbstractValidator<T> where T : BaseMirrorModel
{
    public BaseMirrorModelValidator()
    {
        ConfigureRules();
    }

    protected virtual void ConfigureRules()
    {

    }
}
