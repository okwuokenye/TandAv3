using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TandA.Models
{
    public class EmployeeGroupsModel : ObservableObject
    {

        private Boolean _BelongsToGroup = false;
        private String _GroupRef;

        public Boolean BelongsToGroup
        {
            get { return _BelongsToGroup; }
            set
            {
                if (_BelongsToGroup != value)
                {
                    _BelongsToGroup = value;
                    RaisePropertyChanged("BelongsToGroup");
                }
            }
        }

        public String GroupRef
        {
            get
            { return _GroupRef; }
            set
            {
                if (_GroupRef != value)
                {
                    _GroupRef = value;
                    RaisePropertyChanged("GroupRef");
                }
            }
        }

        public EmployeeGroupsModel(String p_GroupRef, Boolean p_BelongsToGroup)
        {
            GroupRef = p_GroupRef;
            BelongsToGroup = p_BelongsToGroup;
        }

    }
}
