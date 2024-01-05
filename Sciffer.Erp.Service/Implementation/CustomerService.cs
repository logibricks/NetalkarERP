using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System.Data.Entity;
using AutoMapper;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericService;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        public CustomerService(ScifferContext scifferContext, IGenericService GenericService)
        {
            _scifferContext = scifferContext;
            _genericService = GenericService;
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

        public List<CustomerVM> GetCustomerList()
        {
            var q = _scifferContext.REF_CUSTOMER.ToList();
            var query = (from cus in _scifferContext.REF_CUSTOMER
                         join st in _scifferContext.REF_STATE on cus.BILLING_STATE_ID equals st.STATE_ID
                         join st1 in _scifferContext.REF_STATE on cus.CORR_STATE_ID equals st1.STATE_ID
                         join c1 in _scifferContext.REF_COUNTRY on st.COUNTRY_ID equals c1.COUNTRY_ID
                         join c2 in _scifferContext.REF_COUNTRY on st1.COUNTRY_ID equals c2.COUNTRY_ID
                         join t in _scifferContext.REF_TERRITORY on cus.TERRITORY_ID equals t.TERRITORY_ID into j0
                         from terr in j0.DefaultIfEmpty()
                         join cp in _scifferContext.REF_CUSTOMER_PARENT on cus.CUSTOMER_PARENT_ID equals cp.CUSTOMER_PARENT_ID into j1
                         from cpr in j1.DefaultIfEmpty()
                         join p in _scifferContext.REF_PRIORITY on cus.PRIORITY_ID equals p.PRIORITY_ID into j2
                         from pp in j2.DefaultIfEmpty()
                         join pay in _scifferContext.REF_PAYMENT_TERMS on cus.PAYMENT_TERMS_ID equals pay.payment_terms_id into pay1
                         from pay2 in pay1.DefaultIfEmpty()
                         join p1 in _scifferContext.REF_PAYMENT_CYCLE on cus.PAYMENT_CYCLE_ID equals p1.PAYMENT_CYCLE_ID into cy1
                         from cy2 in cy1.DefaultIfEmpty()
                         join p2 in _scifferContext.REF_PAYMENT_CYCLE_TYPE on cy2.PAYMENT_CYCLE_TYPE_ID equals p2.PAYMENT_CYCLE_TYPE_ID into pct1
                         from pct2 in pct1.DefaultIfEmpty()
                         join f in _scifferContext.REF_FREIGHT_TERMS on cus.FREIGHT_TERMS_ID equals f.FREIGHT_TERMS_ID into ft1
                         from ft2 in ft1.DefaultIfEmpty()
                         join o in _scifferContext.REF_ORG_TYPE on cus.ORG_TYPE_ID equals o.ORG_TYPE_ID into o1
                         from o2 in o1.DefaultIfEmpty()
                         join bank in _scifferContext.ref_bank on cus.bank_id equals bank.bank_id into bank1
                         from bank2 in bank1.DefaultIfEmpty()
                         join category in _scifferContext.REF_CUSTOMER_CATEGORY on cus.CUSTOMER_CATEGORY_ID equals category.CUSTOMER_CATEGORY_ID into category1
                         from category2 in category1.DefaultIfEmpty()
                         join salesrm in _scifferContext.ref_sales_rm on cus.SALES_EXEC_ID equals salesrm.sales_rm_id into salesrm1
                         from salesrm2 in salesrm1.DefaultIfEmpty()
                         join currency in _scifferContext.REF_CURRENCY on cus.CREDIT_LIMIT_CURRENCY_ID equals currency.CURRENCY_ID into currency1
                         from currency2 in currency1.DefaultIfEmpty()
                         join tds in _scifferContext.ref_tds_code on cus.tds_id equals tds.tds_code_id into tdd
                         from tdscode in tdd.DefaultIfEmpty()
                         join gv in _scifferContext.ref_gst_customer_type on cus.gst_customer_type_id equals gv.gst_customer_type_id into j5
                         from gvv in j5.DefaultIfEmpty()
                         join gt in _scifferContext.ref_gst_tds_code on cus.gst_tds_id equals gt.gst_tds_code_id into j6
                         from gtt in j6.DefaultIfEmpty()
                         select new CustomerVM()
                         {
                             customer_id = cus.CUSTOMER_ID,
                             category_name = category2.CUSTOMER_CATEGORY_NAME,
                             customer_code = cus.CUSTOMER_CODE,
                             customer_name = cus.CUSTOMER_NAME,
                             customer_display_name = cus.CUSTOMER_DISPLAY_NAME,
                             org_type = o2 == null ? string.Empty : o2.ORG_TYPE_NAME,
                             billing_address = cus.BILLING_ADDRESS,
                             billing_city = cus.BILLING_CITY== null ? string.Empty : cus.BILLING_CITY,
                             billing_pincode = cus.BILLING_PINCODE,
                             billing_country = c1.COUNTRY_NAME,
                             billing_state = st == null ? string.Empty : st.STATE_NAME,
                             sales_rm_code = salesrm2==null?string.Empty:salesrm2.REF_EMPLOYEE.employee_code,
                             sales_rm_name = salesrm2 == null ? string.Empty : salesrm2.REF_EMPLOYEE.employee_name,
                             freight_terms = ft2 == null ? string.Empty : ft2.FREIGHT_TERMS_NAME,
                             has_parent = cus.HAS_PARENT,
                             customer_parent = (cpr == null ? String.Empty : cpr.CUSTOMER_PARENT_NAME),
                             territory = (terr == null ? String.Empty : terr.TERRITORY_NAME),
                             priority = (pp == null ? string.Empty : pp.PRIORITY_NAME),
                             blocked = cus.IS_BLOCKED,
                             bank_name = bank2 == null ? string.Empty : bank2.bank_name,
                             bank_account_number =cus == null ? string.Empty: cus.bank_account_number,
                             ifsc_code = cus.ifsc_code,
                             core_address = cus.CORR_ADDRESS,
                             core_city = cus == null ? string.Empty : cus.CORR_CITY,
                             core_pincode = cus.CORR_PINCODE,
                             core_state =st == null ? string.Empty : st.STATE_NAME,
                             core_country =c2 == null ? string.Empty : c2.COUNTRY_NAME,
                             primary_email = cus.EMAIL_ID_PRIMARY,
                             credit_limit = cus.CREDIT_LIMIT,
                             telephone_code = cus.std_code,
                             telephone = cus.TELEPHONE_PRIMARY,
                             website = cus.WEBSITE_ADDRESS,
                             fax = cus.FAX,
                             default_currency = currency2== null ? string.Empty : currency2.CURRENCY_NAME,
                             payment_terms = pay2==null? string.Empty: pay2.payment_terms_code,
                             payment_cycle_type = pct2 ==null ? string.Empty : pct2.PAYMENT_CYCLE_TYPE_NAME,
                             payment_cycle = cy2 == null ? "" : cy2.PAYMENT_CYCLE_NAME,
                             pan_no = cus.pan_no,
                             tds_no = tdscode == null ? string.Empty : tdscode.tds_code,
                             vat_tin_no =cus == null ? string.Empty : cus.vat_tin_no,
                             cst_tin_no = cus == null ? string.Empty : cus.cst_tin_no,
                             service_tax_no = cus.service_tax_no,
                             tds_id =cus== null ? 0 : cus.tds_id,
                             gst_no = cus.gst_no,
                             ecc_no = cus.ecc_no,
                             commisionerate = cus.commisionerate,
                             range = cus.range,
                             division = cus.division,
                             aditional_info = cus.ADDITIONAL_INFO,
                             attachment = cus.attachment,
                             vendor_code = cus.vendor_code,
                             gst_customer_type_name=gvv==null?string.Empty:gvv.gst_customer_type_name,
                             gst_registration_date=cus.gst_registration_date,
                             gst_tds_code=gtt==null?string.Empty:gtt.gst_tds_code + "/" + gtt.gst_tds_code_description,
                             billing_country_id = st.COUNTRY_ID,
                             billing_state_id = cus.BILLING_STATE_ID,
                             payment_terms_id = cus.PAYMENT_TERMS_ID,
                             payment_cycle_id = cus.PAYMENT_CYCLE_ID,
                             payment_cycle_type_id = pct2.PAYMENT_CYCLE_TYPE_ID,
                             default_currency_id = currency2==null?0:currency2.CURRENCY_ID,
                         }).OrderByDescending(a => a.customer_id).ToList();
            return query;
        }
        public List<REF_CUSTOMER_VM> GetAll()
        {
            Mapper.CreateMap<REF_CUSTOMER, REF_CUSTOMER_VM>();
            return _scifferContext.REF_CUSTOMER.Project().To<REF_CUSTOMER_VM>().ToList();
        }




        public REF_CUSTOMER_VM Get(int id)
        {
            REF_CUSTOMER customer = _scifferContext.REF_CUSTOMER.FirstOrDefault(c => c.CUSTOMER_ID == id);
            Mapper.CreateMap<REF_CUSTOMER, REF_CUSTOMER_VM>();
            REF_CUSTOMER_VM customervm = Mapper.Map<REF_CUSTOMER, REF_CUSTOMER_VM>(customer);
            if (customer.REF_CUSTOMER_CONTACTS != null)
            {
                customervm.REF_CUSTOMER_CONTACTS = customervm.REF_CUSTOMER_CONTACTS.Where(c => c.IS_ACTIVE == true).ToList();
            }

            var billingcountry = _scifferContext.REF_STATE.Where(s => s.STATE_ID == customervm.BILLING_STATE_ID)
           .Select(s => new
           {
               COUNTRY_ID = s.COUNTRY_ID
           }).Single();
            var corrcountry = _scifferContext.REF_STATE.Where(s => s.STATE_ID == customervm.CORR_STATE_ID)
            .Select(s => new
            {
                COUNTRY_ID = s.COUNTRY_ID
            }).Single();
            customervm.BILLING_COUNTRY_ID = billingcountry.COUNTRY_ID;
            customervm.CORR_COUNTRY_ID = corrcountry.COUNTRY_ID;
            return customervm;
        }

        public string Add(REF_CUSTOMER_VM cusotmerViewModel)
        {

            try
            {
                DataTable t11 = new DataTable();//for sub ledger
                t11.Columns.Add("entity_type_id", typeof(int));
                t11.Columns.Add("gl_ledger_id", typeof(int));
                t11.Columns.Add("ledger_account_type_id", typeof(int));
                if(cusotmerViewModel.ledger_account_type_id!=null)
                {
                    for (var i = 0; i < cusotmerViewModel.ledger_account_type_id.Count; i++)
                    {
                        t11.Rows.Add(1, int.Parse(cusotmerViewModel.gl_ledger_id[i]==""?"0": cusotmerViewModel.gl_ledger_id[i]), int.Parse(cusotmerViewModel.ledger_account_type_id[i] == "" ? "0" : cusotmerViewModel.ledger_account_type_id[i]));
                    }
                }
                
                DataTable t21 = new DataTable();//for contact
                t21.Columns.Add("contact_name", typeof(string));
                t21.Columns.Add("designation", typeof(string));
                t21.Columns.Add("email_address", typeof(string));
                t21.Columns.Add("mobile", typeof(string));
                t21.Columns.Add("phone", typeof(string));
                t21.Columns.Add("send_sms", typeof(bool));
                t21.Columns.Add("send_email", typeof(bool));
                t21.Columns.Add("is_active", typeof(bool));
                if (cusotmerViewModel.REF_CUSTOMER_CONTACTS != null)
                {
                    foreach (var contacts in cusotmerViewModel.REF_CUSTOMER_CONTACTS)
                    {
                        t21.Rows.Add(contacts.CONTACT_NAME, contacts.DESIGNATION, contacts.EMAIL_ADDRESS, contacts.MOBILE_NO, contacts.PHONE_NO, contacts.SEND_SMS_FLAG, contacts.SEND_EMAIL_FLAG, true);

                    }
                }

                DataTable t31 = new DataTable();
                t31.Columns.Add("item_category_id", typeof(int));
                t31.Columns.Add("rate", typeof(double));
                if(cusotmerViewModel.item_category_id !=null)
                {
                    for(var i =0;i<cusotmerViewModel.item_category_id.Count;i++)
                    {
                        t31.Rows.Add(cusotmerViewModel.item_category_id[i]==""?0:int.Parse(cusotmerViewModel.item_category_id[i]), cusotmerViewModel.rate[i] == "" ? 0 : double.Parse(cusotmerViewModel.rate[i]));
                    }
                }
                DateTime dte = new DateTime(1990, 1, 1);
                var loginId = HttpContext.Current.Session["User_Id"];
                var entity = new SqlParameter("@entity", "save");
                var customer_id = new SqlParameter("@customer_id", cusotmerViewModel.CUSTOMER_ID);
                var customer_category_id = new SqlParameter("@customer_category_id", cusotmerViewModel.CUSTOMER_CATEGORY_ID);
                var has_parent = new SqlParameter("@has_parent", cusotmerViewModel.HAS_PARENT);
                var customer_parent_id = new SqlParameter("@customer_parent_id", cusotmerViewModel.CUSTOMER_PARENT_ID == null ? 0 : cusotmerViewModel.CUSTOMER_PARENT_ID);
                var customer_code = new SqlParameter("@customer_code", cusotmerViewModel.CUSTOMER_CODE);
                var customer_name = new SqlParameter("@customer_name", cusotmerViewModel.CUSTOMER_NAME);
                var customer_display_name = new SqlParameter("@customer_display_name", cusotmerViewModel.CUSTOMER_DISPLAY_NAME);
                var org_type_id = new SqlParameter("@org_type_id", cusotmerViewModel.ORG_TYPE_ID);
                var priority_id = new SqlParameter("@priority_id", cusotmerViewModel.PRIORITY_ID == null ? 0 : cusotmerViewModel.PRIORITY_ID);
                var territory_id = new SqlParameter("@territory_id", cusotmerViewModel.TERRITORY_ID == null ? 0 : cusotmerViewModel.TERRITORY_ID);
                var billing_address = new SqlParameter("@billing_address", cusotmerViewModel.BILLING_ADDRESS);
                var billing_city = new SqlParameter("@billing_city", cusotmerViewModel.BILLING_CITY);
                var billing_pincode = new SqlParameter("@billing_pincode", cusotmerViewModel.BILLING_PINCODE);
                var billing_state_id = new SqlParameter("@billing_state_id", cusotmerViewModel.BILLING_STATE_ID);
                var corr_address = new SqlParameter("@corr_address", cusotmerViewModel.CORR_ADDRESS);
                var corr_city = new SqlParameter("@corr_city", cusotmerViewModel.CORR_CITY);
                var corr_pincode = new SqlParameter("@corr_pincode", cusotmerViewModel.CORR_PINCODE);
                var corr_state_id = new SqlParameter("@corr_state_id", cusotmerViewModel.CORR_STATE_ID);
                var additional_info = new SqlParameter("@additional_info", cusotmerViewModel.ADDITIONAL_INFO == null ? "" : cusotmerViewModel.ADDITIONAL_INFO);
                var email_id_primary = new SqlParameter("@email_id_primary", cusotmerViewModel.EMAIL_ID_PRIMARY == null ? "" : cusotmerViewModel.EMAIL_ID_PRIMARY);
                var telephone_primary = new SqlParameter("@telephone_primary", cusotmerViewModel.TELEPHONE_PRIMARY == null ? "" : cusotmerViewModel.TELEPHONE_PRIMARY);
                var fax = new SqlParameter("@fax", cusotmerViewModel.FAX == null ? string.Empty : cusotmerViewModel.FAX);
                var sales_exec_id = new SqlParameter("@sales_exec_id", cusotmerViewModel.SALES_EXEC_ID == null ? 0 : cusotmerViewModel.SALES_EXEC_ID);
                var payment_terms_id = new SqlParameter("@payment_terms_id", cusotmerViewModel.PAYMENT_TERMS_ID);
                var credit_limit = new SqlParameter("@credit_limit", cusotmerViewModel.CREDIT_LIMIT == null ? 0 : cusotmerViewModel.CREDIT_LIMIT);
                var credit_limit_currency_id = new SqlParameter("@credit_limit_currency_id", cusotmerViewModel.CREDIT_LIMIT_CURRENCY_ID);
                var is_blocked = new SqlParameter("@is_blocked", cusotmerViewModel.IS_BLOCKED);
                var payment_cycle_type_id = new SqlParameter("@payment_cycle_type_id", cusotmerViewModel.PAYMENT_CYCLE_TYPE_ID);
                var payment_cycle_id = new SqlParameter("@payment_cycle_id", cusotmerViewModel.PAYMENT_CYCLE_ID);
                var tds_applicable = new SqlParameter("@tds_applicable", cusotmerViewModel.TDS_APPLICABLE);
                var overall_discount = new SqlParameter("@overall_discount", cusotmerViewModel.OVERALL_DISCOUNT == null ? 0 : cusotmerViewModel.OVERALL_DISCOUNT);
                var freight_terms_id = new SqlParameter("@freight_terms_id", cusotmerViewModel.FREIGHT_TERMS_ID);
                var website_address = new SqlParameter("@website_address", cusotmerViewModel.WEBSITE_ADDRESS == null ? "" : cusotmerViewModel.WEBSITE_ADDRESS);
                cusotmerViewModel.CREATED_ON = DateTime.Now;
                var created_on = new SqlParameter("@created_on", cusotmerViewModel.CREATED_ON);
                var created_by = new SqlParameter("@created_by", cusotmerViewModel.CREATED_BY == null ? 0 : cusotmerViewModel.CREATED_BY);
                var std_code = new SqlParameter("@std_code", cusotmerViewModel.std_code == null ? "" : cusotmerViewModel.std_code);
                var pan_no = new SqlParameter("@pan_no", cusotmerViewModel.pan_no);
                var ecc_no = new SqlParameter("@ecc_no", cusotmerViewModel.ecc_no == null ? "" : cusotmerViewModel.ecc_no);
                var vat_tin_no = new SqlParameter("@vat_tin_no", cusotmerViewModel.vat_tin_no == null ? "" : cusotmerViewModel.vat_tin_no);
                var cst_tin_no = new SqlParameter("@cst_tin_no", cusotmerViewModel.cst_tin_no == null ? "" : cusotmerViewModel.cst_tin_no);
                var service_tax_no = new SqlParameter("@service_tax_no", cusotmerViewModel.service_tax_no == null ? "" : cusotmerViewModel.service_tax_no);
                var gst_no = new SqlParameter("@gst_no", cusotmerViewModel.gst_no == null ? "" : cusotmerViewModel.gst_no);
                var tds_id = new SqlParameter("@tds_id", cusotmerViewModel.tds_id == null ? 0 : cusotmerViewModel.tds_id);
                var bank_id = new SqlParameter("@bank_id", cusotmerViewModel.bank_id == null ? 0 : cusotmerViewModel.bank_id);
                var bank_account_number = new SqlParameter("@bank_account_number", cusotmerViewModel.bank_account_number == null ? "" : cusotmerViewModel.bank_account_number);
                var ifsc_code = new SqlParameter("@ifsc_code", cusotmerViewModel.ifsc_code == null ? "" : cusotmerViewModel.ifsc_code);
                var commisionerate = new SqlParameter("@commisionerate", cusotmerViewModel.commisionerate == null ? "" : cusotmerViewModel.commisionerate);
                var range = new SqlParameter("@range", cusotmerViewModel.range == null ? "" : cusotmerViewModel.range);
                var division = new SqlParameter("@division", cusotmerViewModel.division == null ? "" : cusotmerViewModel.division);
                var vendor_code = new SqlParameter("@vendor_code", cusotmerViewModel.vendor_code == null ? "" : cusotmerViewModel.vendor_code);
                var gst_customer_type_id = new SqlParameter("@gst_customer_type_id", cusotmerViewModel.gst_customer_type_id == null ? 0 : cusotmerViewModel.gst_customer_type_id);
                var gst_registration_date = new SqlParameter("@gst_registration_date", cusotmerViewModel.gst_registration_date == null ? dte : cusotmerViewModel.gst_registration_date);
                var gst_tds_applicable = new SqlParameter("@gst_tds_applicable", cusotmerViewModel.gst_tds_applicable);
                var gst_tds_id = new SqlParameter("@gst_tds_id", cusotmerViewModel.gst_tds_id == null ? 0 : cusotmerViewModel.gst_tds_id);
                cusotmerViewModel.attachment = cusotmerViewModel.FileUpload != null ? _genericService.GetFilePath("CustomerMaster", cusotmerViewModel.FileUpload) : "No File";
                var attachment = new SqlParameter("@attachment", cusotmerViewModel.attachment);
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                var modify_user = new SqlParameter("@modify_user", loginId);
                t1.TypeName = "dbo.temp_sub_ledger";
                t1.Value = t11;
                var t2 = new SqlParameter("@t2", SqlDbType.Structured);
                t2.TypeName = "dbo.temp_contact";
                t2.Value = t21;
                var t3 = new SqlParameter("@t3", SqlDbType.Structured);
                t3.TypeName = "dbo.temp_cus_ven_item_category";
                t3.Value = t31;
                var val = _scifferContext.Database.SqlQuery<string>(
                "exec SAVE_CUSTOMER @entity,@customer_id,@customer_category_id,@has_parent,@customer_parent_id,@customer_code,@customer_name,@customer_display_name,@org_type_id,@priority_id,@territory_id,@billing_address,@billing_city,@billing_pincode,@billing_state_id,@corr_address,@corr_city,@corr_pincode,@corr_state_id,@additional_info,@email_id_primary,@telephone_primary,@fax,@sales_exec_id,@payment_terms_id,@credit_limit,@credit_limit_currency_id,@is_blocked, @payment_cycle_type_id, @payment_cycle_id, @tds_applicable, @overall_discount, @freight_terms_id,@website_address,@created_on,@created_by,@std_code,@pan_no,@ecc_no,@vat_tin_no,@cst_tin_no,@service_tax_no,@gst_no,@tds_id,@attachment,@modify_user,@bank_id,@bank_account_number,@ifsc_code,@commisionerate,@range,@division,@vendor_code,@gst_customer_type_id,@gst_registration_date,@gst_tds_applicable,@gst_tds_id,@t1,@t2,@t3", entity, customer_id,
                customer_category_id, has_parent, customer_parent_id, customer_code, customer_name, customer_display_name, org_type_id, priority_id, territory_id,
                billing_address, billing_city, billing_pincode, billing_state_id, corr_address, corr_city, corr_pincode, corr_state_id, additional_info, email_id_primary,
                telephone_primary, fax, sales_exec_id, payment_terms_id, credit_limit, credit_limit_currency_id, is_blocked, payment_cycle_type_id, payment_cycle_id,
                tds_applicable, overall_discount, freight_terms_id, website_address, created_on, created_by, std_code, pan_no, ecc_no, vat_tin_no, cst_tin_no, service_tax_no,
                gst_no, tds_id, attachment, modify_user, bank_id, bank_account_number, ifsc_code, commisionerate, range, division, vendor_code, gst_customer_type_id, gst_registration_date, gst_tds_applicable,gst_tds_id, t1, t2, t3).FirstOrDefault();
                if (cusotmerViewModel.FileUpload != null)
                {
                    cusotmerViewModel.FileUpload.SaveAs(cusotmerViewModel.attachment);
                }
                if (val == "Saved")
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
                //--------------Log4Net
                log4net.GlobalContext.Properties["user"] = 0;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return ex.InnerException.ToString();
                //return "error";
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var country = _scifferContext.REF_CUSTOMER.FirstOrDefault(c => c.CUSTOMER_ID == id);
                _scifferContext.Entry(country).State = EntityState.Deleted;
                _scifferContext.SaveChanges();

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public bool AddExcel(List<customer_excel> customer, List<contact_excel> contact, List<item_excel> item, List<gl_excel> gl)
        {
            try
            {
                foreach (var cusotmerViewModel in customer)
                {
                    DataTable t31 = new DataTable();//for pro item
                    t31.Columns.Add("item_category_id", typeof(int));
                    t31.Columns.Add("rate", typeof(double));
                    if (item != null)
                    {
                        foreach (var items in item)
                        {
                            if (cusotmerViewModel.CUSTOMER_CODE == items.CUSTOMER_CODE)
                            {
                                t31.Rows.Add(items.item_category_id, items.rate);
                            }
                        }
                    }
                    DataTable t21 = new DataTable();//for contact
                    t21.Columns.Add("contact_name", typeof(string));
                    t21.Columns.Add("designation", typeof(string));
                    t21.Columns.Add("email_address", typeof(string));
                    t21.Columns.Add("mobile", typeof(string));
                    t21.Columns.Add("phone", typeof(string));
                    t21.Columns.Add("send_sms", typeof(bool));
                    t21.Columns.Add("send_email", typeof(bool));
                    t21.Columns.Add("is_active", typeof(bool));
                    foreach (var contacts in contact)
                    {
                        if (cusotmerViewModel.CUSTOMER_CODE == contacts.CUSTOMER_CODE)
                        {
                            t21.Rows.Add(contacts.CONTACT_NAME, contacts.DESIGNATION, contacts.EMAIL_ADDRESS, contacts.MOBILE_NO, contacts.PHONE_NO, contacts.SEND_SMS_FLAG, contacts.SEND_EMAIL_FLAG, true);
                        }
                    }
                    DataTable t11 = new DataTable();//for sub ledger
                    t11.Columns.Add("entity_type_id", typeof(int));
                    t11.Columns.Add("gl_ledger_id", typeof(int));
                    t11.Columns.Add("ledger_account_type_id", typeof(int));
                    if (gl != null)
                    {
                        foreach (var glCode in gl)
                        {
                            if (cusotmerViewModel.CUSTOMER_CODE == glCode.CUSTOMER_CODE)
                            {
                                t11.Rows.Add(1, glCode.gl_ledger_id, glCode.ledger_account_type_id);

                            }
                        }
                    }
                    int modifyuser = 0;
                    var entity = new SqlParameter("@entity", "save");
                    var customer_id = new SqlParameter("@customer_id", cusotmerViewModel.CUSTOMER_ID);
                    var customer_category_id = new SqlParameter("@customer_category_id", cusotmerViewModel.CUSTOMER_CATEGORY_ID);
                    var has_parent = new SqlParameter("@has_parent", cusotmerViewModel.HAS_PARENT);
                    var customer_parent_id = new SqlParameter("@customer_parent_id", cusotmerViewModel.CUSTOMER_PARENT_ID == null ? 0 : cusotmerViewModel.CUSTOMER_PARENT_ID);
                    var customer_code = new SqlParameter("@customer_code", cusotmerViewModel.CUSTOMER_CODE);
                    var customer_name = new SqlParameter("@customer_name", cusotmerViewModel.CUSTOMER_NAME);
                    var customer_display_name = new SqlParameter("@customer_display_name", cusotmerViewModel.CUSTOMER_DISPLAY_NAME);
                    var org_type_id = new SqlParameter("@org_type_id", cusotmerViewModel.ORG_TYPE_ID);
                    var priority_id = new SqlParameter("@priority_id", cusotmerViewModel.PRIORITY_ID == null ? 0 : cusotmerViewModel.PRIORITY_ID);
                    var territory_id = new SqlParameter("@territory_id", cusotmerViewModel.TERRITORY_ID == null ? 0 : cusotmerViewModel.TERRITORY_ID);
                    var billing_address = new SqlParameter("@billing_address", cusotmerViewModel.BILLING_ADDRESS);
                    var billing_city = new SqlParameter("@billing_city", cusotmerViewModel.BILLING_CITY);
                    var billing_pincode = new SqlParameter("@billing_pincode", cusotmerViewModel.BILLING_PINCODE);
                    var billing_state_id = new SqlParameter("@billing_state_id", cusotmerViewModel.BILLING_STATE_ID);
                    var corr_address = new SqlParameter("@corr_address", cusotmerViewModel.CORR_ADDRESS);
                    var corr_city = new SqlParameter("@corr_city", cusotmerViewModel.CORR_CITY);
                    var corr_pincode = new SqlParameter("@corr_pincode", cusotmerViewModel.CORR_PINCODE);
                    var corr_state_id = new SqlParameter("@corr_state_id", cusotmerViewModel.CORR_STATE_ID);
                    var additional_info = new SqlParameter("@additional_info", cusotmerViewModel.ADDITIONAL_INFO == null ? "" : cusotmerViewModel.ADDITIONAL_INFO);
                    var email_id_primary = new SqlParameter("@email_id_primary", cusotmerViewModel.EMAIL_ID_PRIMARY == null ? "" : cusotmerViewModel.EMAIL_ID_PRIMARY);
                    var telephone_primary = new SqlParameter("@telephone_primary", cusotmerViewModel.TELEPHONE_PRIMARY == null ? "" : cusotmerViewModel.TELEPHONE_PRIMARY);
                    var fax = new SqlParameter("@fax", cusotmerViewModel.FAX == null ? string.Empty : cusotmerViewModel.FAX);
                    var sales_exec_id = new SqlParameter("@sales_exec_id", cusotmerViewModel.SALES_EXEC_ID == null ? 0 : cusotmerViewModel.SALES_EXEC_ID);
                    var payment_terms_id = new SqlParameter("@payment_terms_id", cusotmerViewModel.PAYMENT_TERMS_ID);
                    var credit_limit = new SqlParameter("@credit_limit", cusotmerViewModel.CREDIT_LIMIT == null ? 0 : cusotmerViewModel.CREDIT_LIMIT);
                    var credit_limit_currency_id = new SqlParameter("@credit_limit_currency_id", cusotmerViewModel.CREDIT_LIMIT_CURRENCY_ID);
                    var is_blocked = new SqlParameter("@is_blocked", cusotmerViewModel.IS_BLOCKED);
                    var payment_cycle_type_id = new SqlParameter("@payment_cycle_type_id", cusotmerViewModel.PAYMENT_CYCLE_TYPE_ID);
                    var payment_cycle_id = new SqlParameter("@payment_cycle_id", cusotmerViewModel.PAYMENT_CYCLE_ID);
                    var tds_applicable = new SqlParameter("@tds_applicable", cusotmerViewModel.TDS_APPLICABLE);
                    var overall_discount = new SqlParameter("@overall_discount", cusotmerViewModel.OVERALL_DISCOUNT == null ? 0 : cusotmerViewModel.OVERALL_DISCOUNT);
                    var freight_terms_id = new SqlParameter("@freight_terms_id", cusotmerViewModel.FREIGHT_TERMS_ID);
                    var website_address = new SqlParameter("@website_address", cusotmerViewModel.WEBSITE_ADDRESS == null ? "" : cusotmerViewModel.WEBSITE_ADDRESS);
                    cusotmerViewModel.CREATED_ON = DateTime.Now;
                    var created_on = new SqlParameter("@created_on", cusotmerViewModel.CREATED_ON);
                    var created_by = new SqlParameter("@created_by", cusotmerViewModel.CREATED_BY == null ? 0 : cusotmerViewModel.CREATED_BY);
                    var std_code = new SqlParameter("@std_code", cusotmerViewModel.std_code == null ? "" : cusotmerViewModel.std_code);
                    var pan_no = new SqlParameter("@pan_no", cusotmerViewModel.pan_no);
                    var ecc_no = new SqlParameter("@ecc_no", cusotmerViewModel.ecc_no == null ? "" : cusotmerViewModel.ecc_no);
                    var vat_tin_no = new SqlParameter("@vat_tin_no", cusotmerViewModel.vat_tin_no == null ? "" : cusotmerViewModel.vat_tin_no);
                    var cst_tin_no = new SqlParameter("@cst_tin_no", cusotmerViewModel.cst_tin_no == null ? "" : cusotmerViewModel.cst_tin_no);
                    var service_tax_no = new SqlParameter("@service_tax_no", cusotmerViewModel.service_tax_no == null ? "" : cusotmerViewModel.service_tax_no);
                    var gst_no = new SqlParameter("@gst_no", cusotmerViewModel.gst_no == null ? "" : cusotmerViewModel.gst_no);
                    var tds_id = new SqlParameter("@tds_id", cusotmerViewModel.tds_id == null ? 0 : cusotmerViewModel.tds_id);
                    var bank_id = new SqlParameter("@bank_id", cusotmerViewModel.bank_id == null ? 0 : cusotmerViewModel.bank_id);
                    var bank_account_number = new SqlParameter("@bank_account_number", cusotmerViewModel.bank_account_no == null ? "" : cusotmerViewModel.bank_account_no);
                    var ifsc_code = new SqlParameter("@ifsc_code", cusotmerViewModel.ifsc_code == null ? "" : cusotmerViewModel.ifsc_code);
                    var commisionerate = new SqlParameter("@commisionerate", cusotmerViewModel.commisionerate == null ? "" : cusotmerViewModel.commisionerate);
                    var range = new SqlParameter("@range", cusotmerViewModel.range == null ? "" : cusotmerViewModel.range);
                    var division = new SqlParameter("@division", cusotmerViewModel.division == null ? "" : cusotmerViewModel.division);
                    var vendor_code = new SqlParameter("@vendor_code", cusotmerViewModel.vendor_code == null ? "" : cusotmerViewModel.vendor_code);
                    cusotmerViewModel.attachment = cusotmerViewModel.FileUpload != null ? _genericService.GetFilePath("CustomerMaster", cusotmerViewModel.FileUpload) : "No File";
                    var attachment = new SqlParameter("@attachment", cusotmerViewModel.attachment);
                    var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                    var modify_user = new SqlParameter("@modify_user", modifyuser);
                    t1.TypeName = "dbo.temp_sub_ledger";
                    t1.Value = t11;
                    var t2 = new SqlParameter("@t2", SqlDbType.Structured);
                    t2.TypeName = "dbo.temp_contact";
                    t2.Value = t21;
                    var t3 = new SqlParameter("@t3", SqlDbType.Structured);
                    t3.TypeName = "dbo.temp_cus_ven_item_category";
                    t3.Value = t31;
                    var val = _scifferContext.Database.SqlQuery<string>(
                    "exec SAVE_CUSTOMER @entity,@customer_id,@customer_category_id,@has_parent,@customer_parent_id,@customer_code,@customer_name,@customer_display_name,@org_type_id,@priority_id,@territory_id,@billing_address,@billing_city,@billing_pincode,@billing_state_id,@corr_address,@corr_city,@corr_pincode,@corr_state_id,@additional_info,@email_id_primary,@telephone_primary,@fax,@sales_exec_id,@payment_terms_id,@credit_limit,@credit_limit_currency_id,@is_blocked, @payment_cycle_type_id, @payment_cycle_id, @tds_applicable, @overall_discount, @freight_terms_id,@website_address,@created_on,@created_by,@std_code,@pan_no,@ecc_no,@vat_tin_no,@cst_tin_no,@service_tax_no,@gst_no,@tds_id,@attachment,@modify_user,@bank_id,@bank_account_number,@ifsc_code,@commisionerate,@range,@division,@vendor_code,@t1,@t2,@t3", entity, customer_id,
                    customer_category_id, has_parent, customer_parent_id, customer_code, customer_name, customer_display_name, org_type_id, priority_id, territory_id,
                    billing_address, billing_city, billing_pincode, billing_state_id, corr_address, corr_city, corr_pincode, corr_state_id, additional_info, email_id_primary,
                    telephone_primary, fax, sales_exec_id, payment_terms_id, credit_limit, credit_limit_currency_id, is_blocked, payment_cycle_type_id, payment_cycle_id,
                    tds_applicable, overall_discount, freight_terms_id, website_address, created_on, created_by, std_code, pan_no, ecc_no, vat_tin_no, cst_tin_no, service_tax_no,
                    gst_no, tds_id, attachment, modify_user, bank_id, bank_account_number, ifsc_code, commisionerate, range, division, vendor_code, t1, t2, t3).FirstOrDefault();
                    if (cusotmerViewModel.FileUpload != null)
                    {
                        cusotmerViewModel.FileUpload.SaveAs(cusotmerViewModel.attachment);
                    }
                    if (val == "Saved")
                    {
                        //return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public ref_customer_balance_VM GetDetails(int id)
        {
            throw new NotImplementedException();
        }
    }
}
