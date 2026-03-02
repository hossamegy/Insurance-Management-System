
using InsuranceAdministration.Core.Entities.MissionEntities;
using InsuranceAdministration.Core.Entities.SoldierEntities;
using InsuranceAdministration.Core.Interfaces.Repository;
using InsuranceAdministration.Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAdministration.Infrastructure.Persistance
{
    public class MissionRepository : IMissionRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Mission> _entities;

        public MissionRepository(AppDbContext context)
        {
            _context = context;
            _entities = _context.Set<Mission>();
        }

        public async ValueTask<Mission> AddNewMission(Mission mission)
        {
            await _entities.AddAsync(mission);
            await _context.SaveChangesAsync();
            return mission;
        }

        public async ValueTask<IEnumerable<Mission>> GetAllMissions()
        {
            return await _entities
                .Include(m => m.SoldierMissions)
                .ToListAsync();
        }

        public async ValueTask<Mission?> GetMission(int id)
        {
            return await _entities
                .Include(m => m.SoldierMissions)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async ValueTask<Mission?> DeleteMission(int id)
        {
            var mission = await _entities.FindAsync(id);
            if (mission == null)
                return null;

            _entities.Remove(mission);
            await _context.SaveChangesAsync();
            return mission;
        }

        public async ValueTask<Mission> UpdateCurrentMission(Mission mission)
        {
            _entities.Update(mission);
            await _context.SaveChangesAsync();
            return mission;
        }

        public async ValueTask<IEnumerable<Mission>> GetAllMissionsByActive(bool IsActive)
        {
           return await _entities.Where(m => m.IsActive == IsActive).ToListAsync();    
        }

        public async ValueTask<IEnumerable<Mission>> GetAllMissionsByActiveAndType(bool IsActive, int MissionType)
        {
            return await _entities.Where(m => m.IsActive == IsActive && m.MissionType == MissionType).ToListAsync();
        }

        public async ValueTask<DailyMission> AddNewDailyMission(DateTime date, IEnumerable<int> soldierMissionIds)
        {
            // Check if DailyMission exists for this date
            var existingDailyMission = await _context.Set<DailyMission>()
                .FirstOrDefaultAsync(dm => dm.Date.Date == date.Date);

            DailyMission dailyMission;

            if (existingDailyMission != null)
            {
                dailyMission = existingDailyMission;
            }
            else
            {
                // Create new DailyMission
                dailyMission = new DailyMission
                {
                    Date = date.Date
                };

                await _context.Set<DailyMission>().AddAsync(dailyMission);
                await _context.SaveChangesAsync();
            }

            // Link SoldierMissions to this DailyMission
            var soldierMissions = await _context.Set<SoldierMission>()
                .Where(sm => soldierMissionIds.Contains(sm.Id))
                .ToListAsync();

            foreach (var sm in soldierMissions)
            {
                sm.DailyMissionId = dailyMission.Id;
            }

            await _context.SaveChangesAsync();


            return dailyMission;
        }

        public async ValueTask<DailyMission?> GetDailyMission(int id)
        {
            return await _context.Set<DailyMission>()
                .Include(dm => dm.SoldierMissions)
                    .ThenInclude(sm => sm.Mission)
                .Include(dm => dm.SoldierMissions)
                    .ThenInclude(sm => sm.Soldier)
                .FirstOrDefaultAsync(dm => dm.Id == id);
        }

        public async ValueTask<IEnumerable<DailyMission>> GetAllDailyMissions()
        {
            return await _context.Set<DailyMission>()
                .Include(dm => dm.SoldierMissions)
                    .ThenInclude(sm => sm.Mission)
                .OrderByDescending(dm => dm.Date)
                .ToListAsync();
        }

        public ValueTask<IEnumerable<Mission>> GetAllMissionsByType()
        {
            throw new NotImplementedException();
        }

        public ValueTask<DailyMission?> DeletDailyMission(int id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<DailyMission> UpdateCurrentDailyMission(DailyMission mission)
        {
            throw new NotImplementedException();
        }


    }
}
