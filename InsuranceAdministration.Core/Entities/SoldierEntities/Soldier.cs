using System.ComponentModel.DataAnnotations;

namespace InsuranceAdministration.Core.Entities.SoldierEntities
{
    public class Soldier
    {
        [Key]
        public int Id { get; set; }

        // الاسم
        [Required(ErrorMessage = "الاسم مطلوب")]
        [StringLength(150, ErrorMessage = "الاسم يجب ألا يتجاوز 150 حرف")]
        [RegularExpression(@"^[\u0600-\u06FFa-zA-Z\s]+$", ErrorMessage = "الاسم يجب أن يحتوي على حروف عربية أو إنجليزية فقط")]
        public string Name { get; set; }

        // تاريخ الميلاد
        [Required(ErrorMessage = "تاريخ الميلاد مطلوب")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        //الحالة الاجتماعية 
        [Required(ErrorMessage = "الحالة الاجتماعية مطلوبة")]
        [StringLength(150, ErrorMessage = "الحالة الاجتماعية يجب ألا تتجاوز 150 حرف")]
        public string MaritalStatus { get; set; }

        // تاريخ التجنيد
        [Required(ErrorMessage = "تاريخ التجنيد مطلوب")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnlistmentDate { get; set; }

        // رقم الشرطة
        [Required(ErrorMessage = "رقم الشرطة مطلوب")]
        [StringLength(50, ErrorMessage = "رقم الشرطة يجب ألا يتجاوز 50 حرف")]
        public string PoliceNumber { get; set; }

        [StringLength(50, ErrorMessage = "البلد يجب ألا يتجاوز 50 حرف")]
        public string? Street { get; set; }

        [Required(ErrorMessage = "المركز مطلوب")]
        [StringLength(50, ErrorMessage = "المركز يجب ألا يتجاوز 50 حرف")]
        public string Region { get; set; }

        [Required(ErrorMessage = "المحافظة مطلوبة")]
        [StringLength(50, ErrorMessage = "المحافظة يجب ألا تتجاوز 50 حرف")]
        public string City { get; set; }

        // الرقم الثلاثي
        [Required(ErrorMessage = "الرقم الثلاثي مطلوب")]
        [StringLength(50, ErrorMessage = "الرقم الثلاثي يجب ألا يتجاوز 50 حرف")]
        public string TripleNumber { get; set; }

        // الرقم القومي
        [Required(ErrorMessage = "الرقم القومي مطلوب")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "الرقم القومي يجب أن يكون 14 رقم بالضبط")]
        [RegularExpression(@"^[0-9]{14}$", ErrorMessage = "الرقم القومي يجب أن يحتوي على 14 رقم فقط بدون مسافات أو حروف")]
        public string NationalId { get; set; }

        // التشغيل
        [StringLength(100, ErrorMessage = "التشغيل يجب ألا يتجاوز 100 حرف")]
        public string? Assignment { get; set; }

        // الوظيفة
        [StringLength(250, ErrorMessage = "الوظيفة يجب ألا تتجاوز 250 حرف")]
        public string? Job { get; set; }

        [Required(ErrorMessage = "المؤهل الدراسي مطلوب")]
        [StringLength(250, ErrorMessage = "المؤهل الدراسي يجب ألا يتجاوز 250 حرف")]
        public string EducationLevel { get; set; }

        // رقم الهاتف - Optional but with validation if provided
        [StringLength(11, MinimumLength = 11, ErrorMessage = "رقم الهاتف يجب أن يكون 11 رقم بالضبط")]
        [RegularExpression(@"^(010|011|012|015)[0-9]{8}$", ErrorMessage = "رقم الهاتف يجب أن يبدأ بـ 010 أو 011 أو 012 أو 015 ويتبعه 8 أرقام")]
        public string? PhoneNumber { get; set; }

        public bool IsActive { get; set; } = true;

        public bool CurrentIsLeave { get; set; } = false;
        public DateTime? ServiceEndDate { get; set; }

        public AcquaintanceDocument? AcquaintanceDocument { get; set; }
        public PoliticalAndCriminal? PoliticalAndCriminal { get; set; }
        public ICollection<Training>? Trainings { get; set; }

        // تاريخ انتهاء الخدمة العسكرية
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public ICollection<SoldierLeave>? Leaves { get; set; }
        public ICollection<Mission>? Missions { get; set; } = new List<Mission>();
    }
}