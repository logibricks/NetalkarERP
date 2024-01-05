using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_ITEM
    {
        [Key]
        public int ITEM_ID { get; set; }
        [Display(Name = "Item Name")]
        public string ITEM_NAME { get; set; }
        [Display(Name = "Item Code")]
        public string ITEM_CODE { get; set; }
        public int ITEM_CATEGORY_ID { get; set; }
        [ForeignKey("ITEM_CATEGORY_ID")]
        public virtual REF_ITEM_CATEGORY REF_ITEM_CATEGORY { get; set; }
        public int ITEM_GROUP_ID { get; set; }
        [ForeignKey("ITEM_GROUP_ID")]
        public virtual REF_ITEM_GROUP REF_ITEM_GROUP { get; set; }
        public int? BRAND_ID { get; set; }        
        public int UOM_ID { get; set; }
        [ForeignKey("UOM_ID")]
        public virtual REF_UOM REF_UOM { get; set; }
        [Display(Name = "Item Length")]
        public double? ITEM_LENGTH { get; set; }
        public int? ITEM_LENGHT_UOM_ID { get; set; }
        [ForeignKey("ITEM_LENGHT_UOM_ID")]
        public virtual REF_UOM REF_UOM1 { get; set; }
        [Display(Name = "Item Width")]
        public double? ITEM_WIDTH { get; set; }
        public int? ITEM_WIDTH_UOM_ID { get; set; }
        [ForeignKey("ITEM_WIDTH_UOM_ID")]
        public virtual REF_UOM REF_UOM2 { get; set; }
        [Display(Name = "Item Height")]
        public double? ITEM_HEIGHT { get; set; }
        public int? ITEM_HEIGHT_UOM_ID { get; set; }
        [ForeignKey("ITEM_HEIGHT_UOM_ID")]
        public virtual REF_UOM REF_UOM3 { get; set; }
        [Display(Name = "Item Volume")]
        public double? ITEM_VOLUME { get; set; }
        public int? ITEM_VOLUME_UOM_ID { get; set; }
        [ForeignKey("ITEM_VOLUME_UOM_ID")]
        public virtual REF_UOM REF_UOM4 { get; set; }
        [Display(Name = "Item Weight")]
        public double? ITEM_WEIGHT { get; set; }
        public int? ITEM_WEIGHT_UOM_ID { get; set; }
        [ForeignKey("ITEM_WEIGHT_UOM_ID")]
        public virtual REF_UOM REF_UOM5 { get; set; }
        public int? PRIORITY_ID { get; set; }
        [ForeignKey("PRIORITY_ID")]
        public virtual REF_PRIORITY REF_PRIORITY { get; set; }
        [Display(Name = "IS Blocked")]
        public bool IS_BLOCKED { get; set; }
        [Display(Name = "Created ON")]
        public DateTime? CREATED_ON { get; set; }
        public int? CREATED_BY { get; set; }
        [ForeignKey("CREATED_BY")]
        public virtual REF_USER REF_USER { get; set; }
        [Display(Name = "Quality Managed")]
        public bool QUALITY_MANAGED { get; set; }
        [Display(Name = "Batch Managed")]
        public bool BATCH_MANAGED { get; set; }
        [Display(Name = "Self Life")]
        public int? SHELF_LIFE { get; set; }
        public int? SHELF_LIFE_UOM_ID { get; set; }
        [ForeignKey("SHELF_LIFE_UOM_ID")]
        public virtual REF_UOM REF_UOM6 { get; set; }
        public int? PREFERRED_VENDOR_ID { get; set; }      
        [Display(Name = "Vendor Part Number")]
        public string VENDOR_PART_NUMBER { get; set; }
        public bool MRP { get; set; }
        [Display(Name = "Minimum Level")]
        public int? MINIMUM_LEVEL { get; set; }
        [Display(Name = "Maximum Level")]
        public int? MAXIMUM_LEVEL { get; set; }
        [Display(Name = "Reorder Level")]
        public int? REORDER_LEVEL { get; set; }
        [Display(Name = "Reorder Quantity")]
        public double? REORDER_QUANTITY { get; set; }
        public int ITEM_VALUATION_ID { get; set; }
        [ForeignKey("ITEM_VALUATION_ID")]
        public virtual REF_ITEM_VALUATION REF_ITEM_VALUATION { get; set; }
        public int ITEM_ACCOUNTING_ID { get; set; }
        [ForeignKey("ITEM_ACCOUNTING_ID")]
        public virtual REF_ITEM_ACCOUNTING REF_ITEM_ACCOUNTING { get; set; }
        public int? EXCISE_CATEGORY_ID { get; set; }
        [ForeignKey("EXCISE_CATEGORY_ID")]
        public virtual REF_EXCISE_CATEGORY REF_EXCISE_CATEGORY { get; set; }
        [Display(Name = "HSN ")]
        public int? EXCISE_CHAPTER_NO { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Additional Information")]
        public string ADDITIONAL_INFORMATION { get; set; }
        public int? auto_batch { get; set; }
        public double? standard_cost_value { get; set; }
        [Display(Name = "Attachment")]
        public string Attachment { get; set; }
        public bool is_active { get; set; }
        public int? item_type_id { get; set; }
        [ForeignKey("item_type_id")]
        public virtual ref_item_type ref_item_type { get; set; }
        public bool? tag_managed { get; set; }
        public string user_description { get; set; }
        public int? gst_applicability_id { get; set; }
        public string rack_no { get; set; }
        public int? sac_id {get; set;}
        public virtual ICollection<ref_item_plant_valuation> ref_item_plant_valuation { get; set; }
        public virtual ICollection<ref_item_alternate_UOM> ref_item_alternate_UOM { get; set; }
        public virtual ICollection<ref_item_parameter> ref_item_parameter { get; set; }
    }
}
