using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class pur_requisition
    {
        [Key]
        public int pur_requisition_id { get; set; }
        public int category_id { get; set; }       
        [Display(Name = "PR Number")]
        public string pur_requisition_number { get; set; }
        [Display(Name = "PR Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime pur_requisition_date { get; set; }
        public int business_unit_id { get; set; }
        [ForeignKey("business_unit_id")]
        public virtual REF_BUSINESS_UNIT REF_BRAND { get; set; }
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        [Display(Name = "Delivery Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? delivery_date { get; set; }
        public int source_id { get; set; }
        [ForeignKey("source_id")]
        public virtual REF_SOURCE REF_SOURCE { get; set; }
        [Display(Name = "Created By")]
        public int created_by { get; set; }
        [Display(Name = "Created On")]
        public DateTime created_on { get; set; }
        [Display(Name = "Internal Remarks")]
        public string internal_remarks { get; set; }
        [Display(Name = "Remarks")]
        public string remarks { get; set; }
        public string attachement { get; set; }
        public int status_id { get; set; }
        [ForeignKey("status_id")]
        public virtual ref_status ref_status { get; set; }
        public virtual ICollection<pur_requisition_detail> pur_requisition_detail { get; set; }
        public bool order_status { get; set; }
        public int? approval_status { get; set; }
        public string approval_comments { get; set; }      
        public int? responsibility_id { get; set; }
        public int? approved_by { get; set; }
        public DateTime? approved_ts { get; set; }
        public string closed_remarks { get; set; }
        public int? closed_by { get; set; }
        public DateTime? closed_ts { get; set; }

        public bool is_seen { get; set; }

    }
}
