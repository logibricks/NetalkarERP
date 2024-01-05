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
    public class mfg_prod_order_VM
    {
        [Key]
        public int prod_order_id { get; set; }
        public string prod_order_no { get; set; }
        public DateTime prod_order_date { get; set; }
        public string prod_order_date_str { get; set; }
        public int process_seq_alt_id { get; set; }
        [ForeignKey("process_seq_alt_id")]
        public virtual mfg_process_seq_alt mfg_process_seq_alt { get; set; }
        public string process_sequence_name { get; set; }
        public string machine_seq { get; set; }
        public int out_item_id { get; set; }
        [ForeignKey("out_item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public string out_item_name { get; set; }
        public double quantity { get; set; }
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        public string plant_name { get; set; }
        public string created_by { get; set; }
        public DateTime? created_ts { get; set; }
        public string modified_by { get; set; }
        public DateTime? modified_ts { get; set; }
        public int child_sloc_id { get; set; }
        [ForeignKey("child_sloc_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }
        public string child_sloc_name { get; set; }
        public int parent_sloc_id { get; set; }
        [ForeignKey("parent_sloc_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION1 { get; set; }
        public string parent_sloc_name { get; set; }
        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }
        public int shift_id { get; set; }
        [ForeignKey("shift_id")]
        public virtual ref_shifts ref_shifts { get; set; }
        public string shift_name { get; set; }
        public string remarks { get; set; }
        public bool is_blocked { get; set; }
        public double balance_qty { get; set; }
        public bool is_active { get; set; }
        public int mfg_bom_id { get; set; }
        public string mfg_bom_name { get; set; }
        public string challan_no { get; set; }
        public virtual List<mfg_prod_order_detail> mfg_prod_order_detail { get; set; }
        public virtual List<mfg_prod_order_bom> mfg_prod_order_bom { get; set; }

        public int mfg_bom_detail_id { get; set; }
        public int in_item_group_id { get; set; }
        public int in_item_id { get; set; }
        public string in_item_code { get; set; }
        public double in_item_qty { get; set; }
        public int in_uom_id { get; set; }
        public string in_uom_name { get; set; }
        public double parent_bom_qty { get; set; }
        public double calculated_qty { get; set; }

        public List<String> prod_order_detail_id { get; set; }
        public List<String> batch_in_item_id { get; set; }
        public List<String> batch_quantity { get; set; }
        public List<String> batch_in_uom_id { get; set; }
        public List<String> item_batch_id { get; set; }
        public List<String> item_batch_detail_id { get; set; }

        //public List<String> tag_no { get; set; }
        public string machine_id { get; set; }
        public string tag_no { get; set; }
        public int status_id { get; set; }

    }

    public class GetBatchForGoodsIssue
    {
        public int item_batch_id { get; set; }
        public int item_batch_detail_id { get; set; }
        public string batch_number { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? expirary_date { get; set; }
        public double? qty { get; set; }
        public double? balance_qty { get; set; }
    }

    public class GetOperationSequenceWithMachine
    {
        public int process_id { get; set; }
        public string process_name { get; set; }
        //public List<int> machine_id { get; set; }
        //public List<string> machine_name { get; set; }

        public List<GetOperationSequenceWithMachineList> GetOperationSequenceWithMachineList { get; set; }
    }

    public class GetOperationSequenceWithMachineList
    {
        public int process_id { get; set; }
        public string process_name { get; set; }
        //public List<int> machine_id { get; set; }
        //public List<string> machine_name { get; set; }
    }
}
