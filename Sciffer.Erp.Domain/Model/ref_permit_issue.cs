using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Sciffer.Erp.Domain.Model
{
   public class ref_permit_issue
    {

        [Key]
        public int permit_id { get; set; }
        public string permit_no { get; set; }
       
        // public int? permit_category { get; set; }

        [Required]
        [Display(Name = "Category *")]
        public int category_id { get; set; }
       
        [Required(ErrorMessage = "Permit date is required")]
        [Display(Name = "Permit Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime permit_date { get; set; }

        [Required(ErrorMessage = "Valid from date is required")]
        [Display(Name = "Valid From Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime valid_from { get; set; }

        [Required(ErrorMessage = "Valid to date is required")]
        [Display(Name = "Valid To Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime valid_to { get; set; }

        public int no_of_workers { get; set; }
        public string work_description { get; set; }
        public string work_location { get; set; }
        public int vender_id { get; set; }
        public int? plant_id { get; set; }

      
        //public int permit_template_id { get; set; }
        //[ForeignKey("permit_template_id")]
        //public virtual Ref_permit_template Ref_permit_template { get; set; }

        public int created_by { get; set; }
        public DateTime created_ts { get; set; }
        public bool is_blocked { get; set; }
        public virtual ICollection<ref_permit_details> ref_permit_details { get; set; }
    }
}
