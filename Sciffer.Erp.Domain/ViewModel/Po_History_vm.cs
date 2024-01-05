using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Po_History_vm
    {
        public int? po_id { get; set; }
        public int? grn_id { get; set; }
        public int? vendor_id { get; set; }
        public string vendor_name { get; set; }
        public string po_no { get; set; }
        public string po_date { get; set; }
        public decimal? item_cost { get; set; }
        public decimal? quantity { get; set; }
        public decimal? grn_quantity { get; set; }
        public decimal? current_stock { get; set; }
        public decimal? free { get; set; }
        public decimal? blocked { get; set; }
        public decimal? Open_PR { get; set; }
        public decimal? Open_PO { get; set; }
    }
}
