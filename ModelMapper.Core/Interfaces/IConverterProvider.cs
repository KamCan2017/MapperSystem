namespace ModelMapper.Core.Interfaces
{
    /// <summary>
    /// The converter provider interface that contains all converters
    /// </summary>
    public interface IConverterProvider
    {
        /// <summary>
        /// Gets the converter method.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The converter method</returns>
        Func<object, object>? GetConverterMethod(Type type);
    }
}