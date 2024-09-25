using ModelMapper.Core.Interfaces;
using ModelMapper.Core.Profiles;

namespace DIRS21.Unittests.Core.Profiles;

public class ModelProfilerTests
{
    private ModelProfiler _modelProfiler;

    private static IEnumerable<TestCaseData> InvalidInputTypesUseCaseDatas => [
                 new TestCaseData(null, typeof(ExternalClient.DataModel.Google.Room).FullName, typeof(ArgumentNullException)),
                 new TestCaseData("", typeof(ExternalClient.DataModel.Google.Room).FullName,typeof(ArgumentException)),
                 new TestCaseData(" ", typeof(ExternalClient.DataModel.Google.Room).FullName, typeof(ArgumentException)),
                 new TestCaseData(typeof(DataModel.Room).FullName, null, typeof(ArgumentNullException)),
                 new TestCaseData(typeof(DataModel.Room).FullName, "", typeof(ArgumentException)),
                 new TestCaseData(typeof(DataModel.Room).FullName, " ", typeof(ArgumentException))
               ];

    private static IEnumerable<TestCaseData> ValidInputTypesUseCaseDatas => [
                new TestCaseData(typeof(DataModel.Room).FullName, typeof(ExternalClient.DataModel.Google.Room).FullName),
                 new TestCaseData(typeof(ExternalClient.DataModel.Google.Room).FullName, typeof(DataModel.Room).FullName)
               ];

    [SetUp]
    public void Setup()
    {
        _modelProfiler = new ModelProfiler();
    }



    [Test, TestCaseSource(nameof(InvalidInputTypesUseCaseDatas))]
    public void FindProfile_ExistProfileThrowException_OperationFailed(string? sourceType, string? targetType, Type exceptionType)
    {
        Assert.Throws(exceptionType, () => _modelProfiler.ExistProfile(sourceType!, targetType!));
    }

    [Test, TestCaseSource(nameof(ValidInputTypesUseCaseDatas))]
    public void FindProfile_ExistProfile_ProfileCanBeFound(string? sourceType, string? targetType)
    {
        IModelProfile? modelProfile = _modelProfiler.ExistProfile(sourceType!, targetType!);
        Assert.That(modelProfile, Is.Not.Null);
        Assert.That(modelProfile, Is.TypeOf<RoomProfile>());
    }
}
