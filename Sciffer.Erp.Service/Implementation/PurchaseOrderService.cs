using AutoMapper;
using AutoMapper.QueryableExtensions;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericService;
        public PurchaseOrderService(ScifferContext scifferContext, IGenericService GenericService)
        {
            _scifferContext = scifferContext;
            _genericService = GenericService;
        }
        public string Add(pur_poVM Purchase)
        {
            try
            {
                if (Purchase.FileUpload != null)
                {
                    Purchase.attachement = _genericService.GetFilePath("PurchaseOrder", Purchase.FileUpload);
                }
                else
                {
                    Purchase.attachement = "No File";
                }
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("po_staggered_delivery_id", typeof(int));
                dt1.Columns.Add("staggered_date", typeof(DateTime));
                dt1.Columns.Add("staggered_qty", typeof(double));
                dt1.Columns.Add("staggered_item", typeof(double));
                dt1.Columns.Add("srnos", typeof(int));

                if (Purchase.staggered_date != null)
                {
                    for (var i = 0; i < Purchase.staggered_date.Count; i++)
                    {
                        if (Purchase.staggered_date[i] != "")
                        {
                            dt1.Rows.Add(int.Parse(Purchase.po_staggered_delivery_id[i]), DateTime.Parse(Purchase.staggered_date[i]), double.Parse(Purchase.staggered_qty[i]), int.Parse(Purchase.staggered_item[i]), int.Parse(Purchase.srnos[i]));
                        }
                    }
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("po_detail_id", typeof(int));
                dt.Columns.Add("sloc_id", typeof(int));
                dt.Columns.Add("item_id", typeof(int));
                dt.Columns.Add("delivery_date", typeof(DateTime));
                dt.Columns.Add("quantity", typeof(double));
                dt.Columns.Add("uom_id", typeof(int));
                dt.Columns.Add("unit_price", typeof(double));
                dt.Columns.Add("discount", typeof(double));
                dt.Columns.Add("eff_unit_price", typeof(double));
                dt.Columns.Add("purchase_value", typeof(double));
                dt.Columns.Add("assessable_rate", typeof(double));
                dt.Columns.Add("assessable_value", typeof(double));
                dt.Columns.Add("tax_code_id", typeof(int));
                dt.Columns.Add("user_description", typeof(string));
                dt.Columns.Add("pur_requisition_detail_id", typeof(int));
                dt.Columns.Add("sac_hsn_id", typeof(int));
                dt.Columns.Add("item_type_id", typeof(int));
                dt.Columns.Add("detsr", typeof(int));
                var j = 0;
                for (var i = 0; i < Purchase.item_ids.Count; i++)
                {
                    int po = -1;
                    if (Purchase.po_detail_ids != null)
                    {
                        po = Purchase.po_detail_ids[i] == "0" ? -1 : Convert.ToInt32(Purchase.po_detail_ids[i]);
                    }
                    var item_type_id = 0;
                    if (Purchase.item_type_id.Count == 1)
                    {
                        item_type_id = Purchase.item_type_id[j];
                    }
                    else if (Purchase.item_type_id.Count > 1)
                    {
                        item_type_id = Purchase.item_type_id[j];
                        j = j + 1;
                    }

                    if (item_type_id != 0)
                    {
                        item_type_id = Purchase.item_type_id[j];
                    }
                    else
                    {
                        var item_id = Purchase.item_ids[i];
                        item_type_id = _genericService.GetItemType_id(item_id);
                    }

                    try
                    {
                        dt.Rows.Add(po, Purchase.sloc_ids[i] == null ? 0 : Purchase.sloc_ids[i], Purchase.item_ids[i], Purchase.Linedelivery_dates[i], Purchase.quantitys[i],
                                      Purchase.uom_ids[i] == null ? 0 : Purchase.uom_ids[i], Purchase.unit_prices[i], Purchase.discounts[i] == null ? 0 : Purchase.discounts[i],
                                     Purchase.eff_prices[i], Purchase.pur_values[i], Purchase.ass_rates[i], Purchase.ass_values[i],
                                     Purchase.tax_ids[i], Purchase.user_descriptions == null ? "" : Purchase.user_descriptions[i] == null ? "" : Purchase.user_descriptions[i], Purchase.pur_requisition_detail_ids[i] == null ? 0 : Purchase.pur_requisition_detail_ids[i], Purchase.sac_hsn_ids[i] == null ? 0 : Purchase.sac_hsn_ids[i], item_type_id, Purchase.detsrs[i]);
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                }
                int createdby = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                DateTime dte = new DateTime(1990, 1, 1);
                var po_id = new SqlParameter("@po_id", Purchase.po_id == 0 ? -1 : Purchase.po_id);
                var category_id = new SqlParameter("@category_id", Purchase.category_id);
                var pur_requisition_id = new SqlParameter("@pur_requisition_id", Purchase.pur_requisition_id == null ? 0 : Purchase.pur_requisition_id);
                var po_no = new SqlParameter("@po_no", Purchase.po_no == null ? "" : Purchase.po_no);
                var vendor_id = new SqlParameter("@vendor_id", Purchase.vendor_id);
                var po_date = new SqlParameter("@po_date", Purchase.po_date);
                var net_value = new SqlParameter("@net_value", Purchase.net_value);
                var net_value_currency_id = new SqlParameter("@net_value_currency_id", Purchase.net_value_currency_id);
                var gross_value = new SqlParameter("@gross_value", Purchase.gross_value);
                var gross_value_currency_id = new SqlParameter("@gross_value_currency_id", Purchase.gross_value_currency_id);
                var business_unit_id = new SqlParameter("@business_unit_id", Purchase.business_unit_id);
                var plant_id = new SqlParameter("@plant_id", Purchase.plant_id);
                var freight_terms_id = new SqlParameter("@freight_terms_id", Purchase.freight_terms_id);
                var delivery_date = new SqlParameter("@delivery_date", Purchase.delivery_date == null ? dte : Purchase.delivery_date);
                var vendor_quotation_no = new SqlParameter("@vendor_quotation_no", Purchase.vendor_quotation_no == null ? string.Empty : Purchase.vendor_quotation_no);
                var vendor_quotation_date = new SqlParameter("@vendor_quotation_date", Purchase.vendor_quotation_date == null ? dte : Purchase.vendor_quotation_date);
                var valid_until = new SqlParameter("@valid_until", Purchase.valid_until == null ? dte : Purchase.valid_until);
                var created_on = new SqlParameter("@created_on", DateTime.Now);
                var created_by = new SqlParameter("@created_by", createdby);
                var billing_address = new SqlParameter("@billing_address", Purchase.billing_address);
                var billing_city = new SqlParameter("@billing_city", Purchase.billing_city);
                var billing_state_id = new SqlParameter("@billing_state_id", Purchase.billing_state_id);
                var billing_pin_code = new SqlParameter("@billing_pin_code", Purchase.billing_pin_code == null ? "" : Purchase.billing_pin_code);
                var email_id = new SqlParameter("@email_id", Purchase.email_id == null ? "" : Purchase.email_id);
                var payment_terms_id = new SqlParameter("@payment_terms_id", Purchase.payment_terms_id);
                var payment_cycle_id = new SqlParameter("@payment_cycle_id", Purchase.payment_cycle_id);
                var status_id = new SqlParameter("@status_id", Purchase.status_id);
                var internal_remarks = new SqlParameter("@internal_remarks", Purchase.internal_remarks == null ? "" : Purchase.internal_remarks);
                var remarks_on_document = new SqlParameter("@remarks_on_document", Purchase.remarks_on_document == null ? "" : Purchase.remarks_on_document);
                var attachement = new SqlParameter("@attachement", Purchase.attachement);
                var pan_no = new SqlParameter("@pan_no", Purchase.pan_no == null ? "" : Purchase.pan_no);
                var ecc_no = new SqlParameter("@ecc_no", Purchase.ecc_no == null ? "" : Purchase.ecc_no);
                var vat_tin_no = new SqlParameter("@vat_tin_no", Purchase.vat_tin_no == null ? "" : Purchase.vat_tin_no);
                var cst_tin_no = new SqlParameter("@cst_tin_no", Purchase.cst_tin_no == null ? "" : Purchase.cst_tin_no);
                var service_tax_no = new SqlParameter("@service_tax_no", Purchase.service_tax_no == null ? "" : Purchase.service_tax_no);
                var gst_no = new SqlParameter("@gst_no", Purchase.gst_no == null ? "" : Purchase.gst_no);
                var tds_id = new SqlParameter("@tds_id", Purchase.tds_id);
                var item_service_id = new SqlParameter("@item_service_id", Purchase.item_service_id);
                var form_id = new SqlParameter("@form_id", Purchase.form_id == null ? 0 : Purchase.form_id);
                var delete = new SqlParameter("@deleteids", Purchase.deleteids == null ? "" : Purchase.deleteids);
                var ref_doc_no = new SqlParameter("@ref_doc_no", Purchase.ref_doc_no == null ? "" : Purchase.ref_doc_no);
                var delivery_type_id = new SqlParameter("@delivery_type_id", Purchase.delivery_type_id);
                var maximum_limit_qty = new SqlParameter("@maximum_limit_qty", Purchase.maximum_limit_qty == null ? 0 : Purchase.maximum_limit_qty);
                var valid_from_date = new SqlParameter("@valid_from_date", Purchase.valid_from_date == null ? dte : Purchase.valid_from_date);
                var valid_to_date = new SqlParameter("@valid_to_date", Purchase.valid_to_date == null ? dte : Purchase.valid_to_date);
                var place_of_supply_id = new SqlParameter("@place_of_supply_id", Purchase.place_of_supply_id);
                var gst_vendor_type_id = new SqlParameter("@gst_vendor_type_id", Purchase.gst_vendor_type_id);
                var gst_in = new SqlParameter("@gst_in", Purchase.gst_in);
                var is_rcm = new SqlParameter("@is_rcm", Purchase.is_rcm);
                var mobile_number = new SqlParameter("@mobile_number", Purchase.mobile_number == null ? "" : Purchase.mobile_number);
                var responsibility_id = new SqlParameter("@responsibility_id", Purchase.responsibility_id == null ? 0 : Purchase.responsibility_id);
                var with_without_service_id = new SqlParameter("@with_without_service_id", Purchase.with_without_service_id == null ? 0 : Purchase.with_without_service_id);
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_pur_po_detail";
                t1.Value = dt;
                var t2 = new SqlParameter("@t2", SqlDbType.Structured);
                t2.TypeName = "dbo.temp_pur_po_staggered_delivery";
                t2.Value = dt1;
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_purchase_order @po_id ,@category_id ,@pur_requisition_id ,@po_no ,@vendor_id ,@po_date ,@net_value ,@net_value_currency_id ,@gross_value ,@gross_value_currency_id ,@business_unit_id ,@plant_id ,@freight_terms_id ,@delivery_date ,@vendor_quotation_no ,@vendor_quotation_date ,@valid_until ,@created_on ,@created_by ,@billing_address ,@billing_city ,@billing_state_id ,@billing_pin_code ,@email_id ,@payment_terms_id ,@payment_cycle_id ,@status_id ,@internal_remarks ,@remarks_on_document ,@attachement ,@pan_no ,@ecc_no ,@vat_tin_no ,@cst_tin_no ,@service_tax_no ,@gst_no ,@tds_id ,@item_service_id,@form_id,@deleteids,@t1,@ref_doc_no,@delivery_type_id,@t2,@maximum_limit_qty,@valid_from_date,@valid_to_date,@place_of_supply_id,@gst_vendor_type_id,@gst_in,@is_rcm,@responsibility_id,@with_without_service_id,@mobile_number",
                    po_id, category_id, pur_requisition_id, po_no, vendor_id, po_date, net_value, net_value_currency_id, gross_value, gross_value_currency_id,
                    business_unit_id, plant_id, freight_terms_id, delivery_date, vendor_quotation_no, vendor_quotation_date, valid_until, created_on, created_by,
                    billing_address, billing_city, billing_state_id, billing_pin_code, email_id, payment_terms_id, payment_cycle_id, status_id, internal_remarks,
                    remarks_on_document, attachement, pan_no, ecc_no, vat_tin_no, cst_tin_no, service_tax_no, gst_no, tds_id, item_service_id,
                    form_id, delete, t1, ref_doc_no, delivery_type_id, t2, maximum_limit_qty, valid_from_date, valid_to_date, place_of_supply_id,
                    gst_vendor_type_id, gst_in, is_rcm, responsibility_id, with_without_service_id, mobile_number).FirstOrDefault();
                if (val.Contains("Saved"))
                {

                    if (Purchase.FileUpload != null)
                    {
                        Purchase.FileUpload.SaveAs(Purchase.attachement);
                    }
                    return val;
                }
                else
                {
                    System.IO.File.Delete(Purchase.attachement);
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
                return "Error : " + ex.Message;
                //return "error";
            }
        }

        public List<pur_po_detail_vm> GetPOProductForGRN(string ent, int id, DateTime? posting_date)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", ent);
            var postingdate = new SqlParameter("@posting_date", posting_date);
            var val = _scifferContext.Database.SqlQuery<pur_po_detail_vm>(
            "exec GetPODetailforGRN @entity,@id,@posting_date", entity, quotation_id, postingdate).ToList();
            return val;
        }
        public List<pur_po_detail_report_vm> GetPOProductForReport(int id, string ent)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", ent);
            var val = _scifferContext.Database.SqlQuery<pur_po_detail_report_vm>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;
        }
        public List<pur_po_detail_report_vm> GetPODeliverydetail(int id, string ent)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", ent);
            var val = _scifferContext.Database.SqlQuery<pur_po_detail_report_vm>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;
        }
        public bool Delete(int? id)
        {
            try
            {
                _scifferContext.Database.ExecuteSqlCommand("update [dbo].[pur_po] set [is_active] = 0,[approval_status]=0 where po_id = " + id);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public string Close(int? id, string closed_remarks)
        {
            try
            {
                var status = _scifferContext.ref_status.Where(x => x.form == "PO" && x.status_name == "Closed").FirstOrDefault();
                int createdby = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                pur_po_temp_close_cancel pur_Po_Temp_Close_Cancel = new pur_po_temp_close_cancel();
                pur_Po_Temp_Close_Cancel.po_id = Convert.ToInt32(id);
                pur_Po_Temp_Close_Cancel.order_status_cancellation_reason_id = 1;
                pur_Po_Temp_Close_Cancel.status_id = status.status_id;
                pur_Po_Temp_Close_Cancel.Remarks = closed_remarks;
                pur_Po_Temp_Close_Cancel.modified_by = createdby;
                pur_Po_Temp_Close_Cancel.modified_ts = DateTime.Now;

                _scifferContext.pur_po_temp_close_cancel.Add(pur_Po_Temp_Close_Cancel);
                _scifferContext.SaveChanges();

                var st = "update [dbo].[pur_po] set [closed_remarks] = '" + closed_remarks + "',[approval_status] = 0 where po_id = " + id;
                _scifferContext.Database.ExecuteSqlCommand(st);
                return "Closed";

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
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

        public pur_poVM Get(int? id)
        {
            pur_po purchase = _scifferContext.pur_po.FirstOrDefault(c => c.po_id == id);
            Mapper.CreateMap<pur_po, pur_poVM>().ForMember(dest => dest.FileUpload, opt => opt.Ignore());
            pur_poVM purchasevm = Mapper.Map<pur_po, pur_poVM>(purchase);
            if (purchasevm.delivery_date == DateTime.Parse("01 - 01 - 1990"))
            {
                purchasevm.delivery_date = null;
            }
            //purchasevm.pur_po_detail = purchasevm.pur_po_detail.Where(c => c.is_active == true).ToList();
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
            var poid = new SqlParameter("@id", id);
            var poid1 = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getpodetail");
            var entity1 = new SqlParameter("@entity", "getpodeliverydetail");
            var val = _scifferContext.Database.SqlQuery<pur_po_detail_vm>(
                   "exec get_purchase_order @entity ,@id", entity, poid).ToList();
            purchasevm.pur_po_detail_vm = val;
            var val1 = _scifferContext.Database.SqlQuery<pur_po_delivery_detail_vm>(
                   "exec get_purchase_order @entity ,@id", entity1, poid1).ToList();
            purchasevm.pur_po_delivery_detail_vm = val1;
            foreach (var i in purchasevm.pur_po_detail)
            {
                purchasevm.maximum_limit_qty = i.maximum_limit_qty;
                purchasevm.valid_from_date = i.valid_from_date;
                purchasevm.valid_to_date = i.valid_to_date;
                break; // get out of the loop
            }
            return purchasevm;
        }

        public List<pur_poVM> GetAll()
        {
            Mapper.CreateMap<pur_po, pur_poVM>().ForMember(dest => dest.FileUpload, opt => opt.Ignore());
            return _scifferContext.pur_po.Project().To<pur_poVM>().Where(a => a.is_active == true).ToList();
        }
        public List<pur_poVM> getall()
        {
            var ent = new SqlParameter("@entity", "getall");
            var val = _scifferContext.Database.SqlQuery<pur_poVM>(
                "exec get_purchase_order @entity", ent).ToList();
            return val;
        }
        public bool Update(pur_poVM Purchase)
        {
            try
            {
                pur_po purchase_order = new pur_po();
                purchase_order.business_unit_id = Purchase.business_unit_id;
                purchase_order.category_id = Purchase.category_id;
                purchase_order.po_no = Purchase.po_no;
                purchase_order.vendor_id = Purchase.vendor_id;
                purchase_order.po_date = Purchase.po_date;
                purchase_order.net_value = Purchase.net_value;
                purchase_order.net_value_currency_id = Purchase.net_value_currency_id;
                purchase_order.gross_value = Purchase.gross_value;
                purchase_order.gross_value_currency_id = Purchase.gross_value_currency_id;
                purchase_order.plant_id = Purchase.plant_id;
                purchase_order.freight_terms_id = Purchase.freight_terms_id;
                purchase_order.delivery_date = Purchase.delivery_date;
                purchase_order.vendor_quotation_no = Purchase.vendor_quotation_no;
                purchase_order.vendor_quotation_date = Purchase.vendor_quotation_date;
                purchase_order.valid_until = Purchase.valid_until;
                purchase_order.created_on = DateTime.Now;
                purchase_order.created_by = 1;
                purchase_order.billing_address = Purchase.billing_address;
                purchase_order.billing_city = Purchase.billing_city;
                purchase_order.billing_state_id = Purchase.billing_state_id;
                purchase_order.billing_pin_code = Purchase.billing_pin_code;
                purchase_order.email_id = Purchase.email_id;
                purchase_order.payment_terms_id = Purchase.payment_terms_id;
                purchase_order.payment_cycle_id = Purchase.payment_cycle_id;
                purchase_order.status_id = Purchase.status_id;
                purchase_order.internal_remarks = Purchase.internal_remarks;
                purchase_order.remarks_on_document = Purchase.remarks_on_document;
                purchase_order.pan_no = Purchase.pan_no;
                purchase_order.ecc_no = Purchase.ecc_no;
                purchase_order.vat_tin_no = Purchase.vat_tin_no;
                purchase_order.cst_tin_no = Purchase.cst_tin_no;
                purchase_order.service_tax_no = Purchase.service_tax_no;
                purchase_order.gst_no = Purchase.gst_no;
                if (Purchase.FileUpload != null)
                {
                    purchase_order.attachement = _genericService.GetFilePath("PurchaseOrder", Purchase.FileUpload);
                }
                else
                {
                    purchase_order.attachement = Purchase.attachement;
                }

                purchase_order.po_id = Purchase.po_id;
                purchase_order.is_active = true;

                string[] deleteStringArray = new string[0];
                try
                {
                    deleteStringArray = Purchase.deleteids.Split(new char[] { '~' });
                }
                catch
                {

                }
                int pur_po_detail_id;
                for (int i = 0; i <= deleteStringArray.Count() - 1; i++)
                {
                    if (deleteStringArray[i] != "")
                    {
                        pur_po_detail_id = int.Parse(deleteStringArray[i]);
                        var purchase_detail = _scifferContext.pur_po_detail.Find(pur_po_detail_id);
                        _scifferContext.Entry(purchase_detail).State = EntityState.Modified;
                        purchase_detail.is_active = false;
                    }
                }
                List<pur_po_detail> detail_list = new List<pur_po_detail>();
                if (Purchase.pur_po_detail != null)
                {
                    foreach (var items in Purchase.pur_po_detail)
                    {
                        pur_po_detail detail = new pur_po_detail();
                        detail.po_id = Purchase.po_id;
                        if (items.po_detail_id == null)
                        {
                            items.po_detail_id = 0;
                        }
                        detail.is_active = items.is_active;
                        detail.sr_no = items.sr_no;
                        detail.sloc_id = items.sloc_id;
                        detail.item_id = items.item_id;
                        detail.delivery_date = items.delivery_date;
                        detail.quantity = items.quantity;
                        detail.uom_id = items.uom_id;
                        detail.unit_price = items.unit_price;
                        detail.discount = items.discount;
                        detail.eff_unit_price = items.eff_unit_price;
                        detail.purchase_value = items.purchase_value;
                        detail.assesable_rate = items.assesable_rate;
                        detail.assessable_value = items.assessable_value;
                        detail.tax_code_id = items.tax_code_id;
                        detail.po_detail_id = items.po_detail_id;
                        detail_list.Add(detail);
                    }
                }
                purchase_order.pur_po_detail = detail_list;
                List<pur_po_form> forms = new List<pur_po_form>();
                if (Purchase.pur_po_form != null)
                {
                    foreach (var i in Purchase.pur_po_form)
                    {
                        pur_po_form frm = new pur_po_form();
                        frm.form_id = i.form_id;
                        frm.po_id = Purchase.po_id;
                        forms.Add(frm);
                    }
                }
                purchase_order.pur_po_form = forms;
                foreach (var detail in purchase_order.pur_po_detail)
                {
                    if (detail.item_id != 0)
                    {
                        if (detail.po_detail_id != 0)
                        {
                            _scifferContext.Entry(detail).State = EntityState.Modified;
                        }
                        else
                        {
                            _scifferContext.Entry(detail).State = EntityState.Added;
                        }
                    }
                }

                foreach (var F in purchase_order.pur_po_form)
                {
                    _scifferContext.pur_po_form.Add(F);

                }
                _scifferContext.Entry(purchase_order).State = EntityState.Modified;
                _scifferContext.SaveChanges();
                if (Purchase.FileUpload != null)
                {
                    Purchase.FileUpload.SaveAs(_genericService.GetFilePath("PurchaseOrder", Purchase.FileUpload));
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public List<pur_po_detail_vm> GetBalancePOList(int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getpodetail");
            var val = _scifferContext.Database.SqlQuery<pur_po_detail_vm>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;
        }
        public List<pur_po_report_vm> GetPOList(int id, int vendor_id)
        {
            if (id != 2)
            {
                var query = (from po in _scifferContext.pur_po.Where(x => x.is_active == true && x.vendor_id == vendor_id && x.approval_status == 1 && x.item_service_id != 2)
                             join v in _scifferContext.REF_VENDOR on po.vendor_id equals v.VENDOR_ID
                             join st in _scifferContext.ref_status.Where(a => a.status_name != "Closed" && a.status_name != "Cancelled") on po.status_id equals st.status_id
                             select new
                             {
                                 po_id = po.po_id,
                                 po_date = po.po_date,
                                 po_no = po.po_no,
                                 vendor_name = v.VENDOR_CODE + "-" + v.VENDOR_NAME,
                                 net_value = po.net_value,
                                 gross_value = po.gross_value,
                             }).ToList().Select(a => new pur_po_report_vm
                             {
                                 podate = DateTime.Parse(a.po_date.ToString()).ToString("dd/MMM/yyyy"),
                                 po_no = a.po_no,
                                 vendor_name = a.vendor_name,
                                 net_value = a.net_value,
                                 po_id = a.po_id,
                             }).ToList().OrderByDescending(a => a.po_id).ToList();
                return query;
            }
            else
            {
                var query = (from po in _scifferContext.pur_po.Where(x => x.is_active == true && x.item_service_id == id && x.vendor_id == vendor_id && x.approval_status == 1 && x.with_without_service_id == 1)
                             join v in _scifferContext.REF_VENDOR on po.vendor_id equals v.VENDOR_ID
                             join st in _scifferContext.ref_status.Where(a => a.status_name != "Closed" && a.status_name != "Cancelled") on po.status_id equals st.status_id
                             select new
                             {
                                 po_id = po.po_id,
                                 po_date = po.po_date,
                                 po_no = po.po_no,
                                 vendor_name = v.VENDOR_CODE + " - " + v.VENDOR_NAME,
                                 net_value = po.net_value,
                                 gross_value = po.gross_value,
                             }).ToList().Select(a => new pur_po_report_vm
                             {
                                 po_no = a.po_no + "  -  " + DateTime.Parse(a.po_date.ToString()).ToString("dd/MMM/yyyy"),
                                 po_id = a.po_id,
                             }).ToList().OrderByDescending(a => a.po_id).ToList();
                return query;
            }
        }
        public List<pur_po_report_vm> GetPOListByItemOrService(int id, int vendor_id)
        {
            if (id != 2)
            {
                var query = (from po in _scifferContext.pur_po.Where(x => x.is_active == true && x.vendor_id == vendor_id && x.item_service_id == id && x.pi_status == false)
                             join grn in _scifferContext.pur_grn.Where(x => x.order_status == false) on po.po_id equals grn.po_id
                             join v in _scifferContext.REF_VENDOR on po.vendor_id equals v.VENDOR_ID
                             select new
                             {
                                 po_id = po.po_id,
                                 po_date = po.po_date,
                                 po_no = po.po_no + "-" + grn.grn_number,
                                 vendor_name = v.VENDOR_CODE + "-" + v.VENDOR_NAME,
                                 net_value = po.net_value,
                                 gross_value = po.gross_value,
                                 po_id_grn = po.po_id,
                             }).Distinct().ToList().Select(a => new pur_po_report_vm
                             {
                                 podate = DateTime.Parse(a.po_date.ToString()).ToString("dd/MMM/yyyy"),
                                 po_no = a.po_no + " - " + DateTime.Parse(a.po_date.ToString()).ToString("dd/MMM/yyyy"),
                                 vendor_name = a.vendor_name,
                                 net_value = a.net_value,
                                 po_id = a.po_id,
                                 po_id_grn = a.po_id_grn,
                             }).Distinct().ToList();
                return query;
            }
            else
            {
                // var with_without_service_id=_scifferContext.pur_po.Where(x=>x.po_id==id).FirstOrDefault().with_without_service_id;
                var query = (from po in _scifferContext.pur_po.Where(x => x.is_active == true && x.item_service_id == id && x.vendor_id == vendor_id && x.pi_status == false && x.approval_status == 1)
                             join s in _scifferContext.pur_srn.Where(x => x.order_status == false) on po.po_id equals s.po_id into j1
                             from j2 in j1.DefaultIfEmpty()
                             join v in _scifferContext.REF_VENDOR on po.vendor_id equals v.VENDOR_ID
                             select new
                             {
                                 po_id = po.po_id,
                                 po_date = po.po_date,
                                 po_no = po.po_no,
                                 srn_no = j2 == null ? "" : "-" + j2.document_no,
                                 vendor_name = v.VENDOR_CODE + " - " + v.VENDOR_NAME,
                                 net_value = po.net_value,
                                 gross_value = po.gross_value,
                                 po_id_grn = po.po_id,
                             }).ToList().Select(a => new pur_po_report_vm
                             {
                                 po_no = a.po_no + a.srn_no + "  -  " + DateTime.Parse(a.po_date.ToString()).ToString("dd/MMM/yyyy"),
                                 po_id = a.po_id,

                             }).Distinct().ToList();
                return query;

            }
        }
        public pur_po_report_vm GetPOForReport(int id)
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
        public List<pur_poVM> GetPendigApprovedList(int id)
        {

            int createdby = int.Parse(HttpContext.Current.Session["User_Id"].ToString());

            var entity = new SqlParameter("@entity", "getPOApprovalList");
            var createduser = new SqlParameter("@id", createdby);
            var value = _scifferContext.Database.SqlQuery<pur_poVM>(
                "exec get_purchase_order @entity,@id", entity, createduser).ToList();
            return value;

        }

        public List<pur_poVM> GetPending__Slab_PO_ApprovalList()
        {
            int createdby = int.Parse(HttpContext.Current.Session["User_Id"].ToString());

            var value_slab_po_approval = _scifferContext.ref_value_slab_po_approval.FirstOrDefault(x => x.User_id == createdby && x.is_active);

            if (value_slab_po_approval != null)
            {
                var entity = new SqlParameter("@entity", "getPOApprovalList");
                var createduser = new SqlParameter("@id", createdby);
                var value = _scifferContext.Database.SqlQuery<pur_poVM>(
                    "exec get_purchase_order @entity,@id", entity, createduser).ToList();
                var result = value.Where(x => x.gross_value >= value_slab_po_approval.From_Value_Slab && x.gross_value <= value_slab_po_approval.To_Value_Slab).ToList();
                return result;
            }

            return null;

        }
        public bool ChangeApprovedStatus(pur_poVM vm)
        {
            try
            {
                var data = _scifferContext.pur_po.FirstOrDefault(x => x.po_id == vm.po_id);
                var cancelled_status = _scifferContext.ref_status.FirstOrDefault(a => a.status_name == "Cancelled" && a.form == "PO");
                var pur_po_temp = _scifferContext.pur_po_temp_close_cancel.FirstOrDefault(x => x.po_id == vm.po_id);

                if (data.closed_remarks != null)
                {
                    int createdby = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                    var status = _scifferContext.ref_status.Where(x => x.form == "PO" && x.status_name == "Closed").FirstOrDefault();
                    var st = "update [dbo].[pur_po] set [order_status] = 1 ,[status_id]=" + pur_po_temp.status_id + ", [closed_remarks] = '" + pur_po_temp.Remarks + "', [closed_by] = " + pur_po_temp.modified_by + ", [closed_ts] = '" + pur_po_temp.modified_ts + "', [approval_status] = 1 where po_id = " + vm.po_id;
                    _scifferContext.Database.ExecuteSqlCommand(st);
                    _scifferContext.Database.ExecuteSqlCommand("update [dbo].[pur_po_detail] set [order_status] = 1 where po_id = " + vm.po_id);
                }
                else if (data.cancellation_remarks != null)
                {
                    int create_user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                    var iex_id = new SqlParameter("@id", vm.po_id);
                    var remarks = new SqlParameter("@cancellation_remarks", pur_po_temp.Remarks);
                    var created_by = new SqlParameter("@created_by", create_user);
                    var cancellation_reason_id = new SqlParameter("@cancellation_reason_id", pur_po_temp.order_status_cancellation_reason_id);
                    var val = _scifferContext.Database.SqlQuery<string>(
                      "exec cancel_po @id ,@cancellation_remarks ,@created_by,@cancellation_reason_id", iex_id, remarks, created_by, cancellation_reason_id).FirstOrDefault();
                }
                else
                {
                    data.approval_status = vm.approval_status;
                    data.approval_comments = vm.approval_comments;
                    data.approved_by = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                    data.approved_ts = DateTime.Now;

                    if (vm.approval_status == 2)
                    {
                        data.status_id = cancelled_status.status_id;
                        data.cancelled_date = DateTime.Now;
                        data.cancelled_by = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                        data.cancellation_remarks = "Approval Rejected";
                    }

                    _scifferContext.Entry(data).State = EntityState.Modified;
                    _scifferContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public string Delete(int id, string cancellation_remarks, int reason_id)
        {
            try
            {
                var status = _scifferContext.ref_status.Where(x => x.form == "PO" && x.status_name == "Cancelled").FirstOrDefault();
                int createdby = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                cancellation_remarks = cancellation_remarks == "" ? "Cancelled" : cancellation_remarks;
                pur_po_temp_close_cancel pur_Po_Temp_Close_Cancel = new pur_po_temp_close_cancel();
                pur_Po_Temp_Close_Cancel.po_id = Convert.ToInt32(id);
                pur_Po_Temp_Close_Cancel.order_status_cancellation_reason_id = reason_id;
                pur_Po_Temp_Close_Cancel.status_id = status.status_id;
                pur_Po_Temp_Close_Cancel.Remarks = cancellation_remarks.Trim();
                pur_Po_Temp_Close_Cancel.modified_by = createdby;
                pur_Po_Temp_Close_Cancel.modified_ts = DateTime.Now;
                _scifferContext.pur_po_temp_close_cancel.Add(pur_Po_Temp_Close_Cancel);
                _scifferContext.SaveChanges();

                var st = "update [dbo].[pur_po] set [cancellation_remarks] = '" + cancellation_remarks.Trim() + "',[approval_status] = 0 where po_id = " + id;
                _scifferContext.Database.ExecuteSqlCommand(st);
                return "Cancelled";
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
        public ref_price_list_vendor_detail_vm GetVendorItemPrice(int vendor_id, int item_id)
        {
            var query = (from p in _scifferContext.ref_price_list_vendor.Where(x => x.vendor_id == vendor_id)
                         join pd in _scifferContext.ref_price_list_vendor_details.Where(x => x.item_id == item_id) on p.price_list_id equals pd.price_list_id
                         select new ref_price_list_vendor_detail_vm
                         {
                             discount = pd.discount,
                             price = pd.price,
                             price_after_discount = pd.price_after_discount,
                             discount_type_id = pd.discount_type_id,
                             effective_date = pd.effective_date,
                         }).OrderByDescending(x => x.effective_date).FirstOrDefault();
            return query;
        }


        public string PurchaseOrderupdatestatusseen()
        {
            try
            {
                int user_id = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var op = _scifferContext.ref_user_role_mapping.Where(x => x.user_id == user_id).FirstOrDefault();
                var role = _scifferContext.ref_user_management_role.Where(x => x.role_id == op.role_id).FirstOrDefault();
                //if(op.role_id == role.role_id)
                if (role.role_code == "PUR_EXEC" || role.role_code == "IT_ADMIN")

                {
                    var purchaseReqDetail = _scifferContext.pur_po.Where(x => x.is_seen == false).ToList();
                    foreach (var item in purchaseReqDetail)
                    {
                        item.is_seen = true;
                        _scifferContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    }

                    _scifferContext.SaveChanges();
                }

                return "true";
            }

            catch (Exception)
            {
                return "false";
            }
        }
        public List<Po_History_vm> GetPoHistory(string ent, int item_id)
        {
            try
            {
                var entity = new SqlParameter("@entity", ent);
                var item_id1 = new SqlParameter("@item_id", item_id);
                var val = _scifferContext.Database.SqlQuery<Po_History_vm>(
                "exec get_po_history @entity,@item_id",
                entity, item_id1).ToList();
                return val;

            }
            catch (Exception ex)
            {
                throw;
            }
        }



    }
}
