using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class ref_asset_class_gl_vm
    {
        public int asset_class_gl_id { get; set; }
        public int ledger_account_type_id { get; set; }
        public int gl_id { get; set; }
        public string ledger_account_type_name { get; set; }
        public string gl_ledger_code { get; set; }
    }
}
