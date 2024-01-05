using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class ref_skill_matrix_vm
    {
        //level
        public int level_id { get; set; }
        public string level_code { get; set; }
        public string level_desc { get; set; }
        public string color_code { get; set; }
        public decimal percentage { get; set; }
        public bool is_active1 { get; set; }
        public string is_blocked1 { get; set; }

        //operatorlevel
        public int operator_level_mapping_id { get; set; }
        public int supervisor_id { get; set; }
        public int level_id1 { get; set; }
        public int machine_id { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public bool is_active2 { get; set; }
        public string machine_code { get; set; }
        public string supervisor_code { get; set; }
        public string is_blocked2 { get; set; }

        //machinelevel

        public int machine_level_mapping_id { get; set; }
        public int level_id2 { get; set; }
        public int machine_id2 { get; set; }
        public DateTime fromDate2 { get; set; }
        public DateTime toDate2 { get; set; }
        public bool is_active3 { get; set; }
        public string machine_code2 { get; set; }
        public string level_code2 { get; set; }
        public string is_blocked3 { get; set; }

        //Operatorchangereq

        public int user_id { get; set; }
        public string user_code { get; set; }
        public string machine_name { get; set; }
        public int operator_change_request_id { get; set; }
        public int category_id { get; set; }
        public DateTime posting_date { get; set; }
        public int operator_id { get; set; }
        public int current_level_id { get; set; }
        public int new_level_id { get; set; }
        public string category { get; set; }
        public string document_no { get; set; }
        public string machine_id_list { get; set; }
        public int MappingId { get; set; }
        public string OperatorName { get; set; }
        public int process_id { get; set; }
        public string machine_level { get; set; }
        public string shift { get; set; }

        public int? temp_operator_level_mapping_id { get; set; }

    }
}
