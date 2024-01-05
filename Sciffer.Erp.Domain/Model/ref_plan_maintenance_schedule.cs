using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sciffer.Erp.Domain.Model
{
    public class ref_plan_maintenance_schedule
    {
        [Key]
        public Int64 plan_maintenance_schedule_id { get; set; }
        public string order_no { get; set; }
      //  public int sr_no { get; set; }

        //public int document_numbering_id { get; set; }
        //[ForeignKey("document_numbering_id")]
        //public virtual ref_document_numbring ref_document_numbring { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? order_date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime schedule_date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? completion_date { get; set; }

        public int status_id { get; set; }
        [ForeignKey("status_id")]
        public virtual ref_status ref_status { get; set; }

        public int plan_maintenance_id { get; set; }
        [ForeignKey("plan_maintenance_id")]
        public virtual ref_plan_maintenance ref_plan_maintenance { get; set; }

        public bool is_active { get; set; }
    }
    public class ref_plan_maintenance_schedules
    {
        public Int64 plan_maintenance_schedule_id { get; set; }
        public string order_no { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? order_date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime schedule_date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? completion_date { get; set; }
        public int status_id { get; set; }
        public int plan_maintenance_id { get; set; }

        public bool is_active { get; set; }
        public string status { get; set; }
    }
}
