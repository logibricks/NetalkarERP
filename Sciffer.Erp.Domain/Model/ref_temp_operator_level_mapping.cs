using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_temp_operator_level_mapping
    {
        [Key]
        public int temp_operator_level_mapping_id { get; set; }

        [Display(Name = "Select Operator *")]
        public int operator_id { get; set; }

        [Display(Name = "Select Level *")]
        public int level_id { get; set; }

        [Display(Name = "Select Machine *")]
        public int machine_id { get; set; }

        [Display(Name = "Select Shift *")]
        public int shift_id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime effective_date { get; set; }
        public bool is_active { get; set; }

        [Display(Name = "Block ")]

        public bool is_blocked { get; set; }
        public int created_by { get; set; }
        public DateTime created_ts { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_ts { get; set; }

    }
}
