using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class pur_po_detail
    {
        [Key]
        public int? po_detail_id { get; set; }
        [Display(Name ="Sr No.")]
        public int sr_no { get; set; }
        public int po_id { get; set; }
        [ForeignKey("po_id")]
        public virtual pur_po pur_po { get; set; }
        public int sloc_id { get; set; }         
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        [Required(ErrorMessage ="delivery date is required")]
        [Display(Name ="Delivery Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime delivery_date { get; set; }
        [Required(ErrorMessage = "quantity is required")]
        [Display(Name = "Quantity")]
        public double quantity { get; set; }
        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }
        [Required(ErrorMessage = "unit price is required")]
        [Display(Name = "Unit Price")]
        public double unit_price { get; set; }
        [Required(ErrorMessage = "discount is required")]
        [Display(Name = "Discount")]
        public double discount { get; set; }
        [Required(ErrorMessage = "eff unit price is required")]
        [Display(Name = "Eff Unite Price")]
        public double eff_unit_price { get; set; }
        [Required(ErrorMessage = "purchase value is required")]
        [Display(Name = "Purchase Value")]
        public double purchase_value { get; set; }
        [Required(ErrorMessage = "assessable rate is required")]
        [Display(Name = "Assessable Rate")]
        public double assesable_rate { get; set; }
        [Required(ErrorMessage = "assessable value is required")]
        [Display(Name = "Assessable Value")]
        public double assessable_value { get; set; }      
        public int tax_code_id { get; set; }
        [ForeignKey("tax_code_id")]
        public virtual ref_tax ref_tax { get; set; }
        public bool is_active { get; set; }
        public string user_description { get; set; }
        public int pur_requisition_detail_id { get; set; }
        public double? maximum_limit_qty { get; set; }
        public DateTime? valid_from_date { get; set; }
        public DateTime? valid_to_date { get; set; }
        public int sac_hsn_id { get; set; }
        [ForeignKey("sac_hsn_id")]
        public virtual ref_hsn_code ref_hsn_code { get; set; }
        public int item_type_id { get; set; }
    }
}
