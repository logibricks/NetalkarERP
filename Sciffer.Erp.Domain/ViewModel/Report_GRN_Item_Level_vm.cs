using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_GRN_Item_Level_vm
    {
        public string Doc_Category { get; set; }
        public string Doc_Number { get; set; }
        public DateTime DOc_Date { get; set; }
        public string Vendor_COde { get; set; }
        public string vendor_Description { get; set; }
        public string Plant { get; set; }
        public string Item_Code { get; set; }
        public string Item_Description { get; set; }
        public string User_Description { get; set; }
        public string SLOC { get; set; }
        public string Batch { get; set; }
        public string Bucket { get; set; }
        public double Qty { get; set; }
        public string UoM { get; set; }
        public string Unit_Price { get; set; }
        public string Discount { get; set; }
        public string Effective_Unit_Price { get; set; }
        public double Purchase_Value { get; set; }

    }
}
