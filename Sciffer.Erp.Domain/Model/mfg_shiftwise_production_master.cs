using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
  public  class mfg_shiftwise_production_master
    {
        [Key]
        public int shiftwise_production_id { get; set; }

        public int document_numbring_id { get; set; }
        [ForeignKey("document_numbring_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }

        public string document_no { get; set; }

        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }

        public int shift_id { get; set; }
        [ForeignKey("shift_id")]
        public virtual ref_shifts ref_shifts { get; set; }

        [Display(Name = "Posting Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }

        public int created_by { get; set; }

        public DateTime created_ts { get; set; }

        public int modified_by { get; set; }

        public DateTime modified_on { get; set; }

        public bool is_active { get; set; }

    }
}
