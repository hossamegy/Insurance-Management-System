using InsuranceAdministration.Core.Entities.MissionEntities;
using InsuranceAdministration.Core.Exceptions;
using InsuranceAdministration.Core.Interfaces.Repository;
using InsuranceAdministration.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace InsuranceAdministration.Services
{
    public class MissionService : IMissionServices
    {
        private readonly IMissionRepository _repository;
        private readonly ILogger<MissionService> _logger;

        public MissionService(
            IMissionRepository repository,
            ILogger<MissionService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /* ================= ADD ================= */

        public async ValueTask<Mission> AddNewMission(Mission mission)
        {
            if (mission == null)
            {
                _logger.LogError("Service: Attempted to add null mission");
                throw new ArgumentNullException(nameof(mission));
            }

            try
            {
                _logger.LogInformation("Service: Adding new mission");

                var result = await _repository.AddNewMission(mission);

                _logger.LogInformation(
                    "Service: Successfully added mission with ID: {MissionId}",
                    result.Id);

                return result;
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Error occurred while adding mission");
                throw;
            }
        }

        /* ================= DELETE ================= */

        public async ValueTask<Mission> DeleteMission(int id)
        {
            try
            {
                _logger.LogInformation(
                    "Service: Deleting mission with ID: {MissionId}",
                    id);

                var mission = await _repository.DeleteMission(id);

                if (mission == null)
                    throw new EntityNotFoundException(
                        $"Mission with ID {id} not found");

                _logger.LogInformation(
                    "Service: Successfully deleted mission with ID: {MissionId}",
                    id);

                return mission;
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Service: Mission with ID {MissionId} not found",
                    id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Service: Error occurred while deleting mission with ID: {MissionId}",
                    id);
                throw;
            }
        }

        /* ================= GET ALL ================= */

        public async ValueTask<IEnumerable<Mission>> GetAllMissions()
        {
            try
            {
                _logger.LogInformation("Service: Retrieving all missions");

                var missions = await _repository.GetAllMissions();

                _logger.LogInformation("Service: Successfully retrieved missions");
                return missions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Error occurred while retrieving missions");
                throw;
            }
        }

        public async ValueTask<IEnumerable<Mission>> GetAllMissionsByActive(bool IsActive)
        {
            try
            {
                _logger.LogInformation(
                    "Service: Retrieving all missions with active status: {IsActive}",
                    IsActive);
                var missions = await _repository.GetAllMissionsByActive(IsActive);
                return missions;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Error occurred while retrieving missions by active status");
                throw;
            }
        }
      


        public async ValueTask<IEnumerable<Mission>> GetAllMissionsByActiveAndType(bool IsActive, int MissionType)
        {
            try
            {
                _logger.LogInformation(
                    "Service: Retrieving all missions with active status: {IsActive}",
                    IsActive);
                var missions = await _repository.GetAllMissionsByActiveAndType(IsActive, MissionType);
                return missions;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Error occurred while retrieving missions by active status");
                throw;
            }
        }
        /* ================= GET BY ID ================= */

        public async ValueTask<Mission> GetMission(int id)
        {
            try
            {
                _logger.LogInformation(
                    "Service: Retrieving mission with ID: {MissionId}",
                    id);

                var mission = await _repository.GetMission(id);

                if (mission == null)
                    throw new EntityNotFoundException(
                        $"Mission with ID {id} not found");

                _logger.LogInformation(
                    "Service: Successfully retrieved mission with ID: {MissionId}",
                    id);

                return mission;
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Service: Mission with ID {MissionId} not found",
                    id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Service: Error occurred while retrieving mission with ID: {MissionId}",
                    id);
                throw;
            }
        }

        /* ================= UPDATE ================= */

        public async ValueTask<Mission> UpdateCurrentMission(Mission mission)
        {
            if (mission == null)
            {
                _logger.LogError("Service: Attempted to update null mission");
                throw new ArgumentNullException(nameof(mission));
            }

            try
            {
                _logger.LogInformation(
                    "Service: Updating mission with ID: {MissionId}",
                    mission.Id);

                var existingMission = await _repository.GetMission(mission.Id);

                if (existingMission == null)
                    throw new EntityNotFoundException(
                        $"Mission with ID {mission.Id} not found");

                existingMission.MissionType = mission.MissionType;
                existingMission.Name = mission.Name;
                existingMission.BoatNumber = mission.BoatNumber;
                existingMission.CodeNumber = mission.CodeNumber;
                existingMission.WirelessCallSign = mission.WirelessCallSign;
                existingMission.IsActive = mission.IsActive;

               

                var updatedMission =
                    await _repository.UpdateCurrentMission(existingMission);

                _logger.LogInformation(
                    "Service: Successfully updated mission with ID: {MissionId}",
                    updatedMission.Id);

                return updatedMission;
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
                    "Service: Error occurred while updating mission with ID: {MissionId}",
                    mission.Id);
                throw;
            }

        }
        /* ================= DAILY MISSION - ADD ================= */

        public async ValueTask<DailyMission> AddNewDailyMission(DateTime date, IEnumerable<int> soldierMissionIds)
        {
            if (soldierMissionIds == null || !soldierMissionIds.Any())
            {
                _logger.LogError("Service: Attempted to add daily mission with null or empty soldier mission IDs");
                throw new ArgumentNullException(nameof(soldierMissionIds));
            }

            try
            {
                _logger.LogInformation("Service: Adding new daily mission for date {Date} with {Count} soldier missions",
                    date, soldierMissionIds.Count());

                var result = await _repository.AddNewDailyMission(date, soldierMissionIds);

                _logger.LogInformation("Service: Successfully added daily mission for date {Date}", date);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Error occurred while adding daily mission");
                throw;
            }
        }

        /* ================= DAILY MISSION - GET ================= */

        public async ValueTask<DailyMission?> GetDailyMission(int id)
        {
            try
            {
                _logger.LogInformation("Service: Retrieving daily mission with ID: {DailyMissionId}", id);

                var dailyMission = await _repository.GetDailyMission(id);

                if (dailyMission == null)
                    throw new EntityNotFoundException($"Daily mission with ID {id} not found");

                _logger.LogInformation("Service: Successfully retrieved daily mission with ID: {DailyMissionId}", id);

                return dailyMission;
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning(ex, "Service: Daily mission with ID {DailyMissionId} not found", id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Error occurred while retrieving daily mission with ID: {DailyMissionId}", id);
                throw;
            }
        }

        public async ValueTask<IEnumerable<Mission>> GetAllMissionsByType()
        {
            try
            {
                _logger.LogInformation("Service: Retrieving all missions ordered by type");

                var missions = await _repository.GetAllMissionsByType();

                _logger.LogInformation("Service: Successfully retrieved missions by type");

                return missions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Error occurred while retrieving missions by type");
                throw;
            }
        }

        public async ValueTask<IEnumerable<DailyMission>> GetAllDailyMissions()
        {
            try
            {
                _logger.LogInformation("Service: Retrieving all daily missions");

                var dailyMissions = await _repository.GetAllDailyMissions();

                _logger.LogInformation("Service: Successfully retrieved all daily missions");

                return dailyMissions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Error occurred while retrieving all daily missions");
                throw;
            }
        }

        /* ================= DAILY MISSION - DELETE ================= */

        public async ValueTask<DailyMission?> DeletDailyMission(int id)
        {
            try
            {
                _logger.LogInformation("Service: Deleting daily mission with ID: {DailyMissionId}", id);

                var dailyMission = await _repository.DeletDailyMission(id);

                if (dailyMission == null)
                    throw new EntityNotFoundException($"Daily mission with ID {id} not found");

                _logger.LogInformation("Service: Successfully deleted daily mission with ID: {DailyMissionId}", id);

                return dailyMission;
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning(ex, "Service: Daily mission with ID {DailyMissionId} not found", id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Error occurred while deleting daily mission with ID: {DailyMissionId}", id);
                throw;
            }
        }

        /* ================= DAILY MISSION - UPDATE ================= */

        public async ValueTask<DailyMission> UpdateCurrentDailyMission(DailyMission mission)
        {
            if (mission == null)
            {
                _logger.LogError("Service: Attempted to update null daily mission");
                throw new ArgumentNullException(nameof(mission));
            }

            try
            {
                _logger.LogInformation("Service: Updating daily mission with ID: {DailyMissionId}", mission.Id);

                var existingDailyMission = await _repository.GetDailyMission(mission.Id);

                if (existingDailyMission == null)
                    throw new EntityNotFoundException($"Daily mission with ID {mission.Id} not found");

                existingDailyMission.Date = mission.Date;

                var updatedDailyMission = await _repository.UpdateCurrentDailyMission(existingDailyMission);

                _logger.LogInformation("Service: Successfully updated daily mission with ID: {DailyMissionId}", updatedDailyMission.Id);

                return updatedDailyMission;
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
                _logger.LogError(ex, "Service: Error occurred while updating daily mission with ID: {DailyMissionId}", mission.Id);
                throw;
            }
        }

    }
}
