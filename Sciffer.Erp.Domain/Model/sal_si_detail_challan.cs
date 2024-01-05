using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class sal_si_detail_challan
    {
        [Key]
        public int si_detail_challan_id { get; set; }
        public int si_detail_id { get; set; }
        [ForeignKey("si_detail_id")]
        public virtual sal_si_detail sal_si_detail { get; set; }
        public int job_work_detail_in_id { get; set; }
        [ForeignKey("job_work_detail_in_id")]
        public virtual in_jobwork_in_detail in_jobwork_in_detail { get; set; }
        public double quantity { get; set; }
        public double bal_quantity { get; set; }
    }
}
