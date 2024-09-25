using sourceNamespace = DIRS21.DataModel;
using targetNamespace = ExternalClient.DataModel.Google;

namespace ModelMapper.Core.Profiles;

/// <summary>
/// The profile for the rentalcar models
/// </summary>
/// <seealso cref="ModelMapper.Core.Profiles.BaseProfile&lt;DIRS21.DataModel.RentalCar, ExternalClient.DataModel.Google.RentalCar&gt;" />
public class RentalCarProfile : BaseProfile<sourceNamespace.RentalCar, targetNamespace.RentalCar>
{
    public RentalCarProfile() : base()
    {
    }
    protected override void BuildProfile()
    {
        InsertPropetiesMapping(nameof(targetNamespace.RentalCar.Id).ToLowerInvariant(), nameof(sourceNamespace.RentalCar.CarId).ToLowerInvariant());
        InsertPropetiesMapping(nameof(targetNamespace.RentalCar.Description).ToLowerInvariant(), nameof(sourceNamespace.RentalCar.Description).ToLowerInvariant());
        InsertPropetiesMapping(nameof(targetNamespace.RentalCar.Name).ToLowerInvariant(), nameof(sourceNamespace.RentalCar.Name).ToLowerInvariant());
    }
}


