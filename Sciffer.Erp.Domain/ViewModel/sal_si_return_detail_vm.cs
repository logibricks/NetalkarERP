using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class sal_si_return_detail_vm
    {
        public int sales_return_detail_id { get; set; }
        public int? si_detail_id { get; set; }
        public int si_id { get; set; }
        public int sales_return_id { get; set; }
        public int item_id { get; set; }
        public string item_code { get; set; }
        public string item_name { get; set; }
        public double? quantity { get; set; }
        public int uom_id { get; set; }
        public string uom_name { get; set; }
        public double? unit_price { get; set; }
        public double? discount { get; set; }
        public double? effective_unit_price { get; set; }
        public double? sales_value { get; set; }
        public double? assessable_rate { get; set; }
        public double? assessable_value { get; set; }
        public int tax_id { get; set; }
        public string tax_code { get; set; }
        public int storage_location_id { get; set; }
        public string storage_location_name { get; set; }
        public string drawing_no { get; set; }
        public int sac_hsn_id { get; set; }
        public string sac_hsn_code { get; set; }
        public int plant_id { get; set; }
        public string plant_name { get; set; }
        public string si_number { get; set; }
        public string batch_number { get; set; }
        public double? sal_si_return_qty { get; set; }
        public int? item_batch_detail_id { get; set; }
        public int? item_batch_id { get; set; }
        public double? issue_quantity { get; set; }
        public double? balance_quantity { get; set; }
        public double? batch_original_qty { get; set; }
    }
    public class sal_si_return_batch_vm
    {
        public int si_detail_batch_id { get; set; }
        public int si_detail_id { get; set; }
        public int item_batch_detail_id { get; set; }
        public double quantity { get; set; }
        public string batch_number { get; set; }
        public int item_id { get; set; }
        public string item_name { get; set; }
    }
  
   
    public class sal_si_return_batch_lsit_vm
    {
        public List<sal_si_return_detail_vm> sal_si_return_detail_vm { get; set; }
        public List<sal_si_return_batch_vm> sal_si_return_batch_vm { get; set; }
        public List<CustomerVM> CustomerVM { get; set; }
        public List<sal_si_vm> sal_si_vm { get; set; }
    }
}
