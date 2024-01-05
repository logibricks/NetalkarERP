using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class VendorService : IVendorService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericService;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        public VendorService(ScifferContext scifferContext, IGenericService GenericService)
        {
            _scifferContext = scifferContext;
            _genericService = GenericService;
        }

        public List<VendorVM1> GetVendorList()
        {
            var query = (from i in _scifferContext.REF_VENDOR
                         select new VendorVM1
                         {
                             VENDOR_ID = i.VENDOR_ID,
                             VENDOR_CODE = i.VENDOR_CODE,
                             VENDOR_NAME = i.VENDOR_NAME,

                         }).ToList();
            return query;

        }
       
        public string Add(REF_VENDOR_VM vendorViewModel)
        {
            try
            {
                DataTable t11 = new DataTable();//for sub ledger
                t11.Columns.Add("entity_type_id", typeof(int));
                t11.Columns.Add("gl_ledger_id", typeof(int));
                t11.Columns.Add("ledger_account_type_id", typeof(int));
                if (vendorViewModel.ledger_account_type_id != null)
                {
                    for (var i = 0; i < vendorViewModel.ledger_account_type_id.Count; i++)
                    {
                        t11.Rows.Add(1, int.Parse(vendorViewModel.gl_ledger_id[i] == "" ? "0" : vendorViewModel.gl_ledger_id[i]), int.Parse(vendorViewModel.ledger_account_type_id[i] == "" ? "0" : vendorViewModel.ledger_account_type_id[i]));
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
                if (vendorViewModel.REF_VENDOR_CONTACTS != null)
                {
                    foreach (var contacts in vendorViewModel.REF_VENDOR_CONTACTS)
                    {
                        t21.Rows.Add(contacts.CONTACT_NAME, contacts.DESIGNATION, contacts.EMAIL_ADDRESS, contacts.MOBILE_NO, contacts.PHONE_NO, contacts.SEND_SMS_FLAG, contacts.SEND_EMAIL_FLAG, true);

                    }
                }
                DataTable t31 = new DataTable();
                t31.Columns.Add("item_category_id", typeof(int));
                t31.Columns.Add("rate", typeof(double));
                if (vendorViewModel.item_category_id != null)
                {
                    for (var i = 0; i < vendorViewModel.item_category_id.Count; i++)
                    {
                        t31.Rows.Add(vendorViewModel.item_category_id[i] == "" ? 0 : int.Parse(vendorViewModel.item_category_id[i]), vendorViewModel.rate[i] == "" ? 0 : double.Parse(vendorViewModel.rate[i]));
                    }
                }
                DateTime dte = new DateTime(1990, 1, 1);
                var loginId = HttpContext.Current.Session["User_Id"];
                var modify_user = new SqlParameter("@modify_user", loginId);
                var entity = new SqlParameter("@entity", "save");
                var vendor_id = new SqlParameter("@vendor_id", vendorViewModel.VENDOR_ID);
                var vendor_category_id = new SqlParameter("@vendor_category_id", vendorViewModel.VENDOR_CATEGORY_ID);
                var has_parent = new SqlParameter("@has_parent", vendorViewModel.HAS_PARENT);
                var vendor_parent_id = new SqlParameter("@vendor_parent_id", vendorViewModel.VENDOR_PARENT_ID == null ? 0 : vendorViewModel.VENDOR_PARENT_ID);
                var vendor_code = new SqlParameter("@vendor_code", vendorViewModel.VENDOR_CODE);
                var vendor_name = new SqlParameter("@vendor_name", vendorViewModel.VENDOR_NAME);
                var vendor_display_name = new SqlParameter("@vendor_display_name", vendorViewModel.VENDOR_DISPLAY_NAME);
                var org_type_id = new SqlParameter("@org_type_id", vendorViewModel.ORG_TYPE_ID);
                var priority_id = new SqlParameter("@priority_id", vendorViewModel.PRIORITY_ID == null ? 0 : vendorViewModel.PRIORITY_ID);
                var billing_address = new SqlParameter("@billing_address", vendorViewModel.BILLING_ADDRESS);
                var billing_city = new SqlParameter("@billing_city", vendorViewModel.BILLING_CITY);
                var billing_pincode = new SqlParameter("@billing_pincode", vendorViewModel.BILLING_PINCODE==null?"":vendorViewModel.BILLING_PINCODE);
                var billing_state_id = new SqlParameter("@billing_state_id", vendorViewModel.BILLING_STATE_ID);
                var corr_address = new SqlParameter("@corr_address", vendorViewModel.CORR_ADDRESS);
                var corr_city = new SqlParameter("@corr_city", vendorViewModel.CORR_CITY);
                var corr_pincode = new SqlParameter("@corr_pincode", vendorViewModel.CORR_PINCODE==null?"":vendorViewModel.CORR_PINCODE);
                var corr_state_id = new SqlParameter("@corr_state_id", vendorViewModel.CORR_STATE_ID);
                var additional_info = new SqlParameter("@additional_info", vendorViewModel.ADDITIONAL_INFO == null ? "" : vendorViewModel.ADDITIONAL_INFO);
                var email_id_primary = new SqlParameter("@email_id_primary", vendorViewModel.EMAIL_ID_PRIMARY == null ? "" : vendorViewModel.EMAIL_ID_PRIMARY);
                var telephone_primary = new SqlParameter("@telephone_primary", vendorViewModel.TELEPHONE_PRIMARY == null ? "" : vendorViewModel.TELEPHONE_PRIMARY);
                var fax = new SqlParameter("@fax", vendorViewModel.FAX == null ? string.Empty : vendorViewModel.FAX);               
                var payment_terms_id = new SqlParameter("@payment_terms_id", vendorViewModel.PAYMENT_TERMS_ID);
                var credit_limit = new SqlParameter("@credit_limit", vendorViewModel.CREDIT_LIMIT == null ? 0 : vendorViewModel.CREDIT_LIMIT);
                var credit_limit_currency_id = new SqlParameter("@credit_limit_currency_id", vendorViewModel.CREDIT_LIMIT_CURRENCY_ID);
                var is_blocked = new SqlParameter("@is_blocked", vendorViewModel.IS_BLOCKED);
                var payment_cycle_id = new SqlParameter("@payment_cycle_id", vendorViewModel.PAYMENT_CYCLE_ID);
                var tds_applicable = new SqlParameter("@tds_applicable", vendorViewModel.TDS_APPLICABLE);               
                var freight_terms_id = new SqlParameter("@freight_terms_id", vendorViewModel.FREIGHT_TERMS_ID);
                var website_address = new SqlParameter("@website_address", vendorViewModel.WEBSITE_ADDRESS == null ? "" : vendorViewModel.WEBSITE_ADDRESS);
                vendorViewModel.CREATED_ON = DateTime.Now;
                var created_on = new SqlParameter("@created_on", vendorViewModel.CREATED_ON);
                var created_by = new SqlParameter("@created_by", vendorViewModel.CREATED_BY == null ? 0 : vendorViewModel.CREATED_BY);
                var phone_code = new SqlParameter("@phone_code", vendorViewModel.phone_code == null ? "" : vendorViewModel.phone_code);
                var pan_no = new SqlParameter("@pan_no", vendorViewModel.pan_no==null?"":vendorViewModel.pan_no);
                var ecc_no = new SqlParameter("@ecc_no", vendorViewModel.ecc_no == null ? "" : vendorViewModel.ecc_no);
                var vat_tin_no = new SqlParameter("@vat_tin_no", vendorViewModel.vat_tin_no == null ? "" : vendorViewModel.vat_tin_no);
                var cst_tin_no = new SqlParameter("@cst_tin_no", vendorViewModel.cst_tin_no == null ? "" : vendorViewModel.cst_tin_no);
                var service_tax_no = new SqlParameter("@service_tax_no", vendorViewModel.service_tax_no == null ? "" : vendorViewModel.service_tax_no);
                var gst_no = new SqlParameter("@gst_no", vendorViewModel.gst_no == null ? "" : vendorViewModel.gst_no);
                var tds_id = new SqlParameter("@tds_no", vendorViewModel.tds_id == null ? 0 : vendorViewModel.tds_id);
                vendorViewModel.attachment = vendorViewModel.FileUpload != null ? _genericService.GetFilePath("VendorMaster", vendorViewModel.FileUpload) : "No File";
                var attachment = new SqlParameter("@attachment", vendorViewModel.attachment);
                var overall_discount = new SqlParameter("@overall_discount", vendorViewModel.overall_discount==null?0:vendorViewModel.overall_discount);

                var bank_id = new SqlParameter("@bank_id", vendorViewModel.bank_id==null ? 0 : vendorViewModel.bank_id);
                var bank_account_number = new SqlParameter("@bank_account_number", vendorViewModel.bank_account_number == null ? "" : vendorViewModel.bank_account_number);
                var ifsc_code = new SqlParameter("@ifsc_code", vendorViewModel.ifsc_code == null ? "" : vendorViewModel.ifsc_code);
                var gst_vendor_type_id = new SqlParameter("@gst_vendor_type_id", vendorViewModel.gst_vendor_type_id == null ? 0 : vendorViewModel.gst_vendor_type_id);
                var gst_registration_date = new SqlParameter("@gst_registration_date",vendorViewModel.gst_registration_date==null?dte:vendorViewModel.gst_registration_date);
                var gst_tds_applicable = new SqlParameter("@gst_tds_applicable", vendorViewModel.gst_tds_applicable);
                var gst_tds_id = new SqlParameter("@gst_tds_id", vendorViewModel.gst_tds_id == null ? 0 : vendorViewModel.gst_tds_id);
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_sub_ledger";
                t1.Value = t11;
                var t2 = new SqlParameter("@t2", SqlDbType.Structured);
                t2.TypeName = "dbo.temp_contact";
                t2.Value = t21;
                var t3 = new SqlParameter("@t3", SqlDbType.Structured);
                t3.TypeName = "dbo.temp_cus_ven_item_category";
                t3.Value = t31;
                var val = _scifferContext.Database.SqlQuery<string>(
                "exec save_vendor @entity,@vendor_id,@vendor_category_id,@has_parent,@vendor_parent_id,@vendor_code,@vendor_name,@vendor_display_name,@org_type_id,@priority_id,@billing_address,@billing_city,@billing_pincode,@billing_state_id,@corr_address,@corr_city,@corr_pincode,@corr_state_id,@additional_info,@email_id_primary,@telephone_primary,@fax,@payment_terms_id,@credit_limit,@credit_limit_currency_id,@is_blocked,@payment_cycle_id, @tds_applicable, @freight_terms_id,@website_address,@created_on,@created_by,@phone_code,@pan_no,@ecc_no,@vat_tin_no,@cst_tin_no,@service_tax_no,@gst_no,@tds_no,@attachment,@overall_discount,@modify_user,@bank_id,@bank_account_number,@ifsc_code,@gst_vendor_type_id,@gst_registration_date,@gst_tds_applicable,@gst_tds_id,@t1,@t2,@t3", entity, vendor_id,
                vendor_category_id, has_parent, vendor_parent_id, vendor_code, vendor_name, vendor_display_name, org_type_id, 
                priority_id, billing_address, billing_city, billing_pincode, billing_state_id, corr_address, corr_city, corr_pincode,
                corr_state_id, additional_info, email_id_primary,telephone_primary, fax, payment_terms_id, credit_limit,
                credit_limit_currency_id, is_blocked, payment_cycle_id,tds_applicable, freight_terms_id, website_address, 
                created_on, created_by, phone_code, pan_no, ecc_no, vat_tin_no, cst_tin_no, service_tax_no,gst_no, tds_id, 
                attachment, overall_discount, modify_user, @bank_id, @bank_account_number, @ifsc_code, gst_vendor_type_id, gst_registration_date, gst_tds_applicable,gst_tds_id, t1, t2, t3).FirstOrDefault();
                if (vendorViewModel.FileUpload != null)
                {
                    vendorViewModel.FileUpload.SaveAs(vendorViewModel.attachment);
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
            throw new NotImplementedException();
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


        public REF_VENDOR_VM Get(int id)
        {

            REF_VENDOR vendor = _scifferContext.REF_VENDOR.FirstOrDefault(c => c.VENDOR_ID == id);
            Mapper.CreateMap<REF_VENDOR, REF_VENDOR_VM>().ForMember(dest => dest.REF_PAYMENT_CYCLE_TYPE, opt => opt.Ignore()); 
            REF_VENDOR_VM vendorvm = Mapper.Map<REF_VENDOR, REF_VENDOR_VM>(vendor);
            if (id != 0)
            {
                vendorvm.REF_VENDOR_CONTACTS = vendorvm.REF_VENDOR_CONTACTS.Where(c => c.IS_ACTIVE == true).ToList();
            }
          
          
                                var billingcountry = _scifferContext.REF_STATE.Where(s => s.STATE_ID == vendorvm.BILLING_STATE_ID)
                                       .Select(s => new
                                       {
                                            COUNTRY_ID=s.COUNTRY_ID
                                        }).Single();
                                    vendorvm.BILLING_COUNTRY_ID = billingcountry.COUNTRY_ID;
                                 var corrcountry = _scifferContext.REF_STATE.Where(s => s.STATE_ID == vendorvm.CORR_STATE_ID)
                                   .Select(s => new
                                   {
                                       COUNTRY_ID = s.COUNTRY_ID
                                   }).Single();
                                vendorvm.CORR_COUNTRY_ID = corrcountry.COUNTRY_ID;
                                var paymentcyletype = _scifferContext.REF_PAYMENT_CYCLE.Where(P => P.PAYMENT_CYCLE_ID == vendorvm.PAYMENT_CYCLE_ID)
                                       .Select(P => new
                                       {
                                           PAYMENT_CYCLE_TYPE_ID = P.PAYMENT_CYCLE_TYPE_ID
                                       }).Single();
                                vendorvm.PAYMENT_CYCLE_TYPE_ID = paymentcyletype.PAYMENT_CYCLE_TYPE_ID;
                                return vendorvm;
        }

        public List<REF_VENDOR_VM> GetAll()
        {
            Mapper.CreateMap<REF_VENDOR, REF_VENDOR_VM>().ForMember(dest => dest.REF_PAYMENT_CYCLE_TYPE, opt => opt.Ignore());
            return _scifferContext.REF_VENDOR.Project().To<REF_VENDOR_VM>().ToList();

        } 
        List<VendorVM> IVendorService.GetVendorDetail()
        {
          
            var query = (from v in _scifferContext.REF_VENDOR
                         join vc in _scifferContext.REF_VENDOR_CATEGORY on v.VENDOR_CATEGORY_ID equals vc.VENDOR_CATEGORY_ID
                         join f in _scifferContext.REF_FREIGHT_TERMS on v.FREIGHT_TERMS_ID equals f.FREIGHT_TERMS_ID
                         join o in _scifferContext.REF_ORG_TYPE on v.ORG_TYPE_ID equals o.ORG_TYPE_ID
                         join p in _scifferContext.REF_PAYMENT_CYCLE on v.PAYMENT_CYCLE_ID equals p.PAYMENT_CYCLE_ID
                         join p1 in _scifferContext.REF_PAYMENT_CYCLE_TYPE on p.PAYMENT_CYCLE_TYPE_ID equals p1.PAYMENT_CYCLE_TYPE_ID
                         join p2 in _scifferContext.REF_PRIORITY on v.PRIORITY_ID equals p2.PRIORITY_ID into j1
                         from pr in j1.DefaultIfEmpty()
                         join vp in _scifferContext.REF_VENDOR_PARENT on v.VENDOR_PARENT_ID equals vp.VENDOR_PARENT_ID into j2
                         from vp1 in j2.DefaultIfEmpty()
                         join gv in _scifferContext.ref_gst_customer_type on v.gst_vendor_type_id equals gv.gst_customer_type_id into j5
                         from gvv in j5.DefaultIfEmpty()
                         join tds in _scifferContext.ref_tds_code on v.tds_id equals tds.tds_code_id into j4
                         from td in j4.DefaultIfEmpty()
                         join cur in _scifferContext.REF_CURRENCY on v.CREDIT_LIMIT_CURRENCY_ID equals cur.CURRENCY_ID
                         join p3 in _scifferContext.REF_PAYMENT_TERMS on v.PAYMENT_TERMS_ID equals p3.payment_terms_id
                         join s in _scifferContext.REF_STATE on v.BILLING_STATE_ID equals s.STATE_ID
                         join c in _scifferContext.REF_COUNTRY on s.COUNTRY_ID equals c.COUNTRY_ID
                         join s1 in _scifferContext.REF_STATE on v.CORR_STATE_ID equals s1.STATE_ID
                         join c1 in _scifferContext.REF_COUNTRY on s1.COUNTRY_ID equals c1.COUNTRY_ID
                         join bnk in _scifferContext.ref_bank on v.bank_id equals bnk.bank_id into j3
                         from bank in j3.DefaultIfEmpty()
                         join gt in _scifferContext.ref_gst_tds_code on v.gst_tds_id equals gt.gst_tds_code_id into j6
                         from gtt in j6.DefaultIfEmpty()
                         select new VendorVM
                         {
                             vendor_id = v.VENDOR_ID,
                             vendor_category_name = vc.VENDOR_CATEGORY_NAME,
                             vendor_code = v.VENDOR_CODE,
                             vendor_name = v.VENDOR_NAME,
                             vendor_display_name = v.VENDOR_DISPLAY_NAME,
                             org_type = o.ORG_TYPE_NAME,
                             billing_address = v.BILLING_ADDRESS,
                             billing_city = v.BILLING_CITY,
                             billing_pincode = v.BILLING_PINCODE,
                             billing_country = c.COUNTRY_NAME,
                             billing_state = s.STATE_NAME,
                             freight_terms = f.FREIGHT_TERMS_NAME,
                             priority = (pr == null ? string.Empty : pr.PRIORITY_NAME),
                             has_parent = v.HAS_PARENT,
                             vendor_parent_name = vp1 == null ? string.Empty : vp1.VENDOR_PARENT_NAME,
                             blocked = v.IS_BLOCKED,
                             bank_name = bank == null ? string.Empty : bank.bank_name,
                             bank_account_number = v.bank_account_number,
                             ifsc_code = v.ifsc_code,
                             corr_address = v.CORR_ADDRESS,
                             corr_city = v.CORR_CITY,
                             corr_country = c1.COUNTRY_NAME,
                             corr_pincode = v.CORR_PINCODE,
                             corr_state = s1.STATE_NAME,
                             telephone_code = v.phone_code,
                             telephone = v.TELEPHONE_PRIMARY,
                             fax = v.FAX,
                             website = v.WEBSITE_ADDRESS,
                             default_currency = cur.CURRENCY_NAME,
                             payment_terms = p3.payment_terms_code,
                             payment_cycle_type = p1.PAYMENT_CYCLE_TYPE_NAME,
                             payment_cycle = p.PAYMENT_CYCLE_NAME,
                             credit_limit = v.CREDIT_LIMIT,
                             tds_applicable = v.TDS_APPLICABLE,
                             tds_name = td == null ? string.Empty : td.tds_code,
                             tds_desc = td == null ? string.Empty : td.tds_code_description,
                             pan_no = v.pan_no,
                             gst_no = v.gst_no,
                             ecc_no = v.ecc_no,
                             vat_tin_no = v.vat_tin_no,
                             cst_tin_no = v.cst_tin_no,
                             service_tax_no = v.service_tax_no,
                             email = v.EMAIL_ID_PRIMARY,
                             overall_discount = v.overall_discount,
                             aditional_info=v.ADDITIONAL_INFO,
                             gst_registration_date=v.gst_registration_date,
                             gst_vendor_type_name=gvv==null?string.Empty:gvv.gst_customer_type_name,
                             gst_tds_code=gtt==null?string.Empty:gtt.gst_tds_code + "/" + gtt.gst_tds_code_description,
                             billing_country_id = s.COUNTRY_ID,
                             billing_state_id = v.BILLING_STATE_ID,
                             payment_terms_id = v.PAYMENT_TERMS_ID,
                             payment_cycle_id = v.PAYMENT_CYCLE_ID,
                             payment_cycle_type_id = p1.PAYMENT_CYCLE_TYPE_ID,
                             default_currency_id = cur.CURRENCY_ID,
                         }).OrderByDescending(a => a.vendor_id).ToList();
            return query;
        }

        public bool AddExcel(List<vendor_excel> vendor, List<vendor_contact_excel> contact, List<vendor_item_excel> item, List<vendor_gl_excel> gl)
        {
            try
            {
                foreach (var vendorViewModel in vendor)
                {
                    DataTable t31 = new DataTable();//for pro item
                    t31.Columns.Add("item_category_id", typeof(int));
                    t31.Columns.Add("rate", typeof(double));
                    if (item != null)
                    {
                        foreach (var items in item)
                        {
                            if (vendorViewModel.VENDOR_CODE == items.VENDOR_CODE)
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
                        if (vendorViewModel.VENDOR_CODE == contacts.VENDOR_CODE)
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
                            if (vendorViewModel.VENDOR_CODE == glCode.VENDOR_CODE)
                            {
                                t11.Rows.Add(1, glCode.gl_ledger_id, glCode.ledger_account_type_id);

                            }
                        }
                    }
                    int muser = 0;
                    var modify_user = new SqlParameter("@modify_user", muser);
                    var entity = new SqlParameter("@entity", "save");
                    var vendor_id = new SqlParameter("@vendor_id", vendorViewModel.VENDOR_ID);
                    var vendor_category_id = new SqlParameter("@vendor_category_id", vendorViewModel.VENDOR_CATEGORY_ID);
                    var has_parent = new SqlParameter("@has_parent", vendorViewModel.HAS_PARENT);
                    var vendor_parent_id = new SqlParameter("@vendor_parent_id", vendorViewModel.VENDOR_PARENT_ID == null ? 0 : vendorViewModel.VENDOR_PARENT_ID);
                    var vendor_code = new SqlParameter("@vendor_code", vendorViewModel.VENDOR_CODE);
                    var vendor_name = new SqlParameter("@vendor_name", vendorViewModel.VENDOR_NAME);
                    var vendor_display_name = new SqlParameter("@vendor_display_name", vendorViewModel.VENDOR_DISPLAY_NAME);
                    var org_type_id = new SqlParameter("@org_type_id", vendorViewModel.ORG_TYPE_ID);
                    var priority_id = new SqlParameter("@priority_id", vendorViewModel.PRIORITY_ID == null ? 0 : vendorViewModel.PRIORITY_ID);
                    var billing_address = new SqlParameter("@billing_address", vendorViewModel.BILLING_ADDRESS);
                    var billing_city = new SqlParameter("@billing_city", vendorViewModel.BILLING_CITY);
                    var billing_pincode = new SqlParameter("@billing_pincode", vendorViewModel.BILLING_PINCODE);
                    var billing_state_id = new SqlParameter("@billing_state_id", vendorViewModel.BILLING_STATE_ID);
                    var corr_address = new SqlParameter("@corr_address", vendorViewModel.CORR_ADDRESS);
                    var corr_city = new SqlParameter("@corr_city", vendorViewModel.CORR_CITY);
                    var corr_pincode = new SqlParameter("@corr_pincode", vendorViewModel.CORR_PINCODE);
                    var corr_state_id = new SqlParameter("@corr_state_id", vendorViewModel.CORR_STATE_ID);
                    var additional_info = new SqlParameter("@additional_info", vendorViewModel.ADDITIONAL_INFO == null ? "" : vendorViewModel.ADDITIONAL_INFO);
                    var email_id_primary = new SqlParameter("@email_id_primary", vendorViewModel.EMAIL_ID_PRIMARY == null ? "" : vendorViewModel.EMAIL_ID_PRIMARY);
                    var telephone_primary = new SqlParameter("@telephone_primary", vendorViewModel.TELEPHONE_PRIMARY == null ? "" : vendorViewModel.TELEPHONE_PRIMARY);
                    var fax = new SqlParameter("@fax", vendorViewModel.FAX == null ? string.Empty : vendorViewModel.FAX);
                    var payment_terms_id = new SqlParameter("@payment_terms_id", vendorViewModel.PAYMENT_TERMS_ID);
                    var credit_limit = new SqlParameter("@credit_limit", vendorViewModel.CREDIT_LIMIT == null ? 0 : vendorViewModel.CREDIT_LIMIT);
                    var credit_limit_currency_id = new SqlParameter("@credit_limit_currency_id", vendorViewModel.CREDIT_LIMIT_CURRENCY_ID);
                    var is_blocked = new SqlParameter("@is_blocked", vendorViewModel.IS_BLOCKED);
                    var payment_cycle_id = new SqlParameter("@payment_cycle_id", vendorViewModel.PAYMENT_CYCLE_ID);
                    var tds_applicable = new SqlParameter("@tds_applicable", vendorViewModel.TDS_APPLICABLE);
                    var freight_terms_id = new SqlParameter("@freight_terms_id", vendorViewModel.FREIGHT_TERMS_ID);
                    var website_address = new SqlParameter("@website_address", vendorViewModel.WEBSITE_ADDRESS == null ? "" : vendorViewModel.WEBSITE_ADDRESS);
                    vendorViewModel.CREATED_ON = DateTime.Now;
                    var created_on = new SqlParameter("@created_on", vendorViewModel.CREATED_ON);
                    var created_by = new SqlParameter("@created_by", vendorViewModel.CREATED_BY == null ? 0 : vendorViewModel.CREATED_BY);
                    var phone_code = new SqlParameter("@phone_code", vendorViewModel.phone_code == null ? "" : vendorViewModel.phone_code);
                    var pan_no = new SqlParameter("@pan_no", vendorViewModel.pan_no);
                    var ecc_no = new SqlParameter("@ecc_no", vendorViewModel.ecc_no == null ? "" : vendorViewModel.ecc_no);
                    var vat_tin_no = new SqlParameter("@vat_tin_no", vendorViewModel.vat_tin_no == null ? "" : vendorViewModel.vat_tin_no);
                    var cst_tin_no = new SqlParameter("@cst_tin_no", vendorViewModel.cst_tin_no == null ? "" : vendorViewModel.cst_tin_no);
                    var service_tax_no = new SqlParameter("@service_tax_no", vendorViewModel.service_tax_no == null ? "" : vendorViewModel.service_tax_no);
                    var gst_no = new SqlParameter("@gst_no", vendorViewModel.gst_no == null ? "" : vendorViewModel.gst_no);
                    var tds_id = new SqlParameter("@tds_no", vendorViewModel.tds_id == null ? 0 : vendorViewModel.tds_id);
                    vendorViewModel.attachment = vendorViewModel.FileUpload != null ? _genericService.GetFilePath("VendorMaster", vendorViewModel.FileUpload) : "No File";
                    var attachment = new SqlParameter("@attachment", vendorViewModel.attachment);
                    var overall_discount = new SqlParameter("@overall_discount", vendorViewModel.overall_discount == null ? 0 : vendorViewModel.overall_discount);

                    var bank_id = new SqlParameter("@bank_id", vendorViewModel.bank_id == null ? 0 : vendorViewModel.bank_id);
                    var bank_account_number = new SqlParameter("@bank_account_number", vendorViewModel.bank_account_number == null ? "" : vendorViewModel.bank_account_number);
                    var ifsc_code = new SqlParameter("@ifsc_code", vendorViewModel.ifsc_code == null ? "" : vendorViewModel.ifsc_code);

                    var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                    t1.TypeName = "dbo.temp_sub_ledger";
                    t1.Value = t11;
                    var t2 = new SqlParameter("@t2", SqlDbType.Structured);
                    t2.TypeName = "dbo.temp_contact";
                    t2.Value = t21;
                    var t3 = new SqlParameter("@t3", SqlDbType.Structured);
                    t3.TypeName = "dbo.temp_cus_ven_item_category";
                    t3.Value = t31;
                    var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_vendor @entity,@vendor_id,@vendor_category_id,@has_parent,@vendor_parent_id,@vendor_code,@vendor_name,@vendor_display_name,@org_type_id,@priority_id,@billing_address,@billing_city,@billing_pincode,@billing_state_id,@corr_address,@corr_city,@corr_pincode,@corr_state_id,@additional_info,@email_id_primary,@telephone_primary,@fax,@payment_terms_id,@credit_limit,@credit_limit_currency_id,@is_blocked,@payment_cycle_id, @tds_applicable, @freight_terms_id,@website_address,@created_on,@created_by,@phone_code,@pan_no,@ecc_no,@vat_tin_no,@cst_tin_no,@service_tax_no,@gst_no,@tds_no,@attachment,@overall_discount,@modify_user,@bank_id,@bank_account_number,@ifsc_code,@t1,@t2,@t3", entity, vendor_id,
                    vendor_category_id, has_parent, vendor_parent_id, vendor_code, vendor_name, vendor_display_name, org_type_id,
                    priority_id, billing_address, billing_city, billing_pincode, billing_state_id, corr_address, corr_city, corr_pincode,
                    corr_state_id, additional_info, email_id_primary, telephone_primary, fax, payment_terms_id, credit_limit,
                    credit_limit_currency_id, is_blocked, payment_cycle_id, tds_applicable, freight_terms_id, website_address,
                    created_on, created_by, phone_code, pan_no, ecc_no, vat_tin_no, cst_tin_no, service_tax_no, gst_no, tds_id,
                    attachment, overall_discount, modify_user, @bank_id, @bank_account_number, @ifsc_code, t1, t2, t3).FirstOrDefault();
                    if (vendorViewModel.FileUpload != null)
                    {
                        vendorViewModel.FileUpload.SaveAs(vendorViewModel.attachment);
                    }
                    if (val == "Saved")
                    {
                        // return true;
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
        public string GetContactPerson(int id)
        {
            string contact_person_name = "";
            var nme = _scifferContext.REF_VENDOR_CONTACTS.Where(x => x.VENDOR_ID == id && x.IS_ACTIVE==true).FirstOrDefault();
            contact_person_name = nme == null ? string.Empty : nme.CONTACT_NAME;
            return contact_person_name;
        }
    }
}
