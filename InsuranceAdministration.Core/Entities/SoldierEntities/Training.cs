namespace InsuranceAdministration.Core.Entities.SoldierEntities
{
    public class Training
    {
        public int Id { get; set; }
        public string? TraingName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string? Institution { get; set; }
        public int SoldierId { get; set; }
        public Soldier? Soldier { get; set; }
    }
}
