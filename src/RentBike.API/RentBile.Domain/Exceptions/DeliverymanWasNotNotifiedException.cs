namespace RentBike.Domain.Exceptions
{
    public class DeliverymanWasNotNotifiedException : Exception
    {
        private static readonly string _message = "Delivery man was not notified";
        public DeliverymanWasNotNotifiedException() : base(_message) { }
        public DeliverymanWasNotNotifiedException(string message) : base(message) { }
        public DeliverymanWasNotNotifiedException(string message, Exception exception) : base(message, exception) { }
    }
}
