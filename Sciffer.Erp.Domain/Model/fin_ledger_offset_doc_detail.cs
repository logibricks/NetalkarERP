using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class fin_ledger_offset_doc_detail
    {
       [Key]
       public int fin_ledger_offset_doc_detail_id { get; set; }
       public int fin_ledger_detail_id { get; set; }
       public int setoff_fin_ledger_detail_id { get; set; }
       public double setoff_amount { get; set; }
       public double setoff_amount_local { get; set; }
       public bool is_active { get; set; }
    }
}
