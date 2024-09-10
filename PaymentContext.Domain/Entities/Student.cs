using System.Reflection.Metadata.Ecma335;

namespace PaymentContext.Domain.Entities
{
    public class Student
    {
        private IList<Subscription> _subscription;
        public Student(string firstName, string lastName, string email, string document)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Document = document;
            _subscription = new List<Subscription>();
        }
        //Private in SET is to not change infos in outher class.
        //Is necessary create functions for change informations;
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Document { get; private set; }
        public string Adress { get; private set; }
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
