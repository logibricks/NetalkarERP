using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_machine
    {
        [Key]
        public int machine_id { get; set; }
        public int machine_category_id { get; set; }
        public string machine_code { get; set; }
        public string machine_name { get; set; }
        public int? machine_parent_code { get; set; }
        public int status_id { get; set; }
        [ForeignKey("status_id")]
        public virtual ref_status ref_status { get; set; }
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }

        public int? length_uom_id { get; set; }
        [ForeignKey("length_uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }
        public int? breadth_uom_id { get; set; }
        [ForeignKey("breadth_uom_id")]
        public virtual REF_UOM REF_UOM1 { get; set; }
        public int? weight_uom_id { get; set; }
        [ForeignKey("weight_uom_id")]
        public virtual REF_UOM REF_UOM2 { get; set; }
        public int? height_uom_id { get; set; }
        [ForeignKey("height_uom_id")]
        public virtual REF_UOM REF_UOM3 { get; set; }
       
       public string location { get; set; }

        public int? acquisition_value { get; set; }
        
        public int? currency_id { get; set; }
        [ForeignKey("currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY { get; set; }

        public string purchase_order_id { get; set; }

        public string manufacturer { get; set; }

        public string manufacturer_part_no { get; set; }
        public int? manufacturing_country_id { get; set; }
        [ForeignKey("manufacturing_country_id")]
        public virtual REF_COUNTRY REF_COUNTRY { get; set; }
        public int? priority_id { get; set; }
        [ForeignKey("priority_id")]
        public virtual REF_PRIORITY REF_PRIORITY { get; set; }

        [Display(Name = "Acquisition Date")]        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? acquisition_date { get; set; }

        public int? purchasing_vendor { get; set; }
        [ForeignKey("purchasing_vendor")]
        public virtual REF_VENDOR REF_VENDOR { get; set; }

        public string model_no { get; set; }
        public string manufacturing_serial_number { get; set; }
       
        [Display(Name = "Manufacturing Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? manufacturing_date { get; set; }

        [Display(Name = "Blocked ?")]
        public bool is_blocked { get; set; }
        public int? business_area { get; set; }
        [ForeignKey("business_area")]
        public virtual REF_BUSINESS_UNIT REF_BUSINESS_UNIT { get; set; }

        public int? cost_center { get; set; }
        [ForeignKey("cost_center")]
        public virtual ref_cost_center ref_cost_center { get; set; }

        public string asset_code_id { get; set; }
        public string asset_tag_no { get; set; }
        public bool warranty_applicable { get; set; }
        [Display(Name = "Warranty start date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? warranty_start_date { get; set; }

        [Display(Name = "Warranty end date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? warranty_end_date { get; set; }

        public bool guarantee_applicable { get; set; }

        [Display(Name = "Guarantee start date")]        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? guarantee_start_date { get; set; }


        [Display(Name = "Guarantee end date")]       
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? guarantee_end_date { get; set; }
        public string remarks { get; set; }

        [Display(Name = "Attachment")]
        public string attachement { get; set; }
       
        public int? amc_vendor { get; set; }
        [ForeignKey("amc_vendor")]
        public virtual REF_VENDOR REF_VENDOR1 { get; set; }
        public int? repairs_vendor { get; set; }
        [ForeignKey("repairs_vendor")]
        public virtual REF_VENDOR REF_VENDOR2 { get; set; }
        public bool is_active { get; set; }

        public string mac_address { get; set; }

        public int? length { get; set; }
        public int? breadth { get; set; }
        public int? height { get; set; }
        public int? weight { get; set; }

        public virtual ICollection<ref_machine_details> ref_machine_details { get; set; }


    }
}
