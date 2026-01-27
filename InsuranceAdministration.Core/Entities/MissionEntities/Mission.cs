using InsuranceAdministration.Core.Entities.MissionEntities;
using InsuranceAdministration.Core.Entities.PoliceManEntities;
using InsuranceAdministration.Core.Entities.SoldierEntities;
using System.ComponentModel.DataAnnotations;

public class Mission
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int MissionType { get; set; }
    [Required]
    public string Name { get; set; }
    public int? CodeNumber { get; set; }

    public string? BoatNumber { get; set; }

    
    // رقم الجاهز اللاسلكي
    public string? WirelessCallSign { get; set; }
    public bool IsActive { get; set; } = true;

    public DateTime DateTime { get; set; } = DateTime.Now;
 
    // Many-to-Many
    public ICollection<PoliceMan>? Policemen { get; set; } = new List<PoliceMan>();
    public ICollection<SoldierMission>? SoldierMissions { get; set; } = new List<SoldierMission>();

}
