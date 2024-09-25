using ExternalClient.DataModel.Google;

namespace DIRS21.Unittests.Dirs21Models
{
    public class GoogleReservationTests
    {

        private static IEnumerable<TestCaseData> ReservationUseCaseDatas => [
                    new TestCaseData(new Reservation()),
                    new TestCaseData(new Reservation { Id = "", CustomerId = Guid.NewGuid().ToString(), Start = DateTime.Now, End = DateTime.Now.AddDays(1)}),
                    new TestCaseData(new Reservation { Id = " ", CustomerId = Guid.NewGuid().ToString(), Start = DateTime.Now, End = DateTime.Now.AddDays(1)}),
                    new TestCaseData(new Reservation { Id = Guid.NewGuid().ToString(), CustomerId = "", Start = DateTime.Now, End = DateTime.Now.AddDays(3)}),
                    new TestCaseData(new Reservation { Id = Guid.NewGuid().ToString(), CustomerId = " ", Start = DateTime.Now, End = DateTime.Now.AddDays(3)}),
                    new TestCaseData(new Reservation { Id = Guid.NewGuid().ToString(), CustomerId = Guid.NewGuid().ToString(), Start = DateTime.Now, End = DateTime.Now.AddDays(-3)}),
                    new TestCaseData(new Reservation { Id = Guid.NewGuid().ToString(), CustomerId = Guid.NewGuid().ToString(), Start = DateTime.Now.AddDays(2), End = DateTime.Now}),
                    new TestCaseData(new Reservation { Id = Guid.NewGuid().ToString(), CustomerId = Guid.NewGuid().ToString(), Start = DateTime.Now.AddDays(-2), End = DateTime.Now, RentalCar = new RentalCar()}),
                ];

        [Test]
        public void CreateReservation_ValidateReservation_ReservationIsValid()
        {
            Reservation reservation = new Reservation { Id = Guid.NewGuid().ToString(), CustomerId = Guid.NewGuid().ToString(), Start = DateTime.Now, End = DateTime.Now.AddDays(3),
                RentalCar = new RentalCar() { Id = "1458", Description = "Combi class", Name = "byd" }
            };

            Assert.That(reservation.IsValid, Is.True);
            Assert.That(reservation.Errors, Is.Empty);
        }

        [Test, TestCaseSource(nameof(ReservationUseCaseDatas))]
        public void CreateReservation_ValidateReservation_ReservationIsInValid(Reservation reservation)
        {
            Assert.That(reservation.IsValid, Is.False);
            Assert.That(reservation.Errors, Is.Not.Empty);
        }
    }
}