using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_CURRENCY
    {
        [Key]
        public int CURRENCY_ID { get; set; }
        [Display(Name = "Currency Name")]
        [Required]
        public string CURRENCY_NAME { get; set; }
        [Display(Name ="Descripion")]
        [Required]
        public string CURRENCY_DESCRIPTION { get; set; }      
        public int CURRENCY_COUNTRY_ID { get; set; }
        [ForeignKey("CURRENCY_COUNTRY_ID")]
        public virtual REF_COUNTRY REF_COUNTRY { get; set; }
        public bool IS_ACTIVE { get; set; }
        public bool is_blocked { get; set; }
    }
    public class REF_CURRENCYVM
    {
        public int CURRENCY_ID { get; set; }            
        public string CURRENCY_NAME { get; set; }              
        public string CURRENCY_DESCRIPTION { get; set; }        
        public int CURRENCY_COUNTRY_ID { get; set; } 
        public bool IS_ACTIVE { get; set; }
        public bool is_blocked { get; set; }
        public string CountryName { get; set; }
    }
}
