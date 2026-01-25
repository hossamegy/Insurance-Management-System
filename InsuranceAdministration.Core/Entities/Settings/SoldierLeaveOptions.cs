
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceAdministration.Core.Entities.Settings
{
    public class SoldierLeaveOptions
    {
        public int Id { get; set; }
        public string LeaveType { get; set; }
        public int? SettingsOptionsId { get; set; }

        [ForeignKey(nameof(SettingsOptionsId))]
        public SettingsOptions? SettingsOptions { get; set; }
    }
}
