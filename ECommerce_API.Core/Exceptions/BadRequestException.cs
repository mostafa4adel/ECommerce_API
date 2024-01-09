namespace ECommerce_API.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string name, object key) : base($"{name} Bad Request")
        {

        }
    }
}
