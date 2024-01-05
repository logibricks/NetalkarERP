using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Data;
using AutoMapper;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class IncomingExciseService : IIncomingExciseService
    {
        private readonly ScifferContext _scifferContext;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        public IncomingExciseService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public string Add(pur_incoming_excise_vm excises)
        {
            try
            {

                DataTable dt1 = new DataTable();
                dt1.Columns.Add("sr_no", typeof(int));
                dt1.Columns.Add("tax_element_id", typeof(int));
                dt1.Columns.Add("assessable_rate", typeof(double));
                dt1.Columns.Add("assessable_value", typeof(double));
                dt1.Columns.Add("tax_element_rate", typeof(double));
                dt1.Columns.Add("tax_element_value", typeof(double));
                if(excises.tax_element_id!=null)
                {
                    for(var j = 0;j<=excises.tax_element_id.Count-1;j++)
                    {
                        dt1.Rows.Add(excises.sr_no[j]==""?0:int.Parse(excises.sr_no[j]),excises.tax_element_id[j]==null?0:int.Parse(excises.tax_element_id[j]),
                            excises.assessable_rate[j]==null?0:double.Parse(excises.assessable_rate[j]),excises.assessable_value[j]==null?0:double.Parse(excises.assessable_value[j]),
                            excises.tax_element_rate[j]==null?0:double.Parse(excises.tax_element_rate[j]),excises.tax_element_value[j]==null?0:double.Parse(excises.tax_element_value[j]));
                    }
                }
                
                DataTable dt = new DataTable();
                dt.Columns.Add("incoming_excise_detail_id", typeof(int));
                dt.Columns.Add("grn_detail_id", typeof(int));
                dt.Columns.Add("item_id", typeof(int));
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
                dt.Columns.Add("tax_value", typeof(double));
                dt.Columns.Add("sr_no", typeof(int));
                if (excises.item_id11 != null)
                {
                    for (var i = 0; i <= excises.item_id11.Count - 1; i++)
                    {
                        if (excises.item_id11 != null)
                        {
                            var excise_detail = excises.incoming_excise_detail_id1[i] == "0" ? -1 : Convert.ToInt32(excises.incoming_excise_detail_id1[i]);
                            var grn_detail_id = int.Parse(excises.grn_detail_id1[i]);
                            var item_id = int.Parse(excises.item_id11[i]);
                            var delivery_date1 = DateTime.Parse(excises.delivery_date1[i]);
                            var storage_location_id = int.Parse(excises.storage_location_id1[i]);
                            var quantity = double.Parse(excises.quantity1[i]);
                            var uom_id = int.Parse(excises.uom_id1[i]);
                            var unit_price = double.Parse(excises.unit_price1[i]);
                            var discount = double.Parse(excises.discount1[i]);
                            var eff_unit_price = double.Parse(excises.eff_unit_price1[i]);
                            var purchase_value = double.Parse(excises.purchase_value1[i]);
                            var assessable_rate = double.Parse(excises.assessable_rate1[i]);
                            var assessable_value = double.Parse(excises.assessable_value1[i]);
                            var tax_id = int.Parse(excises.tax_id11[i]);
                            var tax_value = excises.tax_value[i] == "" ? 0 : double.Parse(excises.tax_value[i]);
                            var sr_no = excises.srno[i] == null ? 0 : int.Parse(excises.srno[i]);
                            dt.Rows.Add(excise_detail, grn_detail_id, item_id, delivery_date1, storage_location_id, quantity, uom_id,
                                unit_price, discount, eff_unit_price, purchase_value, assessable_rate, assessable_value, tax_id, tax_value, sr_no);
                        }
                    }

                }

                   
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_incoming_excise_detail";
                t1.Value = dt;
                var t2 = new SqlParameter("@t2", SqlDbType.Structured);
                t2.TypeName = "dbo.temp_incoming_excise_tax";
                t2.Value = dt1;
                DateTime dte = new DateTime(1990, 1, 1);
                excises.created_by= int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var incoming_excise_id = new SqlParameter("@incoming_excise_id", excises.incoming_excise_id==0?-1:excises.incoming_excise_id);
                var incoming_excise_number = new SqlParameter("@incoming_excise_number", excises.incoming_excise_number==null?"0": excises.incoming_excise_number);
                var excise_ref_no = new SqlParameter("@excise_ref_no", excises.excise_ref_no);
                var excise_ref_date = new SqlParameter("@excise_ref_date", excises.excise_ref_date);
                var vendor_id = new SqlParameter("@vendor_id", excises.vendor_id);
                var category_id = new SqlParameter("@category_id", excises.category_id);
                var incoming_excise_posting_date = new SqlParameter("@incoming_excise_posting_date", excises.incoming_excise_posting_date);
                var business_unit_id = new SqlParameter("@business_unit_id", excises.business_unit_id);
                var plant_id = new SqlParameter("@plant_id", excises.plant_id);
                var freight_terms_id = new SqlParameter("@freight_terms_id", excises.freight_terms_id);
                var delivery_date = new SqlParameter("@delivery_date", excises.delivery_date==null?dte:excises.delivery_date);
                var vendor_doc_no = new SqlParameter("@vendor_doc_no", excises.vendor_doc_no==null?string.Empty:excises.vendor_doc_no);
                var vendor_doc_date = new SqlParameter("@vendor_doc_date", excises.vendor_doc_date==null?dte:excises.vendor_doc_date);
                var gate_entry_number = new SqlParameter("@gate_entry_number", excises.gate_entry_number==null?string.Empty:excises.gate_entry_number);
                var gate_entry_date = new SqlParameter("@gate_entry_date", excises.gate_entry_date==null?dte:excises.gate_entry_date);
                var billing_address = new SqlParameter("@billing_address", excises.billing_address);
                var billing_city = new SqlParameter("@billing_city", excises.billing_city);
                var billing_state_id = new SqlParameter("@billing_state_id", excises.billing_state_id);
                var billing_pin_code = new SqlParameter("@billing_pin_code", excises.billing_pin_code==null?string.Empty:excises.billing_pin_code);
                var email_id = new SqlParameter("@email_id", excises.email_id==null?string.Empty:excises.email_id);
                var payment_terms_id = new SqlParameter("@payment_terms_id", excises.payment_terms_id);
                var payment_cycle_id = new SqlParameter("@payment_cycle_id", excises.payment_cycle_id);
                var grn_id = new SqlParameter("@grn_id", excises.grn_id==null?0:excises.grn_id);
                var incoming_excise_net_value = new SqlParameter("@incoming_excise_net_value", excises.incoming_excise_net_value);
                var net_currency_id = new SqlParameter("@net_currency_id", excises.net_currency_id);
                var incoming_excise_gross_value = new SqlParameter("@incoming_excise_gross_value", excises.incoming_excise_gross_value);
                var gross_currency_id = new SqlParameter("@gross_currency_id", excises.gross_currency_id);
                var pan_no = new SqlParameter("@pan_no", excises.pan_no==null?string.Empty:excises.pan_no);
                var ecc_no = new SqlParameter("@ecc_no", excises.ecc_no==null?string.Empty:excises.ecc_no);
                var vat_tin_no = new SqlParameter("@vat_tin_no", excises.vat_tin_no==null?string.Empty:excises.vat_tin_no);
                var cst_tin_no = new SqlParameter("@cst_tin_no", excises.cst_tin_no==null?string.Empty:excises.cst_tin_no);
                var service_tax_no = new SqlParameter("@service_tax_no", excises.service_tax_no==null?string.Empty:excises.service_tax_no);
                var created_by = new SqlParameter("@created_by", excises.created_by);
                var gst_no = new SqlParameter("@gst_no", excises.gst_no == null ? string.Empty : excises.gst_no);
                var internal_remarks = new SqlParameter("@internal_remarks", excises.internal_remarks == null ? string.Empty : excises.internal_remarks);
                var remarks_on_doc = new SqlParameter("@remarks_on_doc", excises.remarks_on_doc == null ? string.Empty : excises.remarks_on_doc);
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_incoming_excise @incoming_excise_id ,@incoming_excise_number ,@excise_ref_no ,@excise_ref_date ,@vendor_id ,@category_id ,@incoming_excise_posting_date ,@business_unit_id ,@plant_id ,@freight_terms_id ,@delivery_date ,@vendor_doc_no ,@vendor_doc_date ,@gate_entry_number ,@gate_entry_date ,@billing_address ,@billing_city ,@billing_state_id ,@billing_pin_code ,@email_id ,@payment_terms_id ,@payment_cycle_id ,@grn_id ,@incoming_excise_net_value ,@net_currency_id ,@incoming_excise_gross_value ,@gross_currency_id ,@pan_no ,@ecc_no ,@vat_tin_no ,@cst_tin_no ,@service_tax_no,@gst_no ,@internal_remarks,@remarks_on_doc,@created_by ,@t1,@t2 ",
                    incoming_excise_id, incoming_excise_number, excise_ref_no, excise_ref_date, vendor_id, category_id, 
                    incoming_excise_posting_date, business_unit_id, plant_id, freight_terms_id, delivery_date, vendor_doc_no, 
                    vendor_doc_date, gate_entry_number, gate_entry_date, billing_address, billing_city, billing_state_id, billing_pin_code,
                    email_id, payment_terms_id, payment_cycle_id, grn_id, incoming_excise_net_value, net_currency_id, 
                    incoming_excise_gross_value, gross_currency_id, pan_no, ecc_no, vat_tin_no, cst_tin_no, service_tax_no, gst_no, internal_remarks,remarks_on_doc,
                    created_by, t1,t2).FirstOrDefault();

                if (val.Contains("Saved"))
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
                return ex.Message;
            }
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

        public pur_incoming_excise_vm Get(int id)
        {
            pur_incoming_excise so = _scifferContext.pur_incoming_excise.FirstOrDefault(c => c.incoming_excise_id == id);
            Mapper.CreateMap<pur_incoming_excise, pur_incoming_excise_vm>();
            pur_incoming_excise_vm sovm = Mapper.Map<pur_incoming_excise, pur_incoming_excise_vm>(so);
           
            sovm.pur_incoming_excise_detail = sovm.pur_incoming_excise_detail.Where(c => c.is_active == true).ToList();
            var billingcountry = _scifferContext.REF_STATE.Where(s => s.STATE_ID == sovm.billing_state_id)
                                      .Select(s => new
                                      {
                                          COUNTRY_ID = s.COUNTRY_ID
                                      }).Single();
           
            var paymentcyletype = _scifferContext.REF_PAYMENT_CYCLE.Where(P => P.PAYMENT_CYCLE_ID == sovm.payment_cycle_id)
                                     .Select(P => new
                                     {
                                         PAYMENT_CYCLE_TYPE_ID = P.PAYMENT_CYCLE_TYPE_ID
                                     }).Single();
            sovm.payment_cycle_type_id = paymentcyletype.PAYMENT_CYCLE_TYPE_ID;
            sovm.country_id = billingcountry.COUNTRY_ID;
            return sovm;
        }

        public List<pur_incoming_excise_vm> GetAll()
        {
            var query = (from excise in _scifferContext.pur_incoming_excise.Where(a => a.is_active == true)
                         join grn in _scifferContext.pur_grn on excise.grn_id equals grn.grn_id into grn1
                         from grn2 in grn1.DefaultIfEmpty()
                         join business in _scifferContext.REF_BUSINESS_UNIT on excise.business_unit_id equals business.BUSINESS_UNIT_ID
                         join plant in _scifferContext.REF_PLANT on excise.plant_id equals plant.PLANT_ID
                         join fr in _scifferContext.REF_FREIGHT_TERMS on excise.freight_terms_id equals fr.FREIGHT_TERMS_ID
                         join state in _scifferContext.REF_STATE on excise.billing_state_id equals state.STATE_ID
                         join payment_terms in _scifferContext.REF_PAYMENT_TERMS on excise.payment_terms_id equals payment_terms.payment_terms_id into payment_terms1
                         from payment_terms2 in payment_terms1.DefaultIfEmpty()
                         join payment_cycle in _scifferContext.REF_PAYMENT_CYCLE on excise.payment_cycle_id equals payment_cycle.PAYMENT_CYCLE_ID into payment_cycle1
                         from payment_cycle2 in payment_cycle1.DefaultIfEmpty()
                         join net_currency in _scifferContext.REF_CURRENCY on excise.net_currency_id equals net_currency.CURRENCY_ID
                         join gross_currency in _scifferContext.REF_CURRENCY on excise.gross_currency_id equals gross_currency.CURRENCY_ID
                         join category in _scifferContext.ref_document_numbring on excise.category_id equals category.document_numbring_id
                         join vendor in _scifferContext.REF_VENDOR on excise.vendor_id equals vendor.VENDOR_ID into vendor1
                         from vendor2 in vendor1.DefaultIfEmpty()
                         select new pur_incoming_excise_vm()
                         {
                             incoming_excise_id = excise.incoming_excise_id,
                             incoming_excise_number = excise.incoming_excise_number,
                             incoming_excise_posting_date = excise.incoming_excise_posting_date,
                             excise_ref_no = excise.excise_ref_no,
                             excise_ref_date = excise.excise_ref_date,
                             delivery_date = excise.delivery_date,
                             vendor_doc_no = excise.vendor_doc_no,
                             vendor_doc_date = excise.vendor_doc_date,
                             gate_entry_number = excise.gate_entry_number,
                             gate_entry_date = excise.gate_entry_date,
                             created_by = excise.created_by,
                             billing_address = excise.billing_address,
                             billing_city = excise.billing_city,
                             billing_pin_code = excise.billing_pin_code,
                             is_active = excise.is_active,
                             grn_no = grn2.grn_number,
                             incoming_excise_net_value = excise.incoming_excise_net_value,
                             incoming_excise_gross_value = excise.incoming_excise_gross_value,
                             pan_no = excise.pan_no,
                             ecc_no = excise.ecc_no,
                             vat_tin_no = excise.vat_tin_no,
                             cst_tin_no = excise.cst_tin_no,
                             service_tax_no = excise.service_tax_no,
                             document_numbring_name = category.category,
                             business_unit_name = business.BUSINESS_UNIT_DESCRIPTION,
                             business_code = business.BUSINESS_UNIT_NAME,
                             plant_name = plant.PLANT_NAME,
                             plant_code = plant.PLANT_CODE,
                             freight_terms_name = fr.FREIGHT_TERMS_NAME,
                             state_name = state.STATE_NAME,
                             country_name = state.REF_COUNTRY.COUNTRY_NAME,
                             payment_cycle_name = payment_cycle2.PAYMENT_CYCLE_NAME,
                             payment_cycle_type_name = payment_cycle2.REF_PAYMENT_CYCLE_TYPE.PAYMENT_CYCLE_TYPE_NAME,
                             net_currency_name = net_currency.CURRENCY_NAME,
                             payment_terms_name = payment_terms2.payment_terms_code,
                             gross_currency_name = gross_currency.CURRENCY_NAME,
                             vendor_name = vendor2.VENDOR_NAME,
                             vendor_code = vendor2.VENDOR_CODE,
                             cancellation_remarks = grn2.cancellation_remarks,
                         }
                         ).OrderByDescending(a => a.incoming_excise_id).ToList();
            return query;
        }





        public List<pur_po_detail_vm> GetGRNProductList(int id)
        {
            var grn_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getgrndetailforiex");
            var val = _scifferContext.Database.SqlQuery<pur_po_detail_vm>(
            "exec GetBalanceQuantity @entity,@id", entity, grn_id).ToList();
            return val;
        }

        public pur_grnVM GetGrnDetailForIEX(int id)
        {
            var query = (from g in _scifferContext.pur_grn.Where(x => x.grn_id == id)
                         join c in _scifferContext.REF_STATE on  g.billing_state_id equals c.STATE_ID
                         select new pur_grnVM
                         {
                              vendor_id=g.vendor_id,
                              business_unit_id=g.business_unit_id,
                              plant_id=g.plant_id,
                              freight_terms_id=g.freight_terms_id,
                              delivery_date=g.delivery_date,
                              vendor_doc_no=g.vendor_doc_no,
                              vendor_doc_date=g.vendor_doc_date,
                              gate_entry_number=g.gate_entry_number,
                              gate_entry_date=g.gate_entry_date,
                              billing_address=g.billing_address,
                              billing_city=g.billing_city,
                              billing_pin_code=g.billing_pin_code,
                               billing_state_id=g.billing_state_id,
                               country_id=c.REF_COUNTRY.COUNTRY_ID,
                               email_id=g.email_id,
                               payment_cycle_id=g.payment_cycle_id,
                               pan_no=g.pan_no,
                               payment_terms_id=g.payment_terms_id,
                               payment_cycle_type_id=g.REF_PAYMENT_CYCLE.PAYMENT_CYCLE_TYPE_ID,
                               ecc_no=g.ecc_no,
                               vat_tin_no=g.vat_tin_no,
                               cst_tin_no=g.cst_tin_no,
                               service_tax_no=g.service_tax_no,
                               gst_no=g.gst_no,
                               internal_remarks=g.internal_remarks,
                               remarks_on_doc = g.remarks_on_doc,
                               gross_currency_id =g.gross_currency_id,
                               net_currency_id=g.net_currency_id,
                         }).FirstOrDefault();
            return query;
           
        }
        public pur_incoming_excise_report_vm GetIEXForReport(int id)
        {
            var query = (from p in _scifferContext.pur_incoming_excise.Where(x => x.incoming_excise_id == id)
                         from c in _scifferContext.REF_COMPANY
                         join d in _scifferContext.ref_document_numbring on p.category_id equals d.document_numbring_id
                         join v in _scifferContext.REF_VENDOR on p.vendor_id equals v.VENDOR_ID
                         join pl in _scifferContext.REF_PLANT on p.plant_id equals pl.PLANT_ID
                         join gl in _scifferContext.pur_grn on p.grn_id equals gl.grn_id

                         select new
                         {
                             iexid = p.incoming_excise_id,
                             Doc_Category = d.category,
                             Doc_No = p.incoming_excise_number,
                             Doc_PostingDate = p.incoming_excise_posting_date,
                             GRNDate = gl.posting_date,
                             Vendor_Code = v.VENDOR_CODE,
                             VendorName = v.VENDOR_NAME,
                             excise_refno = p.excise_ref_no,
                             excise_date = p.excise_ref_date,
                             Vendor_ECC = v.ecc_no,
                             Purchase_Order = (from po in _scifferContext.pur_po where (po.po_id == gl.po_id) select (po.po_no)).FirstOrDefault(),
                             Company_display_Name = c.COMPANY_NAME,
                             Regd_Address = pl.PLANT_ADDRESS,
                             PlantName_Details = pl.PLANT_NAME,
                             GRN_DocNo = gl.grn_number,

                         }).ToList().Select(a => new pur_incoming_excise_report_vm()
                         {
                             iexid = a.iexid,
                             Doc_Category = a.Doc_Category,
                             Doc_No = a.Doc_No,
                             Doc_PostingDate = a.Doc_PostingDate,
                             GRNDate = a.GRNDate,
                             Vendor_Code = a.Vendor_Code,
                             VendorName = a.VendorName,
                             excise_refno = a.excise_refno,
                             excise_date = a.excise_date,
                             Vendor_ECC = a.Vendor_ECC,
                             Purchase_Order = a.Purchase_Order,
                             Company_display_Name = a.Company_display_Name,
                             Regd_Address = a.Regd_Address,
                             PlantName_Details = a.PlantName_Details,
                             GRN_DocNo = a.GRN_DocNo,
                         }).FirstOrDefault();
            return query;
        }
        public List<pur_incoming_excise_detail_vm> GetDetailIEXForReport(int id)
        {
            var query = (from p in _scifferContext.pur_incoming_excise_detail.Where(x => x.incoming_excise_id == id)
                         join i in _scifferContext.REF_ITEM on p.item_id equals i.ITEM_ID
                         join t in _scifferContext.ref_tax on p.tax_id equals t.tax_id
                         join u in _scifferContext.REF_UOM on p.uom_id equals u.UOM_ID

                         select new
                         {
                             iexid = p.incoming_excise_id,
                             itemcode = i.ITEM_CODE,
                             Itemdescription = i.ITEM_NAME,
                             Qty = p.quantity,
                             UoM = u.UOM_NAME,
                             Ass_rate = p.assessable_rate,
                             Eff_rate = p.eff_unit_price,
                             Ass_value = p.assessable_value,
                             Purchase_value = p.purchase_value,
                             Tax_Code = p.tax_id,
                             iexdetail = p.incoming_excise_detail_id,

                         }).ToList().Select(a => new pur_incoming_excise_detail_vm()
                         {
                             iexid = a.iexid,
                             itemcode = a.itemcode,
                             Itemdescription = a.Itemdescription,
                             Qty = a.Qty,
                             UoM = a.UoM,
                             Ass_rate = a.Ass_rate,
                             Eff_rate = a.Eff_rate,
                             Ass_value = a.Ass_value,
                             Purchase_value = a.Purchase_value,
                             Tax_Code = a.Tax_Code,
                             iexdetail = a.iexdetail,
                         }).ToList();
            return query;
        }

        public List<pur_incoming_excise_tax> GetIncomingExciseTax(string entity, string tax, double amt, DateTime posting_date, int tds_code_id)
        {
            var sp = new SqlParameter("@tax", tax);
            var ent = new SqlParameter("@entity", entity);
            var amount = new SqlParameter("@amount", amt);
            var dt = new SqlParameter("@posting_date", posting_date);
            var tds_code = new SqlParameter("@tds_code_id", tds_code_id);
            var val = _scifferContext.Database.SqlQuery<pur_incoming_excise_tax>(
            "exec CalculateTax @entity,@tax,@amount,@posting_date,@tds_code_id", ent, sp, amount, dt, tds_code).ToList();
            return val;
        }
        public string Delete(int id, string cancellation_remarks, int reason_id)
        {
            try
            {
                int create_user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var iex_id = new SqlParameter("@id", id);
                var remarks = new SqlParameter("@cancellation_remarks", cancellation_remarks == null ? "" : cancellation_remarks);
                var created_by = new SqlParameter("@created_by", create_user);
                var cancellation_reason_id = new SqlParameter("@cancellation_reason_id", reason_id);
                var val = _scifferContext.Database.SqlQuery<string>(
                  "exec cancel_incoming_excise @id ,@cancellation_remarks ,@created_by,@cancellation_reason_id", iex_id, remarks, created_by, cancellation_reason_id).FirstOrDefault();
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
