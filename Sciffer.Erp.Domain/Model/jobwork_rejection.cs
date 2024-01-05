using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class jobwork_rejection
    {
        [Key]
        public int jobwork_rejection_id { get; set; }
        public string doc_number { get; set; }
        public int category_id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }
        public int bill_to_party { get; set; }
        public int ship_to_party { get; set; }
        public int place_of_supply { get; set; }
        public int place_of_delivery { get; set; }
        public int business_unit_id { get; set; }
        public int plant_id { get; set; }
        public int freight_term_id { get; set; }
        public int territory_id { get; set; }
        public int sales_rm_id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? customer_po_date { get; set; }
        public string customer_po_number { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? removal_date { get; set; }
        public TimeSpan? removal_time { get; set; }
        public int sales_against_order { get; set; }
        public string internal_remarks { get; set; }
        public string remarks_on_doc { get; set; }
        public string attachment { get; set; }
        public bool is_active { get; set; }
        public string vehicle_no { get; set; }
        public string mode_of_transport { get; set; }
        public int reason_id { get; set; }
        public int created_by { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime created_ts { get; set; }
        public virtual ICollection<jobwork_rejection_detail> jobwork_rejection_detail { get; set; }
        public virtual ICollection<jobwork_rejection_detail_tag> jobwork_rejection_detail_tag { get; set; }
    }
}
