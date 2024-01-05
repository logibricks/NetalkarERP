using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_mfg_bom
    {
        [Key]        
        public int mfg_bom_id { get; set; }
        [Display(Name = "Bom Name")]
        public string mfg_bom_name { get; set; }
        [Required]
        [Display(Name = "Item")]
        public int out_item_id { get; set; }
        [ForeignKey("out_item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        
        [Display(Name = "Quantity")]
        public double mfg_bom_qty { get; set; }
        [Display(Name = "Remark")]
        public string remarks { get; set; }
        [Display(Name = "Bom No ")]

        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }

        [Display(Name = "Drawing No ")]
        public string drawing_no { get; set; }
        [Display(Name = "Version ")]
        public string version { get; set; }
        public string mfg_bom_no { get; set; }
        [Display(Name = "Created On ")]
        [Required(ErrorMessage = "date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime create_ts { get; set; }
        public bool is_blocked { get; set; }
        public virtual ICollection<ref_mfg_bom_detail> ref_mfg_bom_detail { get; set; }
    }
}
