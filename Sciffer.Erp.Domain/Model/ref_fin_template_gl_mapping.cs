using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_fin_template_gl_mapping
    {
        [Key, Column(Order = 0)]
        public int template_detail_id { get; set; }
        [Key, Column(Order = 1)]
        public int gl_ledger_id { get; set; }
        [Key, Column(Order = 2)]
        public int template_id { get; set; }
    }

    public class ref_fin_template_gl_mapping_vm
    {
        public List<int> template_detail_id { get; set; }
        public string group_name { get; set; }
        public List<int> gl_ledger_id { get; set; }
        public string gl_ledger_name { get; set; }
        public int template_id { get; set; }
        public string template_name { get; set; }
        public int? gl_ledger_id1 { get; set; }
        public int template_detail_id1 { get; set; }
    }

}
