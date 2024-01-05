using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class operator_incentive_detail_vm
    {
        [Display(Name = "Start Date*")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MMMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime from_date { get; set; }
        [Display(Name = "End Date*")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MMMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime to_date { get; set; }
        [Display(Name = "Plant*")]
        public int plant_id { get; set; }
        public int operation_id { get; set; }
        public int machine_id { get; set; }       
        public string start_time { get; set; }  
        public string end_time { get; set; }   
        public bool is_incentive_appl { get; set; }      
        public bool is_multi_machining { get; set; }     
        public decimal prod_qty { get; set; }       
        public decimal benchmark_qty { get; set; }        
        public decimal excess_qty { get; set; }       
        public decimal incentive_rate { get; set; }     
        public decimal incentive_amount { get; set; }
        public string shift_code { get; set; }
        public string PLANT_NAME { get; set; }
        public string item_name { get; set; }
        public string process_description { get; set; }
        public string machine_name { get; set; }
        public int mfg_operator_incentive_detail_id { get; set; }
        public string date { get; set; }

    }
}
