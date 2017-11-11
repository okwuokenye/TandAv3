using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TandA.Models
{
    public class PunchesModel
    {
        public Int32 ID { get; set; }
        public Int32 PeriodId { get; set; }
        public DateTime PunchDate { get; set; }
        public String sPunchDate { get; set; }
        public String PunchTime { get; set; } //approx. time
        public String ActualTime { get; set; }
        public String PunchType { get; set; }
        public String EmployeeRef { get; set; }

        public PunchesModel(Int32 p_ID, Int32 p_PeriodId, DateTime p_PunchDate, String p_PunchTime, String p_PunchType, String p_EmployeeRef)
        {
            ID = p_ID;
            PeriodId = p_PeriodId;
            PunchDate = p_PunchDate;
            sPunchDate = p_PunchDate.Date.ToShortDateString();
            PunchTime = p_PunchTime;
            PunchType = p_PunchType;
            EmployeeRef = p_EmployeeRef;
            ActualTime = p_PunchDate.Hour + ":" + p_PunchDate.Minute;
        }
    }
}
