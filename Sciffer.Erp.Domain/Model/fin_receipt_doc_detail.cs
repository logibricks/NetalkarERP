using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class fin_receipt_doc_detail
    {
       [Key]
       public int receipt_doc_detail_id { get; set; }
       public int doc_type_id { get; set; }
       public int doc_no { get; set; }
       public double amount { get; set; }
       public int entity_id { get; set; }
    }
}
