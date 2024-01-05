using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class plan_maintenance_order_parameter_cost_new_vm
    {
        public int maintenance_order_cost_id { get; set; }
        public int item_id { get; set; }
        public int plan_maintenance_order_id { get; set; }
        public double quantity { get; set; }
        public double? actual_quantity { get; set; }
        public int? sloc_id { get; set; }
        public int? bucket_id { get; set; }
        public double? value { get; set; }
        public string doc_number { get; set; }
        public int parameter_id { get; set; }
        public DateTime? posting_date { get; set; }
        public bool is_active { get; set; }
        public int plan_maintenance_component_id { get; set; }
        public string plan_type { get; set; }
        public int sr_no { get; set; }
        public int uom_id { get; set; }

        public string parameter_code { get; set; }
        public string storage_code { get; set; }
        public string bucket { get; set; }
        public string ItemName { get; set; }
        public string UOM_NAME { get; set; }
    }
}