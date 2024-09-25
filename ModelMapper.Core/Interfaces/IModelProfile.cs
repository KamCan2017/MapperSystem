namespace ModelMapper.Core.Interfaces;

/// <summary>
/// The IModelProfile interface provides the target property depending of the source property
/// </summary>
public interface IModelProfile
{
    /// <summary>
    /// Get the related property
    /// </summary>
    /// <param name="propertyName">The property name</param>
    /// <returns>The mapped property</returns>
    string GetRelatedProperty(string propertyName);

    /// <summary>
    /// The source type name
    /// </summary>
    string SourceTypeName { get; }

    /// <summary>
    /// The target type name
    /// </summary>
    string TargetTypeName { get; }

    /// <summary>
    /// The target type
    /// </summary>
    Type TargetType { get; }

    /// <summary>
    /// The source type
    /// </summary>
    Type SourceType { get; }
}
