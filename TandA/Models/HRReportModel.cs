using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TandA.Models
{
    class HRReportModel : ObservableObject
    {
        DataTable _HRR;
        String _GroupName;

        public DataTable HRR
        {
            get { return _HRR; }
            set
            {
                if (_HRR != value)
                {
                    _HRR = value;
                }
                RaisePropertyChanged("HRR");
            }
        }

        public String GroupName
        {
            get { return _GroupName; }
            set
            {
                if (_GroupName != value)
                {
                    _GroupName = value;
                }
                RaisePropertyChanged("GroupName");
            }
        }

        public HRReportModel(DataTable p_HRR, String p_GroupName)
        {
            _HRR = p_HRR;
            _GroupName = p_GroupName;
        }
    }
}
