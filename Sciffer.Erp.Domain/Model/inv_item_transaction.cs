using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class inv_item_transaction
    {
        [Key]
        public int item_transaction_id { get; set; }
        public int item_id { get; set; }
        public int plant_id { get; set; }
        public int sloc_id { get; set; }
        public string transaction_type_code { get; set; }
        public string document_type_code { get; set; }
        public string document_id { get; set; }
        public DateTime? item_transaction_date { get; set; }
        public double? item_quantity { get; set; }
        public decimal? item_basic_value { get; set; }
        public int create_user { get; set; }
        public DateTime create_ts { get; set; }
        public int? modify_user { get; set; }
        public DateTime? modify_ts { get; set; }
        public int? doc_detail_id { get; set; }
        public int? reason_determination_id { get; set; }
        public int? bucket_id { get; set; }
        public bool? is_active { get; set; }
        public decimal? item_value { get; set; }
        public int? document_numbering_id { get; set; }
        public string document_no { get; set; }
    }
}
