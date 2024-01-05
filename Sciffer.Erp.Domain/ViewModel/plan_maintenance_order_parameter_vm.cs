using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class plan_maintenance_order_parameter_vm
    {
        public Int64 rowIndex { get; set; }


        public string parameter { get; set; }

        public string range { get; set; }

        public string actual_result { get; set; }

        public string method_used { get; set; }

        public string self_check { get; set; }

        public string document_reference { get; set; }

        public string attended_by { get; set; }

        public int? plan_order_parameter_id { get; set; }

    }
}
