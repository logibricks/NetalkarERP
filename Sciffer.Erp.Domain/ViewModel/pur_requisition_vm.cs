using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class pur_requisition_vm
    {

        public int pur_requisition_id { get; set; }
        public int category_id { get; set; }
        [Display(Name = "PR Number *")]
        public string pur_requisition_number { get; set; }
        [Display(Name = "Posting Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime pur_requisition_date { get; set; }
        [Display(Name = "Business Unit *")]
        public int business_unit_id { get; set; }
        [ForeignKey("business_unit_id")]
        public virtual REF_BUSINESS_UNIT REF_BRAND { get; set; }
        [Display(Name = "Plant *")]
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        [Display(Name = "Delivery Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? delivery_date { get; set; }
        [Display(Name = "Source *")]
        public int source_id { get; set; }
        [ForeignKey("source_id")]
        public virtual REF_SOURCE REF_SOURCE { get; set; }
        [Display(Name = "Created By")]
        public int created_by { get; set; }
        [Display(Name = "Created On")]
        public DateTime created_on { get; set; }
        [Display(Name = "Internal Remarks")]
        [DataType(DataType.MultilineText)]
        public string internal_remarks { get; set; }
        [Display(Name = "Remarks on Document")]
        [DataType(DataType.MultilineText)]
        public string remarks { get; set; }
        public int? item_id { get; set; }
        public string deleteids { get; set; }
        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }
        public string attachement { get; set; }
        [Display(Name = "PR Status *")]
        public int status_id { get; set; }
        [ForeignKey("status_id")]
        public virtual ref_status ref_status { get; set; }
        public virtual List<pur_requisition_detail> pur_requisition_detail { get; set; }
        public  List<pur_requisition_detail_vm> pur_requisition_detail_vm { get; set; }
        public bool order_status { get; set; }
        public List<string> pur_requisition_detail_id { get; set; }
        public List<string> item_id1 { get; set; }
        public List<string> delivery_date1 { get; set; }
        public List<string> quantity { get; set; }
        public List<string> uom_id { get; set; }
        public List<string> info_price { get; set; }
        public List<string> storage_location_id1 { get; set; }
        public List<string> vendor_id1 { get; set; }
        public List<string> item_service_id1 { get; set; }
       
        public List<int> sac_hsn_id { get; set; }
        public string business_unit_name { get; set; }
        public string status { get; set; }
        public string plant_name { get; set; }
        public string source_name { get; set; }
        public string pr_date { get; set; }
        public string item_name { get; set; }
        public string vendor_name { get; set; }
        [Display(Name = "Approval Status")]
        public int? approval_status { get; set; }
        public string approval_status_name { get; set; }
        public string approval_comments { get; set; }
        public int? approved_by { get; set; }
        public string category { get; set; }
        [Display(Name = "Responsibility")]
        public int? responsibility_id { get; set; }
        public string responsibility { get; set; }
        public DateTime? approved_ts { get; set; }
        public string closed_remarks { get; set; }
        public int? closed_by { get; set; }
        public DateTime? closed_ts { get; set; }
        public string approved_user { get; set; }
        public string closed_user { get; set; }
     
        public string created_user { get; set; }
        public string modify_user { get; set; }
        public DateTime? created_ts { get; set; }
        public string status_name { get; set; }
    }
    public class pur_req_stock
    {
        public string plant_name { get; set; }
        public string sloc_name { get; set; }
        public double cu_stock { get; set; }
    }
    public class pur_req_report_vm
    {
        public string companyname { get; set; }
        public string plantname { get; set; }
        public string plantaddress { get; set; }
        public string doccategory { get; set; }
        public string docnum { get; set; }
        public string postingdate { get; set; }
        public string purreqsource { get; set; }
        public int purreqid { get; set; }
        public DateTime? posting_date { get; set; }
        public decimal total { get; set; }
        public string gst_no { get; set; }
        public string plant_mobile { get; set; }
        public string plant_email { get; set; }
        public string remarks { get; set; }
        //public string internal_remarks { get; set; }
        public int? approval_status { get; set; }
        public string PLANT_CODE { get; set; }
    }
    public class pur_req_report_detail_vm
    {
        public int purreqid { get; set; }
        public string Itmcode { get; set; }
        public string itemdesc { get; set; }
        public decimal Qty { get; set; }
        public string Deldate { get; set; }
        public string prefvendor { get; set; }
        public string sloc { get; set; }
        public decimal rate { get; set; }
        public int purdetailid { get; set; }
        public DateTime? delivery_date { get; set; }
        public string user_description { get; set; }
        public string UOM_NAME { get; set; }
    }
}
