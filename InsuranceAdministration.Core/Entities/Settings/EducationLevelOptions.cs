using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceAdministration.Core.Entities.Settings
{
    public class EducationLevelOptions
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? SettingsOptionsId { get; set; }

        [ForeignKey(nameof(SettingsOptionsId))]
        public SettingsOptions? SettingsOptions { get; set; }
    }
}
