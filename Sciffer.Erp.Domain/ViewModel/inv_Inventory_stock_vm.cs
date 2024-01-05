using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sciffer.Erp.Domain.Model;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class inv_Inventory_stock_vm
    {
        public int inventory_stock_id { get; set; }
        [Display(Name = "Category *")]
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }
        public string number { get; set; }
        [Display(Name = "Plant *")]
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        [Display(Name = "Posting Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }
        public string postingdate { get; set; }
        [Display(Name = "Sloc *")]
        public int sloc_id { get; set; }
        [ForeignKey("sloc_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }
        [Display(Name = "Document Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime document_date { get; set; }
        [Display(Name = "Bucket")]
        public int bucket_id { get; set; }
        [Display(Name = "Ref1")]
        public string ref1 { get; set; }
        public string slocname { get; set; }
        public string plant_name { get; set; }
        public string bucket_name { get; set; }
        public string company_name { get; set; }
        public string company_address { get; set; }
        public virtual List<inv_Inventory_stock> inv_Inventory_stock { get; set; }

        public List<string> inventory_stock_detail_id { get; set; }
        public List<string> item_id { get; set; }
        public List<string> item_code { get; set; }
        public List<string> item_batch_id { get; set; }
        public List<string> batch_number { get; set; }
        public List<string> uom_id { get; set; }
        public List<string> UOM_NAME { get; set; }
        public List<string> rowIndex1 { get; set; }
        public List<string> actual_qty { get; set; }

        public virtual List<inv_Inventory_stock_detail_VM> inv_Inventory_stock_detail_VM { get; set; }
    }
    public class GetItemForStock
    {
          
        public int item_batch_id { get; set; }
        public string batch_number { get; set; }
        public long rowIndex1 { get; set; }
        public int item_id { get; set; }
        public string item_code { get; set; }
        public int inventory_stock_detail_id { get; set; }
        public double actual_qty { get; set; }
        //public string ITEM_NAME { get; set; }
        //public string ITEM_CODE { get; set; }
        public int uom_id { get; set; }

        public string UOM_NAME { get; set; }

    }

    public class inv_Inventory_stock_detail_VM
    {

        public int inventory_stock_detail_id { get; set; }
        public int inventory_stock_id { get; set; }
        public int item_id { get; set; }
        public string item_name { get; set; }
        public int item_batch_id { get; set; }
        public string batch_number { get; set; }
        public int uom_id { get; set; }
        public string UOM_NAME { get; set; }
        public double actual_qty { get; set; }
        public long rowIndex { get; set; }
    }



}
