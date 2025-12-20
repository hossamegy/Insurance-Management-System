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

        public SettingsRepository(AppDbContext context)
        {
            _context = context;
            _assignment = _context.Set<AssignmentOptions>();
            _education = _context.Set<EducationLevelOptions>();
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

    }
}
