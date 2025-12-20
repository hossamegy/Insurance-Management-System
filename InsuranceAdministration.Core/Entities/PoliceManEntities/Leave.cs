using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace InsuranceAdministration.Core.Entities.PoliceManEntities
{
    public class Leave
    {
        [Key]
        public int Id { get; set; }

        // نوع الإجازة
        [Required]
        public LeaveType LeaveType { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        // FK
        public int PolicemanId { get; set; }

        [ForeignKey(nameof(PolicemanId))]
        public PoliceMan PoliceMan { get; set; }
    
    }
    public enum LeaveType
    {
        Emergency = 1,   // طارئة
        Sick = 2,        // مرضية
        Regular = 3     // عامة
    }
}
