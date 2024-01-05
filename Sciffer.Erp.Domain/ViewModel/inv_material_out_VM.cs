using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sciffer.Erp.Domain.Model;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
  public class inv_material_out_VM
    {
        
        [Key]
        public int material_out_id { get; set; }
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }
        public string document_number { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }

        public int vendor_id { get; set; }
        [ForeignKey("vendor_id")]
        public virtual REF_VENDOR REF_VENDOR { get; set; }
        public string contact_name { get; set; }
        public int business_unit_id { get; set; }
        [ForeignKey("business_unit_id")]
        public virtual REF_BUSINESS_UNIT REF_BUSINESS_UNIT { get; set; }

        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }

        public int storage_location_id { get; set; }
        [ForeignKey("storage_location_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }

       

        public string ge_number { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ge_date { get; set; }

        public string item_name { get; set; }
        public string bucket_name { get; set; }
        public string batch_number { get; set; }
        public string uom_name { get; set; }
        public string storage_location_name { get; set; }
        public string vendor_name { get; set; }
        public string plant_name { get; set; }
        public string busienss_unit { get; set; }
        public string employee_name { get; set; }
        public string company_name { get; set; }
        public string company_address { get; set; }
        public string posting_date11 { get; set; }
        public string ge_date11 { get; set; }
        public string status { get; set; }
        public int employee_id { get; set; }
        [ForeignKey("employee_id")]
        public virtual REF_EMPLOYEE REF_EMPLOYEE { get; set; }

        public string returnable_nonreturnable { get; set; }
        public string deleteids { get; set; }
        public List<string> material_out_detail_id { get; set; }
        public List<string> item_id { get; set; }
        public List<string> user_description { get; set; }
        public List<string> uom_id { get; set; }
        public List<string> batch_id { get; set; }
        public List<string> sloc_id { get; set; }
        public List<string> bucket_id { get; set; }
        public List<string> tax { get; set; }
        public List<string> hsn { get; set; }
        public List<string> quantity { get; set; }
        public List<string> rate { get; set; }
        public List<string> value { get; set; }
        public List<string> reason { get; set; }
        public List<string> er_date { get; set; }
        public List<string> Sr_No { get; set; }
        public List<string> balance_qty { get; set; }
        public List<string> remarks1 { get; set; }
        public string total_quantity { get; set; }
        public string BILLING_ADDRESS { get; set; }
        public string gst_number { get; set; }
       
        [Display(Name = "Remarks")]
        [DataType(DataType.MultilineText)]
        public string remarks { get; set; }
        public virtual List<inv_material_out_detail_VM> inv_material_out_detail_VM { get; set; }
        public virtual IList<inv_material_out_detail> inv_material_out_detail { get; set; }

        public string email_id_primary { get; set; }
        public string TELEPHONE_PRIMARY { get; set; }
        public string state_name { get; set; }
        public string gst_no { get; set; }
        public string plant_gst_number { get; set; }



        [Display(Name = "Internal Remarks")]
        [DataType(DataType.MultilineText)]
        public string internal_remarks { get; set; }
        [Display(Name = "Remarks On Document")]
        [DataType(DataType.MultilineText)]
        public string remarks_on_document { get; set; }

        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }
        public string attachement { get; set; }
        public string plant_email { get; set; }
        public string plant_mobile { get; set; }
        public decimal? net_value { get; set; }
        public decimal? gross_value { get; set; }
        public string state_code { get; set; }
        public string exp_dt_of_return { get; set; }
        public string VENDOR_CODE { get; set; }
        public int status_id { get; set; }
        public string plant_phone { get; set; }
    }


    public class inv_material_out_detail_VM
    {

        public int material_out_detail_id { get; set; }
        public int material_out_id { get; set; }
        public int? item_id { get; set; }
        public string ITEM_NAME { get; set; }
        public string user_description { get; set; }
        public int uom_id { get; set; }
        public string UOM_NAME { get; set; }
        public int batch_id { get; set; }
        public string batch_number { get; set; }
        public int sloc_id { get; set; }
        public string storage_location_name { get; set; }
        public int bucket_id { get; set; }
        public string bucket_name { get; set; }
        public double quantity { get; set; }
        public string reason { get; set; }
        public string er_date { get; set; }
        public double balance_qty { get; set; }
        [Display(Name = "Remarks")]
        [DataType(DataType.MultilineText)]
        public string remarks { get; set; }
        public string hsn_code { get; set; }
        public string uom_code { get; set; }
        public string type_of_goods { get; set; }
        public decimal? taxable_value { get; set; }
        public string tax_name { get; set; }
        public string cgst_value { get; set; }
        public string sgst_value { get; set; }
        public string igst_value { get; set; }
        public decimal? cgst_rate { get; set; }
        public decimal? sgst_rate { get; set; }
        public decimal? igst_rate { get; set; }
        public string ITEM_CATEGORY_NAME { get; set; }
    }

}
