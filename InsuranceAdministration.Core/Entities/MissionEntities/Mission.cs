using InsuranceAdministration.Core.Entities.DailyMissionEntities;
using InsuranceAdministration.Core.Entities.PoliceManEntities;
using InsuranceAdministration.Core.Entities.SoldierEntities;
using System.ComponentModel.DataAnnotations;

public class Mission
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    public int? CodeNumber { get; set; }

    public string? BoatNumber { get; set; }

    
    // رقم الجاهز اللاسلكي
    public string? WirelessCallSign { get; set; }
    public bool IsActive { get; set; } = true;
    public int? DailyMissionId { get; set; }
    public DailyMission? DailyMission { get; set; }

    // Many-to-Many
    public ICollection<PoliceMan>? Policemen { get; set; } = new List<PoliceMan>();
    public ICollection<Soldier>? Soldiers { get; set; } = new List<Soldier>();
}
