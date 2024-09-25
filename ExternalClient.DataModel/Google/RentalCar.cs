using ExternalClient.DataModel.Google.Validators;
using ExternalClient.DataModel.Interfaces;

namespace ExternalClient.DataModel.Google;

/// <summary>The rental car</summary>
/// <seealso cref="ExternalClient.DataModel.Google.GoogleModel" />
/// <seealso cref="ExternalClient.DataModel.Interfaces.IExternalModel" />
public class RentalCar : GoogleModel, IExternalModel
{
    public RentalCar() : base(new RentalCarValidator())
    {

    }

    public string Name { get; set; } = string.Empty;    
    public string Description { get; set; } = string.Empty;

}
