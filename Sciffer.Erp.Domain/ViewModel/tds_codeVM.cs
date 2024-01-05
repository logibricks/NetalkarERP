using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sciffer.Erp.Domain.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class tds_codeVM
    {
        [Key]
        public int tds_code_id { get; set; }
        [Display(Name = "TDS code *")]
        [Required(ErrorMessage = "TDS Code is required")]
        public string tds_code { get; set; }
        [Display(Name = "TDS Description *")]
        [Required(ErrorMessage = "TDS Code Description is required")]
        public string tds_code_description { get; set; }
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
        [Required(ErrorMessage = "Block is required")]
        public bool is_blocked { get; set; }
        public bool is_active { get; set; }
        public string deleteids { get; set; }
        public virtual List<ref_tds_code_detail> ref_tds_code_detail { get; set; }
    }
}
