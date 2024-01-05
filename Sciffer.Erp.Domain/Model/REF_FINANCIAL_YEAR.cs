using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_FINANCIAL_YEAR
    {
        [Key]
        public int FINANCIAL_YEAR_ID { get; set; }
        [Display(Name ="Financial Year")]
        public string FINANCIAL_YEAR_NAME { get; set; }
        [Display(Name ="From")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime FINANCIAL_YEAR_FROM { get; set; }
        [Display(Name = "To")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime FINANCIAL_YEAR_TO { get; set; }
        public bool is_active { get; set; }
       
    }
}
