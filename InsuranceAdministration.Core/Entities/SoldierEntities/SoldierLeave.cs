

using System.ComponentModel.DataAnnotations;

namespace InsuranceAdministration.Core.Entities.SoldierEntities
{
    public class SoldierLeave
    {
        public int Id { set; get; }
        public string? Type { set; get; }
        public int? StartNum { set; get; }
        public int? StartPage { set; get; }
        public DateTime? Start { set; get; }

        public int? EndNum { set; get; }
        public int? EndPage { set; get; }
        public DateTime? End { set; get; }
        [Required(ErrorMessage = "معرف المجند مطلوب")]
        public int SoldierId { get; set; }

        // Make navigation property nullable
        public Soldier? Soldier { get; set; }

    }
}
