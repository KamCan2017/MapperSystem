using DIRS21.Core;
using FluentValidation;

namespace ExternalClient.DataModel.Google;

/// <summary>
/// The google base model
/// </summary>
/// <param name="validator">The validator</param>
public abstract class GoogleModel(IValidator validator) : BaseModel(validator)
{
    public string Id { get; set; } = string.Empty;

}
