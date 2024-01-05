using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class report_quality_vm
    {
        public string bucket_number { get; set; }
        public DateTime posting_date { get; set; }
        public string category { get; set; }
        public string document_no { get; set; }
        public string item_code { get; set; }
        public string item_name { get; set; }
       public string batch_number { get; set; }
      public int? document_qty { get; set; }
        public string plant_code { get; set; }
        public string document_type_code { get; set; }
        public string categry { get; set; }
        public string Doc_Category { get; set; }
        public DateTime P_Date { get; set; }
        public string status_name { get; set; }
        public string parameter_name { get; set; }
        public string parameter_range { get; set; }
        public string actual_value { get; set; }
        public string method_used { get; set; }
        public string checked_by { get; set; }
        public string document_reference { get; set; }
        public int? pass_fail { get; set; }
        public int? sample_size_checked { get; set; }
        public int? sample_size_accepted { get; set; }
        public int? sample_size_rejected { get; set; }
        public int? total_accepted_qty { get; set; }
        public int? total_rejected_qty { get; set; }
        public int? shelf_life { get; set; }
        public int? date_based_on { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public string storage_location_name { get; set; }
        public int? Delay { get; set; }
        public DateTime Report_Date { get; set; }
        public double? qty { get; set; }
        public string item_type_name { get; set; }
        public string item_category_name { get; set; }
        public string item_group_name { get; set; }
        public string material_Doc_Number { get; set; }
        public DateTime material_Doc_Date { get; set; }
        public string Reason_Code { get; set; }
    

    }
}
