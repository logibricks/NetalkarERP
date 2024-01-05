using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_PO_to_Payment_Tracker_vm
    {
        public string PO_Doc_Category { get; set; }
        public string PO_Doc_Number { get; set; }
        public DateTime PO_Posting_Date { get; set; }
        public DateTime Scheduled_Delivery_Date { get; set; }
        public string GRN_Doc_Category { get; set; }
        public string GRN_Doc_Number { get; set; }
        public DateTime GRN_Posting_Date { get; set; }
        public string IEX_Doc_Category { get; set; }
        public string IEX_Doc_Number { get; set; }
        public DateTime IEX_Posting_Date { get; set; }
        public string QC_Doc_Category { get; set; }
        public string QC_Doc_Number { get; set; }
        public DateTime QC_Posting_Date { get; set; }
        public string PI_Doc_Category { get; set; }
        public string PI_Doc_Number { get; set; }
        public DateTime PI_Posting_Date { get; set; }
        public DateTime Payment_1_Date { get; set; }
        public DateTime Payment_2_Date { get; set; }
        public DateTime Payment_3_Date { get; set; }
        public DateTime Payment_4_Date { get; set; }

    }
}
