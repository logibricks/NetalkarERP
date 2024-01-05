using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_incentive_vm
    {
        public int operator_id { get; set; }
        public string operator_name { get; set; }
        public string user_name { get; set; }
        public DateTime? date { get; set; }
        public string date_str { get; set; }
        public int shift_id { get; set; }
        public string shift_code { get; set; }
        public string item_code { get; set; }
        public string item_name { get; set; }
        public string machine_id { get; set; }
        public string machine_code { get; set; }
        public string machine_name { get; set; }
        public int? process_id { get; set; }
        public string process_desc { get; set; }
        public string process_description { get; set; }
        public int prod_qty { get; set; }
        public int plant_id { get; set; }
        public string plant_name { get; set; }
        public string incentive_applicability { get; set; }
        public DateTime? login_time { get; set; }
        public DateTime? logout_time { get; set; }
        public decimal shift_hours { get; set; }
        public double? reporting_quantity { get; set; }
        public double? incentive { get; set; }
        public double? diff_qty { get; set; }
        public double? amount { get; set; }
        public string remarks { get; set; }
        public bool is_multi_machine { get; set; }
        public bool is_continued_shift { get; set; }
        public int? item_id { get; set; }
        public int user_id { get; set; }
        public int startrow { get; set; }
        public int endrow { get; set; }
        public string is_multi_machine1 { get; set; }
        public string columnname { get; set; }
        public string login_time1 { get; set; }
        public string logout_time1 { get; set; }
        public int? machine_id1 { get; set; }
        public decimal? prod_qty1 { get; set; }
    }

    public class DataTableAggregateFunction
    {
        /// <summary>
        /// The function to be performed
        /// </summary>
        public AggregateFunction enmFunction { get; set; }

        /// <summary>
        /// Performed for which column
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// What should be the name after output
        /// </summary>
        public string OutPutColumnName { get; set; }
    }

    public enum AggregateFunction
    {
        Sum,
        Avg,
        Count,
        Max,
        Min
    }

    public class status_vm
    {
        public bool success { get; set; }
        public List<string_data> result { get; set; }
    }

    public class string_data
    {
        public List<string> key_value_pair { get; set; }
    }
    public class key_value_pair
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class ref_easy_hr_data_vm
    {
        public Int64 easy_hr_data_id { get; set; }
        public string shift_date { get; set; }
        public int operator_id { get; set; }
        public string operator_code { get; set; }
        public string in_time { get; set; }
        public string out_time { get; set; }
        public int shift_id { get; set; }
        public string shift_name { get; set; }
        public double time_diff { get; set; }
    }
    public class incentive_status
    {
        public int plant_id { get; set; }
        public int shift_id { get; set; }
        public string date { get; set; }
        public string incentive_status_name { get; set; }
        public string incentive_status_code { get; set; }
    }
}