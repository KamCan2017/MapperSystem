using DIRS21.Core.Result;

namespace ModelMapper.Core.Interfaces;

/// <summary>
/// The IMapHandler interface maps the source object to the target object
/// </summary>
public interface IMapHandler
{
    /// <summary>
    /// Map the given object the oject with a specific target type
    /// </summary>
    /// <param name="data">The source object</param>
    /// <param name="sourceType">The source type</param>
    /// <param name="targetType">The target type</param>
    /// <returns></returns>
    object Map(object data, string sourceType, string targetType);

    /// <summary>
    /// Maps the object with result.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="sourceType">Type of the source.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <returns>The result with the converted model</returns>
    OperationResult MapWithResult(object data, string sourceType, string targetType);

    /// <summary>
    /// Maps the collection of objects.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="sourceType">Type of the source.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <returns>The mapping collection</returns>
    /// <exception cref="System.ArgumentNullException"></exception>
    /// <exception cref="System.Exception">
    /// The source type must be equal to the input type {dataType.FullName}.
    /// or
    /// The {targetType} type is not valid.
    /// </exception>
    object MapCollection(object data, string sourceType, string targetType);

    /// <summary>
    /// Maps the collection with result.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="sourceType">Type of the source.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <returns>The result with the converted collection</returns>
    OperationResult MapCollectionWithResult(object data, string sourceType, string targetType);
}
