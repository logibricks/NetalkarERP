using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
   public class prod_downtime_vm
    {
        public int prod_downtime_id { get; set; }
        public int prod_plan_detail_id { get; set; }
        [Display(Name = "M/C Breakdown")]
        public decimal mac_breakdown { get; set; }
        [Display(Name = "PM")]
        public decimal pm { get; set; }
        [Display(Name = "No Power")]
        public decimal no_power { get; set; }
        [Display(Name = "No Operator")]
        public decimal no_operator { get; set; }
        [Display(Name = "No Load")]
        public decimal no_load { get; set; }
        [Display(Name = "Setup")]
        public decimal setup { get; set; }
        [Display(Name = "Restart")]
        public decimal restart { get; set; }
        [Display(Name = "Tool Change")]
        public decimal tool_change { get; set; }
        [Display(Name = "Quality Check")]
        public decimal quality_check { get; set; }
        [Display(Name = "No Plan")]
        public decimal no_plan { get; set; }
        [Display(Name = "Training")]
        public decimal training { get; set; }
        [Display(Name = "JH")]
        public decimal jh { get; set; }
        [Display(Name = "Remarks")]
        public decimal remarks { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_ts { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_ts { get; set; }       
        public bool is_active { get; set; }
        [Display(Name = "Machine")]
        public int machine_id { get; set; }
        [Display(Name = "Item")]
        public int item_id { get; set; }
        [Display(Name = "Shift")]
        public int? shift_id { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? prod_date { get; set; }

        public string item_name { get; set; }
        public string machine_code { get; set; }
        public string prod_dates { get; set; }     
        public int? supervisor_id { get; set; }   
        public string shift_code { get; set; }
        public string supervisor_code { get; set; }

    }
}
