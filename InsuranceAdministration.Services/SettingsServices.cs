using InsuranceAdministration.Core.Entities.Settings;
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

       
    }
}
