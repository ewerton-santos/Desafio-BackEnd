namespace RentBike.Domain.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        private static readonly string _message = "Order not found";
        public OrderNotFoundException() : base(_message) { }
        public OrderNotFoundException(string message) : base(message) { }
        public OrderNotFoundException(string message, Exception exception) : base(message, exception) { }
    }
}
