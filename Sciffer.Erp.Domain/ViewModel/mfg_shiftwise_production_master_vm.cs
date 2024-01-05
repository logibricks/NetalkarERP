using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class mfg_shiftwise_production_master_vm
    {
        public int shiftwise_production_id { get; set; }

        public int document_numbring_id { get; set; }
        
        public virtual ref_document_numbring ref_document_numbring { get; set; }
        public int? plant_id { get; set; }
        public string Plant_Name { get; set; }
        public virtual REF_PLANT REF_PLANT { get; set; }

        public int? shift_id { get; set; }

        public string Shift_Desc { get; set; }
        public string shift_time { get; set; }
        public virtual ref_shifts ref_shifts { get; set; }

        [Display(Name = "Posting Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }

        [Display(Name = "Document Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime document_date { get; set; }

        public string document_no { get; set; }
        public string category { get; set; }
        public int created_by { get; set; }
        public string created_name { get; set; }
        public DateTime created_ts { get; set; }
        public int? modified_by { get; set; }
        public string modified_name { get; set; }
        public DateTime? modified_on { get; set; }
        public bool is_active { get; set; }

        public virtual List<shiftwiseProductionDetails> shiftwiseProductionDetails { get; set; }

    }

    public class shiftwiseProductionDetails
    {
        public Int64 rowIndex { get; set; }
        public int shiftwise_production_details_id { get; set; }

        public int process_id { get; set; }

        public string operation_code { get; set; }

        public string operation_name { get; set; }

        public int machine_id { get; set; }

        public string machine_name { get; set; }

        public int ITEM_ID { get; set; }
        public string ITEM_NAME { get; set; }
        public int? cycle_time { get; set; }
        public TimeSpan? cycle_time_span { get; set; }
        public decimal? std_prod_qty { get; set; }
        public int? std_prod_quantity { get; set; }
        public int? wip_qty { get; set; }

        public int? target_qty { get; set; }
        public int? new_target_qty { get; set; }
        public int shiftwise_production_id { get; set; }

        public bool is_active { get; set; }

        public DateTime prod_date { get; set; }


    }
}
