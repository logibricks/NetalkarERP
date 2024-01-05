using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class post_variance_vm
    {

        [DataType(DataType.Date)]
        [Display(Name = "Posting Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Document Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime document_date { get; set; }
        public int post_variances_id { get; set; }
        [Display(Name = "Category")]
        public int category_id { get; set; }
        [Display(Name = "Doc Number")]
        public string doc_number { get; set; }
        [Display(Name = "Plant")]
        public int plant_id { get; set; }
        [Display(Name = "SLoc")]
        public int sloc_id { get; set; }
        [Display(Name = "Bucket")]
        public int bucket_id { get; set; }
        [Display(Name = "Reference 1")]
        public string ref1 { get; set; }
        [Display(Name = "Update Stock Count")]
        public int update_stock_count_id { get; set; }
        [Display(Name = "Stock Sheet")]
        public int create_stock_sheet_id { get; set; }
        public bool is_active { get; set; }
        public int created_by { get; set; }
        public DateTime created_ts { get; set; }
        public int modify_by { get; set; }
        public DateTime modify_ts { get; set; }
        public int cancelled_by { get; set; }
        public DateTime cancelled_ts { get; set; }
        public int cancellation_reason_id { get; set; }
        public string cancellaion_remarks { get; set; }
        public HttpPostedFileBase file { get; set; }
        [Display(Name ="Item Category")]
        public int item_category_id { get; set; }
        public List<int> post_variances_detail_id { get; set; }
        public List<int> item_id { get; set; }
        public List<int> uom_id { get; set; }
        public List<string> batch_number { get; set; }
        public List<double> stock_qty { get; set; }
        public List<double> actual_qty { get; set; }
        public List<double> diff_qty { get; set; }
        public List<double> rate { get; set; }
        public List<double> value { get; set; }
        public List<int> create_stock_sheet_detail_id { get; set; }
        public string document_date1 { get; set; }
        public string posting_date1 { get; set; }
        public string plant_name { get; set; }
        public string sloc_name { get; set; }
        public string bucket_name { get; set; }
        public string status_name { get; set; }
        public int status_id { get; set; }
    }
}
