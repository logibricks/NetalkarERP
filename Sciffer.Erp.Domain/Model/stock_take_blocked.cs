using System;
using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class stock_take_blocked
    {
        [Key]
        public int stock_take_blocked_id { get; set; }
        public int plant_id { get; set; }
        public int sloc_id { get; set; }
        public int bucket_id { get; set; }
        public bool is_blocked { get; set; }
        public int item_category_id { get; set; }
        public int? created_by { get; set; }
        public DateTime?  created_ts { get; set; }
        public int? modify_by { get; set; }
        public DateTime? modify_ts { get; set; }
    }
}
