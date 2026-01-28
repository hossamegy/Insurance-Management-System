
using InsuranceAdministration.Core.Entities.MissionEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceAdministration.Core.Entities.SoldierEntities
{
    public class SoldierMission
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SoldierId { get; set; }

        [ForeignKey(nameof(SoldierId))]
        public Soldier Soldier { get; set; }

        [Required]
        public int MissionId { get; set; }

        [ForeignKey(nameof(MissionId))]
        public Mission Mission { get; set; }

       
        [StringLength(1000, ErrorMessage = "الملاحظات يجب ألا تتجاوز 1000 حرف")]
        public string? Notes { get; set; }
        [Required]
        public DateTime AssignedAt { get; set; } = DateTime.Now;

        
        public int? DailyMissionId { get; set; }
        public DailyMission? DailyMission { get; set; }
    }
}
