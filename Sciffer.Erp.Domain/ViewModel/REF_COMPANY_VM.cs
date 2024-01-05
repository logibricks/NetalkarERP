using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class REF_COMPANY_VM
    {
        public int COMPANY_ID { get; set; }
        [Display(Name = "Company Name *")]
        public string COMPANY_NAME { get; set; }
        [Display(Name = "Company Display Name *")]
        public string COMPANY_DISPLAY_NAME { get; set; }
        [Display(Name = "Registered Address *")]
        [DataType(DataType.MultilineText)]
        public string REGISTERED_ADDRESS { get; set; }
        [Display(Name = "Registered City *")]
        public string REGISTERED_CITY { get; set; }
        [Display(Name = "Registered State *")]
        public int REGISTERED_STATE_ID { get; set; }
        [ForeignKey("REGISTERED_STATE_ID")]
        public virtual REF_STATE REF_STATE { get; set; }
        [Display(Name = "Registered Country *")]
        public int REGISTERED_COUNTRY_ID { get; set; }
        [ForeignKey("REGISTERED_COUNTRY_ID")]
        public virtual REF_COUNTRY REF_COUNTRY { get; set; }
        [Display(Name = "Registered Telephone")]
        public double? REGISTERED_TELEPHONE { get; set; }
        [Display(Name = "Registered Mobile")]
        public double? REGISTERED_MOBILE { get; set; }
        [Display(Name = "Registered Email")]
        public string REGISTERED_EMAIL { get; set; }
        [Display(Name = "Corporate Address *")]
        [DataType(DataType.MultilineText)]
        public string CORPORATE_ADDRESS { get; set; }
        [Display(Name = "Corporate City *")]
        public string CORPORATE_CITY { get; set; }
        [Display(Name = "Corporate State *")]
        public int CORPORATE_STATE_ID { get; set; }
        [ForeignKey("CORPORATE_STATE_ID")]
        public virtual REF_STATE REF_STATE1 { get; set; }
        [Display(Name = "Corporate Country *")]
        public int CORPORATE_COUNTRY_ID { get; set; }
        [ForeignKey("CORPORATE_COUNTRY_ID")]
        public virtual REF_COUNTRY REF_COUNTRY1 { get; set; }
        [Display(Name = "Corporate Telephone")]
        public double? CORPORATE_TELEPHONE { get; set; }
        [Display(Name = "Corporate Mobile")]
        public double? CORPORATE_MOBILE { get; set; }
        [Display(Name = "Organization Type *")]
        public int ORG_TYPE_ID { get; set; }
        [ForeignKey("ORG_TYPE_ID")]
        public virtual REF_ORG_TYPE REF_ORG_TYPE { get; set; }
        public int std_code1 { get; set; }
        public int std_code2 { get; set; }
        [Display(Name ="Registered Telephone")]
        public string registered_telephone_code { get; set; }
        [Display(Name = "Corporate Telephone")]
        public string corporate_telephone_code { get; set; }
        [Display(Name = "Website")]
        public string WEBSITE { get; set; }       
        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }
        [Display(Name = "Logo")]
        public string LOGO { get; set; }
        [Display(Name ="PAN No *")]
        public string PAN_NO { get; set; }
        [Display(Name = "CIN No")]
        public string CIN_NO { get; set; }
        [Display(Name = "TAN No")]
        public string TAN_NO { get; set; }
        //[Display(Name = "CURRENCY ID")]
        public int CURRENCY_ID { get; set; }
        [ForeignKey("CURRENCY_ID")]
        public virtual REF_CURRENCY REF_CURRENCY { get; set; }
        public bool ALLOW_NEGATIVE_CASH { get; set; }
        public bool ALLOW_NEGATIVE_INVENTORY { get; set; }
        [Display(Name ="Registered Pincode *")]
        public int? registered_pincode { get; set; }
        [Display(Name = "Corporate Pincode *")]
        public int? corporate_pincode { get; set; }
    }
    public class comapnyvm
    {
        public int company_id { get; set; }
        public string company_name { get; set; }
        public string company_display_name { get; set; }
        public string registered_address { get; set; }
        public string registered_city { get; set; }
        public string registered_state { get; set; }
        public string registered_country { get; set; }
        public double? registered_mobile { get; set; }
        public double? registered_telephone { get; set; }
        public string registered_email { get; set; }
        public string corporate_address { get; set; }
        public string corporate_city { get; set; }
        public string corporate_state { get; set; }
        public string corporate_country { get; set; }
        public double? corporate_mobile { get; set; }
        public double? corporate_telephone { get; set; }
        public string ORG_TYPE_NAME { get; set; }
        public string website { get; set; }
        public string PAN_NO { get; set; }
        public string CIN_NO { get; set; }
        public string TAN_NO { get; set; }
        public string currency { get; set; }
        public string logo { get; set; }
        public int? registered_pincode { get; set; }
        public int? corporate_pincode { get; set; }
        public bool ALLOW_NEGATIVE_CASH { get; set; }
        public bool ALLOW_NEGATIVE_INVENTORY { get; set; }
        public string registered_telephone_code { get; set; }
        public string corporate_telephone_code { get; set; }
    }
}
