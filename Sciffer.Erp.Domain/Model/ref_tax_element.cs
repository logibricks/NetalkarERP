using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_tax_element
    {
        [Key]
        public int tax_element_id { get; set; }
        [Display(Name = "Tax Element Code")]
        [Required(ErrorMessage = "tax element code is required")]
        public string tax_element_code { get; set; }
        [Display(Name = "Tax Element Description ")]
        [Required(ErrorMessage = "tax element description is required")]
        public string tax_element_description { get; set; }
        [Display(Name = "Purchase GL")]
        [Required(ErrorMessage = "purchase GL is required")]
        public int purchase_gl { get; set; }
        [ForeignKey("purchase_gl")]
        public virtual ref_general_ledger ref_general_ledger { get; set; }
        [Display(Name = "Sales GL")]
        [Required(ErrorMessage = "sales GL is required")]
        public int sales_gl { get; set; }
        [ForeignKey("sales_gl")]
        public virtual ref_general_ledger ref_general_ledgers { get; set; }
        [Display(Name = "Tax Type")]
        [Required(ErrorMessage = "tax type is required")]
        public int tax_type_id { get; set; }
        [ForeignKey("tax_type_id")]
        public virtual ref_tax_type ref_tax_type { get; set; }
        public bool is_active { get; set; }
        [Display(Name = "Block")]
        [Required(ErrorMessage = "blocked is required")]
        public bool is_blocked { get; set; }
        public virtual ICollection<ref_tax_element_detail> ref_tax_element_detail { get; set; }
        [Display(Name = "No Setoff GL")]
        public int? no_setoff_gl { get; set; }

        [Display(Name = "On Hold GL")]
        public int? on_hold_gl { get; set; }
        public bool is_rcm { get; set; }     
        public int? rcm_asset_gl { get; set; }       
        public int? rcm_liability_gl { get; set; }
    }
}
