using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_asset_class_gl
    {
        [Key]
        public int asset_class_gl_id { get; set; }
        public int ledger_account_type_id { get; set; }
        public int gl_id { get; set; }
        public int asset_class_id { get; set; }
    }
}
