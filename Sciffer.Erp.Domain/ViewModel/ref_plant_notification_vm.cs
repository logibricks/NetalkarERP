using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
   public class ref_plant_notification_vm
    {
        [Key]
        public int plant_notification_id { get; set; }
        public int category_id { get; set; }
        public string category_name { get; set; }
        public string doc_number { get; set; }
        public string notification_type { get; set; }
        public DateTime notification_date { get; set; }
        public int plant_id { get; set; }
        public string plant_name { get; set; }
        public string machine_code { get; set; }
        public DateTime pl_breakdown_start_date { get; set; }
        public DateTime pl_breakdown_end_date { get; set; }
        public TimeSpan pl_breakdown_start_time { get; set; }
        public TimeSpan pl_breakdown_end_time { get; set; }
        public DateTime? a_breakdown_start_date { get; set; }
        public DateTime? a_breakdown_end_date { get; set; }
        public TimeSpan? a_breakdown_start_time { get; set; }
        public TimeSpan? a_breakdown_end_time { get; set; } 
        public string order_id { get; set; }
        public int created_by { get; set; }
        public DateTime created_ts { get; set; }
        public string notification_for { get; set; }
    }
}
