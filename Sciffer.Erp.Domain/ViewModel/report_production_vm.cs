using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class report_production_vm
    {
        public string item_name { get; set; }
        public string machine_id { get; set; }
        public string item_code { get; set; }
        public string prod_order_no { get; set; }
        public int? qty { get; set; }
        public double? quaintity { get; set; }
        public double? value { get; set; }
        public double? cost { get; set; }
        public string machine_code { get; set; }
        public string machine_desc { get; set; }
        public double? thirty_days { get; set; }
        public double? sixty_days { get; set; }
        public double? ninety_days { get; set; }
        public double? one_twenty_days { get; set; }
        public double? one_fifty_days { get; set; }
        public double? one_eighty_days { get; set; }
        public double? sixty_five_days { get; set; }
        public double? three_sixty_five_days { get; set; }
        public double? total { get; set; }
        public int? onedays { get; set; }
        public int? twodays { get; set; }
        public int? fivedays { get; set; }
        public int? eightdays { get; set; }
        public int? sixteendays { get; set; }
        public int? morethanthirty { get; set; }
        public string user_code { get; set; }
        public string machine_name { get; set; }
        public string user_name { get; set; }
        public int? OK_Qty { get; set; }
        public int? Rejected_Qty { get; set; }
        public int? Total_Qty { get; set; }
        //public string tag_id { get; set; }
        public string tag_no { get; set; }
        public string parameter_name { get; set; }
        public string std_range_start { get; set; }
        public string parameter_value { get; set; }
        public string std_range_end { get; set; }
        public string item_batch_no { get; set; }
        public string in_item_desc { get; set; }
        public string status_name { get; set; }
        public DateTime? task_time { get; set; }
        public string task_time_report { get; set; }
        public string DatePart { get; set; }
        public string TimePart { get; set; }


        public int? shift_a_ok_qty { get; set; }
        public int? shift_a_reject_qty { get; set; }
        public int? shift_a_total { get; set; }
        public int? shift_b_ok_qty { get; set; }
        public int? shift_b_reject_qty { get; set; }
        public int? shift_b_total { get; set; }
        public int? shift_c_ok_qty { get; set; }
        public int? shift_c_reject_qty { get; set; }
        public int? shift_c_total { get; set; }
        public string operator_name { get; set; }
        public DateTime? date { get; set; }
        public string date_str { get; set; }
        public string shift_code { get; set; }
        public double prod_qty { get; set; }
        public double min_prod_qty { get; set; }
        public double incentive_per_qty { get; set; }
        public double incentive_value { get; set; }
        public DateTime? create_ts { get; set; }
        public string oper { get; set; }
        public double? quantity { get; set; }
        public double? balance_qty { get; set; }
        public DateTime? prod_order_date { get; set; }
        public string Operation { get; set; }

        //Breakdown Order
        public string doc_number { get; set; }
       // public DateTime actual_start_date { get; set; }
        //ublic DateTime actual_finish_date { get; set; }
        //public TimeSpan actual_start_time { get; set; }
        //public TimeSpan actual_end_time { get; set; }
        public string MinuteDiff { get; set; }
        //public int cycle_time { get; set; }
        //public int production_loss { get; set; }
        public string actual_start_date1 { get; set; }
        public string actual_finish_date1 { get; set; }
        public string actual_start_time1{ get; set; }
        public string actual_end_time1 { get; set; }
        //public string Time_Difference { get; set; }
        //public string starttime { get; set; }
        //public string endttime { get; set; }
         public decimal Cycle_Time_Minute { get; set; }
         public decimal production_loss_qty { get; set; }

        public string qc_remark { get; set; }
        public string process_id { get; set; }
        public string process_code { get; set; }
        public string process_description { get; set; }
        public string process_name { get; set; }

        //ROL Report
        public string UOM_NAME { get; set; }
        public string storage_location_name { get; set; }
        public int MINIMUM_LEVEL { get; set; }
        public int MAXIMUM_LEVEL { get; set; }
        public double REORDER_QUANTITY { get; set; }
        public int REORDER_LEVEL { get; set; }
        public int AvailableStock { get; set; }
        public int Shortfall { get; set; }

        public DateTime? From_date { get; set; }
        public DateTime? To_date { get; set; }
        public string Level { get; set; }
        public string Active_inactive_user { get; set; }

        public int plant_id { get; set; }

    }
}
