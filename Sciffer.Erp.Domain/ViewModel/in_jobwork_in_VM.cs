using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class in_jobwork_in_VM
    {
        [Key]
        public int job_work_in_id { get; set; }

        [Required]
        [Display(Name = "Doc Category *")]
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }
        public string document_no { get; set; }
        [Required]
        [Display(Name = "Posting Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
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


        [Required]
        [Display(Name = "Customer Challan Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? customer_chalan_date { get; set; }

        
        [Display(Name = "Gate Entry Number ")]
        public string gate_entry_no { get; set; }

        
        [Display(Name = "Gate Entry Date ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? gate_entry_date { get; set; }

        [Display(Name = "State *")]
        public int state_id { get; set; }
        [ForeignKey("state_id")]
        public virtual REF_STATE REF_STATE { get; set; }

        
        [Display(Name = "Billing Address *")]
        [DataType(DataType.MultilineText)]
        public string billing_address { get; set; }

        
        [Display(Name = "Billing City *")]
        public string billing_city { get; set; }

       
        [Display(Name = "Pin Code *")]
        public string pin_code { get; set; }

      
        [Display(Name = "Email ")]
        public string email { get; set; }


        public string categoryName { get; set; }
        public string CustomerName { get; set; }
        public string business_UnitName { get; set; }
        public string plant_name { get; set; }
        public string state_name { get; set; }
        public int Country_id { get; set; }
        public string Country_name { get; set; }
        public List<string> job_Work_detail_in_id { get; set; }
        
        public List<string> Item_id { get; set; }
        public List<string> Quantity { get; set; }
        public List<string> uom_id { get; set; }
        public List<string> batch { get; set; }
        public List<string> sloc_id { get; set; }
        public List<string> bucket_id { get; set; }
        public List<string> rate { get; set; }
        public virtual IList<in_jobwork_in_detail> in_jobwork_detail { get; set; }
        public string item_name { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public int? document_numbring_id { get; set; }
        public int? cancellation_reason_id { get; set; }
        public string cancellation_remark { get; set; }
        [Display(Name ="Status")]
        public int? status_id { get; set; }
        public string status_name { get; set; }
    }
}
