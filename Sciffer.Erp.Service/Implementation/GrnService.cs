
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
    public class GrnService : IGrnService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericService;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        public GrnService(ScifferContext ScifferContext, IGenericService generric)
        {
            _scifferContext = ScifferContext;
            _genericService = generric;
        }
        public List<pur_grnVM> GetGrnList()
        {
            var query = (from ed in _scifferContext.pur_grn.Where(x => x.is_active == true)

                         select new
                         {
                             grn_id = ed.grn_id,
                             grn_number = ed.grn_number,
                             posting_date = ed.posting_date,
                         }).ToList().Select(a => new pur_grnVM
                         {
                             grn_id = a.grn_id,
                             grn_number = a.grn_number + " - " + DateTime.Parse(a.posting_date.ToString()).ToString("dd/MMM/yyyy"),
                         }).ToList();
            return query;
        }
        public string Add(pur_grnVM GRN)
        {
            try
            {
                var FileAttachment = "";
                if (GRN.FileUpload != null)
                {
                    FileAttachment = _genericService.GetFilePath("GRN", GRN.FileUpload);
                }
                DateTime dte = new DateTime(1990, 1, 1);
                DataTable dt = new DataTable();
                dt.Columns.Add("grn_detail_id", typeof(int));
                dt.Columns.Add("item_id", typeof(int));
                dt.Columns.Add("po_detail_id", typeof(int));
                dt.Columns.Add("delivery_date", typeof(DateTime));
                dt.Columns.Add("storage_location_id", typeof(int));
                dt.Columns.Add("quantity", typeof(double));
                dt.Columns.Add("uom_id", typeof(int));
                dt.Columns.Add("unit_price", typeof(double));
                dt.Columns.Add("discount", typeof(double));
                dt.Columns.Add("eff_unit_price", typeof(double));
                dt.Columns.Add("purchase_value", typeof(double));
                dt.Columns.Add("assessable_rate", typeof(double));
                dt.Columns.Add("assessable_value", typeof(double));
                dt.Columns.Add("tax_id", typeof(int));
                dt.Columns.Add("is_active", typeof(bool));
                dt.Columns.Add("user_description", typeof(string));
                dt.Columns.Add("bucket_id", typeof(int));
                dt.Columns.Add("batch", typeof(string));
                dt.Columns.Add("batch_managed", typeof(bool));
                dt.Columns.Add("expirary_date", typeof(DateTime));
                dt.Columns.Add("sac_hsn_id", typeof(int));
                dt.Columns.Add("po_staggered_delivery_id", typeof(int));
                if (GRN.item_id1 != null)
                {


                    for (var i = 0; i < GRN.item_id1.Count; i++)
                    {
                        int po = -1;
                        if (GRN.item_id1 != null)
                        {
                            po = GRN.grn_detail_id1[i] == "0" ? -1 : GRN.grn_detail_id1[i] == "" ? -1 : Convert.ToInt32(GRN.grn_detail_id1[i]);
                        }

                        var item = int.Parse(GRN.item_id1[i]);
                        var detail = int.Parse(GRN.po_detail_id1[i]);
                        var ddate = DateTime.Parse(GRN.delivery_date1[i]);
                        var sloc = int.Parse(GRN.storage_location_id1[i]);
                        var qty = double.Parse(GRN.quantity1[i]);
                        var uom = int.Parse(GRN.uom_id1[i]);
                        var uprice = double.Parse(GRN.unit_price1[i]);
                        var discount = double.Parse(GRN.discount1[i]);
                        var eprice = double.Parse(GRN.eff_unit_price1[i]);
                        var pvalue = double.Parse(GRN.purchase_value1[i]);
                        var assabblerate = double.Parse(GRN.assessable_rate1[i]);
                        var asablevalue = double.Parse(GRN.assessable_value1[i]);
                        var tax = int.Parse(GRN.tax_id1[i]);
                        var desc = GRN.user_description1 == null ? "" : GRN.user_description1[i] == null ? "" : GRN.user_description1[i];
                        var bucket = int.Parse(GRN.bucket_id1[i]);
                        var batch = GRN.batch1[i];
                        var edate = GRN.expirary_date1[i] == "" ? dte : DateTime.Parse(GRN.expirary_date1[i]);
                        var hsn_id = GRN.sac_hsn_id1[i] == "" ? 0 : Convert.ToInt32(GRN.sac_hsn_id1[i]);
                        var batch_yes_no = GRN.batch_managed1[i] == "false" ? false : true;
                        var del_detail = int.Parse(GRN.po_staggered_delivery_id1[i]);
                        dt.Rows.Add(po, item, detail, ddate, sloc, qty, uom, uprice, discount, eprice, pvalue, assabblerate, asablevalue, tax, 1, desc, bucket, batch, batch_yes_no, edate, hsn_id, del_detail);
                    }
                }
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_pur_grn_detail";
                t1.Value = dt;
                int create_user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var grn_id = new SqlParameter("@grn_id", GRN.grn_id == 0 ? -1 : GRN.grn_id);
                var po_id = new SqlParameter("@po_id", GRN.po_id);
                var grn_number = new SqlParameter("@grn_number", GRN.grn_number == null ? "" : GRN.grn_number);
                var vendor_id = new SqlParameter("@vendor_id", GRN.vendor_id);
                var net_value = new SqlParameter("@net_value", GRN.net_value);
                var category_id = new SqlParameter("@category_id", GRN.category_id);
                var net_currency_id = new SqlParameter("@net_currency_id", GRN.net_currency_id);
                var gross_value = new SqlParameter("@gross_value", GRN.gross_value);
                var gross_currency_id = new SqlParameter("@gross_currency_id", GRN.gross_currency_id);
                var posting_date = new SqlParameter("@posting_date", GRN.posting_date);
                var business_unit_id = new SqlParameter("@business_unit_id", GRN.business_unit_id);
                var plant_id = new SqlParameter("@plant_id", GRN.plant_id);
                var freight_terms_id = new SqlParameter("@freight_terms_id", GRN.freight_terms_id);
                var delivery_date = new SqlParameter("@delivery_date", GRN.delivery_date == null ? dte : GRN.delivery_date);
                var vendor_doc_no = new SqlParameter("@vendor_doc_no", GRN.vendor_doc_no == null ? string.Empty : GRN.vendor_doc_no);
                var vendor_doc_date = new SqlParameter("@vendor_doc_date", GRN.vendor_doc_date);
                var gate_entry_number = new SqlParameter("@gate_entry_number", GRN.gate_entry_number == null ? "" : GRN.gate_entry_number);
                var gate_entry_date = new SqlParameter("@gate_entry_date", GRN.gate_entry_date == null ? dte : GRN.gate_entry_date);
                var created_by = new SqlParameter("@created_by", create_user);
                var billing_address = new SqlParameter("@billing_address", GRN.billing_address == null ? "" : GRN.billing_address);
                var billing_city = new SqlParameter("@billing_city", GRN.billing_city == null ? "" : GRN.billing_city);
                var billing_state_id = new SqlParameter("@billing_state_id", GRN.billing_state_id == null ? 0 : GRN.billing_state_id);
                var billing_pin_code = new SqlParameter("@billing_pin_code", GRN.billing_pin_code == null ? "" : GRN.billing_pin_code);
                var email_id = new SqlParameter("@email_id", GRN.email_id == null ? "" : GRN.email_id);
                var payment_terms_id = new SqlParameter("@payment_terms_id", GRN.payment_terms_id);
                var payment_cycle_id = new SqlParameter("@payment_cycle_id", GRN.payment_cycle_id);
                var status_id = new SqlParameter("@status_id", GRN.status_id);
                var internal_remarks = new SqlParameter("@internal_remarks", GRN.internal_remarks == null ? "" : GRN.internal_remarks);
                var remarks_on_doc = new SqlParameter("@remarks_on_doc", GRN.remarks_on_doc == null ? "" : GRN.remarks_on_doc);
                var is_active = new SqlParameter("@is_active", 1);
                var pan_no = new SqlParameter("@pan_no", GRN.pan_no == null ? string.Empty : GRN.pan_no);
                var ecc_no = new SqlParameter("@ecc_no", GRN.ecc_no == null ? "" : GRN.ecc_no);
                var vat_tin_no = new SqlParameter("@vat_tin_no", GRN.vat_tin_no == null ? "" : GRN.vat_tin_no);
                var cst_tin_no = new SqlParameter("@cst_tin_no", GRN.cst_tin_no == null ? "" : GRN.cst_tin_no);
                var service_tax_no = new SqlParameter("@service_tax_no", GRN.service_tax_no == null ? "" : GRN.service_tax_no);
                var gst_no = new SqlParameter("@gst_no", GRN.gst_no == null ? "" : GRN.gst_no);
                var form_id = new SqlParameter("@form_id", GRN.form_id == null ? -1 : GRN.form_id);
                var deleteids = new SqlParameter("@deleteids", GRN.deleteids == null ? "" : GRN.deleteids);
                var user = new SqlParameter("@create_user", create_user);
                var attachment = new SqlParameter("@attachment", FileAttachment == "" ? "No File" : FileAttachment);
                var place_of_supply_id = new SqlParameter("@place_of_supply_id", GRN.place_of_supply_id);
                var gst_vendor_type_id = new SqlParameter("@gst_vendor_type_id", GRN.gst_vendor_type_id == null ? 0 : GRN.gst_vendor_type_id);
                var gst_in = new SqlParameter("@gst_in", GRN.gst_in);
                var val = _scifferContext.Database.SqlQuery<string>("exec save_GRN @grn_id ,@po_id ,@grn_number ,@vendor_id ,@net_value ,@category_id ,@net_currency_id ,@gross_value ,@gross_currency_id ,@posting_date ,@business_unit_id ,@plant_id ,@freight_terms_id ,@delivery_date ,@vendor_doc_no ,@vendor_doc_date ,@gate_entry_number ,@gate_entry_date ,@created_by ,@billing_address ,@billing_city ,@billing_state_id ,@billing_pin_code ,@email_id ,@payment_terms_id ,@payment_cycle_id ,@status_id ,@internal_remarks ,@remarks_on_doc ,@attachment ,@is_active ,@pan_no ,@ecc_no ,@vat_tin_no ,@cst_tin_no ,@service_tax_no ,@gst_no,@deleteids,@t1,@form_id,@create_user,@place_of_supply_id,@gst_vendor_type_id,@gst_in",
                    grn_id, po_id, grn_number, vendor_id, net_value, category_id, net_currency_id, gross_value, gross_currency_id, posting_date, business_unit_id,
                    plant_id, freight_terms_id, delivery_date, vendor_doc_no, vendor_doc_date, gate_entry_number, gate_entry_date, created_by, billing_address,
                    billing_city, billing_state_id, billing_pin_code, email_id, payment_terms_id, payment_cycle_id, status_id, internal_remarks, remarks_on_doc,
                    attachment, is_active, pan_no, ecc_no, vat_tin_no, cst_tin_no, service_tax_no, gst_no, deleteids, t1, form_id, user, place_of_supply_id,
                    gst_vendor_type_id, gst_in).FirstOrDefault();
                if (val.Contains("Saved"))
                //if (val==1)
                {
                    if (GRN.FileUpload != null)
                    {
                        GRN.FileUpload.SaveAs(GRN.attachment);
                    }
                    return val.ToString();
                }
                else
                {
                    System.IO.File.Delete(GRN.attachment);
                    return val.ToString();
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Delete(int id, string cancellation_remarks, int reason_id)
        {
            try
            {
                int create_user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var grn_id = new SqlParameter("@id", id);
                var remarks = new SqlParameter("@cancellation_remarks", cancellation_remarks == null ? "" : cancellation_remarks);
                var created_by = new SqlParameter("@created_by", create_user);
                var cancellation_reason_id = new SqlParameter("@cancellation_reason_id", reason_id);
                var val = _scifferContext.Database.SqlQuery<string>(
                  "exec cancel_grn @id ,@cancellation_remarks ,@created_by,@cancellation_reason_id", grn_id, remarks, created_by, cancellation_reason_id).FirstOrDefault();
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

        public pur_grnVM Get(int? id)
        {
            pur_grn purchase = _scifferContext.pur_grn.FirstOrDefault(c => c.grn_id == id);
            var po_no = _scifferContext.pur_po.Where(x => x.po_id == purchase.po_id).FirstOrDefault();
            Mapper.CreateMap<pur_grn, pur_grnVM>().ForMember(dest => dest.FileUpload, opt => opt.Ignore());
            pur_grnVM purchasevm = Mapper.Map<pur_grn, pur_grnVM>(purchase);
            purchasevm.po_no = po_no.po_no;
            purchasevm.pur_grn_detail = purchasevm.pur_grn_detail.Where(c => c.is_active == true).ToList();
            purchasevm.vendor_code = purchasevm.REF_VENDOR.VENDOR_CODE;
            purchasevm.vendor_name = purchasevm.REF_VENDOR.VENDOR_NAME;
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
            purchasevm.grn_detail_vm = (from a in purchasevm.pur_grn_detail
                                        join item in _scifferContext.REF_ITEM on a.item_id equals item.ITEM_ID
                                        join sac in _scifferContext.ref_sac on a.sac_hsn_id equals sac.sac_id into sac1
                                        from sac2 in sac1.DefaultIfEmpty()
                                        join hsn in _scifferContext.ref_hsn_code on a.sac_hsn_id equals hsn.hsn_code_id into hsn1
                                        from hsn2 in hsn1.DefaultIfEmpty()
                                        select new
                                        {
                                            grn_detail_id = a.grn_detail_id,
                                            delivery_date = a.delivery_date,
                                            storage_location_id = a.storage_location_id,
                                            item_id = a.item_id,
                                            item_code = a.REF_ITEM.ITEM_CODE + "/" + a.REF_ITEM.ITEM_NAME,
                                            storage_location_name = a.REF_STORAGE_LOCATION.storage_location_name + "/" + a.REF_STORAGE_LOCATION.description,
                                            quantity = a.quantity,
                                            uom_id = a.uom_id,
                                            uom_name = a.REF_UOM.UOM_NAME,
                                            unit_price = a.unit_price,
                                            discount = a.discount,
                                            eff_unit_price = a.eff_unit_price,
                                            purchase_value = a.purchase_value,
                                            assessable_rate = a.assessable_rate,
                                            assessable_value = a.assessable_value,
                                            tax_id = a.tax_id,
                                            is_active = a.is_active,
                                            grn_id = a.grn_id,
                                            po_detail_id = a.po_detail_id,
                                            user_description = a.user_description,
                                            bucket_id = a.bucket_id,
                                            batch = a.batch,
                                            batch_managed = a.batch_managed,
                                            expirary_date = a.expirary_date,
                                            tax_code = a.REF_TAX.tax_code + "/" + a.REF_TAX.tax_name,
                                            hsn_id = item.item_type_id == 2 ? sac2.sac_id : hsn2.hsn_code_id,
                                            hsn_code = item.item_type_id == 2 ? sac2.sac_code + "/" + sac2.sac_description : hsn2.hsn_code + "/" + hsn2.hsn_code_description,
                                        }).ToList().Select(a => new grn_detail_vm
                                        {
                                            grn_detail_id = a.grn_detail_id,
                                            delivery_date = a.delivery_date,
                                            storage_location_id = a.storage_location_id,
                                            storage_location_name = a.storage_location_name,
                                            item_id = a.item_id,
                                            item_code = a.item_code,
                                            quantity = a.quantity,
                                            uom_id = a.uom_id,
                                            uom_name = a.uom_name,
                                            unit_price = a.unit_price,
                                            discount = a.discount,
                                            eff_unit_price = a.eff_unit_price,
                                            purchase_value = a.purchase_value,
                                            assessable_rate = a.assessable_rate,
                                            assessable_value = a.assessable_value,
                                            tax_id = a.tax_id,
                                            is_active = a.is_active,
                                            grn_id = a.grn_id,
                                            po_detail_id = a.po_detail_id,
                                            user_description = a.user_description,
                                            bucket_id = a.bucket_id,
                                            batch = a.batch,
                                            batch_managed = a.batch_managed,
                                            expirary_date = a.expirary_date,
                                            hsn_id = a.hsn_id,
                                            hsn_code = a.hsn_code,
                                            tax_code = a.tax_code
                                        }).ToList();

            return purchasevm;
        }

        public List<pur_grnVM> GetAll()
        {
            Mapper.CreateMap<pur_grn, pur_grnVM>().ForMember(dest => dest.FileUpload, opt => opt.Ignore());
            return _scifferContext.pur_grn.Project().To<pur_grnVM>().Where(a => a.is_active == true).ToList();
        }
        public List<pur_grnViewModel> getall()
        {

            var val = _scifferContext.Database.SqlQuery<pur_grnViewModel>("exec get_grn_index_list ").ToList();
            return val;
            //var query = (from grn in _scifferContext.pur_grn.Where(x => x.is_active == true)
            //                 //join st in _scifferContext.REF_STATE on grn.billing_state_id equals st.STATE_ID
            //             join st1 in _scifferContext.REF_STATE on grn.status_id equals st1.STATE_ID
            //             join pay in _scifferContext.REF_PAYMENT_TERMS on grn.payment_terms_id equals pay.payment_terms_id
            //             join p1 in _scifferContext.REF_PAYMENT_CYCLE on grn.payment_cycle_id equals p1.PAYMENT_CYCLE_ID
            //             join p2 in _scifferContext.REF_PAYMENT_CYCLE_TYPE on p1.PAYMENT_CYCLE_TYPE_ID equals p2.PAYMENT_CYCLE_TYPE_ID
            //             join f in _scifferContext.REF_FREIGHT_TERMS on grn.freight_terms_id equals f.FREIGHT_TERMS_ID
            //             join o in _scifferContext.REF_PLANT on grn.plant_id equals o.PLANT_ID
            //             join p in _scifferContext.REF_VENDOR on grn.vendor_id equals p.VENDOR_ID
            //             join q in _scifferContext.ref_document_numbring on grn.category_id equals q.document_numbring_id
            //             join r in _scifferContext.REF_CURRENCY on grn.net_currency_id equals r.CURRENCY_ID
            //             join s in _scifferContext.REF_CURRENCY on grn.gross_currency_id equals s.CURRENCY_ID
            //             join b in _scifferContext.REF_BUSINESS_UNIT on grn.business_unit_id equals b.BUSINESS_UNIT_ID
            //             join u in _scifferContext.REF_USER on grn.created_by equals u.USER_ID into jo
            //             from terr in jo.DefaultIfEmpty()
            //             join purchaseorder in _scifferContext.pur_po on grn.po_id equals purchaseorder.po_id into purchaseorder1
            //             from purchaseorder2 in purchaseorder1.DefaultIfEmpty()
            //             join form in _scifferContext.pur_grn_form on grn.grn_id equals form.grn_id into form1
            //             from form2 in form1.DefaultIfEmpty()
            //             join status in _scifferContext.ref_status on grn.status_id equals status.status_id
            //             //join grnd in _scifferContext.pur_grn_detail on grn.grn_id equals grnd.grn_id 
            //             //join itm in _scifferContext.REF_ITEM on grnd.item_id equals itm.ITEM_ID

            //             select new
            //             {
            //                 grn_id = grn.grn_id,
            //                 grn_number = grn.grn_number,
            //                 vendor_name = p.VENDOR_NAME,
            //                 vendor_code = p.VENDOR_CODE,
            //                 category_name = q.category,
            //                 grn_net_value = grn.net_value,
            //                 net_currency = r.CURRENCY_NAME,
            //                 grn_gross_value = grn.gross_value,
            //                 gross_currency = s.CURRENCY_NAME,
            //                 grn_posting_date = grn.posting_date,
            //                 grn_business_unit = b.BUSINESS_UNIT_DESCRIPTION,
            //                 business_unit_code = b.BUSINESS_UNIT_NAME,
            //                 plant_name = o.PLANT_NAME,
            //                 plant_code = o.PLANT_CODE,
            //                 freight_terms = f.FREIGHT_TERMS_NAME,
            //                 delivery_date = grn.delivery_date,
            //                 vendor_doc_no = grn.vendor_doc_no,
            //                 vendor_doc_date = grn.vendor_doc_date,
            //                 gate_entry_number = grn.gate_entry_number,
            //                 gate_entry_date = grn.gate_entry_date,
            //                 created_by1 = terr.USER_NAME,
            //                 payment_terms = pay.payment_terms_code,
            //                 billing_address = grn.billing_address,
            //                 billing_city = grn.billing_city,
            //                 billing_state = st1.STATE_NAME,
            //                 billing_pin_code = grn.billing_pin_code,
            //                 email_id = grn.email_id,
            //                 payment_cycle = p1.PAYMENT_CYCLE_NAME,
            //                 payment_cycle_type = p1.REF_PAYMENT_CYCLE_TYPE.PAYMENT_CYCLE_TYPE_NAME,
            //                 status_name = status.status_name,
            //                 internal_remarks = grn.internal_remarks,
            //                 remarks_on_doc = grn.remarks_on_doc,
            //                 attachment = grn.attachment,
            //                 is_active = grn.is_active,
            //                 purchase_name = purchaseorder2.po_no,
            //                 country_name = st1.REF_COUNTRY.COUNTRY_NAME,
            //                 pan_no = grn.pan_no,
            //                 gst_no = grn.gst_no,
            //                 ecc_no = grn.ecc_no,
            //                 vat_tin_no = grn.vat_tin_no,
            //                 cst_tin_no = grn.cst_tin_no,
            //                 service_tax_no = grn.service_tax_no,
            //                 form_name = form2.REF_FORM.FORM_NAME,
            //                 cancellation_remarks = grn.cancellation_remarks,
            //                 item_name = "",
            //                 item_ids = _scifferContext.pur_grn_detail.Where(w => w.grn_id == grn.grn_id).Select(grnd => grnd.item_id).ToList(),
            //                 //item_name = string.Join(",", itm1.Select(kvp => kvp.ITEM_NAME))
            //             }).ToList().Select(x =>
            //             new pur_grnViewModel()
            //             {

            //                 grn_id = x.grn_id,
            //                 grn_number = x.grn_number,
            //                 vendor_name = x.vendor_name,
            //                 vendor_code = x.vendor_code,
            //                 category_name = x.category_name,
            //                 grn_net_value = x.grn_net_value,
            //                 net_currency = x.net_currency,
            //                 grn_gross_value = x.grn_gross_value,
            //                 gross_currency = x.gross_currency,
            //                 grn_posting_date = x.grn_posting_date,
            //                 grn_business_unit = x.grn_business_unit,
            //                 business_unit_code = x.business_unit_code,
            //                 plant_name = x.plant_name,
            //                 plant_code = x.plant_code,
            //                 freight_terms = x.freight_terms,
            //                 delivery_date = x.delivery_date,
            //                 vendor_doc_no = x.vendor_doc_no,
            //                 vendor_doc_date = x.vendor_doc_date,
            //                 gate_entry_number = x.gate_entry_number,
            //                 gate_entry_date = x.gate_entry_date,
            //                 created_by1 = x.created_by1,
            //                 payment_terms = x.payment_terms,
            //                 billing_address = x.billing_address,
            //                 billing_city = x.billing_city,
            //                 billing_state = x.billing_state,
            //                 billing_pin_code = x.billing_pin_code,
            //                 email_id = x.email_id,
            //                 payment_cycle = x.payment_cycle,
            //                 payment_cycle_type = x.payment_cycle_type,
            //                 status_name = x.status_name,
            //                 internal_remarks = x.internal_remarks,
            //                 remarks_on_doc = x.remarks_on_doc,
            //                 attachment = x.attachment,
            //                 is_active = x.is_active,
            //                 purchase_name = x.purchase_name,
            //                 country_name = x.country_name,
            //                 pan_no = x.pan_no,
            //                 gst_no = x.gst_no,
            //                 ecc_no = x.ecc_no,
            //                 vat_tin_no = x.vat_tin_no,
            //                 cst_tin_no = x.cst_tin_no,
            //                 service_tax_no = x.service_tax_no,
            //                 form_name = x.form_name,
            //                 cancellation_remarks = x.cancellation_remarks,
            //                 item_name = string.Join(",", _scifferContext.REF_ITEM.Where(w => x.item_ids.Contains(w.ITEM_ID)).Select(e => e.ITEM_NAME).ToList())

            //             }).OrderByDescending(a => a.grn_id).ToList();
            //return query;
        }


        public pur_poVM GetQuotationForGRN(int id)
        {
            var quotationvm = (from sq in _scifferContext.pur_po.Where(x => x.po_id == id)
                               join s1 in _scifferContext.REF_STATE on sq.billing_state_id equals s1.STATE_ID
                               join p in _scifferContext.REF_PAYMENT_CYCLE on sq.payment_cycle_id equals p.PAYMENT_CYCLE_ID
                               join f in _scifferContext.pur_po_form on sq.po_id equals f.po_id into jo
                               from form in jo.DefaultIfEmpty()
                               join v in _scifferContext.REF_VENDOR on sq.vendor_id equals v.VENDOR_ID
                               join td in _scifferContext.ref_tds_code on v.tds_id equals td.tds_code_id into j1
                               from tds in j1.DefaultIfEmpty()
                               join state in _scifferContext.REF_STATE on sq.place_of_supply_id equals state.STATE_ID into state1
                               from state2 in state1.DefaultIfEmpty()
                               select new pur_poVM()
                               {
                                   business_unit_id = sq.business_unit_id,
                                   freight_terms_id = sq.freight_terms_id,
                                   plant_id = sq.plant_id,
                                   billing_address = sq.billing_address,
                                   billing_city = sq.billing_city,
                                   country_id = s1.COUNTRY_ID,
                                   email_id = sq.email_id,
                                   billing_pin_code = sq.billing_pin_code,
                                   billing_state_id = sq.billing_state_id,
                                   gross_value = sq.gross_value,
                                   net_value = sq.net_value,
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
                                   gross_value_currency_id = sq.gross_value_currency_id,
                                   net_value_currency_id = sq.gross_value_currency_id,
                                   vendor_id = sq.vendor_id,
                                   vendor_quotation_no = sq.vendor_quotation_no,
                                   vendor_quotation_date = sq.vendor_quotation_date,
                                   status_id = sq.status_id,
                                   internal_remarks = sq.internal_remarks,
                                   remarks_on_document = sq.remarks_on_document,
                                   delivery_date = sq.delivery_date,
                                   tds_id = tds == null ? 0 : tds.tds_code_id,
                                   place_of_supply_id = state2.STATE_ID,
                                   gst_vendor_type_id = sq.gst_vendor_type_id,
                                   is_rcm = sq.is_rcm,
                               }).FirstOrDefault();
            return quotationvm;
        }


        public List<pur_grnVM> GetGrnListForIEX(int vendor_id)
        {
            var idstr1 = (from b in _scifferContext.pur_incoming_excise.Where(x => x.vendor_id == vendor_id && x.cancellation_remarks == null)
                              // where (b.vendor_id == vendor_id && b.cancellation_remarks != null)
                          select b.grn_id).ToList();
            var query = (from grn in _scifferContext.pur_grn.Where(x => x.vendor_id == vendor_id)
                         join po in _scifferContext.pur_po on grn.po_id equals po.po_id
                         join grn_tax in _scifferContext.pur_po_detail_tax_element on po.po_id equals grn_tax.po_id
                         join rt in _scifferContext.ref_tax_element on grn_tax.tax_element_id equals rt.tax_element_id
                         join ty in _scifferContext.ref_tax_type.Where(x => x.income_excise == true) on rt.tax_type_id equals ty.tax_type_id
                         where (!idstr1.Contains(grn.grn_id))
                         select new
                         {
                             grn_id = grn.grn_id,
                             grn_number = grn.grn_number,
                             posting_date = grn.posting_date,
                             po_number = po.po_no,
                         }).Distinct().ToList().Select(a => new pur_grnVM
                         {
                             grn_id = a.grn_id,
                             grn_number = a.po_number + " - " + a.grn_number + " - " + DateTime.Parse(a.posting_date.ToString()).ToString("dd/MMM/yyyy"),
                         }).ToList();
            return query;
        }
        public DateTime? GetBatchForExpiraryDate(string batch)
        {
            var expirarydate = _scifferContext.pur_grn_detail.Where(x => x.batch == batch).FirstOrDefault();
            if (expirarydate == null)
            {
                return null;
            }
            else
            {
                return expirarydate.expirary_date;
            }

        }
        public pur_grnViewModel GetGrnForReport(int id)
        {
            var query = (from c in _scifferContext.REF_COMPANY
                         join s1 in _scifferContext.REF_STATE on c.REGISTERED_STATE_ID equals s1.STATE_ID
                         from g in _scifferContext.pur_grn.Where(x => x.grn_id == id)
                         join p in _scifferContext.REF_PLANT on g.plant_id equals p.PLANT_ID
                         join s2 in _scifferContext.REF_STATE on p.PLANT_STATE equals s2.STATE_ID
                         join v in _scifferContext.REF_VENDOR on g.vendor_id equals v.VENDOR_ID
                         join d in _scifferContext.ref_document_numbring on g.category_id equals d.document_numbring_id
                         join po in _scifferContext.pur_po on g.po_id equals po.po_id
                         select new pur_grnViewModel
                         {
                             company_address = c.REGISTERED_ADDRESS + " , " + c.REGISTERED_CITY + " , " + c.registered_pincode + " , " + s1.STATE_NAME + " - "+ s1.REF_COUNTRY.COUNTRY_NAME,
                             company_cin_no = c.CIN_NO,
                             company_name = c.COMPANY_DISPLAY_NAME,
                             plant_name = p.PLANT_NAME,
                             plant_address = p.PLANT_ADDRESS + " , " + p.PLANT_CITY + " , "+ p.pincode + " , " + s2.STATE_NAME + " - " + s2.REF_COUNTRY.COUNTRY_NAME,
                             category_name = d.category,
                             grn_number = g.grn_number,
                             grn_posting_date = g.posting_date,
                             gate_entry_date = g.gate_entry_date,
                             gate_entry_number = g.gate_entry_number,
                             vendor_name = v.VENDOR_NAME,
                             vendor_code = v.VENDOR_CODE,
                             po_number = po.po_no,
                             vendor_doc_date = g.vendor_doc_date,
                             vendor_doc_no = g.vendor_doc_no,
                             PLANT_TELEPHONE = "Phone : +91 831 " + p.PLANT_TELEPHONE,
                             PLANT_EMAIL = "Email : " + p.PLANT_EMAIL,
                             plant_code = p.PLANT_CODE,
                         }).FirstOrDefault();

            string[] telephones = query.PLANT_TELEPHONE.Split(',');

            foreach (var phone in telephones)
            {
                if (phone.Contains("Phone"))
                    query.PLANT_TELEPHONE = phone;
                else
                    query.PLANT_TELEPHONE += ", +91 831" + phone;
            }

            return query;
        }
        public List<grn_report_detail> GetGrnDetailForReport(int id)
        {
            var query = (from pg in _scifferContext.pur_grn_detail.Where(x => x.grn_id == id)
                         join i in _scifferContext.REF_ITEM on pg.item_id equals i.ITEM_ID
                         join u in _scifferContext.REF_UOM on pg.uom_id equals u.UOM_ID
                         join b in _scifferContext.ref_bucket on pg.bucket_id equals b.bucket_id
                         join s in _scifferContext.REF_STORAGE_LOCATION on pg.storage_location_id equals s.storage_location_id
                         select new grn_report_detail
                         {
                             batch_number = pg.batch,
                             bucket = b.bucket_name,
                             item_code = i.ITEM_CODE,
                             item_description = i.ITEM_NAME,
                             quantity = pg.quantity,
                             sloc_name = s.description,
                             uom_name = u.UOM_NAME,
                             grn_detail_id = pg.grn_detail_id,
                         }).ToList();
            return query;
        }
    }
}