using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
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
    public class pla_transferVM
    {
        [Key]
        public int pla_transfer_id { get; set; }
        public bool is_active { get; set; }
       
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
        [Display(Name = "Plant *")]
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }

        [Required(ErrorMessage = "sending sloc is required")]
        [Display(Name = "Sendig SLoc *")]
        public int pla_send_sloc { get; set; }
        [ForeignKey("pla_send_sloc")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }

        [Required(ErrorMessage = "sending sloc is required")]
        [Display(Name = "Receiving SLoc *")]
        public int pla_receive_sloc { get; set; }
        [ForeignKey("pla_receive_sloc")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION1 { get; set; }

        [Required(ErrorMessage ="send bucket is required")]
        [Display(Name ="Sending Bucket *")]
        public int pla_send_bucket { get; set; }

        [Required(ErrorMessage ="recieving bucke is required")]
        [Display(Name ="Receiving Bucket *")]
        public int pla_receive_bucket { get; set; }

        [Required(ErrorMessage ="Category is required")]
        [Display(Name = "Category *")]
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual REF_CATEGORY REF_CATEGORY { get; set; }

        public virtual List<pla_transfer_detail> pla_transfer_detail { get; set; }

        public List<string> pla_transfer_detail_id { get; set; }
        public List<string> item_id { get; set; }
        public List<string> uom_id { get; set; }
        public List<string> sloc_id { get; set; }
        public List<string> bucket_id { get; set; }
        public List<string> rate { get; set; }
        public List<string> issue_quantity { get; set; }
        public List<string> value { get; set; }


        public List<string> pla_transfer_batch_id { get; set; }
        public List<string> batch_item_id { get; set; }
        public List<string> batch_id { get; set; }
        public List<string> batch_number { get; set; }
        public List<string> issue_batch_quantity { get; set; }


        [Display(Name = "Remarks on Document")]
        [DataType(DataType.MultilineText)]
        public string remarks_on_document { get; set; }

        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }

        public string item_category_id { get; set; }



        public string pla_attachment { get; set; }
     
        public string item_code { get; set; }
        public string deleteids { get; set; }
        public virtual List<inter_pla_transfer_detail_vm> inter_pla_transfer_detail_vm { get; set; }
    }
    public class PlantTransferVM
    {
        public int pla_transfer_id { get; set; }
        public string pla_transfer_number { get; set; }
        public DateTime pla_posting_date { get; set; }
        public DateTime pla_document_date { get; set; }
        public string Plant { get; set; }
        public string pla_send_sloc { get; set; }
        public string pla_receive_sloc { get; set; }
        public string pla_send_bucket { get; set; }
        public string pla_receive_bucket { get; set; }
        public string category { get; set; }
        public string pla_remark { get; set; }
        public string pla_attachment { get; set; }
        public string remarks_on_document { get; set; }

        public string item_category_id { get; set; }
       
    }
    public class GetTagForPlaTransfer
    {
        public int tag_id { get; set; }
        public string tag_no { get; set; }
        public double qty { get; set; }
        public double balance_qty { get; set; }
        public int item_batch_id { get; set; }
        public string batch_number { get; set; }
        public string item_category_name { get; set; }
        public double tag_qty { get; set; }
        public double tag_balance_qty { get; set; }
        public int item_batch_detail_id { get; set; }
        public long rowIndex { get; set; }
        public long rowIndex1 { get; set; }
        public int? item_id { get; set; }
        public string item_name { get; set; }

    }
    public class inter_pla_transfer_detail_vm
    {
        public int pla_transfer_detail_id { get; set; }
        public int item_id { get; set; }
        public string item_code { get; set; }
        public string item_desc { get; set; }
        public string uom_name { get; set; }
        public string sloc_name { get; set; }
        public string bucket_name { get; set; }
        public string rate { get; set; }
        public string issue_quantity { get; set; }
        public string value { get; set; }
    }

}

