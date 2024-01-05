using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_document_numbring
    {
        [Key]
        public int document_numbring_id { get; set; }
        [Display(Name = "Module")]
        public int module_id { get; set; }
        [ForeignKey("module_id")]
        public virtual ref_module ref_module { get; set; }
        [Display(Name = "Form")]
        public int module_form_id { get; set; }
        [ForeignKey("module_form_id")]
        public virtual ref_module_form ref_module_form { get; set; }
        public int financial_year_id { get; set; }
        [ForeignKey("financial_year_id")]
        public virtual REF_FINANCIAL_YEAR REF_FINANCIAL_YEAR { get; set; }
        public string category { get; set; }    
        public int prefix_sufix_id { get; set; }      
        public string prefix_sufix { get; set; }
        [Display(Name = "From Number")]
        public string from_number { get; set; }
        [Display(Name = "To Number")]
        public string to_number { get; set; }
        [Display(Name = "Current Number")]
        public string current_number { get; set; }
        [Display(Name = "Set Default")]
        public bool set_default { get; set; }
        [Display(Name ="Blocked")]
        public bool is_blocked { get; set; }
        public int plant_id { get; set; }
    }
    public class document_numbring
    {
        public int document_numbring_id { get; set; }
        public int module_id { get; set; }
        public string module_name { get; set; }
        public int module_form_id { get; set; }
        public string form_name { get; set; }
        public int financial_year_id { get; set; }
        public string financial_year { get; set; }
        public string category { get; set; }
        public int prefix_sufix_id { get; set; }
        public string prefix_sufix_name { get; set; }
        public string prefix_sufix { get; set; }
        public string from_number { get; set; }
        public string to_number { get; set; }
        public string current_number { get; set; }
        public bool set_default { get; set; }
        public bool is_blocked { get; set; }
        public int plant_id { get; set; }
        public string plant_name { get; set; }
    }
}
