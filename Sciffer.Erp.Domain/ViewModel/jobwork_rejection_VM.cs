using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class jobwork_rejection_VM
    {
        public int jobwork_rejection_id { get; set; }
        public string doc_number { get; set; }
        public int category_id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }
        public int bill_to_party { get; set; }
        public int ship_to_party { get; set; }
        public int place_of_supply { get; set; }
        public int place_of_delivery { get; set; }
        public int business_unit_id { get; set; }
        public int plant_id { get; set; }
        public int? freight_term_id { get; set; }
        public int? territory_id { get; set; }
        public int? sales_rm_id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? customer_po_date { get; set; }
        public string customer_po_number { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? removal_date { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan? removal_time { get; set; }
        public int sales_against_order { get; set; }
        [DataType(DataType.MultilineText)]
        public string internal_remarks { get; set; }
        [DataType(DataType.MultilineText)]
        public string remarks_on_doc { get; set; }
        public string attachment { get; set; }
        public bool is_active { get; set; }
        public string deleteids1 { get; set; }
        public string deleteids2 { get; set; }
        public string vehicle_no { get; set; }
        public string mode_of_transport { get; set; }
        public int reason_id { get; set; }
        public string reason { get; set; }
        public List<int> detail_id1 { get; set; }
        public List<int> job_work_detail_id { get; set; }
        public List<int> item_id1 { get; set; }
        public List<int> uom_id1 { get; set; }
        public List<int> batch_id1 { get; set; }
        public List<int> bucket_id1 { get; set; }
        public List<double> batch_bal_quantity { get; set; }
        public List<double> quantity1 { get; set; }
        public List<int> sloc_id1 { get; set; }
        public List<double> rate { get; set; }
        public List<double> value { get; set; }
        public List<int> detail_id2 { get; set; }
        public List<int> item_id2 { get; set; }
        public List<int> tag_id2 { get; set; }
        public List<double> quantity2 { get; set; }
        public string place_of_supply_name { get; set; }
        public string category_name { get; set; }
        public string place_of_delivery_name { get; set; }
        public string bill_to_party_name { get; set; }
        public string ship_to_party_name { get; set; }
        public string business_unit_id_name { get; set; }
        public string plant_name { get; set; }
        public string freight_term_id_name { get; set; }
        public string territory_id_name { get; set; }
        public string sales_rm_id_name { get; set; }
        public int created_by { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime created_ts { get; set; }
        public virtual List<jobwork_rejection_detail> jobwork_rejection_detail { get; set; }
        public virtual IList<jobwork_rejection_detail_tag> jobwork_rejection_detail_tag { get; set; }

        public string COMPANY_NAME { get; set; }
        public string address { get; set; }
        public double? phone { get; set; }
        public string email { get; set; }
        public string customer_po_date45 { get; set; }
        public string PAN_NO { get; set; }
        public string gst_number { get; set; }
        public string posting_date11 { get; set; }
        public string batch_number { get; set; }
        public string batch_date { get; set; }
        public string batch_item { get; set; }
        public decimal batch_quantity { get; set; }
        public decimal desp_quantity { get; set; }
        public decimal bal_quantity { get; set; }
        public string ch_sac_code { get; set; }
        public string billing_address { get; set; }
        public string customer_chalan_no { get; set; }
        public double? quantity { get; set; }
        public string uom_name { get; set; }
        public string sr_no { get; set; }
        public double? dispatch_qty { get; set; }
        public string item_name { get; set; }
        public double? balanace_qty { get; set; }
        public string registered_city { get; set; }

        public string plant_address { get; set; }
        public string plant_gst { get; set; }
    }
    public class jobwork_rejection_detail_VM
    {
        public int jobwork_rejection_id { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string cust_address { get; set; }
        public string state_ut_code { get; set; }
        public string state_name { get; set; }
        public string pan_no { get; set; }
        public string gst_no { get; set; }
        public string removal_date { get; set; }

        public TimeSpan? removal_time { get; set; }
        public string total_quantity { get; set; }
        public string place_of_supply { get; set; }
        public string CUSTOMER_CODE { get; set; }

        public double? total { get; set; }
        public string mode_of_transport { get; set; }
        public string vehical_no { get; set; }       

    }
    public class jobwork_rejection_item_detail_VM
    {
        public int jobwork_rejection_id { get; set; }
        public string item_name { get; set; }
        public string UOM_NAME { get; set; }
        public decimal quantity { get; set; }
        public string sac_code { get; set; }
        public string remarks_on_doc { get; set; }

        public decimal rate { get; set; }
        public decimal value { get; set; }
        public string batch_number { get; set; }
        public string batch_date { get; set; }
        public string batch_item { get; set; }
        public decimal batch_quantity { get; set; }
        public decimal desp_quantity { get; set; }
        public decimal bal_quantity { get; set; }
        public int jobwork_rejection_detail_id { get; set; }
        public string vendor_part_number { get; set; }

    }
}
