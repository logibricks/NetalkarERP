using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_value_slab_po_approval
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int User_id { get; set; }

        public double From_Value_Slab { get; set; }

        public double To_Value_Slab { get; set; }

        public bool is_active { get; set; }
    }
}
