using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(FirstName, 3, "Name.FirstName", "Name deve conter no mínimo 3 caracteres.")
                .HasMinLen(LastName, 3, "Name.LastName", "Sobrenome deve conter no mínimo 3 caracteres.")
            );
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}
