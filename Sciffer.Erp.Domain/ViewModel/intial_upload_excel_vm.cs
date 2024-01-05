using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class intial_upload_excel_vm
    {
        public bool is_wdv_or_block { get; set; }
        public int sr_no { get; set; }
        public string asset_code { get; set; }
        public string dep_area { get; set; }
        public string capitalization_date { get; set; }
        public string original_cost { get; set; }
        public string  acc_depreciation { get; set; }
        public string net_value { get; set; }
        
        public string asset_class { get; set;}
        public string net_wdv_value { get; set; }
    }
}
