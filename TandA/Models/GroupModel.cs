using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TandA.Models
{
    class GroupModel
    {
        public Int32 Id { get; set; }
        public String GroupRef { get; set; }
        public String GroupDescription { get; set; }
        public String SupervisorNo { get; set; }

        public GroupModel(Int32 p_Id, String p_GroupRef, String p_GroupDescription, String p_SupervisorNo)
        {
            Id = p_Id;
            GroupRef = p_GroupRef;
            GroupDescription = p_GroupDescription;
            SupervisorNo = p_SupervisorNo;
        }
    }
}
