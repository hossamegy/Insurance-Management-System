
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
                .Include(m => m.Policemen)
                .Include(m => m.Soldiers)
                .ToListAsync();
        }

        public async ValueTask<Mission?> GetMission(int id)
        {
            return await _entities
                .Include(m => m.Policemen)
                .Include(m => m.Soldiers)
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
    }
}
