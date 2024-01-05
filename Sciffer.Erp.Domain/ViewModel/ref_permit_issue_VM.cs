using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Sciffer.Erp.Domain.Model;

namespace Sciffer.Erp.Domain.ViewModel
{
   public class ref_permit_issue_VM
    {

        [Key]
        public int permit_id { get; set; }
        public string permit_no { get; set; }

        //[Display(Name = "Permit Category ")]
        //public int? permit_category { get; set; }

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

        [Display(Name = "No of Workers")]
        public int no_of_workers { get; set; }
        [Display(Name = "Work Description")]
        public string work_description { get; set; }
        [Display(Name = "Work Location")]
        public string work_location { get; set; }
        [Display(Name = "Vendor ")]
        public int vender_id { get; set; }
        [Display(Name = "Plant * ")]
        public int? plant_id { get; set; }

        public string category_name { get; set; }
        //[Display(Name = "Permit Category ")]
        //public int? permit_category { get; set; }
        //[Display(Name = "Permit Template No")]
        //public int permit_template_id { get; set; }
        //[ForeignKey("permit_template_id")]
        //public virtual Ref_permit_template Ref_permit_template { get; set; }

        public int created_by { get; set; }
        public DateTime created_ts { get; set; }
        public bool is_blocked { get; set; }

        public virtual IList<ref_permit_details> ref_permit_details { get; set; }
        public List<string> permit_detail_id { get; set; }
        public List<string> checkpoints { get; set; }
        public List<string> ideal_scenario { get; set; }
        public List<string> permit_template_id{ get; set; }
        public List<string> check_point_id { get; set; }
        //public List<string> permit_category{ get; set; }
        public string deleteids { get; set; }
    }
}
