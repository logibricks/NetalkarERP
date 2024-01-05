using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class inv_material_in
    {
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
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ge_date { get; set; }


        public int employee_id { get; set; }
        [ForeignKey("employee_id")]
        public virtual REF_EMPLOYEE REF_EMPLOYEE { get; set; }


        public int material_out_document_number { get; set; }
        public string vendor_invoice_no { get; set; }      
        public DateTime vendor_invoice_date { get; set; }
        public int? status_id { get; set; }
        public string cancellation_remarks { get; set; }
        public DateTime? cancelled_date { get; set; }
        public int? cancellation_reason_id { get; set; }
        public int? cancelled_by { get; set; }
        //public string mo_document_number { get; set; }
        public virtual ICollection<inv_material_in_detail> inv_material_in_detail { get; set; }
    }
}
