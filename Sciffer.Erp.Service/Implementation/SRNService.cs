using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Data;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Sciffer.Erp.Domain.Model;
using AutoMapper;

namespace Sciffer.Erp.Service.Implementation
{
    public class SRNService : ISRNService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericService;
        public SRNService(ScifferContext ScifferContext, IGenericService generric)
        {
            _scifferContext = ScifferContext;
            _genericService = generric;
        }
        public string Add(pur_srn_vm SRN, List<pur_srn_detail_vm> detail)
        {
            try
            {
                var FileAttachment = "";
                if (SRN.FileUpload != null)
                {
                    FileAttachment = _genericService.GetFilePath("SRN", SRN.FileUpload);
                }
                DateTime dte = new DateTime(1990, 1, 1);
                DataTable dt = new DataTable();
                dt.Columns.Add("srn_detail_id", typeof(int));
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
                dt.Columns.Add("tax_id", typeof(int));              
                dt.Columns.Add("user_description", typeof(string));             
                dt.Columns.Add("sac_hsn_id", typeof(int));
                dt.Columns.Add("staggerred_delivery_detail_id", typeof(int));
                if (detail!=null)
                {
                    foreach(var d in detail)
                    {
                        int srn_detail_id = -1;
                        if (d.srn_detail_id != 0)
                        {
                            srn_detail_id = (int)d.srn_detail_id;
                        }
                        var item = d.item_id;
                        var po_detail_id = d.po_detail_id;
                        var ddate = d.delivery_date;
                        var sloc = d.storage_location_id;
                        var qty = d.quantity;
                        var uom = d.uom_id;
                        var uprice = d.unit_price;
                        var discount = d.discount;
                        var eprice = d.eff_unit_price;
                        var pvalue = d.purchase_value;
                        var tax = d.tax_id;
                        var desc = d.user_description;
                        var hsn_id = d.hsn_id;
                        var staggerred_delivery_detail_id = d.staggerred_delivery_detail_id;
                        dt.Rows.Add(srn_detail_id, item, po_detail_id, ddate, sloc, qty, uom, uprice, discount, eprice, pvalue, tax ,desc, hsn_id, staggerred_delivery_detail_id);
                    }
                }
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_pur_srn_detail";
                t1.Value = dt;
                int create_user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var srn_id = new SqlParameter("@srn_id", SRN.srn_id == 0 ? -1 : SRN.srn_id);              
                var document_no = new SqlParameter("@document_no", SRN.document_no == null ? "" : SRN.document_no);
                var vendor_id = new SqlParameter("@vendor_id", SRN.vendor_id);
                var net_value = new SqlParameter("@net_value", SRN.net_value);
                var category_id = new SqlParameter("@category_id", SRN.category_id);
                var net_currency_id = new SqlParameter("@net_currency_id", SRN.net_currency_id);
                var gross_value = new SqlParameter("@gross_value", SRN.gross_value);
                var gross_currency_id = new SqlParameter("@gross_currency_id", SRN.gross_currency_id);
                var posting_date = new SqlParameter("@posting_date", SRN.posting_date);
                var business_unit_id = new SqlParameter("@business_unit_id", SRN.business_unit_id);
                var plant_id = new SqlParameter("@plant_id", SRN.plant_id);
                var freight_terms_id = new SqlParameter("@freight_terms_id", SRN.freight_terms_id);
                var delivery_date = new SqlParameter("@delivery_date", SRN.delivery_date == null ? dte : SRN.delivery_date);
                var vendor_doc_no = new SqlParameter("@vendor_doc_no", SRN.vendor_doc_no == null ? string.Empty : SRN.vendor_doc_no);
                var vendor_doc_date = new SqlParameter("@vendor_doc_date", SRN.vendor_doc_date == null ? dte : SRN.vendor_doc_date);
                var gate_entry_number = new SqlParameter("@gate_entry_number", SRN.gate_entry_number == null ? "" : SRN.gate_entry_number);
                var gate_entry_date = new SqlParameter("@gate_entry_date", SRN.gate_entry_date == null ? dte : SRN.gate_entry_date);
                var created_by = new SqlParameter("@created_by", create_user);
                var billing_address = new SqlParameter("@billing_address", SRN.billing_address == null ? "" : SRN.billing_address);
                var billing_city = new SqlParameter("@billing_city", SRN.billing_city == null ? "" : SRN.billing_city);
                var billing_state_id = new SqlParameter("@billing_state_id", SRN.billing_state_id == null ? 0 : SRN.billing_state_id);
                var billing_pin_code = new SqlParameter("@billing_pin_code", SRN.billing_pin_code == null ? "" : SRN.billing_pin_code);
                var email_id = new SqlParameter("@email_id", SRN.email_id == null ? "" : SRN.email_id);
                var payment_terms_id = new SqlParameter("@payment_terms_id", SRN.payment_terms_id);
                var payment_cycle_id = new SqlParameter("@payment_cycle_id", SRN.payment_cycle_id);
                var status_id = new SqlParameter("@status_id", SRN.status_id);
                var internal_remarks = new SqlParameter("@internal_remarks", SRN.internal_remarks == null ? "" : SRN.internal_remarks);
                var remarks_on_doc = new SqlParameter("@remarks_on_doc", SRN.remarks_on_doc == null ? "" : SRN.remarks_on_doc);
                var is_active = new SqlParameter("@is_active", 1);
                var pan_no = new SqlParameter("@pan_no", SRN.pan_no == null ? string.Empty : SRN.pan_no);
                var ecc_no = new SqlParameter("@ecc_no", SRN.ecc_no == null ? "" : SRN.ecc_no);
                var vat_tin_no = new SqlParameter("@vat_tin_no", SRN.vat_tin_no == null ? "" : SRN.vat_tin_no);
                var cst_tin_no = new SqlParameter("@cst_tin_no", SRN.cst_tin_no == null ? "" : SRN.cst_tin_no);
                var service_tax_no = new SqlParameter("@service_tax_no", SRN.service_tax_no == null ? "" : SRN.service_tax_no);
                var gst_no = new SqlParameter("@gst_no", SRN.gst_no == null ? "" : SRN.gst_no);                                
                var attachment = new SqlParameter("@attachment", FileAttachment == "" ? "No File" : FileAttachment);
                var place_of_supply_id = new SqlParameter("@place_of_supply_id", SRN.place_of_supply_id);
                var gst_vendor_type_id = new SqlParameter("@gst_vendor_type_id", SRN.gst_vendor_type_id == null ? 0 : SRN.gst_vendor_type_id);
                var gst_in = new SqlParameter("@gst_in", SRN.gst_in);
                var po_id = new SqlParameter("@po_id", SRN.po_id);
                var val = _scifferContext.Database.SqlQuery<string>("exec save_srn @srn_id,@po_id ,@document_no ,@vendor_id ,@net_value ,@category_id ,@net_currency_id ,@gross_value ,@gross_currency_id ,@posting_date ,@business_unit_id ,@plant_id ,@freight_terms_id ,@delivery_date ,@vendor_doc_no ,@vendor_doc_date ,@gate_entry_number ,@gate_entry_date ,@created_by ,@billing_address ,@billing_city ,@billing_state_id ,@billing_pin_code ,@email_id ,@payment_terms_id ,@payment_cycle_id  ,@internal_remarks ,@remarks_on_doc ,@attachment ,@pan_no ,@ecc_no ,@vat_tin_no ,@cst_tin_no ,@service_tax_no ,@gst_no,@place_of_supply_id,@gst_vendor_type_id,@gst_in,@t1",
                    srn_id, po_id, document_no, vendor_id, net_value, category_id, net_currency_id, gross_value, gross_currency_id, posting_date, business_unit_id,
                    plant_id, freight_terms_id, delivery_date, vendor_doc_no, vendor_doc_date, gate_entry_number, gate_entry_date, created_by, billing_address,
                    billing_city, billing_state_id, billing_pin_code, email_id, payment_terms_id, payment_cycle_id, internal_remarks, remarks_on_doc,
                    attachment, pan_no, ecc_no, vat_tin_no, cst_tin_no, service_tax_no, gst_no, place_of_supply_id,
                    gst_vendor_type_id, gst_in, t1).FirstOrDefault();
                if (val.Contains("Saved"))
                //if (val==1)
                {
                    if (SRN.FileUpload != null)
                    {
                        SRN.FileUpload.SaveAs(SRN.attachment);
                    }
                    return val.ToString();
                }
                else
                {
                    System.IO.File.Delete(SRN.attachment);
                    return val.ToString();
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                GC.Collect();
            }
        }

        public string Delete(int id, string cancellation_remarks, int reason_id)
        {
            throw new NotImplementedException();
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

        public pur_srn_vm Get(int? id)
        {
            pur_srn purchase = _scifferContext.pur_srn.FirstOrDefault(c => c.srn_id == id);
            Mapper.CreateMap<pur_srn, pur_srn_vm>().ForMember(dest => dest.FileUpload, opt => opt.Ignore());
            pur_srn_vm purchasevm = Mapper.Map<pur_srn, pur_srn_vm>(purchase);
          //  purchasevm.pur_srn_detail = purchasevm.pur_srn_detail.Where(c => c.is_active == true).ToList();
            purchasevm.vendor_code = purchasevm.REF_VENDOR.VENDOR_CODE;
            purchasevm.vendor_name = purchasevm.REF_VENDOR.VENDOR_NAME;
            purchasevm.po_no = _scifferContext.pur_po.Where(x => x.po_id == purchase.po_id).FirstOrDefault().po_no;
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
            purchasevm.pur_srn_detail_vm = (from a in _scifferContext.pur_srn_detail.Where(c => c.srn_id == id)
                                        join item in _scifferContext.REF_ITEM on a.item_id equals item.ITEM_ID
                                        join sac in _scifferContext.ref_sac on a.sac_hsn_id equals sac.sac_id into sac1
                                        from sac2 in sac1.DefaultIfEmpty()
                                        join hsn in _scifferContext.ref_hsn_code on a.sac_hsn_id equals hsn.hsn_code_id into hsn1
                                        from hsn2 in hsn1.DefaultIfEmpty()
                                        select new
                                        {
                                            srn_detail_id = a.srn_detail_id,
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
                                            tax_id = a.tax_id,
                                            is_active = a.is_active,
                                            srn_id = a.srn_id,
                                            po_detail_id = a.po_detail_id,
                                            user_description = a.user_description,                                           
                                            tax_code = a.REF_TAX.tax_code + "/" + a.REF_TAX.tax_name,
                                            hsn_id = item.item_type_id == 2 ? sac2.sac_id : hsn2.hsn_code_id,
                                            hsn_code = item.item_type_id == 2 ? sac2.sac_code + "/" + sac2.sac_description : hsn2.hsn_code + "/" + hsn2.hsn_code_description,
                                            staggerred_delivery_detail_id=a.staggerred_delivery_detail_id,
                                        }).ToList().Select(a => new pur_srn_detail_vm
                                        {
                                            srn_detail_id = a.srn_detail_id,
                                            delivery_date = (DateTime)a.delivery_date,
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
                                            tax_id = a.tax_id,
                                            is_active = a.is_active,
                                            srn_id = a.srn_id,
                                            po_detail_id = a.po_detail_id,
                                            user_description = a.user_description,                                          
                                            hsn_id = a.hsn_id,
                                            hsn_code = a.hsn_code,
                                            tax_code = a.tax_code,
                                            staggerred_delivery_detail_id=a.staggerred_delivery_detail_id
                                        }).ToList();

            return purchasevm;
           // throw new NotImplementedException();
        }

        public List<pur_srn_vm> GetAll()
        {
            try
            {
                var s111 = _scifferContext.pur_srn.Where(x => x.is_active == true).ToList();
                var query = (from srn in _scifferContext.pur_srn.Where(x => x.is_active == true)
                             join po in _scifferContext.pur_po on srn.po_id equals po.po_id
                                 //join st in _scifferContext.REF_STATE on grn.billing_state_id equals st.STATE_ID
                             join st1 in _scifferContext.REF_STATE on srn.billing_state_id equals st1.STATE_ID
                             join pay in _scifferContext.REF_PAYMENT_TERMS on srn.payment_terms_id equals pay.payment_terms_id
                             join p1 in _scifferContext.REF_PAYMENT_CYCLE on srn.payment_cycle_id equals p1.PAYMENT_CYCLE_ID
                             join p2 in _scifferContext.REF_PAYMENT_CYCLE_TYPE on p1.PAYMENT_CYCLE_TYPE_ID equals p2.PAYMENT_CYCLE_TYPE_ID
                             join f in _scifferContext.REF_FREIGHT_TERMS on srn.freight_terms_id equals f.FREIGHT_TERMS_ID
                             join o in _scifferContext.REF_PLANT on srn.plant_id equals o.PLANT_ID
                             join p in _scifferContext.REF_VENDOR on srn.vendor_id equals p.VENDOR_ID
                             join q in _scifferContext.ref_document_numbring on srn.category_id equals q.document_numbring_id
                             join r in _scifferContext.REF_CURRENCY on srn.net_currency_id equals r.CURRENCY_ID
                             join s in _scifferContext.REF_CURRENCY on srn.gross_currency_id equals s.CURRENCY_ID
                             join b in _scifferContext.REF_BUSINESS_UNIT on srn.business_unit_id equals b.BUSINESS_UNIT_ID
                             join u in _scifferContext.REF_USER on srn.created_by equals u.USER_ID into jo
                             from terr in jo.DefaultIfEmpty()
                            
                             select new pur_srn_vm()
                             {
                                 srn_id = srn.srn_id,
                                 document_no = srn.document_no,
                                 vendor_name = p.VENDOR_NAME,
                                 vendor_code = p.VENDOR_CODE,
                                 category_name = q.category,
                                 net_value = srn.net_value,
                                 net_currency = r.CURRENCY_NAME,
                                 gross_value = srn.gross_value,
                                 gross_currency = s.CURRENCY_NAME,
                                 posting_date = srn.posting_date,
                                 BUSINESS_UNIT_DESCRIPTION = b.BUSINESS_UNIT_DESCRIPTION,
                                 business_unit_code = b.BUSINESS_UNIT_NAME,
                                 plant_name = o.PLANT_NAME,
                                 plant_code = o.PLANT_CODE,
                                 freight_terms = f.FREIGHT_TERMS_NAME,
                                 delivery_date = srn.delivery_date,
                                 vendor_doc_no = srn.vendor_doc_no,
                                 vendor_doc_date = srn.vendor_doc_date,
                                 gate_entry_number = srn.gate_entry_number,
                                 gate_entry_date = srn.gate_entry_date,
                                 created_by1 = terr.USER_NAME,
                                 payment_terms = pay.payment_terms_code,
                                 billing_address = srn.billing_address,
                                 billing_city = srn.billing_city,
                                 billing_state = st1.STATE_NAME,
                                 billing_pin_code = srn.billing_pin_code,
                                 email_id = srn.email_id,
                                 payment_cycle = p1.PAYMENT_CYCLE_NAME,
                                 payment_cycle_type = p1.REF_PAYMENT_CYCLE_TYPE.PAYMENT_CYCLE_TYPE_NAME,                               
                                 internal_remarks = srn.internal_remarks,
                                 remarks_on_doc = srn.remarks_on_doc,
                                 attachment = srn.attachment,                                
                                 purchase_name = po.po_no,
                                 country_name = st1.REF_COUNTRY.COUNTRY_NAME,
                                 pan_no = srn.pan_no,
                                 gst_no = srn.gst_no,
                                 ecc_no = srn.ecc_no,
                                 vat_tin_no = srn.vat_tin_no,
                                 cst_tin_no = srn.cst_tin_no,
                                 service_tax_no = srn.service_tax_no,                                
                                 cancellation_remarks = srn.cancellation_remarks,
                             }).OrderByDescending(a => a.srn_id).ToList();
                return query;
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                GC.Collect();
            }
        }

        public List<pur_srn_vm> GetGrnList()
        {
            throw new NotImplementedException();
        }
    }
}
