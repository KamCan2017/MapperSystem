using ExternalClient.DataModel.Google.Validators;
using ExternalClient.DataModel.Interfaces;

namespace ExternalClient.DataModel.Google
{
    /// <summary>
    /// The reservation class
    public class Reservation : GoogleModel, IExternalModel
    {
        public Reservation() : base(new ReservationValidator())
        {

        }

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

}
