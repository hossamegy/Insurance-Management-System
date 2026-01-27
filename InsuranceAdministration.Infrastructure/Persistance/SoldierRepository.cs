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
        private readonly DbSet<SoldierLeave> _leavesEntity;

        public SoldierRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entity = _context.Set<Soldier>();
            _leavesEntity = _context.Set<SoldierLeave>();
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
                .Include(s => s.Leaves)
                .Include(s => s.SoldierMissions)
                .ToListAsync();
        }

        public async ValueTask<Soldier> GetSoldier(int id)
        {
            var soldier = await _entity
                .Include(s => s.AcquaintanceDocument)
                    .ThenInclude(a => a.BaseFamily)
                .Include(s => s.AcquaintanceDocument)
                    .ThenInclude(a => a.Family)
                .Include(s => s.Leaves)
                .Include(s => s.SoldierMissions)
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
                .Include(s => s.Leaves)
                .Include(s => s.SoldierMissions)
                .ToListAsync();
        }

        public async ValueTask<IEnumerable<Soldier>> GetSoldierByAssignment(string soldierAssignment)
        {
            if (string.IsNullOrWhiteSpace(soldierAssignment))
                throw new ArgumentException("Assignment cannot be null or empty.", nameof(soldierAssignment));

            return await _entity
                .Where(s => s.Assignment == soldierAssignment)
                .Include(s => s.AcquaintanceDocument)
                .Include(s => s.Leaves)
                .Include(s => s.SoldierMissions)
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
                .Include(s => s.Leaves)
                .FirstOrDefaultAsync(s => s.Id == soldierLeave.SoldierId);

            if (soldier == null)
                throw new KeyNotFoundException($"Soldier with ID {soldierLeave.SoldierId} not found.");

            // إزالة الشرط الخاطئ - المجند يمكن أن يكون لديه عدة إجازات
            // Initialize the collection if null (في حالات نادرة)
            if (soldier.Leaves == null)
                soldier.Leaves = new List<SoldierLeave>();

            // ببساطة أضف الإجازة الجديدة
            soldier.Leaves.Add(soldierLeave);
            await _context.SaveChangesAsync();

            return soldierLeave;
        }

        public async ValueTask<IEnumerable<SoldierLeave>> GetSoldierLeave(int soldierId)
        {
            var soldier = await _entity
                .Include(s => s.Leaves)
                .FirstOrDefaultAsync(s => s.Id == soldierId);

            if (soldier == null)
                throw new KeyNotFoundException($"Soldier with ID {soldierId} not found.");
            var soldierLeaves = soldier.Leaves?.OrderByDescending(l => l.Start);
            return soldierLeaves;
        }

        public async ValueTask<SoldierLeave> UpdateSoldierLeave(SoldierLeave soldierLeave)
        {
            var soldier = await _entity
                .Include(s => s.Leaves)
                .FirstOrDefaultAsync(l => l.Id == soldierLeave.SoldierId);

            if (soldier == null)
                throw new KeyNotFoundException($"Soldier with ID {soldierLeave.SoldierId} not found.");

            var existingLeave = soldier.Leaves?
                .OrderByDescending(l => l.Start)
                .FirstOrDefault();

            if (existingLeave == null)
                throw new KeyNotFoundException($"No leave found for soldier ID {soldierLeave.SoldierId}");

            // Update properties
            existingLeave.EndNum = soldierLeave.EndNum;
            existingLeave.EndPage = soldierLeave.EndPage;
            existingLeave.End = soldierLeave.End;
            existingLeave.Type = soldierLeave.Type;

            await _context.SaveChangesAsync();
            return existingLeave;
        }


        public async ValueTask<SoldierLeave> GetLastSoldierLeave(int soldierId)
        {
            var soldier = await _entity
                .Where(l => l.Id == soldierId)
                .FirstOrDefaultAsync();

            var lastLeave = soldier?.Leaves?
                .OrderByDescending(l => l.Start)
                .FirstOrDefault();

            return lastLeave;
        }
        public async ValueTask<int> GetSoldiersCountsIsActive()
        {
            return await _entity.CountAsync(s => s.IsActive == true); ;
        }
        public async ValueTask<int> GetSoldiersLeaveCounts()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            // Count soldiers where ANY leave has End >= tomorrow (meaning End > today)
            return await _entity
                .Where(s => s.IsActive == true
                         && s.Leaves.Any(l => l.End > tomorrow))
                .CountAsync();
           
        }

        public async ValueTask<int> GetSoldierAttendanceCounts()
        {
            int countSoldiersAttendance = await _entity
                .Where(s => s.CurrentIsLeave == false)
                .CountAsync();
            return countSoldiersAttendance;
        }

        public async ValueTask<int> GetSoldiersLeaveCountsByType(string type)
        {
            var count = await _entity
                .Where(s => s.IsActive == true)
                .SelectMany(s => s.Leaves)
                .Where(l => l.Type == type)
                .CountAsync();

            return count;
        }
        public async ValueTask<SoldierLeave> GetSoldierLeaveById(int soldierLeaveId)
        {
            var soldierLeave = await _leavesEntity
                .FirstOrDefaultAsync(l => l.Id == soldierLeaveId);
            if (soldierLeave == null)
                throw new KeyNotFoundException($"Soldier leave with ID {soldierLeaveId} not found.");
            return soldierLeave;
        }
        public async ValueTask<SoldierLeave> DeleteSoldierLeave(SoldierLeave soldierLeave)
        {
            _leavesEntity.Remove(soldierLeave);
            await _context.SaveChangesAsync();
            return soldierLeave;
        }

        public async ValueTask<IEnumerable<Soldier>> GetAllSoldierAttendenceNotRiver()
        {
            var soldiers = await _entity
                .Include(s => s.SoldierMissions)
                .Where(s => s.IsActive == true && s.CurrentIsLeave == false && s.Assignment != "مسطح مائى")
                .ToListAsync();
            return soldiers;
        }
        public async ValueTask<IEnumerable<Soldier>> GetAllSoldierAttendenceRiver()
        {
            var soldiers = await _entity
                .Include(s => s.SoldierMissions)
                .Where(s => s.IsActive == true && s.CurrentIsLeave == false && s.Assignment == "مسطح مائى")
                .ToListAsync();
            return soldiers;
        }
        // SoldierRepository.cs
        public async ValueTask<IEnumerable<SoldierMission>> AddDailyMissionsToSoldiers(List<SoldierMission> soldierMissions)
        {
            if (soldierMissions == null || !soldierMissions.Any())
                throw new ArgumentException("Soldier missions list cannot be null or empty");

            try
            {
                // Validate that all soldiers exist
                var soldierIds = soldierMissions.Select(sm => sm.SoldierId).Distinct().ToList();
                var existingSoldiers = await _entity
                    .Where(s => soldierIds.Contains(s.Id))
                    .Select(s => s.Id)
                    .ToListAsync();

                var missingSoldiers = soldierIds.Except(existingSoldiers).ToList();
                if (missingSoldiers.Any())
                {
                    throw new KeyNotFoundException($"Soldiers with IDs {string.Join(", ", missingSoldiers)} not found");
                }

                // Add all soldier missions
                await _context.Set<SoldierMission>().AddRangeAsync(soldierMissions);
                await _context.SaveChangesAsync();

                return soldierMissions;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error adding soldier missions", ex);
            }
        }
    }
}