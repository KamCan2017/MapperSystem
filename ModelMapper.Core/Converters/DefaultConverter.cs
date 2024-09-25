using ModelMapper.Core.Interfaces;

namespace ModelMapper.Core.Converters;

/// <summary>
/// The default converter converts an object to a object or a string
/// </summary>
/// <seealso cref="ModelMapper.Core.Interfaces.ITypeConverter" />
public class DefaultConverter : ITypeConverter
{
    /// <summary>
    /// Gets the type of the target.
    /// </summary>
    /// <value>
    /// The type of the target.
    /// </value>
    public Type TargetType => typeof(object);

    /// <summary>
    /// Gets the method.
    /// </summary>
    /// <returns>The convertion method</returns>
    public Func<object, object> GetMethod()
    {
        Func<object, object> func = (input) =>
        {
            if (input is IConvertible)
                return Convert.ChangeType(input, typeof(object));
            return input.ToString()!;
        };

        return func;
    }
}
