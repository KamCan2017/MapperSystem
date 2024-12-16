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
    public void FindGUIDConverter_GetConverter_ConverterCanBeInvoked()
    {
        var result = _converterProvider.Convert((typeof(string),typeof(Guid)),"445CFA2E-7707-426D-8457-A0D7D6003362");

        Assert.That(result, Is.TypeOf<Guid>());
    }

    [Test]
    public void FindGUIDConverter_GetConverter_ReturnEmptyGuid()
    {
        var result = _converterProvider.Convert((typeof(string), typeof(Guid)), " ");

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void FindDefaultConverter_GetConverter_ReturnObjectType()
    {
        int input = 10_000;
        var result = _converterProvider.Convert((typeof(object), typeof(int)), input);

        Assert.That(result, Is.EqualTo(input));
    }

    [Test]
    public void CreateFilters_Should_Add_DefaultConverter()
    {
        object input = 10_000f;
        var result = _converterProvider.Convert((typeof(object), typeof(object)), input);

        Assert.That(result, Is.EqualTo(input));

    }


}
