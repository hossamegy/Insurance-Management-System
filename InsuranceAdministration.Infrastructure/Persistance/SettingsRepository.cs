using InsuranceAdministration.Core.Entities.Settings;
using InsuranceAdministration.Core.Interfaces.Repository;
using InsuranceAdministration.Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAdministration.Infrastructure.Persistance
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly AppDbContext _context;

        private readonly DbSet<AssignmentOptions> _assignment;
        private readonly DbSet<EducationLevelOptions> _education;
        private readonly DbSet<MainSettings> _main;
        private readonly DbSet<SoldierLeaveOptions> _soldierLeave;
        private readonly DbSet<DailyMealOptions> _dailyMeal;

        public SettingsRepository(AppDbContext context)
        {
            _context = context;
            _assignment = _context.Set<AssignmentOptions>();
            _education = _context.Set<EducationLevelOptions>();
            _main = _context.Set<MainSettings>();
            _soldierLeave = _context.Set<SoldierLeaveOptions>();
            _dailyMeal = _context.Set<DailyMealOptions>();
        }

        /* ================= Assignment ================= */

        public async ValueTask<AssignmentOptions> AddNewAssignmentOptions(AssignmentOptions entity)
        {
            await _assignment.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async ValueTask<IEnumerable<AssignmentOptions>> GetAllAssignmentOptions()
        {
            return await _assignment.ToListAsync();
        }

        public async ValueTask<AssignmentOptions> UpdateCurrentAssignmentOptions(AssignmentOptions entity)
        {
            _assignment.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async ValueTask<AssignmentOptions> DeleteAssignmentOptions(int id)
        {
            var entity = await _assignment.FindAsync(id);
            if (entity == null) return null;

            _assignment.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /* ================= Education ================= */

        public async ValueTask<EducationLevelOptions> AddNewEducationLevelOptions(EducationLevelOptions entity)
        {
            await _education.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async ValueTask<IEnumerable<EducationLevelOptions>> GetAllEducationLevelOptions()
        {
            return await _education.ToListAsync();
        }

        public async ValueTask<EducationLevelOptions> UpdateCurrentEducationLevelOptions(EducationLevelOptions entity)
        {
            _education.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async ValueTask<EducationLevelOptions> DeleteEducationLevelOptions(int id)
        {
            var entity = await _education.FindAsync(id);
            if (entity == null) return null;

            _education.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async ValueTask<MainSettings> AddNewMainSettings(MainSettings entity)
        {
            await _main.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async ValueTask<IEnumerable<MainSettings>> GetAllMainSettings()
        {
            return await _main.ToListAsync();
        }

        public async ValueTask<MainSettings> UpdateMainSettings(MainSettings entity)
        {
            _main.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async ValueTask<MainSettings> DeleteMainSettings(int id)
        {
            var entity = await _main.FindAsync(id);
            if (entity == null) return null;

            _main.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async ValueTask<MainSettings> GetMainSettings(int id)
        {
             var entity = await _main.FindAsync(id);
             return entity;
        }

        public async ValueTask<string> GetMainSettingsByDepartmentName()
        {
            var entity = await _main.FirstOrDefaultAsync();
            return entity?.DepartmentName;
        }

        public async ValueTask<string> GetMainSettingsByDepartmentDirectorName()
        {
            var entity = await _main.FirstOrDefaultAsync();
            return entity?.DepartmentDirectorName;
        }

        public async ValueTask<string> GetMainSettingsByConscriptsAffairsOfficerName()
        {
            var entity = await _main.FirstOrDefaultAsync();
            return entity?.ConscriptsAffairsOfficerName;
        }

        public async ValueTask<string> GetMainSettingsByConscriptsAffairsPoliceManName()
        {
            var entity = await _main.FirstOrDefaultAsync();
            return entity?.ConscriptsAffairsPoliceManName;
        }

        public async ValueTask<SoldierLeaveOptions> AddNewSoldierLeaveOptions(SoldierLeaveOptions entity)
        {

            await _soldierLeave.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async ValueTask<IEnumerable<SoldierLeaveOptions>> GetAllSoldierLeaveOptions()
        {
            return await _soldierLeave.ToListAsync();
        }

        public async ValueTask<SoldierLeaveOptions> UpdateCurrentSoldierLeaveOptions(SoldierLeaveOptions entity)
        {
            _soldierLeave.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async ValueTask<SoldierLeaveOptions> DeleteSoldierLeaveOptions(int id)
        {
            var entity = await _soldierLeave.FindAsync(id);
            if (entity == null) return null;

            _soldierLeave.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async ValueTask<DailyMealOptions> AddNewDailyMealOptions(DailyMealOptions entity)
        {

            await _dailyMeal.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async ValueTask<IEnumerable<DailyMealOptions>> GetAllDailyMealOptions()
        {
            return await _dailyMeal.ToListAsync();
        }

        public async ValueTask<DailyMealOptions> DeleteDailyMealOptions(int id)
        {
            var entity = await _dailyMeal.FindAsync(id);
            if (entity == null) return null;

            _dailyMeal.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

    }
}
