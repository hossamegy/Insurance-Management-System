using InsuranceAdministration.Core.Entities.Settings;
using InsuranceAdministration.Core.Entities.SoldierEntities;
using InsuranceAdministration.Core.Exceptions;
using InsuranceAdministration.Core.Interfaces.Repository;
using InsuranceAdministration.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace InsuranceAdministration.Services
{
    public class SettingsServices : ISettingsServices
    {
        private readonly ISettingsRepository _repository;
        private readonly ILogger<SettingsServices> _logger;

        public SettingsServices(
            ISettingsRepository repository,
            ILogger<SettingsServices> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /* ================= Assignment ================= */

        public async ValueTask<AssignmentOptions> AddNewAssignmentOption(AssignmentOptions entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _logger.LogInformation("Adding Assignment Option");

            return await _repository.AddNewAssignmentOptions(entity);
        }

        public async ValueTask<IEnumerable<AssignmentOptions>> GetAllAssignmentOptions()
        {
            return await _repository.GetAllAssignmentOptions();
        }

        public async ValueTask<AssignmentOptions> UpdateAssignmentOption(AssignmentOptions entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return await _repository.UpdateCurrentAssignmentOptions(entity);
        }

        public async ValueTask<AssignmentOptions> DeleteAssignmentOption(int id)
        {
            var entity = await _repository.DeleteAssignmentOptions(id);

            if (entity == null)
                throw new EntityNotFoundException(
                    $"AssignmentOption with ID {id} not found");

            return entity;
        }

        /* ================= Education Level ================= */

        public async ValueTask<EducationLevelOptions> AddNewEducationLevelOption(EducationLevelOptions entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return await _repository.AddNewEducationLevelOptions(entity);
        }

        public async ValueTask<IEnumerable<EducationLevelOptions>> GetAllEducationLevelOptions()
        {
            return await _repository.GetAllEducationLevelOptions();
        }

        public async ValueTask<EducationLevelOptions> UpdateEducationLevelOption(EducationLevelOptions entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return await _repository.UpdateCurrentEducationLevelOptions(entity);
        }

        public async ValueTask<EducationLevelOptions> DeleteEducationLevelOption(int id)
        {
            var entity = await _repository.DeleteEducationLevelOptions(id);

            if (entity == null)
                throw new EntityNotFoundException(
                    $"EducationLevelOption with ID {id} not found");

            return entity;
        }

        public async ValueTask<MainSettings> AddNewMainSettings(MainSettings entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return await _repository.AddNewMainSettings(entity);
        }

        public async ValueTask<IEnumerable<MainSettings>> GetAllMainSettings()
        {
            return await _repository.GetAllMainSettings();
        }

        public async ValueTask<MainSettings> UpdateMainSettings(MainSettings entity)
        {
            if (entity == null)
            {
                _logger.LogError("Service: Attempted to update with null Main entity");
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                _logger.LogInformation("Service: Updating Main Entity with ID: {MainEntityId}", entity.Id);

                // Retrieve the existing entity (tracked by EF Core)
                var existingEntity = await _repository.GetMainSettings(entity.Id);

                // Update all relevant properties
                existingEntity.DepartmentName = entity.DepartmentName;
                existingEntity.DepartmentDirectorName = entity.DepartmentDirectorName;
                existingEntity.ConscriptsAffairsOfficerName = entity.ConscriptsAffairsOfficerName;
                existingEntity.ConscriptsAffairsPoliceManName = entity.ConscriptsAffairsPoliceManName;
             
                var updatedSoldier = await _repository.UpdateMainSettings(existingEntity);

                _logger.LogInformation("Service: Successfully updated MainEntity with ID: {MainEntityId}", existingEntity.Id);
                return updatedSoldier;
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Service: Validation failed for MainEntity ID: {MainEntityId}", entity.Id);
                throw;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Service: MainEntity with ID: {MainEntityId} not found", entity.Id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Error occurred while updating MainEntity with ID: {MainEntityId}", entity.Id);
                throw;
            }
        }

        public ValueTask<MainSettings> DeleteMainSettings(int id)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<MainSettings> GetMainSettings(int id)
        {
          return  await _repository.GetMainSettings(id);
        }

        public async ValueTask<string> GetMainSettingsByDepartmentName()
        {
            return await _repository.GetMainSettingsByDepartmentName();

        }

        public async ValueTask<string> GetMainSettingsByDepartmentDirectorName()
        {
            return await _repository.GetMainSettingsByDepartmentDirectorName();
            ;
        }

        public async ValueTask<string> GetMainSettingsByConscriptsAffairsPoliceManName()
        {
            return await _repository.GetMainSettingsByConscriptsAffairsPoliceManName();

        }

        public async ValueTask<string> GetMainSettingsByConscriptsAffairsOfficerName()
        {
            return await _repository.GetMainSettingsByConscriptsAffairsOfficerName();

        }
    }
}
