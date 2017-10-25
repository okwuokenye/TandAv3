using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TandA.Models
{
    public class PeriodModel
    {
        public Int32 ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Int32 WeekNumber { get; set; }

        public PeriodModel(Int32 p_ID, DateTime p_StartDate, DateTime p_EndDate, Int32 p_WeekNumber)
        {
            ID = p_ID;
            StartDate = p_StartDate;
            EndDate = p_EndDate;
            WeekNumber = p_WeekNumber;
        }
    }
}
