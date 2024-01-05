using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.ViewModel;
using System.Data.SqlClient;
using Sciffer.Erp.Data;
using System.Data;
using System.Web;
using Sciffer.Erp.Domain.Model;
using AutoMapper;

namespace Sciffer.Erp.Service.Implementation
{
    public class FinInternalReconcileService : IFinInternalReconcileService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        private readonly ScifferContext _scifferContext;
        public FinInternalReconcileService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }
        public string Add(fin_internal_reconcile_vm reconcile)
        {
            try
            {
                DataTable dt = new DataTable();
                DateTime dte = new DateTime(1990, 1, 1);
                dt.Columns.Add("internal_reconcile_detail_id", typeof(int));
                dt.Columns.Add("doc_type_id", typeof(int));
                dt.Columns.Add("doc_category_id", typeof(int));
                dt.Columns.Add("doc_no", typeof(string));
                dt.Columns.Add("doc_posting_date", typeof(string));
                dt.Columns.Add("amount", typeof(double));
                dt.Columns.Add("balance_amount", typeof(double));
                dt.Columns.Add("reconcile_amount", typeof(double));
                dt.Columns.Add("fin_ledger_detail_id", typeof(int));
                for (var i = 0; i < reconcile.doc_type_id1.Count; i++)
                {
                    if (reconcile.doc_type_id1[i] != "")
                    {
                        var reoncile_id = 0;
                        var doc_type_id = reconcile.doc_type_id1[i] == "" ? 0 : Convert.ToInt32(reconcile.doc_type_id1[i]);
                        var doc_category_id = reconcile.doc_category_id1[i] == "" ? 0 : Convert.ToInt16(reconcile.doc_category_id1[i]);
                        var doc_no = reconcile.doc_no1[i] == "" ? "" : reconcile.doc_no1[i];
                        var parsedDate = reconcile.doc_posting_date1[i] == "" ? "" : reconcile.doc_posting_date1[i];
                        var amount = reconcile.amount1[i] == "" ? 0 : Convert.ToDouble(reconcile.amount1[i]);
                        var balance_amount = reconcile.balance_amount1[i] == "" ? 0 : Convert.ToDouble(reconcile.balance_amount1[i]);
                        var reconcile_amount = reconcile.reconcile_amount1[i] == "" ? 0 : Convert.ToDouble(reconcile.reconcile_amount1[i]);
                        var fin_ledger_detail_id = reconcile.fin_ledger_detail_id[i] == "" ? 0 : int.Parse(reconcile.fin_ledger_detail_id[i]);
                        dt.Rows.Add(reoncile_id, doc_type_id, doc_category_id, doc_no, parsedDate, amount, balance_amount, reconcile_amount, fin_ledger_detail_id);
                    }

                }

                var internal_reconcile_id = new SqlParameter("@internal_reconcile_id", reconcile.internal_reconcile_id == null ? -1 : reconcile.internal_reconcile_id);
                var document_no = new SqlParameter("@document_no", reconcile.document_no == null ? "" : reconcile.document_no);
                var category_id = new SqlParameter("@category_id", reconcile.category_id);
                var posting_date = new SqlParameter("@posting_date", reconcile.posting_date);
                var entity_type_id = new SqlParameter("@entity_type_id", reconcile.entity_type_id);
                var entity_id = new SqlParameter("@entity_id", reconcile.entity_id);
                var loginId = HttpContext.Current.Session["User_Id"];
                var created_by = new SqlParameter("@created_by", loginId);
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_fin_reconcile_detail";
                t1.Value = dt;
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_fin_internal_reconcile @internal_reconcile_id ,@category_id ,@document_no ,@posting_date ,@entity_type_id ,@entity_id,@created_by, @t1",
                    internal_reconcile_id, category_id, document_no, posting_date, entity_type_id, entity_id, created_by, t1).FirstOrDefault();

                if (val.Contains("Saved"))
                {
                    return val;
                }
                else
                {
                    return "error";
                }
            }
            catch (Exception ex)
            {
                //--------------Log4Net
                log4net.GlobalContext.Properties["user"] = 0;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "error";
                //return "error";
            }
        }

        public fin_internal_reconcile_vm Get(int id)
        {
            fin_internal_reconcile purchase = _scifferContext.fin_internal_reconcile.FirstOrDefault(c => c.internal_reconcile_id == id);
            Mapper.CreateMap<fin_internal_reconcile, fin_internal_reconcile_vm>();
            fin_internal_reconcile_vm purchasevm = Mapper.Map<fin_internal_reconcile, fin_internal_reconcile_vm>(purchase);
            purchasevm.fin_internal_reconcile_detail = purchasevm.fin_internal_reconcile_detail.ToList();
            purchasevm.reconcileDate = purchasevm.posting_date;
            if (purchasevm.entity_type_id == 1)
            {
                purchasevm.entity_name = _scifferContext.REF_CUSTOMER.FirstOrDefault(x => x.CUSTOMER_ID == purchasevm.entity_id).CUSTOMER_CODE + '/' + _scifferContext.REF_CUSTOMER.FirstOrDefault(x => x.CUSTOMER_ID == purchasevm.entity_id).CUSTOMER_NAME;
            }
            else if (purchasevm.entity_type_id == 2)
            {
                purchasevm.entity_name = _scifferContext.REF_VENDOR.FirstOrDefault(x => x.VENDOR_ID == purchasevm.entity_id).VENDOR_CODE + '/' + _scifferContext.REF_VENDOR.FirstOrDefault(x => x.VENDOR_ID == purchasevm.entity_id).VENDOR_NAME;
            }
            else if (purchasevm.entity_type_id == 3)
            {
                purchasevm.entity_name = _scifferContext.REF_EMPLOYEE.FirstOrDefault(x => x.employee_id == purchasevm.entity_id).employee_code + '/' + _scifferContext.REF_EMPLOYEE.FirstOrDefault(x => x.employee_id == purchasevm.entity_id).employee_name;
            }
            else
            {
                purchasevm.entity_name = _scifferContext.ref_general_ledger.FirstOrDefault(x => x.gl_ledger_id == purchasevm.entity_id).gl_ledger_code + '/' + _scifferContext.ref_general_ledger.FirstOrDefault(x => x.gl_ledger_id == purchasevm.entity_id).gl_ledger_code;
            }
            return purchasevm;
        }

        public List<fin_internal_reconcile_vm> GetAll()
        {
            var query = (from concile in _scifferContext.fin_internal_reconcile
                         join doc in _scifferContext.ref_document_numbring on concile.category_id equals doc.document_numbring_id
                         join entity_type in _scifferContext.REF_ENTITY_TYPE on concile.entity_type_id equals entity_type.ENTITY_TYPE_ID
                         join customer in _scifferContext.REF_CUSTOMER on concile.entity_id equals customer.CUSTOMER_ID into customer1
                         from customer2 in customer1.DefaultIfEmpty()
                         join vendor in _scifferContext.REF_VENDOR on concile.entity_id equals vendor.VENDOR_ID into vendor1
                         from vendor2 in vendor1.DefaultIfEmpty()
                         join employee in _scifferContext.REF_EMPLOYEE on concile.entity_id equals employee.employee_id into employee1
                         from employee2 in employee1.DefaultIfEmpty()
                         join account in _scifferContext.ref_general_ledger on concile.entity_id equals account.gl_ledger_id into account1
                         from account2 in account1.DefaultIfEmpty()
                         join st in _scifferContext.ref_status on concile.status_id equals st.status_id into j1
                         from s in j1.DefaultIfEmpty()
                         select new fin_internal_reconcile_vm
                         {
                             internal_reconcile_id = concile.internal_reconcile_id,
                             document_no = concile.document_no,
                             category_name = doc.category,
                             posting_date = concile.posting_date,
                             entity_type_name = entity_type.ENTITY_TYPE_NAME,
                             entity_code = entity_type.ENTITY_TYPE_ID == 1 ? customer2.CUSTOMER_CODE : entity_type.ENTITY_TYPE_ID == 2 ? vendor2.VENDOR_CODE : entity_type.ENTITY_TYPE_ID == 3 ? employee2.employee_code : entity_type.ENTITY_TYPE_ID == 4 ? account2.gl_ledger_code : string.Empty,
                             entity_name = entity_type.ENTITY_TYPE_ID == 1 ? customer2.CUSTOMER_NAME : entity_type.ENTITY_TYPE_ID == 2 ? vendor2.VENDOR_NAME : entity_type.ENTITY_TYPE_ID == 3 ? employee2.employee_name : entity_type.ENTITY_TYPE_ID == 4 ? account2.gl_ledger_name : string.Empty,
                             status_name = s == null ? string.Empty : s.status_name,
                         }).OrderByDescending(a => a.internal_reconcile_id).ToList();
            return query;
        }
        public List<fin_internal_reconcile_detail_vm> forInternalReconcileDetail(int entity_type_id, int entity_id, DateTime from_date)
        {
            var entity_type_id1 = new SqlParameter("@entity_type_id", entity_type_id);
            var entity_id1 = new SqlParameter("@entity_id", entity_id);
            var from_date1 = new SqlParameter("@from_date", from_date);
            var val = _scifferContext.Database.SqlQuery<fin_internal_reconcile_detail_vm>(
            "exec forInternalReconcileDetail @entity_type_id,@entity_id,@from_date", entity_type_id1, entity_id1, from_date1).ToList();
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
                  "exec cancel_internal_reconcile @id ,@cancellation_remarks ,@created_by,@cancellation_reason_id", pi_id, remarks, created_by, cancellation_reason).FirstOrDefault();
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
