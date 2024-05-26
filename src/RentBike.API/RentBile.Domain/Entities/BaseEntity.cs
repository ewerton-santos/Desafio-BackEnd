namespace RentBikeUsers.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime Created {  get; private set; } = DateTime.UtcNow;
        public DateTime? LastUpdated { get; set; }
    }
}
