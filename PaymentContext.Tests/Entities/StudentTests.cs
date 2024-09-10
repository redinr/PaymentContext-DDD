using PaymentContext.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void AddNewSubscription()
        {
            var subscription = new Subscription(null);
            var student = new Student("Ricardo","Redin","123456789","ricardoredin@gmail.com");
            student.AddSubscription(subscription);
        }
    }
}
