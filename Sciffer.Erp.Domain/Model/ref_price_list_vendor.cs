using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sciffer.Erp.Domain.Model;
using System;
using System.Web;
namespace Sciffer.Erp.Domain.Model
{
    public class ref_price_list_vendor
    {
        [Key]
        public int price_list_id { get; set; }
        public int vendor_id { get; set; }
        [ForeignKey("vendor_id")]
        public virtual REF_VENDOR REF_VENDOR { get; set; }
        public bool is_active { get; set; }
        public virtual ICollection<ref_price_list_vendor_details> ref_price_list_vendor_details { get; set; }
    }

    public class ref_price_list_vendor_details
    {
        [Key]
        public int? price_list_detail_id { get; set; }
        public int price_list_id { get; set; }
        [ForeignKey("price_list_id")]
        public virtual ref_price_list_vendor ref_price_list { get; set; }

        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }

        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }

        public double price { get; set; }
        public double discount { get; set; }
        public int discount_type_id { get; set; }
        public double price_after_discount { get; set; }
        public DateTime? effective_date { get; set; }
        public bool is_active { get; set; }
    }

    public class price_list_vendor_vm
    {
        public int price_list_id { get; set; }
        [Display(Name = "Vendor")]
        public int vendor_id { get; set; }
        public string vendor_code { get; set; }
        public string vendor_name { get; set; }
        public string deleteids { get; set; }
        public virtual List<ref_price_list_vendor_details> ref_price_list_vendor_details { get; set; }
        public List<string> price_after_discount { get; set; }
        public List<string> discount_type_id { get; set; }
        public List<string> discount { get; set; }
        public List<string> price { get; set; }
        public List<string> uom_id { get; set; }
        public List<string> item_id { get; set; }
        public List<string> price_list_detail_id { get; set; }
        [Display(Name = "Delivery Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public List<DateTime?> effective_date { get; set; }
    }
    public class ref_price_list_vendor_vm
    {
        public int price_list_id { get; set; }
        public int vendor_id { get; set; }
        public string vendor_code { get; set; }
    }
    public class ref_price_list_vendor_detail_vm
    {
        public int? price_list_detail_id { get; set; }
        public int price_list_id { get; set; }
        public int item_id { get; set; }
        public string item_code { get; set; }
        public int uom_id { get; set; }
        public double price { get; set; }
        public double discount { get; set; }
        public int discount_type_id { get; set; }
        public double price_after_discount { get; set; }
        public string vendor_code { get; set; }
        public DateTime? effective_date { get; set; }
    }
}

