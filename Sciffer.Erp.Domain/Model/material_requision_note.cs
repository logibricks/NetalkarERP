using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class material_requision_note
    {
        [Key]
        public int material_requision_note_id { get; set; }
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }
        public string number { get; set; }
        public DateTime? document_date { get; set; }
        public int? requirement_by { get; set; }
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        public string remarks { get; set; }
        public int receiving_sloc { get; set; }
        [ForeignKey("receiving_sloc")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION_receiving { get; set; }
        public int sending_sloc { get; set; }
        [ForeignKey("sending_sloc")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION_sending { get; set; }
        public int? type { get; set; }
        public int status_id { get; set; }
        [ForeignKey("status_id")]
        public virtual ref_status ref_status { get; set; }
        public bool is_active { get; set; }
        public bool? is_seen { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }
        public int? approval_status { get; set; }
        public string approval_comments { get; set; }
        public int? approved_by { get; set; }
        public virtual ICollection<material_requision_note_detail> material_requision_note_detail { get; set; }
    }
}
