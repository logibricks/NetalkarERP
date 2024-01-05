using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class ref_inventory_balance_VM
    {
        public int? inventory_balance_id { get; set; }
        public int? offset_account_id { get; set; }
        [ForeignKey("offset_account_id")]
        public virtual ref_general_ledger ref_general_ledger { get; set; }
        public int? gl_ledger_id { get; set; }
        public string gl_ledger_code { get; set; }
        public string gl_ledger_name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }
        public string header_remarks { get; set; }
        public string deleteids { get; set; }
        public int category_id { get; set; }
        public string doc_number { get; set; }        
        public virtual IList<ref_inventory_balance_details> ref_inventory_balance_details { get; set; }
        public virtual List<inventory_balance_detail> inventory_balance_detail { get; set; }
        public List<string> item_id { get; set; }
        public List<string> plant_id { get; set; }
        public List<string> sloc_id { get; set; }
        public List<string> bucket_id { get; set; }
        public List<string> batch { get; set; }
        public List<string> qty { get; set; }
        public List<string> uom_id { get; set; }
        public List<string> rate { get; set; }
        public List<string> value { get; set; }
        public List<string> line_remarks { get; set; }
        public List<string> inventory_balance_detail_id { get; set; }

    }
    public class inventory_balance_detail
    {
        public string item_code { get; set; }
        public string item_desc { get; set; }
        public string plant_code { get; set; }
        public string plant_desc { get; set; }
        public string sloc { get; set; }
        public string bucket { get; set; }
        public string batch { get; set; }
        public string qty { get; set; }
        public string uom { get; set; }
        public string rate { get; set; }
        public string value { get; set; }
        public string line_remarks { get; set; }
    }
    public class ref_inventory_balanceVM
    {
        public int? inventory_balance_id { get; set; }
        public int? offset_account_id { get; set; }
        public int gl_ledger_id { get; set; }
        public string gl_ledger_code { get; set; }
        public string gl_ledger_name { get; set; }
        public string offset_account { get; set; }
        public DateTime posting_date { get; set; }
        public string hearder_remarks { get; set; }
        public int category_id { get; set; }
        public string category_name { get; set; }
        public string doc_number { get; set; }

    }

    public class inventory_balance_VM
    {

        public int? inventory_balance_id { get; set; }
        public int offset_account_id { get; set; }
        public string offset_account { get; set; }
        public DateTime posting_date { get; set; }
        public string header_remarks { get; set; }
        public int category_id { get; set; }
        public string doc_number { get; set; }
    }
    public class inventory_balance_detail_VM
    {
        public int? inventory_balance_detail_id { get; set; }
        public int? item_id { get; set; }
        public string item_code { get; set; }
        public int? plant_id { get; set; }
        public string plant { get; set; }
        public int? sloc_id { get; set; }
        public string sloc { get; set; }
        public int? bucket_id { get; set; }
        public string bucket { get; set; }
        public string batch { get; set; }
        public double? qty { get; set; }
        public string uom { get; set; }
        public int? uom_id { get; set; }
        public double rate { get; set; }
        public double? value { get; set; }
        public string line_remarks { get; set; }
        public string offset_account { get; set; }
    }
}
