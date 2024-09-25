using DIRS21.DataModel;

namespace DIRS21.Unittests.Dirs21Models
{
    public class PaymentTests
    {

        private static IEnumerable<TestCaseData> PaymentUseCaseDatas => [
                    new TestCaseData(new Payment()),
                    new TestCaseData(new Payment() {Id = "", ServiceName = "paypal", Token = "xxx-ggf-kol98"}),
                    new TestCaseData(new Payment() {Id = " ", ServiceName = "paypal", Token = "xxx-ggf-kol98"}),
                    new TestCaseData(new Payment() {Id = "47", ServiceName = "", Token = "xxx-ggf-kol98"}),
                    new TestCaseData(new Payment() {Id = "47", ServiceName = " ", Token = "xxx-ggf-kol98"}),
                    new TestCaseData(new Payment() {Id = "47", ServiceName = "paypal", Token = ""}),
                    new TestCaseData(new Payment() {Id = "47", ServiceName = "paypal", Token = " "}),
                ];

        [Test]
        public void CreatePayment_ValidatePayment_PaymentIsValid()
        {
            Room room = new() { Id = Guid.NewGuid().ToString(), Category = "family", NumberOfBad = 1, NumberOfBed = 3 };

            Assert.That(room.IsValid, Is.True);
            Assert.That(room.Errors, Is.Empty);
        }

        [Test, TestCaseSource(nameof(PaymentUseCaseDatas))]
        public void CreatePayment_ValidatePayment_PaymentIsNotValid(Payment payment)
        {
            Assert.That(payment.IsValid, Is.False);
            Assert.That(payment.Errors, Is.Not.Empty);
        }
    }
}