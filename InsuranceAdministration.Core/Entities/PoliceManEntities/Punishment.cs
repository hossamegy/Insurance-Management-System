
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceAdministration.Core.Entities.PoliceManEntities
{
    public class Punishment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        // FK
        public int PoliceManId { get; set; }

        [ForeignKey(nameof(PoliceManId))]
        public PoliceMan PoliceMan { get; set; }
    }
}
