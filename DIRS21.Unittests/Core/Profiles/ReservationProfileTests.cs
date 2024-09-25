using ModelMapper.Core.Profiles;

namespace DIRS21.Unittests.Core.Profiles;

public class ReservationProfileTests
{
    private ReservationProfile _reservationProfile;

    private static IEnumerable<TestCaseData> PropertiesUseCaseDatas => [
                  new TestCaseData(nameof(ExternalClient.DataModel.Google.Reservation.Id), nameof(DataModel.Reservation.ReservationId).ToLowerInvariant()),
                  new TestCaseData(nameof(ExternalClient.DataModel.Google.Reservation.Start), nameof(DataModel.Reservation.Start).ToLowerInvariant()),
                  new TestCaseData(nameof(ExternalClient.DataModel.Google.Reservation.End), nameof(DataModel.Reservation.End).ToLowerInvariant()),
                  new TestCaseData("test", "")
                ];

    [SetUp]
    public void Setup()
    {
        _reservationProfile = new ReservationProfile();
    }

    [Test, TestCaseSource(nameof(PropertiesUseCaseDatas))]
    public void FindRelatedProperty_GetRelatedProperty_PropertyExist(string targetProperty, string sourceProperty)
    {
        string result = _reservationProfile.GetRelatedProperty(targetProperty);

        Assert.That(result, Is.EqualTo(sourceProperty));
    }

    [Test]
    public void FindRelatedProperty_GetRelatedProperty_PropertyNotExist()
    {
        string sourceProperty = "test";
        string result = _reservationProfile.GetRelatedProperty(sourceProperty);

        Assert.That(result, Is.Empty);
    }



    [Test]
    public void FindSourceType_GetSourceType_SourceTypeIsSet()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_reservationProfile.SourceType, Is.Not.Null);
            Assert.That(_reservationProfile.SourceTypeName, Is.EqualTo(typeof(DataModel.Reservation).FullName!.ToLowerInvariant()));
        });
    }


    [Test]
    public void FindTargetType_GetTargetType_TargetTypeIsSet()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_reservationProfile.TargetType, Is.Not.Null);
            Assert.That(_reservationProfile.TargetTypeName, Is.EqualTo(typeof(ExternalClient.DataModel.Google.Reservation).FullName!.ToLowerInvariant()));
        });
    }

}
