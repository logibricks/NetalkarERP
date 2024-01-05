using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_production_order
    {
        [Key]
        public int prod_order_id { get; set; }
        [Display(Name = "Production Order No.")]
        public string prod_order_no { get; set; }
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }

        [Display(Name = "Planned Start Date")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Planned start date is required")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime planned_start_date { get; set; }

        [Display(Name = "Planned End Date")]
        [Required(ErrorMessage = "Planned end date is required")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime planned_end_date { get; set; }

        public int shift_start { get; set; }
        [ForeignKey("shift_start")]
        public virtual ref_shifts ref_shifts_start { get; set; }
        public int shift_end { get; set; }
        [ForeignKey("shift_start")]
        public virtual ref_shifts ref_shifts_end { get; set; }

        public DateTime posting_date { get; set; } 
        public string ref1 { get; set; }
        public int? so_id { get; set; }
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }
        public double planned_output_quantity { get; set; }

        public bool create_sub_order { get; set; }
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        public int storage_location_id { get; set; }
        [ForeignKey("storage_location_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }
        public bool short_close { get; set; }
        public bool apply_bom { get; set; }
        public int bom_id { get; set; }
        public bool backflush_consumption { get; set; }
        public string version { get; set; }
        public int? order_status_id { get; set; }
        [ForeignKey("order_status_id")]
        public virtual ref_status ref_status { get; set; }
        public string remarks { get; set; }
        [Display(Name = "Attachment")]
        public string attachement { get; set; }
        public double balance_quantity { get; set; }
        public virtual ICollection<ref_production_order_detail> ref_production_order_detail { get; set; }
    }
}
