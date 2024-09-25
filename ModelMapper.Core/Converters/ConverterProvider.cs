using ModelMapper.Core.Interfaces;

namespace ModelMapper.Core.Converters;

/// <summary>
/// The converter filter provides converter for specific type
/// </summary>
public class ConverterProvider : IConverterProvider
{
    private readonly Dictionary<Type, ITypeConverter> _converters = [];

    public ConverterProvider()
    {
        CreateFilters();
    }
    private void CreateFilters()
    {
        //Add the default converter at first converter. It will be used as default converter
        ITypeConverter converter = new DefaultConverter();
        _converters.Add(converter.TargetType, converter);

        //Add the guid converter
        converter = new GuidConverter();
        _converters.Add(converter.TargetType, converter);

    }

    public Func<object, object>? GetConverterMethod(Type type)
    {
        if (!_converters.TryGetValue(type, out ITypeConverter? converter)) return _converters.First().Value.GetMethod();
        return converter.GetMethod();
    }
}
