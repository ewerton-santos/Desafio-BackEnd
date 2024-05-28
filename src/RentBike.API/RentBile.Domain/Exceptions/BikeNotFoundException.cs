namespace RentBike.Domain.Exceptions
{
    public class BikeNotFoundException : Exception
    {
        private static readonly string _message = "Bike not found";
        public BikeNotFoundException() : base(_message) { }
        public BikeNotFoundException(string message) : base(message) { }
        public BikeNotFoundException(string message, Exception exception) : base(message, exception) { }
    }
}
