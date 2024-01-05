using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class create_stock_sheet
    {
        [Key]
        public int create_stock_sheet_id { get; set; }
        public int category_id { get; set; }
        public string document_no { get; set; }
        public DateTime document_date { get; set; }
        public string ref_1 { get; set; }
        public int plant_id { get; set; }
        public int sloc_id { get; set; }
        public int bucket_id { get; set; }
        public bool is_active { get; set; }
        public int created_by { get; set; }
        public DateTime created_ts { get; set; }
        public int status_id { get; set; }
        public int? modify_by { get; set; }
        public DateTime? modify_ts { get; set; }
        public int? cancellation_reason_id { get; set; }
        public string cancellation_remarks { get; set; }
        public int? cancelled_by { get; set; }
        public DateTime? cancelled_ts { get; set; }
        public int item_category_id { get; set; }
    }
}
