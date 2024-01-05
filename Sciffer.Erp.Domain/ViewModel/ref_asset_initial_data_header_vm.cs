using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
   public class ref_asset_initial_data_header_vm
    {
        public int asset_initial_data_header_id { get; set; }
        [Display(Name = "Category")]
        public int? document_category_id { get; set; }
        public string document_no { get; set; }
        [Display(Name = "Posting Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? posting_date { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_ts { get; set; }
        public bool is_active { get; set; }
        public string category { get; set; }
        public string posting_dates { get; set; }

        public List<ref_asset_initial_data_vm> ref_asset_initial_data_vm { get; set; }
    }
}
