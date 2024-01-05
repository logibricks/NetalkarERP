using AutoMapper;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class CreditDebitNoteTransactionService : ICreditDebitNoteTransactionService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        private readonly ScifferContext _scifferContext;

        public CreditDebitNoteTransactionService(ScifferContext scifferContext)
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
                dt.Columns.Add("item_type_id", typeof(int));
                dt.Columns.Add("sac_hsn_id", typeof(int));
                dt.Columns.Add("tax_rate", typeof(int));
                dt.Columns.Add("exclusive_inclusive", typeof(int));



                for (var i = 0; i < cd.gl_ledger_id1.Count; i++)
                {
                    if (cd.gl_ledger_id1[i] != "")
                    {
                        dt.Rows.Add(cd.fin_credit_debit_node_detail_id==null?0:cd.fin_credit_debit_node_detail_id[i] == null ? 0 :int.Parse(cd.fin_credit_debit_node_detail_id[i]),
                            cd.gl_ledger_id1==null?0: cd.gl_ledger_id1[i] == "" ? 0 : int.Parse(cd.gl_ledger_id1[i]),
                            cd.user_description==null?"":cd.user_description[i],
                            cd.credit_debit_amount == null ? 0 :  cd.credit_debit_amount[i] == "" ? 0 :  double.Parse(cd.credit_debit_amount[i]),
                            cd.tax_id1 == null ? 0 : cd.tax_id1[i] == "" ? 0 :  int.Parse(cd.tax_id1[i]),
                            cd.cost_center_id1 == null ? 0 : cd.cost_center_id1[i] == "" ? 0 :  int.Parse(cd.cost_center_id1[i]),
                            cd.item_type_id1 == null ? 0 : cd.item_type_id1[i] == "" ? 0 : int.Parse(cd.item_type_id1[i]),
                            cd.hsn_sac_id1 == null ? 0 :  cd.hsn_sac_id1[i] == "" ? 0 :  int.Parse(cd.hsn_sac_id1[i]),
                            cd.tax_rate1 == null ? 0 : cd.tax_rate1[i] == "" ? 0 :  int.Parse(cd.tax_rate1[i]),
                            cd.exclusive_inclusive1 == null ? 0 : cd.exclusive_inclusive1[i] == null ? 0 :  cd.exclusive_inclusive1[i]==""?0: int.Parse(cd.exclusive_inclusive1[i]));
                    }
                }



                var detail1 = new SqlParameter("@crdr", SqlDbType.Structured);
                detail1.TypeName = "dbo.temp_fin_credit_debit_note_details";
                detail1.Value = dt;


                DataTable ds = new DataTable();
                ds.Columns.Add("fin_credit_debit_note_transaction_id ", typeof(int));
                ds.Columns.Add("module_form_code ", typeof(string));
                ds.Columns.Add("document_id ", typeof(int));
                ds.Columns.Add("taxable_value ", typeof(decimal));
                ds.Columns.Add("dr_cr_amount ", typeof(decimal));

                if(cd.document_id1 != null)
                {
                    for (var i = 0; i < cd.document_id1.Count; i++)
                    {

                        if (cd.document_id1[i] != "")
                        {

                            ds.Rows.Add(0, cd.module_form_code1[i], cd.document_id1[i] == "" ? 0 : int.Parse(cd.document_id1[i]),
                            cd.taxable_value1[i] == null ? 0 : decimal.Parse(cd.taxable_value1[i]), cd.dr_cr_amount1[i] == "" ? 0 : decimal.Parse(cd.dr_cr_amount1[i]));

                        }

                    }
                }
               



                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_fin_credit_debit_transactions";
                t1.Value = ds;

                DateTime dt1 = new DateTime(0001, 01, 01);

                DateTime dt2 = new DateTime(1990, 01, 01);
                int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString()); 
                var fin_credit_debit_node_id = new SqlParameter("@fin_credit_debit_node_id", cd.fin_credit_debit_node_id);
                var category_id = new SqlParameter("@category_id", cd.category_id);
                var document_no = new SqlParameter("@document_no", cd.document_no == null ? string.Empty : cd.document_no);
                var credit_debit_id = new SqlParameter("@credit_debit_id", cd.credit_debit_id);
                var entity_type_id = new SqlParameter("@entity_type_id", cd.entity_type_id);
                var entity_id = new SqlParameter("@entity_id", cd.entity_id);
                var posting_date = new SqlParameter("@posting_date", cd.posting_date==dt1?dt2:cd.posting_date);
                var currency_id = new SqlParameter("@currency_id", cd.currency_id);
                var remarks = new SqlParameter("@remarks", cd.remarks == null ? string.Empty : cd.remarks);
                var created_by = new SqlParameter("@created_by", user);
                var total_amount = new SqlParameter("@total_amount", cd.total_amount);
                var business_unit_id = new SqlParameter("@business_unit_id", cd.business_unit_id == null ? 0 : cd.business_unit_id);
                var payment_terms_id = new SqlParameter("@payment_terms_id", cd.payment_terms_id);
                var billing_address = new SqlParameter("@billing_address", cd.billing_address == null ? "" : cd.billing_address);
                var billing_city = new SqlParameter("@billing_city", cd.billing_city == null ? "" : cd.billing_city);
                var billing_pin_code = new SqlParameter("@billing_pin_code", cd.billing_pin_code == null ? "" : cd.billing_pin_code);
                var billing_state_id = new SqlParameter("@billing_state_id", cd.billing_state_id);
                var billing_country_id = new SqlParameter("@billing_country_id", cd.billing_country_id);
                var gstin_no = new SqlParameter("@gstin_no", cd.gstin_no == null ? "" : cd.gstin_no);
                var pan_no = new SqlParameter("@pan_no", cd.pan_no == null ? "" : cd.pan_no);
                var payment_cycle_id = new SqlParameter("@payment_cycle_id", cd.payment_cycle_id);
                var payment_cycle_type_id = new SqlParameter("@payment_cycle_type_id", cd.payment_cycle_type_id);
                var internal_remarks = new SqlParameter("@internal_remarks", cd.internal_remarks == null ? "" : cd.internal_remarks);
                var remarks_on_document = new SqlParameter("@remarks_on_document", cd.remarks_on_document == null ? "" : cd.remarks_on_document);
                var attachement = new SqlParameter("@attachement", cd.attachement == null ? "" : cd.attachement);
                var gst_category_id = new SqlParameter("@gst_category_id", cd.gst_category_id == null ? 0 : cd.gst_category_id);
                var gross_value = new SqlParameter("@gross_value", cd.gross_value == null ? 0 : cd.gross_value);
                var net_value = new SqlParameter("@net_value", cd.net_value == null ? 0 : cd.net_value);
                var email_id = new SqlParameter("@email_id", cd.email_id == null ? "" : cd.email_id);
                var is_rcm = new SqlParameter("@is_rcm", cd.is_rcm);
                var round_off = new SqlParameter("@round_off", cd.round_off == null ? 0 : cd.round_off);
                var tds_code_id = new SqlParameter("@tds_code_id", cd.tds_code_id == null ? 0 : cd.tds_code_id);


                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_fin_credit_debit_transaction @fin_credit_debit_node_id ,@category_id ,@document_no ,@credit_debit_id ,@entity_type_id ,@entity_id ,@posting_date ,@currency_id ,@remarks ,@created_by,@total_amount ,@business_unit_id,@payment_terms_id,@billing_address,@billing_city,@billing_pin_code,@billing_state_id,@billing_country_id,@gstin_no,@pan_no,@gst_category_id,@email_id,@payment_cycle_id,@payment_cycle_type_id,@internal_remarks,@remarks_on_document,@attachement,@round_off,@tds_code_id,@is_rcm,@gross_value,@net_value, @crdr,@t1 ",
                    fin_credit_debit_node_id, category_id, document_no, credit_debit_id, entity_type_id, entity_id, posting_date, currency_id, remarks, created_by, total_amount, business_unit_id, payment_terms_id, billing_address, billing_city, billing_pin_code, billing_state_id, billing_country_id, gstin_no, pan_no, gst_category_id, email_id, payment_cycle_id, payment_cycle_type_id, internal_remarks, remarks_on_document, attachement, round_off, tds_code_id, is_rcm, gross_value, net_value, detail1, t1).FirstOrDefault();
                if (val.Contains("Saved"))
                {
                    return val;
                }
                else
                {
                    return val;
                }
            }
            catch (Exception ex)
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

            List<fin_credit_debit_note_detail_vm> fcd = new List<fin_credit_debit_note_detail_vm>();

            var cd = (from debit_detail in invoice.fin_credit_debit_node_detail
                      join i in _scifferContext.ref_item_type on debit_detail.item_type_id equals i.item_type_id

                      select new fin_credit_debit_note_detail_vm
                      {

                          credit_debit_amount = debit_detail.credit_debit_amount,
                          cost_center = debit_detail.ref_cost_center == null ? string.Empty : debit_detail.ref_cost_center.cost_center_code,
                          description = debit_detail.ref_general_ledger.gl_ledger_code + "/" + debit_detail.ref_general_ledger.gl_ledger_name,
                          item_type_name = i.item_type_name,
                          user_description = debit_detail.user_description,
                          tax_name = debit_detail.ref_tax.tax_code + "/" + debit_detail.ref_tax.tax_name,
                          tax_id = debit_detail.ref_tax.tax_id,
                          exclusive_inclusive = debit_detail.exclusive_inclusive == "1" ? "Exclusive" : "Inclusive",
                          tax_rate = debit_detail.tax_rate,
                          fin_credit_debit_note_detail_id = debit_detail.fin_credit_debit_node_detail_id,
                          hsn_code = debit_detail.item_type_id == 1 ? debit_detail.sac_hsn_id == 0 ? string.Empty : _scifferContext.ref_hsn_code.FirstOrDefault(x => x.hsn_code_id == debit_detail.sac_hsn_id).hsn_code : debit_detail.sac_hsn_id == 0 ? string.Empty : _scifferContext.ref_sac.FirstOrDefault(x => x.sac_id == debit_detail.sac_hsn_id).sac_code,

                      }).ToList();


            vm.fin_credit_debit_note_detail_vm = cd;


            var val = _scifferContext.Database.SqlQuery<GetFinCreditDebitTransactionById>("exec GetFinCreditDebitTransactionById @fin_credit_debit_note_id", new SqlParameter("fin_credit_debit_note_id", id)).ToList<GetFinCreditDebitTransactionById>();
            vm.GetFinCreditDebitTransactionById = val;

            return vm;
        
        }

        public List<fin_credit_debit_note_vm> GetAll(int id)
        {
            var query = (from cd in _scifferContext.fin_credit_debit_note.Where(x => x.credit_debit_id == id)
                         join d in _scifferContext.ref_document_numbring on cd.category_id equals d.document_numbring_id
                         join c1 in _scifferContext.REF_CUSTOMER on cd.entity_id equals c1.CUSTOMER_ID into j1
                         from c2 in j1.DefaultIfEmpty()
                         join e1 in _scifferContext.REF_EMPLOYEE on cd.entity_id equals e1.employee_id into j2
                         from e2 in j2.DefaultIfEmpty()
                         join v1 in _scifferContext.REF_VENDOR on cd.entity_id equals v1.VENDOR_ID into j3
                         from v2 in j3.DefaultIfEmpty()
                         join rc in _scifferContext.ref_cancellation_reason on cd.cancellation_reason_id equals rc.cancellation_reason_id into j4
                         from rcc in j4.DefaultIfEmpty()
                         join u in _scifferContext.ref_user_management on cd.cancelled_by equals u.user_id into j5
                         from uu in j5.DefaultIfEmpty()
                         join c in _scifferContext.REF_CURRENCY on cd.currency_id equals c.CURRENCY_ID
                         select new fin_credit_debit_note_vm
                         {
                             document_no = cd.document_no,
                             fin_credit_debit_node_id = cd.fin_credit_debit_node_id,
                             entity_type_name = cd.entity_type_id == 1 ? "Customer" : cd.entity_type_id == 2 ? "Vendor" : "Employee",
                             entity_code = cd.entity_type_id == 1 ? c2.CUSTOMER_CODE : cd.entity_type_id == 2 ? v2.VENDOR_CODE : e2.employee_code,
                             entity_name = cd.entity_type_id == 1 ? c2.CUSTOMER_NAME : cd.entity_type_id == 2 ? v2.VENDOR_NAME : e2.employee_name,
                             currency_name = c.CURRENCY_NAME,
                             remarks = cd.remarks,
                             category_name = d.category,
                             posting_date = cd.posting_date,
                             cancelled_by_user = uu == null ? "" : uu.notes,
                             cancellation_reason = rcc == null ? "" : rcc.cancellation_reason,
                             cancellation_remarks = cd.cancellation_remarks,
                         }).OrderByDescending(a => a.fin_credit_debit_node_id).ToList();
            return query;
        }
        public fin_credit_debit_note_vm GetCDNForReport(int id)
        {
            var fin_credit_debit_node_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getheader");
            var val = _scifferContext.Database.SqlQuery<fin_credit_debit_note_vm>(
            "exec get_credit_note @entity,@id", entity, fin_credit_debit_node_id).FirstOrDefault();
            return val;
        }
        public List<fin_credit_debit_note_detail_vm> GetCDNDetailsForReport(int id)
        {
            var fin_credit_debit_node_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getdetail");
            var val = _scifferContext.Database.SqlQuery<fin_credit_debit_note_detail_vm>(
            "exec get_credit_note @entity,@id", entity, fin_credit_debit_node_id).ToList();
            return val;
        }
        public string Delete(int id, string cancellation_remarks, int reason_id)
        {
            try
            {
                int create_user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var iex_id = new SqlParameter("@id", id);
                var remarks = new SqlParameter("@cancellation_remarks", cancellation_remarks == null ? "" : cancellation_remarks);
                var created_by = new SqlParameter("@created_by", create_user);
                var cancellation_reason_id = new SqlParameter("@cancellation_reason_id", reason_id);
                var val = _scifferContext.Database.SqlQuery<string>(
                  "exec cancel_credit_debit_note @id ,@cancellation_reason_id,@cancellation_remarks,@created_by", iex_id,cancellation_reason_id, remarks, created_by ).FirstOrDefault();
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
