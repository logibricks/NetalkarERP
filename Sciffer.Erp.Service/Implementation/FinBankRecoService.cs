using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Data;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using Sciffer.Erp.Domain.Model;
using AutoMapper;

namespace Sciffer.Erp.Service.Implementation
{
    public class FinBankRecoService : IFinBankRecoService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        private readonly ScifferContext _scifferContext;
        public FinBankRecoService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public string Add(fin_bank_reco_vm reco)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("fin_bank_reco_payment_id ", typeof(int));
                dt.Columns.Add("fin_ledger_payment_detail_id ", typeof(int));
                dt.Columns.Add("fin_bank_reco_id ", typeof(int));
                dt.Columns.Add("is_selected ", typeof(bool));
                dt.Columns.Add("doc_category_id ", typeof(int));
                dt.Columns.Add("doc_no ", typeof(string));
                dt.Columns.Add("doc_posting_date ", typeof(DateTime));
                dt.Columns.Add("entity_type_id ", typeof(int));
                dt.Columns.Add("entity_id ", typeof(int));
                dt.Columns.Add("bank_tran_date ", typeof(DateTime));
                dt.Columns.Add("amount ", typeof(double));
                dt.Columns.Add("in_out ", typeof(int));
                if (reco.fin_ledger_payment_detail_id != null)
                {
                    for (var i = 0; i < reco.fin_ledger_payment_detail_id.Count; i++)
                    {
                        if (reco.fin_ledger_payment_detail_id[i] != "")
                        {
                            dt.Rows.Add(0, int.Parse(reco.fin_ledger_payment_detail_id[i]), 0, 1, int.Parse(reco.doc_category_id[i]), reco.doc_no[i],
                                DateTime.Parse(reco.doc_posting_date[i]), int.Parse(reco.entity_type_id[i]), int.Parse(reco.entity_id[i]),
                                DateTime.Parse(reco.bank_tran_date[i]), double.Parse(reco.amount[i]), 2);
                        }

                    }
                }
                if (reco.fin_ledger_payment_detail_id1 != null)
                {
                    for (var i = 0; i < reco.fin_ledger_payment_detail_id1.Count; i++)
                    {
                        if (reco.fin_ledger_payment_detail_id1[i] != "")
                        {
                            dt.Rows.Add(0, int.Parse(reco.fin_ledger_payment_detail_id1[i]), 0, 1, int.Parse(reco.doc_category_id1[i]), reco.doc_no1[i],
                                DateTime.Parse(reco.doc_posting_date1[i]), int.Parse(reco.entity_type_id1[i]), int.Parse(reco.entity_id1[i]),
                                DateTime.Parse(reco.bank_tran_date1[i]), double.Parse(reco.amount1[i]), 1);
                        }

                    }
                }
                int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var fin_bank_reco_id = new SqlParameter("@fin_bank_reco_id", reco.fin_bank_reco_id);
                var category_id = new SqlParameter("@category_id", reco.category_id);
                var document_no = new SqlParameter("@document_no", reco.document_no==null?string.Empty:reco.document_no);
                var posting_date = new SqlParameter("@posting_date", reco.posting_date);
                var reco_start_date = new SqlParameter("@reco_start_date", reco.reco_start_date);
                var reco_end_date = new SqlParameter("@reco_end_date", reco.reco_end_date);
                var bank_account_id = new SqlParameter("@bank_account_id", reco.bank_account_id);
                var currency_id = new SqlParameter("@currency_id", reco.currency_id);
                var user_closing_bal = new SqlParameter("@user_closing_bal", reco.user_closing_bal);
                var ledger_closing_bal = new SqlParameter("@ledger_closing_bal", reco.ledger_closing_bal);
                var payment_total = new SqlParameter("@payment_total", reco.payment_total);
                var receipt_total = new SqlParameter("@receipt_total", reco.receipt_total);
                var payment_not_sel_total = new SqlParameter("@payment_not_sel_total", reco.payment_not_sel_total);
                var receipt_not_sel_total = new SqlParameter("@receipt_not_sel_total", reco.receipt_not_sel_total);
                var derived_closing_bal = new SqlParameter("@derived_closing_bal", reco.derived_closing_bal);
                var created_by = new SqlParameter("@created_by", user);
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_fin_bank_reco_detail";
                t1.Value = dt;
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_brs @fin_bank_reco_id ,@category_id ,@document_no ,@posting_date ,@reco_start_date ,@reco_end_date ,@bank_account_id ,@currency_id ,@user_closing_bal ,@ledger_closing_bal ,@payment_total ,@receipt_total ,@payment_not_sel_total ,@receipt_not_sel_total ,@derived_closing_bal ,@created_by ,@t1 ",
                    fin_bank_reco_id, category_id, document_no, posting_date, reco_start_date, reco_end_date, bank_account_id, currency_id,
                    user_closing_bal, ledger_closing_bal, payment_total, receipt_total, payment_not_sel_total, receipt_not_sel_total,
                    derived_closing_bal, created_by, t1).FirstOrDefault();
                return val;
            }
            catch (Exception ex)
            {
                //--------------Log4Net
                log4net.GlobalContext.Properties["user"] = int.Parse(HttpContext.Current.Session["User_Id"].ToString()); ;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "Error : " + ex.Message;
                //return "error";
            }
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

        public List<fin_bank_payment_receipt_reco_vm> GetPaymentReceiptForBRS(int id, DateTime start_date, DateTime end_date)
        {   
            var idd = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getpaymentreceiptforbrs");
            var inout = new SqlParameter("@in_out", (int)(1));
            var startdate = new SqlParameter("@start_date", start_date);
            var enddate = new SqlParameter("@end_date", end_date);
            var val = _scifferContext.Database.SqlQuery<fin_bank_payment_receipt_reco_vm>(
            "exec get_payment_receipt @entity,@id,@in_out,@start_date,@end_date", entity, idd, inout, startdate, enddate).ToList();
            return val;
        }

        public List<fin_bank_reco_vm> GetAll()
        {
            var query = (from reco in _scifferContext.fin_bank_reco
                         join d in _scifferContext.ref_document_numbring on reco.category_id equals d.document_numbring_id
                         join b in _scifferContext.ref_bank_account on reco.bank_account_id equals b.bank_account_id
                         join c in _scifferContext.REF_CURRENCY on reco.currency_id equals c.CURRENCY_ID
                         select new fin_bank_reco_vm {
                             fin_bank_reco_id=reco.fin_bank_reco_id,
                             category_name=d.category,
                             document_no=reco.document_no,
                             posting_date=reco.posting_date,
                             reco_start_date=reco.reco_start_date,
                             reco_end_date=reco.reco_end_date,
                             bank_account_code=b.bank_account_code,
                             bank_account_number=b.bank_account_number,
                             currency_name=c.CURRENCY_NAME,
                             user_closing_bal=reco.user_closing_bal,
                             payment_total=reco.payment_total,
                             receipt_total=reco.receipt_total,
                             payment_not_sel_total=reco.payment_not_sel_total,
                             receipt_not_sel_total=reco.receipt_not_sel_total,
                             derived_closing_bal=reco.derived_closing_bal,
                         }).OrderByDescending(a => a.fin_bank_reco_id).ToList();
            return query;
        }

        public fin_bank_reco_vm Get(int id)
        {
            var idd = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "get");
            fin_bank_reco reco = _scifferContext.fin_bank_reco.FirstOrDefault(c => c.fin_bank_reco_id == id);
            Mapper.CreateMap<fin_bank_reco, fin_bank_reco_vm>();
            fin_bank_reco_vm recovm = Mapper.Map<fin_bank_reco, fin_bank_reco_vm>(reco);
            recovm.fin_bank_payment_receipt_reco_vm= _scifferContext.Database.SqlQuery<fin_bank_payment_receipt_reco_vm>(
            "exec Get_BRS_Detail @entity,@id", entity, idd).ToList();
            return recovm;
        }
    }
}
