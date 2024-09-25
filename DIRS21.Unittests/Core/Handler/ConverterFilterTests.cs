using ModelMapper.Core.Converters;

namespace DIRS21.Unittests.Core.Handler;

public class ConverterFilterTests
{
    private ConverterProvider _converterProvider;

    [SetUp]
    public void Setup()
    {
        _converterProvider = new ConverterProvider();
    }

    [Test]
    public void FindGUIDConverter_GetConverter_ConverterIsFound()
    {
        Func<object, object>? func = _converterProvider.GetConverterMethod(typeof(Guid));
        Assert.That(func, Is.Not.Null);
    }

    [Test]
    public void FindGUIDConverter_GetConverter_ConverterCanBeInvoked()
    {
        Func<object, object>? func = _converterProvider.GetConverterMethod(typeof(Guid));
        var result = func?.Invoke("445CFA2E-7707-426D-8457-A0D7D6003362");

        Assert.That(result, Is.TypeOf<Guid>());
    }

    [Test]
    public void FindGUIDConverter_GetConverter_ReturnEmptyGuid()
    {
        Func<object, object>? func = _converterProvider.GetConverterMethod(typeof(Guid));
        var result = func?.Invoke("");

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void FindDefaultConverter_GetConverter_ReturnObjectType()
    {
        int input = 10_000;
        Func<object, object>? func = _converterProvider.GetConverterMethod(typeof(int));
        var result = func?.Invoke(input);

        Assert.That(result, Is.EqualTo(input));
    }

    [Test]
    public void CreateFilters_Should_Add_DefaultConverter()
    {
        object input = 10_000f;
        Func<object, object>? func = _converterProvider.GetConverterMethod(typeof(object)); // Assuming object is the default target type
        var result = func?.Invoke(input);

        Assert.That(func, Is.Not.Null);
        Assert.That(result, Is.EqualTo(input));

    }


}
