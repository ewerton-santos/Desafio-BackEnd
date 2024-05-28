namespace RentBike.Domain.Exceptions
{
    public class BikeNotAvailableException : Exception
    {
        private static readonly string _message = "There are no motorbikes available";
        public BikeNotAvailableException() : base(_message) { }
        public BikeNotAvailableException(string message) : base(message) { }
        public BikeNotAvailableException(string message, Exception exception) : base(message, exception) { }
    }
}
