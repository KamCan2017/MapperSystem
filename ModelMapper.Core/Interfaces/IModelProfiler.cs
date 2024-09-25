namespace ModelMapper.Core.Interfaces;

/// <summary>
/// The IModelProfiler provides all the configuration required to map the objects
/// </summary>
public interface IModelProfiler
{
    /// <summary>
    /// Check if a profile exists for the source and target type
    /// </summary>
    /// <param name="sourceType">The source type</param>
    /// <param name="targetType">The target type</param>
    /// <returns>The correponsing profile</returns>
    IModelProfile? ExistProfile(string sourceType, string targetType);

}
