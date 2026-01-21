using InsuranceAdministration.Core.Entities.SoldierEntities;

namespace InsuranceAdministration.Core.Interfaces.Services
{
    public interface ISoldierServices
    {
        ValueTask<Soldier> AddNewSoldier(Soldier soldier);
        ValueTask<IEnumerable<Soldier>> GetAllSoldier();
        ValueTask<Soldier> GetSoldier(int id);
        ValueTask<IEnumerable<Soldier>> GetSoldierByAssignment(String SoldierAssignment);

        ValueTask<Soldier> UpdateCurrentSoldier(Soldier soldier);
        ValueTask<Soldier> DeleteSoldier(int id);
        ValueTask<IEnumerable<Soldier>> GetPaginatedSoldiersByActive(int pageNumber, int pageSize, bool IsActive);
        ValueTask<AcquaintanceDocument> AddAcquaintanceDocument(int SoldierId, AcquaintanceDocument acquaintanceDocument);
        ValueTask<AcquaintanceDocument> GetAcquaintanceDocument(int SoldierId);
        ValueTask<AcquaintanceDocument> UpdateAcquaintanceDocument(AcquaintanceDocument acquaintanceDocument);

        ValueTask<SoldierLeave> AddSoldierLeave(SoldierLeave SoldierLeave);
        ValueTask<SoldierLeave> GetSoldierLeave(int SoldierId);
        ValueTask<SoldierLeave> UpdateSoldierLeave(SoldierLeave SoldierLeave);

    }
}
