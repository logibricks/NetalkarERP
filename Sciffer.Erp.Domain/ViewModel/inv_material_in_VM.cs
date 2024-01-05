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
    public class inv_material_in_VM
    {
        //public List<inv_material_in_detail_VM> inv_material_in_detail_VM;

        [Key]
        public int material_in_id { get; set; }
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }
        public string document_number { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }

        public int vendor_id { get; set; }
        [ForeignKey("vendor_id")]
        public virtual REF_VENDOR REF_VENDOR { get; set; }

        public int business_unit_id { get; set; }
        [ForeignKey("business_unit_id")]
        public virtual REF_BUSINESS_UNIT REF_BUSINESS_UNIT { get; set; }

        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }

        public string ge_number { get; set; }
        public int material_out_id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ge_date { get; set; }

        public string item_name { get; set; }
        public string UOM_NAME { get; set; }
        public string vendor_name { get; set; }
        public string plant_name { get; set; }
        public string busienss_unit { get; set; }
        public string employee_name { get; set; }
        public string company_name { get; set; }
        public string company_address { get; set; }
        public string posting_date11 { get; set; }
        public string ge_date11 { get; set; }
        public string vendor_invoice_no { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? vendor_invoice_date { get; set; }
        public int employee_id { get; set; }
        [ForeignKey("employee_id")]
        public virtual REF_EMPLOYEE REF_EMPLOYEE { get; set; }

        public int material_out_document_number { get; set; }

        public string mo_document_number { get; set; }


        public List<string> material_in_detail_id { get; set; }
        public List<string> item_id { get; set; }
        public List<string> user_description { get; set; }
        public List<string> uom_id { get; set; }
        public List<string> quantity { get; set; }
        public List<string> reason { get; set; }
        public List<DateTime> er_date { get; set; }
        public List<string> Sr_No { get; set; }
        public List<string> balance_qty { get; set; }
        public List<string> material_out_detail_id { get; set; }

        public virtual List<inv_material_in_detail_VM> inv_material_in_detail_VM { get; set; }
        public virtual IList<inv_material_in_detail> inv_material_in_detail { get; set; }
        //public virtual List<inv_material_in_detail> inv_material_in_detail { get; set; }
        public int status_id { get; set; }
        public string status { get; set; }
    }

    public class GetMaterialInforVendor
    {
        public int material_in_id { get; set; }
        public int? category_id { get; set; }
        public string document_number { get; set; }
        public DateTime posting_date { get; set; }
        public int? business_unit_id { get; set; }
        public int? plant_id { get; set; }
        public DateTime ge_date { get; set; }
        public string ge_number { get; set; }
        public int? employee_id { get; set; }
        public double? quantity { get; set; }
        public double balance_qty { get; set; }

        public string item_name { get; set; }
        public string UOM_NAME { get; set; }
        public string vendor_name { get; set; }
        public string plant_name { get; set; }
        public string busienss_unit { get; set; }
        public string employee_name { get; set; }

        public int? material_out_detail_id { get; set; }
        public int? item_id { get; set; }
        
        public int? uom_id { get; set; }
        public int? vendor_id { get; set; }

        public long rowIndex { get; set;}

        public string reason { get; set; }
        public string er_date { get; set; }
        public string user_description { get; set; }

    }
    public class GetMOList
    {
        public int material_out_id { get; set; }
        public int category_id { get; set; }
        public string document_number { get; set; }
       
    }


    public class inv_material_in_detail_VM
    {

        public int material_in_detail_id { get; set; }
        public int material_in_id { get; set; }
        public int item_id { get; set; }
        public string ITEM_NAME { get; set; }
        public string user_description { get; set; }
        public int uom_id { get; set; }
        public string UOM_NAME { get; set; }
        public double quantity { get; set; }
        public string reason { get; set; }
        public string er_date { get; set; }
        public double balance_qty { get; set; }
        public int mo_document_number { get; set; }

    }
}
