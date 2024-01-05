using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class plan_maintenance_order_parameter_new_vm
    {
        public int plan_order_parameter_id { get; set; }
        public int parameter_id { get; set; }
        public string paramaterName { get; set; }
        public string range { get; set; }
        public string actual_result { get; set; }
        public string method_used { get; set; }
        public int? self_check { get; set; }
        public string document_reference { get; set; }
        public bool is_active { get; set; }
        public int sr_no { get; set; }
        public int plan_maintenance_order_id { get; set; }
        public int maintenance_detail_id { get; set; }
        public int? attended_by { get; set; }
        public string employeeName { get; set; }
    }
}