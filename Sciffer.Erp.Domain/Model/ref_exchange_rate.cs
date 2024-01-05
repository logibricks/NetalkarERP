using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class ref_exchange_rate
    {
        [Key]
        public int exchange_rate_id { get; set; }
        [Required(ErrorMessage = "unit1 is required")]
        [Display(Name ="Unit1")]
        public double unit1 { get; set; }
        public int currency_id1 { get; set; }
        [ForeignKey("currency_id1")]
        public virtual REF_CURRENCY REF_CURRENCY1 { get; set; }
        [Required(ErrorMessage ="unit2 is required")]
        [Display(Name = "Unit2")]
        public double unit2 { get; set; }
        public int currency_id2 { get; set; }
        [ForeignKey("currency_id2")]
        public virtual REF_CURRENCY REF_CURRENCY2 { get; set; }
        [Required(ErrorMessage = "from date is required")]
        [Display(Name = "From Date")]
        [DataType(DataType.Date)]
       // [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime from_date { get; set; }
        public bool is_blocked { get; set; }
             
    }
    public class ref_exchangerate_vm
    {
        public int exchange_rate_id { get; set; }
        public double unit1 { get; set; }
        public double unit2 { get; set; }
        public int currency1 { get; set; }
        public int currency2 { get; set; }
        public string currency1_name { get; set; }
        public string currency2_name { get; set; }
        [DataType(DataType.Date)]      
        public DateTime from_date { get; set; }
        public bool is_blocked { get; set; }

    }
}
