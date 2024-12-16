namespace ModelMapper.Core.Interfaces
{
    /// <summary>
    /// The converter provider interface that contains all converters
    /// </summary>
    public interface IConverterProvider
    {
        /// <summary>
        /// Converts the specified source target type.
        /// </summary>
        /// <param name="sourceTargetType">Type of the source target.</param>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        object? Convert((Type, Type) sourceTargetType, object input);
    }
}