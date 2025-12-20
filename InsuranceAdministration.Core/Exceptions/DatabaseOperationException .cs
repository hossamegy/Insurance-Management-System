namespace InsuranceAdministration.Core.Exceptions
{
   
    public class DatabaseOperationException : BaseExceptions
    {
        public DatabaseOperationException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
