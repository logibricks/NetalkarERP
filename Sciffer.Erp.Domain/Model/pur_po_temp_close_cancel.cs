using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class pur_po_temp_close_cancel
    {
        [Key]
        public int Id { get; set; }

        public int po_id { get; set; }

        public int order_status_cancellation_reason_id { get; set; }

        public int status_id { get; set; }

        public string Remarks { get; set; }

        public int modified_by { get; set; }

        public DateTime modified_ts { get; set; }
    }
}
