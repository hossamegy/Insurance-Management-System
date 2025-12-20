

namespace InsuranceAdministration.Core.Interfaces.Services
{
    public interface IMissionServices
    {
        ValueTask<Mission> AddNewMission(Mission mission);
        ValueTask<IEnumerable<Mission>> GetAllMissions();
        ValueTask<Mission> GetMission(int id);
        ValueTask<Mission> UpdateCurrentMission(Mission mission);
        ValueTask<Mission> DeleteMission(int id);
    }
}
