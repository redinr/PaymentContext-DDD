using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _subscription;
        public Student(Name name, Email email, Document document)
        {
            Name = name;
            Email = email;
            Document = document;
            _subscription = new List<Subscription>();
        }
        //Private in SET is to not change infos in outher class.
        //Is necessary create functions for change informations;
        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public Document Document { get; private set; }
        public Address Address { get; private set; }
        //Obriga a chamar um método para add uma nova subscription.
        public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscription.ToArray(); } }

        public void AddSubscription(Subscription subscription)
        {
            foreach (var sub in Subscriptions) {
                sub.Inactivate();
            }
            _subscription.Add(subscription);

        }

    }
}
