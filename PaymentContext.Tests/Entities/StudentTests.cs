using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void AddNewSubscription()
        {
            var name = new Name("Teste", "Teste");
            foreach(var notif in name.Notifications)
            {
                //notif.Message;
            }
        }
    }
}
