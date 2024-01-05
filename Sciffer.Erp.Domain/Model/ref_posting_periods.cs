using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public  class ref_posting_periods
    {
        [Key]
        public int posting_periods_id { get; set; }
        public int financial_year_id { get; set; }
        [ForeignKey("financial_year_id")]
        public virtual REF_FINANCIAL_YEAR REF_FINANCIAL_YEAR { get; set; }
        public int frequency_id { get; set; }
        [ForeignKey("frequency_id")]
        public virtual ref_frequency ref_frequency { get; set; }
        public int no_of_periods { get; set; }
        public bool is_active { get; set; }
        public int company_id { get; set; }
        public virtual ICollection<ref_posting_periods_detail> ref_posting_periods_detail { get; set; }
    }

    public class ref_posting_periods_detail
    {
        [Key]
        public int posting_periods_detail_id { get; set; }
        public int posting_periods_id { get; set; }
        [ForeignKey("posting_periods_id")]
        public virtual ref_posting_periods ref_posting_periods { get; set; }
        public string posting_periods_code { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime from_date { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime to_date { get; set; }
        public int? status_id { get; set; }
        [ForeignKey("status_id")]
        public virtual ref_status ref_status { get; set; }
    }
}
