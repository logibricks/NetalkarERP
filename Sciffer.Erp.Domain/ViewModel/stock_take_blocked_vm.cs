using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class stock_take_blocked_vm
    {
        public int stock_take_blocked_id { get; set; }
        public int plant_id { get; set; }
        public int sloc_id { get; set; }
        public int bucket_id { get; set; }
        public bool is_blocked { get; set; }
        public int item_category_id { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_ts { get; set; }
        public int? modify_by { get; set; }
        public DateTime? modify_ts { get; set; }
        public string plant_name { get; set; }
        public string sloc_name { get; set; }
        public string bucket_name { get; set; }
        public string item_category_name { get; set; }
        public string created_name { get; set; }
        public string modify_name { get; set; }
    }
}
