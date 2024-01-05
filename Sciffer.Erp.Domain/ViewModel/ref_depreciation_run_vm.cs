using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
   public class ref_depreciation_run_vm
    {
      
        public int depreciation_run_id { get; set; }
        [Display(Name = "Doc Category *")]
        public int document_numbering_id { get; set; }
        public string document_no { get; set; }
        [Display(Name = "Depreciation Area *")]
        public int dep_area_id { get; set; }
        [Display(Name = "Frequency *")]
        public int dep_frequency_id { get; set; }
        [Display(Name = "Financial Year *")]
        public int dep_financial_year_id { get; set; }
        [Display(Name = "Posting Date For Last Run *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? posting_for_last_run_date { get; set; }
        [Display(Name = "Current Depreciation Till  *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? current_dep_till_date { get; set; }
        [Display(Name = "Asset Class *")]
        public int asset_class_id { get; set; }
        public bool is_active { get; set; }
    }
}
