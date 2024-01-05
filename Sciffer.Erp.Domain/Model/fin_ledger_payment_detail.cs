using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class fin_ledger_payment_detail
    {
        [Key]        
        public int fin_ledger_payment_detail_id { get; set; }
        public int fin_ledger_payment_id { get; set; }          
        public int entity_id { get; set; }       
        public string tran_ref_no { get; set; }
        public double amount { get; set; }
        public double local_amount { get; set; }
    }
}
