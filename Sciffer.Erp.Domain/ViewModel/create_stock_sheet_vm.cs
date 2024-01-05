using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class create_stock_sheet_vm
    {
        public int create_stock_sheet_id { get; set; }
        [Display(Name = "Category *")]
        public int category_id { get; set; }
        public string category_name { get; set; }
        public string document_no { get; set; }
        [Display(Name ="Item Category")]
        public int item_category_id { get; set; }

        [Display(Name = "Posting Date *")]
        [Required(ErrorMessage = "date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime document_date { get; set; }

        public string document_date1 { get; set;}
        [Display(Name = "Ref 1 ")]
        public string ref_1 { get; set; }

        [Display(Name = "Plant *")]
        public int plant_id { get; set; }
        public string plant_name { get; set; }
        [Display(Name = "Storage *")]
        public int sloc_id { get; set; }
        public string sloc_name { get; set; }
        [Display(Name = "Bucket *")]
        public int bucket_id { get; set; }
        public string bucket_name { get; set; }
        public bool is_active { get; set; }
        public int created_by { get; set; }
        public DateTime created_ts { get; set; }
        [Display(Name = "Status * ")]
        public int? status_id { get; set; }
        public  string status_name { get; set; }
        public int modify_by { get; set; }
        public DateTime modify_ts { get; set; }
        public int cancellation_reason_id { get; set; }
        public string cancellation_remarks { get; set; }
        public int cancelled_by { get; set; }
        public DateTime cancelled_ts { get; set; }

        public int create_stock_sheet_detail_id { get; set; }
        public string item_code { get; set; }
        public string UOM { get; set; }
        public string batch_number { get; set; }
        public double? actual_qty { get; set; }
        public double? stock_qty { get; set; }
        public double? diff_qty { get; set; }
        public double? rate { get; set; }
        public double? value { get; set; }
        public int item_id { get; set; }
        public int uom_id { get; set; }
    }

}
