namespace RentBike.Domain.Exceptions
{
    public class DriverNotQualifiedForCategoryException : Exception
    {
        private static readonly string _message = "Driver's license not found";
        public DriverNotQualifiedForCategoryException() : base(_message) { }
        public DriverNotQualifiedForCategoryException(string message) : base(message) { }
        public DriverNotQualifiedForCategoryException(string message, Exception exception) : base(message, exception) { }
    }
}
