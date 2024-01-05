using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class pur_requisition_detail
    {
        [Key]
        public int pur_requisition_detail_id { get; set; }
        public int pur_requisition_id { get; set; }
        [ForeignKey("pur_requisition_id")]
        public virtual pur_requisition pur_requisition { get; set; }
        public int sr_no { get; set; }
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public DateTime delivery_date { get; set; }
        public double quantity { get; set; }
        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }
        public double info_price { get; set; }
        public bool? is_short_close { get; set; }
        public bool? is_active { get; set; }
        public int storage_location_id { get; set; }
        [ForeignKey("storage_location_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }
        public int vendor_id { get; set; }
        [ForeignKey("vendor_id")]
        public virtual REF_VENDOR REF_VENDOR { get; set; }
        public bool order_status { get; set; }
        public int sac_hsn_id { get; set; }
        
       // public virtual ref_hsn_code ref_hsn_code { get; set; }
    }
    public class pur_requisition_detail_vm
    {
        public int pur_requisition_detail_id { get; set; }
        public int pur_requisition_id { get; set; }
        public int sr_no { get; set; }
        public int item_id { get; set; }
        public DateTime delivery_date { get; set; }
        public double quantity { get; set; }
        public int uom_id { get; set; }
        public double info_price { get; set; }
        public bool? is_short_close { get; set; }
        public bool? is_active { get; set; }
        public int storage_location_id { get; set; }
        public int vendor_id { get; set; }
        public bool order_status { get; set; }
        public int hsn_id { get; set; }
        public string hsn_code { get; set; }
        public string item_code { get; set; }
        public string uom_name { get; set; }
        public string storage_location_name { get; set; }
        public string vendor_name { get; set; }
        public int? item_type_id { get; set; }
        public string item_type_name { get; set; }
        public string pur_requisition_number { get; set; }
    }
}
