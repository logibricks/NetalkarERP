using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_mode_of_transport
    {
        [Key]
        public int mode_of_transport_id { get; set; }
        public string mode_of_transport_name { get; set; }
    }
}
