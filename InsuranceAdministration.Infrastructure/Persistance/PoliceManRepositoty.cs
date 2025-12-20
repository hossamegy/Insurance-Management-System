
using InsuranceAdministration.Core.Entities.PoliceManEntities;
using InsuranceAdministration.Core.Interfaces.Repository;
using InsuranceAdministration.Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAdministration.Infrastructure.Persistance
{
    public class PoliceManRepository : IPoliceManRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<PoliceMan> _entity;

        public PoliceManRepository(AppDbContext context)
        {
            _context = context;
            _entity = _context.Set<PoliceMan>();
        }

        public async ValueTask<PoliceMan> AddNewPoliceMan(PoliceMan policeman)
        {
            await _entity.AddAsync(policeman);
            await _context.SaveChangesAsync();
            return policeman;
        }

        public async ValueTask<PoliceMan> DeletePoliceMan(int id)
        {
            PoliceMan policeman = await _entity.FindAsync(id);
            _entity.Remove(policeman);
            await _context.SaveChangesAsync();
            return policeman;
        }

        public async ValueTask<IEnumerable<PoliceMan>> GetAllPoliceMan()
        {
            return await _entity.ToListAsync();
        }

        public async ValueTask<PoliceMan> GetPoliceMan(int id)
        {
            return await _entity.FindAsync(id);
        }

        public async ValueTask<PoliceMan> UpdateCurrentPoliceMan(PoliceMan policeman)
        {
            _entity.Update(policeman);
            await _context.SaveChangesAsync();
            return policeman;
        }

        public async ValueTask<IEnumerable<PoliceMan>> GetPaginatedPoliceMan(int pageNumber, int pageSize)
        {
            return await _entity
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
