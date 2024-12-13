using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Student _student;
        private readonly Email _email;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Subscription _subscription;

        public StudentTests()
        {
            _name = new Name("John", "Karl");
            _document = new Document("90631349081", EDocumentType.CPF);
            _email = new Email("john@gmail.com");
            _address = new Address("Rua 1", "1324", "ABC", "SCS", "RSa", "BRL", "13245-678");
            _student = new Student(_name, _email, _document);
            _subscription = new Subscription(null);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            var pay = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, _address, _document, "Sicredi", _email);
            _subscription.AddPayment(pay);

            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Invalid);
        }

        public void ShouldReturnErrorWhenActiveSubscriptionHasNoPayment()
        {
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenAddSubscription()
        {
            var pay = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, _address, _document, "Sicredi", _email);
            _subscription.AddPayment(pay);

            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Valid);
        }

    }
}
