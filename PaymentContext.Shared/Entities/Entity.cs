namespace PaymentContext.Shared.Entities
{
    public abstract class Entity
    {
        //Preferível usar Guid pq é menos request no BD
        //Não depende do BD para gerar ID
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}
