using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class bom_details
    {
        public string prod { get; set; }
        public int bom_id { get; set; }
        public int plant_id { get; set; }
        public List<SubProdOrderDetailVM> SubProd { get; set; }
        public List<item_lists> item_lists { get; set; }
    }
    public class item_lists
    {
        public int item_id { get; set; }
        public double consumed_qty { get; set; }
    }
}
