using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class ref_asset_initial_data_header
    {
        [Key]
        public int asset_initial_data_header_id { get; set; }
        public int? document_category_id { get; set; }
        public string document_no { get; set; }
        public DateTime? posting_date { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_ts { get; set; }
        public bool is_active { get; set; }


    }
}
