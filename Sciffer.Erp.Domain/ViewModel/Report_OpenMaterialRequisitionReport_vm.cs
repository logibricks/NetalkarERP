using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_OpenMaterialRequisitionReport_vm
    {
        public string Category { get; set; }
        public string Doc_Number { get; set; }
        public DateTime Doc_Date { get; set; }
        public string Type { get; set; }
        public string Item_Code { get; set; }
        public double Required_Qty { get; set; }
        public double issued_Qtya { get; set; }
        public double balance_Qty { get; set; }
        public string Ageing { get; set; }

    }
}
