using InsuranceAdministration.Core.DTOs.Soldiers;
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
        ValueTask<int> GetSoldiersCountsIsActive();
        ValueTask<int> GetSoldierAttendanceCounts();
        ValueTask<int> GetSoldiersLeaveCounts();
        ValueTask<int> GetSoldiersLeaveCountsByType(string type);
        ValueTask<IEnumerable<SoldierMission>> GetSoldierMissions(int id);

        ValueTask<IEnumerable<Soldier>> GetAllSoldierAttendenceRiver();
        ValueTask<IEnumerable<Soldier>> GetAllSoldierAttendenceNotRiver(); 

        ValueTask<AcquaintanceDocument> AddAcquaintanceDocument(int SoldierId, AcquaintanceDocument acquaintanceDocument);
        ValueTask<AcquaintanceDocument> GetAcquaintanceDocument(int SoldierId);
        ValueTask<AcquaintanceDocument>UpdateAcquaintanceDocument(AcquaintanceDocument acquaintanceDocument);

        ValueTask<SoldierLeave> AddSoldierLeave(SoldierLeave SoldierLeave);
        ValueTask<IEnumerable<SoldierLeave>> GetSoldierLeave(int SoldierId);
  
        ValueTask<SoldierLeave> GetLastSoldierLeave(int soldierId);
        ValueTask<SoldierLeave> GetSoldierLeaveById(int soldierLeaveId);
        ValueTask<SoldierLeave> UpdateSoldierLeave(SoldierLeave SoldierLeave);
        ValueTask<SoldierLeave> DeleteSoldierLeave(SoldierLeave SoldierLeave);


        ValueTask<IEnumerable<SoldierMission>> AddDailyMissionsToSoldiers(List<SoldierMission> soldierMissions);

    }
}
