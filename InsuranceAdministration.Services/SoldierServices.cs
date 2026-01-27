using InsuranceAdministration.Core.DTOs.Soldiers;
using InsuranceAdministration.Core.Entities.SoldierEntities;
using InsuranceAdministration.Core.Exceptions;
using InsuranceAdministration.Core.Interfaces.Repository;
using InsuranceAdministration.Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InsuranceAdministration.Services
{
    public class SoldierServices : ISoldierServices
    {
        private readonly ISoldierRepository _repository;
        private readonly ILogger<SoldierServices> _logger;

        public SoldierServices(ISoldierRepository repository, ILogger<SoldierServices> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async ValueTask<Soldier> AddNewSoldier(Soldier soldier)
        {
            if (soldier == null)
            {
                _logger.LogError("Attempted to add null soldier in service layer");
                throw new ArgumentNullException(nameof(soldier));
            }

            try
            {
                _logger.LogInformation("Service: Adding new soldier with name: {Name}", soldier.Name);

                var result = await _repository.AddNewSoldier(soldier);

                _logger.LogInformation("Service: Successfully added soldier with ID: {SoldierId}", result.Id);
                return result;
            }
            catch (ValidationException)
            {
                _logger.LogWarning("Service: Validation failed for soldier: {Name}", soldier.Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Error occurred while adding soldier: {Name}", soldier.Name);
                throw;
            }
        }

        public async ValueTask<Soldier> DeleteSoldier(int id)
        {
            try
            {
                _logger.LogInformation("Service: Deleting soldier with ID: {SoldierId}", id);

                var soldier = await _repository.DeleteSoldier(id);

                _logger.LogInformation("Service: Successfully deleted soldier with ID: {SoldierId}", id);
                return soldier;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Service: Soldier with ID: {SoldierId} not found", id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Error occurred while deleting soldier with ID: {SoldierId}", id);
                throw;
            }
        }

        public async ValueTask<IEnumerable<Soldier>> GetAllSoldier()
        {
            try
            {
                _logger.LogInformation("Service: Retrieving all soldiers");

                var soldiers = await _repository.GetAllSoldier();

                _logger.LogInformation("Service: Successfully retrieved {Count} soldiers", soldiers.Count());
                return soldiers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Error occurred while retrieving all soldiers");
                throw;
            }
        }

        public async ValueTask<Soldier> GetSoldier(int id)
        {
            try
            {
                _logger.LogInformation("Service: Retrieving soldier with ID: {SoldierId}", id);

                var soldier = await _repository.GetSoldier(id);

                _logger.LogInformation("Service: Successfully retrieved soldier with ID: {SoldierId}", id);
                return soldier;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Service: Soldier with ID: {SoldierId} not found", id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Error occurred while retrieving soldier with ID: {SoldierId}", id);
                throw;
            }
        }

        public async ValueTask<Soldier> UpdateCurrentSoldier(Soldier soldier)
        {
            if (soldier == null)
            {
                _logger.LogError("Service: Attempted to update with null soldier");
                throw new ArgumentNullException(nameof(soldier));
            }

            try
            {
                _logger.LogInformation("Service: Updating soldier with ID: {SoldierId}", soldier.Id);

                // Retrieve the existing entity (tracked by EF Core)
                var existingSoldier = await _repository.GetSoldier(soldier.Id);

                // Update all relevant properties
                existingSoldier.Name = soldier.Name;
                existingSoldier.BirthDate = soldier.BirthDate;
                existingSoldier.Job = soldier.Job;
                existingSoldier.MaritalStatus = soldier.MaritalStatus;
                existingSoldier.EnlistmentDate = soldier.EnlistmentDate;
                existingSoldier.PoliceNumber = soldier.PoliceNumber;
                existingSoldier.Street = soldier.Street;
                existingSoldier.Region = soldier.Region;
                existingSoldier.City = soldier.City;
                existingSoldier.TripleNumber = soldier.TripleNumber;
                existingSoldier.NationalId = soldier.NationalId;
                existingSoldier.Assignment = soldier.Assignment;
                existingSoldier.EducationLevel = soldier.EducationLevel;
                existingSoldier.PhoneNumber = soldier.PhoneNumber;
                existingSoldier.ServiceEndDate = soldier.ServiceEndDate;
                existingSoldier.IsActive = soldier.IsActive;
                existingSoldier.AcquaintanceDocument = soldier.AcquaintanceDocument;
                existingSoldier.CurrentIsLeave = soldier.CurrentIsLeave;
                existingSoldier.Leaves = soldier.Leaves;


                // Update missions if provided
                if (soldier.SoldierMissions != null && soldier.SoldierMissions.Any())
                {
                    existingSoldier.SoldierMissions = soldier.SoldierMissions;
                }

                var updatedSoldier = await _repository.UpdateCurrentSoldier(existingSoldier);

                _logger.LogInformation("Service: Successfully updated soldier with ID: {SoldierId}", updatedSoldier.Id);
                return updatedSoldier;
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Service: Validation failed for soldier ID: {SoldierId}", soldier.Id);
                throw;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Service: Soldier with ID: {SoldierId} not found", soldier.Id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Error occurred while updating soldier with ID: {SoldierId}", soldier.Id);
                throw;
            }
        }

        public async ValueTask<IEnumerable<Soldier>> GetPaginatedSoldiersByActive(int pageNumber, int pageSize, bool isActive)
        {
            try
            {
                _logger.LogInformation(
                    "Service: Retrieving paginated soldiers - Page: {PageNumber}, Size: {PageSize}, Active: {IsActive}",
                    pageNumber, pageSize, isActive);

                var soldiers = await _repository.GetPaginatedSoldiersByActive(pageNumber, pageSize, isActive);

                _logger.LogInformation("Service: Successfully retrieved {Count} paginated soldiers", soldiers.Count());
                return soldiers;
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex,
                    "Service: Invalid pagination parameters - Page: {PageNumber}, Size: {PageSize}",
                    pageNumber, pageSize);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Error occurred while retrieving paginated soldiers");
                throw;
            }
        }

        public async ValueTask<IEnumerable<Soldier>> GetSoldierByAssignment(string soldierAssignment)
        {
            if (string.IsNullOrWhiteSpace(soldierAssignment))
            {
                _logger.LogWarning("Service: SoldierAssignment is null or empty");
                throw new ArgumentException("Assignment cannot be null or empty", nameof(soldierAssignment));
            }

            try
            {
                _logger.LogInformation("Service: Retrieving soldiers with assignment: {Assignment}", soldierAssignment);

                var soldiers = await _repository.GetSoldierByAssignment(soldierAssignment);

                _logger.LogInformation(
                    "Service: Successfully retrieved {Count} soldiers with assignment: {Assignment}",
                    soldiers.Count(), soldierAssignment);

                return soldiers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Service: Error occurred while retrieving soldiers with assignment: {Assignment}",
                    soldierAssignment);
                throw;
            }
        }

        // AcquaintanceDocument Methods
        public async ValueTask<AcquaintanceDocument> AddAcquaintanceDocument(int SoldierId, AcquaintanceDocument acquaintanceDocument)
        {
            if (acquaintanceDocument == null)
            {
                _logger.LogError("Service: Attempted to add null acquaintance document");
                throw new ArgumentNullException(nameof(acquaintanceDocument));
            }

            try
            {
                _logger.LogInformation(
                    "Service: Adding acquaintance document for soldier ID: {SoldierId}",
                    acquaintanceDocument.SoldierId);

                var result = await _repository.AddAcquaintanceDocument(SoldierId, acquaintanceDocument);

                _logger.LogInformation(
                    "Service: Successfully added acquaintance document with ID: {DocumentId}",
                    result.Id);

                return result;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex,
                    "Service: Soldier with ID: {SoldierId} not found",
                    acquaintanceDocument.SoldierId);
                throw;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex,
                    "Service: Acquaintance document already exists for soldier ID: {SoldierId}",
                    acquaintanceDocument.SoldierId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Service: Error occurred while adding acquaintance document for soldier ID: {SoldierId}",
                    acquaintanceDocument.SoldierId);
                throw;
            }
        }

        public async ValueTask<AcquaintanceDocument> GetAcquaintanceDocument(int soldierId)
        {
            try
            {
                _logger.LogInformation(
                    "Service: Retrieving acquaintance document for soldier ID: {SoldierId}",
                    soldierId);

                var document = await _repository.GetAcquaintanceDocument(soldierId);

                _logger.LogInformation(
                    "Service: Successfully retrieved acquaintance document for soldier ID: {SoldierId}",
                    soldierId);

                return document;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex,
                    "Service: Acquaintance document for soldier ID: {SoldierId} not found",
                    soldierId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Service: Error occurred while retrieving acquaintance document for soldier ID: {SoldierId}",
                    soldierId);
                throw;
            }
        }

        public async ValueTask<AcquaintanceDocument> UpdateAcquaintanceDocument(AcquaintanceDocument acquaintanceDocument)
        {
            if (acquaintanceDocument == null)
            {
                _logger.LogError("Service: Attempted to update null acquaintance document");
                throw new ArgumentNullException(nameof(acquaintanceDocument));
            }

            try
            {
                _logger.LogInformation(
                    "Service: Updating acquaintance document for soldier ID: {SoldierId}",
                    acquaintanceDocument.SoldierId);

                var result = await _repository.UpdateAcquaintanceDocument(acquaintanceDocument);

                _logger.LogInformation(
                    "Service: Successfully updated acquaintance document for soldier ID: {SoldierId}",
                    acquaintanceDocument.SoldierId);

                return result;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex,
                    "Service: Acquaintance document for soldier ID: {SoldierId} not found",
                    acquaintanceDocument.SoldierId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Service: Error occurred while updating acquaintance document for soldier ID: {SoldierId}",
                    acquaintanceDocument.SoldierId);
                throw;
            }
        }

        // SoldierLeave Methods
        public async ValueTask<SoldierLeave> AddSoldierLeave(SoldierLeave soldierLeave)
        {
            if (soldierLeave == null)
            {
                _logger.LogError("Service: Attempted to add null soldier leave");
                throw new ArgumentNullException(nameof(soldierLeave));
            }

            try
            {
                _logger.LogInformation(
                    "Service: Adding leave record for soldier ID: {SoldierId}",
                    soldierLeave.SoldierId);

                var result = await _repository.AddSoldierLeave(soldierLeave);

                _logger.LogInformation(
                    "Service: Successfully added leave record with ID: {LeaveId}",
                    result.Id);

                return result;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex,
                    "Service: Soldier with ID: {SoldierId} not found",
                    soldierLeave.SoldierId);
                throw;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex,
                    "Service: Leave record already exists for soldier ID: {SoldierId}",
                    soldierLeave.SoldierId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Service: Error occurred while adding leave record for soldier ID: {SoldierId}",
                    soldierLeave.SoldierId);
                throw;
            }
        }

        public async ValueTask<IEnumerable<SoldierLeave>> GetSoldierLeave(int soldierId)
        {
            try
            {
                _logger.LogInformation(
                    "Service: Retrieving leave record for soldier ID: {SoldierId}",
                    soldierId);

                var leaves = await _repository.GetSoldierLeave(soldierId);

                _logger.LogInformation(
                    "Service: Successfully retrieved leave record for soldier ID: {SoldierId}",
                    soldierId);

                return leaves;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex,
                    "Service: Leave record for soldier ID: {SoldierId} not found",
                    soldierId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Service: Error occurred while retrieving leave record for soldier ID: {SoldierId}",
                    soldierId);
                throw;
            }
        }

        public async ValueTask<SoldierLeave> UpdateSoldierLeave(SoldierLeave soldierLeave)
        {
            if (soldierLeave == null)
            {
                _logger.LogError("Service: Attempted to update null soldier leave");
                throw new ArgumentNullException(nameof(soldierLeave));
            }

            try
            {
                _logger.LogInformation(
                    "Service: Updating leave record for soldier ID: {SoldierId}",
                    soldierLeave.SoldierId);

                var result = await _repository.UpdateSoldierLeave(soldierLeave);

                _logger.LogInformation(
                    "Service: Successfully updated leave record for soldier ID: {SoldierId}",
                    soldierLeave.SoldierId);

                return result;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex,
                    "Service: Leave record for soldier ID: {SoldierId} not found",
                    soldierLeave.SoldierId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Service: Error occurred while updating leave record for soldier ID: {SoldierId}",
                    soldierLeave.SoldierId);
                throw;
            }
        }

        public async ValueTask<SoldierLeave> GetLastSoldierLeave(int soldierId)
        {
            var leave = await _repository.GetLastSoldierLeave(soldierId);
            return leave;
        }

        public async ValueTask<int> GetSoldiersCountsIsActive()
        {
            return await _repository.GetSoldiersCountsIsActive();
        }
         
        
        public async ValueTask<int> GetSoldiersLeaveCounts()
        {
            return await _repository.GetSoldiersLeaveCounts();
        }

        public async ValueTask<int> GetSoldierAttendanceCounts()
        {
            return await _repository.GetSoldierAttendanceCounts();
        }
        public async ValueTask<int> GetSoldiersLeaveCountsByType(string Type)
        {
            return await _repository.GetSoldiersLeaveCountsByType(Type);
        }

        public async ValueTask<SoldierLeave> GetSoldierLeaveById(int soldierLeaveId)
        {
           
            return await _repository.GetSoldierLeaveById(soldierLeaveId);
        }
        public async ValueTask<SoldierLeave> DeleteSoldierLeave(SoldierLeave soldierLeave)
        {
            return await _repository.DeleteSoldierLeave(soldierLeave);
        }
        public async ValueTask<IEnumerable<Soldier>> GetAllSoldierAttendenceNotRiver()
        {
            var soldiers = await _repository.GetAllSoldierAttendenceNotRiver();
            return soldiers.ToList();
        }
        public async ValueTask<IEnumerable<Soldier>> GetAllSoldierAttendenceRiver()
        {
            var soldiers = await _repository.GetAllSoldierAttendenceRiver();
            return soldiers.ToList();
        }
        // SoldierServices.cs
        public async ValueTask<IEnumerable<SoldierMission>> AddDailyMissionsToSoldiers(List<SoldierMission> soldierMissions)
        {
            if (soldierMissions == null || !soldierMissions.Any())
            {
                _logger.LogWarning("Service: Attempted to add null or empty soldier missions list");
                throw new ArgumentException("Soldier missions list cannot be null or empty");
            }

            try
            {
                _logger.LogInformation($"Service: Adding {soldierMissions.Count} soldier missions");

                var result = await _repository.AddDailyMissionsToSoldiers(soldierMissions);

                _logger.LogInformation($"Service: Successfully added {soldierMissions.Count} soldier missions");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Error occurred while adding soldier missions");
                throw;
            }
        }
    }
}