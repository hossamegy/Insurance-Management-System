
namespace InsuranceAdministration.Core.DTOs.Soldiers
{
    public class SoldierDailyMealDto
    {
        public int SoldierCounts { get; set; }
        public int SoldiersLeaveCounts { get; set; }
        public int SoldiersLeaveCountsMamoria { get; set; }
        public int SoldiersLeaveCountsIllness { get; set; }
        public int SoldiersLeaveCountsTraining { get; set; }
        public int SoldiersLeaveCountsEscape { get; set; }
        public int SoldiersLeaveCountsAbsence { get; set; }
        public int SoldiersLeaveCountsPrison { get; set; }
        public int SoldiersLeaveCountsReplacement { get; set; }
        public int SoldiersLeaveCountsStrikeOff { get; set; }
        public int AllCountLeaves { get; set; }
        public int SoldierAttendanceCounts { get; set; }
        public int Afrad { get; set; }
        public int GrandTotal { get; set; }
        public DateTime ReportDate { get; set; }
        public string Notes { get; set; }

        public List<DepartmentMealCount> DepartmentMeals { get; set; } = new List<DepartmentMealCount>();

        public void CalculateTotals()
        {
            AllCountLeaves = SoldiersLeaveCounts + SoldiersLeaveCountsMamoria +
                           SoldiersLeaveCountsIllness + SoldiersLeaveCountsTraining +
                           SoldiersLeaveCountsEscape + SoldiersLeaveCountsAbsence +
                           SoldiersLeaveCountsPrison + SoldiersLeaveCountsReplacement +
                           SoldiersLeaveCountsStrikeOff;

            SoldierAttendanceCounts = SoldierCounts - AllCountLeaves;

            int departmentMealsTotal = 0;
            foreach (var dept in DepartmentMeals)
            {
                departmentMealsTotal += dept.Count;
            }

            GrandTotal = SoldierAttendanceCounts + Afrad + departmentMealsTotal;
        }
    }

    public class DepartmentMealCount
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int Count { get; set; }
    }
}