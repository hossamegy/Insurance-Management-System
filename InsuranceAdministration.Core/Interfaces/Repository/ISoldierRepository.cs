
using InsuranceAdministration.Core.Entities.SoldierEntities;


namespace InsuranceAdministration.Core.Interfaces.Repository
{
    public interface ISoldierRepository
    {
      
        ValueTask<Soldier> AddNewSoldier(Soldier soldier);
        ValueTask<IEnumerable<Soldier>> GetAllSoldier();
        ValueTask<Soldier> GetSoldier(int id);
        ValueTask<IEnumerable<Soldier>> GetSoldierByAssignment(String SoldierAssignment);
        ValueTask<Soldier> UpdateCurrentSoldier(Soldier soldier);
        ValueTask<Soldier> DeleteSoldier(int id);
        ValueTask<IEnumerable<Soldier>> GetPaginatedSoldiersByActive(int pageNumber, int pageSize, bool IsActive);

    }
}
