using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.SqlClient;
using System.Data;

namespace Sciffer.Erp.Service.Implementation
{
    public class ValidationService : IValidationService
    {
        private readonly ScifferContext _scifferContext;
        public ValidationService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public bool Add(ref_validation validation)
        {
            try
            {
                int user = 0;
                DataTable t11 = new DataTable();//for sub ledger
                t11.Columns.Add("entity_type_id", typeof(int));
                t11.Columns.Add("gl_ledger_id", typeof(int));
                t11.Columns.Add("ledger_account_type_id", typeof(int));
                foreach (var i in validation.ref_validation_gl)
                {
                    t11.Rows.Add(9, i.gl_ledger_id, i.ledger_account_type_id);
                }

                var validation_id = new SqlParameter("@validation_id", validation.validation_id == 0 ? -1 : validation.validation_id);
                var so_mandatory_for_si = new SqlParameter("@so_mandatory_for_si", validation.so_mandatory_for_si);
                var rate_change_at_si = new SqlParameter("@rate_change_at_si", validation.rate_change_at_si);
                var pr_mandatory_for_po = new SqlParameter("@pr_mandatory_for_po", validation.pr_mandatory_for_po);
                var po_mandatory_for_grn = new SqlParameter("@po_mandatory_for_grn", validation.po_mandatory_for_grn);
                var po_mandatory_for_pi = new SqlParameter("@po_mandatory_for_pi", validation.po_mandatory_for_pi);
                var allow_to_change_bom = new SqlParameter("@allow_to_change_bom", validation.allow_to_change_bom);
                var allow_negative_cash = new SqlParameter("@allow_negative_cash", validation.allow_negative_cash);
                var allow_negative_bank = new SqlParameter("@allow_negative_bank", validation.allow_negative_bank);
                var allow_to_check_cycle_time = new SqlParameter("@allow_to_check_cycle_time", validation.allow_to_check_cycle_time);
                var round_off_values = new SqlParameter("@round_off_values", validation.round_off_values);
                var grn_for_qa = new SqlParameter("@grn_for_qa", validation.grn_for_qa);
                var goods_receipt_for_qa = new SqlParameter("@goods_receipt_for_qa", validation.goods_receipt_for_qa);
                var production_receipt_for_qa = new SqlParameter("@production_receipt_for_qa", validation.production_receipt_for_qa);
                var inventory_adjustment = new SqlParameter("@inventory_adjustment", validation.inventory_adjustment);
                var order_creation_date_for_next_month = new SqlParameter("@order_creation_date_for_next_month", validation.order_creation_date_for_next_month);
                var create_user = new SqlParameter("@create_user", user);
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_sub_ledger";
                t1.Value = t11;
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_validation @validation_id ,@so_mandatory_for_si ,@rate_change_at_si ,@pr_mandatory_for_po ,@po_mandatory_for_grn ,@po_mandatory_for_pi ,@allow_to_change_bom ,@allow_negative_cash ,@allow_negative_bank ,@round_off_values ,@grn_for_qa ,@goods_receipt_for_qa ,@production_receipt_for_qa ,@inventory_adjustment ,@order_creation_date_for_next_month ,@create_user,@allow_to_check_cycle_time ,@t1 ",
                    validation_id, so_mandatory_for_si, rate_change_at_si, pr_mandatory_for_po, po_mandatory_for_grn, po_mandatory_for_pi,
                    allow_to_change_bom, allow_negative_cash, allow_negative_bank, round_off_values, grn_for_qa, goods_receipt_for_qa,
                    production_receipt_for_qa, inventory_adjustment, order_creation_date_for_next_month, create_user, allow_to_check_cycle_time, t1).FirstOrDefault();

                if (val == "Saved")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #region dispoable methods
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _scifferContext.Dispose();
            }
        }
        #endregion

        public ref_validation Get(int id)
        {
            return _scifferContext.ref_validation.Where(x => x.validation_id == id).FirstOrDefault();
        }

        public List<ref_validation> GetAll()
        {
            return _scifferContext.ref_validation.ToList();
        }
    }
}
