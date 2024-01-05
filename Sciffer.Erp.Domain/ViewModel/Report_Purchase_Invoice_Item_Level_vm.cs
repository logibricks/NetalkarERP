using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Purchase_Invoice_Item_Level_vm
    {
        public string Doc_Category { get; set; }
        public string Doc_Number { get; set; }
        public DateTime DOc_Date { get; set; }
        public string Vendor_COde { get; set; }
        public string vendor_Description { get; set; }
        public string Item_Service { get; set; }
        public string Plant { get; set; }
        public string Item_Code { get; set; }
        public string Item_Description { get; set; }
        public string User_Description { get; set; }
        public string SLOC { get; set; }
        public DateTime Delivery_Date { get; set; }
        public double Qty { get; set; }
        public string UoM { get; set; }
        public double Unit_Price { get; set; }
        public double Discount { get; set; }
        public double Effective_Unit_Price { get; set; }
        public double Purcase_Value { get; set; }
        public double Assesaable_Rate { get; set; }
        public double Assessable_value { get; set; }
        public string GL_Code { get; set; }
        public string tax_code { get; set; }
        public string tax_descriptiption { get; set; }
        public string tax_element_1 { get; set; }
        public string tax_element_2 { get; set; }
        public string tax_element_3 { get; set; }
        public string total_taxes { get; set; }
        public double net_total { get; set; }

    }
}
