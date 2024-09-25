using DIRS21.DataModel;

namespace DIRS21.Unittests.Dirs21Models
{
    public class ReservationTests
    {

        private static IEnumerable<TestCaseData> ReservationUseCaseDatas => [
                    new TestCaseData(new Reservation()),
                    new TestCaseData(new Reservation { ReservationId = Guid.Empty, CustomerId = Guid.NewGuid().ToString(), Start = DateTime.Now, End = DateTime.Now.AddDays(1)}),
                    new TestCaseData(new Reservation { ReservationId = Guid.NewGuid(), CustomerId = "", Start = DateTime.Now, End = DateTime.Now.AddDays(3)}),
                    new TestCaseData(new Reservation { ReservationId = Guid.NewGuid(), CustomerId = " ", Start = DateTime.Now, End = DateTime.Now.AddDays(3)}),
                    new TestCaseData(new Reservation { ReservationId = Guid.NewGuid(), CustomerId = Guid.NewGuid().ToString(), Start = DateTime.Now, End = DateTime.Now.AddDays(-3)}),
                    new TestCaseData(new Reservation { ReservationId = Guid.NewGuid(), CustomerId = Guid.NewGuid().ToString(), Start = DateTime.Now.AddDays(2), End = DateTime.Now}),
                    new TestCaseData(new Reservation { ReservationId = Guid.NewGuid(), CustomerId = Guid.NewGuid().ToString(), Start = DateTime.Now.AddDays(-2), End = DateTime.Now, RentalCar = new RentalCar()})
                ];

        [Test]
        public void CreateReservation_ValidateReservation_ReservationIsValid()
        {
            Reservation reservation = new()
            {
                ReservationId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid().ToString(),
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(3),
                RentalCar = new RentalCar() { CarId = "1458", Description = "Combi class", Name ="byd" }
            };
            Assert.Multiple(() =>
            {
                Assert.That(reservation.IsValid, Is.True);
                Assert.That(reservation.Errors, Is.Empty);
            });
        }

        [Test, TestCaseSource(nameof(ReservationUseCaseDatas))]
        public void CreateReservation_ValidateReservation_ReservationIsInValid(Reservation reservation)
        {
            Assert.Multiple(() =>
            {
                Assert.That(reservation.IsValid, Is.False);
                Assert.That(reservation.Errors, Is.Not.Empty);
            });
        }
    }
}