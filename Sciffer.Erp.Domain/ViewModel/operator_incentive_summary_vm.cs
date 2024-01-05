using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
  public class operator_incentive_summary_vm
    {
        public int user_id { get; set; }
        [Display(Name = "Start Date*")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MMMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime from_date { get; set; }
        [Display(Name = "End Date*")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MMMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime to_date { get; set; }
        [Display(Name = "Incentive Amount")]
        public decimal incentive_amt { get; set; }
        [Display(Name = "Shift*")]

        public int shift_id { get; set; }
        public string user_name { get; set; }
        public string operator_name { get; set; }
        public string remarks { get; set; }
        public string shift_code { get; set; }
        public string status_name { get; set; }
        public string date { get; set; }
        public bool is_incentive_appl { get; set; }
        public string login_time { get; set; }
        public string logout_time { get; set; }
        public string incentive_applicability { get; set; }
        public double incentive_amount { get; set; }
    }
}
