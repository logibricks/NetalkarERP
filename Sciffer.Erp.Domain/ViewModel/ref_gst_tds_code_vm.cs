using Sciffer.Erp.Domain.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class ref_gst_tds_code_vm
    {
        [Key]
        public int gst_tds_code_id { get; set; }
        [Display(Name = "GST TDS Code *")]
        [Required(ErrorMessage = "GST TDS Code is required")]
        public string gst_tds_code { get; set; }
        [Display(Name = "GST TDS Dscription *")]
        [Required(ErrorMessage = "GST TDS Dscription is required")]
        public string gst_tds_code_description { get; set; }
        [Display(Name = "Creditor GL *")]
        [Required(ErrorMessage = "Creditor GL is required")]
        public int creditor_gl { get; set; }
        public string creditor_gl_name { get; set; }
        [ForeignKey("creditor_gl")]
        public virtual ref_general_ledger ref_general_ledger { get; set; }
        [Display(Name = "Debtor GL *")]
        [Required(ErrorMessage = "Debtor GL is required")]
        public int debtor_gl { get; set; }
        public string debtor_gl_name { get; set; }
        [ForeignKey("debtor_gl")]
        public virtual ref_general_ledger ref_general_ledgerd { get; set; }
        [Display(Name = "Blocked")]       
        public bool is_blocked { get; set; }
        public bool? is_active { get; set; }
        public string deleteids { get; set; }
        public List<string> tds_code_detail_id { get; set; }
        public List<string> effective_from { get; set; }
        public List<string> rate { get; set; }
        public virtual List<ref_gst_tds_code_detail> ref_gst_tds_code_detail { get; set; }
    }
}
