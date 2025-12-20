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
        ValueTask<IEnumerable<Soldier>> GetPaginatedSoldiers(int pageNumber, int pageSize);

    }
}
