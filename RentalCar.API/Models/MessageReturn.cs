namespace RentalCar.API.Models
{
    public enum enumMessage
    {
        Success = 1,
        Fail = 0
    }

    public class MessageReturn
    {
        public enumMessage StatusCode { get; set; }
        
        public string Message { get; set; }
    }
}