using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_customer_complaint
    {
        [Key]
        public int customer_complaint_id { get; set; }

        [Display(Name = "Item Name *")]
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        [Display(Name = "Tag No *")]
        public string tag_no { get; set; }
        
        [Display(Name = "Customer *")]
        public int customer_id { get; set; }
        [ForeignKey("customer_id")]
        public virtual REF_CUSTOMER REF_CUSTOMER { get; set; }
        [Display(Name = "Complaint Received From *")]
        public string complaint_received_from { get; set; }
        [Display(Name = "Complaint Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime complaint_date { get; set; }
        [Display(Name = "Complaint Receipt Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime complaint_receipt_date { get; set; }
        [Display(Name = "Complaint No *")]       
        public string complaint_no { get; set; }
        [Display(Name = "Complaint Type *")]
       
        public string complaint_type { get; set; }
        [Display(Name = "NPT Customer Account Holder")]
       
        public string npt_customer_account_holder { get; set; }
        [Display(Name = "Complaint Details")]
       
        public string complaint_details { get; set; }
        [Display(Name = "Reported First / Repeated")]
      
        public string reported_first_repeated { get; set; }
        [Display(Name = "Quantity affected")]
     
        public int? quantity_affected { get; set; }
        [Display(Name = "Containment Action")]

        public string containment_action { get; set; }
        [Display(Name = "Root Cause at Occurrence")]
        public string root_cause_at_occurrence { get; set; }
        [Display(Name = "Root cause for Detection")]
        public string root_cause_for_detection { get; set; }
        [Display(Name = "Corrective action")]
        public string corrective_action { get; set; }
        [Display(Name = "Preventive action")]
        public string preventive_action { get; set; }
        [Display(Name = "Status")]
        public string status { get; set; }
        [Display(Name = "PFC")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? pfc { get; set; }
        [Display(Name = "CP")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? cp { get; set; }
        [Display(Name = "FMEA")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? fmea { get; set; }
        [Display(Name = "WI / SOP")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? wi_sop { get; set; }
        [Display(Name = "OIR")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? oir { get; set; }
        [Display(Name = "LIR")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? lir { get; set; }
        [Display(Name = "Start-up Check sheet")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? start_up_check_sheet { get; set; }
        [Display(Name = "Others PM, Gauge, Poka Yoke  ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? others_pm_gauge_poka_yoke { get; set; }
        [Display(Name = "PTDB")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ptdb { get; set; }
        [Display(Name = "Change Note")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? change_note { get; set; }
        [Display(Name = "CAPA No submission date ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? capa_no_submission_date { get; set; }
        [Display(Name = "Month1")]
        public string month1 { get; set; }
        [Display(Name = "Month2")]
        public string month2 { get; set; }
        [Display(Name = "Month3")]
        public string month3 { get; set; }
        public int created_by { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? created_ts { get; set; }
        public int modified_id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? modified_ts { get; set; }

    }
}
