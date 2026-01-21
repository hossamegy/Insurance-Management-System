using InsuranceAdministration.Core.Entities.SoldierEntities.Acquaintance;

namespace InsuranceAdministration.Core.Entities.SoldierEntities
{
    public class AcquaintanceDocument
    {

        public int Id { set; get; }
        public ICollection<BaseFamily> BaseFamily { set; get; } = new List<BaseFamily>();

        public ICollection<Family> Family { set; get; } = new List<Family>();


        public int SoldierId { get; set; }
        public Soldier Soldier { get; set; }
    }
}
