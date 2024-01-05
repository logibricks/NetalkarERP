using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.ViewModel;
using AutoMapper;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class SalesInvoiceService : ISalesInvoiceService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericService;
        private readonly ISalSoservice _soservice;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        public SalesInvoiceService(ISalSoservice SalSoservice, ScifferContext scifferContext, IGenericService GenericService)
        {
            _scifferContext = scifferContext;
            _genericService = GenericService;
            _soservice = SalSoservice;
        }
        public string Add(sal_si_vm invoice)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("si_detail_id", typeof(int));
                dt.Columns.Add("so_detail_id", typeof(int));
                dt.Columns.Add("sr_no", typeof(int));
                dt.Columns.Add("item_id", typeof(int));
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
                dt.Columns.Add("drawing_no", typeof(string));
                dt.Columns.Add("material_cost_per_unit", typeof(double));
                dt.Columns.Add("sac_hsn_id", typeof(int));
                dt.Columns.Add("no_of_boxes", typeof(string));
                int k = 1;
                if (invoice.item_id != null)
                {
                    for (var i = 0; i < invoice.item_id.Count; i++)
                    {
                        if (invoice.item_id[i] != "")
                        {
                            int item_id = Convert.ToInt32(invoice.item_id[i]);

                            int result = _genericService.GetCheck_Inventory(Convert.ToInt32(invoice.item_id[i]), Convert.ToInt32(invoice.plant_id), Convert.ToInt32(invoice.storage_location_id[i]), 2, Convert.ToDecimal(invoice.quantity[i]));

                            if (result == 0)
                            {
                                var Item_name = _scifferContext.REF_ITEM.Where(x => x.ITEM_ID == item_id).FirstOrDefault().ITEM_NAME;
                                return "Stock is Not Available " + Item_name;
                            }

                            dt.Rows.Add(0, Convert.ToInt32(invoice.so_detail_id[i]), k,
                            invoice.item_id[i] == "" ? 0 : Convert.ToInt32(invoice.item_id[i]), invoice.quantity[i] == "" ? 0 : Convert.ToDouble(invoice.quantity[i]),
                            invoice.uom_id[i] == "" ? 0 : Convert.ToInt32(invoice.uom_id[i]),
                            invoice.unit_price[i] == "" ? 0 : Convert.ToDouble(invoice.unit_price[i]),
                            invoice.discount[i] == "" ? 0 : Convert.ToDouble(invoice.discount[i]),
                            invoice.effective_unit_price[i] == "" ? 0 : Convert.ToDouble(invoice.effective_unit_price[i]),
                            invoice.sales_value[i] == "" ? 0 : Convert.ToDouble(invoice.sales_value[i]),
                            invoice.assessable_rate[i] == "" ? 0 : Convert.ToDouble(invoice.assessable_rate[i]), invoice.assessable_value[i] == "" ? 0 : Convert.ToDouble(invoice.assessable_value[i]),
                            invoice.tax_id[i] == "" ? 0 : Convert.ToInt32(invoice.tax_id[i]),
                            invoice.storage_location_id[i] == "" ? 0 : Convert.ToInt32(invoice.storage_location_id[i]),
                            invoice.drawing_no[i], invoice.material_cost_per_unit[i] == "" ? 0 : Convert.ToDouble(invoice.material_cost_per_unit[i]),
                            invoice.sac_hsn_id1[i] == "" || invoice.sac_hsn_id1[i] == "null" ? 0 : int.Parse(invoice.sac_hsn_id1[i]),
                            invoice.no_of_boxes[i] == "" || invoice.no_of_boxes[i] == "null" ? "0" : invoice.no_of_boxes[i]);
                            k = k + 1;
                        }

                    }
                }
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("item_batch_detail_id", typeof(int));
                dt1.Columns.Add("item_id", typeof(int));
                dt1.Columns.Add("quantity", typeof(double));
                if (invoice.batch_item_id != null)
                {
                    for (var i = 0; i < invoice.batch_item_id.Count; i++)
                    {
                        if (invoice.batch_item_id[i] != "")
                        {
                            dt1.Rows.Add(int.Parse(invoice.item_batch_detail_id[i]), int.Parse(invoice.batch_item_id[i]), double.Parse(invoice.item_batch_quantity[i]));
                        }
                    }
                }

                DataTable dt2 = new DataTable();
                dt2.Columns.Add("item_batch_detail_id", typeof(int));
                dt2.Columns.Add("item_tag_id", typeof(int));
                dt2.Columns.Add("item_id", typeof(int));
                dt2.Columns.Add("quantity", typeof(double));
                if (invoice.tag_item_id != null)
                {
                    for (var i = 0; i < invoice.tag_item_id.Count; i++)
                    {
                        if (invoice.tag_item_id[i] != "")
                        {
                            dt2.Rows.Add(int.Parse(invoice.item_tag_batch_detail_id[i]), int.Parse(invoice.item_tag_id[i]), int.Parse(invoice.tag_item_id[i]), double.Parse(invoice.item_tag_quantity[i]));
                        }
                    }
                }

                DataTable dt3 = new DataTable();
                dt3.Columns.Add("job_work_detail_in_id", typeof(int));
                dt3.Columns.Add("quantity", typeof(double));
                if (invoice.job_work_detail_in_id != null)
                {
                    for (var i = 0; i < invoice.job_work_detail_in_id.Count; i++)
                    {
                        if (invoice.job_work_detail_in_id[i] != "")
                        {
                            dt3.Rows.Add(
                                int.Parse(invoice.job_work_detail_in_id[i]),
                                double.Parse(invoice.job_work_quantity[i]));
                        }
                    }
                }

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_sale_si_detail";
                t1.Value = dt;

                var t2 = new SqlParameter("@t2", SqlDbType.Structured);
                t2.TypeName = "dbo.temp_sal_si_detail_batch";
                t2.Value = dt1;

                var t3 = new SqlParameter("@t3", SqlDbType.Structured);
                t3.TypeName = "dbo.temp_sal_si_detail_batch_tag";
                t3.Value = dt2;

                var t4 = new SqlParameter("@t4", SqlDbType.Structured);
                t4.TypeName = "dbo.temp_sal_si_detail_challan";
                t4.Value = dt3;

                int create_user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                DateTime dte = new DateTime(1990, 1, 1);
                var si_id = new SqlParameter("@si_id", invoice.si_id);
                var so_id = new SqlParameter("@so_id", invoice.so_id);
                var sales_category_id = new SqlParameter("@sales_category_id", invoice.sales_category_id);
                var si_number = new SqlParameter("@si_number", invoice.si_number == null ? "" : invoice.si_number);
                var si_date = new SqlParameter("@si_date", invoice.si_date == null ? DateTime.UtcNow : invoice.si_date);
                var buyer_id = new SqlParameter("@buyer_id", invoice.buyer_id);
                var consignee_id = new SqlParameter("@consignee_id", invoice.consignee_id);
                var sal_net_value = new SqlParameter("@sal_net_value", invoice.sal_net_value);
                var net_value_currency_id = new SqlParameter("@net_value_currency_id", invoice.net_value_currency_id);
                var sal_gross_value = new SqlParameter("@sal_gross_value", invoice.sal_gross_value);
                var gross_value_currency_id = new SqlParameter("@gross_value_currency_id", invoice.gross_value_currency_id);
                var business_unit_id = new SqlParameter("@business_unit_id", invoice.business_unit_id);
                var plant_id = new SqlParameter("@plant_id", invoice.plant_id);
                var freight_terms_id = new SqlParameter("@freight_terms_id", invoice.freight_terms_id);
                var territory_id = new SqlParameter("@territory_id", invoice.territory_id == null ? 0 : invoice.territory_id);
                var sales_rm_id = new SqlParameter("@sales_rm_id", invoice.sales_rm_id == null ? 0 : invoice.sales_rm_id);
                var customer_po_no = new SqlParameter("@customer_po_no", invoice.customer_po_no == null ? string.Empty : invoice.customer_po_no);
                var customer_po_date = new SqlParameter("@customer_po_date", invoice.customer_po_date == null ? dte : invoice.customer_po_date);
                var payment_terms_id = new SqlParameter("@payment_terms_id", invoice.payment_terms_id);
                var payment_cycle_id = new SqlParameter("@payment_cycle_id", invoice.payment_cycle_id);
                var credit_avail_after_invoice = new SqlParameter("@credit_avail_after_invoice", invoice.credit_avail_after_invoice == null ? 0 : invoice.credit_avail_after_invoice);
                var vehicle_no = new SqlParameter("@vehicle_no", invoice.vehicle_no == null ? string.Empty : invoice.vehicle_no);
                var removal_date = new SqlParameter("@removal_date", invoice.removal_date == null ? dte : invoice.removal_date);
                var removal_time = new SqlParameter("@removal_time", invoice.removal_time == null ? DateTime.UtcNow : invoice.removal_time);
                var internal_remarks = new SqlParameter("@internal_remarks", invoice.internal_remarks == null ? string.Empty : invoice.internal_remarks);
                var remarks = new SqlParameter("@remarks", invoice.remarks == null ? string.Empty : invoice.remarks);
                var billing_address = new SqlParameter("@billing_address", invoice.billing_address);
                var billing_city = new SqlParameter("@billing_city", invoice.billing_city);
                var billing_pincode = new SqlParameter("@billing_pincode", invoice.billing_pincode);
                var billing_state_id = new SqlParameter("@billing_state_id", invoice.billing_state_id);
                var billing_email_id = new SqlParameter("@billing_email_id", invoice.billing_email_id == null ? string.Empty : invoice.billing_email_id);
                var shipping_address = new SqlParameter("@shipping_address", invoice.shipping_address);
                var shipping_city = new SqlParameter("@shipping_city", invoice.shipping_city);
                var shipping_pincode = new SqlParameter("@shipping_pincode", invoice.shipping_pincode);
                var shipping_state_id = new SqlParameter("@shipping_state_id", invoice.shipping_state_id);
                var pan_no = new SqlParameter("@pan_no", invoice.pan_no == null ? string.Empty : invoice.pan_no);
                var ecc_no = new SqlParameter("@ecc_no", invoice.ecc_no == null ? string.Empty : invoice.ecc_no);
                var vat_tin_no = new SqlParameter("@vat_tin_no", invoice.vat_tin_no == null ? string.Empty : invoice.vat_tin_no);
                var cst_tin_no = new SqlParameter("@cst_tin_no", invoice.cst_tin_no == null ? string.Empty : invoice.cst_tin_no);
                var service_tax_no = new SqlParameter("@service_tax_no", invoice.service_tax_no == null ? string.Empty : invoice.service_tax_no);
                var gst_no = new SqlParameter("@gst_no", invoice.gst_no == null ? string.Empty : invoice.gst_no);
                var tds_code_id = new SqlParameter("@tds_code_id", invoice.tds_code_id == null ? 0 : invoice.tds_code_id);
                var doc_currency_id = new SqlParameter("@doc_currency_id", invoice.net_value_currency_id);
                var deleteids = new SqlParameter("@deleteids", invoice.deleteids == null ? string.Empty : invoice.deleteids);
                var createuser = new SqlParameter("@create_user", create_user);
                var asn_no = new SqlParameter("@asn_no", invoice.asn_no == null ? string.Empty : invoice.asn_no);
                var transporter = new SqlParameter("@transporter", invoice.transporter == null ? string.Empty : invoice.transporter);
                var lr_no = new SqlParameter("@lr_no", invoice.lr_no == null ? string.Empty : invoice.lr_no);
                var round_off = new SqlParameter("@round_off", invoice.round_off == null ? 0 : invoice.round_off);
                var supply_state_id = new SqlParameter("@supply_state_id", invoice.supply_state_id == null ? 0 : invoice.supply_state_id);
                var delivery_state_id = new SqlParameter("@delivery_state_id", invoice.delivery_state_id == null ? 0 : invoice.delivery_state_id);
                var mode_of_transport = new SqlParameter("@mode_of_transport", invoice.mode_of_transport == null ? 0 : invoice.mode_of_transport);
                var lr_date = new SqlParameter("@lr_date", invoice.lr_date == null ? dte : invoice.lr_date);
                var e_way_bill = new SqlParameter("@e_way_bill", invoice.e_way_bill == null ? string.Empty : invoice.e_way_bill);
                var e_way_bill_date = new SqlParameter("@e_way_bill_date", invoice.e_way_bill_date == null ? dte : invoice.e_way_bill_date);
                var shipping_email_id = new SqlParameter("@shipping_email_id", invoice.shipping_email_id == null ? string.Empty : invoice.shipping_email_id);
                var shipping_pan_no = new SqlParameter("@shipping_pan_no", invoice.shipping_pan_no == null ? string.Empty : invoice.shipping_pan_no);
                var shipping_gst_no = new SqlParameter("@shipping_gst_no", invoice.shipping_gst_no == null ? string.Empty : invoice.shipping_gst_no);
                var available_credit_limit = new SqlParameter("@available_credit_limit", invoice.available_credit_limit == null ? 0 : invoice.available_credit_limit);
                var gst_tds_code_id = new SqlParameter("@gst_tds_code_id", invoice.gst_tds_code_id == null ? 0 : invoice.gst_tds_code_id);
                var out_date = new SqlParameter("@out_date", invoice.out_date == null ? dte : invoice.out_date);
                var out_time = new SqlParameter("@out_time", invoice.out_time == null ? dte.TimeOfDay : invoice.out_time);
                if (invoice.FileUpload != null)
                {
                    invoice.attachment = _genericService.GetFilePath("SalesInvoice", invoice.FileUpload);
                }
                else
                {
                    invoice.attachment = "No File";
                }
                var attachment = new SqlParameter("@attachment", invoice.attachment);
                var commisionerate = new SqlParameter("@commisionerate", invoice.commisionerate == null ? "" : invoice.commisionerate);
                var range = new SqlParameter("@range", invoice.range == null ? "" : invoice.range);
                var division = new SqlParameter("@division", invoice.division == null ? "" : invoice.division);
                var form_id = new SqlParameter("@form_id", invoice.form_id == null ? 0 : invoice.form_id);

                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_sales_invoice @si_id ,@so_id ,@sales_category_id ,@si_number ,@si_date ,@buyer_id ,@consignee_id ,@sal_net_value ,@net_value_currency_id ,@sal_gross_value ,@gross_value_currency_id ,@business_unit_id ,@plant_id ,@freight_terms_id ,@territory_id ,@sales_rm_id ,@customer_po_no ,@customer_po_date ,@payment_terms_id ,@payment_cycle_id ,@credit_avail_after_invoice ,@vehicle_no ,@removal_date ,@removal_time ,@internal_remarks ,@remarks ,@billing_address ,@billing_city ,@billing_pincode ,@billing_state_id ,@billing_email_id ,@shipping_address ,@shipping_city ,@shipping_pincode ,@shipping_state_id ,@pan_no ,@ecc_no ,@vat_tin_no ,@cst_tin_no ,@service_tax_no ,@gst_no ,@attachment ,@commisionerate,@range,@division,@doc_currency_id,@form_id,@deleteids,@tds_code_id,@create_user,@t1,@asn_no,@transporter,@lr_no,@round_off,@supply_state_id,@delivery_state_id,@mode_of_transport,@lr_date,@e_way_bill,@e_way_bill_date,@shipping_email_id,@shipping_pan_no,@shipping_gst_no,@available_credit_limit,@gst_tds_code_id,@out_date,@out_time,@t2,@t3,@t4 ",
                    si_id, so_id, sales_category_id, si_number, si_date, buyer_id, consignee_id, sal_net_value, net_value_currency_id, sal_gross_value,
                    gross_value_currency_id, business_unit_id, plant_id, freight_terms_id, territory_id, sales_rm_id, customer_po_no, customer_po_date,
                    payment_terms_id, payment_cycle_id, credit_avail_after_invoice, vehicle_no, removal_date, removal_time, internal_remarks, remarks,
                    billing_address, billing_city, billing_pincode, billing_state_id, billing_email_id, shipping_address, shipping_city, shipping_pincode,
                    shipping_state_id, pan_no, ecc_no, vat_tin_no, cst_tin_no, service_tax_no, gst_no, attachment, commisionerate, range, division, doc_currency_id,
                    form_id, deleteids, tds_code_id, createuser, t1, asn_no, transporter, lr_no, round_off, supply_state_id, delivery_state_id, mode_of_transport, lr_date, e_way_bill, e_way_bill_date, shipping_email_id, shipping_pan_no, shipping_gst_no, available_credit_limit, gst_tds_code_id, out_date, out_time, t2, t3, t4).FirstOrDefault();

                if (val.Contains("Saved"))
                {

                    if (invoice.FileUpload != null)
                    {
                        invoice.FileUpload.SaveAs(invoice.attachment);
                    }
                    return val;
                }
                else
                {
                    System.IO.File.Delete(invoice.attachment);
                    return val;
                }

            }
            catch (Exception ex)
            {
                log4net.GlobalContext.Properties["user"] = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "Error : " + ex.Message;
            }
            // return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var invoice = _scifferContext.SAL_SI.Find(id);
                invoice.is_active = false;
                _scifferContext.Entry(invoice).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
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

        public sal_si_vm Get(int id)
        {
            sal_si invoice = _scifferContext.SAL_SI.FirstOrDefault(c => c.si_id == id);
            Mapper.CreateMap<sal_si, sal_si_vm>();
            sal_si_vm quotationvm = Mapper.Map<sal_si, sal_si_vm>(invoice);
            quotationvm.SAL_SI_DETAIL = quotationvm.SAL_SI_DETAIL.Where(c => c.is_active == true).ToList();
            List<sal_si_detail_report_vm> sm = new List<sal_si_detail_report_vm>();
            foreach (var i in quotationvm.SAL_SI_DETAIL)
            {
                sal_si_detail_report_vm s = new sal_si_detail_report_vm();
                s.assessable_rate = (decimal)i.assessable_rate;
                s.assessable_value = (decimal)i.assessable_value;
                s.discount = (decimal)i.discount;
                s.drawing_no = i.drawing_no;
                s.effective_unit_price = (decimal)i.effective_unit_price;
                s.item_id = i.item_id;
                s.item_name = i.REF_ITEM.ITEM_CODE + "/" + i.REF_ITEM.ITEM_NAME;
                s.material_cost_per_unit = (decimal)i.material_cost_per_unit;
                s.quantity = (decimal)i.quantity;
                s.sales_value = (decimal)i.sales_value;
                s.si_detail_id = (int)i.si_detail_id;
                s.tax_id = i.tax_id;
                s.unit_price = (decimal)i.unit_price;
                s.uom_id = i.uom_id;
                s.uom_name = i.REF_UOM.UOM_NAME;
                s.tax_name = i.ref_tax.tax_code + "/" + i.ref_tax.tax_name;
                s.storage_location_id = i.storage_location_id;
                s.storage_location_name = i.REF_STORAGE_LOCATION.storage_location_name + "/" + i.REF_STORAGE_LOCATION.description;
                var sac = _genericService.GetUserDescriptionForItem(i.item_id);
                s.sac_hsn_code = sac.sac_name;
                sm.Add(s);
            }
            quotationvm.sal_si_detail_report_vm = sm;
            foreach (var i in quotationvm.SAL_SI_FORM)
            {
                quotationvm.form_id = i.form_id;
                break;
            }
            var billingcountry = _scifferContext.REF_STATE.Where(s => s.STATE_ID == quotationvm.billing_state_id)
                                      .Select(s => new
                                      {
                                          COUNTRY_ID = s.COUNTRY_ID
                                      }).Single();
            var country = _scifferContext.REF_STATE.Where(s => s.STATE_ID == quotationvm.shipping_state_id)
                          .Select(s => new
                          {
                              COUNTRY_ID = s.COUNTRY_ID
                          }).Single();
            var paymentcyletype = _scifferContext.REF_PAYMENT_CYCLE.Where(P => P.PAYMENT_CYCLE_ID == quotationvm.payment_cycle_id)
                                     .Select(P => new
                                     {
                                         PAYMENT_CYCLE_TYPE_ID = P.PAYMENT_CYCLE_TYPE_ID
                                     }).Single();
            quotationvm.payment_cycle_type_id = paymentcyletype.PAYMENT_CYCLE_TYPE_ID;
            quotationvm.billing_country_id = billingcountry.COUNTRY_ID;
            quotationvm.shipping_country_id = country.COUNTRY_ID;
            //quotationvm.buyer_id1 = quotationvm.BUYER_ID;
            //quotationvm.CONSIGNEE_ID1 = quotationvm.CONSIGNEE_ID;
            return quotationvm;
        }

        public List<sal_si_vm> GetAll()
        {
            var query = (from si in _scifferContext.SAL_SI.Where(x => x.is_active == true)
                         join so_num in _scifferContext.SAL_SO on si.so_id equals so_num.so_id
                         join c in _scifferContext.ref_document_numbring on si.sales_category_id equals c.document_numbring_id
                         join buyer in _scifferContext.REF_CUSTOMER on si.buyer_id equals buyer.CUSTOMER_ID
                         join consignee in _scifferContext.REF_CUSTOMER on si.consignee_id equals consignee.CUSTOMER_ID
                         join plant in _scifferContext.REF_PLANT on si.plant_id equals plant.PLANT_ID
                         join business in _scifferContext.REF_BUSINESS_UNIT on si.business_unit_id equals business.BUSINESS_UNIT_ID
                         join freight in _scifferContext.REF_FREIGHT_TERMS on si.freight_terms_id equals freight.FREIGHT_TERMS_ID
                         join territory in _scifferContext.REF_TERRITORY on si.territory_id equals territory.TERRITORY_ID into jo
                         from terr in jo.DefaultIfEmpty()
                         join payterms in _scifferContext.REF_PAYMENT_TERMS on si.payment_terms_id equals payterms.payment_terms_id
                         join paycycle in _scifferContext.REF_PAYMENT_CYCLE on si.payment_cycle_id equals paycycle.PAYMENT_CYCLE_ID
                         join paytype in _scifferContext.REF_PAYMENT_CYCLE_TYPE on paycycle.PAYMENT_CYCLE_TYPE_ID equals paytype.PAYMENT_CYCLE_TYPE_ID
                         join bstate in _scifferContext.REF_STATE on si.billing_state_id equals bstate.STATE_ID
                         join c1 in _scifferContext.REF_COUNTRY on bstate.COUNTRY_ID equals c1.COUNTRY_ID
                         join sstate in _scifferContext.REF_STATE on si.shipping_state_id equals sstate.STATE_ID
                         join c2 in _scifferContext.REF_COUNTRY on sstate.COUNTRY_ID equals c2.COUNTRY_ID
                         join net_currency in _scifferContext.REF_CURRENCY on si.net_value_currency_id equals net_currency.CURRENCY_ID
                         join gross_currency in _scifferContext.REF_CURRENCY on si.gross_value_currency_id equals gross_currency.CURRENCY_ID
                         join sr in _scifferContext.REF_EMPLOYEE on si.sales_rm_id equals sr.employee_id into j1
                         from salesrm in j1.DefaultIfEmpty()
                         join form in _scifferContext.SAL_SI_FORM on si.si_id equals form.si_id into form1
                         from form2 in form1.DefaultIfEmpty()
                         join tds in _scifferContext.ref_tds_code on si.tds_code_id equals tds.tds_code_id into tds1
                         from tds2 in tds1.DefaultIfEmpty()
                         join trr in _scifferContext.ref_mode_of_transport on si.mode_of_transport equals trr.mode_of_transport_id into j2
                         from tr in j2.DefaultIfEmpty()
                         join s1 in _scifferContext.REF_STATE on si.supply_state_id equals s1.STATE_ID into j3
                         from s11 in j3.DefaultIfEmpty()
                         join s2 in _scifferContext.REF_STATE on si.delivery_state_id equals s2.STATE_ID into j4
                         from s22 in j4.DefaultIfEmpty()
                         join gs in _scifferContext.ref_gst_tds_code on si.gst_tds_code_id equals gs.gst_tds_code_id into j5
                         from gst in j5.DefaultIfEmpty()
                         join sd in _scifferContext.SAL_SI_DETAIL.Where(x => x.is_active == true) on si.si_id equals sd.si_id
                         join i in _scifferContext.REF_ITEM on sd.item_id equals i.ITEM_ID
                         select new sal_si_vm
                         {
                             si_id = si.si_id,
                             si_number = si.si_number,
                             so_number = so_num.so_number,
                             si_date = si.si_date,
                             buyer_name = buyer.CUSTOMER_NAME,
                             buyer_code = buyer.CUSTOMER_CODE,
                             consignee_name = consignee.CUSTOMER_NAME,
                             consignee_code = consignee.CUSTOMER_CODE,
                             sal_net_value = si.sal_net_value,
                             sal_gross_value = si.sal_gross_value,
                             removal_date = si.removal_date,
                             removal_time = si.removal_time,
                             customer_po_no = si.customer_po_no,
                             customer_po_date = si.customer_po_date,
                             credit_avail_after_invoice = si.credit_avail_after_invoice,
                             internal_remarks = si.internal_remarks,
                             remarks = si.remarks,
                             billing_address = si.billing_address,
                             billing_city = si.billing_city,
                             billing_pincode = si.billing_pincode,
                             billing_email_id = si.billing_email_id,
                             shipping_address = si.shipping_address,
                             shipping_city = si.shipping_city,
                             shipping_pincode = si.shipping_pincode,
                             vehicle_no = si.vehicle_no,
                             attachment = si.attachment,
                             sales_category_name = c.category,
                             customer_code = buyer.CUSTOMER_CODE,
                             net_currency_name = net_currency.CURRENCY_NAME,
                             gross_currency_name = gross_currency.CURRENCY_NAME,
                             business_unit_name = business.BUSINESS_UNIT_NAME,
                             business_desc = business.BUSINESS_UNIT_DESCRIPTION,
                             plant_name = plant.PLANT_NAME,
                             plant_code = plant.PLANT_CODE,
                             freight_terms_name = freight.FREIGHT_TERMS_NAME,
                             territory_name = terr == null ? string.Empty : terr.TERRITORY_NAME,
                             sales_rm = salesrm == null ? string.Empty : salesrm.employee_code + "-" + salesrm.employee_name,
                             payment_terms_name = payterms.payment_terms_code,
                             payment_cycle_name = paycycle.PAYMENT_CYCLE_NAME,
                             payment_cycle_type_name = paytype.PAYMENT_CYCLE_TYPE_NAME,
                             billing_state_name = bstate.STATE_NAME,
                             billing_country_name = c1.COUNTRY_NAME,
                             shipping_state_name = sstate.STATE_NAME,
                             shipping_country_name = c2.COUNTRY_NAME,
                             pan_no = si.pan_no,
                             ecc_no = si.ecc_no,
                             vat_tin_no = si.vat_tin_no,
                             cst_tin_no = si.cst_tin_no,
                             service_tax_no = si.service_tax_no,
                             gst_no = si.gst_no,
                             commisionerate = si.commisionerate,
                             range = si.range,
                             division = si.division,
                             form_name = form2.REF_FORM.FORM_NAME,
                             transporter = si.transporter,
                             lr_no = si.lr_no,
                             asn_no = si.asn_no,
                             tds_name = tds2 == null ? string.Empty : tds2.tds_code + "/" + tds2.tds_code_description,
                             transport_mode = tr == null ? string.Empty : tr.mode_of_transport_name,
                             lr_date = si.lr_date,
                             e_way_bill = si.e_way_bill,
                             supply_state = s11 == null ? string.Empty : s11.STATE_NAME,
                             delivery_state = s22 == null ? string.Empty : s22.STATE_NAME,
                             e_way_bill_date = si.e_way_bill_date,
                             shipping_email_id = si.shipping_email_id,
                             shipping_gst_no = si.shipping_gst_no,
                             shipping_pan_no = si.shipping_pan_no,
                             available_credit_limit = si.available_credit_limit,
                             gst_tds_code = gst == null ? string.Empty : gst.gst_tds_code + "/" + gst.gst_tds_code_description,
                             item_name = i.ITEM_NAME,
                             sales_quantity = sd.quantity,
                             out_date = si.out_date,
                             outtime = si.out_time.ToString(),
                         }).OrderByDescending(a => a.si_id).ToList();
            return query;
        }


        public sal_si_report_vm GetSIDetailForReport(int id, string ent)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getsalesheaderforreport");
            var val = _scifferContext.Database.SqlQuery<sal_si_report_vm>(
            "exec get_sales_invoice @entity,@id", entity, quotation_id).FirstOrDefault();
            return val;
        }

        public List<sales_si_challan> GetSIDetailForReportChallan(int id, string ent)
        {

            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", ent);
            var val = _scifferContext.Database.SqlQuery<sales_si_challan>(
            "exec get_sales_invoice @entity,@id", entity, quotation_id).ToList();
            return val;
        }
        public List<sal_si_detail_report_vm> GetSIProductDetailForSI(int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getsalesitemdetailforreport");
            var val = _scifferContext.Database.SqlQuery<sal_si_detail_report_vm>(
            "exec get_sales_invoice @entity,@id", entity, quotation_id).ToList();
            return val;
        }


        public List<GetBatchForSalesInvoice> gettagbatchforsalesinvoice(int item_id, int plant_id, int sloc_id, int bucket_id, int customer_id)
        {
            var item = new SqlParameter("@item_id", item_id);
            var plant = new SqlParameter("@plant_id", plant_id);
            var sloc = new SqlParameter("@sloc_id", sloc_id);
            var bucket = new SqlParameter("@bucket_id", bucket_id);
            var entity = new SqlParameter("@entity", "gettagbatchforsalesinvoice");
            var customer = new SqlParameter("@customer_id", customer_id);
            var val = _scifferContext.Database.SqlQuery<GetBatchForSalesInvoice>(
            "exec GetTagForSalesInvoice @item_id,@plant_id,@sloc_id,@bucket_id,@entity,@customer_id", item, plant, sloc, bucket, entity, customer).ToList();
            return val;
        }


    }
}
