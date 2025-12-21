using InsuranceAdministration.Core.Entities.SoldierEntities;
using InsuranceAdministration.Core.Exceptions;
using InsuranceAdministration.Core.Interfaces.Repository;
using InsuranceAdministration.Core.Interfaces.Services;
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
                _logger.LogInformation("Service: Adding new soldier");

             

                var result = await _repository.AddNewSoldier(soldier);

                _logger.LogInformation("Service: Successfully added soldier with ID: {SoldierId}", result.Id);
                return result;
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Error occurred while adding soldier");
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
            catch (EntityNotFoundException ex)
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

                _logger.LogInformation("Service: Successfully retrieved soldiers");
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
            catch (EntityNotFoundException ex)
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
                existingSoldier.Missions = soldier.Missions;

                // Save the updated entity
                var updatedSoldier = await _repository.UpdateCurrentSoldier(existingSoldier);

                _logger.LogInformation("Service: Successfully updated soldier with ID: {SoldierId}", updatedSoldier.Id);
                return updatedSoldier;
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (EntityNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Error occurred while updating soldier with ID: {SoldierId}", soldier.Id);
                throw;
            }
        }


        public async ValueTask<IEnumerable<Soldier>> GetPaginatedSoldiersByActive(int pageNumber, int pageSize, bool IsActive)
        {
            try
            {
                _logger.LogInformation("Service: Retrieving paginated soldiers - Page: {PageNumber}, Size: {PageSize}",
                    pageNumber, pageSize);

                var soldiers = await _repository.GetPaginatedSoldiersByActive(pageNumber, pageSize, true);

                _logger.LogInformation("Service: Successfully retrieved paginated soldiers");
                return soldiers;
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Service: Invalid pagination parameters - Page: {PageNumber}, Size: {PageSize}",
                    pageNumber, pageSize);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Error occurred while retrieving paginated soldiers");
                throw;
            }
        }
        public async ValueTask<IEnumerable<Soldier>> GetSoldierByAssignment(string SoldierAssignment)
        {
            if (string.IsNullOrWhiteSpace(SoldierAssignment))
            {
                _logger.LogWarning("Service: SoldierAssignment is null or empty");
                return Enumerable.Empty<Soldier>();
            }

            try
            {
                _logger.LogInformation("Service: Retrieving soldiers with assignment: {Assignment}", SoldierAssignment);

                var soldiers = await _repository.GetSoldierByAssignment(SoldierAssignment);

                _logger.LogInformation("Service: Successfully retrieved {Count} soldiers with assignment: {Assignment}",
                    soldiers?.Count() ?? 0, SoldierAssignment);

                return soldiers;
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning(ex, "Service: No soldiers found with assignment: {Assignment}", SoldierAssignment);
                return Enumerable.Empty<Soldier>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Error occurred while retrieving soldiers with assignment: {Assignment}", SoldierAssignment);
                throw;
            }
        }

    }
}
