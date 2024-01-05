using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class fin_ledger_payment
    {
        [Key]
        public int fin_ledger_payment_id { get; set; }
        [Display(Name = "IN/OUT")]
        public int in_out { get; set; }
        [Display(Name = "Category *")]
        public int category_id { get; set; }
        [Display(Name = "Cash/Bank")]
        public int cash_bank { get; set; }
        [Display(Name = "Payment Type")]      
        public int payment_type_id { get; set; }
        [ForeignKey("payment_type_id")]
        public virtual REF_PAYMENT_TYPE REF_PAYMENT_TYPE { get; set; }       
        [Display(Name = "Bank Account")]        
        public int bank_account_id { get; set; } 
        [Display(Name = "Amount")]      
        public double payment_amount { get; set; }
        [Display(Name = "Currency")]        
        public int currency_id { get; set; }
        [ForeignKey("currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY { get; set; }
        [Display(Name = "Entity")]       
        public int entity_type_id { get; set; }
        [ForeignKey("entity_type_id")]
        public virtual REF_ENTITY_TYPE REF_ENTITY_TYPE { get; set; }
        [Display(Name = "Remark")]
        public string remarks { get; set; }
        [Display(Name = "Document no")]      
        public string document_no { get; set; }
        [Display(Name = "Payment Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]       
        public DateTime payment_date { get; set; }
        public virtual ICollection<fin_ledger_payment_detail> fin_ledger_payment_detail { get; set; }
        public string cancellation_remarks { get; set; }
        public int? cancellation_reason_id { get; set; }
        public int? status_id { get; set; }
        public virtual ref_status ref_status { get; set; }
        public DateTime? cancelled_date { get; set; }
        public int? cancelled_by { get; set; }
        public DateTime? created_ts { get; set; }
        public int? created_by { get; set; }
    }

}
