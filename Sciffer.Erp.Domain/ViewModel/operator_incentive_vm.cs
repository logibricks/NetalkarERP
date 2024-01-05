using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
   public class operator_incentive_vm
    {
        public string date { get; set; }
        public string shift_code { get; set; }
        public string user_name { get; set; }
        public string operator_name { get; set; }
        public string incentive_applicability { get; set; } 
        public string login_time { get; set; }
        public string logout_time { get; set; }
        public string machine_code { get; set; }
        public string machine_name { get; set; }
        public string process_desc { get; set; }
        public string process_description { get; set; }
        public double prod_qty { get; set; }
        public double reporting_quantity { get; set; }
        public double incentive { get; set; }
        public double diff_qty { get; set; }
        public double amount { get; set; }
        public int shift_id { get; set; }
        public int process_id { get; set; }
        public string machine_id { get; set; }
        public int operator_id   { get; set; }
        public int plant_id { get; set; }
        public int status_id { get; set; }
        public int item_id { get; set; }
        public string item_name { get; set; }

        public List<string> shift_ids { get; set; }
        public List<string> user_ids { get; set; }
        public List<string> incentive_app { get; set; }
        public List<string> inc_amt { get; set; }
        public List<string> mfg_process_ids { get; set; }
        public List<string> machine_ids { get; set; }
        public List<string> dates { get; set; }
        public List<string> plant_ids { get; set; }
        public List<string> benchmark_qtys { get; set; }
        public List<string> incentive_rates { get; set; }
        public List<string> prod_qtys { get; set; }
        public List<string> excess_qtys { get; set; }
        public List<string> is_multi_machinings { get; set; }
        public List<string> start_times { get; set; }
        public List<string> end_times { get; set; }
        public List<string> incentive_amounts { get; set; }
        public List<string> is_incentive_appls { get; set; }
        public List<string> statuss { get; set; }
        public List<string> items { get; set; }


        [Display(Name = "Start Date*")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MMMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime from_date { get; set; }
        [Display(Name = "End Date*")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MMMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime to_date { get; set; }
        

    }
}
