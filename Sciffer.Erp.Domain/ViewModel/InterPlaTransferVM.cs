using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class InterPlaTransferVM
    {
        public int pla_transfer_id { get; set; }
        [Required(ErrorMessage = "Number is required")]
        [Display(Name = "Plant Transfer Number *")]
        public string pla_transfer_number { get; set; }
        [Display(Name = "Posting Date *")]
        [Required(ErrorMessage = "posting date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime pla_posting_date { get; set; }
        [Display(Name = "Document Date *")]
        [Required(ErrorMessage = "document date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime pla_document_date { get; set; }

        [Required(ErrorMessage = "plant is required")]
        [Display(Name = "Receiving Plant")]
        public int receiving_plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }

        public string receving_plant_name { get; set; }


        [Required(ErrorMessage = "plant is required")]
        [Display(Name = "Sending Plant")]
        public int sending_plant_id { get; set; }
        [ForeignKey("sending_plant_id")]
        public virtual REF_PLANT REF_PLANT1 { get; set; }

        public string sending_plant_name { get; set; }


        [Required(ErrorMessage = "sending sloc is required")]
        [Display(Name = "Sendig SLoc *")]
        public int pla_send_sloc { get; set; }

        public string send_sloc_name { get; set; }

        [Required(ErrorMessage = "sending sloc is required")]
        [Display(Name = "Receiving SLoc *")]
        public int pla_receive_sloc { get; set; }

        public string receive_sloc_name { get; set; }

        [Required(ErrorMessage = "send bucket is required")]
        [Display(Name = "Sending Bucket *")]
        public int pla_send_bucket { get; set; }

        [Required(ErrorMessage = "recieving bucke is required")]
        [Display(Name = "Receiving Bucket *")]
        public int pla_receive_bucket { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category *")]
        public int category_id { get; set; }
       

        [DataType(DataType.MultilineText)]
        public string pla_remark { get; set; }

        [Display(Name = "Remarks on Document")]
        [DataType(DataType.MultilineText)]
        public string remarks_on_document { get; set; }

        public virtual List<inter_pla_transfer_detail> inter_pla_transfer_detail { get; set; }
        public virtual List<inter_pla_transfer_detail_tag> inter_pla_transfer_detail_tag { get; set; }

        public List<string> pla_transfer_detail_id { get; set; }
        public List<string> pla_qty { get; set; }
        public List<string> uom_id { get; set; }
        public List<string> item_batch_id { get; set; }
        public List<string> item_id { get; set; }
        public List<string> sr_no { get; set; }
        public List<string> item_batch_detail_id { get; set; }
       
        public List<string> pla_transfer_detail_tag_id { get; set; }
        public List<string> tsr_no { get; set; }
        public List<string> tag_no { get; set; }
        public List<string> tag_id { get; set; }
        public List<string> quantity { get; set; }
        public List<string> titem_batch_id { get; set; }
        public List<string> titem_id { get; set; }
        public List<string> titem_batch_detail_id { get; set; }
        public List<string> tuom_id { get; set; }

        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }

        public string item_category_id { get; set; }
        public string pla_attachment { get; set; }

        public string item_code { get; set; }
        public string deleteids { get; set; }

        public string category { get; set; }
        public string send_bucket_name { get; set; }
        public string rcv_bucket_name { get; set; }
        public string sender_name { get; set; }
        public string sender_address { get; set; }
        public string PLANT_MOBILE { get; set; }

        public string reciver_name { get; set; }
        public string reciver_address { get; set; }
        public string gst_number { get; set; }
        public string posting_date { get; set; }
        public string ITEM_NAME { get; set; }
        public double plant_qty { get; set; }
        public double total_quantity { get; set; }
        public string cst_number { get; set; }


       
        public string plant_email { get; set; }
        public string state_name { get; set; }
        public string plant_name { get; set; }

    }
    public class inter_plant_detail_vm
    {
        public int pla_transfer_id { get; set; }
        public string ITEM_NAME { get; set; }
        public double plant_qty { get; set; }

    }
}
