namespace InsuranceAdministration.Core.Exceptions
{
    public class EntityNotFoundException : BaseExceptions
    {
        public string EntityName { get; set; }
        public object EntityId { get; set; }

        public EntityNotFoundException(string entityName, object entityId) : base($"{entityName} With ID '{entityId}' was not found") 
        {
                EntityName = entityName;
                EntityId = entityId;
        }

        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
}
