using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.ViewModel;
using System.Data.SqlClient;
using System.Data;
using AutoMapper;

namespace Sciffer.Erp.Service.Implementation
{
    public class SalesReturnService : ISalesReturnService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericService;
        public SalesReturnService(ScifferContext scifferContext, IGenericService GenericService)
        {
            _genericService = GenericService;
            _scifferContext = scifferContext;
        }
        public string Add(sal_si_return_vm sal_si_return_vm)
        {
            try
            {
               
                    DataTable line_item = new DataTable();
                    line_item.Columns.Add("si_id", typeof(int));
                    line_item.Columns.Add("item_id", typeof(int));
                    line_item.Columns.Add("issue_quantity", typeof(double));
                    line_item.Columns.Add("uom_id", typeof(int));
                    line_item.Columns.Add("unit_price", typeof(double));
                    line_item.Columns.Add("discount", typeof(double));
                    line_item.Columns.Add("effective_unit_price", typeof(double));
                    line_item.Columns.Add("sales_value", typeof(double));
                    line_item.Columns.Add("assessable_rate", typeof(double));
                    line_item.Columns.Add("assessable_value", typeof(double));
                    line_item.Columns.Add("tax_id", typeof(int));
                    line_item.Columns.Add("storage_location_id", typeof(int));
                    line_item.Columns.Add("drawing_no", typeof(string));
                    line_item.Columns.Add("sac_hsn_id", typeof(int));
                    line_item.Columns.Add("plant_id", typeof(int));
                    line_item.Columns.Add("item_batch_detail_id", typeof(int));
                    line_item.Columns.Add("item_batch_id", typeof(int));
                    line_item.Columns.Add("si_detail_id", typeof(int));
                if (sal_si_return_vm.item_ids != null)
                {
                    var i = 0;
                    foreach (var item in sal_si_return_vm.item_ids)
                    {
                        var si_detail_id = Convert.ToInt32(sal_si_return_vm.si_detail_ids[i]);
                        var si_id1 = Convert.ToInt32(sal_si_return_vm.si_ids[i]);
                        var item_id = Convert.ToInt32(sal_si_return_vm.item_ids[i]);
                        var issue_quantity = Convert.ToDouble(sal_si_return_vm.issue_quantitys[i]);
                        var uom_id = Convert.ToInt32(sal_si_return_vm.uom_ids[i]);
                        var unit_price = sal_si_return_vm.unit_prices[i]==null?0: Convert.ToDouble(sal_si_return_vm.unit_prices[i]);
                        var discount = sal_si_return_vm.discounts[i] == null ? 0 : Convert.ToDouble(sal_si_return_vm.discounts[i]);
                        var effective_unit_price = sal_si_return_vm.effective_unit_prices[i] == null ? 0 : Convert.ToDouble(sal_si_return_vm.effective_unit_prices[i]);
                        var sales_value = sal_si_return_vm.sales_values[i] == null ? 0 : Convert.ToDouble(sal_si_return_vm.sales_values[i]);
                        var assessable_rate = sal_si_return_vm.assessable_rates[i] == null ? 0 : Convert.ToDouble(sal_si_return_vm.assessable_rates[i]);
                        var assessable_value = sal_si_return_vm.assessable_values[i] == null ? 0 : Convert.ToDouble(sal_si_return_vm.assessable_values[i]);
                        var tax_id = sal_si_return_vm.tax_ids[i] == null ? 0 : Convert.ToInt32(sal_si_return_vm.tax_ids[i]);
                        var storage_location_id = Convert.ToInt32(sal_si_return_vm.storage_location_ids[i]);
                        var drawing_no = sal_si_return_vm.drawing_nos[i];
                        var sac_hsn_id = sal_si_return_vm.sac_hsn_ids[i] == null ? 0 : Convert.ToInt32(sal_si_return_vm.sac_hsn_ids[i]);
                        var plant_id1 = sal_si_return_vm.plant_ids[i] == null ? 0 : Convert.ToInt32(sal_si_return_vm.plant_ids[i]);
                        var item_batch_detail_id = sal_si_return_vm.item_batch_detail_ids[i] == null ? 0 : sal_si_return_vm.item_batch_detail_ids[i] == "" ? 0 : Convert.ToInt32(sal_si_return_vm.item_batch_detail_ids[i]);
                        var item_batch_id = sal_si_return_vm.item_batch_ids[i] == null ? 0 : sal_si_return_vm.item_batch_ids[i] == "" ? 0 : Convert.ToInt32(sal_si_return_vm.item_batch_ids[i]);
                        line_item.Rows.Add(si_id1, item_id, issue_quantity, uom_id, unit_price, discount, effective_unit_price,
                            sales_value, assessable_rate, assessable_value, tax_id, storage_location_id, drawing_no, sac_hsn_id, plant_id1,
                            item_batch_detail_id, item_batch_id, si_detail_id); i++;
                    }
                }
                    var line_item_datatable = new SqlParameter("@line_item_datatable", SqlDbType.Structured);
                    line_item_datatable.TypeName = "dbo.temp_sal_si_return_detail";
                    line_item_datatable.Value = line_item;
                
                DateTime dte = new DateTime(1990, 1, 1);
                var sales_return_id = new SqlParameter("@sales_return_id", sal_si_return_vm.sales_return_id);
                var category_id = new SqlParameter("@category_id", sal_si_return_vm.category_id);
                var posting_date = new SqlParameter("@posting_date", sal_si_return_vm.posting_date);
                var buyer_id = new SqlParameter("@buyer_id", sal_si_return_vm.buyer_id);
                var consignee_id = new SqlParameter("@consignee_id", sal_si_return_vm.consignee_id);
                var net_value = new SqlParameter("@net_value", sal_si_return_vm.net_value);
                var net_currency_id = new SqlParameter("@net_currency_id", sal_si_return_vm.net_currency_id);
                var gross_value = new SqlParameter("@gross_value", sal_si_return_vm.gross_value);
                var gross_currency_id = new SqlParameter("@gross_currency_id", sal_si_return_vm.gross_currency_id);
                var business_unit_id = new SqlParameter("@business_unit_id", sal_si_return_vm.business_unit_id);
                var plant_id = new SqlParameter("@plant_id", sal_si_return_vm.plant_id);
                var sales_rm_id = new SqlParameter("@sales_rm_id", sal_si_return_vm.sales_rm_id == null ? 0 : sal_si_return_vm.sales_rm_id);
                var territory_id = new SqlParameter("@territory_id", sal_si_return_vm.territory_id == null ? 0 : sal_si_return_vm.territory_id);
                var gate_entry_no = new SqlParameter("@gate_entry_no", sal_si_return_vm.gate_entry_no == null ? "" : sal_si_return_vm.gate_entry_no);
                var gate_entry_date = new SqlParameter("@gate_entry_date", sal_si_return_vm.gate_entry_date == null ? dte : sal_si_return_vm.gate_entry_date);
                var invoice_number = new SqlParameter("@invoice_number", sal_si_return_vm.invoice_number == null ? "" : sal_si_return_vm.invoice_number);
                var reason_for_return = new SqlParameter("@reason_for_return", sal_si_return_vm.reason_for_return == null ? "" : sal_si_return_vm.reason_for_return);
                var billing_address = new SqlParameter("@billing_address", sal_si_return_vm.billing_address == null ? "" : sal_si_return_vm.billing_address);
                var billing_city = new SqlParameter("@billing_city", sal_si_return_vm.billing_city == null ? "" : sal_si_return_vm.billing_city);
                var billing_pincode = new SqlParameter("@billing_pincode", sal_si_return_vm.billing_pincode == null ? "" : sal_si_return_vm.billing_pincode);
                var billing_state_id = new SqlParameter("@billing_state_id", sal_si_return_vm.billing_state_id);
                var billing_email_id = new SqlParameter("@billing_email_id", sal_si_return_vm.billing_email_id == null ? string.Empty : sal_si_return_vm.billing_email_id);
                var shipping_address = new SqlParameter("@shipping_address", sal_si_return_vm.shipping_address == null ? "" : sal_si_return_vm.shipping_address);
                var shipping_city = new SqlParameter("@shipping_city", sal_si_return_vm.shipping_city == null ? "" : sal_si_return_vm.shipping_city);
                var shipping_pincode = new SqlParameter("@shipping_pincode", sal_si_return_vm.shipping_pincode);
                var shipping_state_id = new SqlParameter("@shipping_state_id", sal_si_return_vm.shipping_state_id);
                var payment_cycle_id = new SqlParameter("@payment_cycle_id", sal_si_return_vm.payment_cycle_id);
                var payment_terms_id = new SqlParameter("@payment_terms_id", sal_si_return_vm.payment_terms_id);
                var credit_avail_after_invoice = new SqlParameter("@credit_avail_after_invoice", sal_si_return_vm.credit_avail_after_invoice == null ? 0 : sal_si_return_vm.credit_avail_after_invoice);
                var tds_code_id = new SqlParameter("@tds_code_id", sal_si_return_vm.tds_code_id == null ? 0 : sal_si_return_vm.tds_code_id == null ? 0 : sal_si_return_vm.tds_code_id);
                var gst_tds_code_id = new SqlParameter("@gst_tds_code_id", sal_si_return_vm.gst_tds_code_id == null ? 0 : sal_si_return_vm.gst_tds_code_id);
                var available_credit_limit = new SqlParameter("@available_credit_limit", sal_si_return_vm.available_credit_limit == null ? 0 : sal_si_return_vm.available_credit_limit);
                var internal_remarks = new SqlParameter("@internal_remarks", sal_si_return_vm.internal_remarks == null ? string.Empty : sal_si_return_vm.internal_remarks);
                var remarks_on_document = new SqlParameter("@remarks_on_document", sal_si_return_vm.remarks_on_document == null ? string.Empty : sal_si_return_vm.remarks_on_document);
                var invoice_date = new SqlParameter("@invoice_date", sal_si_return_vm.invoice_date == null ?dte : sal_si_return_vm.invoice_date);
              
                var round_off = new SqlParameter("@round_off", sal_si_return_vm.round_off == null ? 0 : sal_si_return_vm.round_off);
                if (sal_si_return_vm.FileUpload != null)
                {
                    sal_si_return_vm.attachment = _genericService.GetFilePath("SalesInvoice", sal_si_return_vm.FileUpload);
                }
                else
                {
                    sal_si_return_vm.attachment = "No File";
                }
                var attachment = new SqlParameter("@attachment", sal_si_return_vm.attachment == null ? "" : sal_si_return_vm.attachment);
                var return_number = new SqlParameter("@return_number", sal_si_return_vm.return_number == null ? "" : sal_si_return_vm.return_number);
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_salesreturn @sales_return_id, @category_id,@posting_date,@buyer_id,@consignee_id,@net_value,@net_currency_id,@gross_value,@gross_currency_id,@business_unit_id,@plant_id,@sales_rm_id,@territory_id,@gate_entry_no,@gate_entry_date,@invoice_number,@reason_for_return,@billing_address,@billing_city,@billing_pincode,@billing_state_id,@billing_email_id,@shipping_address,@shipping_city,@shipping_pincode,@shipping_state_id,@payment_cycle_id,@payment_terms_id,@credit_avail_after_invoice,@tds_code_id,@gst_tds_code_id,@available_credit_limit,@internal_remarks,@remarks_on_document,@attachment,@return_number,@invoice_date,@round_off,@line_item_datatable"
                   , sales_return_id, category_id, posting_date, buyer_id, consignee_id, net_value, net_currency_id, gross_value, gross_currency_id,
                  business_unit_id, plant_id, sales_rm_id, territory_id, gate_entry_no, gate_entry_date, invoice_number, reason_for_return, billing_address,
                  billing_city, billing_pincode, billing_state_id, billing_email_id, shipping_address, shipping_city, shipping_pincode, shipping_state_id,
                  payment_cycle_id, payment_terms_id, credit_avail_after_invoice, tds_code_id, gst_tds_code_id, available_credit_limit, internal_remarks,
                  remarks_on_document, attachment, return_number, invoice_date, round_off,line_item_datatable).FirstOrDefault();
                if (val.Contains("Saved"))
                {
                    if (sal_si_return_vm.FileUpload != null)
                    {
                        sal_si_return_vm.FileUpload.SaveAs(sal_si_return_vm.attachment);
                    }
                    return val;
                }
                else
                {
                    System.IO.File.Delete(sal_si_return_vm.attachment);
                    return val;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }     
       
        public sal_si_return_batch_lsit_vm getBuyer()
        {
            var entity = new SqlParameter("@entity", "getBuyer");
            var buyer = _scifferContext.Database.SqlQuery<CustomerVM>(
             "exec GetSalesReturnDetails @entity", entity).ToList();
            var SIentity = new SqlParameter("@entity", "getSINumber");
            var getSINumber = _scifferContext.Database.SqlQuery<sal_si_vm>(
             "exec GetSalesReturnDetails @entity", SIentity).ToList();


            var Batchentity = new SqlParameter("@entity", "getSIBatch");
            var getSIBatch = _scifferContext.Database.SqlQuery<sal_si_return_batch_vm>(
             "exec GetSalesReturnDetails @entity", Batchentity).ToList();
            sal_si_return_batch_lsit_vm list = new sal_si_return_batch_lsit_vm();

            list.CustomerVM = buyer;
            list.sal_si_vm = getSINumber;
            list.sal_si_return_batch_vm = getSIBatch;

            return list;
        }

       
        
        public List<sal_si_return_detail_vm> getItems(int? plant_id,int? buyer_id,int? item_id,string batch_number,int? si_id,string entity,int consignee_id)
        {
            var plant = new SqlParameter("@plant_id", plant_id);
            var buyer = new SqlParameter("@buyer_id", buyer_id);            
            var item = new SqlParameter("@item_id", item_id);
            var batch = new SqlParameter("@batch_number", batch_number);
            var si = new SqlParameter("@si_id", si_id);
            var entitys = new SqlParameter("@entity", entity);
            var consignee_ids = new SqlParameter("@consignee_id", consignee_id);
            var items = _scifferContext.Database.SqlQuery<sal_si_return_detail_vm>(
           "exec GetSalesReturnDetails @entity,@plant_id,@buyer_id,@item_id,@batch_number,@si_id,@consignee_id", entitys, plant, buyer, item, batch, si, consignee_ids).ToList();
            return items;
        }
       
        public List<sal_si_return_vm> getall()
        {
            var salesreturn = (from si in _scifferContext.sal_si_return
                               join si_num in _scifferContext.SAL_SI on si.si_id equals si_num.si_id into si_num1
                               from si_num2 in si_num1.DefaultIfEmpty()
                               join c in _scifferContext.ref_document_numbring on si.category_id equals c.document_numbring_id
                               join buyer in _scifferContext.REF_CUSTOMER on si.buyer_id equals buyer.CUSTOMER_ID
                               join consignee in _scifferContext.REF_CUSTOMER on si.consignee_id equals consignee.CUSTOMER_ID
                               join plant in _scifferContext.REF_PLANT on si.plant_id equals plant.PLANT_ID
                               join business in _scifferContext.REF_BUSINESS_UNIT on si.business_unit_id equals business.BUSINESS_UNIT_ID
                               join territory in _scifferContext.REF_TERRITORY on si.territory_id equals territory.TERRITORY_ID into jo
                               from terr in jo.DefaultIfEmpty()
                               join payterms in _scifferContext.REF_PAYMENT_TERMS on si.payment_terms_id equals payterms.payment_terms_id
                               join paycycle in _scifferContext.REF_PAYMENT_CYCLE on si.payment_cycle_id equals paycycle.PAYMENT_CYCLE_ID
                               join paytype in _scifferContext.REF_PAYMENT_CYCLE_TYPE on paycycle.PAYMENT_CYCLE_TYPE_ID equals paytype.PAYMENT_CYCLE_TYPE_ID
                               join bstate in _scifferContext.REF_STATE on si.billing_state_id equals bstate.STATE_ID
                               join c1 in _scifferContext.REF_COUNTRY on bstate.COUNTRY_ID equals c1.COUNTRY_ID
                               join sstate in _scifferContext.REF_STATE on si.shipping_state_id equals sstate.STATE_ID
                               join c2 in _scifferContext.REF_COUNTRY on sstate.COUNTRY_ID equals c2.COUNTRY_ID
                               join net_currency in _scifferContext.REF_CURRENCY on si.net_currency_id equals net_currency.CURRENCY_ID
                               join gross_currency in _scifferContext.REF_CURRENCY on si.gross_currency_id equals gross_currency.CURRENCY_ID
                               join sr in _scifferContext.REF_EMPLOYEE on si.sales_rm_id equals sr.employee_id into j1
                               from salesrm in j1.DefaultIfEmpty()
                               join form in _scifferContext.SAL_SI_FORM on si.si_id equals form.si_id into form1
                               from form2 in form1.DefaultIfEmpty()
                               join tds in _scifferContext.ref_tds_code on si.tds_code_id equals tds.tds_code_id into tds1
                               from tds2 in tds1.DefaultIfEmpty()
                               join gs in _scifferContext.ref_gst_tds_code on si.gst_tds_code_id equals gs.gst_tds_code_id into j5
                               from gst in j5.DefaultIfEmpty()
                               select new sal_si_return_vm
                               {
                                   sales_return_id =si.sales_return_id,
                                   return_number = si.return_number,
                                   si_number = si_num2.si_number,
                                   posting_date = si.posting_date,
                                   buyer_name = buyer.CUSTOMER_NAME,
                                   buyer_code = buyer.CUSTOMER_CODE,
                                   consignee_name = consignee.CUSTOMER_NAME,
                                   consignee_code = consignee.CUSTOMER_CODE,
                                   net_value = si.net_value,
                                   gross_value = si.gross_value,
                                   credit_avail_after_invoice = si.credit_avail_after_invoice,
                                   internal_remarks = si.internal_remarks,
                                   remarks_on_document = si.remarks_on_document,
                                   billing_address = si.billing_address,
                                   billing_city = si.billing_city,
                                   billing_pincode = si.billing_pincode,
                                   billing_email_id = si.billing_email_id,
                                   shipping_address = si.shipping_address,
                                   shipping_city = si.shipping_city,
                                   shipping_pincode = si.shipping_pincode,
                                   attachment = si.attachment,
                                   category_name = c.category,
                                   net_currency_name = net_currency.CURRENCY_NAME,
                                   gross_currency_name = gross_currency.CURRENCY_NAME,
                                   business_unit_name = business.BUSINESS_UNIT_NAME,
                                   business_desc = business.BUSINESS_UNIT_DESCRIPTION,
                                   plant_name = plant.PLANT_NAME,
                                   plant_code = plant.PLANT_CODE,
                                   territory_name = terr == null ? string.Empty : terr.TERRITORY_NAME,
                                   sales_rm_name = salesrm == null ? string.Empty : salesrm.employee_code + "-" + salesrm.employee_name,
                                   payment_terms_name = payterms.payment_terms_code,
                                   payment_cycle_name = paycycle.PAYMENT_CYCLE_NAME,
                                   payment_cycle_type_name = paytype.PAYMENT_CYCLE_TYPE_NAME,
                                   billing_state_name = bstate.STATE_NAME,
                                   billing_country_name = c1.COUNTRY_NAME,
                                   shipping_state_name = sstate.STATE_NAME,
                                   shipping_country_name = c2.COUNTRY_NAME,
                                   tds_code_name = tds2 == null ? string.Empty : tds2.tds_code + "/" + tds2.tds_code_description,
                                   available_credit_limit = si.available_credit_limit,
                                   gst_tds_code_name = gst == null ? string.Empty : gst.gst_tds_code + "/" + gst.gst_tds_code_description,
                                   round_off = si.round_off
                               }).OrderByDescending(a => a.sales_return_id).ToList();
            return salesreturn;
        }
        public sal_si_return_vm Detail(int id)
        {
            try { 
            sal_si_return returns = _scifferContext.sal_si_return.FirstOrDefault(c => c.sales_return_id == id);
            Mapper.CreateMap<sal_si_return, sal_si_return_vm>();
            sal_si_return_vm returns_vm = Mapper.Map<sal_si_return, sal_si_return_vm>(returns);
            var detail = (from return_detail in returns_vm.sal_si_return_detail
                          join si in _scifferContext.SAL_SI on return_detail.si_id equals si.si_id
                          select new 
                          {
                              si_number = si.si_number,
                              sales_return_detail_id = return_detail.sales_return_detail_id,
                              sales_return_id= return_detail.sales_return_id,
                              item_id = return_detail.item_id,
                              item_name = return_detail.REF_ITEM.ITEM_NAME, 
                              item_code = return_detail.REF_ITEM.ITEM_CODE,
                              issue_quantity = return_detail.issue_quantity,
                              uom_id = return_detail.uom_id,
                              uom_name = return_detail.REF_UOM.UOM_NAME,
                              unit_price = return_detail.unit_price,
                              discount = return_detail.discount,
                              effective_unit_price = return_detail.effective_unit_price,
                              sales_value = return_detail.sales_value,
                              assessable_rate = return_detail.assessable_rate,
                              assessable_value = return_detail.assessable_value,
                              tax_id = return_detail.ref_tax.tax_id,
                              tax_code = return_detail.ref_tax.tax_code,
                              tax_name = return_detail.ref_tax.tax_name,
                              storage_location_id = return_detail.storage_location_id,
                              storage_location_name = return_detail.REF_STORAGE_LOCATION.storage_location_name,
                              storage_location_code = return_detail.REF_STORAGE_LOCATION.description,
                              drawing_no = return_detail.drawing_no,
                              sac_hsn_id = return_detail.sac_hsn_id,
                              sac_hsn_code = return_detail.ref_hsn_code.hsn_code,
                              sac_hsn_name = return_detail.ref_hsn_code.hsn_code_description,
                              plant_id = return_detail.plant_id,
                              plant_name = return_detail.REF_PLANT.PLANT_NAME,
                              plant_code = return_detail.REF_PLANT.PLANT_CODE,
                              item_batch_detail_id = return_detail.item_batch_detail_id,
                              item_batch_id = return_detail.item_batch_id,
                              si_id = return_detail.si_id,
                              si_detail_id = return_detail.si_detail_id
                          }).ToList().Select(a=> new sal_si_return_detail_vm
                          {
                              si_number  = a.si_number,
                              sales_return_detail_id = a.sales_return_detail_id,
                              sales_return_id = a.sales_return_id,
                              item_id = a.item_id,
                              item_name = a.item_code,
                              item_code = a.item_code,
                              issue_quantity = a.issue_quantity,
                              uom_id = a.uom_id,
                              uom_name = a.uom_name,
                              unit_price = a.unit_price,
                              discount = a.discount,
                              effective_unit_price = a.effective_unit_price,
                              sales_value = a.sales_value,
                              assessable_rate = a.assessable_rate,
                              assessable_value = a.assessable_value,
                              tax_id = a.tax_id,
                              tax_code = a.tax_code+"/"+a.tax_name,
                              storage_location_id = a.storage_location_id,
                              storage_location_name = a.storage_location_code+"/"+a.storage_location_code,
                              drawing_no = a.drawing_no,
                              sac_hsn_id = a.sac_hsn_id,
                              sac_hsn_code = a.sac_hsn_code + "/"+a.sac_hsn_name,
                              plant_id = a.plant_id,
                              plant_name = a.plant_name+"/"+a.plant_code,
                              item_batch_detail_id = a.item_batch_detail_id,
                              item_batch_id = a.item_batch_id,
                              si_id = a.si_id,
                              si_detail_id = a.si_detail_id
                          }).ToList();

            returns_vm.sal_si_return_detail_vm = detail;
            var billingcountry = _scifferContext.REF_STATE.Where(s => s.STATE_ID == returns_vm.billing_state_id)
                                      .Select(s => new
                                      {
                                          COUNTRY_ID = s.COUNTRY_ID
                                      }).Single();
            var country = _scifferContext.REF_STATE.Where(s => s.STATE_ID == returns_vm.shipping_state_id)
                          .Select(s => new
                          {
                              COUNTRY_ID = s.COUNTRY_ID
                          }).Single();
            var paymentcyletype = _scifferContext.REF_PAYMENT_CYCLE.Where(P => P.PAYMENT_CYCLE_ID == returns_vm.payment_cycle_id)
                                     .Select(P => new
                                     {
                                         PAYMENT_CYCLE_TYPE_ID = P.PAYMENT_CYCLE_TYPE_ID
                                     }).Single();
            returns_vm.payment_cycle_type_id = paymentcyletype.PAYMENT_CYCLE_TYPE_ID;
            returns_vm.billing_country_id = billingcountry.COUNTRY_ID;
            returns_vm.shipping_country_id = country.COUNTRY_ID;
            return returns_vm;
            }
            catch (Exception ex) { return null; }
        }
    }

}
