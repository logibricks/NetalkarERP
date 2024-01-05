using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class FinLedgerService : IFinLedgerService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        private readonly ScifferContext _scifferContext;
        public FinLedgerService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }
        public string Add(fin_ledgerVM party)
        {
            try
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("fin_ledger_detail_id", typeof(int));
                dt.Columns.Add("entity_type_id", typeof(int));
                dt.Columns.Add("gl_ledger_id", typeof(int));
                dt.Columns.Add("dr_amount", typeof(float));
                dt.Columns.Add("cr_amount", typeof(float));
                dt.Columns.Add("cost_center_id", typeof(int));
                dt.Columns.Add("line_remarks", typeof(string));
                if (party.gl_ledger_id != null)
                {
                    for (var i = 0; i < party.gl_ledger_id.Count; i++)
                    {
                        if (party.gl_ledger_id[i] != "")
                        {
                            dt.Rows.Add(0, party.entity_type_id[i] == "" ? 0 : Convert.ToInt32(party.entity_type_id[i]),
                                party.gl_ledger_id[i] == "" ? 0 : Convert.ToInt32(party.gl_ledger_id[i]),
                                party.dr_amount[i] == "" ? 0 : Double.Parse(party.dr_amount[i]),
                                party.cr_amount[i] == "" ? 0 : Double.Parse(party.cr_amount[i]),
                                party.cost_center_id[i] == "" ? 0 : Convert.ToInt32(party.cost_center_id[i]),
                                party.line_remarks[i]);
                        }
                    }
                }
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_fin_ledger_detail";
                t1.Value = dt;
                party.create_user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var fin_ledger_id = new SqlParameter("@fin_ledger_id", party.fin_ledger_id == 0 ? -1 : party.fin_ledger_id);
                var category_id = new SqlParameter("@category_id", party.category_id);
                var document_no = new SqlParameter("@document_no", party.document_no);
                var ledger_date = new SqlParameter("@ledger_date", party.ledger_date);
                var ledger_amount = new SqlParameter("@ledger_amount", party.ledger_amount);
                var currency_id = new SqlParameter("@currency_id", party.currency_id);
                var narration = new SqlParameter("@narration", party.narration == null ? string.Empty : party.narration);
                var create_user = new SqlParameter("@create_user", party.create_user);
                var ref1 = new SqlParameter("@ref1", party.ref1 == null ? string.Empty : party.ref1);
                var ref2 = new SqlParameter("@ref2", party.ref2 == null ? string.Empty : party.ref2);
                var ref3 = new SqlParameter("@ref3", party.ref3 == null ? string.Empty : party.ref3);
                var document_date = new SqlParameter("@document_date", party.document_date);
                var due_date = new SqlParameter("@due_date", party.due_date);
                var transaction_id = new SqlParameter("@transaction_id", party.source_document_id);
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec dbo.save_journal_entry @fin_ledger_id ,@category_id  ,@transaction_id ,@document_no ,@ledger_date  ,@ledger_amount,@currency_id  ,@narration  ,@create_user,@document_date ,@ref1,@ref2,@ref3,@due_date,@t1",
                    fin_ledger_id, category_id, transaction_id, document_no, ledger_date, ledger_amount, currency_id, narration, create_user, document_date, ref1, ref2, ref3, due_date, t1).FirstOrDefault();
                if (val.Contains("Saved"))
                {
                    return val;
                }
                else
                    return val;
            }
            catch (Exception ex)
            {
                log4net.GlobalContext.Properties["user"] = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return ex.InnerException.ToString();
            }

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
                  "exec cancel_journal_entry @id ,@cancellation_remarks ,@created_by,@cancellation_reason_id", pi_id, remarks, created_by, cancellation_reason).FirstOrDefault();
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

        public fin_ledgerVM Get(int? id)
        {
            try
            {
                var val = _scifferContext.Database.SqlQuery<GetFinLedgerDetailById>("exec GetFinLedgerDetailById @entity,@fin_ledger_id", new SqlParameter("entity", "getledgerdetail"), new SqlParameter("fin_ledger_id", id)).ToList<GetFinLedgerDetailById>();
                //  var val1 = _scifferContext.Database.SqlQuery<GetDocDetail>("exec GetFinLedgerDetailById @entity,@fin_ledger_id", new SqlParameter("entity", "getledgerdocdetail"), new SqlParameter("fin_ledger_id", id)).ToList<GetDocDetail>();
                fin_ledger JR = _scifferContext.fin_ledger.FirstOrDefault(c => c.fin_ledger_id == id);
                Mapper.CreateMap<fin_ledger, fin_ledgerVM>();
                fin_ledgerVM JRVM = Mapper.Map<fin_ledger, fin_ledgerVM>(JR);
                JRVM.GetFinLedgerDetailById = val;
                var doc = _scifferContext.ref_document_type.Where(x => x.document_type_code == JR.document_type_code).FirstOrDefault();
                JRVM.document_type_code = doc.document_type_name;
                return JRVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<fin_ledgerVM> GetAll()
        {
            try
            {
                Mapper.CreateMap<fin_ledger, fin_ledgerVM>();
                return null; //_scifferContext.journal_entry.Project().To<journal_entryVM>().Where(a => a.journal_entry_is_active == true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public List<fin_ledgerVM> getall()
        {
            var query = (from journal in _scifferContext.fin_ledger.OrderByDescending(x => x.fin_ledger_id)
                         join category in _scifferContext.ref_document_numbring on journal.category_id equals category.document_numbring_id into category1
                         from category2 in category1.DefaultIfEmpty()
                         join currency in _scifferContext.REF_CURRENCY on journal.currency_id equals currency.CURRENCY_ID into currency1
                         from currency2 in currency1.DefaultIfEmpty()
                         join d in _scifferContext.ref_document_type on journal.document_type_code equals d.document_type_code
                         join s in _scifferContext.ref_status on journal.status_id equals s.status_id into j1
                         from st in j1.DefaultIfEmpty()
                         select new fin_ledgerVM()
                         {
                             fin_ledger_id = journal.fin_ledger_id,
                             category_name = category2.category,
                             document_no = journal.document_no,
                             ledger_date = journal.ledger_date,
                             ref1 = journal.ref1,
                             ref2 = journal.ref2,
                             ref3 = journal.ref3,
                             document_date = journal.document_date,
                             due_date = journal.due_date,
                             currency_name = currency2.CURRENCY_NAME,
                             narration = journal.narration == null ? "" : journal.narration,
                             document_type_code = d.document_type_name,
                             source_document_no = journal.source_document_no,
                             status_name=st==null?"Closed":st.status_name,
                         }).OrderByDescending(x => x.fin_ledger_id).ToList();
            return query;
        }
        public bool Update(fin_ledgerVM party)
        {
            try
            {

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
