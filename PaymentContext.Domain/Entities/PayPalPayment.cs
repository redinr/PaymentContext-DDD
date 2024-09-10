namespace PaymentContext.Domain.Entities
{
    public class PayPalPayment : Payment
    {
        public PayPalPayment(
            string transactionCode, 
            DateTime paidDate,
            DateTime expireDate,
            decimal total,
            decimal totalPaid,
            string adress,
            string document,
            string payer,
            string email) : base(
                paidDate,
                expireDate,
                total,
                totalPaid,
                adress,
                document,
                payer,
                email)
        {
            TransactionCode = transactionCode;
        }

        public string TransactionCode { get; private set; }
    }
}
