using ExternalClient.DataModel.Google;

namespace DIRS21.Unittests.Dirs21Models
{
    public class GoogleRentalCarTests
    {

        private static IEnumerable<TestCaseData> RentalCarUseCaseDatas => [
                    new TestCaseData(new RentalCar()),
                    new TestCaseData(new RentalCar() { Id = " ", Description = "family", Name = "byd" }),
                    new TestCaseData(new RentalCar() { Id = "", Description = "family", Name = "byd" }),
                    new TestCaseData(new RentalCar() { Id = Guid.NewGuid().ToString(), Description = "", Name = "byd" }),
                    new TestCaseData(new RentalCar() { Id = Guid.NewGuid().ToString(), Description = " ", Name = "byd" }),
                    new TestCaseData(new RentalCar() { Id = Guid.NewGuid().ToString(), Description = "family", Name = "" }),
                    new TestCaseData(new RentalCar() { Id = Guid.NewGuid().ToString(), Description = "family", Name = " " }),
                ];

        [Test]
        public void CreateRentalCar_ValidateRentalCar_RentalCarIsValid()
        {
            RentalCar RentalCar = new RentalCar() { Id = Guid.NewGuid().ToString(), Description = "family", Name = "byd" };

            Assert.That(RentalCar.IsValid, Is.True);
            Assert.That(RentalCar.Errors, Is.Empty);
        }

        [Test, TestCaseSource(nameof(RentalCarUseCaseDatas))]
        public void CreateRentalCar_ValidateRentalCar_RentalCarIsInValid(RentalCar RentalCar)
        {
            Assert.That(RentalCar.IsValid, Is.False);
            Assert.That(RentalCar.Errors, Is.Not.Empty);
        }
    }
}