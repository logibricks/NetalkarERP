using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class mfg_tag_numbering_ref
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int tag_numbering_ref_id { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public string series { get; set; }
    }
}
