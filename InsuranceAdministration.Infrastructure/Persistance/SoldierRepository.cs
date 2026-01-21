using InsuranceAdministration.Core.Entities.SoldierEntities;
using InsuranceAdministration.Core.Entities.SoldierEntities.Acquaintance;
using InsuranceAdministration.Core.Interfaces.Repository;
using InsuranceAdministration.Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAdministration.Infrastructure.Persistance
{
    public class SoldierRepository : ISoldierRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Soldier> _entity;

        public SoldierRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entity = _context.Set<Soldier>();
        }

        public async ValueTask<Soldier> AddNewSoldier(Soldier soldier)
        {
            if (soldier == null)
                throw new ArgumentNullException(nameof(soldier));

            await _entity.AddAsync(soldier);
            await _context.SaveChangesAsync();
            return soldier;
        }

        public async ValueTask<Soldier> DeleteSoldier(int id)
        {
            var soldier = await _entity.FindAsync(id);

            if (soldier == null)
                throw new KeyNotFoundException($"Soldier with ID {id} not found.");

            _entity.Remove(soldier);
            await _context.SaveChangesAsync();
            return soldier;
        }

        public async ValueTask<IEnumerable<Soldier>> GetAllSoldier()
        {
            return await _entity
                .Include(s => s.AcquaintanceDocument)
                    .ThenInclude(a => a.BaseFamily)
                .Include(s => s.AcquaintanceDocument)
                    .ThenInclude(a => a.Family)
                .Include(s => s.Leave)
                .Include(s => s.Missions)
                .ToListAsync();
        }

        public async ValueTask<Soldier> GetSoldier(int id)
        {
            var soldier = await _entity
                .Include(s => s.AcquaintanceDocument)
                    .ThenInclude(a => a.BaseFamily)
                .Include(s => s.AcquaintanceDocument)
                    .ThenInclude(a => a.Family)
                .Include(s => s.Leave)
                .Include(s => s.Missions)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (soldier == null)
                throw new KeyNotFoundException($"Soldier with ID {id} not found.");

            return soldier;
        }

        public async ValueTask<Soldier> UpdateCurrentSoldier(Soldier soldier)
        {
            if (soldier == null)
                throw new ArgumentNullException(nameof(soldier));

            _entity.Update(soldier);
            await _context.SaveChangesAsync();
            return soldier;
        }

        public async ValueTask<IEnumerable<Soldier>> GetPaginatedSoldiersByActive(int pageNumber, int pageSize, bool isActive = true)
        {
            if (pageNumber < 1)
                throw new ArgumentException("Page number must be greater than 0.", nameof(pageNumber));

            if (pageSize < 1)
                throw new ArgumentException("Page size must be greater than 0.", nameof(pageSize));

            return await _entity
                .Where(s => s.IsActive == isActive)
                .OrderBy(s => s.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(s => s.AcquaintanceDocument)
                .Include(s => s.Leave)
                .Include(s => s.Missions)
                .ToListAsync();
        }

        public async ValueTask<IEnumerable<Soldier>> GetSoldierByAssignment(string soldierAssignment)
        {
            if (string.IsNullOrWhiteSpace(soldierAssignment))
                throw new ArgumentException("Assignment cannot be null or empty.", nameof(soldierAssignment));

            return await _entity
                .Where(s => s.Assignment == soldierAssignment)
                .Include(s => s.AcquaintanceDocument)
                .Include(s => s.Leave)
                .Include(s => s.Missions)
                .ToListAsync();
        }

        // AcquaintanceDocument Methods

        public async ValueTask<AcquaintanceDocument> AddAcquaintanceDocument(int soldierId, AcquaintanceDocument acquaintanceDocument)
        {
            if (acquaintanceDocument == null)
                throw new ArgumentNullException(nameof(acquaintanceDocument));

            // Check if soldier exists and doesn't have a document
            var soldier = await _entity
                .AsNoTracking()
                .Include(s => s.AcquaintanceDocument)
                .FirstOrDefaultAsync(s => s.Id == soldierId);

            if (soldier == null)
                throw new KeyNotFoundException($"Soldier with ID {soldierId} not found.");

            if (soldier.AcquaintanceDocument != null)
                throw new InvalidOperationException($"Soldier with ID {soldierId} already has an acquaintance document.");

            // Set the SoldierId
            acquaintanceDocument.SoldierId = soldierId;

            // Add and save
            await _context.Set<AcquaintanceDocument>().AddAsync(acquaintanceDocument);
            var result = await _context.SaveChangesAsync();

            Console.WriteLine($"Repository: Saved {result} records. Document ID: {acquaintanceDocument.Id}");

            return acquaintanceDocument;
        }
        

        public async ValueTask<AcquaintanceDocument> GetAcquaintanceDocument(int soldierId)
        {
            var acquaintanceDocument = await _context.Set<AcquaintanceDocument>()
                .Include(a => a.BaseFamily)
                .Include(a => a.Family)
                .FirstOrDefaultAsync(a => a.SoldierId == soldierId);

            if (acquaintanceDocument == null)
                throw new KeyNotFoundException($"Acquaintance document for soldier ID {soldierId} not found.");

            return acquaintanceDocument;
        }

        // في SoldierRepository.cs

        public async ValueTask<AcquaintanceDocument> UpdateAcquaintanceDocument(AcquaintanceDocument acquaintanceDocument)
        {
            if (acquaintanceDocument == null)
                throw new ArgumentNullException(nameof(acquaintanceDocument));

            var existingDocument = await _context.Set<AcquaintanceDocument>()
                .Include(a => a.BaseFamily)
                .Include(a => a.Family)
                .FirstOrDefaultAsync(a => a.Id == acquaintanceDocument.Id);

            if (existingDocument == null)
                throw new KeyNotFoundException($"Acquaintance document with ID {acquaintanceDocument.Id} not found.");

            // Remove old family members
            if (existingDocument.BaseFamily != null && existingDocument.BaseFamily.Any())
            {
                _context.Set<BaseFamily>().RemoveRange(existingDocument.BaseFamily);
            }

            if (existingDocument.Family != null && existingDocument.Family.Any())
            {
                _context.Set<Family>().RemoveRange(existingDocument.Family);
            }

            // Add new family members
            if (acquaintanceDocument.BaseFamily != null)
            {
                foreach (var member in acquaintanceDocument.BaseFamily)
                {
                    member.Id = 0; // Reset ID for new entries
                    member.AcquaintanceDocumentId = existingDocument.Id;
                }
                existingDocument.BaseFamily = acquaintanceDocument.BaseFamily;
            }

            if (acquaintanceDocument.Family != null)
            {
                foreach (var member in acquaintanceDocument.Family)
                {
                    member.Id = 0; // Reset ID for new entries
                    member.AcquaintanceDocumentId = existingDocument.Id;
                }
                existingDocument.Family = acquaintanceDocument.Family;
            }

            await _context.SaveChangesAsync();
            return existingDocument;
        }

        // SoldierLeave Methods
        public async ValueTask<SoldierLeave> AddSoldierLeave(SoldierLeave soldierLeave)
        {
            if (soldierLeave == null)
                throw new ArgumentNullException(nameof(soldierLeave));

            var soldier = await _entity
                .Include(s => s.Leave)
                .FirstOrDefaultAsync(s => s.Id == soldierLeave.SoldierId);

            if (soldier == null)
                throw new KeyNotFoundException($"Soldier with ID {soldierLeave.SoldierId} not found.");

            if (soldier.Leave != null)
                throw new InvalidOperationException($"Soldier with ID {soldierLeave.SoldierId} already has a leave record.");

            soldier.Leave = soldierLeave;
            await _context.SaveChangesAsync();

            return soldierLeave;
        }

        public async ValueTask<SoldierLeave> GetSoldierLeave(int soldierId)
        {
            var soldierLeave = await _context.Set<SoldierLeave>()
                .FirstOrDefaultAsync(l => l.SoldierId == soldierId);

            if (soldierLeave == null)
                throw new KeyNotFoundException($"Leave record for soldier ID {soldierId} not found.");

            return soldierLeave;
        }

        public async ValueTask<SoldierLeave> UpdateSoldierLeave(SoldierLeave soldierLeave)
        {
            if (soldierLeave == null)
                throw new ArgumentNullException(nameof(soldierLeave));

            var existingLeave = await _context.Set<SoldierLeave>()
                .FirstOrDefaultAsync(l => l.SoldierId == soldierLeave.SoldierId);

            if (existingLeave == null)
                throw new KeyNotFoundException($"Leave record for soldier ID {soldierLeave.SoldierId} not found.");

            // Update properties
            existingLeave.StartNum = soldierLeave.StartNum;
            existingLeave.StartPage = soldierLeave.StartPage;
            existingLeave.Start = soldierLeave.Start;
            existingLeave.EndNum = soldierLeave.EndNum;
            existingLeave.EndPage = soldierLeave.EndPage;
            existingLeave.End = soldierLeave.End;

            await _context.SaveChangesAsync();
            return existingLeave;
        }
    }
}