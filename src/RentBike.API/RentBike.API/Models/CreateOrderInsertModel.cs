using System.ComponentModel.DataAnnotations;

namespace RentBike.API.Models
{
    public class CreateOrderInsertModel
    {
        [Required(ErrorMessage ="Delivery Fee is required")]
        public double DeliveryFee { get; set; }
    }
}
