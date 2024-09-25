using DIRS21.Core;
using DIRS21.DataModel.Interfaces;
using FluentValidation;

namespace DIRS21.DataModel;

/// <summary>
/// The base mirror model class
/// </summary>
/// <seealso cref="DIRS21.Core.BaseModel" />
/// <seealso cref="DIRS21.DataModel.Interfaces.IMirrorModel" />
public abstract class BaseMirrorModel(IValidator validator) : BaseModel(validator), IMirrorModel
{

}
