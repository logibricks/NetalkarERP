using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class fin_credit_debit_note_report_vm
    {
        public int fin_credit_debit_node_id { get; set; }
        public string document_no { get; set; }
        public string state_name { get; set; }
        public string bill_state_name { get; set; }
        public string entity_name { get; set; }
    }
}
