using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class mfg_prod_order
    {
        [Key]
        public int prod_order_id { get; set; }
        public string prod_order_no { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime prod_order_date { get; set; }

        public int process_seq_alt_id { get; set; }
        [ForeignKey("process_seq_alt_id")]
        public virtual mfg_process_seq_alt mfg_process_seq_alt { get; set; }

        public string machine_seq { get; set; }

        public int out_item_id { get; set; }
        [ForeignKey("out_item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }

        public double quantity { get; set; }

        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }

        public int? created_by { get; set; }
        public DateTime? created_ts { get; set; }

        public int child_sloc_id { get; set; }
        [ForeignKey("child_sloc_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }

        public int parent_sloc_id { get; set; }
        [ForeignKey("parent_sloc_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION1 { get; set; }

        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }

        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }

        public int shift_id { get; set; }
        [ForeignKey("shift_id")]
        public virtual ref_shifts ref_shifts { get; set; }

        public string remarks { get; set; }
        public bool is_blocked { get; set; }
        public bool order_status { get; set; }
        public double balance_qty { get; set; }
        //public bool is_active { get; set; }
        
        public int? modified_by { get; set; }
        public DateTime? modified_ts { get; set; }

        public virtual ICollection<mfg_prod_order_detail> mfg_prod_order_detail { get; set; }
        public virtual ICollection<mfg_prod_order_bom> mfg_prod_order_bom { get; set; }
    }
}
