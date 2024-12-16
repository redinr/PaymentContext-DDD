using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "Jonh";
            command.LastName = "Assad";
            command.Document = "1234567891011";
            command.Email = "teste@gmail.com";

            command.BarCode = "123456789";
            command.BoletoNumber = "12345678";

            command.PaymentNumber = "123456";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 123;
            command.TotalPaid = 123;
            command.PayerDocument = "123456798";
            command.PayerDocumentType = EDocumentType.CPF;
            command.Payer = "Joao";
            command.PayerEmail = "abc@gmail.com";

            command.Street = "Rua AB";
            command.Number = "123";
            command.Neighborhood = "Bairro";
            command.City = "Santa";
            command.State = "ABC";
            command.Country = "Brazil";
            command.ZipCode = "1254-847";

            handler.Handle(command);
            Assert.AreEqual(false, handler.Valid);
        }
    }
}
