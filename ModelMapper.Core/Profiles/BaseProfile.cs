using DIRS21.DataModel.Interfaces;
using ExternalClient.DataModel.Interfaces;
using ModelMapper.Core.Interfaces;

namespace ModelMapper.Core.Profiles;


/// <summary>
/// The abstract class for the model profile
/// </summary>
/// <typeparam name="T">The source model</typeparam>
/// <typeparam name="V">The target model</typeparam>
public abstract class BaseProfile<T, V> : IModelProfile
 where T : IMirrorModel, new()
 where V : IExternalModel, new()
{
    /// <summary>
    /// Properties mapping collection with reverse functionality, so that only one profile is used to 
    /// convert the models in both direction.
    /// </summary>
    protected readonly List<(string, string)> _propertiesMappings = [];
    protected BaseProfile()
    {
        BuildProfile();
    }

    public string SourceTypeName => typeof(T).FullName!.ToLowerInvariant();
    public string TargetTypeName => typeof(V).FullName!.ToLowerInvariant();

    public Type TargetType => typeof(V);
    public Type SourceType => typeof(T);

    /// <summary>
    /// Get the related property
    /// </summary>
    /// <param name="propertyName">The property name</param>
    /// <returns>
    /// The mapped property
    /// </returns>
    public string GetRelatedProperty(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);
        string key = propertyName.ToLowerInvariant();
        (string, string) mapping = _propertiesMappings.SingleOrDefault(p => p.Item1 == key || p.Item2 == key);
        if (string.IsNullOrWhiteSpace(mapping.Item1))
            return string.Empty;
        //ToDO Force mapping for all properties or not?
        //throw new Exception($"No mapping property for the {sourceProperty} is set. Adjust the profile to fix it.");

        return mapping.Item1 == key ? mapping.Item2 : mapping.Item1;
    }

    protected virtual void BuildProfile()
    {

    }

    protected void InsertPropetiesMapping(string sourceProperty, string targetProperty)
    {
        if (string.IsNullOrWhiteSpace(GetRelatedProperty(sourceProperty)) || string.IsNullOrWhiteSpace(targetProperty))
        {
            _propertiesMappings.Add((sourceProperty, targetProperty));
        }
        else throw new Exception($"A mapping for the properties {sourceProperty} and {targetProperty} is already available for the {GetType().Name}.");
    }
}
