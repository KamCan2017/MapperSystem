using ModelMapper.Core.Interfaces;
using System.Reflection;

namespace ModelMapper.Core.Profiles;

/// <summary>
///  The ModelProfiler provides all the configuration required to map the objects
/// </summary>
public class ModelProfiler : IModelProfiler
{

    private readonly List<IModelProfile> _profiles = [];

    public ModelProfiler()
    {
        LoadProfiles();
    }

    /// <summary>
    /// Check if a profile exists for the source and target type
    /// </summary>
    /// <param name="sourceType">The source type</param>
    /// <param name="targetType">The target type</param>
    /// <returns>
    /// The correponsing profile
    /// </returns>
    public IModelProfile? ExistProfile(string sourceType, string targetType)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sourceType);
        ArgumentException.ThrowIfNullOrWhiteSpace(targetType);
        IModelProfile? modelProfile =
            _profiles.SingleOrDefault(p => (p.SourceTypeName.Equals(sourceType, StringComparison.InvariantCultureIgnoreCase)
             && p.TargetTypeName.Equals(targetType, StringComparison.InvariantCultureIgnoreCase))
                                 ||
                                 (p.SourceTypeName.Equals(targetType, StringComparison.InvariantCultureIgnoreCase)
             && p.TargetTypeName.Equals(sourceType, StringComparison.InvariantCultureIgnoreCase)));

        return modelProfile;
    }

    private void LoadProfiles()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        Type lookupType = typeof(IModelProfile);
        IEnumerable<Type> lookupTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => lookupType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

        foreach (Type type in lookupTypes)
        {
            var modelProfile = (IModelProfile)Activator.CreateInstance(type)!;
            if (ExistProfile(modelProfile.SourceTypeName, modelProfile.TargetTypeName) != null) continue;
            _profiles.Add(modelProfile);
        }
    }
}
