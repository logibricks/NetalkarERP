using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class pla_transfer
    {
        [Key]
        public int pla_transfer_id { get; set; }
        [Required(ErrorMessage ="Number is required")]
        [Display(Name ="Number")]
        public string pla_transfer_number { get; set; }
        [Display(Name ="Posting Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage ="posting date is required")]
        [DataType(DataType.Date)]
        public DateTime pla_posting_date { get; set; }
        [Display(Name = "Document Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "document date is required")]
        [DataType(DataType.Date)]
        public DateTime pla_document_date { get; set; }
        [Required(ErrorMessage ="plant is required")]
        [Display(Name ="Plant")]
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        [Required(ErrorMessage ="sending sloc is required")]
        [Display(Name ="Sendig Sloc")]
        public int pla_send_sloc { get; set; }
        [ForeignKey("pla_send_sloc")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }
        [Required(ErrorMessage = "sending sloc is required")]
        [Display(Name = "Receiving Sloc")]
        public int pla_receive_sloc { get; set; }
        [ForeignKey("pla_receive_sloc")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION1 { get; set; }
        [Required(ErrorMessage ="send bucket is required")]
        [Display(Name ="Send Bucket")]
        public int pla_send_bucket { get; set; }
        [ForeignKey("pla_send_bucket")]
        public virtual ref_bucket ref_bucket{ get; set; }
        [Required(ErrorMessage ="receiving bucket is required")]
        [Display(Name ="Receive Bucket")]
        public int pla_receive_bucket { get; set; }
        [ForeignKey(" pla_receive_bucket")]
        public virtual ref_bucket ref_bucket1 { get; set; }
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual REF_CATEGORY REF_CATEGORY { get; set; }
        public bool is_active { get; set; }



        [Display(Name = "Remarks on Document")]
        [DataType(DataType.MultilineText)]
        public string remarks_on_document { get; set; }


        public string pla_attachment { get; set; }
        public virtual ICollection<pla_transfer_detail> pla_transfer_detail { get; set; }
        public virtual ICollection<pla_transfer_detail_tag> pla_transfer_detail_tag { get; set; }
      
    }
}
