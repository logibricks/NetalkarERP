using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_mfg_nc_status
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int nc_status_id { get; set; }
        public string nc_status_code { get; set; }
        public string nc_status_desc { get; set; }
    }
}
