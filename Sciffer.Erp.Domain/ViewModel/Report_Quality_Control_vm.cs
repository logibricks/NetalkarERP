using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Quality_Control_vm
    {
        public string Operation { get; set; }
        public string Machine { get; set; }
        public string Item { get; set; }
        public string UOM { get; set; }
        public string Name { get; set; }
        public string From_Range { get; set; }
        public string To_Range { get; set; }

    }
}
