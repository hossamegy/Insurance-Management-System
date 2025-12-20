namespace InsuranceAdministration.Core.Exceptions
{
    public class ValidationException : BaseExceptions
    {
        public IDictionary<string, string[]> Errors { get; set; }
        public ValidationException(IDictionary<string, string[]> errors) : base("One or more validation errors occurred")
        {
            Errors = errors;
        }

        public ValidationException(string message) : base(message)
        {
            Errors = new Dictionary<string, string[]>();
        }

    }
}
