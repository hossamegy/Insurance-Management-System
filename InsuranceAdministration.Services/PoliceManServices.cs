using InsuranceAdministration.Core.Entities.PoliceManEntities;
using InsuranceAdministration.Core.Exceptions;
using InsuranceAdministration.Core.Interfaces.Repository;
using InsuranceAdministration.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;


namespace InsuranceAdministration.Services
{
    public class PoliceManServices : IPoliceManServices
    {
        private readonly IPoliceManRepository _repository;
        private readonly ILogger<PoliceManServices> _logger;

        public PoliceManServices(
            IPoliceManRepository repository,
            ILogger<PoliceManServices> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async ValueTask<PoliceMan> AddNewPoliceMan(PoliceMan PoliceMan)
        {
            if (PoliceMan == null)
            {
                _logger.LogError("Service: Attempted to add null PoliceMan");
                throw new ArgumentNullException(nameof(PoliceMan));
            }

            try
            {
                _logger.LogInformation("Service: Adding new PoliceMan");

                var result = await _repository.AddNewPoliceMan(PoliceMan);

                _logger.LogInformation(
                    $"Service: Successfully added PoliceMan with ID: {result.Id}");

                return result;
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Service: Error occurred while adding PoliceMan");
                throw;
            }
        }

        public async ValueTask<PoliceMan> DeletePoliceMan(int id)
        {
            try
            {
                _logger.LogInformation(
                    $"Service: Deleting PoliceMan with ID: {id}");

                var PoliceMan = await _repository.DeletePoliceMan(id);

                _logger.LogInformation(
                    $"Service: Successfully deleted PoliceMan with ID: {id}");

                return PoliceMan;
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning(
                    ex,
                    $"Service: PoliceMan with ID: {id} not found");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    $"Service: Error occurred while deleting PoliceMan with ID: {id}");
                throw;
            }
        }

        public async ValueTask<IEnumerable<PoliceMan>> GetAllPoliceMan()
        {
            try
            {
                _logger.LogInformation(
                    "Service: Retrieving all PoliceMen");

                var PoliceMen = await _repository.GetAllPoliceMan();

                _logger.LogInformation(
                    "Service: Successfully retrieved all PoliceMen");

                return PoliceMen;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Service: Error occurred while retrieving all PoliceMen");
                throw;
            }
        }

        public async ValueTask<PoliceMan> GetPoliceMan(int id)
        {
            try
            {
                _logger.LogInformation(
                    $"Service: Retrieving PoliceMan with ID: {id}");

                var PoliceMan = await _repository.GetPoliceMan(id);

                _logger.LogInformation(
                    $"Service: Successfully retrieved PoliceMan with ID: {id}");

                return PoliceMan;
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning(
                    ex,
                    $"Service: PoliceMan with ID: {id} not found");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    $"Service: Error occurred while retrieving PoliceMan with ID: {id}");
                throw;
            }
        }

        public async ValueTask<PoliceMan> UpdateCurrentPoliceMan(PoliceMan PoliceMan)
        {
            if (PoliceMan == null)
            {
                _logger.LogError(
                    "Service: Attempted to update with null PoliceMan");
                throw new ArgumentNullException(nameof(PoliceMan));
            }

            try
            {
                _logger.LogInformation(
                    $"Service: Updating PoliceMan with ID: {PoliceMan.Id}");

                var existingPoliceMan =
                    await _repository.GetPoliceMan(PoliceMan.Id);

                existingPoliceMan.Name = PoliceMan.Name;
                existingPoliceMan.Street = PoliceMan.Street;
                existingPoliceMan.Region = PoliceMan.Region;
                existingPoliceMan.City = PoliceMan.City;
                existingPoliceMan.PhoneNumber = PoliceMan.PhoneNumber;
                existingPoliceMan.HasChantDriverCertificate =
                    PoliceMan.HasChantDriverCertificate;
                existingPoliceMan.Punishments = PoliceMan.Punishments;
                existingPoliceMan.Leaves = PoliceMan.Leaves;
                existingPoliceMan.Missions = PoliceMan.Missions;

                var updatedPoliceMan =
                    await _repository.UpdateCurrentPoliceMan(existingPoliceMan);

                _logger.LogInformation(
                    $"Service: Successfully updated PoliceMan with ID: {updatedPoliceMan.Id}");

                return updatedPoliceMan;
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
                _logger.LogError(
                    ex,
                    $"Service: Error occurred while updating PoliceMan with ID: {PoliceMan.Id}");
                throw;
            }
        }

        public async ValueTask<IEnumerable<PoliceMan>> GetPaginatedPoliceMan(
            int pageNumber,
            int pageSize)
        {
            try
            {
                _logger.LogInformation(
                    $"Service: Retrieving paginated PoliceMen - Page: {pageNumber}, Size: {pageSize}");

                var PoliceMen =
                    await _repository.GetPaginatedPoliceMan(pageNumber, pageSize);

                _logger.LogInformation(
                    "Service: Successfully retrieved paginated PoliceMen");

                return PoliceMen;
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(
                    ex,
                    $"Service: Invalid pagination parameters - Page: {pageNumber}, Size: {pageSize}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Service: Error occurred while retrieving paginated PoliceMen");
                throw;
            }
        }
    }
}
