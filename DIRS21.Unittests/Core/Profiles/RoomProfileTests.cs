using ModelMapper.Core.Profiles;

namespace DIRS21.Unittests.Core.Profiles;

public class RoomProfileTests
{
    private RoomProfile _roomProfile;

    private static IEnumerable<TestCaseData> PropertiesUseCaseDatas => [
                  new TestCaseData(nameof(ExternalClient.DataModel.Google.Room.Id), nameof(DataModel.Room.Id).ToLowerInvariant()),
                  new TestCaseData(nameof(ExternalClient.DataModel.Google.Room.Category), nameof(DataModel.Room.Category).ToLowerInvariant()),
                  new TestCaseData(nameof(ExternalClient.DataModel.Google.Room.Details), nameof(DataModel.Room.Details).ToLowerInvariant()),
                  new TestCaseData(nameof(ExternalClient.DataModel.Google.Room.NumberOfBad), nameof(DataModel.Room.NumberOfBad).ToLowerInvariant()),
                  new TestCaseData(nameof(ExternalClient.DataModel.Google.Room.NumberOfBed), nameof(DataModel.Room.NumberOfBed).ToLowerInvariant()),
                  new TestCaseData("test", "")
                ];

    [SetUp]
    public void Setup()
    {
        _roomProfile = new RoomProfile();
    }

    [Test, TestCaseSource(nameof(PropertiesUseCaseDatas))]
    public void FindRelatedProperty_GetRelatedProperty_PropertyExist(string targetProperty, string sourceProperty)
    {
        string result = _roomProfile.GetRelatedProperty(targetProperty);

        Assert.That(result, Is.EqualTo(sourceProperty));
    }

    [Test]
    public void FindRelatedProperty_GetRelatedProperty_PropertyNotExist()
    {
        string sourceProperty = "test";
        string result = _roomProfile.GetRelatedProperty(sourceProperty);

        Assert.That(result, Is.Empty);
    }

    [Test, TestCase("", typeof(ArgumentException)), TestCase(" ", typeof(ArgumentException)), TestCase(null, typeof(ArgumentNullException))]
    public void FindRelatedProperty_GetRelatedPropertyThrowException_OperationFailed(string? sourceProperty, Type exceptionType)
    {
        Assert.Throws(exceptionType, () => _roomProfile.GetRelatedProperty(sourceProperty!));
    }

    [Test]
    public void FindSourceType_GetSourceType_SourceTypeIsSet()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_roomProfile.SourceType, Is.Not.Null);
            Assert.That(_roomProfile.SourceTypeName, Is.EqualTo(typeof(DataModel.Room).FullName!.ToLowerInvariant()));
        });
    }


    [Test]
    public void FindTargetType_GetTargetType_TargetTypeIsSet()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_roomProfile.TargetType, Is.Not.Null);
            Assert.That(_roomProfile.TargetTypeName, Is.EqualTo(typeof(ExternalClient.DataModel.Google.Room).FullName!.ToLowerInvariant()));
        });
    }

}
