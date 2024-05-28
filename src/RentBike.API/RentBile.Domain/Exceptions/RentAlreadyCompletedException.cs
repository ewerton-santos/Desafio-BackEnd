namespace RentBike.Domain.Exceptions
{
    public class RentAlreadyCompletedException : Exception
    {
        private static readonly string _message = "This rent has already been completed";
        public RentAlreadyCompletedException() : base(_message) { }
        public RentAlreadyCompletedException(string message) : base(message) { }
        public RentAlreadyCompletedException(string message, Exception exception) : base(message, exception) { }
    }
}
