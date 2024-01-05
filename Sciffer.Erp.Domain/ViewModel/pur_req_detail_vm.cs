using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class pur_req_detail_vm
    {
        public int pur_requisition_detail_id { get; set; }
        public int pur_requisition_id { get; set; }
        public int uom_id { get; set; }
        public string uom_name { get; set; }
        public double? unit_price { get; set; }
        public double? info_price { get; set; }
        public double? net_value { get; set; }
        public int storage_location_id { get; set; }
        public string storage_location_name { get; set; }
        public int vendor_id { get; set; }
        public string vendor_name { get; set; }
        public string vendor_code { get; set; }
        public int item_id { get; set; }
        public string item_name { get; set; }
        public DateTime? delivery_date { get; set; }
        public double quantity { get; set; }
        public double balance_quantity { get; set; }
        public int? plant_id { get; set; }
        public int? freight_terms_id { get; set; }
        public int? business_unit_id { get; set; }
        public string plant_name { get; set; }
        public string business_unit_name { get; set; }
        public string freight_terms_name { get; set; }
        public int? hsn_id { get; set; }

        public double discount { get; set; }
        public string item_names { get; set; }
        public string item_code { get; set; }
       
        public string delivery_dates { get; set; }
        public List<hsn_sac> hsn_sac { get; set; }
        public string pur_requisition_date { get; set; }
        public string pur_requisition_number { get; set; }
        public string user_description { get; set; }
    }
}
