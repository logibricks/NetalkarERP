using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_batch_numbering
    {
        [Key]
        public int batch_no_id { get; set; }
        [Required(ErrorMessage = "Plant name is required.")]
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        [Required(ErrorMessage = "Category name is required.")]
        public int item_category_id { get; set; }
        [ForeignKey("item_category_id")]
        public virtual REF_ITEM_CATEGORY REF_ITEM_CATEGORY { get; set; }
        public int prefix_sufix_id { get; set; }
        public string prefix_sufix { get; set; }
        public string to_number { get; set; }
        public string from_number { get; set; }
        public string last_used { get; set; }
        public bool is_blocked { get; set; }
        public bool is_active { get; set; }
        public int financial_year_id { get; set; }
        [ForeignKey("financial_year_id")]
        public virtual REF_FINANCIAL_YEAR REF_FINANCIAL_YEAR { get; set; }
    }

    public class batch_numbering_VM
    {
        
        public int batch_no_id { get; set; }        
        public int plant_id { get; set; }
        public int item_category_id { get; set; }
        public int prefix_sufix_id { get; set; }
        public string prefix_sufix_name { get; set; }
        public string prefix_sufix { get; set; }
        public string to_number { get; set; }
        public string from_number { get; set; }
        public string last_used { get; set; }
        public bool is_blocked { get; set; }
        public bool is_active { get; set; }
        public string item_category_name { get; set; }
        public string plant_name { get; set; }
        public string financial_year_name { get; set; }
        public int financial_year_id { get; set; }
    }
}
