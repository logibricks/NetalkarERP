using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class dashboard_vm
    {
        public string month_name { get; set; }
        public double? amount { get; set; }
        public string bank_account_code { get; set; }
        public double? limit { get; set; }
        public int? cash_bank { get; set; }
        public int? in_out { get; set; }
        public double? receipt_amount { get; set; }
        public double? payment_amount { get; set; }
        public string customer_display_name { get; set; }
        public double? not_due { get; set; }
        public double? total_due { get; set; }
        public int? so_count { get; set; }
        public string item_name { get; set; }
        public double? this_year_quantity { get; set; }
        public double? last_month_quantity { get; set; }
        public double? this_month_quantity { get; set; }
        public double? yest_quantity { get; set; }
        public string machine_name { get; set; }
        public int? RM_2CYL { get; set; }
        public int? RM_3CRE { get; set; }
        public int? RM_3CYL { get; set; }
        public int? RM_4CRE { get; set; }
        public int? RM_4CYL { get; set; }
        public int? RM_U301 { get; set; }
        public int? ELS_4DI { get; set; }
        public int? ELS_3DI { get; set; }
        public int? RM_U301_BS6 { get; set; }
        public int? RM_2CYL_D09 { get; set; }
        public int? RM_4CYL_ISUZU_BS6 { get; set; }
        public int? RM_4CYL_ISUZU { get; set; }
        public int? SINGLE_CYL_P602_CRANKSHAFT { get; set; }
        public double? quantity { get; set; }
        public double? store_quantity { get; set; }
        public double? wip_qty { get; set; }
        public string plant_code { get; set; }
        public double? this_year_amount { get; set; }
        public double? last_month_amount { get; set; }
        public double? this_month_amount { get; set; }
        public string input_output { get; set; }
        public string tax_type_name { get; set; }
        public string gl_ledger_code { get; set; }
        public string gl_ledger_name { get; set; }
        public double? wip_qc_quantity { get; set; }
        public double? pending_rejection_qty { get; set; }
        public double? pending_prod_rec_qty { get; set; }
        public double? avail_for_sales_qty { get; set; }
        public double? rejection_not_set_qty { get; set; }
        public double? avail_for_return_qty { get; set; }

        public double? avail_for_batch_qty { get; set; }

        public string Operator { get; set; }
        public string shiftwise { get; set; }
        public int? ProducedQty { get; set; }

        //Used for ReOrderLevel
        public string ITEM_CODE { get; set; }
        public string UOM_NAME { get; set; }
        public string storage_location_name { get; set; }
        public int MINIMUM_LEVEL { get; set; }
        public int MAXIMUM_LEVEL { get; set; }
        public double REORDER_QUANTITY { get; set; }
        public int REORDER_LEVEL { get; set; }
        public int AvailableStock { get; set; }
        public int Shortfall { get; set; }

    }
}
