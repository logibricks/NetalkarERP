using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class REF_PLANT_VM
    {
        public string item_code { get; set; }
        public int PLANT_ID { get; set; }
        [Display(Name = "Description *")]
        public string PLANT_NAME { get; set; }

        [Display(Name = "Code *")]
        public string PLANT_CODE { get; set; }
        [Display(Name = "Address *")]
        [DataType(DataType.MultilineText)]
        public string PLANT_ADDRESS { get; set; }
        [Display(Name = "City *")]
        public string PLANT_CITY { get; set; }

        [Display(Name = "State *")]
        public int PLANT_STATE { get; set; }
        [ForeignKey("PLANT_STATE")]
        public virtual REF_STATE REF_STATE { get; set; }

        [Display(Name = "Country *")]
        public  int PLANT_COUNTRY { get; set; }
        [ForeignKey("PLANT_COUNTRY")]
        public virtual REF_COUNTRY REF_COUNTRY { get; set; }

        [Display(Name = "Telephone")]
        public string PLANT_TELEPHONE { get; set; }
        [Display(Name = "Mobile")]
        public string PLANT_MOBILE { get; set; }
        [Display(Name = "Email")]
        public string PLANT_EMAIL { get; set; }
        [Display(Name = "Excise Number")]
        public string excise_number { get; set; }
        [Display(Name = "Excise Range")]
        public string excise_range { get; set; }
        [Display(Name = "Excise Division ")]
        public string excise_division { get; set; }
        [Display(Name = "Service Tax Number ")]
        public string service_tax_number { get; set; }
        [Display(Name = "Excise Commissionerate")]
        public string excise_commisionerate { get; set; }
        [Display(Name = "VAT Number")]
        public string vat_number { get; set; }
        [Display(Name = "CST Number")]
        public string cst_number { get; set; }
        [Display(Name = "GST Number")]
        public string gst_number { get; set; }
        [Display(Name = "Blocked")]
        public bool is_blocked { get; set; }
        public bool is_active { get; set; }
        [Display(Name = "Pincode *")]
        public string pincode { get; set; }
        [Display(Name = "Telephone")]
        public string telephone_cdoe { get; set; }

    }
    public class plant_vm
    {
        public int PLANT_ID { get; set; }      
        public string PLANT_NAME { get; set; }
        public string PLANT_CODE { get; set; }      
        public string PLANT_ADDRESS { get; set; }
        public string PLANT_CITY { get; set; }
        public string PLANT_STATE { get; set; }     
        public string PLANT_COUNTRY { get; set; }
        public string PLANT_TELEPHONE { get; set; }    
        public string PLANT_MOBILE { get; set; }      
        public string PLANT_EMAIL { get; set; }
        public string excise_number { get; set; }
        public string excise_range { get; set; }
        public string excise_division { get; set; }
        public string service_tax_number { get; set; }
        public string excise_commisionerate { get; set; }
        public string vat_number { get; set; }
        public string cst_number { get; set; }
        public string gst_number { get; set; }
        public string pincode { get; set; }
        public string telephone_cdoe { get; set; }
    }
}
