using ModelMapper.Core.Interfaces;

namespace ModelMapper.Core.Converters;

/// <summary>
/// The converter filter provides converter for specific type
/// </summary>
public class ConverterProvider : IConverterProvider
{
    private readonly Dictionary<(Type,Type), ITypeConverter> _converters = [];

    public ConverterProvider()
    {
        CreateFilters();
    }
    private void CreateFilters()
    {
        //Add the default converter at first converter. It will be used as default converter
        ITypeConverter converter = new DefaultConverter();
        _converters.Add(converter.SourceTargetTypes, converter);

        //Add the guid converter
        converter = new GuidConverter();
        _converters.Add(converter.SourceTargetTypes, converter);

    }

    public object? Convert((Type,Type) sourceTargetType, object input)
    {
        if (!_converters.TryGetValue(sourceTargetType, out ITypeConverter? converter)) return _converters.First().Value.Invoke(input);
        return converter.Invoke(input);
    }
}
