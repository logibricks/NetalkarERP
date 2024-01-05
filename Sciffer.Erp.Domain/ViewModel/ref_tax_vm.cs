using Sciffer.Erp.Domain.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class ref_tax_vm
    {
        public int tax_id { get; set; }
        [Display(Name = "Tax Name *")]
        public string tax_name { get; set; }
        [Display(Name = "Tax Code *")]
        public string tax_code { get; set; }
        [Display(Name = "Block")]
        public bool is_blocked { get; set; }
        public virtual List<ref_tax_detail> ref_tax_detail { get; set; }
        public string taxdetail { get; set; }
        public string deleteids { get; set; }
        public string tax_name_code { get; set; }
    }

    public class tax_vm
    {
        public string tax_name { get; set; }
        [Range(0, 9999999999999999.99)]
        public decimal tax_value { get; set; }
    }
    public class credit_debit_vm
    {
        public int tax_type_id { get; set; }
        public string gl_ledger_name { get; set; }
        public decimal? debit { get; set; }
        public decimal? credit { get; set; }
    }
}
