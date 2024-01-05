using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.ViewModel
{
    public  class ref_posting_periods_vm
    {
        [Key]
        public int posting_periods_id { get; set; }
        public int financial_year_id { get; set; }
        public int frequency_id { get; set; }
        public int no_of_periods { get; set; }
        public bool is_active { get; set; }
        public virtual List<ref_posting_periods_detail> ref_posting_periods_detail { get; set; }
        public string codelist { get; set; }
    }
    public class posting_periods
    {
        public int posting_periods_detail_id { get; set; }
        public int posting_periods_id { get; set; }
        public int financial_year_id { get; set; }
        public string financial_year { get; set; }
        public int frequency_id { get; set; }
        public string frequency { get; set; }
        public int no_of_periods { get; set; }
        public DateTime from_date { get; set; }
        public DateTime to_date { get; set; }
        public int status_id { get; set; }
        public string status_name { get; set; }
        public string posting_periods_code { get; set; }
    }
}
