using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class journal_entryVM
    {
        [Key]
        public int journal_entry_id { get; set; }
        [Required(ErrorMessage = "doc number is required")]
        [Display(Name = "Doc Number")]
        public int journal_entry_doc_number { get; set; }
        [Required(ErrorMessage = "number is required")]
        [Display(Name = "Number")]
        public int journal_entry_number { get; set; }
        [Display(Name = "Referance")]
        [StringLength(50, ErrorMessage = "text is too long")]
        public string journal_entry_reference { get; set; }
        [Display(Name = "Remarks")]
        [StringLength(200, ErrorMessage = "text is too long")]
        public string journal_entry_remarks { get; set; }
        [Required(ErrorMessage = "posting date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Posting Date *")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime journal_entry_date { get; set; }
        [Required(ErrorMessage = "doc date is required")]
        [Display(Name = "Document Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime journal_entry_posting_date { get; set; }
        public virtual List<journal_entry_detail> journal_entry_item { get; set; }
        public bool journal_entry_is_active { get; set; }
        public string deleteids { get; set; }
    }
}
