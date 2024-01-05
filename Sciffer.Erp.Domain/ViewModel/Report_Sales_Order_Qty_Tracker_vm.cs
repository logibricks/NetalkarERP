using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Sales_Order_Qty_Tracker_vm
    {
        public string SO_Document_Category { get; set; }
        public string SO_Document_Num_ber { get; set; }
        public string Bill_to_Party_Code { get; set; }
        public string Bill_to_Party { get; set; }
        public string Plant { get; set; }
        public string Item_COde { get; set; }
        public string Item_Description { get; set; }
        public double SO_Qty { get; set; }
        public string Sales_Quotation_QTy { get; set; }
        public string SQ_Category { get; set; }
        public string SQ_Number { get; set; }
        public DateTime SQ_Posting_Date { get; set; }
        public double Invoice_Qty { get; set; }
        public string Invoice_Document_Category { get; set; }
        public string Invoice_Number { get; set; }
        public DateTime Invoice_Posting_Date { get; set; }

    }
}
