namespace RentBike.Domain.Exceptions
{
    public class RentNotFoundExeception : Exception
    {
        private static readonly string _message = "Rent not found";
        public RentNotFoundExeception() : base(_message) { }
        public RentNotFoundExeception(string message) : base(message) { }
        public RentNotFoundExeception(string message, Exception exception) : base(message, exception) { }
    }
}
