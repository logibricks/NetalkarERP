using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System.Data.SqlClient;
using Sciffer.Erp.Data;
using System.Data;
using AutoMapper;
using System.Web;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Sciffer.Erp.Service.Implementation
{
    public class PurchaseInvoiceService : IPurchaseInvoiceService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _generic;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        public PurchaseInvoiceService(ScifferContext scifferContext, IGenericService generic)
        {
            _scifferContext = scifferContext;
            _generic = generic;
        }
        public string Add(pur_pi_VM Purchase)
        {
            var FileAttachment = "";
            if (Purchase.FileUpload != null)
            {
                FileAttachment = _generic.GetFilePath("PurchaseInvoice", Purchase.FileUpload);
            }
            try
            {
                DataTable dt = new DataTable();
                DateTime dte = new DateTime(1990, 1, 1);
                dt.Columns.Add("pi_detail_id", typeof(int));
                dt.Columns.Add("item_id", typeof(int));
                dt.Columns.Add("user_description", typeof(string));
                dt.Columns.Add("delivery_date", typeof(DateTime));
                dt.Columns.Add("storage_loaction_id", typeof(int));
                dt.Columns.Add("quantity", typeof(double));
                dt.Columns.Add("uom_id", typeof(int));
                dt.Columns.Add("unit_price", typeof(double));
                dt.Columns.Add("discount", typeof(double));
                dt.Columns.Add("eff_unit_price", typeof(double));
                dt.Columns.Add("purchase_value", typeof(double));
                dt.Columns.Add("assessable_rate", typeof(double));
                dt.Columns.Add("assessable_value", typeof(double));
                dt.Columns.Add("tax_id", typeof(int));
                dt.Columns.Add("cost_center_id", typeof(int));
                dt.Columns.Add("grn_detail_id", typeof(int));
                dt.Columns.Add("grir_value", typeof(double));
                dt.Columns.Add("basic_value", typeof(double));
                dt.Columns.Add("sac_hsn_id", typeof(int));
                dt.Columns.Add("item_type_id", typeof(int));
                if (Purchase.items!=null)
                {                
                for (var i = 0; i < Purchase.items.Count; i++)
                {
                    if (Purchase.items[i] != "")
                    {
                            var item_type_id = Purchase.item_type_id[i] == "" ? 0 : Purchase.item_type_id[i] == "NaN" ? 0 : Convert.ToInt32(Purchase.item_type_id[i]);
                            dt.Rows.Add(Purchase.pi_id == 0 ? -1 : Purchase.pi_id == null ? -1 : Convert.ToInt32(Purchase.pi_detail_id[i]), Convert.ToInt32(Purchase.items[i]), Purchase.user_description[i], Purchase.delivery_date1[i] == "" ? dte : Convert.ToDateTime(Purchase.delivery_date1[i]),
                                            Purchase.storage_loaction_id[i] == "" ? 0 : Convert.ToInt32(Purchase.storage_loaction_id[i]), Purchase.quantity[i] == "" ? 0 : Purchase.quantity[i] == "NaN" ? 0 : Convert.ToDouble(Purchase.quantity[i]), Purchase.uom_id[i] == "" ? 0 : Convert.ToInt32(Purchase.uom_id[i]),
                                            Purchase.unit_price[i] == "" ? 0 : Convert.ToDouble(Purchase.unit_price[i]), Purchase.discount[i] == "" ? 0 : Convert.ToDouble(Purchase.discount[i]), Purchase.eff_unit_price[i] == "" ? 0 : Convert.ToDouble(Purchase.eff_unit_price[i]),
                                            Purchase.purchase_value[i] == "" ? 0 : Convert.ToDouble(Purchase.purchase_value[i]), Purchase.eff_unit_price[i] == "" ? 0 : Convert.ToDouble(Purchase.eff_unit_price[i]), Purchase.purchase_value[i] == "" ? 0 : Convert.ToDouble(Purchase.purchase_value[i]),
                                            Purchase.tax_id[i] == "" ? 0 : Convert.ToInt32(Purchase.tax_id[i]), Purchase.cost_center_id[i] == "" ? 0 : Convert.ToInt32(Purchase.cost_center_id[i]), Purchase.grn_detail_id[i] == "" ? 0 : Convert.ToInt32(Purchase.grn_detail_id[i]),
                                            Purchase.grir_value[i] == "" ? 0 : Convert.ToDouble(Purchase.grir_value[i]), Purchase.basic_value[i] == "" ? 0 : Convert.ToDouble(Purchase.basic_value[i]), Purchase.sac_hsn_id[i], item_type_id);
                    }

                }
                }
                Purchase.created_by = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var pi_id = new SqlParameter("@pi_id", Purchase.pi_id == null ? -1 : Purchase.pi_id);
                var po_id = new SqlParameter("@po_id", Purchase.po_id == null ? 0 : Purchase.po_id);
                var document_no = new SqlParameter("@document_no", Purchase.document_no == null ? "" : Purchase.document_no);
                var category_id = new SqlParameter("@category_id", Purchase.category_id);
                var posting_date = new SqlParameter("@posting_date", Purchase.posting_date);
                var vendor_id = new SqlParameter("@vendor_id", Purchase.vendor_id);
                var net_value = new SqlParameter("@net_value", Purchase.net_value);
                var net_currency_id = new SqlParameter("@net_currency_id", Purchase.net_currency_id);
                var gross_value = new SqlParameter("@gross_value", Purchase.gross_value);
                var gross_currency_id = new SqlParameter("@gross_currency_id", Purchase.gross_currency_id);
                var item_service = new SqlParameter("@item_service", Purchase.item_service_id);
                var business_unit_id = new SqlParameter("@business_unit_id", Purchase.business_unit_id);
                var plant_id = new SqlParameter("@plant_id", Purchase.plant_id);
                var freight_terms_id = new SqlParameter("@freight_terms_id", Purchase.freight_terms_id);
                var delivery_date = new SqlParameter("@delivery_date", Purchase.delivery_date == null ? dte : Purchase.delivery_date);
                var vendor_document_no = new SqlParameter("@vendor_document_no", Purchase.vendor_document_no == null ? string.Empty : Purchase.vendor_document_no);
                var vendor_document_date = new SqlParameter("@vendor_document_date", Purchase.vendor_document_date == null ? dte : Purchase.vendor_document_date);
                var gate_entry_no = new SqlParameter("@gate_entry_no", Purchase.gate_entry_no == null ? string.Empty : Purchase.gate_entry_no);
                var gate_entry_date = new SqlParameter("@gate_entry_date", dte);
                var created_by = new SqlParameter("@created_by", Purchase.created_by);
                var billing_address = new SqlParameter("@billing_address", Purchase.billing_address);
                var billing_city = new SqlParameter("@billing_city", Purchase.billing_city);
                var billing_state_id = new SqlParameter("@billing_state_id", Purchase.billing_state_id);
                var billing_pincode = new SqlParameter("@billing_pincode", Purchase.billing_pincode==null?string.Empty:Purchase.billing_pincode);
                var email_id = new SqlParameter("@email_id", Purchase.email_id == null ? string.Empty : Purchase.email_id);
                var payment_terms_id = new SqlParameter("@payment_terms_id", Purchase.payment_terms_id);
                var payment_cycle_id = new SqlParameter("@payment_cycle_id", Purchase.payment_cycle_id);
                var pan_no = new SqlParameter("@pan_no", Purchase.pan_no==null?string.Empty:Purchase.pan_no);
                var ecc_no = new SqlParameter("@ecc_no", Purchase.ecc_no == null ? string.Empty : Purchase.ecc_no);
                var vat_tin_no = new SqlParameter("@vat_tin_no", Purchase.vat_tin_no == null ? string.Empty : Purchase.vat_tin_no);
                var cst_tin_no = new SqlParameter("@cst_tin_no", Purchase.cst_tin_no == null ? string.Empty : Purchase.cst_tin_no);
                var internal_remarks = new SqlParameter("@internal_remarks", Purchase.internal_remarks == null ? string.Empty : Purchase.internal_remarks);
                var remarks_on_doc = new SqlParameter("@remarks_on_doc", Purchase.remarks_on_doc == null ? string.Empty : Purchase.remarks_on_doc);
                var attachment = new SqlParameter("@attachment", FileAttachment);
                var tds_code_id = new SqlParameter("@tds_code_id", Purchase.tds_code_id == null ? 0 : Purchase.tds_code_id);
                var gst_no = new SqlParameter("@gst_no", Purchase.gst_no == null ? string.Empty : Purchase.gst_no);
                var service_tax_no = new SqlParameter("@service_tax_no", Purchase.service_tax_no == null ? string.Empty : Purchase.service_tax_no);
                var form_id = new SqlParameter("@form_id", Purchase.form_id == null ? 0 : Purchase.form_id);
                var round_off = new SqlParameter("@round_off", Purchase.round_off == null ? 0 : Purchase.round_off); 
                var is_hold_payment = new SqlParameter("@is_hold_payment", Purchase.is_hold_payment);
                var place_of_supply_id = new SqlParameter("@place_of_supply_id", Purchase.place_of_supply_id);
                var gst_vendor_type_id = new SqlParameter("@gst_vendor_type_id", Purchase.gst_vendor_type_id);
                var gst_in = new SqlParameter("@gst_in", Purchase.gst_in);
                var is_rcm = new SqlParameter("@is_rcm", Purchase.is_rcm);
                var tcs_amount = new SqlParameter("@tcs_amount", Purchase.tcs_amount == null ? 0 : Purchase.tcs_amount);
                var grn_status_id = new SqlParameter("@grn_status_id", Purchase.status_id == null ? 0 : Purchase.status_id);
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_pur_pi_detail";
                t1.Value = dt;
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_purchase_invoice @pi_id ,@po_id ,@document_no ,@category_id ,@posting_date ,@vendor_id ,@net_value ,@net_currency_id ,@gross_value ,@gross_currency_id ,@item_service ,@business_unit_id ,@plant_id ,@freight_terms_id ,@delivery_date ,@vendor_document_no ,@vendor_document_date ,@gate_entry_no ,@gate_entry_date ,@created_by ,@billing_address ,@billing_city ,@billing_state_id ,@billing_pincode ,@email_id ,@payment_terms_id ,@payment_cycle_id ,@pan_no ,@ecc_no ,@vat_tin_no ,@cst_tin_no ,@internal_remarks ,@remarks_on_doc ,@attachment ,@tds_code_id ,@gst_no ,@service_tax_no,@form_id,@round_off,@is_hold_payment,@place_of_supply_id,@gst_vendor_type_id,@gst_in,@is_rcm,@t1,@tcs_amount,@grn_status_id",
                    pi_id, po_id, document_no, category_id, posting_date, vendor_id, net_value, net_currency_id, gross_value,
                    gross_currency_id, item_service, business_unit_id, plant_id, freight_terms_id, delivery_date, vendor_document_no,
                    vendor_document_date, gate_entry_no, gate_entry_date, created_by, billing_address, billing_city, billing_state_id,
                    billing_pincode, email_id, payment_terms_id, payment_cycle_id, pan_no, ecc_no, vat_tin_no, cst_tin_no,
                    internal_remarks, remarks_on_doc, attachment, tds_code_id, gst_no, service_tax_no, form_id, round_off, is_hold_payment, place_of_supply_id,
                    gst_vendor_type_id, gst_in, is_rcm, t1, tcs_amount, grn_status_id).FirstOrDefault();

                if (val.Contains("Saved"))
                {
                    if (Purchase.FileUpload != null)
                    {
                        Purchase.FileUpload.SaveAs(Purchase.attachment);
                    }
                    return val;
                }
                else
                {
                    log4net.GlobalContext.Properties["user"] = Purchase.created_by;
                    log.Info(val);
                    System.IO.File.Delete(Purchase.attachment);
                    return val;
                }
            }
            catch (Exception ex)
            {
                if (FileAttachment != "")
                {
                    System.IO.File.Delete(FileAttachment);
                }
                //--------------Log4Net
                log4net.GlobalContext.Properties["user"] = Purchase.created_by;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "Error : " + ex.Message;
                //return ex.Message;
            }
        }

        public pur_pi_VM Get(int? id)
        {
            pur_pi purchase = _scifferContext.pur_pi.FirstOrDefault(c => c.pi_id == id);
            purchase.tcs_amount=purchase.tcs_amount == null ? 0 : purchase.tcs_amount;
            Mapper.CreateMap<pur_pi, pur_pi_VM>();
            pur_pi_VM purchasevm = Mapper.Map<pur_pi, pur_pi_VM>(purchase);
            purchasevm.pur_pi_detail = purchasevm.pur_pi_detail.Where(c => c.is_active == true).ToList();
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
            var pi_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getpidetail");
            var val = _scifferContext.Database.SqlQuery<pi_detail_vm>(
                   "exec get_purchase_order @entity ,@id", entity, pi_id).ToList();
            purchasevm.pi_detail_vm = val;
            return purchasevm;
        }

        public pur_pi_VM GetPIDetailForReport(int id)
        {
            var query = (from ed in _scifferContext.pur_pi.Where(x => x.is_active == true && x.pi_id == id)
                             //join pycy in _scifferContext.REF_PAYMENT_CYCLE_TYPE on ed.payment_cycle_id equals pycy.PAYMENT_CYCLE_TYPE_ID
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
        public List<pur_pi_VM> GetAll()
        {
            var query = (from ed in _scifferContext.pur_pi.Where(x => x.is_active == true)
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
                         join po in _scifferContext.pur_po on ed.po_id equals po.po_id into j1
                         from poo in j1.DefaultIfEmpty()
                         join business in _scifferContext.REF_BUSINESS_UNIT on ed.business_unit_id equals business.BUSINESS_UNIT_ID
                         join form in _scifferContext.pur_pi_form on ed.pi_id equals form.pi_id into form1
                         from form2 in form1.DefaultIfEmpty()
                         join tds_code in _scifferContext.ref_tds_code on ed.tds_code_id equals tds_code.tds_code_id into tds_code1
                         from tds_code2 in tds_code1.DefaultIfEmpty()
                         join d1 in _scifferContext.ref_document_numbring on ed.rcm_category_id equals d1.document_numbring_id into j2
                         from d22 in j2.DefaultIfEmpty()
                         join canr in _scifferContext.ref_cancellation_reason on ed.cancellation_reason_id equals canr.cancellation_reason_id into can
                         from canr1 in can.DefaultIfEmpty()
                         join stat in _scifferContext.ref_status on ed.status_id equals stat.status_id into status
                         from status1 in status.DefaultIfEmpty()
                         select new pur_pi_VM
                         {
                             pi_id = ed.pi_id,
                             po_no = poo == null ? string.Empty : poo.po_no,
                             document_no = ed.document_no,
                             category_name = cat.category,
                             posting_date = ed.posting_date,
                             vendor_name = ven.VENDOR_NAME,
                             vendor_code = ven.VENDOR_CODE,
                             item_service_name = ed.item_service_id == 1 ? "Item" : "Service",
                             net_value = ed.net_value,
                             net_currency_name = curr_n.CURRENCY_NAME,
                             gross_value = ed.gross_value,
                             gross_currency_name = curr_g.CURRENCY_NAME,
                             business_name = business.BUSINESS_UNIT_NAME,
                             business_desc = business.BUSINESS_UNIT_DESCRIPTION,
                             plant_desc = plant.PLANT_NAME,
                             plant_code = plant.PLANT_CODE,
                             freight_term = frei.FREIGHT_TERMS_NAME,
                             delivery_date = ed.delivery_date,
                             vendor_document_no = ed.vendor_document_no,
                             vendor_document_date = ed.vendor_document_date,
                             //gate_entry_no = ed.gate_entry_no,
                             //gate_entry_date = ed.gate_entry_date,
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
                             pan_no = ed.pan_no,
                             ecc_no = ed.ecc_no,
                             vat_tin_no = ed.vat_tin_no,
                             cst_tin_no = ed.cst_tin_no,
                             service_tax_no = ed.service_tax_no,
                             tds_code_name = tds_code2.tds_code,
                             internal_remarks = ed.internal_remarks,
                             remarks_on_doc = ed.remarks_on_doc,
                             attachment = ed.attachment,
                             created_by = ed.created_by,
                             created_ts = ed.created_ts,
                             rcm_category = d22 == null ? "" : d22.category,
                             rcm_document_no = ed.rcm_document_no,
                             cancellation_remarks = ed.cancellation_remarks,
                             cancellation_reason = canr1.cancellation_reason,
                             status_name = status1.status_name
                         }).OrderByDescending(a=>a.pi_id).ToList();
            return query;
        }




        public bool Update(pur_pi_VM Purchase)
        {
            throw new NotImplementedException();
        }
        public List<pur_po_detail_vm> GetPOList(int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getpodetail");
            var val = _scifferContext.Database.SqlQuery<pur_po_detail_vm>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;
        }

        public List<pur_pi_vm_detail> GetGRNListForPI(int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getgrnforpi");
            var val = _scifferContext.Database.SqlQuery<pur_pi_vm_detail>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;
        }
        public pur_pi_report_vm GetPIForReport(int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getrcmheaderlevelforreport");
            var val = _scifferContext.Database.SqlQuery<pur_pi_report_vm>(
            "exec get_purchase_order @entity,@id", entity, quotation_id).FirstOrDefault();
            return val;
        }
        public List<pur_pi_detail_report_vm> GetPIDetailsForReport(int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getrcmdetaillevelforreport");
            var val = _scifferContext.Database.SqlQuery<pur_pi_detail_report_vm>(
            "exec get_purchase_order @entity,@id", entity, quotation_id).ToList();
            return val;
        }
        public string Delete(int id, string cancellation_remarks, int reason_id)
        {
            try
            {
                int create_user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var pi_id = new SqlParameter("@id", id);
                var remarks = new SqlParameter("@cancellation_remarks", cancellation_remarks == null ? "" : cancellation_remarks);
                var created_by = new SqlParameter("@created_by", create_user);
                var cancellation_reason_id = new SqlParameter("@cancellation_reason_id", reason_id);
                var val = _scifferContext.Database.SqlQuery<string>(
                  "exec cancel_purchase_invoice @id ,@cancellation_remarks ,@created_by,@cancellation_reason_id", pi_id, remarks, created_by, cancellation_reason_id).FirstOrDefault();
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
