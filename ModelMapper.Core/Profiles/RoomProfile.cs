using sourceNamespace = DIRS21.DataModel;
using targetNamespace = ExternalClient.DataModel.Google;

namespace ModelMapper.Core.Profiles;

/// <summary>
/// The profile for the room models
/// </summary>
/// <seealso cref="ModelMapper.Core.Profiles.BaseProfile&lt;DIRS21.DataModel.Room, ExternalClient.DataModel.Google.Room&gt;" />
public class RoomProfile : BaseProfile<sourceNamespace.Room, targetNamespace.Room>
{
    public RoomProfile() : base()
    {
    }
    protected override void BuildProfile()
    {
        InsertPropetiesMapping(nameof(targetNamespace.Room.Id).ToLowerInvariant(), nameof(sourceNamespace.Room.Id).ToLowerInvariant());
        InsertPropetiesMapping(nameof(targetNamespace.Room.Category).ToLowerInvariant(), nameof(sourceNamespace.Room.Category).ToLowerInvariant());
        InsertPropetiesMapping(nameof(targetNamespace.Room.Details).ToLowerInvariant(), nameof(sourceNamespace.Room.Details).ToLowerInvariant());
        InsertPropetiesMapping(nameof(targetNamespace.Room.NumberOfBad).ToLowerInvariant(), nameof(sourceNamespace.Room.NumberOfBad).ToLowerInvariant());
        InsertPropetiesMapping(nameof(targetNamespace.Room.NumberOfBed).ToLowerInvariant(), nameof(sourceNamespace.Room.NumberOfBed).ToLowerInvariant());
    }
}


