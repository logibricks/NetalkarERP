﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Purchase_Invoice_Header__vm: Report_Purchase_Invoice_Item_Level_vm
    {
        public double Nett_Value { get; set; }
        public double Gross_Value { get; set; }
        public string Business_Unit { get; set; }
        public string Freight_Terms { get; set; }
        public string PO_No { get; set; }
        public DateTime PO_Date { get; set; }
        public string Form { get; set; }
        public string Payment_Cycle_Type { get; set; }
        public string Payment_Cycle { get; set; }
        public string Payment_Terms { get; set; }

    }
}