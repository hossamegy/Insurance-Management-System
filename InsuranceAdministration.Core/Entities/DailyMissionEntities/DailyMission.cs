using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAdministration.Core.Entities.DailyMissionEntities
{
    public class DailyMission
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public ICollection<Mission> Missions { get; set; } = new List<Mission>();
    }
}
