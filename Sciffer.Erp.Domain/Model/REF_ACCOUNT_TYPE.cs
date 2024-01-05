using System.ComponentModel.DataAnnotations;
namespace Sciffer.Erp.Domain.Model
{
    public class REF_ACCOUNT_TYPE
    {

        [Key]
        public int ACCOUNT_TYPE_ID { get; set; }
        public string ACCOUNT_TYPE_NAME { get; set; }

    }
    }