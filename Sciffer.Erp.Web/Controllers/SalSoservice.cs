using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SalSoservice : ISalSoservice
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericService;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        public SalSoservice(ScifferContext scifferContext, IGenericService GenericService)
        {
            _scifferContext = scifferContext;
            _genericService = GenericService;
        }

        public string Add(sal_so_vm quotation)
        {
            try
            {
                if (quotation.FileUpload != null)
                {
                    quotation.attachment = _genericService.GetFilePath("SalesOrder", quotation.FileUpload);
                }
                else
                {
                    quotation.attachment = "No File";
                }
                DateTime dte = new DateTime(1990, 1, 1);
                DataTable dt = new DataTable();
                dt.Columns.Add("so_detail_id", typeof(int));
                dt.Columns.Add("sr_no", typeof(int));
                dt.Columns.Add("item_id", typeof(int));
                dt.Columns.Add("delivery_date", typeof(DateTime));
                dt.Columns.Add("quantity", typeof(double));
                dt.Columns.Add("uom_id", typeof(int));
                dt.Columns.Add("unit_price", typeof(double));
                dt.Columns.Add("discount", typeof(double));
                dt.Columns.Add("effective_unit_price", typeof(double));
                dt.Columns.Add("sales_value", typeof(double));
                dt.Columns.Add("assessable_rate", typeof(double));
                dt.Columns.Add("assessable_value", typeof(double));
                dt.Columns.Add("tax_id", typeof(int));
                dt.Columns.Add("storage_location_id", typeof(int));
                dt.Columns.Add("quotation_detail_id", typeof(int));
                dt.Columns.Add("drawing_no", typeof(string));
                dt.Columns.Add("material_cost_per_unit", typeof(double));
                dt.Columns.Add("sac_hsn_id", typeof(int));
                dt.Columns.Add("machine_charges", typeof(decimal));
                int k = 1;
                for(var i=0;i<quotation.item_id.Count;i++)
                {
                    if(quotation.item_id[i]!="")
                    {
                        dt.Rows.Add(quotation.so_detail_id[i] == "" ? 0 : int.Parse(quotation.so_detail_id[i]), k,
                            quotation.item_id[i] == "" ? 0 : int.Parse(quotation.item_id[i]),
                            quotation.delivery_date1[i] == "" ? dte : DateTime.Parse(quotation.delivery_date1[i]),
                            quotation.quantity[i] == "" ? 0 : double.Parse(quotation.quantity[i]),
                            quotation.uom_id[i] == "" ? 0 : int.Parse(quotation.uom_id[i]),
                            quotation.unit_price[i] == "" ? 0 : double.Parse(quotation.unit_price[i]),
                            quotation.discount[i] == "" ? 0 : double.Parse(quotation.discount[i]),
                            quotation.effective_unit_price[i] == "" ? 0 : double.Parse(quotation.effective_unit_price[i]),
                            quotation.sales_value[i] == "" ? 0 : double.Parse(quotation.sales_value[i]),
                            quotation.assessable_rate[i] == "" ? 0 : double.Parse(quotation.assessable_rate[i]),
                            quotation.assessable_value[i] == "" ? 0 : double.Parse(quotation.assessable_value[i]),
                            quotation.tax_id[i] == "" ? 0 : double.Parse(quotation.tax_id[i]),
                            quotation.sloc_id[i] == "" ? 0 : int.Parse(quotation.sloc_id[i]),
                            quotation.quotation_detail_id[i] == "" ? 0 : int.Parse(quotation.quotation_detail_id[i]),
                            quotation.drawing_no[i], quotation.material_cost_per_unit[i] == "" ? 0 : double.Parse(quotation.material_cost_per_unit[i]),
                            quotation.sac_hsn_id1[i] == "" ? 0 : int.Parse(quotation.sac_hsn_id1[i]),
                            quotation.machine_charges[i] == "" ? 0 : decimal.Parse(quotation.machine_charges[i]));
                            k = k + 1;
                    }
                }                
                var entity = new SqlParameter("@entity", "save");
                var so_id = new SqlParameter("@so_id", quotation.so_id);
                var sales_category_id = new SqlParameter("@sales_category_id", quotation.sales_category_id);
                var so_number = new SqlParameter("@so_number", quotation.so_number==null?string.Empty:quotation.so_number);
                var so_date = new SqlParameter("@so_date", quotation.so_date);
                var buyer_id = new SqlParameter("@buyer_id", quotation.buyer_id);
                var consignee_id = new SqlParameter("@consignee_id", quotation.consignee_id);
                var sal_net_value = new SqlParameter("@sal_net_value", quotation.sal_net_value);
                var net_value_currency_id = new SqlParameter("@net_value_currency_id", quotation.net_value_currency_id);
                var sal_gross_value = new SqlParameter("@sal_gross_value", quotation.sal_gross_value);
                var gross_value_currency_id = new SqlParameter("@gross_value_currency_id", quotation.gross_value_currency_id);
                var business_unit_id = new SqlParameter("@business_unit_id", quotation.business_unit_id);
                var plant_id = new SqlParameter("@plant_id", quotation.plant_id);
                var freight_terms_id = new SqlParameter("@freight_terms_id", quotation.freight_terms_id);
                var territory_id = new SqlParameter("@territory_id", quotation.territory_id==null?0:quotation.territory_id);
                var sales_rm_id = new SqlParameter("@sales_rm_id", quotation.sales_rm_id==null?0:quotation.sales_rm_id);
                var delivery_date = new SqlParameter("@delivery_date", quotation.delivery_date==null?dte:quotation.delivery_date);
                var customer_po_no = new SqlParameter("@customer_po_no", quotation.customer_po_no==null?string.Empty:quotation.customer_po_no);
                var customer_po_date = new SqlParameter("@customer_po_date", quotation.customer_po_date==null? dte:quotation.customer_po_date);
                var payment_terms_id = new SqlParameter("@payment_terms_id", quotation.payment_terms_id);
                var payment_cycle_id = new SqlParameter("@payment_cycle_id", quotation.payment_cycle_id);
                var avail_credit_limit = new SqlParameter("@avail_credit_limit", quotation.avail_credit_limit==null?0:quotation.avail_credit_limit);
                var credit_avail_after_order = new SqlParameter("@credit_avail_after_order", quotation.credit_avail_after_order==null?0:quotation.credit_avail_after_order);
                var internal_remarks = new SqlParameter("@internal_remarks", quotation.internal_remarks==null?string.Empty:quotation.internal_remarks);
                var remarks = new SqlParameter("@remarks", quotation.remarks==null?string.Empty:quotation.remarks);
                var billing_address = new SqlParameter("@billing_address", quotation.billing_address);
                var billing_city = new SqlParameter("@billing_city", quotation.billing_city);
                var billing_pincode = new SqlParameter("@billing_pincode", quotation.billing_pincode);
                var billing_state_id = new SqlParameter("@billing_state_id", quotation.billing_state_id);
                var billing_email_id = new SqlParameter("@billing_email_id", quotation.billing_email_id==null?string.Empty:quotation.billing_email_id);
                var shipping_address = new SqlParameter("@shipping_address", quotation.shipping_address);
                var shipping_city = new SqlParameter("@shipping_city", quotation.shipping_city);
                var shipping_pincode = new SqlParameter("@shipping_pincode", quotation.shipping_pincode);
                var shipping_state_id = new SqlParameter("@shipping_state_id", quotation.shipping_state_id);              
                var pan_no = new SqlParameter("@pan_no", quotation.pan_no==null?string.Empty:quotation.pan_no);
                var ecc_no = new SqlParameter("@ecc_no", quotation.ecc_no==null?string.Empty:quotation.ecc_no);
                var vat_tin_no = new SqlParameter("@vat_tin_no", quotation.vat_tin_no==null?string.Empty:quotation.vat_tin_no);
                var cst_tin_no = new SqlParameter("@cst_tin_no", quotation.cst_tin_no==null?string.Empty:quotation.cst_tin_no);
                var service_tax_no = new SqlParameter("@service_tax_no", quotation.service_tax_no==null?string.Empty:quotation.service_tax_no);
                var gst_no = new SqlParameter("@gst_no", quotation.gst_no==null?string.Empty:quotation.gst_no);
                var attachment = new SqlParameter("@attachment", quotation.attachment);
                var commisionerate = new SqlParameter("@commisionerate", quotation.commisionerate == null ? "" : quotation.commisionerate);
                var range = new SqlParameter("@range", quotation.range == null ? "" : quotation.range);
                var division = new SqlParameter("@division", quotation.division == null ? "" : quotation.division);
                var quotation_id = new SqlParameter("@quotation_id", quotation.quotation_id==null?0:quotation.quotation_id);             
                var doc_currency_id = new SqlParameter("@doc_currency_id", quotation.doc_currency_id);
                var form_id = new SqlParameter("@form_id", quotation.form_id==null?0:quotation.form_id);
                var deleteids = new SqlParameter("@deleteids", quotation.deleteids==null?string.Empty:quotation.deleteids);
                var supply_state_id = new SqlParameter("@supply_state_id", quotation.supply_state_id == null ? 0 : quotation.supply_state_id);
                var delivery_state_id = new SqlParameter("@delivery_state_id", quotation.delivery_state_id == null ? 0 : quotation.delivery_state_id);
                var shipping_email_id = new SqlParameter("@shipping_email_id", quotation.shipping_email_id == null ? string.Empty : quotation.shipping_email_id);
                var shipping_pan_no = new SqlParameter("@shipping_pan_no",quotation.shipping_pan_no==null?string.Empty:quotation.shipping_pan_no);
                var shipping_gst_no = new SqlParameter("@shipping_gst_no",quotation.shipping_gst_no==null?string.Empty:quotation.shipping_gst_no);
                var tds_code_id = new SqlParameter("@tds_code_id", quotation.tds_code_id == null ? 0 : quotation.tds_code_id);
                var gst_tds_code_id = new SqlParameter("@gst_tds_code_id", quotation.gst_tds_code_id == null ? 0 : quotation.gst_tds_code_id);
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_sale_so";
                t1.Value = dt;
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_sales_order @entity,@so_id ,@sales_category_id,@so_number ,@so_date ,@buyer_id ,@consignee_id ,@sal_net_value ,"+ 
                    "@net_value_currency_id ,@sal_gross_value ,@gross_value_currency_id ,@business_unit_id ,@plant_id ,@freight_terms_id ,@territory_id ,"+
                    "@sales_rm_id ,@delivery_date ,@customer_po_no ,@customer_po_date ,@payment_terms_id ,@payment_cycle_id ,@avail_credit_limit ," +
                    "@credit_avail_after_order ,@internal_remarks, @remarks ,@billing_address ,@billing_city,@billing_pincode ,@billing_state_id ,"+
                    "@billing_email_id ,@shipping_address,@shipping_city ,@shipping_pincode,@shipping_state_id ,@pan_no ,@ecc_no ,@vat_tin_no ,"+
                    "@cst_tin_no ,@service_tax_no ,@gst_no,@attachment,@commisionerate,@range,@division,@quotation_id,@doc_currency_id,@form_id,@deleteids,@supply_state_id,@delivery_state_id,@shipping_email_id,@shipping_pan_no,@shipping_gst_no,@tds_code_id,@gst_tds_code_id,@t1 ",
                    entity, so_id, sales_category_id, so_number, so_date, buyer_id, consignee_id, sal_net_value, net_value_currency_id,
                    sal_gross_value, gross_value_currency_id, business_unit_id, plant_id, freight_terms_id, territory_id, sales_rm_id, delivery_date, customer_po_no,
                    customer_po_date, payment_terms_id, payment_cycle_id, avail_credit_limit, credit_avail_after_order, internal_remarks, remarks, billing_address,
                    billing_city, billing_pincode, billing_state_id, billing_email_id, shipping_address, shipping_city, shipping_pincode, shipping_state_id,
                    pan_no, ecc_no, vat_tin_no, cst_tin_no, service_tax_no, gst_no, attachment, commisionerate, range, division, quotation_id, doc_currency_id, form_id, deleteids, supply_state_id, delivery_state_id, shipping_email_id, shipping_pan_no, shipping_gst_no, tds_code_id, gst_tds_code_id, t1).FirstOrDefault();

                if (val.Contains("Saved"))
                {
                    var str = val.Split('~');
                    var mrn_no = str[1];
                    if (quotation.FileUpload != null)
                    {
                        quotation.FileUpload.SaveAs(quotation.attachment);
                    }
                    return mrn_no;
                }
                else
                {
                    System.IO.File.Delete(quotation.attachment);
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
            //return true;
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

        public sal_so_vm Get(int id)
        {
            sal_so so = _scifferContext.SAL_SO.FirstOrDefault(c => c.so_id == id);
            Mapper.CreateMap<sal_so, sal_so_vm>().ForMember(dest => dest.REF_PAYMENT_CYCLE_TYPE, opt => opt.Ignore());
            sal_so_vm sovm = Mapper.Map<sal_so, sal_so_vm>(so);
            sovm.buyer_code = so.REF_CUSTOMER.CUSTOMER_CODE;
            sovm.buyer_name = so.REF_CUSTOMER.CUSTOMER_NAME;
            if (sovm.consignee_id != 0)
            {
                if (sovm.consignee_id != null)
                {
                    sovm.consignee_code = so.REF_CUSTOMER1.CUSTOMER_CODE;
                    sovm.consignee_name = so.REF_CUSTOMER1.CUSTOMER_NAME;
                }
            }
            sovm.sal_so_detail = sovm.sal_so_detail.Where(c => c.is_active == true).ToList();
            List<sal_so_detail_report_vm> sm = new List<sal_so_detail_report_vm>();
            foreach(var i in sovm.sal_so_detail)
            {
                sal_so_detail_report_vm s = new sal_so_detail_report_vm();
                s.assessable_rate = i.assessable_rate;
                s.assessable_value = i.assessable_value;
                s.delivery_date = i.delivery_date;
                s.discount = i.discount;
                s.drawing_no = i.drawing_no;
                s.effective_unit_price = i.effective_unit_price;
                s.item_code = i.REF_ITEM.ITEM_CODE;
                s.item_id = i.item_id;
                s.item_name = i.REF_ITEM.ITEM_NAME;
                s.material_cost_per_unit = i.material_cost_per_unit;
                s.quantity = i.quantity;
                s.sales_value = i.sales_value;
                s.so_detail_id =(int) i.so_detail_id;
                s.sr_no = i.sr_no;
                s.storage_location_id = i.storage_location_id;
                s.storage_location_name = i.REF_STORAGE_LOCATION.storage_location_name + "/" + i.REF_STORAGE_LOCATION.description;
                s.tax_id = i.tax_id;
                s.tax_code = i.ref_tax.tax_code + "/" + i.ref_tax.tax_name;
                s.unit_price = i.unit_price;
                s.uom_id = i.uom_id;
                s.uom_name = i.REF_UOM.UOM_NAME;
                var sac= _genericService.GetUserDescriptionForItem(i.item_id);
                s.sac_hsn_id = sac.sac_id;
                s.sac_hsn_name = sac.sac_name;
                s.quotation_detail_id = i.quotation_detail_id;
                s.machine_charges = i.machine_charges==null ? 0 :i.machine_charges;
                sm.Add(s);
            }
            sovm.sal_so_detail_report_vm = sm;
            var billingcountry = _scifferContext.REF_STATE.Where(s => s.STATE_ID == sovm.billing_state_id)
                                      .Select(s => new
                                      {
                                          COUNTRY_ID = s.COUNTRY_ID
                                      }).Single();
            if (sovm.shipping_state_id != null)
            {
                if (sovm.shipping_state_id != 0)
                {
                    var country = _scifferContext.REF_STATE.Where(s => s.STATE_ID == sovm.shipping_state_id)
                                  .Select(s => new
                                  {
                                      COUNTRY_ID = s.COUNTRY_ID
                                  }).Single();
                    sovm.shipping_country_id = country.COUNTRY_ID;
                }
            }
            var paymentcyletype = _scifferContext.REF_PAYMENT_CYCLE.Where(P => P.PAYMENT_CYCLE_ID == sovm.payment_cycle_id)
                                     .Select(P => new
                                     {
                                         PAYMENT_CYCLE_TYPE_ID = P.PAYMENT_CYCLE_TYPE_ID
                                     }).Single();
            sovm.payment_cycle_type_id = paymentcyletype.PAYMENT_CYCLE_TYPE_ID;
            sovm.billing_country_id = billingcountry.COUNTRY_ID;

            sovm.buyer_id1 = sovm.buyer_id.ToString();
            sovm.consignee_id1 = sovm.consignee_id.ToString();
            return sovm;
        }

        public List<sal_so_vm> GetAll()
        {
            Mapper.CreateMap<sal_so, sal_so_vm>().ForMember(dest => dest.REF_PAYMENT_CYCLE_TYPE, opt => opt.Ignore());
            return _scifferContext.SAL_SO.Project().To<sal_so_vm>().ToList();
        }

        public List<sal_so_vm> getall()
        {
            var query = (from sal in _scifferContext.SAL_SO.Where(a => a.is_active == true)
                         join quotation in _scifferContext.SAL_QUOTATION on sal.quotation_id equals quotation.QUOTATION_ID into quotation1
                         from quotation2 in quotation1.DefaultIfEmpty()
                         join c in _scifferContext.ref_document_numbring on sal.sales_category_id equals c.document_numbring_id
                         join Buyer in _scifferContext.REF_CUSTOMER on sal.buyer_id equals Buyer.CUSTOMER_ID
                         join CONSIGNEE in _scifferContext.REF_CUSTOMER on sal.consignee_id equals CONSIGNEE.CUSTOMER_ID
                         join net_currency in _scifferContext.REF_CURRENCY on sal.net_value_currency_id equals net_currency.CURRENCY_ID
                         join gross_currency in _scifferContext.REF_CURRENCY on sal.gross_value_currency_id equals gross_currency.CURRENCY_ID
                         join business_unit in _scifferContext.REF_BUSINESS_UNIT on sal.business_unit_id equals business_unit.BUSINESS_UNIT_ID
                         join Plant in _scifferContext.REF_PLANT on sal.plant_id equals Plant.PLANT_ID
                         join Territory in _scifferContext.REF_TERRITORY on sal.territory_id equals Territory.TERRITORY_ID into Territory1
                         from Territory2 in Territory1.DefaultIfEmpty()
                         join user in _scifferContext.REF_EMPLOYEE on sal.sales_rm_id equals user.employee_id into user1
                         from user2 in user1.DefaultIfEmpty()
                         join payment_terms in _scifferContext.REF_PAYMENT_TERMS on sal.payment_terms_id equals payment_terms.payment_terms_id
                         join payment_cycle in _scifferContext.REF_PAYMENT_CYCLE on sal.payment_cycle_id equals payment_cycle.PAYMENT_CYCLE_ID
                         join state in _scifferContext.REF_STATE on sal.billing_state_id equals state.STATE_ID into state1
                         from state2 in state1.DefaultIfEmpty()
                         join shipping_state in _scifferContext.REF_STATE on sal.shipping_state_id equals shipping_state.STATE_ID into shipping_state1
                         from shipping_state2 in shipping_state1.DefaultIfEmpty()
                         join F in _scifferContext.REF_FREIGHT_TERMS on sal.freight_terms_id equals F.FREIGHT_TERMS_ID
                         join currency in _scifferContext.REF_CURRENCY on sal.doc_currency_id equals currency.CURRENCY_ID
                         join form in _scifferContext.SAL_SO_FORM on sal.so_id equals form.so_id into form1
                         from form2 in form1.DefaultIfEmpty()
                         join ss in _scifferContext.REF_STATE on sal.supply_state_id equals ss.STATE_ID into j1
                         from sss in j1.DefaultIfEmpty()
                         join ds in _scifferContext.REF_STATE on sal.delivery_state_id equals ds.STATE_ID into j2
                         from dss in j2.DefaultIfEmpty()
                         join td in _scifferContext.ref_tds_code on sal.tds_code_id equals td.tds_code_id into j3
                         from tds in j3.DefaultIfEmpty()
                         join gtd in _scifferContext.ref_gst_tds_code on sal.gst_tds_code_id equals gtd.gst_tds_code_id into j4
                         from gtds in j4.DefaultIfEmpty()
                         join r2 in _scifferContext.ref_user_management on sal.closed_by equals r2.user_id into r3
                         from r4 in r3.DefaultIfEmpty()
                         select new sal_so_vm()
                         {
                             tds_code = tds == null ? string.Empty : tds.tds_code + "/" + tds.tds_code_description,
                             gst_tds_code=gtds==null?string.Empty:gtds.gst_tds_code + "/" + gtds.gst_tds_code_description,
                             so_id = sal.so_id,
                             quotation_number = quotation2.QUOTATION_NUMBER,
                             sales_category_name = c.category,
                             so_number = sal.so_number,
                             so_date = sal.so_date,
                             buyer_name = Buyer.CUSTOMER_NAME,
                             buyer_code = Buyer.CUSTOMER_CODE,
                             consignee_name = CONSIGNEE.CUSTOMER_NAME,
                             consignee_code = CONSIGNEE.CUSTOMER_CODE,
                             sal_net_value = sal.sal_net_value,
                             net_value_currency_name = net_currency.CURRENCY_NAME,
                             sal_gross_value = sal.sal_gross_value,
                             gross_value_currency_name = gross_currency.CURRENCY_NAME,
                             business_unit_name = business_unit.BUSINESS_UNIT_NAME,
                             plant_name = Plant.PLANT_NAME,
                             customer_po_no = sal.customer_po_no,
                             customer_po_date = sal.customer_po_date,
                             freight_terms_name = F.FREIGHT_TERMS_NAME,
                             territory_name = Territory2 == null ? string.Empty : Territory2.TERRITORY_NAME,
                             sales_rm_name = user2.employee_name,
                             sales_rm_code = user2.employee_code,
                             delivery_date = sal.delivery_date,
                             payment_terms_name = payment_terms.payment_terms_code,
                             payment_terms_desc = payment_terms.payment_terms_description,
                             payment_cycle_type_name = payment_cycle.REF_PAYMENT_CYCLE_TYPE.PAYMENT_CYCLE_TYPE_NAME,
                             payment_cycle_name = payment_cycle.PAYMENT_CYCLE_NAME,
                             internal_remarks = sal.internal_remarks,
                             remarks = sal.remarks,
                             billing_address = sal.billing_address,
                             billing_city = sal.billing_city,
                             billing_pincode = sal.billing_pincode,
                             billing_state_name = state2.STATE_NAME,
                             billing_country_name = state2.REF_COUNTRY.COUNTRY_NAME,
                             billing_email_id = sal.billing_email_id,
                             shipping_address = sal.shipping_address,
                             shipping_city = sal.shipping_city,
                             shipping_pincode = sal.shipping_pincode,
                             shipping_state_name = shipping_state2.STATE_NAME,
                             shipping_country_name = shipping_state2.REF_COUNTRY.COUNTRY_NAME,
                             avail_credit_limit = sal.avail_credit_limit,
                             credit_avail_after_order = sal.credit_avail_after_order,
                             pan_no = sal.pan_no,
                             ecc_no = sal.ecc_no,
                             vat_tin_no = sal.vat_tin_no,
                             cst_tin_no = sal.cst_tin_no,
                             service_tax_no = sal.service_tax_no,
                             gst_no = sal.gst_no,
                             attachment = sal.attachment,
                             commisionerate = sal.commisionerate,
                             range = sal.range,
                             division = sal.division,
                             business_desc = business_unit.BUSINESS_UNIT_DESCRIPTION,
                             plant_code = Plant.PLANT_CODE,
                             currency_name = currency.CURRENCY_NAME,
                             form_name = form2.REF_FORM.FORM_NAME,
                             delivery_state = dss == null ? string.Empty : dss.STATE_NAME,
                             supply_state = sss == null ? string.Empty : sss.STATE_NAME,
                             shipping_email_id = sal.shipping_email_id,
                             shipping_gst_no = sal.shipping_gst_no,
                             shipping_pan_no=sal.shipping_pan_no,
                             closed_remarks= sal.closed_remarks,
                             closed_user= r4.user_name,
                         }).OrderByDescending(a => a.so_id).ToList();
            return query;
        }


        public List<sal_so_detail_report_vm> GetSOProductDetail(int code)
        {
            var quotation_id = new SqlParameter("@id", code);
            var entity = new SqlParameter("@entity", "getsodetailforsi");
            var val = _scifferContext.Database.SqlQuery<sal_so_detail_report_vm>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;
        }
        public sal_so_vm GetSOForSI(int id)
        {
            var quotationvm = (from sq in _scifferContext.SAL_SO.Where(x => x.so_id == id)
                               join buyer in _scifferContext.REF_CUSTOMER on sq.buyer_id equals buyer.CUSTOMER_ID
                               join consignee in _scifferContext.REF_CUSTOMER on sq.consignee_id equals consignee.CUSTOMER_ID
                               join s1 in _scifferContext.REF_STATE on sq.billing_state_id equals s1.STATE_ID
                               join s2 in _scifferContext.REF_STATE on sq.shipping_state_id equals s2.STATE_ID
                               join p in _scifferContext.REF_PAYMENT_CYCLE on sq.payment_cycle_id equals p.PAYMENT_CYCLE_ID
                               join f in _scifferContext.SAL_SO_FORM on sq.so_id equals f.so_id into jo
                               from form in jo.DefaultIfEmpty()
                               select new sal_so_vm()
                               {
                                   buyer_id = buyer.CUSTOMER_ID,
                                   buyer_name = buyer.CUSTOMER_NAME,
                                   buyer_code = buyer.CUSTOMER_CODE,
                                   consignee_id = consignee.CUSTOMER_ID,
                                   consignee_code = consignee.CUSTOMER_CODE,
                                   consignee_name = consignee.CUSTOMER_NAME,
                                   business_unit_id = sq.business_unit_id,
                                   freight_terms_id = sq.freight_terms_id,
                                   plant_id = sq.plant_id,
                                   sales_rm_id = sq.sales_rm_id,
                                   territory_id = sq.territory_id,
                                   billing_address = sq.billing_address,
                                   billing_city = sq.billing_city,
                                   billing_country_id = s1.COUNTRY_ID,
                                   billing_email_id = sq.billing_email_id,
                                   billing_pincode = sq.billing_pincode,
                                   billing_state_id = sq.billing_state_id,
                                   shipping_address = sq.shipping_address,
                                   shipping_city = sq.shipping_city,
                                   shipping_country_id = s2.COUNTRY_ID,
                                   shipping_pincode = sq.shipping_pincode,
                                   shipping_state_id = sq.shipping_state_id,
                                   gross_value_currency_id = sq.gross_value_currency_id,
                                   net_value_currency_id = sq.net_value_currency_id,
                                   payment_cycle_id = sq.payment_cycle_id,
                                   payment_cycle_type_id = p.PAYMENT_CYCLE_TYPE_ID,
                                   payment_terms_id = sq.payment_terms_id,
                                   pan_no = sq.pan_no,
                                   cst_tin_no = sq.cst_tin_no,
                                   vat_tin_no = sq.vat_tin_no,
                                   gst_no = sq.gst_no,
                                   service_tax_no = sq.service_tax_no,
                                   ecc_no = sq.ecc_no,
                                   form_id = form == null ? 0 : form.form_id,
                                   doc_currency_id = sq.doc_currency_id,
                                   tds_code_id = (int)buyer.tds_id,
                                   customer_po_date = sq.customer_po_date,
                                   customer_po_no = sq.customer_po_no,
                                   commisionerate = sq.commisionerate,
                                   range = sq.range,
                                   division = sq.division,
                                   delivery_state_id = sq.delivery_state_id,
                                   supply_state_id = sq.supply_state_id,
                                   shipping_email_id = sq.shipping_email_id,
                                   shipping_gst_no = sq.shipping_gst_no,
                                   shipping_pan_no=sq.shipping_pan_no,   
                                   avail_credit_limit=sq.avail_credit_limit,
                                   credit_avail_after_order=sq.credit_avail_after_order,
                                   gst_tds_code_id=sq.gst_tds_code_id, 
                               }).FirstOrDefault();
            return quotationvm;
        }
        public List<sal_so_vm> GetSOForSI()
        {
            var quotationvm = (from sq in _scifferContext.SAL_SO
                               join buyer in _scifferContext.REF_CUSTOMER on sq.buyer_id equals buyer.CUSTOMER_ID
                               join consignee in _scifferContext.REF_CUSTOMER on sq.consignee_id equals consignee.CUSTOMER_ID
                               join s1 in _scifferContext.REF_STATE on sq.billing_state_id equals s1.STATE_ID
                               join s2 in _scifferContext.REF_STATE on sq.shipping_state_id equals s2.STATE_ID
                               join p in _scifferContext.REF_PAYMENT_CYCLE on sq.payment_cycle_id equals p.PAYMENT_CYCLE_ID
                               join f in _scifferContext.SAL_SO_FORM on sq.so_id equals f.so_id into jo
                               from form in jo.DefaultIfEmpty()
                               select new sal_so_vm()
                               {
                                   buyer_id = buyer.CUSTOMER_ID,
                                   buyer_name = buyer.CUSTOMER_NAME,
                                   buyer_code = buyer.CUSTOMER_CODE,
                                   consignee_id = consignee.CUSTOMER_ID,
                                   consignee_code = consignee.CUSTOMER_CODE,
                                   consignee_name = consignee.CUSTOMER_NAME,
                                   business_unit_id = sq.business_unit_id,
                                   freight_terms_id = sq.freight_terms_id,
                                   plant_id = sq.plant_id,
                                   sales_rm_id = sq.sales_rm_id,
                                   territory_id = sq.territory_id,
                                   billing_address = sq.billing_address,
                                   billing_city = sq.billing_city,
                                   billing_country_id = s1.COUNTRY_ID,
                                   billing_email_id = sq.billing_email_id,
                                   billing_pincode = sq.billing_pincode,
                                   billing_state_id = sq.billing_state_id,
                                   shipping_address = sq.shipping_address,
                                   shipping_city = sq.shipping_city,
                                   shipping_country_id = s2.COUNTRY_ID,
                                   shipping_pincode = sq.shipping_pincode,
                                   shipping_state_id = sq.shipping_state_id,
                                   gross_value_currency_id = sq.gross_value_currency_id,
                                   net_value_currency_id = sq.net_value_currency_id,
                                   payment_cycle_id = sq.payment_cycle_id,
                                   payment_cycle_type_id = p.PAYMENT_CYCLE_TYPE_ID,
                                   payment_terms_id = sq.payment_terms_id,
                                   pan_no = sq.pan_no,
                                   cst_tin_no = sq.cst_tin_no,
                                   vat_tin_no = sq.vat_tin_no,
                                   gst_no = sq.gst_no,
                                   service_tax_no = sq.service_tax_no,
                                   ecc_no = sq.ecc_no,
                                   form_id = form == null ? 0 : form.form_id,
                                   doc_currency_id = sq.doc_currency_id,
                                   tds_code_id = (int)buyer.tds_id,
                                   commisionerate = sq.commisionerate,
                                   range = sq.range,
                                   division = sq.division
                               }).ToList();
            return quotationvm;
        }
        public sal_so_report_vm GetSoReport(int id)
        {
            var so = (from s in _scifferContext.SAL_SO.Where(x => x.so_id == id)
                      from c in _scifferContext.REF_COMPANY
                      join p in _scifferContext.REF_PLANT on s.plant_id equals p.PLANT_ID
                      join S1 in _scifferContext.REF_STATE on s.billing_state_id equals S1.STATE_ID
                      join C1 in _scifferContext.REF_COUNTRY on S1.COUNTRY_ID equals C1.COUNTRY_ID
                      join S2 in _scifferContext.REF_STATE on s.shipping_state_id equals S2.STATE_ID
                      join C2 in _scifferContext.REF_COUNTRY on S2.COUNTRY_ID equals C2.COUNTRY_ID
                      join b in _scifferContext.REF_CUSTOMER on s.buyer_id equals b.CUSTOMER_ID
                      join S3 in _scifferContext.REF_STATE on c.REGISTERED_STATE_ID equals S3.STATE_ID
                      join C3 in _scifferContext.REF_COUNTRY on S3.COUNTRY_ID equals C3.COUNTRY_ID
                      join n in _scifferContext.REF_CUSTOMER on s.consignee_id equals n.CUSTOMER_ID
                      select new sal_so_report_vm
                      {
                          buyer_address = s.billing_address,
                          buyer_city = s.billing_city,
                          buyer_state = S1.STATE_NAME,
                          buyer_country = C1.COUNTRY_NAME,
                          buyer_cst_no = s.cst_tin_no,
                          buyer_ecc_no = s.ecc_no,
                          buyer_name = b.CUSTOMER_NAME,
                          buyer_pan_no = s.customer_po_no,
                          buyer_pin_code = s.billing_pincode,
                          company_address = c.REGISTERED_ADDRESS,
                          company_email = c.REGISTERED_EMAIL,
                          compnay_name = c.COMPANY_NAME,
                          plant_name = p.PLANT_CODE,
                          buyer_vat_no = s.vat_tin_no,
                          company_cin_no = c.CIN_NO,
                          company_country = C3.COUNTRY_NAME,
                          company_city=c.REGISTERED_CITY,
                          company_pan_no = c.PAN_NO,
                          company_ecc_no = c.TAN_NO,
                          company_pincode=c.registered_pincode,
                          company_display_name = c.COMPANY_NAME,
                          company_state = S3.STATE_NAME,
                          company_telephone = c.REGISTERED_TELEPHONE.ToString(),
                          company_website = c.WEBSITE,
                          consignee_address = s.shipping_address,
                          consignee_city = s.shipping_city,
                          consignee_country = C2.COUNTRY_NAME,
                          consignee_state = S3.STATE_NAME,
                          so_number = s.so_number,
                          so_date = (DateTime)s.so_date,
                          so_id = s.so_id,
                          consignee_cst_no = n.cst_tin_no,
                          consignee_ecc_no = n.ecc_no,
                          consignee_name = n.CUSTOMER_NAME,
                          consignee_vat_no = n.vat_tin_no,
                          consignee_pan_no = n.pan_no,
                          consignee_pin_code = n.BILLING_PINCODE.ToString(),
                          plant_commissionarate = p.excise_commisionerate,
                          plant_division = p.excise_division,
                          plant_range = p.excise_range, 
                          company_logo=c.LOGO,
                          sal_gross_value=s.sal_gross_value,
                       
                      }).FirstOrDefault();
            return so;
        }

        public List<sal_so_detail_report_vm> GetSOProductList(int id)
        {
            var query = (from s in _scifferContext.SAL_SO_DETAIL.Where(x => x.so_id == id && x.is_active == true)
                         join p in _scifferContext.REF_ITEM on s.item_id equals p.ITEM_ID
                         join u in _scifferContext.REF_UOM on s.uom_id equals u.UOM_ID

                         select new sal_so_detail_report_vm
                         {
                             assessable_rate = s.assessable_rate,
                             assessable_value = s.assessable_value,
                             delivery_date = s.delivery_date,
                             discount = s.discount,
                             effective_unit_price = s.effective_unit_price,
                             item_code = p.ITEM_CODE,
                             item_id=p.ITEM_ID,
                             item_name = p.ITEM_NAME,
                             quantity = s.quantity,
                             so_detail_id = (int)s.so_detail_id,
                             so_id = s.so_id,
                             sales_value = s.sales_value,
                             uom_name = u.UOM_NAME,
                             uom_id=u.UOM_ID,
                             unit_price = s.unit_price, 
                             tax_id=s.tax_id,
                         }).ToList();
            return query; ;
        }

        public int GetSOId(string num)
        {
                return _scifferContext.SAL_SO.Where(a => a.so_number == num).FirstOrDefault().so_id;
        }

        public List<sal_so_vm> GetSOForSalesInvoice(int id)
        {
            var query = (from sq in _scifferContext.SAL_SO.Where(x => x.order_status == false && x.is_active == true && x.buyer_id == id)
                         join sod in _scifferContext.SAL_SO_DETAIL on sq.so_id equals sod.so_id
                         join i in _scifferContext.REF_ITEM on sod.item_id equals i.ITEM_ID
                         join c in _scifferContext.REF_CUSTOMER on sq.buyer_id equals c.CUSTOMER_ID
                         select new
                         {
                             so_id = sq.so_id,
                             so_date = sq.so_date,
                             so_number = i.ITEM_NAME + " - " + sq.so_number,
                             buyer_name = c.CUSTOMER_NAME,
                             buyer_code = c.CUSTOMER_CODE,
                             sal_net_value = sq.sal_net_value,
                             sal_gross_value = sq.sal_gross_value,
                             plant_id = sq.plant_id,
                             buyer_id = sq.buyer_id,
                             consignee_id = sq.consignee_id,
                             business_unit_id = sq.business_unit_id,
                             freight_terms_id = sq.freight_terms_id,
                         }).ToList().Select(a => new sal_so_vm
                         {
                            
                             sodate = DateTime.Parse(a.so_date.ToString()).ToString("dd/MMM/yyyy"),
                             so_number = a.so_number + " - " + DateTime.Parse(a.so_date.ToString()).ToString("dd/MMM/yyyy"),
                             buyer_name = a.buyer_name,
                             sal_net_value = a.sal_net_value,
                             so_id = a.so_id,
                             plant_id = a.plant_id,
                             buyer_id=a.buyer_id,
                             consignee_id = a.consignee_id,
                             business_unit_id = a.business_unit_id,
                             freight_terms_id= a.freight_terms_id,

                         }).OrderByDescending(a=>a.sodate).ToList();
            return query;
        }
        public string Close(int? id, string closed_remarks)
        {
            try
            {
                int createdby = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var st = "update [dbo].[SAL_SO] set [order_status] = 1 , [closed_remarks] = '" + closed_remarks + "', [closed_by] = " + createdby + ", [closed_ts] = '" + DateTime.Now + "' where so_id = " + id;

                _scifferContext.Database.ExecuteSqlCommand(st);
                _scifferContext.Database.ExecuteSqlCommand("update [dbo].[SAL_SO_DETAIL] set [order_status] = 1 where so_id = " + id);
                return "Closed";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }
    }
}
