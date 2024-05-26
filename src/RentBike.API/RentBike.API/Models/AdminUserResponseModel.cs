namespace RentBike.API.Models
{
    public class AdminUserResponseModel
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public DateTime Created { get; private set; } = DateTime.Now;
        public DateTime? LastUpdated { get; private set; }
        
    }
}
