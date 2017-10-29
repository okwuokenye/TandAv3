using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TandA.Models
{
    public class EmployeeAbsenteeismModel
    {
        public Int32 Id { get; set; }
        public String EmployeeReference { get; set; }
        public String EmployeeName { get; set; }
        public DateTime DateAbsent { get; set; }
        public DateTime TimeAbsent { get; set; }
        public DateTime TimeTo { get; set; }
        public String AbsentRef { get; set; }
        public String Note { get; set; }

        public EmployeeAbsenteeismModel(Int32 p_Id, String p_EmployeeReference, String p_EmployeeName, DateTime p_DateAbsent, DateTime p_TimeAbsent, DateTime p_TimeTo, String p_AbsentRef, String p_Note)
        {
            Id = p_Id;
            EmployeeReference = p_EmployeeReference;
            EmployeeName = p_EmployeeName;
            DateAbsent = p_DateAbsent;
            TimeAbsent = p_TimeAbsent;
            TimeTo = p_TimeTo;
            AbsentRef = p_AbsentRef;
            Note = p_Note;
        }
    }
}
