using ExternalClient.DataModel.Google.Validators;
using ExternalClient.DataModel.Interfaces;

namespace ExternalClient.DataModel.Google;

/// <summary>
/// A room class
/// </summary>
public class Room : GoogleModel, IExternalModel
{
    public Room() : base(new RoomValidator())
    {

    }

    public int NumberOfBed { get; set; }
    public int NumberOfBad { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
}
