namespace RentBike.Domain.Exceptions
{
    public class RemoveBikeException : Exception
    {
        private static readonly string _message = "Can't remove this bike";
        public RemoveBikeException() : base(_message) { }
        public RemoveBikeException(string message) : base(message) { }
        public RemoveBikeException(string message, Exception exception) : base(message, exception) { }
    }
}
