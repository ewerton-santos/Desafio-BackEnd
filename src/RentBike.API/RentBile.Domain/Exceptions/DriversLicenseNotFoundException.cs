namespace RentBike.Domain.Exceptions
{
    public class DriversLicenseNotFoundException : Exception
    {
        private static readonly string _message = "Driver's license not found";
        public DriversLicenseNotFoundException() : base(_message) { }
        public DriversLicenseNotFoundException(string message) : base(message) { }
        public DriversLicenseNotFoundException(string message, Exception exception) : base(message, exception) { }
    }
}
