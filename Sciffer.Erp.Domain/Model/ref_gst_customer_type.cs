using System.ComponentModel.DataAnnotations;
namespace Sciffer.Erp.Domain.Model
{
   public class ref_gst_customer_type
    {
        [Key]
        public int gst_customer_type_id { get; set; }
        public string gst_customer_type_name { get; set; }
        public bool is_active { get; set; }
    }
}
