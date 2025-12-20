namespace InsuranceAdministration.Core.Exceptions
{
    public class BaseExceptions : Exception
    {
        public BaseExceptions(string message) : base(message)
        {
            
        }
        public BaseExceptions(string message, Exception innerException) : base (message, innerException)
        {

        }
    }
}
