using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class map_operation_operator
    {
        [Key]
        [Column(Order = 0)]
        public int operation_id { get; set; }
        [ForeignKey("operation_id")]
        public virtual ref_mfg_process ref_mfg_process { get; set; }
        [Key]
        [Column(Order = 1)]
        public int operator_id { get; set; }
        [ForeignKey("operator_id")]
        public virtual ref_user_management ref_user_management { get; set; }
        public bool is_primary { get; set; }
    }
}
