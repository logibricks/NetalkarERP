using Sciffer.Erp.Domain.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class ItemVM
    {
        public int ITEM_ID { get; set; }
        public string ITEM_NAME { get; set; }
        public string ITEM_CODE { get; set; }
    }
    public class VendorVM1
    {
        public int VENDOR_ID { get; set; }
        public string VENDOR_NAME { get; set; }
        public string VENDOR_CODE { get; set; }
    }
    public class Item_Current_balance
    {
        public double quantity { get; set; }
        public double item_value { get; set; }

        public int gl_ledger_id { get; set; }
    }
    public class Ref_item_VM
    {
        public int ITEM_ID { get; set; }
        [Display(Name = "Item Description *")]
        public string ITEM_NAME { get; set; }
        [Display(Name = "Item Code *")]
        public string ITEM_CODE { get; set; }
        [Display(Name = "Item Category *")]
        public int ITEM_CATEGORY_ID { get; set; }
        [Display(Name = "Item Group *")]
        public int ITEM_GROUP_ID { get; set; }
        [Display(Name = "Brand")]
        public int? BRAND_ID { get; set; }
        [Display(Name = "UoM *")]
        public int UOM_ID { get; set; }
        [Display(Name = "Item Length")]
        public double? ITEM_LENGTH { get; set; }
        public int? ITEM_LENGHT_UOM_ID { get; set; }
        [Display(Name = "Item Width")]
        public double? ITEM_WIDTH { get; set; }
        public int? ITEM_WIDTH_UOM_ID { get; set; }
        [Display(Name = "Item Height")]
        public double? ITEM_HEIGHT { get; set; }
        public int? ITEM_HEIGHT_UOM_ID { get; set; }
        [Display(Name = "Item Volume")]
        public double? ITEM_VOLUME { get; set; }
        public int? ITEM_VOLUME_UOM_ID { get; set; }
        [Display(Name = "Item Weight")]
        public double? ITEM_WEIGHT { get; set; }
        public int? ITEM_WEIGHT_UOM_ID { get; set; }
        public int? PRIORITY_ID { get; set; }
        [Display(Name = "Blocked")]
        public bool IS_BLOCKED { get; set; }
        [Display(Name = "Created On")]
        public DateTime? CREATED_ON { get; set; }
        [Display(Name = "Created By *")]
        public int? CREATED_BY { get; set; }
        [Display(Name = "Quality Managed")]
        public bool QUALITY_MANAGED { get; set; }
        [Display(Name = "Batch Managed")]
        public bool BATCH_MANAGED { get; set; }
        [Display(Name = "Shelf Life *")]
        public int? SHELF_LIFE { get; set; }
        public int? SHELF_LIFE_UOM_ID { get; set; }
        public int? PREFERRED_VENDOR_ID { get; set; }

        [Display(Name = "MRP")]
        public bool MRP { get; set; }
        [Display(Name = "Minimum Level")]
        public int? MINIMUM_LEVEL { get; set; }
        [Display(Name = "Maximum Level")]
        public int? MAXIMUM_LEVEL { get; set; }
        [Display(Name = "Reorder Level")]
        public int? REORDER_LEVEL { get; set; }
        [Display(Name = "Reorder Quantity ")]
        public double? REORDER_QUANTITY { get; set; }
        [Display(Name = "Item valuation *")]
        public int ITEM_VALUATION_ID { get; set; }
        [Display(Name = "Item Accounting *")]
        public int ITEM_ACCOUNTING_ID { get; set; }
        [Display(Name = "Classification Category * ")]
        public int? EXCISE_CATEGORY_ID { get; set; }
        [Display(Name = "HSN *")]
        public int? EXCISE_CHAPTER_NO { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Addition Information")]
        public string ADDITIONAL_INFORMATION { get; set; }
        public bool is_active { get; set; }
        public int? auto_batch { get; set; }
        public double? standard_cost_value { get; set; }
        [Display(Name ="Tag Managed ")]
        public bool? tag_managed { get; set; }
        [Display(Name = "User Description *")]
        [DataType(DataType.MultilineText)]
        public string user_description { get; set; }
        [Display(Name = "Attachment")]
        public string Attachment { get; set; }

        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }

        public string parameterdetail { get; set; }
        public string ITEM_CATEGORY_NAME { get; set; }
        public string ITEM_GROUP_NAME { get; set; }
        public string BRAND_NAME { get; set; }
        public string UOM_NAME { get; set; }
        public string PRIORITY_NAME { get; set; }
        public string EXCISE_CATEGORY_NAME { get; set; }
        public string deleteids { get; set; }
        public string VENDOR_PART_NUMBER { get; set; }
        public string deleteparameter { get; set; }
        [Display(Name = "Item Type *")]
        public int? item_type_id { get; set; }
        public string ledgeraccounttype { get; set; }
        [ForeignKey("item_type_id")]
        public virtual ref_item_type ref_item_type { get; set; }
        [Display(Name = "GST Applicability *")]
        public int? gst_applicability_id { get; set; }
        [Display(Name = "SAC *")]
        public int? sac_id { get; set; }
        public string sac_name { get; set; }
        [Display(Name = "Rack No.")]
        public string rack_no { get; set; }
        public virtual List<ref_item_plant_valuation> ref_item_plant_valuation { get; set; }
        public virtual List<ref_item_alternate_UOM> ref_item_alternate_UOM { get; set; }
        public virtual List<ref_item_parameter> ref_item_parameter { get; set; }
        public List<String> plant_id { get; set; }
        public List<String> item_value { get; set; }
        public int? within_state_tax_id { get; set; }
        public int? inter_state_tax_id { get; set; }
        [Display(Name = "Child Item")]
        public int? child_item_id { get; set; }
        public string child_item_list_id { get; set; }
    }

    public class item_list
    {
        public int ITEM_ID { get; set; }
        [Display(Name = "Item Name")]
        public string ITEM_NAME { get; set; }
        [Display(Name = "Item Code")]
        public string ITEM_CODE { get; set; }
        public string vendor_name { get; set; }
        public int ITEM_CATEGORY_ID { get; set; }
        public int ITEM_GROUP_ID { get; set; }
        public int? BRAND_ID { get; set; }
        public int UOM_ID { get; set; }
        [Display(Name = "Item Length")]
        public double? ITEM_LENGTH { get; set; }
        public int? ITEM_LENGHT_UOM_ID { get; set; }
        [Display(Name = "Item Width")]
        public double? ITEM_WIDTH { get; set; }
        public int? ITEM_WIDTH_UOM_ID { get; set; }
        [Display(Name = "Item Height")]
        public double? ITEM_HEIGHT { get; set; }
        public int? ITEM_HEIGHT_UOM_ID { get; set; }
        [Display(Name = "Item Volume")]
        public double? ITEM_VOLUME { get; set; }
        public int? ITEM_VOLUME_UOM_ID { get; set; }
        [Display(Name = "Item Weight")]
        public double? ITEM_WEIGHT { get; set; }
        public int? ITEM_WEIGHT_UOM_ID { get; set; }
        public int? PRIORITY_ID { get; set; }
        [Display(Name = "Blocked")]
        public bool IS_BLOCKED { get; set; }
        [Display(Name = "Created On")]
        public DateTime? CREATED_ON { get; set; }
        [Display(Name = "Created By")]
        public int? CREATED_BY { get; set; }
        [Display(Name = "Quality Managed")]
        public bool QUALITY_MANAGED { get; set; }
        [Display(Name = "Batch Managed")]
        public bool BATCH_MANAGED { get; set; }
        [Display(Name = "Selef Life")]
        public int? SHELF_LIFE { get; set; }
        public int? SHELF_LIFE_UOM_ID { get; set; }
        public int? PREFERRED_VENDOR_ID { get; set; }

        [Display(Name = "MRP")]
        public bool MRP { get; set; }
        [Display(Name = "Minimum Level")]
        public int? MINIMUM_LEVEL { get; set; }
        [Display(Name = "Maximum Level")]
        public int? MAXIMUM_LEVEL { get; set; }
        [Display(Name = "Reorder Level")]
        public int? REORDER_LEVEL { get; set; }
        [Display(Name = "Reorder Quantity ")]
        public double? REORDER_QUANTITY { get; set; }
        public string deleteparameter { get; set; }
        public bool? tag_managed { get; set; }
        public int ITEM_VALUATION_ID { get; set; }
        public int ITEM_ACCOUNTING_ID { get; set; }
        public int EXCISE_CATEGORY_ID { get; set; }
        [Display(Name = "Excersize Chapter No")]
        public string EXCISE_CHAPTER_NO { get; set; }
        [Display(Name = "Addition Information")]
        public string ADDITIONAL_INFORMATION { get; set; }
        public bool is_active { get; set; }
        public string VENDOR_PART_NUMBER { get; set; }
        public string ITEM_CATEGORY_NAME { get; set; }
        public string ITEM_GROUP_NAME { get; set; }
        public string BRAND_NAME { get; set; }
        public string UOM_NAME { get; set; }
        public string PRIORITY_NAME { get; set; }
        public string EXCISE_CATEGORY_NAME { get; set; }
        public int item_type_id { get; set; }
        public int auto_batch { get; set; }
        public int standard_cost_value { get; set; }
        public string itemtype_name { get; set; }
        public string itemvaluation_name { get; set; }
        public string itemaccount_name { get; set; }
        public string hsncategory_name { get; set; }
        public string hsn_name { get; set; }
        public string user_description { get; set; }
        public string gst_applicability_name { get; set; }
        public string sac_name{ get; set; }
        public int? gst_applicability_id { get; set; }
        public int? sac_id { get; set; }
        public string rack_no { get; set; }
    }
    public class Uom_Excel
    {
        public int item_id { get; set; }
        public int uom_id { get; set; }
        public double conversion_rate { get; set; }
        public string itemCode { get; set; }
    }
    public class item_category_gl_Excel
    {
        public string itemCode { get; set; }
        public int item_category_id { get; set; }
        public int ledger_account_type_id { get; set; }
        public int gl_ledger_id { get; set; }
    }
    public class parameter_Excel
    {
        public string itemCode { get; set; }
        public int? parameter_id { get; set; }
        public string parameter_name { get; set; }
        public string parameter_range { get; set; }
    }

    public class duplicateGLExcel
    {
        public string itemCode { get; set; }
        //public int gl_ledger_id { get; set;}
    }
    public class ref_item_plant_vm
    {
        public int plant_id { get; set; }
        public string plant_name { get; set; }
        public double item_value { get; set; }
        public string itemCode { get; set; }
    }
    public class qality_batch
    {
        public string item_code { get; set; }
    }

    public class DataResult
    {
        public IEnumerable result { get; set; }
        public int count { get; set; }

    }
}
