namespace RentBike.Domain.Exceptions
{
    public class DeliverymanUserNotFoundException : Exception
    {
        private static readonly string _message = "Deliveryman's User not found";
        public DeliverymanUserNotFoundException() : base(_message) { }
        public DeliverymanUserNotFoundException(string message) : base(message) { }
        public DeliverymanUserNotFoundException(string message, Exception exception) : base(message, exception) { }
    }
}
