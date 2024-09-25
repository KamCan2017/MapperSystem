using DIRS21.DataModel;
using ModelMapper.Core.Converters;
using ModelMapper.Core.Handlers;
using ModelMapper.Core.Interfaces;
using Newtonsoft.Json;
using NLog;
using NSubstitute;
using System.Xml.Serialization;

namespace DIRS21.Unittests.Core.Handler;

public class MapHandlerTests
{
    private IModelProfiler _modelProfiler;
    private IModelProfile _modelProfile;
    private IConverterProvider _converterProvider;
    private ILogger _logger;
    private MapHandler _mapHandler;
    private Room _validModel;


    [SetUp]
    public void Setup()
    {
        _logger = LogManager.GetCurrentClassLogger();
        _converterProvider = new ConverterProvider();
        _modelProfiler = Substitute.For<IModelProfiler>();
        _modelProfile = Substitute.For<IModelProfile>();
        _modelProfile.GetRelatedProperty(nameof(Room.Id)).Returns(nameof(ExternalClient.DataModel.Google.Room.Id));
        _modelProfile.GetRelatedProperty(nameof(Room.Category)).Returns(nameof(ExternalClient.DataModel.Google.Room.Category));
        _modelProfile.GetRelatedProperty(nameof(Room.Details)).Returns(nameof(ExternalClient.DataModel.Google.Room.Details));
        _modelProfile.GetRelatedProperty(nameof(Room.NumberOfBad)).Returns(nameof(ExternalClient.DataModel.Google.Room.NumberOfBad));
        _modelProfile.GetRelatedProperty(nameof(Room.NumberOfBed)).Returns(nameof(ExternalClient.DataModel.Google.Room.NumberOfBed));

        _mapHandler = new MapHandler(_modelProfiler, _converterProvider, _logger);
        _validModel = new Room() { Id = "1587-1457-p90", NumberOfBad = 1, NumberOfBed = 3, Category = "family", Details = "With animals themes..." };
    }

    [Test]
    public void Map_ThrowsArgumentNullException_WhenModelProfilerIsNull()
    {
        var invalidData = new object();
        IModelProfiler modelProfiler = null!;
        Assert.Throws<ArgumentNullException>(() => _mapHandler = new MapHandler(modelProfiler, _converterProvider, _logger));
    }

    [Test]
    public void Map_ThrowsArgumentNullException_WhenConverterProviderIsNull()
    {
        var invalidData = new object();
        IConverterProvider converterProvider = null!;
        Assert.Throws<ArgumentNullException>(() => _mapHandler = new MapHandler(_modelProfiler, converterProvider, _logger));
    }

    [Test]
    public void Map_ThrowsArgumentNullException_WhenLoggerIsNull()
    {
        var invalidData = new object();
        ILogger logger = null!;
        Assert.Throws<ArgumentNullException>(() => _mapHandler = new MapHandler(_modelProfiler, _converterProvider, logger));
    }


    [Test]
    public void Map_ThrowsInvalidDataException_WhenDataIsNotBaseModel()
    {
        var invalidData = new object();
        Assert.Throws<InvalidDataException>(() => _mapHandler.Map(invalidData, "SourceType", "TargetType"));
    }

    [Test]
    public void Map_ThrowsException_WhenModelIsNotValid()
    {
        var invalidModel = new DataModel.Room();

        Assert.Throws<Exception>(() => _mapHandler.Map(invalidModel, typeof(Room).FullName!, typeof(ExternalClient.DataModel.Google.Room).FullName!),
            "The source data is invalid: Invalid model data");
    }

    [Test]
    public void Map_ThrowsArgumentNullException_WhenDataIsNull()
    {
        object data = null!;
        Assert.Throws<ArgumentNullException>(() => _mapHandler.Map(data, typeof(Room).FullName!, typeof(ExternalClient.DataModel.Google.Room).FullName!));
    }

    [Test]
    public void Map_ThrowsInvalidException_WhenDataIsNotFormBaseModel()
    {
        Assert.Throws<InvalidDataException>(() => _mapHandler.Map(new object(), typeof(Room).FullName!, typeof(ExternalClient.DataModel.Google.Room).FullName!));
    }

    [Test]
    public void Map_ThrowsArgumentException_WhenSourceTypeOrTargetTypeIsNullOrEmpty()
    {
        string type = null!;
        Assert.Throws<ArgumentNullException>(() => _mapHandler.Map(_validModel, type, "TargetType"));
        Assert.Throws<ArgumentNullException>(() => _mapHandler.Map(_validModel, "SourceType", type));
        Assert.Throws<ArgumentException>(() => _mapHandler.Map(_validModel, string.Empty, "TargetType"));
        Assert.Throws<ArgumentException>(() => _mapHandler.Map(_validModel, " ", "TargetType"));
        Assert.Throws<ArgumentException>(() => _mapHandler.Map(_validModel, "SourceType", string.Empty));
        Assert.Throws<ArgumentException>(() => _mapHandler.Map(_validModel, "SourceType", " "));
    }

    [Test]
    public void Map_ThrowsException_WhenSourceTypeAndTargetTypeAreSame()
    {
        Assert.Throws<Exception>(() => _mapHandler.Map(_validModel, "SourceType", "SourceType"),
            "The source type must be different from the target type.");
    }

    [Test]
    public void Map_ThrowsException_WhenProfileDoesNotExist()
    {
        IModelProfile? profile = null;
        _modelProfiler.ExistProfile("SourceType", "TargetType").Returns(profile);

        Assert.Throws<Exception>(() => _mapHandler.Map(_validModel, "SourceType", "TargetType"),
            "No Profile is available for the SourceType and TargetType. Please create a new one.");
    }

    [Test]
    public void Map_ThrowsException_WhenDataTypeIsNotEqualToSourceType()
    {
        _modelProfiler.ExistProfile("SourceType", "TargetType").Returns(Substitute.For<IModelProfile>());

        Assert.Throws<Exception>(() => _mapHandler.Map(_validModel, "InvalidSourceType", "TargetType"),
            $"The input data type {_validModel.GetType().FullName} must be equal to the source type InvalidSourceType.");
    }

    [Test]
    public void Map_SuccessfullyMapsData_WhenAllConditionsAreMet()
    {
        var sourcetype = typeof(Room);
        var targertType = typeof(ExternalClient.DataModel.Google.Room);

        _modelProfile.SourceType.Returns(sourcetype);
        _modelProfile.TargetType.Returns(targertType);
        _modelProfile.TargetTypeName.Returns(targertType.FullName!.ToLowerInvariant()!);
        _modelProfiler.ExistProfile(sourcetype.FullName!, targertType.FullName).Returns(_modelProfile);


        var result = _mapHandler.Map(_validModel, sourcetype.FullName!, targertType.FullName);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<ExternalClient.DataModel.Google.Room>());
    }

    [Test]
    public void Map_SuccessfullyMapsJsonData_WhenAllConditionsAreMet()
    {
        var sourcetype = typeof(Room);
        var targertType = typeof(ExternalClient.DataModel.Google.Room);

        _modelProfile.SourceType.Returns(sourcetype);
        _modelProfile.TargetType.Returns(targertType);
        _modelProfile.TargetTypeName.Returns(targertType.FullName!.ToLowerInvariant()!);
        _modelProfiler.ExistProfile(sourcetype.FullName!, targertType.FullName).Returns(_modelProfile);
        string payload = JsonConvert.SerializeObject(_validModel);

        var result = _mapHandler.Map(payload, sourcetype.FullName!, targertType.FullName);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<ExternalClient.DataModel.Google.Room>());
    }

    [Test]
    public void Map_SuccessfullyMapsXmlData_WhenAllConditionsAreMet()
    {
        var sourcetype = typeof(Payment);
        var targertType = typeof(ExternalClient.DataModel.Google.Payment);

        var modelProfile = Substitute.For<IModelProfile>();
        modelProfile.GetRelatedProperty(nameof(Payment.Id)).Returns(nameof(ExternalClient.DataModel.Google.Payment.Id));
        modelProfile.GetRelatedProperty(nameof(Payment.ServiceName)).Returns(nameof(ExternalClient.DataModel.Google.Payment.ServiceName));
        modelProfile.GetRelatedProperty(nameof(Payment.Token)).Returns(nameof(ExternalClient.DataModel.Google.Payment.Token));


        modelProfile.SourceType.Returns(sourcetype);
        modelProfile.TargetType.Returns(targertType);
        modelProfile.TargetTypeName.Returns(targertType.FullName!.ToLowerInvariant()!);
        _modelProfiler.ExistProfile(sourcetype.FullName!, targertType.FullName).Returns(modelProfile);

        ExternalClient.DataModel.Google.Payment payment = new() { Id = "14", ServiceName = "paypal", Token = "xdr-789-tr" };

        XmlSerializer serializer = new XmlSerializer(typeof(ExternalClient.DataModel.Google.Payment));
        string? xmlContent;
        // Use StringReader to convert the XML string to a readable stream for XmlSerializer.
        using (StringWriter writer = new())
        {
            serializer.Serialize(writer, payment);
            xmlContent = writer.ToString();
        }

        var result = _mapHandler.Map(xmlContent, sourcetype.FullName!, targertType.FullName);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<ExternalClient.DataModel.Google.Payment>());
    }

    [Test]
    public void MapCollection_ThrowsArgumentException_WhenSourceTypeOrTargetTypeIsNullOrEmpty()
    {
        string type = null!;
        var list = new List<ExternalClient.DataModel.Google.Room>();
        Assert.Throws<ArgumentNullException>(() => _mapHandler.MapCollection(list, type, "TargetType"));
        Assert.Throws<ArgumentNullException>(() => _mapHandler.MapCollection(list, "SourceType", type));
        Assert.Throws<ArgumentException>(() => _mapHandler.MapCollection(list, string.Empty, "TargetType"));
        Assert.Throws<ArgumentException>(() => _mapHandler.MapCollection(list, " ", "TargetType"));
        Assert.Throws<ArgumentException>(() => _mapHandler.MapCollection(list, "SourceType", string.Empty));
        Assert.Throws<ArgumentException>(() => _mapHandler.MapCollection(list, "SourceType", " "));
    }

    [Test]
    public void MapCollection_ThrowsArgumentNullException_WhenSourceIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => _mapHandler.MapCollection(null!, "type", "TargetType"));
    }

    [Test]
    public void MapCollection_ThrowsException_WhenSourceTypeIsNotEqualToDataType()
    {
        Type sourceType = typeof(List<object>);
        Type targetType = typeof(List<Room>);
        var list = new List<ExternalClient.DataModel.Google.Room>();
        Assert.Throws<Exception>(() => _mapHandler.MapCollection(list, sourceType.FullName!, targetType.FullName!));
    }

    [Test]
    public void MapCollection_ThrowsException_WhenTargetTypeIsNotAIListType()
    {
        Type sourceType = typeof(List<ExternalClient.DataModel.Google.Room>);
        Type targetType = typeof(Room[]);
        var list = new List<ExternalClient.DataModel.Google.Room>();
        Assert.Throws<Exception>(() => _mapHandler.MapCollection(list, sourceType.FullName!, targetType.FullName!));
    }

}
