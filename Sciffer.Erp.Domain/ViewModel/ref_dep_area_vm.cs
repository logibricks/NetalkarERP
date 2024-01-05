using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Sciffer.Erp.Domain.Model;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class ref_dep_area_vm
    {
        public int dep_area_id  { get; set; }
        public string dep_area_code { get; set; }
        public string dep_area_description { get; set; }
        public int? dep_type_id { get; set; }
        public int? dep_posting_id { get; set; }
        public bool? is_blocked { get; set; }
        public string dep_type_code { get; set; }
        public string dep_type_frquency { get; set; }
        public decimal? rate_value { get; set; }
        public decimal? useful_value { get; set; }
        public string financial_year_id { get; set; }
        public int? frequency_id { get; set; }
        public int? no_of_periods { get; set; }
        public int? dep_type_frquency_id { get; set; }
        public string financial_year_id_selected { get; set; }
        public string dep_area_frequency { get; set; }
        public string dep_type { get; set; }
        public string dep_posting { get; set; }
        public string financial_year_name { get; set; }
        public string to_date { get; set; }
        public string dep_to_date { get; set; }
        public virtual List<ref_dep_posting_period_vm> ref_dep_posting_period_vm { get; set; }
        
    }
}
