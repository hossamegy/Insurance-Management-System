using InsuranceAdministration.Core.Entities.PoliceManEntities;

namespace InsuranceAdministration.Core.Interfaces.Repository
{
    public interface IPoliceManRepository
    {
        ValueTask<PoliceMan> AddNewPoliceMan(PoliceMan policeMan);
        ValueTask<IEnumerable<PoliceMan>> GetAllPoliceMan();
        ValueTask<PoliceMan> GetPoliceMan(int id);
        ValueTask<PoliceMan> UpdateCurrentPoliceMan(PoliceMan policeMan);
        ValueTask<PoliceMan> DeletePoliceMan(int id);
        ValueTask<IEnumerable<PoliceMan>> GetPaginatedPoliceMan(int pageNumber, int pageSize);

    }

}
