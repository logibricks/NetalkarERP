using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_price_list_customer
    {
        [Key]
        public int price_list_id { get; set; }
        public int customer_id { get; set; }
        [ForeignKey("customer_id")]
        public virtual REF_CUSTOMER REF_CUSTOMER { get; set; }
        public bool is_active { get; set; }
        public virtual ICollection<ref_price_list_customer_details> ref_price_list_customer_details { get; set; }
    }
    public class ref_price_list_customer_details
    {
        [Key]
        public int? price_list_detail_id { get; set; }
        public int price_list_id { get; set; }
        [ForeignKey("price_list_id")]
        public virtual ref_price_list_customer ref_price_list_customer { get; set; }

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
    }

    public class price_list_customer_vm
    {
        public int price_list_id { get; set; }
        [Display(Name = "Customer")]
        public int customer_id { get; set; }
        public string customer_code { get; set; }
        public string customer_name { get; set; }
        public string deleteids { get; set; }
        public virtual List<ref_price_list_customer_details> ref_price_list_customer_details { get; set; }
        public List<string> price_after_discount { get; set; }
        public List<string> discount_type_id { get; set; }
        public List<string> discount { get; set; }
        public List<string> price { get; set; }
        public List<string> uom_id { get; set; }
        public List<string> item_id { get; set; }
        public List<string> price_list_detail_id { get; set; }
    }
    public class ref_price_list_customer_vm
    {
        public int price_list_id { get; set; }
        public int customer_id { get; set; }
        public string customer_code { get; set; }

    }
    public class ref_price_list_customer_detail_vm
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
        public string customer_code { get; set; }
    }
}
