using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.ViewModel;
using System.Data.SqlClient;
using System.Data;
using Sciffer.Erp.Domain.Model;
using AutoMapper;

namespace Sciffer.Erp.Service.Implementation
{
    public class CreditDebitNoteService : ICreditDebitNoteService
    {
        private readonly ScifferContext _scifferContext;

        public CreditDebitNoteService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;            
        }

        public string Add(fin_credit_debit_note_vm cd)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("fin_credit_debit_node_detail_id ", typeof(int));
                dt.Columns.Add("gl_ledger_id ", typeof(int));
                dt.Columns.Add("user_description ", typeof(string));
                dt.Columns.Add("credit_debit_amount ", typeof(double));
                dt.Columns.Add("tax_id ", typeof(int));
                dt.Columns.Add("cost_center_id ", typeof(int));
                for (var i=0;i<cd.gl_ledger_id1.Count;i++)
                {
                    if(cd.gl_ledger_id1[i]!="")
                    {
                        dt.Rows.Add(0,cd.gl_ledger_id1[i]==""?0:int.Parse(cd.gl_ledger_id1[i]),
                            cd.user_description[i],cd.credit_debit_amount[i]==""?0:double.Parse(cd.credit_debit_amount[i]),
                            cd.tax_id1[i]==""?0:int.Parse(cd.tax_id1[i]), cd.cost_center_id1[i] == "" ? 0 : int.Parse(cd.cost_center_id1[i]));
                    }
                }
                int user = 0;
                var fin_credit_debit_node_id = new SqlParameter("@fin_credit_debit_node_id", cd.fin_credit_debit_node_id);
                var category_id = new SqlParameter("@category_id", cd.category_id);
                var document_no = new SqlParameter("@document_no", cd.document_no==null?string.Empty:cd.document_no);
                var credit_debit_id = new SqlParameter("@credit_debit_id", cd.credit_debit_id);
                var entity_type_id = new SqlParameter("@entity_type_id", cd.entity_type_id);
                var entity_id = new SqlParameter("@entity_id", cd.entity_id);
                var posting_date = new SqlParameter("@posting_date", cd.posting_date);
                var currency_id = new SqlParameter("@currency_id", cd.currency_id);
                var remarks = new SqlParameter("@remarks", cd.remarks==null?string.Empty:cd.remarks);
                var created_by = new SqlParameter("@created_by", user);
                var total_amount = new SqlParameter("@total_amount", cd.total_amount);
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_fin_credit_debit_note_detail";
                t1.Value = dt;
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_credit_debit_note @fin_credit_debit_node_id ,@category_id ,@document_no ,@credit_debit_id ,@entity_type_id ,@entity_id ,@posting_date ,@currency_id ,@remarks ,@created_by,@total_amount ,@t1 ", 
                    fin_credit_debit_node_id, category_id, document_no, credit_debit_id, entity_type_id, entity_id, posting_date, 
                    currency_id, remarks, created_by, total_amount, t1).FirstOrDefault();
                if(val.Contains("Saved"))
                {
                    return val;
                }
                else
                {
                    return val;
                }               
            }
            catch(Exception ex)
            {
                return ex.Message;
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

        public fin_credit_debit_note_vm Get(int id)
        {
            fin_credit_debit_note invoice = _scifferContext.fin_credit_debit_note.FirstOrDefault(c => c.fin_credit_debit_node_id == id);
            Mapper.CreateMap<fin_credit_debit_note, fin_credit_debit_note_vm>();
            fin_credit_debit_note_vm vm = Mapper.Map<fin_credit_debit_note, fin_credit_debit_note_vm>(invoice);
            vm.entity_code = vm.entity_type_id == 1 ? _scifferContext.REF_CUSTOMER.Where(x => x.CUSTOMER_ID == vm.entity_id).FirstOrDefault().CUSTOMER_CODE : vm.entity_type_id == 2 ? _scifferContext.REF_VENDOR.Where(x => x.VENDOR_ID == vm.entity_id).FirstOrDefault().VENDOR_CODE : _scifferContext.REF_EMPLOYEE.Where(x => x.employee_id == vm.entity_id).FirstOrDefault().employee_code;
            vm.entity_name = vm.entity_type_id == 1 ? _scifferContext.REF_CUSTOMER.Where(x => x.CUSTOMER_ID == vm.entity_id).FirstOrDefault().CUSTOMER_NAME : vm.entity_type_id == 2 ? _scifferContext.REF_VENDOR.Where(x => x.VENDOR_ID == vm.entity_id).FirstOrDefault().VENDOR_NAME : _scifferContext.REF_EMPLOYEE.Where(x => x.employee_id == vm.entity_id).FirstOrDefault().employee_name;
            return vm;
           // quotationvm.SAL_SI_DETAIL = quotationvm.SAL_SI_DETAIL.Where(c => c.is_active == true).ToList();
        }

        public List<fin_credit_debit_note_vm> GetAll(int id)
        {
            var query = (from cd in _scifferContext.fin_credit_debit_note.Where(x=>x.credit_debit_id==id)
                         join d in _scifferContext.ref_document_numbring on cd.category_id equals d.document_numbring_id
                         join c1 in _scifferContext.REF_CUSTOMER on cd.entity_id equals c1.CUSTOMER_ID into j1
                         from c2 in j1.DefaultIfEmpty()
                         join e1 in _scifferContext.REF_EMPLOYEE on cd.entity_id equals e1.employee_id into j2
                         from e2 in j2.DefaultIfEmpty()
                         join v1 in _scifferContext.REF_VENDOR on cd.entity_id equals v1.VENDOR_ID into j3
                         from v2 in j3.DefaultIfEmpty()
                         join c in _scifferContext.REF_CURRENCY on cd.currency_id equals c.CURRENCY_ID
                         select new fin_credit_debit_note_vm {
                             document_no = cd.document_no,
                             fin_credit_debit_node_id = cd.fin_credit_debit_node_id,
                             entity_type_name = cd.entity_type_id == 1 ? "Customer" : cd.entity_type_id == 2 ? "Vendor" : "Employee",
                             entity_code = cd.entity_type_id == 1 ? c2.CUSTOMER_CODE : cd.entity_type_id == 2 ? v2.VENDOR_CODE : e2.employee_code,
                             entity_name = cd.entity_type_id == 1 ? c2.CUSTOMER_NAME : cd.entity_type_id == 2 ? v2.VENDOR_NAME : e2.employee_name,
                             currency_name = c.CURRENCY_NAME,
                             remarks = cd.remarks,
                             category_name=d.category,
                             posting_date=cd.posting_date,
                         }).OrderByDescending(a => a.fin_credit_debit_node_id).ToList();
            return query;
        }
 
    }
}
