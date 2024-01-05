using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.ViewModel;
using System.Data.SqlClient;
using Sciffer.Erp.Data;
using System.Linq;
using System.Data;
using Sciffer.Erp.Domain.Model;
using AutoMapper;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{

    public class ReceiptService : IReceiptService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        private readonly ScifferContext _scifferContext;
        public ReceiptService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public string Add(fin_ledger_paymentVM receipt)
        {
            try
            {
                string transaction_type_code;
                int user;
                user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                transaction_type_code = "";
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("fin_ledger_payment_detail_id", typeof(int));
                dt1.Columns.Add("entity_id", typeof(int));
                dt1.Columns.Add("amount", typeof(double));
                dt1.Columns.Add("tran_ref_no", typeof(string));
                dt1.Columns.Add("on_account_amount", typeof(double));
                dt1.Columns.Add("round_off_amount", typeof(double));
                dt1.Columns.Add("bank_charges_amount", typeof(double));
                if (receipt.entity_id != null)
                {
                    for (var i = 0; i < receipt.entity_id.Count; i++)
                    {
                        if (receipt.entity_id[i] != "")
                        {
                            dt1.Rows.Add(receipt.fin_ledger_payment_detail_id[i] == "" ? 0 : int.Parse(receipt.fin_ledger_payment_detail_id[i]), receipt.entity_id[i] == "" ? 0 : int.Parse(receipt.entity_id[i]),
                           receipt.amount[i] == "" ? 0 : Double.Parse(receipt.amount[i]),
                           receipt.tran_ref_no[i], receipt.on_account_amount == null ? 0 : receipt.on_account_amount[i] == "" ? 0 : Double.Parse(receipt.on_account_amount[i]), receipt.round_off_amount == null ? 0 : receipt.round_off_amount[i] == "" ? 0 : Double.Parse(receipt.round_off_amount[i]), receipt.bank_charges_amount == null ? 0 : receipt.bank_charges_amount[i] == "" ? 0 : Double.Parse(receipt.bank_charges_amount[i]));
                        }

                    }
                }
                DataTable dt2 = new DataTable();
                dt2.Columns.Add("fin_ledger_detail_id", typeof(int));
                dt2.Columns.Add("entity_type_id", typeof(int));
                dt2.Columns.Add("entity_id", typeof(int));
                dt2.Columns.Add("doc_type_name", typeof(string));
                dt2.Columns.Add("document_id", typeof(int));
                dt2.Columns.Add("adjust_amount", typeof(double));
                if (receipt.document_id != null)
                {
                    for (var i = 0; i < receipt.document_id.Count; i++)
                    {
                        dt2.Rows.Add(receipt.fin_ledger_detail_id[i] == "" ? 0 : int.Parse(receipt.fin_ledger_detail_id[i]), receipt.entity_type_id, receipt.entity_id1[i] == "" ? 0 : int.Parse(receipt.entity_id1[i]),
                            receipt.document_type_code[i], receipt.document_id[i] == "" ? 0 : int.Parse(receipt.document_id[i]),
                            receipt.adjust_amount[i] == "" ? 0 : Double.Parse(receipt.adjust_amount[i]));
                    }
                }
                if (receipt.in_out == 1)
                {
                    transaction_type_code = "RECEIPT";
                }
                else
                {
                    transaction_type_code = "PAYMENT";
                }
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_fin_payment_receipt_entity_detail";
                t1.Value = dt1;
                var t2 = new SqlParameter("@t2", SqlDbType.Structured);
                t2.TypeName = "dbo.temp_fin_ledger_payment_transaction_detail";
                t2.Value = dt2;
                var fin_ledger_payment_id = new SqlParameter("@fin_ledger_payment_id", receipt.fin_ledger_payment_id);
                var in_out = new SqlParameter("@in_out", receipt.in_out);
                var cash_bank = new SqlParameter("@cash_bank", receipt.cash_bank);
                var payment_type_id = new SqlParameter("@payment_type_id", receipt.payment_type_id);
                var bank_account_id = new SqlParameter("@bank_account_id", receipt.bank_account_id);
                var payment_amount = new SqlParameter("@payment_amount", receipt.payment_amount);
                var currency_id = new SqlParameter("@currency_id", receipt.currency_id);
                var entity_type_id = new SqlParameter("@entity_type_id", receipt.entity_type_id);
                var document_no = new SqlParameter("@document_no", receipt.document_no == null ? string.Empty : receipt.document_no);
                var payment_date = new SqlParameter("@payment_date", receipt.payment_date);
                var remarks = new SqlParameter("@remarks", receipt.remarks == null ? string.Empty : receipt.remarks);
                var payment_amount_local = new SqlParameter("@payment_amount_local", receipt.payment_amount);
                var create_user = new SqlParameter("@created_by", user);
                var transaction_type = new SqlParameter("@transaction_type_code", transaction_type_code);
                var category_id = new SqlParameter("@category_id", receipt.category_id);
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_payment_receipt @fin_ledger_payment_id,@category_id,@document_no ,@in_out ,@cash_bank ,@payment_type_id ,@bank_account_id,@payment_amount ,@currency_id ,@entity_type_id ,@payment_date ,@remarks ,@created_by,@transaction_type_code,@t1 ,@t2 ",
                    fin_ledger_payment_id, category_id, document_no, in_out, cash_bank, payment_type_id, bank_account_id, payment_amount,
                    currency_id, entity_type_id, payment_date, remarks, create_user, transaction_type, t1, t2).FirstOrDefault();
                if (val.Contains("Saved"))
                {
                    return val.Split('~')[1];
                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception ex)
            {
                //--------------Log4Net
                log4net.GlobalContext.Properties["user"] = 0;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "Error";
                //return "error";
                //  return "Error";
            }
            // return "Error";
        }


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

        public fin_ledger_paymentVM Get(int id, int in_out)
        {

            DateTime dte = new DateTime(1990, 1, 1);
            var idd = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getentitydetail");
            var inout = new SqlParameter("@in_out", in_out);
            var start_date = new SqlParameter("@start_date", dte);
            var end_date = new SqlParameter("@end_date", dte);
            fin_ledger_payment invoice = _scifferContext.fin_ledger_payment.FirstOrDefault(c => c.fin_ledger_payment_id == id);
            Mapper.CreateMap<fin_ledger_payment, fin_ledger_paymentVM>();
            fin_ledger_paymentVM quotationvm = Mapper.Map<fin_ledger_payment, fin_ledger_paymentVM>(invoice);
            quotationvm.payment_receipt_entity_detail = _scifferContext.Database.SqlQuery<payment_receipt_entity_detail>(
            "exec get_payment_receipt @entity,@id,@in_out,@start_date,@end_date", entity, idd, inout, start_date, end_date).ToList();

            var entity1 = new SqlParameter("@entity", "getentitytransactiondetail");
            var idd1 = new SqlParameter("@id", id);
            var inout1 = new SqlParameter("@in_out", in_out);
            var start_date1 = new SqlParameter("@start_date", dte);
            var end_date1 = new SqlParameter("@end_date", dte);
            quotationvm.entity_transaction_detail = _scifferContext.Database.SqlQuery<entity_transaction_detail>(
                "exec get_payment_receipt @entity,@id,@in_out,@start_date,@end_date", entity1, idd1, inout1, start_date1, end_date1).ToList();
            return quotationvm;
        }

        public List<fin_payment_receipt_vm> GetAll(int in_out)
        {
            int id = 0;
            DateTime dte = new DateTime(1990, 1, 1);
            var idd = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getall");
            var inout = new SqlParameter("@in_out", in_out);
            var start_date = new SqlParameter("@start_date", dte);
            var end_date = new SqlParameter("@end_date", dte);
            var val = _scifferContext.Database.SqlQuery<fin_payment_receipt_vm>(
           "exec get_payment_receipt @entity,@id,@in_out,@start_date,@end_date", entity, idd, inout, start_date, end_date).ToList();
            return val;
        }
        public fin_payment_receipt_vm GetPaymentReceiptHeader(int id)
        {
            var idd = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getpaymentheaderforprint");
            var val = _scifferContext.Database.SqlQuery<fin_payment_receipt_vm>(
           "exec get_payment_receipt @entity,@id", entity, idd).FirstOrDefault();
            return val;
        }
        public List<entity_transaction_detail> GetPaymentReceiptDetail(int id)
        {

            var idd = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getpaymentdetailforprint");
            var val = _scifferContext.Database.SqlQuery<entity_transaction_detail>(
           "exec get_payment_receipt @entity,@id", entity, idd).ToList();
            return val;
        }
        public List<entity_transaction_detail> GetEntityTransaction(int entity_type_id, string entity_id)
        {
            var idd = new SqlParameter("@entity_type_id", entity_type_id);
            var entity = new SqlParameter("@entity", "getentitytransaction");
            var entityid = new SqlParameter("@entity_id", entity_id);
            var val = _scifferContext.Database.SqlQuery<entity_transaction_detail>(
            "exec GetEntityTransactionDetail @entity,@entity_type_id,@entity_id", entity, idd, entityid).ToList();
            return val;
        }
        public string Delete(int id, string cancellation_remarks, int cancellation_reason_id)
        {
            try
            {
                int create_user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var pi_id = new SqlParameter("@id", id);
                var remarks = new SqlParameter("@cancellation_remarks", cancellation_remarks == null ? "" : cancellation_remarks);
                var created_by = new SqlParameter("@created_by", create_user);
                var cancellation_reason = new SqlParameter("@cancellation_reason_id", cancellation_reason_id);
                var val = _scifferContext.Database.SqlQuery<string>(
                  "exec cancel_payment_receipt @id ,@cancellation_remarks ,@created_by,@cancellation_reason_id", pi_id, remarks, created_by, cancellation_reason).FirstOrDefault();
                return val;
            }
            catch (Exception ex)
            {
                log4net.GlobalContext.Properties["user"] = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "Error : " + ex.Message;
            }
        }
    }
}
