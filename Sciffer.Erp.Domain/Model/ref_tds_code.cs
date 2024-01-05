using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_tds_code
    {
        [Key]
        public int tds_code_id { get; set; }
        [Display(Name = "TDS code *")]
        [Required(ErrorMessage ="TDS Code is required")]
        public string tds_code { get; set; }
        [Display(Name = "TDS Code Description *")]
        [Required(ErrorMessage = "TDS Code Description is required")]
        public string tds_code_description { get; set; }
        [Display(Name = "Creditor GL *")]
        [Required(ErrorMessage = "Creditor GL is required")]
        public int creditor_gl { get; set; }
        [ForeignKey("creditor_gl")]
        public virtual ref_general_ledger ref_general_ledger { get; set; }
        [Display(Name = "Debtor GL *")]
        [Required(ErrorMessage = "Debtor GL is required")]
        public int debtor_gl { get; set; }
        [ForeignKey("debtor_gl")]
        public virtual ref_general_ledger ref_general_ledgerd { get; set; }
        [Display(Name = "Block *")]
        [Required(ErrorMessage = "Block is required")]
        public bool is_blocked { get; set; }
        public bool is_active { get; set; }
        public virtual ICollection<ref_tds_code_detail> ref_tds_code_detail { get; set; }
    }
}
