using Sciffer.Erp.Domain.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Sciffer.Erp.Domain.Model
{
 public   class ref_instruction_type
    {
        [Key]
        public int instruction_type_id { get; set; }
        public string instruction_name { get; set; }
        public bool? is_active { get; set; }
    }
}
