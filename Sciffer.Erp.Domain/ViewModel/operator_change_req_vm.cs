using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
   public class operator_change_req_vm
    {
        public int operator_change_request_id { get; set; }

        [Display(Name = "Doc Category*")]
        public int category_id { get; set; }
        [Display(Name = "Doc No.*")]
        public string document_no { get; set; }
        [Display(Name = "Posting Date *")]
        [Required(ErrorMessage = "date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }
        [Display(Name = "Operator")]
        public int operator_id { get; set; }
        [Display(Name = "Machine")]
        public int machine_id { get; set; }
        [Display(Name = "Current Level")]
        public int current_level_id { get; set; }
        [Display(Name = "New Level")]
        public int new_level_id { get; set; }

        public List<string> operator_list_id { get; set; }
        public List<string> machine_list_id { get; set; }
        public List<string> current_level_list_id { get; set; }
        public List<string> new_level_list_id { get; set; }

        public List<string> operator_change_request_list_id { get; set; }
        public string current_level_code { get; set; }
        public string machine_name { get; set; }
        public string operator_name { get; set; }
        public string new_level_code { get; set; }
        public string category { get; set; }
        public virtual IList<operator_change_request_detail_vm> operator_change_request_detail_vm { get; set; }
        public int status_id { get; set; }
        public string status_name { get; set; }
        public int approval_status_id { get; set; }
        public string approval_status_name { get; set; }
        public int operator_change_request_detail_id { get; set; }
        public string approval_comments { get; set; }
    }
}
