using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_employee_balance_detail
    {
        [Key]
        public int? employee_balance_detail_id { get; set; }
        public int? employee_balance_id { get; set; }
        [ForeignKey("employee_balance_id")]
        public virtual ref_employee_balance ref_employee_balance { get; set; }
        public int employee_id { get; set; }
        [ForeignKey("employee_id")]
        public virtual REF_EMPLOYEE REF_EMPLOYEE { get; set; }
        public string ref1 { get; set; }
        public string ref2 { get; set; }
        public string ref3 { get; set; }
        public DateTime doc_date { get; set; }
        public DateTime due_date { get; set; }
        public double amount { get; set; }
        public int amount_type_id { get; set; }
        [ForeignKey("amount_type_id")]
        public virtual ref_amount_type ref_amount_type { get; set;}
        public string line_remark { get; set; }
        public bool is_active { get; set; }

    }
}
