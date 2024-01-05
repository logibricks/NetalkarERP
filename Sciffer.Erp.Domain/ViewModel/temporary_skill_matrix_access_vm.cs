using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class temporary_skill_matrix_access_vm
    {
        public int temp_operator_level_mapping_id { get; set; }
        public int operator_id { get; set; }
        public int level_id { get; set; }
        public int machine_id { get; set; }
        public int shift_id { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime effective_date { get; set; }
        public bool is_active { get; set; }
        public bool is_blocked { get; set; }
        public int created_by { get; set; }
        public DateTime created_ts { get; set; }
        public int modified_by { get; set; }
        public DateTime modified_ts { get; set; }
        public string OperatorName { get; set; }
        public string level_code { get; set; }
        public string machine_code { get; set; }
        public string posting_date { get; set; }
        public string shift { get; set; }
        public string is_blocked1 { get; set; }
        public string effective_date1 { get; set; }

    }
}
