namespace RentBike.Domain.Exceptions
{
    public class OrderNotAvailableException : Exception
    {
        private static readonly string _message = "Order not available";
        public OrderNotAvailableException() : base(_message) { }
        public OrderNotAvailableException(string message) : base(message) { }
        public OrderNotAvailableException(string message, Exception exception) : base(message, exception) { }
    }
}
