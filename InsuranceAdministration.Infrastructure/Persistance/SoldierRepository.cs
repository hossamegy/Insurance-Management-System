using InsuranceAdministration.Core.Entities.SoldierEntities;
using InsuranceAdministration.Core.Interfaces.Repository;
using InsuranceAdministration.Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAdministration.Infrastructure.Persistance
{
    public class SoldierRepository : ISoldierRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Soldier> _entitiy;
        public SoldierRepository(AppDbContext context)
        {
            _context = context;
            _entitiy = _context.Set<Soldier>();
        }
        public async ValueTask<Soldier> AddNewSoldier(Soldier soldier)
        {
            await _entitiy.AddAsync(soldier);
            await _context.SaveChangesAsync();

            return soldier;
        }

        public async ValueTask<Soldier> DeleteSoldier(int id)
        {
            Soldier soldier = await _entitiy.FindAsync(id);
             _entitiy.Remove(soldier);
            await _context.SaveChangesAsync();
            return soldier;
        }

        public async ValueTask<IEnumerable<Soldier>> GetAllSoldier()
        {
            IEnumerable<Soldier> soldiers = await _entitiy.ToListAsync();
            return soldiers;
        }

        public async ValueTask<Soldier> GetSoldier(int id)
        {
            return await _entitiy.FindAsync(id);
        }

        public async ValueTask<Soldier> UpdateCurrentSoldier(Soldier soldier)
        {
            _entitiy.Update(soldier);                
            await _context.SaveChangesAsync();
            return soldier;                    
        }
        
        public async ValueTask<IEnumerable<Soldier>> GetPaginatedSoldiersByActive(int pageNumber, int pageSize, bool IsActive = true)
        {
            IEnumerable<Soldier> soldiers = await _entitiy
                .Where(s => s.IsActive == IsActive)
                 .OrderBy(s => s.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            return soldiers;
        }

        public async ValueTask<IEnumerable<Soldier>> GetSoldierByAssignment(string SoldierAssignment)
        {
            IEnumerable<Soldier> soldiers = await _entitiy.Where(s => s.Assignment == SoldierAssignment).ToListAsync();
            return soldiers;
        }
    }
}
