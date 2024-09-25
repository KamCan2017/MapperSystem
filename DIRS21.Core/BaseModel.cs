using FluentValidation;
using FluentValidation.Results;
using System.Diagnostics.CodeAnalysis;

namespace DIRS21.Core
{
    /// <summary>
    /// The base entity class for the entities
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="BaseModel"/> class.
    /// </remarks>
    /// <param name="validator">The validator.</param>
    public abstract class BaseModel([NotNull] IValidator validator)
    {
        public string Errors => Validate().ToString();

        public bool IsValid => Validate().IsValid;

       protected readonly IValidator _validator = validator ?? throw new ArgumentNullException(nameof(IValidator));

        protected ValidationResult Validate() => _validator.Validate(new ValidationContext<BaseModel>(this));


    }
}
