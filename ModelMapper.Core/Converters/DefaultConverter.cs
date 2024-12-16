using ModelMapper.Core.Interfaces;
using NLog;

namespace ModelMapper.Core.Converters;

/// <summary>
/// The default converter converts an object to a object or a string
/// </summary>
/// <seealso cref="ModelMapper.Core.Interfaces.ITypeConverter" />
public class DefaultConverter : BaseConverter, ITypeConverter<object,object>
{
    /// <summary>
    /// Gets the type of the target.
    /// </summary>
    /// <value>
    /// The type of the target.
    /// </value>
    public (Type, Type) SourceTargetTypes => (typeof(object),typeof(object));

    /// <summary>
    /// Gets the method.
    /// </summary>
    /// <returns>The convertion method</returns>
    public Func<object, object> GetMethod()
    {
        Func<object, object> func = (input) =>
        {
            try
            {
                if (input is IConvertible)
                    return Convert.ChangeType(input, typeof(object));
                return input.ToString()!;
            }
            catch (Exception)
            {
                LogManager.GetCurrentClassLogger().Warn($"Convert {input} to object type failed");
                return null!;
            }
            
        };

        return func;
    }

    public override object Invoke(object obj)
    {
        return GetMethod().Invoke(obj);
    }
}

public abstract class BaseConverter
{
    public virtual object Invoke(object obj) => null!;
}
