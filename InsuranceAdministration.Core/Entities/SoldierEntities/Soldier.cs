using System.ComponentModel.DataAnnotations;

namespace InsuranceAdministration.Core.Entities.SoldierEntities
{
 
        public class Soldier
        {
            [Key]
            public int Id { get; set; }

            // الاسم
            [Required(ErrorMessage = "الاسم مطلوب")]
            [StringLength(150)]
            public string Name { get; set; }

            // تاريخ الميلاد
            [Required(ErrorMessage = "تاريخ الميلاد مطلوب")]
            [DataType(DataType.Date)]
            public DateTime BirthDate { get; set; }

            // تاريخ التجنيد
            [Required(ErrorMessage = "تاريخ التجنيد مطلوب")]
            [DataType(DataType.Date)]
            public DateTime EnlistmentDate { get; set; }

            // رقم الشرطة
            [Required(ErrorMessage = "رقم الشرطة مطلوب")]
            [StringLength(50)]
            public string? PoliceNumber { get; set; }

            [StringLength(50)]
            public string? Street { get; set; }

            [Required(ErrorMessage = "المركز مطلوب")]
            [StringLength(50)]
            public string? Region { get; set; }

            [Required(ErrorMessage = "المحافظة مطلوبة")]
            [StringLength(50)]
            public string? City { get; set; }

            // الرقم الثلاثي
            [Required(ErrorMessage = "الرقم الثلاثى مطلوب")]
            [StringLength(50)]
            public string TripleNumber { get; set; }

            // الرقم القومي
            [Required(ErrorMessage = "الرقم القومى مطلوب")]
            [StringLength(14, MinimumLength = 14, ErrorMessage = "الرقم القومي يجب أن يكون 14 رقم")]        
            public string NationalId { get; set; }

            // التشغيل
            [StringLength(100)]
            public string? Assignment { get; set; }
      
            [Required(ErrorMessage = "المؤهل الدراسى مطلوب")]
            // المؤهل الدراسي
            [StringLength(100)]
            public string? EducationLevel { get; set; }

            // رقم الهاتف
            [Phone]
            [StringLength(11, MinimumLength = 11, ErrorMessage = "رقم الهاتف يجب أن يكون 11 رقم")]
            public string? PhoneNumber { get; set; }

            // تاريخ انتهاء الخدمة العسكرية
            [DataType(DataType.Date)]
            public DateTime? ServiceEndDate { get; set; }

            public ICollection<Mission>? Missions { get; set; } = new List<Mission>();

    }
}
