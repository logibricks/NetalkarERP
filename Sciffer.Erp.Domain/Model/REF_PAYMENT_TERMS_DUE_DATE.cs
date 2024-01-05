using System.ComponentModel.DataAnnotations;
namespace Sciffer.Erp.Domain.Model
{
    public class REF_PAYMENT_TERMS_DUE_DATE
    {

        [Key]
        public int PAYMENT_TERMS_DUE_DATE_ID { get; set; }
        public string PAYMENT_TERMS_DUE_DATE_NAME { get; set; }
    }
}