using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_EMPLOYEE
    {
        [Key]
        public int employee_id { get; set; }

        [Display(Name ="Employee Code *")]
        public string employee_code { get; set; }

        public int? salutation_id { get; set; }
        [ForeignKey("salutation_id")]
        public virtual REF_SALUTATION REF_SALUTATION { get; set; }

        public string employee_name { get; set; }
        public string employee_number { get; set; }
        public int? designation_id { get; set; }
        [ForeignKey("designation_id")]
        public virtual REF_DESIGNATION REF_DESIGNATION { get; set; }

        public int? category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual REF_CATEGORY REF_CATEGORY { get; set; }

        public int? department_id { get; set; }
        [ForeignKey("department_id")]
        public virtual REF_DEPARTMENT REF_DEPARTMENT { get; set; }
        public int? grade_id { get; set; }
        [ForeignKey("grade_id")]
        public virtual REF_GRADE REF_GRADE { get; set; }
        public bool is_block { get; set; }
        public string fathers_name { get; set; }
        public string mothers_name { get; set; }

       
        public DateTime? date_of_birth { get; set; }
        public int? gender_id { get; set; }    
         
        public int? marital_status_id { get; set; }
       

        public string spouse_name { get; set; }
        public int? bank_id { get; set; }
        [ForeignKey("bank_id")]
        public virtual ref_bank ref_bank { get; set; }
        public string bank_account_no { get; set; }
        public string ifsc_code { get; set; }
        public string pan_number { get; set; }
        public int? present_add_country_id { get; set; }
        [ForeignKey("present_add_country_id")]
        public virtual REF_COUNTRY REF_COUNTRY1 { get; set; }
        public string present_add_res_no { get; set; }
        public string present_add_res_name { get; set; }
        public string present_add_street { get; set; }
        public string present_add_locality { get; set; }
        public string present_add_city { get; set; }
        public int? present_add_state_id { get; set; }
       

       // [Range(0, 999999, ErrorMessage = "Invalid Pin Code")]
        public int? present_add_pincode { get; set; }

        public int? permanent_add_country_id { get; set; }
        [ForeignKey("permanent_add_country_id")]
        public virtual REF_COUNTRY REF_COUNTRY2 { get; set; }
        public string permanent_add_res_no { get; set; }
        public string permanent_add_res_name { get; set; }
        public string permanent_add_street { get; set; }
        public string permanent_add_locality { get; set; }
        public string permanent_add_city { get; set; }
        public int? permanent_add_state_id { get; set; }
       
      //  [Range(0, 999999, ErrorMessage = "Invalid Pin Code")]
        public int? permanent_add_pincode { get; set; }

        public string email_id { get; set; }
        public string mobile { get; set; }
        public string std_code { get; set; }
        public string phone { get; set; }
        public int? branch_id { get; set; }
        [ForeignKey("branch_id")]
        public virtual REF_BRANCH REF_BRANCH { get; set; }
        public int? division_id { get; set; }
        [ForeignKey("division_id")]
        public virtual REF_DIVISION REF_DIVISION { get; set; }

       
        public DateTime? date_of_joining { get; set; }
        [Display(Name = "Date of Leaving *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime? date_of_leaving { get; set; }
        [Display(Name = "Reason for leaving *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public string reason_for_leaving { get; set; }

        public string esi_no { get; set; }
        public string esi_dispensary { get; set; }
        public string pf_no { get; set; }
        public string pf_no_dept { get; set; }
        public string uan_no { get; set; }
        
        public bool esi_applicable { get; set; }
        public bool pf_applicable { get; set; }

        public int? plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }

        public string remarks { get; set; }
        public string attachment { get; set; }

        public string employeephoto { get; set; }

        public bool is_applicable { get; set; }

    }
}
