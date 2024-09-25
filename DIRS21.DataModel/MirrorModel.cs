using FluentValidation;

namespace DIRS21.DataModel;

/// <summary>
/// The base model class for the DIRS21 system
/// </summary>
/// <param name="validator">The validator</param>
public abstract class MirrorModel(IValidator validator) : BaseMirrorModel(validator)
{
    public string Id { get; set; } = string.Empty;

}


