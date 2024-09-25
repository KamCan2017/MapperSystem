using DIRS21.Core;
using DIRS21.Core.Result;
using ModelMapper.Core.Handlers.Payloads;
using ModelMapper.Core.Interfaces;
using NLog;
using System.Collections;
using System.Reflection;

namespace ModelMapper.Core.Handlers;

/// <summary>
/// The map handler maps the external model to the internal model by using the appropriate profiles
/// </summary>
/// <param name="modelProfiler">The model profiler</param>
/// <param name="converterProvider">The converter provider</param>
/// <param name="logger">The logger</param>
public class MapHandler(IModelProfiler modelProfiler, IConverterProvider converterProvider, ILogger logger) : IMapHandler
{
    private readonly IConverterProvider _converterProvider = converterProvider ?? throw new ArgumentNullException(nameof(IConverterProvider));
    private readonly IModelProfiler _modelProfiler = modelProfiler ?? throw new ArgumentNullException(nameof(IModelProfiler));
    private readonly ILogger _logger = logger ?? throw new ArgumentNullException(nameof(ILogger));


    /// <summary>
    /// Map the given object the oject with a specific target type
    /// </summary>
    /// <param name="data">The source object</param>
    /// <param name="sourceType">The source type</param>
    /// <param name="targetType">The target type</param>
    /// <returns>The mapping object</returns>
    public object Map(object data, string sourceType, string targetType)
    {
        object source = MapPayload(data, sourceType) ?? data;
        return MapObject(source, sourceType, targetType);
    }

    /// <summary>
    /// Maps the object with result.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="sourceType">Type of the source.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <returns>The result with the converted model</returns>
    public OperationResult MapWithResult(object data, string sourceType, string targetType)
    {
        try
        {
            object model = Map(data, sourceType, targetType);
            return new OperationResult(model, ResultType.Success, null);
        }
        catch (Exception exception)
        {
            logger.Error(exception);
            return new OperationResult(null, ResultType.Failed, $"{exception}");
        }
    }

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
    public object MapCollection(object data, string sourceType, string targetType)
    {
        ArgumentNullException.ThrowIfNull(data);
        ArgumentException.ThrowIfNullOrWhiteSpace(sourceType);
        ArgumentException.ThrowIfNullOrWhiteSpace(targetType);

        bool result = DoesObjectSupportListType(data);
        if (!result) throw new Exception("The input data is not a type of List<>.");
        Type dataType = data.GetType();
        if (dataType.FullName?.ToLowerInvariant() != sourceType.ToLowerInvariant())
            throw new Exception($"The source type must be equal to the input type {dataType.FullName}.");

        Type? detectedTargetType = Type.GetType($"{targetType},System.Collections") ?? throw new Exception($"The {targetType} type is not valid.");
        Type itemTagertType = detectedTargetType.GetGenericArguments().First();
        Type itemSourceType = GetListType(data);

        IList? dataList = data as IList;
        IList? targetModels = Activator.CreateInstance(detectedTargetType) as IList;

        foreach (object item in dataList!)
        {
            var targetModel = MapObject(item, itemSourceType.FullName!, itemTagertType.FullName!);
            targetModels?.Add(targetModel);
        }
        return targetModels!;
    }

    /// <summary>
    /// Maps the collection with result.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="sourceType">Type of the source.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <returns>The result with the converted collection</returns>
    public OperationResult MapCollectionWithResult(object data, string sourceType, string targetType)
    {
        try
        {
            object model = MapCollection(data, sourceType, targetType);
            return new OperationResult(model, ResultType.Success, null);
        }
        catch (Exception exception)
        {
            logger.Error(exception);
            return new OperationResult(null, ResultType.Failed, $"{exception}");
        }
    }


    /// <summary>
    /// Maps the object.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="sourceType">Type of the source.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <returns>The converted model</returns>
    /// <exception cref="System.ArgumentNullException"></exception>
    /// <exception cref="System.IO.InvalidDataException">The source model must be a {typeof(BaseModel)} type.</exception>
    /// <exception cref="System.Exception">
    /// The source data is invalid:{model.Errors}
    /// or
    /// The source type must be different from the target type.
    /// or
    /// No Profile is not available for the {sourceType} and {targetType}. Please create a new one.
    /// or
    /// The input data type {dataType} must be equal to the source type {sourceType}.
    /// or
    /// Create an instance for the type {targetType} failed.
    /// or
    /// The source data is invalid:{targetModel.Errors}
    /// </exception>
    private object MapObject(object data, string sourceType, string targetType)
    {
        ArgumentNullException.ThrowIfNull(data);
        if (data is not BaseModel model) throw new InvalidDataException($"The source model must be a {typeof(BaseModel)} type.");
        if (!model.IsValid) throw new Exception($"The source data is invalid:{model.Errors}");
        ArgumentException.ThrowIfNullOrWhiteSpace(sourceType);
        ArgumentException.ThrowIfNullOrWhiteSpace(targetType);
        if (sourceType.Equals(targetType, StringComparison.InvariantCultureIgnoreCase))
            throw new Exception($"The source type must be different from the target type.");

        IModelProfile? modelProfile = _modelProfiler.ExistProfile(sourceType, targetType) ?? throw new Exception($"No Profile is not available for the {sourceType} and {targetType}. Please create a new one.");
        string dataType = data.GetType().FullName!;
        if (!dataType.Equals(sourceType, StringComparison.InvariantCultureIgnoreCase))
            throw new Exception($"The input data type {dataType} must be equal to the source type {sourceType}.");


        //Create a new instance of the target type
        bool switchTarget = !modelProfile.TargetTypeName.Equals(targetType, StringComparison.InvariantCultureIgnoreCase);
        Type currentTargetType = switchTarget ? modelProfile.SourceType : modelProfile.TargetType;
        object? target = Activator.CreateInstance(currentTargetType) ?? throw new Exception($"Create an instance for the type {targetType} failed.");

        //Set values of the target object
        SetTargetValues(data, target, modelProfile, switchTarget);
        var targetModel = target as BaseModel;
        if (!targetModel!.IsValid) throw new Exception($"The source data is invalid:{targetModel.Errors}");

        return target!;

    }




    /// <summary>
    /// Verify if the input data is a specific format and convert it into a object
    /// </summary>
    /// <param name="payload">The payload</param>
    /// <param name="sourceType">The source type in which the payload should be converted</param>
    /// <returns>The converted object</returns>
    private static object? MapPayload(object payload, string sourceType)
    {
        return JsonPayloadHandler.MapJsonContentToObject(payload, sourceType) ?? XmlPayloadHandler.MapXmlContentToObject(payload, sourceType);
    }

    private void SetTargetValues(object source, object target, IModelProfile modelProfile, bool switchTarget = false)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(target);
            ArgumentNullException.ThrowIfNull(modelProfile, nameof(IModelProfile));
            PropertyInfo[] sourceProperties, targetProperties;
            if (switchTarget)
            {
                sourceProperties = modelProfile.TargetType.GetProperties();
                targetProperties = modelProfile.SourceType.GetProperties();
            }
            else
            {
                sourceProperties = modelProfile.SourceType.GetProperties();
                targetProperties = modelProfile.TargetType.GetProperties();
            }


            //Set values of the target object
            foreach (var targetProperty in targetProperties)
            {
                string relatedPropertyName = modelProfile.GetRelatedProperty(targetProperty.Name);
                if (string.IsNullOrWhiteSpace(relatedPropertyName)) continue; //In case that no mapping exists for a source property
                var sourceProperty = sourceProperties.FirstOrDefault(p => p.Name.Equals(relatedPropertyName, StringComparison.InvariantCultureIgnoreCase));
                if(sourceProperty == null) throw new NullReferenceException(nameof(sourceProperty));
                object? targetValue;
                var sourcePropertyValue = sourceProperty!.GetValue(source);
                if(sourcePropertyValue == null) continue;
                //In case of reference type
                if (sourceProperty.PropertyType.IsAssignableTo(typeof(BaseModel)) && targetProperty.PropertyType.IsAssignableTo(typeof(BaseModel)))
                {
                    targetValue = MapObject(sourcePropertyValue!, sourceProperty.PropertyType.FullName!, targetProperty.PropertyType.FullName!);
                }
                else
                {
                    targetValue = _converterProvider.GetConverterMethod(targetProperty.PropertyType)?.Invoke(sourcePropertyValue!);
                }
                targetProperty.SetValue(target, targetValue);
            }
        }
        catch (Exception exception)
        {
            _logger.Error(exception);
            throw;
        }
    }


    private static bool DoesObjectSupportListType(object list)
    {
        return list is IList &&
          list.GetType().IsGenericType &&
          list.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));

    }

    private static Type GetListType(object list)
    {
        return list == null ? throw new ArgumentNullException(nameof(list)) : list.GetType().GetGenericArguments().First();
    }

}
