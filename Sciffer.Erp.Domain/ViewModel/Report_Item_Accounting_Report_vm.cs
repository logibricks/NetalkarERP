using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Item_Accounting_Report_vm
    {
        public string Item_Code { get; set; }
        public string Item_Description { get; set; }
        public string Item_Type { get; set; }
        public string Item_Category { get; set; }
        public string Item_group { get; set; }
        public string Item_valuation { get; set; }
        public string Item_Accounting { get; set; }
        public string Item_valuation1 { get; set; }
        public string Inventory_Account { get; set; }
        public string Consumption_Account { get; set; }
        public string Revaluation_Account { get; set; }
        public string COGP_Account { get; set; }
        public string COGS_Account { get; set; }
        public string Scrap_P_L_Account { get; set; }
        public string Price_Difference_Account { get; set; }
        public string Stock_Differences_Account { get; set; }
        public string By_Product_P_L_Account { get; set; }
        public string GRIR_Clearing_Account { get; set; }
        public string Sales_GL { get; set; }
        public string Sales_Return_GL { get; set; }
        public string Costing_Difference { get; set; }

    }
}
