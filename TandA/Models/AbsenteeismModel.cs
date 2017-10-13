using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TandA.Models
{
    class AbsenteeismModel
    {
        public String Reference { get; set; }
        public String Description { get; set; }
        public String Abbreviation { get; set; }

        public AbsenteeismModel(String p_Reference, String p_Description, String p_Abbreviation)
        {
            Reference = p_Reference;
            Description = p_Description;
            Abbreviation = p_Abbreviation;
        }
    }
}
