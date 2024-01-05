using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class REF_EMPLOYEE_VM
    {
        [Key]
        public int employee_id { get; set; }

        [Display(Name = "Employee Code *")]
        public string employee_code { get; set; }
        public int? salutation_id { get; set; }
        [ForeignKey("salutation_id")]
        public virtual REF_SALUTATION REF_SALUTATION { get; set; }
        public string salutation { get; set; }
        [Display(Name = "Employee name *")]
        public string employee_name { get; set; }
        [Display(Name = "Employee Number")]
        public string employee_number { get; set; }
        [Display(Name = "Designation")]
        public int? designation_id { get; set; }
        [ForeignKey("designation_id")]
        public virtual REF_DESIGNATION REF_DESIGNATION { get; set; }
        public string designation { get; set; }
        [Display(Name = "Category")]
        public int? category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual REF_CATEGORY REF_CATEGORY { get; set; }
        public string category { get; set; }
        [Display(Name = "Department")]
        public int? department_id { get; set; }
        [ForeignKey("department_id")]
        public virtual REF_DEPARTMENT REF_DEPARTMENT { get; set; }
        public string department { get; set; }
        [Display(Name = "Grade")]
        public int? grade_id { get; set; }
        [ForeignKey("grade_id")]
        public virtual REF_GRADE REF_GRADE { get; set; }
        public string grade { get; set; }
        [Display(Name = "Blocked")]
        public bool is_block { get; set; }
        [Display(Name = "Father's Name")]
        public string fathers_name { get; set; }
        [Display(Name = "Mother's Name")]
        public string mothers_name { get; set; }
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date_of_birth { get; set; }
        [Display(Name = "Sex")]
        public int? gender_id { get; set; }
        public string gender { get; set; }
        [Display(Name = "Marital Status")]
        public int? marital_status_id { get; set; }
       

        public string marrital { get; set; }
        [Display(Name = "Spouse's Name")]
        public string spouse_name { get; set; }
        [Display(Name = "Bank Name")]
        public int? bank_id { get; set; }
        [ForeignKey("bank_id")]
        public virtual ref_bank ref_bank { get; set; }
        public string bank_name { get; set; }
        [Display(Name = "Bank Account Number")]
        public string bank_account_no { get; set; }
        [Display(Name = "IFSC Code")]
        public string ifsc_code { get; set; }
        [Display(Name = "PAN No.")]
        public string pan_number { get; set; }
        public int? present_add_country_id { get; set; }
        [ForeignKey("present_add_country_id")]
        public virtual REF_COUNTRY REF_COUNTRY1 { get; set; }
        public string country1 { get; set; }
        [Display(Name = "Res. No")]
        public string present_add_res_no { get; set; }
        [Display(Name = "Res. Name")]
        public string present_add_res_name { get; set; }
        [Display(Name = "Road/Street")]
        public string present_add_street { get; set; }
        [Display(Name = "Locality/Area")]
        public string present_add_locality { get; set; }
        [Display(Name = "City/District")]
        public string present_add_city { get; set; }        
        public int? present_add_state_id { get; set; }
       
        public string state1 { get; set; }
        [Display(Name = "Pincode")]
        public int? present_add_pincode { get; set; } 
        public bool perm_same_as_pre { get; set; }      
        public int? permanent_add_country_id { get; set; }
        [ForeignKey("permanent_add_country_id")]
        public virtual REF_COUNTRY REF_COUNTRY2 { get; set; }
        public string country2 { get; set; }
        [Display(Name = "Res. No")]
        public string permanent_add_res_no { get; set; }
        [Display(Name = "Res. Name")]
        public string permanent_add_res_name { get; set; }
        [Display(Name = "Road/Street")]
        public string permanent_add_street { get; set; }
        [Display(Name = "Locality/Area")]
        public string permanent_add_locality { get; set; }
        [Display(Name = "City/District")]
        public string permanent_add_city { get; set; }        
        public int? permanent_add_state_id { get; set; }
       
        public string state2 { get; set; }
        [Display(Name = "Pincode")]
        public int? permanent_add_pincode { get; set; }
        [Display(Name = "Email ID")]
        public string email_id { get; set; }
        [Display(Name = "Mobile")]
        public string mobile { get; set; }
        [Display(Name = "STD Code")]
        public string std_code { get; set; }
        [Display(Name = "Phone")]
        public string phone { get; set; }        
        public int? branch_id { get; set; }
        [ForeignKey("branch_id")]
        public virtual REF_BRANCH REF_BRANCH { get; set; }
        public string branch { get; set; }
        public int? division_id { get; set; }
        [ForeignKey("division_id")]
        public virtual REF_DIVISION REF_DIVISION { get; set; }
        public string division { get; set; }
        [Display(Name = "Date of Joining")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date_of_joining { get; set; }

        [Display(Name = "Date of Leaving")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date_of_leaving { get; set; }

        [Display(Name = "Reason for leaving")]
        public string reason_for_leaving { get; set; }

        [Display(Name = "ESI NO.")]
        public string esi_no { get; set; }
        [Display(Name = "ESI DISPENSARY")]
        public string esi_dispensary { get; set; }
        [Display(Name = "PF NO.")]
        public string pf_no { get; set; }
        [Display(Name = "PF NO. FOR DEPT. FILE")]
        public string pf_no_dept { get; set; }
        [Display(Name = "UAN NO.")]
        public string uan_no { get; set; }
        [Display(Name = "ESI Applicable ")]
        public bool esi_applicable { get; set; }
        [Display(Name = "PF Applicable ")]
        public bool pf_applicable { get; set; }
        public int? plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        [Display(Name = "Remarks")]
        [DataType(DataType.MultilineText)]
        public string remarks { get; set; }
        public string attachment { get; set; }
        [NotMapped]
        [Display(Name = "Attachment")]
        public HttpPostedFileBase FileUpload { get; set; }

        public string ledgeraccounttype { get; set; }
        public string salutation_name { get; set; }
        public bool blocked { get; set; }
        public string plant_name { get; set; }
        [Display(Name = "Photo Upload")]
        public string employeephoto { get; set; }
        [NotMapped]
      
        public HttpPostedFileBase ImageUpload { get; set; }

        [Display(Name = "Incentives Applicable")]
        public bool is_applicable { get; set; }
    }
}
