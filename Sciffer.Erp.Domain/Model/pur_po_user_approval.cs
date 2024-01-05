using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
   public class pur_po_user_approval
    {
        [Key]
        [Column(Order = 0)]
        public int po_id { get; set; }
        [Key]
        [Column(Order = 1)]
        public int user_id { get; set; }
    }
}
