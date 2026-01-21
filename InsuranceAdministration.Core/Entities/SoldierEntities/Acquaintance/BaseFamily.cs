
namespace InsuranceAdministration.Core.Entities.SoldierEntities.Acquaintance
{
    public class BaseFamily
    {
        public int Id { set; get; }
        public string? Relationship { set; get; }
        public string? Name { set; get; }
        public string? NationalId { set; get; }
        public string? Job { set; get; }
        public string? Address { set; get; }
        public DateTime? BirthDate { set; get; }
        public string? PhoneNumber { set; get; }

        // Foreign key to AcquaintanceDocument
        public int AcquaintanceDocumentId { get; set; }
        public AcquaintanceDocument AcquaintanceDocument { get; set; }
    }
}
