namespace ModelMapper.Core.Interfaces;

/// <summary>
/// The type converter interface
/// </summary>
internal interface ITypeConverter<T,V>: ITypeConverter
{
    /// <summary>
    /// Gets the method to convert an object to another target type.
    /// </summary>
    /// <returns>The convertion method</returns>
    //Func<object, object> GetMethod();

    Func<T, V> GetMethod();

}

public interface ITypeConverter
{
    /// <summary>
    /// Gets the source-type.
    /// </summary>
    /// <value>
    /// The source type paramters.
    /// </value>
    (Type, Type) SourceTargetTypes { get; }

    /// <summary>
    /// Invokes the specified object.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    object Invoke(object obj);
}
