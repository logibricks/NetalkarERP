using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class dep_run
    {
        [Key]
        public int depreciation_run_id { get; set; }
        public int document_numbering_id { get; set; }
        public string document_no { get; set; }
        public DateTime? posting_for_last_run_date { get; set; }
        public int dep_area_id { get; set; }
        public int asset_class_id { get; set; }
        public DateTime? current_dep_till_date { get; set; }
        public int status_id { get; set; }
        public DateTime? created_ts { get; set; }
        public int created_by { get; set; }
        public DateTime? modify_ts { get; set; }
        public int? modify_by { get; set; }
        public bool is_active { get; set; }

    }
}
