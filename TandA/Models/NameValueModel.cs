using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TandA.Models
{
    class NameValueModel
    {
        public string DisplayValue { get; set; }
        public string ActualValue { get; set; }

        public NameValueModel(string p_DisplayValue, string p_ActualValue)
        {
            DisplayValue = p_DisplayValue;
            ActualValue = p_ActualValue;
        }
    }
}
