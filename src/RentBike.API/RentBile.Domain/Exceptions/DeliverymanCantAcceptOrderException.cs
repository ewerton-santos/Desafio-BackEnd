namespace RentBike.Domain.Exceptions
{
    public class DeliverymanCantAcceptOrderException : Exception
    {
        private static readonly string _message = "Delivery man cannot accept the order";
        public DeliverymanCantAcceptOrderException() : base(_message) { }
        public DeliverymanCantAcceptOrderException(string message) : base(message) { }
        public DeliverymanCantAcceptOrderException(string message, Exception exception) : base(message, exception) { }
    }
}
