using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class post_variances_details
    {
        [Key]
        public int post_variances_detail_id { get; set; }
        public int item_id { get; set; }
        public int uom_id { get; set; }
        public string batch_number { get; set; }
        public double stock_qty { get; set; }
        public double actual_qty { get; set; }
        public double diff_qty { get; set; }
        public double rate { get; set; }
        public double value { get; set; }
        public int post_variances_id { get; set; }
        public int create_stock_sheet_detail_id { get; set; }

    }
}
