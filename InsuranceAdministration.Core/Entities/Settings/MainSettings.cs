namespace InsuranceAdministration.Core.Entities.Settings
{
    public class MainSettings
    {
        public int Id { get; set; }

        // اسم الإدارة
        public string DepartmentName { get; set; } = "اسم الادارة";

        // الضابط مدير الإدارة
        public string DepartmentDirectorName { get; set; } = "اسم مدير الادارة";

        // الضابط المسئول عن شئون المجندين
        public string ConscriptsAffairsPoliceManName { get; set; } = "اسم ضابط الشئون المجندين";

        // أمين الشرطة (فرد شرطة) المسئول عن المجندين
        public string ConscriptsAffairsOfficerName { get; set; } = "اسم فرد شرطة شئون المجندين";
    }
}