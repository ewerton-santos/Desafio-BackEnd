namespace RentBike.Domain.Exceptions
{
    public class RentPlanNotFoundExeception : Exception
    {
        private static readonly string _message = "Rent Plan not found";
        public RentPlanNotFoundExeception() : base(_message) { }
        public RentPlanNotFoundExeception(string message) : base(message) { }
        public RentPlanNotFoundExeception(string message, Exception exception) : base(message, exception) { }
    }
}
