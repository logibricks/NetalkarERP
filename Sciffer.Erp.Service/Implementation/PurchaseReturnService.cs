using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Data;
using System.Data.SqlClient;
using System.Data;
using Sciffer.Erp.Domain.Model;
using AutoMapper;
using System.Web;



namespace Sciffer.Erp.Service.Implementation
{
    public class PurchaseReturnService : IPurchaseReturnService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _generic;
        public PurchaseReturnService(ScifferContext scifferContext, IGenericService generic)
        {
            _scifferContext = scifferContext;
            _generic = generic;
        }
        public string Add(pur_pi_return_vm Purchase)
        {
            var FileAttachment = "";
            if (Purchase.FileUpload != null)
            {
                FileAttachment = _generic.GetFilePath("PurchaseInvoice", Purchase.FileUpload);
            }
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("pi_return_detail_id", typeof(int));
                dt.Columns.Add("pi_id", typeof(int));
                dt.Columns.Add("pi_detail_id", typeof(int));
                dt.Columns.Add("item_id", typeof(int));
                dt.Columns.Add("sloc_id", typeof(int));
                dt.Columns.Add("quantity", typeof(double));
                dt.Columns.Add("uom_id", typeof(int));
                dt.Columns.Add("unit_price", typeof(double));
                dt.Columns.Add("discount", typeof(double));
                dt.Columns.Add("eff_unit_price", typeof(double));
                dt.Columns.Add("purchase_value", typeof(double));
                dt.Columns.Add("assessable_rate", typeof(double));
                dt.Columns.Add("assessable_value", typeof(double));
                dt.Columns.Add("tax_id", typeof(int));
                dt.Columns.Add("grir_value", typeof(double));
                dt.Columns.Add("basic_value", typeof(double));
                dt.Columns.Add("bucket_id", typeof(int));
                dt.Columns.Add("batch_id", typeof(int));
                for (var i = 0; i < Purchase.item_id1.Count; i++)
                {
                    if (Purchase.item_id1[i] != 0)
                    {

                        int result = _generic.GetCheck_Inventory(Convert.ToInt32(Purchase.item_id1[i]), Convert.ToInt32(Purchase.plant_id), Convert.ToInt32(Purchase.sloc_id1[i]), Purchase.bucket_id1[i], Convert.ToDecimal(Purchase.quantity1[i]));
                        if (result == 0)
                        {
                            return "Stock is Not Available";
                        }

                        dt.Rows.Add(Purchase.pi_return_detail_id1[i] == 0 ? -1 : Purchase.pi_return_detail_id1[i], Purchase.pi_id1[i], Purchase.pi_detail_id1[i], Purchase.item_id1[i],
                            Purchase.sloc_id1[i], Purchase.quantity1[i], Purchase.uom_id1[i], Purchase.unit_price1[i],
                            Purchase.discount1[i], Purchase.eff_unit_price1[i], Purchase.purchase_value1[i],
                            Purchase.assessable_rate1[i], Purchase.assessable_value1[i], Purchase.tax_id1[i],
                            Purchase.grir_value1[i], Purchase.basic_value1[i], Purchase.bucket_id1[i], Purchase.batch_id1[i]);
                    }

                }

                int createdby = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var pi_return_id = new SqlParameter("@pi_return_id", Purchase.pi_return_id == null ? -1 : Purchase.pi_return_id);
                var document_no = new SqlParameter("@document_no", Purchase.document_no == null ? "" : Purchase.document_no);
                var category_id = new SqlParameter("@category_id", Purchase.category_id);
                var posting_date = new SqlParameter("@posting_date", Purchase.posting_date);
                var vendor_id = new SqlParameter("@vendor_id", Purchase.vendor_id);
                var net_value = new SqlParameter("@net_value", Purchase.net_value);
                var net_currency_id = new SqlParameter("@net_currency_id", Purchase.net_currency_id);
                var gross_value = new SqlParameter("@gross_value", Purchase.gross_value);
                var gross_currency_id = new SqlParameter("@gross_currency_id", Purchase.gross_currency_id);
                var business_unit_id = new SqlParameter("@business_unit_id", Purchase.business_unit_id);
                var plant_id = new SqlParameter("@plant_id", Purchase.plant_id);
                var freight_terms_id = new SqlParameter("@freight_terms_id", Purchase.freight_terms_id);
                var billing_address = new SqlParameter("@billing_address", Purchase.billing_address == null ? "" : Purchase.billing_address);
                var billing_city = new SqlParameter("@billing_city", Purchase.billing_city == null ? "" : Purchase.billing_city);
                var billing_state_id = new SqlParameter("@billing_state_id", Purchase.billing_state_id);
                var billing_pincode = new SqlParameter("@billing_pincode", Purchase.billing_pincode == null ? "" : Purchase.billing_pincode);
                var email_id = new SqlParameter("@email_id", Purchase.email_id == null ? "" : Purchase.email_id);
                var gst_in = new SqlParameter("@gst_in", Purchase.gst_in == null ? "" : Purchase.gst_in);
                var gst_vendor_type_id = new SqlParameter("@gst_vendor_type_id", Purchase.gst_vendor_type_id);
                var payment_terms_id = new SqlParameter("@payment_terms_id", Purchase.payment_terms_id);
                var payment_cycle_id = new SqlParameter("@payment_cycle_id", Purchase.payment_cycle_id);
                var tds_code_id = new SqlParameter("@tds_code_id", Purchase.tds_code_id == null ? 0 : Purchase.tds_code_id);
                var internal_remarks = new SqlParameter("@internal_remarks", Purchase.internal_remarks == null ? "" : Purchase.internal_remarks);
                var remarks_on_doc = new SqlParameter("@remarks_on_doc", Purchase.remarks_on_doc == null ? "" : Purchase.remarks_on_doc);
                var attachment = new SqlParameter("@attachment", Purchase.attachment == null ? "" : Purchase.attachment);
                var is_active = new SqlParameter("@is_active", 1);
                var gst_no = new SqlParameter("@gst_no", Purchase.gst_no == null ? "" : Purchase.gst_no);
                var service_tax_no = new SqlParameter("@service_tax_no", Purchase.service_tax_no == null ? "" : Purchase.service_tax_no);
                var created_by = new SqlParameter("@created_by", createdby);
                var round_off = new SqlParameter("@round_off", Purchase.round_off == null ? 0 : Purchase.round_off);

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_pur_pi_return_detail";
                t1.Value = dt;

                var val = _scifferContext.Database.SqlQuery<string>("exec save_purchase_return @pi_return_id ,@document_no ,@category_id ,@posting_date ,@vendor_id ,@net_value ,@net_currency_id ,@gross_value ,@gross_currency_id ,@business_unit_id ,@plant_id ,@freight_terms_id ,@billing_address ,@billing_city ,@billing_state_id ,@billing_pincode ,@email_id ,@gst_in ,@gst_vendor_type_id ,@payment_terms_id ,@payment_cycle_id ,@tds_code_id ,@internal_remarks ,@remarks_on_doc ,@attachment ,@is_active ,@gst_no ,@service_tax_no ,@round_off ,@created_by,@t1",
                    pi_return_id, document_no, category_id, posting_date, vendor_id, net_value, net_currency_id, gross_value, gross_currency_id, business_unit_id, plant_id, freight_terms_id, billing_address, billing_city, billing_state_id, billing_pincode, email_id, gst_in, gst_vendor_type_id, payment_terms_id, payment_cycle_id, tds_code_id, internal_remarks, remarks_on_doc, attachment, is_active, gst_no, service_tax_no, round_off, created_by, t1).FirstOrDefault();

                if (val.ToString().Contains("Saved"))
                {
                    if (Purchase.FileUpload != null)
                    {
                        Purchase.FileUpload.SaveAs(Purchase.attachment);
                    }
                    return val.ToString();
                }
                else
                {
                    System.IO.File.Delete(Purchase.attachment);
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
            }
        }
        public pur_pi_return_vm Get(int? id)
        {
            pur_pi_return purchase = _scifferContext.pur_pi_return.FirstOrDefault(c => c.pi_return_id == id);
            Mapper.CreateMap<pur_pi_return, pur_pi_return_vm>();
            pur_pi_return_vm purchasevm = Mapper.Map<pur_pi_return, pur_pi_return_vm>(purchase);
            purchasevm.pur_pi_return_detail = purchasevm.pur_pi_return_detail.Where(c => c.is_active == true).ToList();
            var vendor_name = _scifferContext.REF_VENDOR.Where(x => x.VENDOR_ID == purchasevm.vendor_id)
                                .Select(s => new
                                {
                                    vendor_name = s.VENDOR_NAME
                                }).Single();
            var billingcountry = _scifferContext.REF_STATE.Where(s => s.STATE_ID == purchasevm.billing_state_id)
                                      .Select(s => new
                                      {
                                          COUNTRY_ID = s.COUNTRY_ID
                                      }).Single();
            var paymentcyletype = _scifferContext.REF_PAYMENT_CYCLE.Where(P => P.PAYMENT_CYCLE_ID == purchasevm.payment_cycle_id)
                                      .Select(P => new
                                      {
                                          PAYMENT_CYCLE_TYPE_ID = P.PAYMENT_CYCLE_TYPE_ID
                                      }).Single();
            purchasevm.payment_cycle_type_id = paymentcyletype.PAYMENT_CYCLE_TYPE_ID;
            purchasevm.country_id = billingcountry.COUNTRY_ID;
            purchasevm.posting_date = DateTime.Parse(purchasevm.posting_date.ToString("dd/MMM/yyyy"));
            return purchasevm;
        }

        public pur_po_report_vm GetPOForReportPReturn(int id)
        {

            var query = (from p in _scifferContext.pur_po.Where(x => x.po_id == id)
                         join s in _scifferContext.REF_STATE on p.billing_state_id equals s.STATE_ID
                         join cc in _scifferContext.REF_COUNTRY on s.COUNTRY_ID equals cc.COUNTRY_ID
                         join v in _scifferContext.REF_VENDOR on p.vendor_id equals v.VENDOR_ID
                         join plant in _scifferContext.REF_PLANT on p.plant_id equals plant.PLANT_ID
                         join c in _scifferContext.ref_document_numbring on p.category_id equals c.document_numbring_id
                         join f in _scifferContext.REF_FREIGHT_TERMS on p.freight_terms_id equals f.FREIGHT_TERMS_ID
                         join pay in _scifferContext.REF_PAYMENT_TERMS on p.payment_terms_id equals pay.payment_terms_id
                         join gst in _scifferContext.ref_gst_customer_type on v.gst_vendor_type_id equals gst.gst_customer_type_id into j1
                         join cur in _scifferContext.REF_CURRENCY on p.gross_value_currency_id equals cur.CURRENCY_ID
                         from gs in j1.DefaultIfEmpty()
                         from co in _scifferContext.REF_COMPANY
                         select new
                         {
                             vendor_id = p.vendor_id,
                             po_id = p.po_id,
                             po_no = p.po_no,
                             po_date = p.po_date,
                             created_ts = p.created_ts,
                             vendor_name = p.vendor_name,
                             // podate=DateTime.Parse(p.po_date.ToString()).ToString("dd-MM-yyyy"),
                             vendor_address = p.billing_address,
                             vendor_city = p.billing_city,
                             vendor_cst_no = p.cst_tin_no,
                             vendor_ecc_no = p.ecc_no,
                             vendor_pan_no = p.pan_no,
                             vendor_pin_code = p.billing_pin_code,
                             vendor_state = s.STATE_NAME,
                             vendor_country = cc.COUNTRY_NAME,
                             company_address = co.REGISTERED_ADDRESS,
                             company_cin_no = co.CIN_NO,
                             company_city = co.REGISTERED_CITY,
                             company_country = co.REF_STATE.REF_COUNTRY.COUNTRY_NAME,
                             company_display_name = co.COMPANY_DISPLAY_NAME,
                             company_email = co.REGISTERED_EMAIL,
                             company_pan_no = co.PAN_NO,
                             company_pincode = co.registered_pincode,
                             company_state = co.REF_STATE.STATE_NAME,
                             company_tan_no = co.TAN_NO,
                             company_telephone = co.REGISTERED_TELEPHONE.ToString(),
                             company_website = co.WEBSITE,
                             compnay_name = co.COMPANY_NAME,
                             company_corporate_address = co.CORPORATE_ADDRESS,
                             company_corporate_city = co.CORPORATE_CITY,
                             company_corporate_country = co.REF_STATE1.REF_COUNTRY.COUNTRY_NAME,
                             company_corporate_email = co.REGISTERED_EMAIL,
                             company_corporate_pincode = co.corporate_pincode,
                             company_corporate_state = co.REF_STATE1.STATE_NAME,
                             company_corporate_telephone = co.CORPORATE_TELEPHONE.ToString(),
                             company_corporate_website = co.WEBSITE,
                             plant_commissionarate = plant.excise_commisionerate,
                             excisable_commidity = plant.excise_commisionerate,
                             plant_division = plant.excise_division,
                             plant_name = plant.PLANT_CODE + ": " + plant.PLANT_ADDRESS + " , " + plant.PLANT_CITY + "-" + plant.pincode + " , " + plant.REF_STATE.STATE_NAME + " - " + plant.REF_STATE.REF_COUNTRY.COUNTRY_NAME,
                             plant_range = plant.excise_range,
                             company_logo = co.LOGO,
                             freight_terms_name = f.FREIGHT_TERMS_NAME,
                             plant_vat = plant.vat_number,
                             plant_cst = plant.cst_number,
                             plant_ECC = plant.excise_number,
                             vendor_quotationdate = p.vendor_quotation_date,
                             vendor_quotation_refNo = p.vendor_quotation_no,
                             remarks_ondocument = p.remarks_on_document,
                             vendor_code = v.VENDOR_CODE,
                             plant_service_tax_no = plant.service_tax_number,
                             payment_term = pay.payment_terms_description,
                             vendor_email = p.email_id,
                             vendor_phone = p.mobile_number == null ? "" : p.mobile_number,
                             item_service_id = p.item_service_id,
                             delivery_type_id = p.delivery_type_id,
                             plant_gst_no = plant.gst_number,
                             vendor_gst_no = v.gst_no,
                             currency = cur.CURRENCY_NAME,
                             gst_vendor_type = gs == null ? "" : gs.gst_customer_type_name,
                             plant_email = plant.PLANT_EMAIL,
                             COUNTRY_NAME = cc.COUNTRY_NAME,
                             billing_pin_code = p.billing_pin_code,
                             net_value = p.net_value,
                             with_without_service_id = p.with_without_service_id,
                             po_verson = p.version.ToString(),
                             approval_status = p.approval_status,
                             PLANT_TELEPHONE = plant.PLANT_TELEPHONE,
                             plant_code = plant.PLANT_CODE,
                         }).ToList().Select(a => new pur_po_report_vm()
                         {
                             vendor_id = a.vendor_id,
                             po_id = a.po_id,
                             po_no = a.po_no,
                             po_date = a.po_date,
                             vendor_name = a.vendor_name,
                             podate = DateTime.Parse(a.po_date.ToString()).ToString("dd-MM-yyyy"),
                             createdts = DateTime.Parse(a.created_ts.ToString()).ToString("dd-MM-yyyy"),
                             vendor_address = a.vendor_address,
                             vendor_city = a.vendor_city,
                             vendor_cst_no = a.vendor_cst_no,
                             vendor_ecc_no = a.vendor_ecc_no,
                             vendor_pan_no = a.vendor_pan_no,
                             vendor_pin_code = a.vendor_pin_code,
                             vendor_state = a.vendor_state,
                             vendor_country = a.vendor_country,
                             company_address = a.company_address,
                             company_cin_no = a.company_cin_no,
                             company_city = a.company_city,
                             company_country = a.company_country,
                             company_display_name = a.company_display_name,
                             company_email = a.company_email,
                             company_pan_no = a.company_pan_no,
                             company_pincode = a.company_pincode,
                             company_state = a.company_state,
                             company_tan_no = a.company_tan_no,
                             company_telephone = a.company_telephone,
                             company_website = a.company_website,
                             compnay_name = a.compnay_name,
                             company_corporate_address = a.company_corporate_address,
                             company_corporate_city = a.company_corporate_city,
                             company_corporate_country = a.company_corporate_country,
                             company_corporate_email = a.company_corporate_email,
                             company_corporate_pincode = a.company_corporate_pincode,
                             company_corporate_state = a.company_corporate_state,
                             company_corporate_telephone = a.company_corporate_telephone,
                             company_corporate_website = a.company_corporate_website,
                             plant_commissionarate = a.plant_commissionarate,
                             plant_division = a.plant_division,
                             plant_name = a.plant_name.ToUpper(),
                             plant_range = a.plant_range,
                             company_logo = a.company_logo,
                             plant_vat = a.plant_vat,
                             plant_cst = a.plant_cst,
                             plant_ECC = a.plant_ECC,
                             vendor_code = a.vendor_code,
                             vendor_quotationdate = a.vendor_quotationdate == null ? string.Empty : DateTime.Parse(a.vendor_quotationdate.ToString()).ToString("dd-MM-yyyy"),
                             vendor_quotation_refNo = a.vendor_quotation_refNo,
                             remarks_ondocument = a.remarks_ondocument,
                             plant_service_tax_no = a.plant_service_tax_no,
                             freight_terms_name = a.freight_terms_name,
                             excisable_commidity = a.excisable_commidity,
                             payment_term = a.payment_term,
                             vendor_email = a.vendor_email,
                             vendor_phone = a.vendor_phone,
                             item_service_id = a.item_service_id,
                             delivery_type_id = a.delivery_type_id,
                             plant_gst_no = a.plant_gst_no,
                             vendor_gst_no = a.vendor_gst_no,
                             gst_vendor_type = a.gst_vendor_type,
                             currency = a.currency,
                             plant_email = "Email : " + a.plant_email,
                             COUNTRY_NAME = a.COUNTRY_NAME,
                             billing_pin_code = a.billing_pin_code,
                             net_value = a.net_value,
                             with_without_service_id = a.with_without_service_id,
                             po_verson = a.po_verson == "1" ? string.Empty : a.po_verson + " / dated  " + DateTime.Parse(a.created_ts.ToString()).ToString("dd-MM-yyyy"),
                             approval_status = a.approval_status,
                             PLANT_TELEPHONE = "Phone : +91 831 " + a.PLANT_TELEPHONE,
                             plant_code = a.plant_code,
                         }).FirstOrDefault();



            return query;
        }

        public List<pur_po_detail_report_vm> GetPOProductForPurchaseRetyurnReport(int id, string ent)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", ent);
            var val = _scifferContext.Database.SqlQuery<pur_po_detail_report_vm>(
            "exec getpodetailforpurchasereturn @entity,@id", entity, quotation_id).ToList();
            return val;
        }

        public pur_pi_return_vm GetPurchaseReturnheaderReport(int id)
        {
            var pur_return_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getheader");
            var val = _scifferContext.Database.SqlQuery<pur_pi_return_vm>(
            "exec get_purchase_return @entity,@id", entity, pur_return_id).FirstOrDefault();
            return val;
        }

        public List<pur_pi_return_detail_vm> GetPurchaseReturnDetailsForReport(int id)
        {
            var pur_return_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getdetail");
            var val = _scifferContext.Database.SqlQuery<pur_pi_return_detail_vm>(
            "exec get_purchase_return @entity,@id", entity, pur_return_id).ToList();
            return val;
        }

        public List<pur_pi_return_vm> GetAll()
        {
            var query = (from ed in _scifferContext.pur_pi_return.Where(x => x.is_active == true)
                         join pycy in _scifferContext.REF_PAYMENT_CYCLE on ed.payment_cycle_id equals pycy.PAYMENT_CYCLE_ID
                         join state in _scifferContext.REF_STATE on ed.billing_state_id equals state.STATE_ID
                         join country in _scifferContext.REF_COUNTRY on state.COUNTRY_ID equals country.COUNTRY_ID
                         join cat in _scifferContext.ref_document_numbring on ed.category_id equals cat.document_numbring_id
                         join frei in _scifferContext.REF_FREIGHT_TERMS on ed.freight_terms_id equals frei.FREIGHT_TERMS_ID
                         join curr_n in _scifferContext.REF_CURRENCY on ed.net_currency_id equals curr_n.CURRENCY_ID
                         join curr_g in _scifferContext.REF_CURRENCY on ed.gross_currency_id equals curr_g.CURRENCY_ID
                         join pyterm in _scifferContext.REF_PAYMENT_TERMS on ed.payment_terms_id equals pyterm.payment_terms_id
                         join pycycle in _scifferContext.REF_PAYMENT_CYCLE on ed.payment_cycle_id equals pycycle.PAYMENT_CYCLE_ID
                         join ven in _scifferContext.REF_VENDOR on ed.vendor_id equals ven.VENDOR_ID
                         join plant in _scifferContext.REF_PLANT on ed.plant_id equals plant.PLANT_ID
                         join business in _scifferContext.REF_BUSINESS_UNIT on ed.business_unit_id equals business.BUSINESS_UNIT_ID
                         join form in _scifferContext.pur_pi_return_form on ed.pi_return_id equals form.pi_return_id into form1
                         from form2 in form1.DefaultIfEmpty()
                         join tds_code in _scifferContext.ref_tds_code on ed.tds_code_id equals tds_code.tds_code_id into tds_code1
                         from tds_code2 in tds_code1.DefaultIfEmpty()
                         select new pur_pi_return_vm
                         {
                             pi_return_id = ed.pi_return_id,
                             document_no = ed.document_no,
                             category_name = cat.category,
                             posting_date = ed.posting_date,
                             vendor_name = ven.VENDOR_NAME,
                             vendor_code = ven.VENDOR_CODE,
                             net_value = ed.net_value,
                             net_currency_name = curr_n.CURRENCY_NAME,
                             gross_value = ed.gross_value,
                             gross_currency_name = curr_g.CURRENCY_NAME,
                             business_name = business.BUSINESS_UNIT_NAME,
                             business_desc = business.BUSINESS_UNIT_DESCRIPTION,
                             plant_desc = plant.PLANT_NAME,
                             plant_code = plant.PLANT_CODE,
                             freight_term = frei.FREIGHT_TERMS_NAME,
                             form_name = form2.REF_FORM.FORM_NAME,
                             billing_address = ed.billing_address,
                             billing_city = ed.billing_city,
                             billing_pincode = ed.billing_pincode,
                             country_name = country.COUNTRY_NAME,
                             state_name = state.STATE_NAME,
                             email_id = ed.email_id,
                             payment_cycle_type_name = pycy.REF_PAYMENT_CYCLE_TYPE.PAYMENT_CYCLE_TYPE_NAME,
                             payment_cycle_name = pycycle.PAYMENT_CYCLE_NAME,
                             payment_term_name = pyterm.payment_terms_description,
                             gst_no = ed.gst_no,
                             service_tax_no = ed.service_tax_no,
                             tds_code_name = tds_code2.tds_code,
                             internal_remarks = ed.internal_remarks,
                             remarks_on_doc = ed.remarks_on_doc,
                             attachment = ed.attachment,
                             created_by = ed.created_by,
                             created_ts = ed.created_ts,
                         }).OrderByDescending(a => a.pi_return_id).ToList();
            return query;
        }
        public List<pur_pi_return_detail_vm> GetPiListForPI_return(int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getpiforpireturn");
            var val = _scifferContext.Database.SqlQuery<pur_pi_return_detail_vm>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;
        }
        public double forPurchaseReturnPI(int pi_id, int item_id, int bucket_id, int storage_location_id, int plant_id)
        {
            var pi_id1 = new SqlParameter("@pi_id", pi_id);
            var item_id1 = new SqlParameter("@item_id", item_id);
            var bucket_id1 = new SqlParameter("@bucket_id", bucket_id);
            var storage_location_id1 = new SqlParameter("@storage_location_id", storage_location_id);
            var plant_id1 = new SqlParameter("@plant_id", plant_id);
            var val = _scifferContext.Database.SqlQuery<double>(
            "exec forPurchaseReturnPI @pi_id,@item_id,@bucket_id,@storage_location_id,@plant_id", pi_id1, item_id1, bucket_id1, storage_location_id1, plant_id1).FirstOrDefault();
            return val;
        }
        public pur_pi_VM GetPIDetailForReport(int id)
        {
            var query = (from ed in _scifferContext.pur_pi.Where(x => x.is_active == true && x.pi_id == id)
                         join state in _scifferContext.REF_STATE on ed.billing_state_id equals state.STATE_ID
                         join country in _scifferContext.REF_COUNTRY on state.COUNTRY_ID equals country.COUNTRY_ID
                         join cat in _scifferContext.ref_document_numbring on ed.category_id equals cat.document_numbring_id
                         join frei in _scifferContext.REF_FREIGHT_TERMS on ed.freight_terms_id equals frei.FREIGHT_TERMS_ID
                         join curr in _scifferContext.REF_CURRENCY on ed.gross_currency_id equals curr.CURRENCY_ID
                         join pyterm in _scifferContext.REF_PAYMENT_TERMS on ed.payment_terms_id equals pyterm.payment_terms_id
                         join pycycle in _scifferContext.REF_PAYMENT_CYCLE on ed.payment_cycle_id equals pycycle.PAYMENT_CYCLE_ID
                         join ven in _scifferContext.REF_VENDOR on ed.vendor_id equals ven.VENDOR_ID
                         join plant in _scifferContext.REF_PLANT on ed.plant_id equals plant.PLANT_ID
                         join po in _scifferContext.pur_po on ed.po_id equals po.po_id into j1
                         from poo in j1.DefaultIfEmpty()
                         select new pur_pi_VM
                         {
                             vendor_name = ven.VENDOR_NAME,
                             plant_id = ed.plant_id,
                             plant_name = plant.PLANT_NAME,
                             billing_address = ed.billing_address,
                             billing_city = ed.billing_city,
                             billing_pincode = ed.billing_pincode,
                             billing_state_id = ed.billing_state_id,
                             state_name = state.STATE_NAME,
                             country_id = state.COUNTRY_ID,
                             country_name = country.COUNTRY_NAME,
                             business_unit_id = ed.business_unit_id,
                             category_id = ed.category_id,
                             category_name = cat.category,
                             gross_value = ed.gross_value,
                             item_service_id = ed.item_service_id,
                             item_service_name = ed.item_service_id == 1 ? "Item" : "Service",
                             net_currency_id = ed.net_currency_id,
                             net_currency_name = curr.CURRENCY_NAME,
                             net_value = ed.net_value,
                             created_by = ed.created_by,
                             created_ts = ed.created_ts,
                             cst_tin_no = ed.cst_tin_no,
                             delivery_date = ed.delivery_date,
                             document_no = ed.document_no,
                             ecc_no = ed.ecc_no,
                             email_id = ed.email_id,
                             freight_terms_id = ed.freight_terms_id,
                             freight_term = frei.FREIGHT_TERMS_NAME,
                             gate_entry_date = ed.gate_entry_date,
                             gate_entry_no = ed.gate_entry_no,
                             gross_currency_id = ed.gross_currency_id,
                             gross_currency_name = curr.CURRENCY_NAME,
                             pan_no = ed.pan_no,
                             payment_cycle_id = ed.payment_cycle_id,
                             payment_cycle_name = pycycle.PAYMENT_CYCLE_NAME,
                             payment_terms_id = ed.payment_terms_id,
                             payment_term_name = pyterm.payment_terms_description,
                             payment_cycle_type_name = pycycle.REF_PAYMENT_CYCLE_TYPE.PAYMENT_CYCLE_TYPE_NAME,
                             pi_id = ed.pi_id,
                             posting_date = ed.posting_date,
                             po_id = ed.po_id,
                             po_no = poo == null ? string.Empty : poo.po_no,
                             vat_tin_no = ed.vat_tin_no,
                             vendor_document_date = ed.vendor_document_date,
                             vendor_document_no = ed.vendor_document_no,
                             vendor_id = ed.vendor_id,
                             tds_code_id = ed.tds_code_id,

                         }).FirstOrDefault();
            return query;
        }
        public List<pi_detail_vm> GetPIProductDetailForPI(int id)
        {
            var query = (from sd in _scifferContext.pur_pi_detail.Where(x => x.pi_id == id && x.is_active == true)
                         join i in _scifferContext.REF_ITEM on sd.item_id equals i.ITEM_ID
                         join u in _scifferContext.REF_UOM on sd.uom_id equals u.UOM_ID
                         select new pi_detail_vm
                         {
                             assesable_rate = sd.assessable_rate,
                             assesable_value = sd.assessable_value,
                             discount = sd.discount,
                             eff_unit_price = sd.eff_unit_price,
                             item_code = i.ITEM_CODE,
                             item_id = i.ITEM_ID,
                             item_name = i.ITEM_NAME,
                             quantity = sd.quantity,
                             purchase_value = sd.purchase_value,
                             grn_detail_id = (int)sd.grn_detail_id,
                             pi_id = sd.pi_id,
                             tax_id = sd.tax_id,
                             unit_price = sd.unit_price,
                             uom_id = sd.uom_id,
                             uom_name = u.UOM_NAME,
                         }).ToList();
            return query;
        }

        public bool Update(pur_pi_return_vm Purchase)
        {
            throw new NotImplementedException();
        }
        public List<pur_pi_VM> GetPiforPiReturn(int vendor_id)
        {
            var query = (from pi in _scifferContext.pur_pi.Where(x => x.is_active == true && x.vendor_id == vendor_id && x.item_service_id == 1)
                         select new
                         {
                             pi_id = pi.pi_id,
                             pi_date = pi.posting_date,
                             pi_no = pi.document_no,

                         }).Distinct().ToList().Select(a => new pur_pi_VM
                         {
                             pi_id = a.pi_id,
                             document_no = a.pi_no + " / " + DateTime.Parse(a.pi_date.ToString()).ToString("dd/MMM/yyyy"),
                         }).Distinct().ToList();
            return query;
        }
        public pur_pi_VM PiforPireturn(int id)
        {
            var query = (from ed in _scifferContext.pur_pi.Where(x => x.is_active == true && x.pi_id == id)
                         join form in _scifferContext.pur_pi_form on ed.pi_id equals form.pi_id into form1
                         from form2 in form1.DefaultIfEmpty()
                         select new pur_pi_VM
                         {
                             pi_id = ed.pi_id,
                             po_no = ed.document_no,
                             document_no = ed.document_no,
                             category_id = ed.category_id,
                             posting_date = ed.posting_date,
                             vendor_id = ed.vendor_id,
                             net_value = ed.net_value,
                             net_currency_id = ed.net_currency_id,
                             gross_value = ed.gross_value,
                             gross_currency_id = ed.gross_currency_id,
                             business_unit_id = ed.business_unit_id,
                             plant_id = ed.plant_id,
                             freight_terms_id = ed.freight_terms_id,
                             delivery_date = ed.delivery_date,
                             vendor_document_no = ed.vendor_document_no,
                             vendor_document_date = ed.vendor_document_date,
                             form_id = form2.form_id,
                             billing_address = ed.billing_address,
                             billing_city = ed.billing_city,
                             billing_pincode = ed.billing_pincode,
                             billing_state_id = ed.billing_state_id,
                             email_id = ed.email_id,
                             payment_cycle_id = ed.payment_cycle_id,
                             payment_cycle_type_id = ed.REF_PAYMENT_CYCLE.PAYMENT_CYCLE_TYPE_ID,
                             payment_terms_id = ed.payment_terms_id,
                             gst_no = ed.gst_no,
                             pan_no = ed.pan_no,
                             ecc_no = ed.ecc_no,
                             vat_tin_no = ed.vat_tin_no,
                             cst_tin_no = ed.cst_tin_no,
                             service_tax_no = ed.service_tax_no,
                             tds_code_id = ed.tds_code_id,
                             internal_remarks = ed.internal_remarks,
                             remarks_on_doc = ed.remarks_on_doc,
                             attachment = ed.attachment,
                             created_by = ed.created_by,
                             created_ts = ed.created_ts,
                             country_id = ed.REF_STATE.COUNTRY_ID,
                         }).FirstOrDefault();
            return query;
        }
        public List<pur_po_detail_vm> GetPOList(int id)
        {
            throw new NotImplementedException();
        }

        public List<pur_pi_return_detail_vm> GetPurchasereturnDetail(string entity, int buyer_id, int plant_id, string item_id, string sloc_id, string bucket_id, int pi_id)
        {
            try
            {
                var entityid = new SqlParameter("@entity", entity);
                var buyerid = new SqlParameter("@buyer_id", buyer_id);
                var plantid = new SqlParameter("@plant_id", plant_id);
                var itemid = new SqlParameter("@item_id", item_id);
                var slocid = new SqlParameter("@sloc_id", sloc_id);
                var bucketid = new SqlParameter("@bucket_id", bucket_id);
                var piid = new SqlParameter("@pi_id", pi_id);
                var val = _scifferContext.Database.SqlQuery<pur_pi_return_detail_vm>(
                "exec GetPurchaseReturnDetails @entity,@buyer_id,@plant_id,@item_id,@sloc_id,@bucket_id,@pi_id", entityid, buyerid, plantid, itemid, slocid, bucketid, piid).ToList();
                return val;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string Delete(int id, string cancellation_remarks, int reason_id)
        {
            try
            {
                int create_user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var pi_return_id = new SqlParameter("@id", id);
                var remarks = new SqlParameter("@cancellation_remarks", cancellation_remarks == null ? "" : cancellation_remarks);
                var created_by = new SqlParameter("@created_by", create_user);
                var cancellation_reason_id = new SqlParameter("@cancellation_reason_id", reason_id);
                var val = _scifferContext.Database.SqlQuery<string>(
                  "exec cancel_purchase_return @id ,@cancellation_remarks ,@created_by,@cancellation_reason_id", pi_return_id, remarks, created_by, cancellation_reason_id).FirstOrDefault();
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
