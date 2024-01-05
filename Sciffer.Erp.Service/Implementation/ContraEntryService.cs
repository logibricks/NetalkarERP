using Sciffer.Erp.Data;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using System.Data;
using System.Data.SqlClient;
using Sciffer.Erp.Domain.ViewModel;
using AutoMapper;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class ContraEntryService : IContraEntryService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericService;
        public ContraEntryService(ScifferContext scifferContext, IGenericService GenericService)
        {
            _scifferContext = scifferContext;
            _genericService = GenericService;
        }

        public string Add(fin_contra_entry_vm contra)
        {
            try
            {
                int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                DateTime dte = new DateTime(1990, 1, 1);
                var contra_entry_id = new SqlParameter("@contra_entry_id", contra.contra_entry_id);
                var category_id = new SqlParameter("@category_id", contra.category_id);
                var document_no = new SqlParameter("@document_no", contra.document_no == null ? string.Empty : contra.document_no);
                var posting_date = new SqlParameter("@posting_date", contra.posting_date);
                var document_date = new SqlParameter("@document_date", contra.document_date);
                var ref_doc_no = new SqlParameter("@ref_doc_no", contra.ref_doc_no == null ? string.Empty : contra.ref_doc_no);
                var curreny_id = new SqlParameter("@curreny_id", contra.curreny_id);
                var from_cash_bank = new SqlParameter("@from_cash_bank", contra.from_cash_bank);
                var transfer_funds_from = new SqlParameter("@transfer_funds_from", contra.transfer_funds_from);
                var to_cash_bank = new SqlParameter("@to_cash_bank", contra.to_cash_bank);
                var transfer_funds_to = new SqlParameter("@transfer_funds_to", contra.transfer_funds_to);
                var transfer_amount = new SqlParameter("@transfer_amount", contra.transfer_amount);
                var current_balance_from = new SqlParameter("@current_balance_from", contra.current_balance_from);
                var current_balance_to = new SqlParameter("@current_balance_to", contra.current_balance_to);
                var reamrks = new SqlParameter("@reamrks", contra.reamrks == null ? string.Empty : contra.reamrks);
                var created_by = new SqlParameter("@created_by", user);

                if (contra.FileUpload != null)
                {
                    contra.attachment = _genericService.GetFilePath("ContraEntry", contra.FileUpload);
                }
                else
                {
                    contra.attachment = "No File";
                }
                var attachment = new SqlParameter("@attachment", contra.attachment);

                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_contra_entry @contra_entry_id ,@category_id ,@document_no ,@posting_date ,@document_date ,@ref_doc_no ,@curreny_id ,@from_cash_bank ,@transfer_funds_from ,@to_cash_bank ,@transfer_funds_to ,@transfer_amount ,@current_balance_from ,@current_balance_to ,@reamrks ,@attachment ,@created_by ",
                    contra_entry_id, category_id, document_no, posting_date, document_date, ref_doc_no, curreny_id, from_cash_bank,
                    transfer_funds_from, to_cash_bank, transfer_funds_to, transfer_amount, current_balance_from, current_balance_to,
                    reamrks, attachment, created_by).FirstOrDefault();
                if (val.Contains("Saved"))
                {
                    var str = val.Split('~');
                    var mrn_no = str[1];
                    if (contra.FileUpload != null)
                    {
                        contra.FileUpload.SaveAs(contra.attachment);
                    }
                    return mrn_no;
                }
                else
                {
                    System.IO.File.Delete(contra.attachment);
                    return "error";
                }

            }
            catch (Exception ex)
            {
                log4net.GlobalContext.Properties["user"] = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "error";
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

        public fin_contra_entry_vm Get(int id)
        {
            fin_contra_entry purchase = _scifferContext.fin_contra_entry.FirstOrDefault(c => c.contra_entry_id == id);
            Mapper.CreateMap<fin_contra_entry, fin_contra_entry_vm>().ForMember(dest => dest.FileUpload, opt => opt.Ignore());
            fin_contra_entry_vm purchasevm = Mapper.Map<fin_contra_entry, fin_contra_entry_vm>(purchase);
            if (purchasevm.from_cash_bank == 2)
            {
                purchasevm.transfer_funds_from_name = _scifferContext.ref_bank_account.FirstOrDefault(x => x.bank_account_id == purchasevm.transfer_funds_from).bank_account_code + '/' + _scifferContext.ref_bank_account.FirstOrDefault(x => x.bank_account_id == purchasevm.transfer_funds_from).bank_account_number;
            }
            else
            {
                purchasevm.transfer_funds_from_name = _scifferContext.ref_cash_account.FirstOrDefault(x => x.cash_account_id == purchasevm.transfer_funds_from).cash_account_code + '/' + _scifferContext.ref_cash_account.FirstOrDefault(x => x.cash_account_id == purchasevm.transfer_funds_from).cash_account_desc;
            }
            if (purchasevm.to_cash_bank == 2)
            {
                purchasevm.transfer_funds_to_name = _scifferContext.ref_bank_account.FirstOrDefault(x => x.bank_account_id == purchasevm.transfer_funds_to).bank_account_code + '/' + _scifferContext.ref_bank_account.FirstOrDefault(x => x.bank_account_id == purchasevm.transfer_funds_to).bank_account_number;
            }
            else
            {
                purchasevm.transfer_funds_to_name = _scifferContext.ref_cash_account.FirstOrDefault(x => x.cash_account_id == purchasevm.transfer_funds_to).cash_account_code + '/' + _scifferContext.ref_cash_account.FirstOrDefault(x => x.cash_account_id == purchasevm.transfer_funds_to).cash_account_desc;
            }
            return purchasevm;



            //var query = _scifferContext.contra_entry.Where(a => a.contra_entry_id == id).FirstOrDefault();
            // return query;
        }

        public List<fin_contra_entry_vm> GetAll()
        {
            var query = (from cont in _scifferContext.fin_contra_entry.Where(x => x.is_active == true)
                         join doc in _scifferContext.ref_document_numbring on cont.category_id equals doc.document_numbring_id
                         join currency in _scifferContext.REF_CURRENCY on cont.curreny_id equals currency.CURRENCY_ID
                         join cash in _scifferContext.ref_cash_account on cont.transfer_funds_from equals cash.cash_account_id into j1
                         from c1 in j1.DefaultIfEmpty()
                         join bank in _scifferContext.ref_bank_account on cont.transfer_funds_from equals bank.bank_account_id into j2
                         from b1 in j2.DefaultIfEmpty()
                         join cash1 in _scifferContext.ref_cash_account on cont.transfer_funds_to equals cash1.cash_account_id into j3
                         from c2 in j3.DefaultIfEmpty()
                         join bank1 in _scifferContext.ref_bank_account on cont.transfer_funds_to equals bank1.bank_account_id into j4
                         from b2 in j4.DefaultIfEmpty()
                         join st in _scifferContext.ref_status on cont.status_id equals st.status_id into j5
                         from s in j5.DefaultIfEmpty()
                         select new
                         {
                             contra_entry_id = cont.contra_entry_id,
                             category_name = doc.category,
                             document_no = cont.document_no,
                             posting_date = cont.posting_date,
                             document_date = cont.document_date,
                             ref_doc_no = cont.ref_doc_no,
                             curreny_name = currency.CURRENCY_NAME,
                             transfer_funds_from_name = cont.from_cash_bank == 1 ? c1.cash_account_code + " - " + c1.cash_account_desc : b1.bank_account_code + " - " + b1.bank_account_number,
                             transfer_funds_to_name = cont.to_cash_bank == 1 ? c2.cash_account_code + " - " + c2.cash_account_desc : b2.bank_account_code + " - " + b2.bank_account_number,
                             transfer_amount = cont.transfer_amount,
                             current_balance_from = cont.current_balance_from,
                             current_balance_to = cont.current_balance_to,
                             reamrks = cont.reamrks,
                             status_name = s == null ? "" : s.status_name,
                             attachment = cont.attachment,
                         }).ToList().Select(a => new fin_contra_entry_vm
                         {
                             contra_entry_id = a.contra_entry_id,
                             category_name = a.category_name,
                             document_no = a.document_no,
                             posting_date = a.posting_date,
                             document_date = a.document_date,
                             ref_doc_no = a.ref_doc_no,
                             curreny_name = a.curreny_name,
                             transfer_funds_from_name = a.transfer_funds_from_name,
                             transfer_funds_to_name = a.transfer_funds_to_name,
                             transfer_amount = a.transfer_amount,
                             current_balance_from = a.current_balance_from,
                             current_balance_to = a.current_balance_to,
                             reamrks = a.reamrks,
                             status_name = a.status_name,
                             attachment = a.attachment,
                         }).OrderByDescending(a => a.contra_entry_id).ToList();
            return query;
        }

        public fin_contra_entry Update(fin_contra_entry contra)
        {
            throw new NotImplementedException();
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
                  "exec cancel_contra_entry @id ,@cancellation_remarks ,@created_by,@cancellation_reason_id", pi_id, remarks, created_by, cancellation_reason).FirstOrDefault();
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
