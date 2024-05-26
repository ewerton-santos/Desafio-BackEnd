namespace RentBikeUsers.Domain.Entities
{
    public sealed class DeliverymanUser : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Cnpj { get; set; } = string.Empty;        
        public DriversLicence? DriversLicence { get; set; }
    }
}
