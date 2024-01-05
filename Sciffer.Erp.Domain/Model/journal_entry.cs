
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class journal_entry
    {
        [Key]
        public int journal_entry_id { get; set; }
        [Required(ErrorMessage ="doc number is required")]
        [Display(Name ="Doc Number")]
        public int journal_entry_doc_number { get; set; }
        [Required(ErrorMessage = "number is required")]
        [Display(Name = "Number")]
        public int journal_entry_number { get; set; }
        [Display(Name ="Referance")]
        [StringLength(50,ErrorMessage ="text is too long")]
        public string journal_entry_reference { get; set; }
        [Display(Name = "Remarks")]
        [StringLength(200, ErrorMessage = "text is too long")]
        public string journal_entry_remarks { get; set; }
        [Required(ErrorMessage ="date is required")]
        [DataType(DataType.Date)]
        [Display(Name ="Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime journal_entry_date { get; set; }
        [Required(ErrorMessage ="doc date is required")]
        [Display(Name ="Doc Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime journal_entry_posting_date { get; set; }
        public virtual ICollection<journal_entry_detail> journal_entry_item { get; set; }
        public bool journal_entry_is_active { get; set; }
    }
}
