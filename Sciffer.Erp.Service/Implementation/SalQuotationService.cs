using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.ViewModel;
using AutoMapper;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using AutoMapper.QueryableExtensions;
using System.Data.SqlClient;
using System.Data;

namespace Sciffer.Erp.Service.Implementation
{
    public class SalQuotationService : ISalQuotationService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericService;
        public SalQuotationService(ScifferContext scifferContext, IGenericService GenericService)
        {
            _scifferContext = scifferContext;
            _genericService = GenericService;
        }
        public string Add(SAL_QUOTATION_VM quotation)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("quotation_detail_id",typeof(int));
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
                dt.Columns.Add("sloc_id", typeof(int));
                dt.Columns.Add("sales_value_local", typeof(double));
                dt.Columns.Add("asessable_value_local", typeof(double));
                dt.Columns.Add("drawing_no", typeof(string));
                dt.Columns.Add("material_cost_per_unit", typeof(double));
                dt.Columns.Add("sac_hsn_id", typeof(int));
                DateTime dte = new DateTime(1990, 1, 1);
                int k = 1;
                for (var i = 0; i < quotation.item_id1.Count; i++)
                {
                    if (quotation.item_id1[i]!= "")
                    {
                        dt.Rows.Add(quotation.quotation_detail_id1[i]==""?0:int.Parse(quotation.quotation_detail_id1[i]), k, 
                            quotation.item_id1[i] == "" ? 0 : int.Parse(quotation.item_id1[i]), 
                            quotation.delivery_date1[i]==""? dte : DateTime.Parse(quotation.delivery_date1[i]), 
                            quotation.quantity1[i] == "" ? 0 : double.Parse(quotation.quantity1[i]),
                            quotation.uom_id1[i] == "" ? 0 : int.Parse(quotation.uom_id1[i]), 
                            quotation.unit_price1[i] == "" ? 0 : double.Parse(quotation.unit_price1[i]),
                            quotation.discount1[i] == "" ? 0 : double.Parse(quotation.discount1[i]),
                            quotation.effective_unit_price1[i] == "" ? 0 : double.Parse(quotation.effective_unit_price1[i]), 
                            quotation.sales_value1[i] == "" ? 0 : double.Parse(quotation.sales_value1[i]),
                            quotation.assessable_rate1[i] == "" ? 0 : double.Parse(quotation.assessable_rate1[i]),
                            quotation.assessable_value1[i] == "" ? 0 : double.Parse(quotation.assessable_value1[i]),
                            quotation.tax_id1[i] == "" ? 0 : int.Parse(quotation.tax_id1[i]),
                             quotation.sloc_id1[i] == "" ? 0 : int.Parse(quotation.sloc_id1[i]), 0,
                            0, quotation.drawing_no1[i] == "" ? "0" : quotation.drawing_no1[i],
                            quotation.material_cost_per_unit1[i] == "" ? 0 : double.Parse(quotation.material_cost_per_unit1[i]),
                            quotation.sac_hsn_id1[i] == "" ? 0 : int.Parse(quotation.sac_hsn_id1[i]));
                        k = k + 1;
                    }
                }
               
                var entity = new SqlParameter("@entity", "save");
                var quotation_id = new SqlParameter("@quotation_id", quotation.QUOTATION_ID);
                var business_unit_id = new SqlParameter("@business_unit_id", quotation.BUSINESS_UNIT_ID);
                var buyer_id = new SqlParameter("@buyer_id", quotation.BUYER_ID);
                var consignee_id = new SqlParameter("@consignee_id", quotation.CONSIGNEE_ID); 
                 var customer_rfq_date = new SqlParameter("@customer_rfq_date", quotation.CUSTOMER_RFQ_DATE==null? dte : quotation.CUSTOMER_RFQ_DATE);
                var customer_rfq_no = new SqlParameter("@customer_rfq_no", quotation.CUSTOMER_RFQ_NO==null?string.Empty:quotation.CUSTOMER_RFQ_NO);
                var delivery_date = new SqlParameter("@delivery_date", quotation.DELIVERY_DATE==null?dte: quotation.DELIVERY_DATE);
                var freight_terms_id = new SqlParameter("@freight_terms_id", quotation.FREIGHT_TERMS_ID);
                var gross_value_currency_id = new SqlParameter("@gross_value_currency_id", quotation.GROSS_VALUE_CURRENCY_ID);
                var internal_remarks = new SqlParameter("@internal_remarks", quotation.INTERNAL_REMARKS==null?string.Empty: quotation.INTERNAL_REMARKS);
                var net_value_currency_id = new SqlParameter("@net_value_currency_id", quotation.NET_VALUE_CURRENCY_ID);
                var payment_cycle_id = new SqlParameter("@payment_cycle_id", quotation.PAYMENT_CYCLE_ID);
                var payment_terms_id = new SqlParameter("@payment_terms_id", quotation.PAYMENT_TERMS_ID);
                var plant_id = new SqlParameter("@plant_id", quotation.PLANT_ID);
                var remarks = new SqlParameter("@remarks", quotation.REMARKS==null?string.Empty:quotation.REMARKS);
                var quotation_date = new SqlParameter("@quotation_date", quotation.QUOTATION_DATE);
                var quotation_expiry_date = new SqlParameter("@quotation_expiry_date", quotation.QUOTATION_EXPIRY_DATE==null?dte: quotation.QUOTATION_EXPIRY_DATE);
                var quotation_number = new SqlParameter("@quotation_number", quotation.QUOTATION_NUMBER);
                var sales_category_id = new SqlParameter("@sales_category_id", quotation.SALES_CATEGORY_ID);
                var sales_rm_id = new SqlParameter("@sales_rm_id", quotation.SALES_RM_ID==null?0:quotation.SALES_RM_ID);
                var sal_gross_value = new SqlParameter("@sal_gross_value", quotation.SAL_GROSS_VALUE);
                var sal_net_value = new SqlParameter("@sal_net_value", quotation.SAL_NET_VALUE);
                var territory_id = new SqlParameter("@territory_id", quotation.TERRITORY_ID==null?0:quotation.TERRITORY_ID);
                var billing_address = new SqlParameter("@billing_address", quotation.BILLING_ADDRESS);
                var billing_city = new SqlParameter("@billing_city", quotation.BILLING_CITY);
                var billing_email_id = new SqlParameter("@billing_email_id", quotation.BILLING_EMAIL_ID==null?string.Empty:quotation.BILLING_EMAIL_ID);
                var billing_pincode = new SqlParameter("@billing_pincode", quotation.BILLING_PINCODE);
                var billing_state_id = new SqlParameter("@billing_state_id", quotation.BILLING_STATE_ID);
                var shipping_address = new SqlParameter("@shipping_address", quotation.SHIPPING_ADDRESS);
                var shipping_city = new SqlParameter("@shipping_city", quotation.SHIPPING_CITY);
                var shipping_pincode = new SqlParameter("@shipping_pincode", quotation.SHIPPING_PINCODE);
                var shipping_state_id = new SqlParameter("@shipping_state_id", quotation.SHIPPING_STATE_ID);
                var pan_no = new SqlParameter("@pan_no", quotation.pan_no);
                var ecc_no = new SqlParameter("@ecc_no", quotation.ecc_no==null?string.Empty:quotation.ecc_no);
                var vat_tin_no = new SqlParameter("@vat_tin_no", quotation.vat_tin_no==null?string.Empty:quotation.vat_tin_no);
                var doc_currency_id = new SqlParameter("@doc_currency_id", quotation.doc_currency_id);
                var cst_tin_no = new SqlParameter("@cst_tin_no", quotation.cst_tin_no==null?string.Empty:quotation.cst_tin_no);
                var service_tax_no = new SqlParameter("@service_tax_no", quotation.service_tax_no==null?string.Empty:quotation.service_tax_no);
                var gst_no = new SqlParameter("@gst_no", quotation.gst_no==null?string.Empty:quotation.gst_no);
                var deleteids = new SqlParameter("@deleteids", quotation.DELETEIDS == null ? string.Empty : quotation.DELETEIDS);
                if (quotation.FileUpload != null)
                {
                    quotation.attachment = _genericService.GetFilePath("SalesQuotation", quotation.FileUpload);
                }
                else
                {
                    quotation.attachment = "No File";
                }
                var attachment = new SqlParameter("@attachment", quotation.attachment);
                var commisionerate = new SqlParameter("@commisionerate", quotation.commisionerate == null ? "" : quotation.commisionerate);
                var range = new SqlParameter("@range", quotation.range == null ? "" : quotation.range);
                var division = new SqlParameter("@division", quotation.division == null ? "" : quotation.division);
                var form_id = new SqlParameter("@form_id", quotation.FORM_ID == null ? 0 : quotation.FORM_ID);
                var supply_state_id = new SqlParameter("@supply_state_id", quotation.supply_state_id == null ? 0 : quotation.supply_state_id);
                var delivery_state_id = new SqlParameter("@delivery_state_id", quotation.delivery_state_id == null ? 0 : quotation.delivery_state_id);
                var shipping_email_id = new SqlParameter("@shipping_email_id", quotation.SHIPPING_EMAIL_ID == null ? string.Empty : quotation.SHIPPING_EMAIL_ID);
                var shipping_pan_no = new SqlParameter("@shipping_pan_no", quotation.shipping_pan_no == null ? string.Empty : quotation.shipping_pan_no);
                var shipping_gst_no = new SqlParameter("@shipping_gst_no", quotation.shipping_gst_no == null ? string.Empty : quotation.shipping_gst_no);
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_sales_quotation_detail";
                t1.Value = dt;

               var val = _scifferContext.Database.SqlQuery<string>("exec save_sales_quotation @entity,@quotation_id ,@business_unit_id ,@buyer_id ,@consignee_id , @customer_rfq_date ,@customer_rfq_no ,@delivery_date ,@freight_terms_id ,@gross_value_currency_id ,@internal_remarks ,@net_value_currency_id ,@payment_cycle_id ,@payment_terms_id ,@plant_id ,@remarks ,@quotation_date ,@quotation_expiry_date ,@quotation_number ,@sales_category_id ,@sales_rm_id ,@sal_gross_value ,@sal_net_value ,@territory_id ,@billing_address ,@billing_city ,@billing_email_id ,@billing_pincode ,@billing_state_id ,@shipping_address ,@shipping_city ,@shipping_pincode ,@shipping_state_id ,@pan_no ,@ecc_no ,@vat_tin_no ,@doc_currency_id ,@cst_tin_no ,@service_tax_no ,@gst_no,@attachment,@commisionerate,@range,@division,@form_id,@deleteids,@supply_state_id,@delivery_state_id,@shipping_email_id,@shipping_pan_no,@shipping_gst_no,@t1", entity, quotation_id, business_unit_id, buyer_id, consignee_id, customer_rfq_date, customer_rfq_no, delivery_date, 
               freight_terms_id, gross_value_currency_id, internal_remarks, net_value_currency_id, payment_cycle_id, payment_terms_id, plant_id, remarks, quotation_date,
               quotation_expiry_date, quotation_number, sales_category_id, sales_rm_id, sal_gross_value, sal_net_value, territory_id, billing_address, billing_city,
               billing_email_id, billing_pincode, billing_state_id, shipping_address, shipping_city, shipping_pincode, shipping_state_id, pan_no, ecc_no, vat_tin_no,
               doc_currency_id, cst_tin_no, service_tax_no, gst_no, attachment, commisionerate, range, division, form_id,deleteids, supply_state_id, delivery_state_id, shipping_email_id, shipping_pan_no, shipping_gst_no, t1).FirstOrDefault();
                if (quotation.FileUpload != null)
                {
                    quotation.FileUpload.SaveAs(quotation.attachment);
                }
                if(val.Contains("Saved"))
                {
                    return val.Split('~')[1];
                }
                else
                {
                    return "Error";
                }
            }
            catch(Exception ex)
            {
                return "Error";
            }
           // return true;
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

        public SAL_QUOTATION_VM Get(int id)
        {
            SAL_QUOTATION quotation = _scifferContext.SAL_QUOTATION.FirstOrDefault(c => c.QUOTATION_ID == id);

            Mapper.CreateMap<SAL_QUOTATION, SAL_QUOTATION_VM>().ForMember(dest => dest.REF_PAYMENT_CYCLE_TYPE, opt => opt.Ignore()); ;
            SAL_QUOTATION_VM quotationvm = Mapper.Map<SAL_QUOTATION, SAL_QUOTATION_VM>(quotation);
            quotationvm.BUYER_CODE = quotation.REF_CUSTOMER.CUSTOMER_CODE;
            quotationvm.BUYER_NAME = quotation.REF_CUSTOMER.CUSTOMER_NAME;
            quotationvm.CONSIGNEE_CODE = quotation.REF_CUSTOMER1.CUSTOMER_CODE;
            quotationvm.CONSIGNEE_NAME = quotation.REF_CUSTOMER1.CUSTOMER_NAME;
            quotationvm.SAL_QUOTATION_DETAIL = quotationvm.SAL_QUOTATION_DETAIL.Where(c => c.IS_ACTIVE == true).ToList();
            List<sal_quotation_detail_vm> sm = new List<sal_quotation_detail_vm>();
            foreach(var item in quotationvm.SAL_QUOTATION_DETAIL)
            {
                sal_quotation_detail_vm vs = new sal_quotation_detail_vm();
                vs.assessable_rate = item.ASSESSABLE_RATE;
                vs.assessable_value = item.ASSESSABLE_VALUE;
                vs.delivery_date = item.DELIVERY_DATE;
                vs.discount = item.DISCOUNT;
                vs.drawing_no = item.drawing_no;
                vs.effective_unit_price = item.EFFECTIVE_UNIT_PRICE;
                vs.item_code = item.REF_ITEM.ITEM_CODE;
                vs.item_id = item.ITEM_ID;
                vs.item_name = item.REF_ITEM.ITEM_NAME;
                vs.material_cost_per_unit = item.material_cost_per_unit;
                vs.quantity = item.QUANTITY;
                vs.quotation_detail_id = item.QUOTATION_DETAIL_ID;
                vs.quotation_id = item.QUOTATION_ID;
                vs.sales_value = item.SALES_VALUE;
                vs.sloc_id = item.SLOC_ID;
                vs.sloc_name = item.REF_STORAGE_LOCATION.storage_location_name + "/" + item.REF_STORAGE_LOCATION.description;
                vs.sr_no = item.SR_NO;
                vs.tax_code = item.ref_tax.tax_code;
                vs.tax_id = item.tax_id;
                vs.tax_name = item.ref_tax.tax_name;
                vs.unit_price = item.UNIT_PRICE;
                vs.uom_id = item.UOM_ID;
                vs.uom_name = item.REF_UOM.UOM_NAME;              
                var i = _genericService.GetUserDescriptionForItem(item.ITEM_ID);
                vs.sac_hsn_id = i.sac_id;
                vs.sac_hsn_name = i.sac_name;
                sm.Add(vs);
            }
            quotationvm.sal_quotation_detail_vm = sm;
            var billingcountry = _scifferContext.REF_STATE.Where(s => s.STATE_ID == quotationvm.BILLING_STATE_ID)
                                      .Select(s => new
                                      {
                                          COUNTRY_ID = s.COUNTRY_ID
                                      }).Single();
            var country = _scifferContext.REF_STATE.Where(s => s.STATE_ID == quotationvm.SHIPPING_STATE_ID)
                          .Select(s => new
                          {
                              COUNTRY_ID = s.COUNTRY_ID
                          }).Single();
            var paymentcyletype = _scifferContext.REF_PAYMENT_CYCLE.Where(P => P.PAYMENT_CYCLE_ID == quotationvm.PAYMENT_CYCLE_ID)
                                     .Select(P => new
                                     {
                                         PAYMENT_CYCLE_TYPE_ID = P.PAYMENT_CYCLE_TYPE_ID
                                     }).Single();
            quotationvm.PAYMENT_CYCLE_TYPE_ID = paymentcyletype.PAYMENT_CYCLE_TYPE_ID;
            quotationvm.BILLING_COUNTRY_ID = billingcountry.COUNTRY_ID;
            quotationvm.SHIPPING_COUNTRY_ID = country.COUNTRY_ID;
            return quotationvm;
        }

        public List<SAL_QUOTATION_VM> GetAll()
        {
            Mapper.CreateMap<SAL_QUOTATION, SAL_QUOTATION_VM>().ForMember(dest => dest.REF_PAYMENT_CYCLE_TYPE, opt => opt.Ignore());
            return _scifferContext.SAL_QUOTATION.Project().To<SAL_QUOTATION_VM>().ToList();

        }

        public List<SAL_QUOTATION_VM> getall()
        {
            var query = (from salQuot in _scifferContext.SAL_QUOTATION
                         join c in _scifferContext.ref_document_numbring on salQuot.SALES_CATEGORY_ID equals c.document_numbring_id
                         join Buyer in _scifferContext.REF_CUSTOMER on salQuot.BUYER_ID equals Buyer.CUSTOMER_ID
                         join CONSIGNEE in _scifferContext.REF_CUSTOMER on salQuot.CONSIGNEE_ID equals CONSIGNEE.CUSTOMER_ID
                         join net_currency in _scifferContext.REF_CURRENCY on salQuot.NET_VALUE_CURRENCY_ID equals net_currency.CURRENCY_COUNTRY_ID
                         join gross_currency in _scifferContext.REF_CURRENCY on salQuot.GROSS_VALUE_CURRENCY_ID equals gross_currency.CURRENCY_ID
                         join business_unit in _scifferContext.REF_BUSINESS_UNIT on salQuot.BUSINESS_UNIT_ID equals business_unit.BUSINESS_UNIT_ID
                         join Plant in _scifferContext.REF_PLANT on salQuot.PLANT_ID equals Plant.PLANT_ID
                         join Freight in _scifferContext.REF_FREIGHT_TERMS on salQuot.FREIGHT_TERMS_ID equals Freight.FREIGHT_TERMS_ID
                         join Territory in _scifferContext.REF_TERRITORY on salQuot.TERRITORY_ID equals Territory.TERRITORY_ID into j
                         from t in j.DefaultIfEmpty()
                         join user in _scifferContext.REF_EMPLOYEE on salQuot.SALES_RM_ID equals user.employee_id into j1
                         from emp in j1.DefaultIfEmpty()
                         join payment_terms in _scifferContext.REF_PAYMENT_TERMS on salQuot.PAYMENT_TERMS_ID equals payment_terms.payment_terms_id
                         join payment_cycle in _scifferContext.REF_PAYMENT_CYCLE on salQuot.PAYMENT_CYCLE_ID equals payment_cycle.PAYMENT_CYCLE_ID
                         join state in _scifferContext.REF_STATE on salQuot.BILLING_STATE_ID equals state.STATE_ID
                         join shipping_state in _scifferContext.REF_STATE on salQuot.SHIPPING_STATE_ID equals shipping_state.STATE_ID
                         join currency in _scifferContext.REF_CURRENCY on salQuot.doc_currency_id equals currency.CURRENCY_ID
                         join form in _scifferContext.SAL_QUOTATION_FORM on salQuot.QUOTATION_ID equals form.QUOTATION_ID into form1
                         from form2 in form1.DefaultIfEmpty()
                         join s1 in _scifferContext.REF_STATE on salQuot.supply_state_id equals s1.STATE_ID into j2
                         from s11 in j2.DefaultIfEmpty()
                         join s2 in _scifferContext.REF_STATE on salQuot.delivery_state_id equals s2.STATE_ID into j3
                         from s22 in j3.DefaultIfEmpty()
                         select new SAL_QUOTATION_VM()
                         {
                             QUOTATION_ID = salQuot.QUOTATION_ID,
                             SALES_CATEGORY = c.category,
                             QUOTATION_NUMBER = salQuot.QUOTATION_NUMBER,
                             QUOTATION_DATE = salQuot.QUOTATION_DATE,
                             BUYER_NAME = Buyer.CUSTOMER_NAME,
                             BUYER_CODE = Buyer.CUSTOMER_CODE,
                             CONSIGNEE_NAME = CONSIGNEE.CUSTOMER_NAME,
                             CONSIGNEE_CODE = CONSIGNEE.CUSTOMER_CODE,
                             SAL_NET_VALUE = salQuot.SAL_NET_VALUE,
                             NET_VALUE_CURRENCY_NAME = net_currency.CURRENCY_NAME,
                             SAL_GROSS_VALUE = salQuot.SAL_GROSS_VALUE,
                             GROSS_VALUE_CURRENCY_NAME = gross_currency.CURRENCY_NAME,
                             BUSINESS_UNIT_NAME = business_unit.BUSINESS_UNIT_NAME,
                             PLANT_NAME = Plant.PLANT_CODE,
                             FREIGHT_TERMS_NAME = Freight.FREIGHT_TERMS_NAME,
                             TERRITORY_NAME = t == null ? string.Empty : t.TERRITORY_NAME,
                             SALES_RM_NAME = emp == null ? string.Empty : emp.employee_code + "-" + emp.employee_name,
                             DELIVERY_DATE = salQuot.DELIVERY_DATE,
                             CUSTOMER_RFQ_NO = salQuot.CUSTOMER_RFQ_NO,
                             CUSTOMER_RFQ_DATE = salQuot.CUSTOMER_RFQ_DATE,
                             QUOTATION_EXPIRY_DATE = salQuot.QUOTATION_EXPIRY_DATE,
                             PAYMENT_TERMS_NAME = payment_terms.payment_terms_description,
                             PAYMENT_CYCLE_TYPE_NAME = payment_cycle.REF_PAYMENT_CYCLE_TYPE.PAYMENT_CYCLE_TYPE_NAME,
                             PAYMENT_CYCLE_NAME = payment_cycle.PAYMENT_CYCLE_NAME,
                             INTERNAL_REMARKS = salQuot.INTERNAL_REMARKS,
                             REMARKS = salQuot.REMARKS,
                             BILLING_ADDRESS = salQuot.BILLING_ADDRESS,
                             BILLING_CITY = salQuot.BILLING_CITY,
                             BILLING_PINCODE = salQuot.BILLING_PINCODE,
                             BILLING_STATE_NAME = state.STATE_NAME,
                             BILLING_COUNTRY_NAME = state.REF_COUNTRY.COUNTRY_NAME,
                             BILLING_EMAIL_ID = salQuot.BILLING_EMAIL_ID,
                             SHIPPING_ADDRESS = salQuot.SHIPPING_ADDRESS,
                             SHIPPING_CITY = salQuot.SHIPPING_CITY,
                             SHIPPING_PINCODE = salQuot.SHIPPING_PINCODE,
                             SHIPPING_STATE_NAME = shipping_state.STATE_NAME,
                             SHIPPING_COUNTRY_NAME = shipping_state.REF_COUNTRY.COUNTRY_NAME,
                             SHIPPING_EMAIL_ID = salQuot.SHIPPING_EMAIL_ID,
                             pan_no = salQuot.pan_no,
                             ecc_no = salQuot.ecc_no,
                             vat_tin_no = salQuot.vat_tin_no,
                             cst_tin_no = salQuot.cst_tin_no,
                             service_tax_no = salQuot.service_tax_no,
                             gst_no = salQuot.gst_no,
                             attachment = salQuot.attachment,
                             commisionerate = salQuot.commisionerate,
                             range = salQuot.range,
                             division = salQuot.division,
                             business_desc = business_unit.BUSINESS_UNIT_DESCRIPTION,
                             plant_code = Plant.PLANT_NAME,
                             currency_name = currency.CURRENCY_NAME,
                             form_name = form2.REF_FORM.FORM_NAME,
                             delivery_state = s22 == null ? string.Empty : s22.STATE_NAME,
                             supply_state = s11 == null ? string.Empty : s11.STATE_NAME,
                             shipping_gst_no = salQuot.shipping_gst_no,
                             shipping_pan_no=salQuot.shipping_pan_no,
                         }).OrderByDescending(a => a.QUOTATION_ID).ToList();
            return query;
        }

        public List<SAL_QUOTATION_VM> GetQuotationDeatilForSo(int id)
        {
            var query = (from sq in _scifferContext.SAL_QUOTATION.Where(x=>x.order_status==false && x.BUYER_ID==id)
                         join c in _scifferContext.REF_CUSTOMER on sq.BUYER_ID equals c.CUSTOMER_ID
                         select new 
                         {
                               QUOTATION_ID = sq.QUOTATION_ID,
                               QUOTATION_DATE= sq.QUOTATION_DATE,                             
                               QUOTATION_NUMBER =sq.QUOTATION_NUMBER,
                               BUYER_NAME=c.CUSTOMER_NAME,
                               BUYER_CODE=c.CUSTOMER_CODE,
                               SAL_NET_VALUE=sq.SAL_NET_VALUE,
                               SAL_GROSS_VALUE=sq.SAL_GROSS_VALUE,
                         }).ToList().Select(a => new SAL_QUOTATION_VM
                         {
                             quotation_No_Date = a.QUOTATION_NUMBER + "-" + DateTime.Parse(a.QUOTATION_DATE.ToString()).ToString("dd/MMM/yyyy"),
                             sq_date = DateTime.Parse(a.QUOTATION_DATE.ToString()).ToString("dd/MMM/yyyy"),
                             QUOTATION_NUMBER = a.QUOTATION_NUMBER,
                             BUYER_NAME = a.BUYER_NAME,
                             SAL_NET_VALUE=a.SAL_NET_VALUE,
                             QUOTATION_ID=a.QUOTATION_ID,
                         }).ToList();
            return query;
        }
        public List<SAL_QUOTATION_VM> GetQuotationDeatilForSo()
        {
            var query = (from sq in _scifferContext.SAL_QUOTATION.Where(x => x.order_status == false)
                         join c in _scifferContext.REF_CUSTOMER on sq.BUYER_ID equals c.CUSTOMER_ID
                         select new
                         {
                             QUOTATION_ID = sq.QUOTATION_ID,
                             QUOTATION_DATE = sq.QUOTATION_DATE,
                             QUOTATION_NUMBER = sq.QUOTATION_NUMBER,
                             BUYER_NAME = c.CUSTOMER_NAME,
                             BUYER_CODE = c.CUSTOMER_CODE,
                             SAL_NET_VALUE = sq.SAL_NET_VALUE,
                             SAL_GROSS_VALUE = sq.SAL_GROSS_VALUE,
                         }).ToList().Select(a => new SAL_QUOTATION_VM
                         {
                             quotation_No_Date = a.QUOTATION_NUMBER + "-" + DateTime.Parse(a.QUOTATION_DATE.ToString()).ToString("dd/MMM/yyyy"),
                             sq_date = DateTime.Parse(a.QUOTATION_DATE.ToString()).ToString("dd/MMM/yyyy"),
                             QUOTATION_NUMBER = a.QUOTATION_NUMBER,
                             BUYER_NAME = a.BUYER_NAME,
                             SAL_NET_VALUE = a.SAL_NET_VALUE,
                             QUOTATION_ID = a.QUOTATION_ID,
                         }).ToList();
            return query;
        }
        public List<sal_quotation_detail_vm> GetQuotationProductDetail(string id)
        {
           // var id = _scifferContext.SAL_QUOTATION.FirstOrDefault(x => x.QUOTATION_NUMBER == code).QUOTATION_ID;
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getquotationdetailforso");          
            var val = _scifferContext.Database.SqlQuery<sal_quotation_detail_vm>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;         
        }

        public SAL_QUOTATION_VM GetQuotationForSO(int id)
        {
            var quotationvm = (from sq in _scifferContext.SAL_QUOTATION.Where(x => x.QUOTATION_ID == id)
                               join buyer in _scifferContext.REF_CUSTOMER on sq.BUYER_ID equals buyer.CUSTOMER_ID
                               join consignee in _scifferContext.REF_CUSTOMER on sq.CONSIGNEE_ID equals consignee.CUSTOMER_ID
                               join s1 in _scifferContext.REF_STATE on sq.BILLING_STATE_ID equals s1.STATE_ID
                               join s2 in _scifferContext.REF_STATE on sq.SHIPPING_STATE_ID equals s2.STATE_ID
                               join p in _scifferContext.REF_PAYMENT_CYCLE on sq.PAYMENT_CYCLE_ID equals p.PAYMENT_CYCLE_ID
                               join f in _scifferContext.SAL_QUOTATION_FORM on sq.QUOTATION_ID equals f.QUOTATION_ID into jo
                               from form in jo.DefaultIfEmpty()
                               select new SAL_QUOTATION_VM()
                               {
                                   BUYER_ID = buyer.CUSTOMER_ID,
                                   BUYER_NAME = buyer.CUSTOMER_NAME,
                                   BUYER_CODE = buyer.CUSTOMER_CODE,
                                   CONSIGNEE_ID = consignee.CUSTOMER_ID,
                                   CONSIGNEE_CODE = consignee.CUSTOMER_CODE,
                                   CONSIGNEE_NAME = consignee.CUSTOMER_NAME,
                                   BUSINESS_UNIT_ID = sq.BUSINESS_UNIT_ID,
                                   FREIGHT_TERMS_ID = sq.FREIGHT_TERMS_ID,
                                   PLANT_ID = sq.PLANT_ID,
                                   SALES_RM_ID = sq.SALES_RM_ID,
                                   TERRITORY_ID = sq.TERRITORY_ID,
                                   BILLING_ADDRESS = sq.BILLING_ADDRESS,
                                   BILLING_CITY=sq.BILLING_CITY,
                                   BILLING_COUNTRY_ID=s1.COUNTRY_ID,
                                   BILLING_EMAIL_ID=sq.BILLING_EMAIL_ID,
                                   BILLING_PINCODE=sq.BILLING_PINCODE,
                                   BILLING_STATE_ID=sq.BILLING_STATE_ID,
                                   SHIPPING_ADDRESS=sq.SHIPPING_ADDRESS,
                                   SHIPPING_CITY=sq.SHIPPING_CITY,
                                   SHIPPING_COUNTRY_ID=s2.COUNTRY_ID,
                                   SHIPPING_PINCODE=sq.SHIPPING_PINCODE,
                                   SHIPPING_STATE_ID=sq.SHIPPING_STATE_ID,
                                   GROSS_VALUE_CURRENCY_ID=sq.GROSS_VALUE_CURRENCY_ID,
                                   NET_VALUE_CURRENCY_ID=sq.NET_VALUE_CURRENCY_ID,
                                   PAYMENT_CYCLE_ID=sq.PAYMENT_CYCLE_ID,
                                   PAYMENT_CYCLE_TYPE_ID=p.PAYMENT_CYCLE_TYPE_ID,
                                   PAYMENT_TERMS_ID=sq.PAYMENT_TERMS_ID,
                                   pan_no=sq.pan_no,
                                   cst_tin_no=sq.cst_tin_no,
                                   vat_tin_no=sq.vat_tin_no,
                                   gst_no=sq.gst_no,
                                   service_tax_no=sq.service_tax_no,
                                   ecc_no=sq.ecc_no, 
                                   FORM_ID= form==null?0: form.FORM_ID,
                                   doc_currency_id=sq.doc_currency_id,
                                   commisionerate = sq.commisionerate,
                                   range = sq.range,
                                   division = sq.division,
                                   supply_state_id=sq.supply_state_id,
                                   delivery_state_id=sq.delivery_state_id
                               }).FirstOrDefault();
            return quotationvm;
        }

        public sal_quotation_report_vm GetQuotationDetailForReport(int id)
        {
            var query = (from si in _scifferContext.SAL_QUOTATION.Where(x => x.QUOTATION_ID == id)
                         from co in _scifferContext.REF_COMPANY
                         join p in _scifferContext.REF_PLANT on si.PLANT_ID equals p.PLANT_ID
                         join bu in _scifferContext.REF_CUSTOMER on si.BUYER_ID equals bu.CUSTOMER_ID
                         join s1 in _scifferContext.REF_STATE on si.BILLING_STATE_ID equals s1.STATE_ID
                         join c1 in _scifferContext.REF_COUNTRY on s1.COUNTRY_ID equals c1.COUNTRY_ID
                         join con in _scifferContext.REF_CUSTOMER on si.CONSIGNEE_ID equals con.CUSTOMER_ID
                         join s2 in _scifferContext.REF_STATE on si.SHIPPING_STATE_ID equals s2.STATE_ID
                         join c2 in _scifferContext.REF_COUNTRY on s2.COUNTRY_ID equals c2.COUNTRY_ID
                         join s3 in _scifferContext.REF_STATE on co.REGISTERED_STATE_ID equals s3.STATE_ID
                         join c3 in _scifferContext.REF_COUNTRY on s3.COUNTRY_ID equals c3.COUNTRY_ID                         
                         select new sal_quotation_report_vm
                         {                             
                             company_logo = co.LOGO,
                             buyer_address = si.BILLING_ADDRESS,
                             buyer_city = si.BILLING_CITY,
                             buyer_country = c1.COUNTRY_NAME,
                             buyer_cst_no = si.cst_tin_no,
                             buyer_ecc_no = si.ecc_no,
                             buyer_name = bu.CUSTOMER_NAME,
                             buyer_pan_no = si.pan_no,
                             buyer_pin_code = si.BILLING_PINCODE,
                             buyer_state = s1.STATE_NAME,
                             buyer_vat_no = si.vat_tin_no,
                             company_address = co.REGISTERED_ADDRESS,
                             company_cin_no = co.CIN_NO,
                             company_city = co.REGISTERED_CITY,
                             company_country = c3.COUNTRY_NAME,
                             company_display_name = co.COMPANY_DISPLAY_NAME,
                             company_email = co.REGISTERED_EMAIL,
                             company_pan_no = co.PAN_NO,
                             company_pincode = co.registered_pincode,
                             company_state = s3.STATE_NAME,
                             company_tan_no = co.TAN_NO,
                             company_telephone = co.REGISTERED_TELEPHONE.ToString(),
                             company_website = co.WEBSITE,
                             compnay_name = co.COMPANY_NAME,
                             consignee_address = si.SHIPPING_ADDRESS,
                             consignee_city = si.SHIPPING_CITY,
                             consignee_country = c2.COUNTRY_NAME,
                             consignee_cst_no = con.cst_tin_no,
                             consignee_ecc_no = con.ecc_no,
                             consignee_name = con.CUSTOMER_NAME,
                             consignee_pan_no = con.pan_no,
                             consignee_pin_code = con.BILLING_PINCODE.ToString(),
                             consignee_state = s2.STATE_NAME,
                             consignee_vat_no = con.vat_tin_no,                            
                             plant_commissionarate = p.excise_commisionerate,
                             plant_division = p.excise_division,
                             plant_name = p.PLANT_NAME,
                             plant_range = p.excise_range,
                             quotation_date = (DateTime)si.QUOTATION_DATE,
                             quotation_id = si.QUOTATION_ID,
                             quotation_number = si.QUOTATION_NUMBER,                          
                             sal_gross_value = si.SAL_GROSS_VALUE,
                         }).FirstOrDefault();
            return query;
        }

        public List<sal_quotation_detail_vm> GetQuotationProductForReport(int id)
        {
            var query = (from sd in _scifferContext.SAL_QUOTATION_DETAIL.Where(x => x.QUOTATION_ID == id && x.IS_ACTIVE == true)
                         join i in _scifferContext.REF_ITEM on sd.ITEM_ID equals i.ITEM_ID
                         join u in _scifferContext.REF_UOM on sd.UOM_ID equals u.UOM_ID
                         select new sal_quotation_detail_vm
                         {
                             assessable_rate = sd.ASSESSABLE_RATE,
                             assessable_value = sd.ASSESSABLE_VALUE,
                             discount = sd.DISCOUNT,
                             effective_unit_price = sd.EFFECTIVE_UNIT_PRICE,
                             item_code = i.ITEM_CODE,
                             item_id = i.ITEM_ID,
                             item_name = i.ITEM_NAME,
                             quantity = sd.QUANTITY,
                             sales_value = sd.SALES_VALUE,
                             quotation_detail_id = (int)sd.QUOTATION_DETAIL_ID,
                             quotation_id = sd.QUOTATION_ID,
                             tax_id = sd.tax_id,
                             unit_price = sd.UNIT_PRICE,
                             uom_id = sd.UOM_ID,
                             uom_name = u.UOM_NAME,
                         }).ToList();
            return query;
        }
    }
}
