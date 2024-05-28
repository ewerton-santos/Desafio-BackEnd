namespace RentBike.Domain.Exceptions
{
    public class AdminUserNotFoundException : Exception
    {
        private static readonly string _message = "User not found or user isn't Admin";
        public AdminUserNotFoundException() : base(_message) { }
        public AdminUserNotFoundException(string message) : base(message) { }
        public AdminUserNotFoundException(string message, Exception exception) : base(message, exception) { }
    }
}
