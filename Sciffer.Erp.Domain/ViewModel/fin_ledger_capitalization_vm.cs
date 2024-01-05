using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
   public class fin_ledger_capitalization_vm
    {
        public int fin_ledger_capitalization_id { get; set; }
        [Display(Name = "Document Category *")]
        public int? document_numbering_id { get; set; }
        [Display(Name = "Document Number")]
        public string document_no { get; set; }
        [Display(Name = "Posting Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }
        [Display(Name = "Asset Code *")]
        public int? asset_code_id { get; set; }
        [Display(Name = "Capitalization Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime capitalization_date { get; set; }
        [Display(Name = "Select GL *")]
        public int? gl_ledger_id { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_ts { get; set; }
        public bool is_active { get; set; }
        public string category { get; set; }
        public string asset_code { get; set; }
        public string gl_ledger_code { get; set; }
        public virtual List<fin_ledger_capitalization_detail_vm> fin_ledger_capitalization_detail_vm { get; set; }
    }
}
