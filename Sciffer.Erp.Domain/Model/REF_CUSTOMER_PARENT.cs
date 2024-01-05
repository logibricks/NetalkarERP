using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_CUSTOMER_PARENT
    {
        [Key]
        public int CUSTOMER_PARENT_ID { get; set; }
        [Display(Name = "Customer Code *")]
        [Required]
        public string customer_code { get; set; }

        [Display(Name = "Customer Name *")]
        [Required]
        public string CUSTOMER_PARENT_NAME { get; set; }
        [Display(Name = "Address *")]
        [DataType(DataType.MultilineText)]
        [Required]
        public string REGD_OFFICE_ADDRESS { get; set; }
        [Display(Name = "City *")]
        [Required]
        public string REGD_OFFICE_CITY { get; set; }
        [Display(Name = "Pincode *")]
        [Range(100000, 999999, ErrorMessage = "Please enter correct Pincode.")]
        [Required]
        public int REGD_OFFICE_PINCODE { get; set; }        //[Display(Name = "State")]
        [Display(Name = "State *")]
        public int REGD_OFFICE_STATE_ID { get; set; }
        [ForeignKey("REGD_OFFICE_STATE_ID")]
        public virtual REF_STATE REF_STATE { get; set; }

        [Display(Name = "Credit Limit")]
        public double? PARENT_CREDIT_LIMIT { get; set; }
        [Display(Name = "Website")]
        public string WEBSITE_ADDRESS { get; set; }
        [Display(Name = "Blocked")]
        public bool blocked { get; set; }
    }

    public class CustomerParentVM
    {        
        public int CUSTOMER_PARENT_ID { get; set; }
        [Display(Name = "Customer Code *")]
        public string customer_code { get; set; }
        [Display(Name = "Customer Name *")]
        public string CUSTOMER_PARENT_NAME { get; set; }
        [Display(Name = "Address *")]
        [DataType(DataType.MultilineText)]
        public string REGD_OFFICE_ADDRESS { get; set; }
        [Display(Name = "City *")]
        public string REGD_OFFICE_CITY { get; set; }
        [Display(Name = "Pincode *")]
        public int REGD_OFFICE_PINCODE { get; set; }
        [Display(Name = "State *")]
        public int REGD_OFFICE_STATE_ID { get; set; }
        [Display(Name = "Country *")]
        public int REGD_OFFICE_COUNTRY_ID { get; set; }
        [Display(Name = "Credit Limit")]
        public double? PARENT_CREDIT_LIMIT { get; set; }
        [Display(Name = "Website")]
        public string WEBSITE_ADDRESS { get; set; }
        public string REGD_OFFICE_STATE_NAME { get; set; }
        public string REGD_OFFICE_COUNTRY_NAME { get; set; }
        [Display(Name = "Blocked")]
        public bool blocked { get; set; }
        public string customer_parent_code_name { get; set; }
    }
}
