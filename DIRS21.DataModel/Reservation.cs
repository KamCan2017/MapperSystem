using DIRS21.DataModel.Validators;

namespace DIRS21.DataModel;

/// <summary>
/// The reservation class
/// </summary>
public class Reservation : BaseMirrorModel
{
    public Reservation() : base(new ReservationValidator())
    {

    }

    /// <summary>
    /// The reservation identifier
    /// </summary>
    public Guid ReservationId { get; set; }

    /// <summary>
    /// The customer identifier
    /// </summary>
    public string CustomerId { get; set; } = string.Empty;

    /// <summary>
    /// End of the reservation
    /// </summary>
    public DateTime End { get; set; }

    /// <summary>
    /// Start of the reservation
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// Gets or sets the rental car.
    /// </summary>
    /// <value>
    /// The rental car.
    /// </value>
    public RentalCar? RentalCar { get; set; }


}
