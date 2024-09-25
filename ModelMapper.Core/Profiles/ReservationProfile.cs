using sourceNamespace = DIRS21.DataModel;
using targetNamespace = ExternalClient.DataModel.Google;

namespace ModelMapper.Core.Profiles;

/// <summary>
/// The profile for the reservation models
/// </summary>
/// <seealso cref="ModelMapper.Core.Profiles.BaseProfile&lt;DIRS21.DataModel.Reservation, ExternalClient.DataModel.Google.Reservation&gt;" />
public class ReservationProfile : BaseProfile<sourceNamespace.Reservation, targetNamespace.Reservation>
{
    public ReservationProfile() : base()
    {
    }
    protected override void BuildProfile()
    {
        InsertPropetiesMapping(nameof(targetNamespace.Reservation.Id).ToLowerInvariant(), nameof(sourceNamespace.Reservation.ReservationId).ToLowerInvariant());
        InsertPropetiesMapping(nameof(targetNamespace.Reservation.CustomerId).ToLowerInvariant(), nameof(sourceNamespace.Reservation.CustomerId).ToLowerInvariant());
        InsertPropetiesMapping(nameof(targetNamespace.Reservation.Start).ToLowerInvariant(), nameof(sourceNamespace.Reservation.Start).ToLowerInvariant());
        InsertPropetiesMapping(nameof(targetNamespace.Reservation.End).ToLowerInvariant(), nameof(sourceNamespace.Reservation.End).ToLowerInvariant());
        InsertPropetiesMapping(nameof(targetNamespace.Reservation.RentalCar).ToLowerInvariant(), nameof(sourceNamespace.Reservation.RentalCar).ToLowerInvariant());
    }
}


