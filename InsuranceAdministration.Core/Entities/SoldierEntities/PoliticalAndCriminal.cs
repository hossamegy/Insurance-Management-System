
namespace InsuranceAdministration.Core.Entities.SoldierEntities
{
    public class PoliticalAndCriminal
    {
        public int Id { get; set; }
        public string? PoliticalStatus { get; set; }
        public string? CriminalRecord { get; set; }
        public int SoldierId { get; set; }
        public Soldier? Soldier { get; set; }
    }
}
