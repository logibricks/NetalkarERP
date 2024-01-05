using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_bucket
    {
        [Key]
        public int bucket_id { get; set; }
        public string bucket_name { get; set; }
        public bool is_active { get; set; }
    }
}
