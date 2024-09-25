using DIRS21.DataModel.Validators;

namespace DIRS21.DataModel;

/// <summary>
/// A room class
/// </summary>
public class Room : MirrorModel
{
    public Room() : base(new RoomValidator())
    {

    }
    public int NumberOfBed { get; set; }
    public int NumberOfBad { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
}


