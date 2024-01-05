using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public  class ref_send_mail
    {
        [Key]
        public int send_mail_id { get; set; }
        public string send_from { get; set; }
        public string send_password { get; set; }
        public string name { get; set; }
        public int port_name { get; set; }
        public bool EnableSsl { get; set; }
        public string host_name { get; set; }
       
    }
}
