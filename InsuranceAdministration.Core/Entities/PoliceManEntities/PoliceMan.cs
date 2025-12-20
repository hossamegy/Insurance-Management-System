using System.ComponentModel.DataAnnotations;

namespace InsuranceAdministration.Core.Entities.PoliceManEntities
{     
        public class PoliceMan
        {
            [Key]
            public int Id { get; set; }

            // الاسم
            [Required]
            [StringLength(150)]
            public string Name { get; set; }

            // رقم الهاتف
            [Phone]
            [StringLength(20)]
            public string? PhoneNumber { get; set; }

            [Required(ErrorMessage ="مطلوب ادخل هذه الحقل")]
            public string HasChantDriverCertificate { get; set; }
            [StringLength(50)]
            public string? Street { get; set; }

            [Required(ErrorMessage = "المركز مطلوب")]
            [StringLength(50)]
            public string Region { get; set; }

            [Required(ErrorMessage = "المحافظة مطلوبة")]
            [StringLength(50)]
            public string City { get; set; }

            [Required(ErrorMessage = "ادخل اسم الدورية")]
            public string GroupName { get; set; }
    
            // العلاقات
            public ICollection<Punishment>? Punishments { get; set; } = new List<Punishment>();
            public ICollection<Leave>? Leaves { get; set; } = new List<Leave>();

            public ICollection<Mission>? Missions { get; set; } = new List<Mission>();
    }      
 }
