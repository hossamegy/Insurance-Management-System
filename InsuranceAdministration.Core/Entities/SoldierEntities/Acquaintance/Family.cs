namespace InsuranceAdministration.Core.Entities.SoldierEntities.Acquaintance
{
    public class Family
    {
        // Family member info
        public int Id { get; set; }

        public string? Relationship { get; set; }

        public string? FullName { get; set; }

        public string? NationalId { get; set; }

        public string? Job { get; set; }

        public string? Address { get; set; }

        // Spouse info (زوج / زوجة)
        public string? SpouseFullName { get; set; }

        public string? SpouseNationalId { get; set; }

        public string? SpouseJob { get; set; }

        public string? SpouseAddress { get; set; }

        // Foreign key to AcquaintanceDocument
        public int AcquaintanceDocumentId { get; set; }
        public AcquaintanceDocument AcquaintanceDocument { get; set; }
    }
}
