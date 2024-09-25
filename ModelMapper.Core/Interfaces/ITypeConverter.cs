namespace ModelMapper.Core.Interfaces;

/// <summary>
/// The type converter interface
/// </summary>
public interface ITypeConverter
{
    /// <summary>
    /// Gets the method to convert an object to another target type.
    /// </summary>
    /// <returns>The convertion method</returns>
    Func<object, object> GetMethod();

    /// <summary>
    /// Gets the type of the target.
    /// </summary>
    /// <value>
    /// The type of the target.
    /// </value>
    Type TargetType { get; }
}
