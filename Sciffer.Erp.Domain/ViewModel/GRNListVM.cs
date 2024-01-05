using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class GRNListVM
    {
        public int grn_detail_id { get; set; }
        public int grn_id { get; set; }
        public int item_id { get; set; }
        public int po_detail_id { get; set; }
        public DateTime delivery_date { get; set; }
        public int storage_location_id { get; set; }
        public double quantity { get; set; }
        public int uom_id { get; set; }
        public double unit_price { get; set; }
        public double discount { get; set; }
        public double eff_unit_price { get; set; }
        public double purchase_value { get; set; }
        public double assessable_rate { get; set; }
        public double assessable_value { get; set; }
        public int tax_id { get; set; }
        public string user_description { get; set; }
        public string sloc_name { get; set; }
        public string item_name { get; set; }
        public string UOM_NAME { get; set; }
        public string tax_code { get; set; }
        public string tax_name { get; set; }
        public double balance_quantity { get; set; }
    }
}
