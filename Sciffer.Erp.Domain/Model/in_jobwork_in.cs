using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class in_jobwork_in
    {
        [Key]
        public int job_work_in_id { get; set; }

        [Required]
        [Display(Name = "Category *")]
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }
        public string document_no { get; set; }
        [Required]
        [Display(Name = "Posting Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }    
            
        [Required]
        [Display(Name = "Customer Code *")]
        public int customer_id { get; set; }
        [ForeignKey("customer_id")]
        public virtual REF_CUSTOMER REF_CUSTOMER { get; set; }

        [Required]
        [Display(Name = "Business Unit *")]
        public int business_unit_id { get; set; }
        [ForeignKey("business_unit_id")]
        public virtual REF_BUSINESS_UNIT REF_BUSINESS_UNIT { get; set; }


        [Required]
        [Display(Name = "Plant *")]
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }

        [Required]
        [Display(Name = "Customer Challan Number *")]
        public string customer_chalan_no { get; set; }
        

      
        [Display(Name = "Customer Challan Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? customer_chalan_date { get; set; }
        
        [Display(Name = "Gate Entry Number")]
        public string gate_entry_no { get; set; }

      
        [Display(Name = "Gate Entry Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? gate_entry_date { get; set; }
                       
        [Display(Name ="")]
        public int state_id { get; set; }
        [ForeignKey("state_id")]
        public virtual REF_STATE REF_STATE { get; set; }

        [Required]
        [Display(Name = "Billing Address *")]
        public string billing_address { get; set; }
        
        [Required]
        [Display(Name = "Billing City *")]
        public string billing_city { get; set; }
        
        [Required]
        [Display(Name = "Pin Code *")]
        public string pin_code { get; set; }
        
        
        [Display(Name = "Email")]
        public string email { get; set; }

        public virtual ICollection<in_jobwork_in_detail> in_jobwork_detail { get; set; }
        public int? cancellation_reason_id { get; set; }
        public string cancellation_remark { get; set; }
        public int? status_id { get; set; }
    }
}
