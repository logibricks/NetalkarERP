using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
namespace Sciffer.Erp.Service.Implementation
{
    public class ReportService : IReportService
    {
        private readonly ScifferContext _scifferContext;
        public ReportService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
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

        public List<report_sales_summary> SalesSummaryReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {
            try
            {
                var cus_category_id = new SqlParameter("@customer_category_id", customer_category_id == null ? "-1" : customer_category_id);
                var sal_rm_id = new SqlParameter("@sales_rm_id", sales_rm_id == null ? "-1" : sales_rm_id);
                var territory = new SqlParameter("@territory_id", territory_id == null ? "-1" : territory_id);
                var priority = new SqlParameter("@priority_id", priority_id == null ? "-1" : priority_id);
                var control_accoun = new SqlParameter("@control_account_id", control_account_id == null ? "-1" : control_account_id);
                var currency = new SqlParameter("@currency_id", currency_id == null ? "-1" : currency_id);
                var business_unit = new SqlParameter("@business_unit_id", business_unit_id == null ? "-1" : business_unit_id);
                var s_date = new SqlParameter("@start_date", @start_date);
                var e_date = new SqlParameter("@end_date", @end_date);
                var plant_id = new SqlParameter("@plant_id", plant == null ? "-1" : plant);

                var val = _scifferContext.Database.SqlQuery<report_sales_summary>(
                    "exec report_sales_summary @customer_category_id ,@sales_rm_id  ,@territory_id ,@priority_id ,@control_account_id  ,@currency_id  ,@business_unit_id  ,@start_date,@end_date ,@plant_id",
                  cus_category_id, sal_rm_id, territory, priority, control_accoun, currency, business_unit, s_date, e_date, plant_id).ToList();
                HttpContext.Current.Session["SalesSummaryReport"] = null;
                HttpContext.Current.Session["SalesSummaryReport"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<report_plant_maintenanace> MaintenancePlanCycleandFrequencyReport(string entity, string plant_id, string machine_id, string maintenance_type_id, string frequency_id, string status_id, DateTime? from_date, DateTime? to_date, string item_id, string auto_manual, string notification_type, string employee_id)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            DateTime dte1 = new DateTime(0001, 1, 1);
            var ent = new SqlParameter("@entity", entity);
            var plantid = new SqlParameter("@plant_id", plant_id == null ? "-1" : plant_id);
            var machineid = new SqlParameter("@machine_id", machine_id == null ? "-1" : machine_id);
            var maintenancetypeid = new SqlParameter("@maintenance_type_id", maintenance_type_id == null ? "-1" : maintenance_type_id);
            var frequencyid = new SqlParameter("@frequency_id", frequency_id == null ? "-1" : frequency_id);
            var statusid = new SqlParameter("@status_id", status_id == null ? "-1" : status_id);
            var fromdate = new SqlParameter("@from_date", from_date == null ? dte : from_date == dte1 ? dte : from_date);
            var todate = new SqlParameter("@to_date", to_date == null ? dte : to_date);
            var itemid = new SqlParameter("@item_id", item_id == null ? "-1" : item_id);
            var automanual = new SqlParameter("@auto_manual", auto_manual == null ? "-1" : auto_manual);
            var notificationtype = new SqlParameter("@notification_type", notification_type == null ? "-1" : notification_type);
            var employeeid = new SqlParameter("@employee_id", employee_id == null ? "-1" : employee_id);
            var val = _scifferContext.Database.SqlQuery<report_plant_maintenanace>(
                "exec report_plan_maintenance @entity,@plant_id,@machine_id,@maintenance_type_id,@frequency_id,@status_id,@from_date,@to_date,@item_id,@auto_manual,@notification_type,@employee_id", ent, plantid, machineid, maintenancetypeid, frequencyid, statusid, fromdate, todate, itemid, automanual, notificationtype, employeeid).ToList();
            return val;
        }
        public List<report_quality_vm> report_quality(string entity, DateTime? from_date, DateTime? to_date, string plant_id, string document_type_code, string item_id, string status_id, string sloc_id, string reason_id)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            DateTime dte1 = new DateTime(0001, 1, 1);
            var ent = new SqlParameter("@entity", entity);
            var fromdate = new SqlParameter("@from_date", from_date == null ? dte : from_date == dte1 ? dte : from_date);
            var todate = new SqlParameter("@to_date", to_date == null ? dte : to_date == dte1 ? dte : to_date);
            var plantid = new SqlParameter("@plant_id", plant_id == "" ? "-1" : plant_id == null ? "-1" : plant_id);
            var documenttypecode = new SqlParameter("@document_type_code", document_type_code == null ? "-1" : document_type_code);
            var itemid = new SqlParameter("@item_id", item_id == null ? "-1" : item_id == "" ? "-1" : item_id);
            var statusid = new SqlParameter("@status_id", status_id == null ? "-1" : status_id == "" ? "-1" : status_id);
            var slocid = new SqlParameter("@sloc_id", sloc_id == null ? "-1" : sloc_id == "" ? "-1" : sloc_id);
            var reason = new SqlParameter("@reason_id", reason_id == null ? "-1" : reason_id == "" ? "-1" : reason_id);
            var val = _scifferContext.Database.SqlQuery<report_quality_vm>(
                "exec report_qc_report @entity,@from_date,@to_date,@plant_id,@document_type_code,@item_id,@status_id,@sloc_id,@reason_id", ent, fromdate, todate, plantid, documenttypecode, itemid, statusid, slocid, reason).ToList();
            return val;
        }


        public List<report_production_vm> report_production(string entity, DateTime? from_date, DateTime? to_date, string plant_id, string machine_id, string tag_id, string item_id, string tag_number, string prod_order_id, string item_status_id, string user_id, string process_id, string shift_id, string is_qc_remark)
        {
            try
            {
                DateTime dte = new DateTime(1990, 1, 1);
                DateTime dte1 = new DateTime(0001, 1, 1);
                var ent = new SqlParameter("@entity", entity);
                var fromdate = new SqlParameter("@from_date", from_date == null ? dte : from_date == dte1 ? dte : from_date);
                var todate = new SqlParameter("@to_date", to_date == null ? dte : to_date == dte1 ? dte : to_date);
                var plantid = new SqlParameter("@plant_id", plant_id == null || plant_id == "" ? "-1" : plant_id);
                var machineid = new SqlParameter("@machine_id", machine_id == null || machine_id == "" ? "-1" : machine_id);
                var tagid = new SqlParameter("@tag_id", tag_id == null ? "-1" : tag_id);
                var itemid = new SqlParameter("@item_id", item_id == null || item_id == "" ? "-1" : item_id);
                var tagnumber = new SqlParameter("@tag_number", tag_number == null ? "-1" : tag_number);
                var prodorderid = new SqlParameter("@prod_order_id", prod_order_id == null ? "-1" : prod_order_id);
                var itemstatusid = new SqlParameter("@item_status_id", item_status_id == null || item_status_id == "" ? "-1" : item_status_id);
                var userid = new SqlParameter("@user_id", user_id == null || user_id == "" ? "-1" : user_id);
                var processid = new SqlParameter("@process_id", process_id == null ? "-1" : process_id);
                var shiftid = new SqlParameter("@shift_id", shift_id == null ? "-1" : shift_id);
                var isqc_remark = new SqlParameter("@is_qc_remark", is_qc_remark == null ? "-1" : is_qc_remark);

                var val = _scifferContext.Database.SqlQuery<report_production_vm>(
                    "exec report_production_report @entity,@from_date,@to_date,@plant_id,@machine_id,@tag_id,@item_id,@tag_number,@prod_order_id,@item_status_id,@user_id,@process_id,@shift_id,@is_qc_remark",
                    ent, fromdate, todate, plantid, machineid, tagid, itemid, tagnumber, prodorderid, itemstatusid, userid, processid, shiftid, isqc_remark).ToList();

                if (entity == "get_wip_report_detailed" || entity == "get_wip_report_summary" || entity == "get_QC_Paramaters_Status" || entity == "get_WIP_Report_Ageing_Report_Summary" || entity == "get_tag_history_report")
                {
                    foreach (var item in val)
                    {
                        if (item.machine_id != null)
                        {
                            var machine_name = "";
                            var machine_code = "";
                            var machine_id_array = item.machine_id.Split('/');
                            var count = 0;
                            foreach (var zz in machine_id_array)
                            {
                                if (zz != "")
                                {
                                    count++;
                                    var id = int.Parse(zz);
                                    if (count == machine_id_array.Count())
                                    {
                                        machine_name = machine_name + _scifferContext.ref_machine.Find(id).machine_name;
                                        machine_code = machine_code + _scifferContext.ref_machine.Find(id).machine_code;
                                    }
                                    else
                                    {
                                        machine_name = machine_name + _scifferContext.ref_machine.Find(id).machine_name;
                                        machine_code = machine_code + _scifferContext.ref_machine.Find(id).machine_code;
                                    }
                                }
                            }
                            item.machine_desc = machine_name;
                            item.machine_code = machine_code;
                        }
                    }
                }

                return val;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        public List<Report_Sales_header_level_VM> Salesheaderlevel(DateTime? From_date, DateTime? To_date, string Sales_RM, string Territory, string Priority, string Control_account, string currency, string plant, string business_unit)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var SalesRM1 = new SqlParameter("@Sales_RM", Sales_RM == null ? "-1" : Sales_RM);
                var Territory1 = new SqlParameter("@Territory", Territory == null ? "-1" : Territory);
                var Priority1 = new SqlParameter("@Priority", Priority == null ? "-1" : Priority);
                var Controlaccount1 = new SqlParameter("@Control_account", Control_account == null ? "-1" : Control_account);
                var currency1 = new SqlParameter("@currency", currency == null ? "-1" : currency);
                var plant1 = new SqlParameter("@plant", plant == null ? "-1" : plant);
                var businessunit1 = new SqlParameter("@business_unit", business_unit == null ? "-1" : business_unit);
                var val = _scifferContext.Database.SqlQuery<Report_Sales_header_level_VM>("exec PROCEDURENAME @From_date, @To_date, @Sales_RM, @Territory, @Priority, @Control_account, @currency, @plant, @business_unit", Fromdate1, Todate1, SalesRM1, Territory1, Priority1, Controlaccount1, currency1, plant1, businessunit1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Sales_detail_level_VM> Salesdetaillevel(DateTime? From_date, DateTime? To_date, string Customer_category, string Sales_RM, string Territory, string Priority, string Control_account, string currency, string plant, string business_unit)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var Customercategory1 = new SqlParameter("@Customer_category", Customer_category == null ? "-1" : Customer_category);
                var SalesRM1 = new SqlParameter("@Sales_RM", Sales_RM == null ? "-1" : Sales_RM);
                var Territory1 = new SqlParameter("@Territory", Territory == null ? "-1" : Territory);
                var Priority1 = new SqlParameter("@Priority", Priority == null ? "-1" : Priority);
                var Controlaccount1 = new SqlParameter("@Control_account", Control_account == null ? "-1" : Control_account);
                var currency1 = new SqlParameter("@currency", currency == null ? "-1" : currency);
                var plant1 = new SqlParameter("@plant", plant == null ? "-1" : plant);
                var businessunit1 = new SqlParameter("@business_unit", business_unit == null ? "-1" : business_unit);
                var val = _scifferContext.Database.SqlQuery<Report_Sales_detail_level_VM>("exec PROCEDURENAME @From_date, @To_date, @Customer_category, @Sales_RM, @Territory, @Priority, @Control_account, @currency, @plant, @business_unit", Fromdate1, Todate1, Customercategory1, SalesRM1, Territory1, Priority1, Controlaccount1, currency1, plant1, businessunit1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_customer_contact_details_VM> customercontactdetails()
        {
            try
            {
                var val = _scifferContext.Database.SqlQuery<Report_customer_contact_details_VM>("exec PROCEDURENAME ").ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Customer_Payment_Master_List_vm> CustomerPaymentMasterList(string Customer_category, string Prioirty)
        {
            try
            {
                var Customercategory1 = new SqlParameter("@Customer_category", Customer_category == null ? "-1" : Customer_category);
                var Prioirty1 = new SqlParameter("@Prioirty", Prioirty == null ? "-1" : Prioirty);
                var val = _scifferContext.Database.SqlQuery<Report_Customer_Payment_Master_List_vm>("exec PROCEDURENAME @Customer_category, @Prioirty", Customercategory1, Prioirty1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Client_Ledger_VM> ClientLedger(DateTime? From_date, DateTime? To_date, string Customer_COde, string Customer_Description_)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var CustomerCOde1 = new SqlParameter("@Customer_COde", Customer_COde == null ? "-1" : Customer_COde);
                var CustomerDescription1 = new SqlParameter("@Customer_Description_", Customer_Description_ == null ? "-1" : Customer_Description_);
                var val = _scifferContext.Database.SqlQuery<Report_Client_Ledger_VM>("exec PROCEDURENAME @From_date, @To_date, @Customer_COde, @Customer_Description_", Fromdate1, Todate1, CustomerCOde1, CustomerDescription1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Forex_Sales__VM> ForexSales(DateTime? From_date, DateTime? To_date, string Customer_category, string Sales_RM, string Territory, string Priority, string Control_account, string currency, string plant, string business_unit)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var Customercategory1 = new SqlParameter("@Customer_category", Customer_category == null ? "-1" : Customer_category);
                var SalesRM1 = new SqlParameter("@Sales_RM", Sales_RM == null ? "-1" : Sales_RM);
                var Territory1 = new SqlParameter("@Territory", Territory == null ? "-1" : Territory);
                var Priority1 = new SqlParameter("@Priority", Priority == null ? "-1" : Priority);
                var Controlaccount1 = new SqlParameter("@Control_account", Control_account == null ? "-1" : Control_account);
                var currency1 = new SqlParameter("@currency", currency == null ? "-1" : currency);
                var plant1 = new SqlParameter("@plant", plant == null ? "-1" : plant);
                var businessunit1 = new SqlParameter("@business_unit", business_unit == null ? "-1" : business_unit);
                var val = _scifferContext.Database.SqlQuery<Report_Forex_Sales__VM>("exec PROCEDURENAME @From_date, @To_date, @Customer_category, @Sales_RM, @Territory, @Priority, @Control_account, @currency, @plant, @business_unit", Fromdate1, Todate1, Customercategory1, SalesRM1, Territory1, Priority1, Controlaccount1, currency1, plant1, businessunit1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Sales_Quotation_Summary_VM> SalesQuotationSummary(DateTime? From_date, DateTime? To_date, string Customer_category, string Sales_RM, string Territory, string Priority, string currency, string plant, string business_unit)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var Customercategory1 = new SqlParameter("@Customer_category", Customer_category == null ? "-1" : Customer_category);
                var SalesRM1 = new SqlParameter("@Sales_RM", Sales_RM == null ? "-1" : Sales_RM);
                var Territory1 = new SqlParameter("@Territory", Territory == null ? "-1" : Territory);
                var Priority1 = new SqlParameter("@Priority", Priority == null ? "-1" : Priority);
                var currency1 = new SqlParameter("@currency", currency == null ? "-1" : currency);
                var plant1 = new SqlParameter("@plant", plant == null ? "-1" : plant);
                var businessunit1 = new SqlParameter("@business_unit", business_unit == null ? "-1" : business_unit);
                var val = _scifferContext.Database.SqlQuery<Report_Sales_Quotation_Summary_VM>("exec PROCEDURENAME @From_date, @To_date, @Customer_category, @Sales_RM, @Territory, @Priority, @currency, @plant, @business_unit", Fromdate1, Todate1, Customercategory1, SalesRM1, Territory1, Priority1, currency1, plant1, businessunit1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_sales_Quotation_header_level_VM> salesQuotationheaderlevel(DateTime? From_date, DateTime? To_date, string Customer_category, string Sales_RM, string Territory, string Priority, string currency, string plant, string business_unit)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var Customercategory1 = new SqlParameter("@Customer_category", Customer_category == null ? "-1" : Customer_category);
                var SalesRM1 = new SqlParameter("@Sales_RM", Sales_RM == null ? "-1" : Sales_RM);
                var Territory1 = new SqlParameter("@Territory", Territory == null ? "-1" : Territory);
                var Priority1 = new SqlParameter("@Priority", Priority == null ? "-1" : Priority);
                var currency1 = new SqlParameter("@currency", currency == null ? "-1" : currency);
                var plant1 = new SqlParameter("@plant", plant == null ? "-1" : plant);
                var businessunit1 = new SqlParameter("@business_unit", business_unit == null ? "-1" : business_unit);
                var val = _scifferContext.Database.SqlQuery<Report_sales_Quotation_header_level_VM>("exec PROCEDURENAME @From_date, @To_date, @Customer_category, @Sales_RM, @Territory, @Priority, @currency, @plant, @business_unit", Fromdate1, Todate1, Customercategory1, SalesRM1, Territory1, Priority1, currency1, plant1, businessunit1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_sales_Quotation_Detail_level_VM> salesQuotationDetaillevel(DateTime? From_date, DateTime? To_date, string Customer_category, string Sales_RM, string Territory, string Priority, string currency, string plant, string business_unit)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var Customercategory1 = new SqlParameter("@Customer_category", Customer_category == null ? "-1" : Customer_category);
                var SalesRM1 = new SqlParameter("@Sales_RM", Sales_RM == null ? "-1" : Sales_RM);
                var Territory1 = new SqlParameter("@Territory", Territory == null ? "-1" : Territory);
                var Priority1 = new SqlParameter("@Priority", Priority == null ? "-1" : Priority);
                var currency1 = new SqlParameter("@currency", currency == null ? "-1" : currency);
                var plant1 = new SqlParameter("@plant", plant == null ? "-1" : plant);
                var businessunit1 = new SqlParameter("@business_unit", business_unit == null ? "-1" : business_unit);
                var val = _scifferContext.Database.SqlQuery<Report_sales_Quotation_Detail_level_VM>("exec PROCEDURENAME @From_date, @To_date, @Customer_category, @Sales_RM, @Territory, @Priority, @currency, @plant, @business_unit", Fromdate1, Todate1, Customercategory1, SalesRM1, Territory1, Priority1, currency1, plant1, businessunit1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Sales_Order_Summary_VM> SalesOrderSummary(DateTime? From_date, DateTime? To_date, string Customer_category, string Sales_RM, string Territory, string Priority, string currency, string plant, string business_unit)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var Customercategory1 = new SqlParameter("@Customer_category", Customer_category == null ? "-1" : Customer_category);
                var SalesRM1 = new SqlParameter("@Sales_RM", Sales_RM == null ? "-1" : Sales_RM);
                var Territory1 = new SqlParameter("@Territory", Territory == null ? "-1" : Territory);
                var Priority1 = new SqlParameter("@Priority", Priority == null ? "-1" : Priority);
                var currency1 = new SqlParameter("@currency", currency == null ? "-1" : currency);
                var plant1 = new SqlParameter("@plant", plant == null ? "-1" : plant);
                var businessunit1 = new SqlParameter("@business_unit", business_unit == null ? "-1" : business_unit);
                var val = _scifferContext.Database.SqlQuery<Report_Sales_Order_Summary_VM>("exec PROCEDURENAME @From_date, @To_date, @Customer_category, @Sales_RM, @Territory, @Priority, @currency, @plant, @business_unit", Fromdate1, Todate1, Customercategory1, SalesRM1, Territory1, Priority1, currency1, plant1, businessunit1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_sales_order_header_level_vm> salesorderheaderlevel(DateTime? From_date, DateTime? To_date, string Customer_category, string Sales_RM, string Territory, string Priority, string currency, string plant, string business_unit)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var Customercategory1 = new SqlParameter("@Customer_category", Customer_category == null ? "-1" : Customer_category);
                var SalesRM1 = new SqlParameter("@Sales_RM", Sales_RM == null ? "-1" : Sales_RM);
                var Territory1 = new SqlParameter("@Territory", Territory == null ? "-1" : Territory);
                var Priority1 = new SqlParameter("@Priority", Priority == null ? "-1" : Priority);
                var currency1 = new SqlParameter("@currency", currency == null ? "-1" : currency);
                var plant1 = new SqlParameter("@plant", plant == null ? "-1" : plant);
                var businessunit1 = new SqlParameter("@business_unit", business_unit == null ? "-1" : business_unit);
                var val = _scifferContext.Database.SqlQuery<Report_sales_order_header_level_vm>("exec PROCEDURENAME @From_date, @To_date, @Customer_category, @Sales_RM, @Territory, @Priority, @currency, @plant, @business_unit", Fromdate1, Todate1, Customercategory1, SalesRM1, Territory1, Priority1, currency1, plant1, businessunit1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_sales_Order_Detail_level_vm> salesOrderDetaillevel(DateTime? From_date, DateTime? To_date, string Customer_category, string Sales_RM, string Territory, string Priority, string currency, string plant, string business_unit)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var Customercategory1 = new SqlParameter("@Customer_category", Customer_category == null ? "-1" : Customer_category);
                var SalesRM1 = new SqlParameter("@Sales_RM", Sales_RM == null ? "-1" : Sales_RM);
                var Territory1 = new SqlParameter("@Territory", Territory == null ? "-1" : Territory);
                var Priority1 = new SqlParameter("@Priority", Priority == null ? "-1" : Priority);
                var currency1 = new SqlParameter("@currency", currency == null ? "-1" : currency);
                var plant1 = new SqlParameter("@plant", plant == null ? "-1" : plant);
                var businessunit1 = new SqlParameter("@business_unit", business_unit == null ? "-1" : business_unit);
                var val = _scifferContext.Database.SqlQuery<Report_sales_Order_Detail_level_vm>("exec PROCEDURENAME @From_date, @To_date, @Customer_category, @Sales_RM, @Territory, @Priority, @currency, @plant, @business_unit", Fromdate1, Todate1, Customercategory1, SalesRM1, Territory1, Priority1, currency1, plant1, businessunit1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Sales_Order_Item_wise_Plan_vm> SalesOrderItemwisePlan(DateTime? From_date, DateTime? To_date, string Item_Code, string Plant, string Customer_COde)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var ItemCode1 = new SqlParameter("@Item_Code", Item_Code == null ? "-1" : Item_Code);
                var Plant1 = new SqlParameter("@Plant", Plant == null ? "-1" : Plant);
                var CustomerCOde1 = new SqlParameter("@Customer_COde", Customer_COde == null ? "-1" : Customer_COde);
                var val = _scifferContext.Database.SqlQuery<Report_Sales_Order_Item_wise_Plan_vm>("exec PROCEDURENAME @From_date, @To_date, @Item_Code, @Plant, @Customer_COde", Fromdate1, Todate1, ItemCode1, Plant1, CustomerCOde1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Item_Wise_Sales_Summary_vm> ItemWiseSalesSummary(DateTime? From_date, DateTime? To_date, string Item_Code, string Plant)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var ItemCode1 = new SqlParameter("@Item_Code", Item_Code == null ? "-1" : Item_Code);
                var Plant1 = new SqlParameter("@Plant", Plant == null ? "-1" : Plant);
                var val = _scifferContext.Database.SqlQuery<Report_Item_Wise_Sales_Summary_vm>("exec PROCEDURENAME @From_date, @To_date, @Item_Code, @Plant", Fromdate1, Todate1, ItemCode1, Plant1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Item_Wise_Sales_Detail_vm> ItemWiseSalesDetail(DateTime? From_date, DateTime? To_date, string Item_Code, string Plant, string Customer_COde)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var ItemCode1 = new SqlParameter("@Item_Code", Item_Code == null ? "-1" : Item_Code);
                var Plant1 = new SqlParameter("@Plant", Plant == null ? "-1" : Plant);
                var CustomerCOde1 = new SqlParameter("@Customer_COde", Customer_COde == null ? "-1" : Customer_COde);
                var val = _scifferContext.Database.SqlQuery<Report_Item_Wise_Sales_Detail_vm>("exec PROCEDURENAME @From_date, @To_date, @Item_Code, @Plant, @Customer_COde", Fromdate1, Todate1, ItemCode1, Plant1, CustomerCOde1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Sales_Order_Qty_Tracker_vm> SalesOrderQtyTracker()
        {
            try
            {
                var val = _scifferContext.Database.SqlQuery<Report_Sales_Order_Qty_Tracker_vm>("exec PROCEDURENAME ").ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Collections_Received_Report_vm> CollectionsReceivedReport(DateTime? From_date, DateTime? To_date)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var val = _scifferContext.Database.SqlQuery<Report_Collections_Received_Report_vm>("exec PROCEDURENAME @From_date, @To_date", Fromdate1, Todate1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Collections_Expected_Report__vm> CollectionsExpectedReport(DateTime? From_date, DateTime? To_date, string Customer_Category, string Plant, string Business_Unit)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var CustomerCategory1 = new SqlParameter("@Customer_Category", Customer_Category == null ? "-1" : Customer_Category);
                var Plant1 = new SqlParameter("@Plant", Plant == null ? "-1" : Plant);
                var BusinessUnit1 = new SqlParameter("@Business_Unit", Business_Unit == null ? "-1" : Business_Unit);
                var val = _scifferContext.Database.SqlQuery<Report_Collections_Expected_Report__vm>("exec PROCEDURENAME @From_date, @To_date, @Customer_Category, @Plant, @Business_Unit", Fromdate1, Todate1, CustomerCategory1, Plant1, BusinessUnit1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Customer_Master_Discount_Details_vm> CustomerMasterDiscountDetails()
        {
            try
            {
                var val = _scifferContext.Database.SqlQuery<Report_Customer_Master_Discount_Details_vm>("exec PROCEDURENAME ").ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Sales_Price_List_vm> SalesPriceList(string Customer_Category, string Item_Group, string Customer_Priority, string Item_Prioirty, string Territory)
        {
            try
            {
                var CustomerCategory1 = new SqlParameter("@Customer_Category", Customer_Category == null ? "-1" : Customer_Category);
                var ItemGroup1 = new SqlParameter("@Item_Group", Item_Group == null ? "-1" : Item_Group);
                var CustomerPriority1 = new SqlParameter("@Customer_Priority", Customer_Priority == null ? "-1" : Customer_Priority);
                var ItemPrioirty1 = new SqlParameter("@Item_Prioirty", Item_Prioirty == null ? "-1" : Item_Prioirty);
                var Territory1 = new SqlParameter("@Territory", Territory == null ? "-1" : Territory);
                var val = _scifferContext.Database.SqlQuery<Report_Sales_Price_List_vm>("exec PROCEDURENAME @Customer_Category, @Item_Group, @Customer_Priority, @Item_Prioirty, @Territory", CustomerCategory1, ItemGroup1, CustomerPriority1, ItemPrioirty1, Territory1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Open_Sales_Order_vm> OpenSalesOrder(DateTime? As_of_Date, string Customer_Category, string Plant, string Business_Unit)
        {
            try
            {
                var AsofDate1 = new SqlParameter("@As_of_Date", As_of_Date);
                var CustomerCategory1 = new SqlParameter("@Customer_Category", Customer_Category == null ? "-1" : Customer_Category);
                var Plant1 = new SqlParameter("@Plant", Plant == null ? "-1" : Plant);
                var BusinessUnit1 = new SqlParameter("@Business_Unit", Business_Unit == null ? "-1" : Business_Unit);
                var val = _scifferContext.Database.SqlQuery<Report_Open_Sales_Order_vm>("exec PROCEDURENAME @As_of_Date, @Customer_Category, @Plant, @Business_Unit", AsofDate1, CustomerCategory1, Plant1, BusinessUnit1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Vendor_Contact_Details_vm> VendorContactDetails()
        {
            try
            {
                var val = _scifferContext.Database.SqlQuery<Report_Vendor_Contact_Details_vm>("exec PROCEDURENAME ").ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_VENDOR_Payment_Master_List__vm> VENDORPaymentMasterList()
        {
            try
            {
                var val = _scifferContext.Database.SqlQuery<Report_VENDOR_Payment_Master_List__vm>("exec PROCEDURENAME ").ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Vendor_Ledger_vm> VendorLedger(DateTime? From_Date, DateTime? To_Date, string VENDOR_COde, string VENDOR_Description_)
        {
            try
            {
                var FromDate1 = new SqlParameter("@From_Date", From_Date);
                var ToDate1 = new SqlParameter("@To_Date", To_Date);
                var VENDORCOde1 = new SqlParameter("@VENDOR_COde", VENDOR_COde == null ? "-1" : VENDOR_COde);
                var VENDORDescription1 = new SqlParameter("@VENDOR_Description_", VENDOR_Description_ == null ? "-1" : VENDOR_Description_);
                var val = _scifferContext.Database.SqlQuery<Report_Vendor_Ledger_vm>("exec PROCEDURENAME @From_Date, @To_Date, @VENDOR_COde, @VENDOR_Description_", FromDate1, ToDate1, VENDORCOde1, VENDORDescription1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Vendor_Master_Discount_Details__vm> VendorMasterDiscountDetails()
        {
            try
            {
                var val = _scifferContext.Database.SqlQuery<Report_Vendor_Master_Discount_Details__vm>("exec PROCEDURENAME ").ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Purchase_Price_List__vm> PurchasePriceList(string Vendor_Category, string Item_Group, string Vendor_Priority, string Item_Prioirty, string Territory)
        {
            try
            {
                var VendorCategory1 = new SqlParameter("@Vendor_Category", Vendor_Category == null ? "-1" : Vendor_Category);
                var ItemGroup1 = new SqlParameter("@Item_Group", Item_Group == null ? "-1" : Item_Group);
                var VendorPriority1 = new SqlParameter("@Vendor_Priority", Vendor_Priority == null ? "-1" : Vendor_Priority);
                var ItemPrioirty1 = new SqlParameter("@Item_Prioirty", Item_Prioirty == null ? "-1" : Item_Prioirty);
                var Territory1 = new SqlParameter("@Territory", Territory == null ? "-1" : Territory);
                var val = _scifferContext.Database.SqlQuery<Report_Purchase_Price_List__vm>("exec PROCEDURENAME @Vendor_Category, @Item_Group, @Vendor_Priority, @Item_Prioirty, @Territory", VendorCategory1, ItemGroup1, VendorPriority1, ItemPrioirty1, Territory1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Purchase_Requisition_Summary__vm> PurchaseRequisitionSummary(DateTime? From_Date, DateTime? To_Date, string Item_Category, string Status)
        {
            try
            {
                var FromDate1 = new SqlParameter("@From_Date", From_Date);
                var ToDate1 = new SqlParameter("@To_Date", To_Date);
                var ItemCategory1 = new SqlParameter("@Item_Category", Item_Category == null ? "-1" : Item_Category);
                var Status1 = new SqlParameter("@Status", Status == null ? "-1" : Status);
                var val = _scifferContext.Database.SqlQuery<Report_Purchase_Requisition_Summary__vm>("exec PROCEDURENAME @From_Date, @To_Date, @Item_Category, @Status", FromDate1, ToDate1, ItemCategory1, Status1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Purchase_Requisition_Detailed__vm> PurchaseRequisitionDetailed(DateTime? From_Date, DateTime? To_Date, string Vendir_Category, string Item_Category, string Created_by, string Plant, string Source)
        {
            try
            {
                var FromDate1 = new SqlParameter("@From_Date", From_Date);
                var ToDate1 = new SqlParameter("@To_Date", To_Date);
                var VendirCategory1 = new SqlParameter("@Vendir_Category", Vendir_Category == null ? "-1" : Vendir_Category);
                var ItemCategory1 = new SqlParameter("@Item_Category", Item_Category == null ? "-1" : Item_Category);
                var Createdby1 = new SqlParameter("@Created_by", Created_by == null ? "-1" : Created_by);
                var Plant1 = new SqlParameter("@Plant", Plant == null ? "-1" : Plant);
                var Source1 = new SqlParameter("@Source", Source == null ? "-1" : Source);
                var val = _scifferContext.Database.SqlQuery<Report_Purchase_Requisition_Detailed__vm>("exec PROCEDURENAME @From_Date, @To_Date, @Vendir_Category, @Item_Category, @Created_by, @Plant, @Source", FromDate1, ToDate1, VendirCategory1, ItemCategory1, Createdby1, Plant1, Source1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Open_PR_Report__vm> OpenPRReport(DateTime? As_of_Date_, string Item_Category, string Delivery_Date, string From, string To)
        {
            try
            {
                var AsofDate1 = new SqlParameter("@As_of_Date", As_of_Date_);
                var ItemCategory1 = new SqlParameter("@Item_Category", Item_Category == null ? "-1" : Item_Category);
                var DeliveryDate1 = new SqlParameter("@Delivery_Date", Delivery_Date == null ? "-1" : Delivery_Date);
                var From1 = new SqlParameter("@From", From == null ? "-1" : From);
                var To1 = new SqlParameter("@To", To == null ? "-1" : To);
                var val = _scifferContext.Database.SqlQuery<Report_Open_PR_Report__vm>("exec PROCEDURENAME @As_of_Date, @Item_Category, @Delivery_Date, @From, @To", AsofDate1, ItemCategory1, DeliveryDate1, From1, To1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Purchase_Order_Summary_vm> PurchaseOrderSummary(DateTime? From_date, DateTime? To_date, string Vendor_category, string Priority, string currency, string plant, string business_unit)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var Vendorcategory1 = new SqlParameter("@Vendor_category", Vendor_category == null ? "-1" : Vendor_category);
                var Priority1 = new SqlParameter("@Priority", Priority == null ? "-1" : Priority);
                var currency1 = new SqlParameter("@currency", currency == null ? "-1" : currency);
                var plant1 = new SqlParameter("@plant", plant == null ? "-1" : plant);
                var businessunit1 = new SqlParameter("@business_unit", business_unit == null ? "-1" : business_unit);
                var val = _scifferContext.Database.SqlQuery<Report_Purchase_Order_Summary_vm>("exec PROCEDURENAME @From_date, @To_date, @Vendor_category, @Priority, @currency, @plant, @business_unit", Fromdate1, Todate1, Vendorcategory1, Priority1, currency1, plant1, businessunit1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Purchase_Order_Header__vm> PurchaseOrderHeader(DateTime? From_date, DateTime? To_date, string Vendor_category, string Priority, string currency, string plant, string business_unit, string Vendor_Code, string Item)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var Vendorcategory1 = new SqlParameter("@Vendor_category", Vendor_category == null ? "-1" : Vendor_category);
                var Priority1 = new SqlParameter("@Priority", Priority == null ? "-1" : Priority);
                var currency1 = new SqlParameter("@currency", currency == null ? "-1" : currency);
                var plant1 = new SqlParameter("@plant", plant == null ? "-1" : plant);
                var businessunit1 = new SqlParameter("@business_unit", business_unit == null ? "-1" : business_unit);
                var VendorCode1 = new SqlParameter("@Vendor_Code", Vendor_Code == null ? "-1" : Vendor_Code);
                var Item1 = new SqlParameter("@Item", Item == null ? "-1" : Item);
                var val = _scifferContext.Database.SqlQuery<Report_Purchase_Order_Header__vm>("exec PROCEDURENAME @From_date, @To_date, @Vendor_category, @Priority, @currency, @plant, @business_unit, @Vendor_Code, @Item ", Fromdate1, Todate1, Vendorcategory1, Priority1, currency1, plant1, businessunit1, VendorCode1, Item1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<purchase_order_report_vm> Purchase_Order_Report(string entity, string vendor_category_id, string vendor_priority_id,
       string item_priority_id, string vendor_id, DateTime? from_date, DateTime? to_date, string item_group_id, string territory_id,
       string item_category, string status_id, string created_by, string plant_id, string source_id, DateTime? posting_date,
       string currency_id, string business_unit_id, string item_service_id, string item_id)
        {
            try
            {
                DateTime dte1 = new DateTime(0001, 1, 1);
                DateTime dte = new DateTime(1990, 1, 1);
                var ent = new SqlParameter("@entity", entity);
                var ven_cat = new SqlParameter("@vendor_category_id", vendor_category_id == null ? "-1" : vendor_category_id);
                var ven_pri = new SqlParameter("@vendor_priority_id", vendor_priority_id == null ? "-1" : vendor_priority_id);
                var item_pri = new SqlParameter("@item_priority_id", item_priority_id == null ? "-1" : item_priority_id);
                var vendor = new SqlParameter("@vendor_id", vendor_id == null ? "-1" : vendor_id);
                var fromdate = new SqlParameter("@from_date", from_date == null ? dte : from_date == dte1 ? dte : from_date);
                var todate = new SqlParameter("@to_date", to_date == null ? dte : to_date == dte1 ? dte : to_date);
                var itemgroupid = new SqlParameter("@item_group_id", item_group_id == null ? "-1" : item_group_id);
                var territoryid = new SqlParameter("@territory_id", territory_id == null ? "-1" : territory_id);
                var itemcategory = new SqlParameter("@item_category_id", item_category == null ? "-1" : item_category);
                var statusid = new SqlParameter("@status_id", status_id == null ? "-1" : status_id);
                var createdby = new SqlParameter("@created_by", created_by == null ? "-1" : created_by);
                var plantid = new SqlParameter("@plant_id", plant_id == null ? "-1" : plant_id);
                var sourceid = new SqlParameter("@source_id", source_id == null ? "-1" : source_id);
                var postingdate = new SqlParameter("@posting_date", posting_date == null ? dte : posting_date == dte1 ? dte : posting_date);
                var currencyid = new SqlParameter("@currency_id", currency_id == null ? "-1" : currency_id);
                var businessunit_id = new SqlParameter("@business_unit_id", business_unit_id == null ? "-1" : business_unit_id);
                var item_service = new SqlParameter("@item_service_id", item_service_id == null ? "-1" : item_service_id);
                var item_id1 = new SqlParameter("@item_id", item_id == null ? "-1" : item_id);
                var val = _scifferContext.Database.SqlQuery<purchase_order_report_vm>(
                    "exec report_purchase @entity, @vendor_category_id,@vendor_priority_id,@item_priority_id,@vendor_id,@from_date,@to_date,@item_group_id,@territory_id,@item_category_id,@status_id,@created_by,@plant_id,@source_id,@posting_date,@currency_id,@business_unit_id,@item_service_id,@item_id",
                    ent, ven_cat, ven_pri, item_pri, vendor, fromdate, todate, itemgroupid, territoryid, itemcategory, statusid, createdby, plantid, sourceid, postingdate, currencyid, businessunit_id, item_service, item_id1).ToList();
                return val;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<sales_order_report_vm> Sales_Order_Report(string entity, string customer_category_id, string customer_priority_id,
           string item_priority_id, string customer_id, DateTime? from_date, DateTime? to_date, string item_group_id, string territory_id,
           string item_category, string status_id, string created_by, string plant_id, string source_id, DateTime? posting_date,
           string currency_id, string business_unit_id, string item_service_id, string item_id, string sales_rm)
        {
            try
            {
                DateTime dte1 = new DateTime(0001, 1, 1);
                DateTime dte = new DateTime(1990, 1, 1);
                var ent = new SqlParameter("@entity", entity);
                var cus_cat = new SqlParameter("@customer_category_id", customer_category_id == null ? "-1" : customer_category_id);
                var cus_pri = new SqlParameter("@customer_priority_id", customer_priority_id == null ? "-1" : customer_priority_id);
                var item_pri = new SqlParameter("@item_priority_id", item_priority_id == null ? "-1" : item_priority_id);
                var customer = new SqlParameter("@customer_id", customer_id == null ? "-1" : customer_id);
                var fromdate = new SqlParameter("@from_date", from_date == null ? dte : from_date == dte1 ? dte : from_date);
                var todate = new SqlParameter("@to_date", to_date == null ? dte : to_date == dte1 ? dte : to_date);
                var itemgroupid = new SqlParameter("@item_group_id", item_group_id == null ? "-1" : item_group_id);
                var territoryid = new SqlParameter("@territory_id", territory_id == null ? "-1" : territory_id);
                var itemcategory = new SqlParameter("@item_category_id", item_category == null ? "-1" : item_category);
                var statusid = new SqlParameter("@status_id", status_id == null ? "-1" : status_id);
                var createdby = new SqlParameter("@created_by", created_by == null ? "-1" : created_by);
                var plantid = new SqlParameter("@plant_id", plant_id == null ? "-1" : plant_id);
                var sourceid = new SqlParameter("@source_id", source_id == null ? "-1" : source_id);
                var postingdate = new SqlParameter("@posting_date", posting_date == null ? dte : posting_date == dte1 ? dte : posting_date);
                var currencyid = new SqlParameter("@currency_id", currency_id == null ? "-1" : currency_id);
                var businessunit_id = new SqlParameter("@business_unit_id", business_unit_id == null ? "-1" : business_unit_id);
                var item_service = new SqlParameter("@item_service_id", item_service_id == null ? "-1" : item_service_id);
                var item = new SqlParameter("@item_id", item_id == null ? "-1" : item_id);
                var rm = new SqlParameter("@sales_rm", sales_rm == null ? "-1" : sales_rm);
                var val = _scifferContext.Database.SqlQuery<sales_order_report_vm>(
                    "exec report_sales @entity, @customer_category_id,@customer_priority_id,@item_priority_id,@customer_id,@from_date,@to_date,@item_group_id,@territory_id,@item_category_id,@status_id,@created_by,@plant_id,@source_id,@posting_date,@currency_id,@business_unit_id,@item_service_id,@item_id,@sales_rm",
                    ent, cus_cat, cus_pri, item_pri, customer, fromdate, todate, itemgroupid, territoryid, itemcategory, statusid, createdby, plantid, sourceid, postingdate, currencyid, businessunit_id, item_service, item, rm).ToList();
                return val;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<Report_GRN_Summary__vm> GRNSummary(DateTime? From_date, DateTime? To_date, string Vendor_category, string Priority, string currency, string plant, string business_unit)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var Vendorcategory1 = new SqlParameter("@Vendor_category", Vendor_category == null ? "-1" : Vendor_category);
                var Priority1 = new SqlParameter("@Priority", Priority == null ? "-1" : Priority);
                var currency1 = new SqlParameter("@currency", currency == null ? "-1" : currency);
                var plant1 = new SqlParameter("@plant", plant == null ? "-1" : plant);
                var businessunit1 = new SqlParameter("@business_unit", business_unit == null ? "-1" : business_unit);
                var val = _scifferContext.Database.SqlQuery<Report_GRN_Summary__vm>("exec PROCEDURENAME @From_date, @To_date, @Vendor_category, @Priority, @currency, @plant, @business_unit", Fromdate1, Todate1, Vendorcategory1, Priority1, currency1, plant1, businessunit1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_GRN_Header__vm> GRNHeader(DateTime? From_date, DateTime? To_date, string Vendor_category, string Priority, string currency, string plant, string business_unit, string Vendor_Code)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var Vendorcategory1 = new SqlParameter("@Vendor_category", Vendor_category == null ? "-1" : Vendor_category);
                var Priority1 = new SqlParameter("@Priority", Priority == null ? "-1" : Priority);
                var currency1 = new SqlParameter("@currency", currency == null ? "-1" : currency);
                var plant1 = new SqlParameter("@plant", plant == null ? "-1" : plant);
                var businessunit1 = new SqlParameter("@business_unit", business_unit == null ? "-1" : business_unit);
                var VendorCode1 = new SqlParameter("@Vendor_Code", Vendor_Code == null ? "-1" : Vendor_Code);
                var val = _scifferContext.Database.SqlQuery<Report_GRN_Header__vm>("exec PROCEDURENAME @From_date, @To_date, @Vendor_category, @Priority, @currency, @plant, @business_unit, @Vendor_Code", Fromdate1, Todate1, Vendorcategory1, Priority1, currency1, plant1, businessunit1, VendorCode1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_GRN_Item_Level_vm> GRNItemLevel(DateTime? From_date, DateTime? To_date, string Vendor_category, string Priority, string currency, string plant, string business_unit, string Vendor_Code)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var Vendorcategory1 = new SqlParameter("@Vendor_category", Vendor_category == null ? "-1" : Vendor_category);
                var Priority1 = new SqlParameter("@Priority", Priority == null ? "-1" : Priority);
                var currency1 = new SqlParameter("@currency", currency == null ? "-1" : currency);
                var plant1 = new SqlParameter("@plant", plant == null ? "-1" : plant);
                var businessunit1 = new SqlParameter("@business_unit", business_unit == null ? "-1" : business_unit);
                var VendorCode1 = new SqlParameter("@Vendor_Code", Vendor_Code == null ? "-1" : Vendor_Code);
                var val = _scifferContext.Database.SqlQuery<Report_GRN_Item_Level_vm>("exec PROCEDURENAME @From_date, @To_date, @Vendor_category, @Priority, @currency, @plant, @business_unit, @Vendor_Code", Fromdate1, Todate1, Vendorcategory1, Priority1, currency1, plant1, businessunit1, VendorCode1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_GRN_Pending_for_Excise__vm> GRNPendingforExcise(DateTime? As_of_Date)
        {
            try
            {
                var AsofDate1 = new SqlParameter("@As_of_Date", As_of_Date);
                var val = _scifferContext.Database.SqlQuery<Report_GRN_Pending_for_Excise__vm>("exec PROCEDURENAME @As_of_Date", AsofDate1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }

        public List<Report_Purchase_Invoice_Summary__vm> PurchaseInvoiceSummary(DateTime? From_date, DateTime? To_date, string Vendor_category, string Priority, string currency, string plant, string business_unit)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var Vendorcategory1 = new SqlParameter("@Vendor_category", Vendor_category == null ? "-1" : Vendor_category);
                var Priority1 = new SqlParameter("@Priority", Priority == null ? "-1" : Priority);
                var currency1 = new SqlParameter("@currency", currency == null ? "-1" : currency);
                var plant1 = new SqlParameter("@plant", plant == null ? "-1" : plant);
                var businessunit1 = new SqlParameter("@business_unit", business_unit == null ? "-1" : business_unit);
                var val = _scifferContext.Database.SqlQuery<Report_Purchase_Invoice_Summary__vm>("exec PROCEDURENAME @From_date, @To_date, @Vendor_category, @Priority, @currency, @plant, @business_unit", Fromdate1, Todate1, Vendorcategory1, Priority1, currency1, plant1, businessunit1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Purchase_Invoice_Header__vm> PurchaseInvoiceHeader(DateTime? From_date, DateTime? To_date, string Vendor_category, string Priority, string currency, string plant, string business_unit, string Vendor_Code, string Item_)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var Vendorcategory1 = new SqlParameter("@Vendor_category", Vendor_category == null ? "-1" : Vendor_category);
                var Priority1 = new SqlParameter("@Priority", Priority == null ? "-1" : Priority);
                var currency1 = new SqlParameter("@currency", currency == null ? "-1" : currency);
                var plant1 = new SqlParameter("@plant", plant == null ? "-1" : plant);
                var businessunit1 = new SqlParameter("@business_unit", business_unit == null ? "-1" : business_unit);
                var VendorCode1 = new SqlParameter("@Vendor_Code", Vendor_Code == null ? "-1" : Vendor_Code);
                var Item1 = new SqlParameter("@Item_", Item_ == null ? "-1" : Item_);
                var val = _scifferContext.Database.SqlQuery<Report_Purchase_Invoice_Header__vm>("exec PROCEDURENAME @From_date, @To_date, @Vendor_category, @Priority, @currency, @plant, @business_unit, @Vendor_Code, @Item_", Fromdate1, Todate1, Vendorcategory1, Priority1, currency1, plant1, businessunit1, VendorCode1, Item1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Purchase_Invoice_Item_Level_vm> PurchaseInvoiceItemLevel(DateTime? From_date, DateTime? To_date, string Vendor_category, string Priority, string currency, string plant, string business_unit, string Vendor_Code, string Item_)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var Vendorcategory1 = new SqlParameter("@Vendor_category", Vendor_category == null ? "-1" : Vendor_category);
                var Priority1 = new SqlParameter("@Priority", Priority == null ? "-1" : Priority);
                var currency1 = new SqlParameter("@currency", currency == null ? "-1" : currency);
                var plant1 = new SqlParameter("@plant", plant == null ? "-1" : plant);
                var businessunit1 = new SqlParameter("@business_unit", business_unit == null ? "-1" : business_unit);
                var VendorCode1 = new SqlParameter("@Vendor_Code", Vendor_Code == null ? "-1" : Vendor_Code);
                var Item1 = new SqlParameter("@Item_", Item_ == null ? "-1" : Item_);
                var val = _scifferContext.Database.SqlQuery<Report_Purchase_Invoice_Item_Level_vm>("exec PROCEDURENAME @From_date, @To_date, @Vendor_category, @Priority, @currency, @plant, @business_unit, @Vendor_Code, @Item_", Fromdate1, Todate1, Vendorcategory1, Priority1, currency1, plant1, businessunit1, VendorCode1, Item1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_LD_report__vm> LDreport(DateTime? GRN_From_date, DateTime? GRN_To_date, string Vendor_category, string Priority, string plant, string business_unit, string Vendor_Code)
        {
            try
            {
                var GRNFromdate1 = new SqlParameter("@GRN_From_date", GRN_From_date);
                var GRNTodate1 = new SqlParameter("@GRN_To_date", GRN_To_date);
                var Vendorcategory1 = new SqlParameter("@Vendor_category", Vendor_category == null ? "-1" : Vendor_category);
                var Priority1 = new SqlParameter("@Priority", Priority == null ? "-1" : Priority);
                var plant1 = new SqlParameter("@plant", plant == null ? "-1" : plant);
                var businessunit1 = new SqlParameter("@business_unit", business_unit == null ? "-1" : business_unit);
                var VendorCode1 = new SqlParameter("@Vendor_Code", Vendor_Code == null ? "-1" : Vendor_Code);
                var val = _scifferContext.Database.SqlQuery<Report_LD_report__vm>("exec PROCEDURENAME @GRN_From_date, @GRN_To_date, @Vendor_category, @Priority, @plant, @business_unit, @Vendor_Code", GRNFromdate1, GRNTodate1, Vendorcategory1, Priority1, plant1, businessunit1, VendorCode1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Purchase_Return_Summary__vm> PurchaseReturnSummary(DateTime? From_date, DateTime? To_date, string Vendor_category, string Priority, string currency, string plant, string business_unit)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var Vendorcategory1 = new SqlParameter("@Vendor_category", Vendor_category == null ? "-1" : Vendor_category);
                var Priority1 = new SqlParameter("@Priority", Priority == null ? "-1" : Priority);
                var currency1 = new SqlParameter("@currency", currency == null ? "-1" : currency);
                var plant1 = new SqlParameter("@plant", plant == null ? "-1" : plant);
                var businessunit1 = new SqlParameter("@business_unit", business_unit == null ? "-1" : business_unit);
                var val = _scifferContext.Database.SqlQuery<Report_Purchase_Return_Summary__vm>("exec PROCEDURENAME @From_date, @To_date, @Vendor_category, @Priority, @currency, @plant, @business_unit", Fromdate1, Todate1, Vendorcategory1, Priority1, currency1, plant1, businessunit1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Purchase_Return_Header__vm> PurchaseReturnHeader(DateTime? From_date, DateTime? To_date, string Vendor_category, string Priority, string currency, string plant, string business_unit, string Vendor_Code, string Item_)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var Vendorcategory1 = new SqlParameter("@Vendor_category", Vendor_category == null ? "-1" : Vendor_category);
                var Priority1 = new SqlParameter("@Priority", Priority == null ? "-1" : Priority);
                var currency1 = new SqlParameter("@currency", currency == null ? "-1" : currency);
                var plant1 = new SqlParameter("@plant", plant == null ? "-1" : plant);
                var businessunit1 = new SqlParameter("@business_unit", business_unit == null ? "-1" : business_unit);
                var VendorCode1 = new SqlParameter("@Vendor_Code", Vendor_Code == null ? "-1" : Vendor_Code);
                var Item1 = new SqlParameter("@Item_", Item_ == null ? "-1" : Item_);
                var val = _scifferContext.Database.SqlQuery<Report_Purchase_Return_Header__vm>("exec PROCEDURENAME @From_date, @To_date, @Vendor_category, @Priority, @currency, @plant, @business_unit, @Vendor_Code, @Item_", Fromdate1, Todate1, Vendorcategory1, Priority1, currency1, plant1, businessunit1, VendorCode1, Item1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Purchase_Return_Item_Level_vm> PurchaseReturnItemLevel(DateTime? From_date, DateTime? To_date, string Vendor_category, string Priority, string currency, string plant, string business_unit, string Vendor_Code, string Item_)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var Vendorcategory1 = new SqlParameter("@Vendor_category", Vendor_category == null ? "-1" : Vendor_category);
                var Priority1 = new SqlParameter("@Priority", Priority == null ? "-1" : Priority);
                var currency1 = new SqlParameter("@currency", currency == null ? "-1" : currency);
                var plant1 = new SqlParameter("@plant", plant == null ? "-1" : plant);
                var businessunit1 = new SqlParameter("@business_unit", business_unit == null ? "-1" : business_unit);
                var VendorCode1 = new SqlParameter("@Vendor_Code", Vendor_Code == null ? "-1" : Vendor_Code);
                var Item1 = new SqlParameter("@Item_", Item_ == null ? "-1" : Item_);
                var val = _scifferContext.Database.SqlQuery<Report_Purchase_Return_Item_Level_vm>("exec PROCEDURENAME @From_date, @To_date, @Vendor_category, @Priority, @currency, @plant, @business_unit, @Vendor_Code, @Item_", Fromdate1, Todate1, Vendorcategory1, Priority1, currency1, plant1, businessunit1, VendorCode1, Item1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_PO_to_Payment_Tracker_vm> POtoPaymentTracker(DateTime? GRN_From_Date, DateTime? GRN_TO_Date, string Plant)
        {
            try
            {
                var GRNFromDate1 = new SqlParameter("@GRN_From_Date", GRN_From_Date);
                var GRN_TO_Date1 = new SqlParameter("@GRN_TO_Date", GRN_TO_Date);
                var Plant1 = new SqlParameter("@Plant_", Plant == null ? "-1" : Plant);
                var val = _scifferContext.Database.SqlQuery<Report_PO_to_Payment_Tracker_vm>("exec PROCEDURENAME @GRN_From_Date, @GRN_From_Date, @Plant_", GRNFromDate1, GRN_TO_Date1, Plant1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Sales_Contribution_vm> SalesContribution(DateTime? From_date, DateTime? To_date, string Customer_category, string Priority, string currency, string plant, string business_unit)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var Customercategory1 = new SqlParameter("@Customer_category", Customer_category == null ? "-1" : Customer_category);
                var Priority1 = new SqlParameter("@Priority", Priority == null ? "-1" : Priority);
                var currency1 = new SqlParameter("@currency", currency == null ? "-1" : currency);
                var plant1 = new SqlParameter("@plant", plant == null ? "-1" : plant);
                var businessunit1 = new SqlParameter("@business_unit", business_unit == null ? "-1" : business_unit);
                var val = _scifferContext.Database.SqlQuery<Report_Sales_Contribution_vm>("exec PROCEDURENAME @From_date, @To_date, @Customer_category, @Priority, @currency, @plant, @business_unit", Fromdate1, Todate1, Customercategory1, Priority1, currency1, plant1, businessunit1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_ARAGeingReport_vm> ARAgeingReport(DateTime? As_of_Date_, string Customer_Category_)
        {
            try
            {
                var AsofDate1 = new SqlParameter("@As_of_Date", As_of_Date_);
                var CustomerCategory1 = new SqlParameter("@Customer_Category_", Customer_Category_ == null ? "-1" : Customer_Category_);
                var val = _scifferContext.Database.SqlQuery<Report_ARAGeingReport_vm>("exec PROCEDURENAME @As_of_Date, @Customer_Category_", AsofDate1, CustomerCategory1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_APAgeingReport_vm> APAgeingReport(DateTime? As_of_Date_, string Vendor_Category_)
        {
            try
            {
                var AsofDate1 = new SqlParameter("@As_of_Date", As_of_Date_);
                var VendorCategory1 = new SqlParameter("@Vendor_Category_", Vendor_Category_ == null ? "-1" : Vendor_Category_);
                var val = _scifferContext.Database.SqlQuery<Report_APAgeingReport_vm>("exec PROCEDURENAME @As_of_Date, @Vendor_Category_", AsofDate1, VendorCategory1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }

        public List<Report_LedgerDetails_vm> LedgerDetails(DateTime? From_date, DateTime? To_date, string Account_Type)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var AccountType1 = new SqlParameter("@Account_Type", Account_Type == null ? "-1" : Account_Type);
                var val = _scifferContext.Database.SqlQuery<Report_LedgerDetails_vm>("exec PROCEDURENAME @From_date, @To_date, @Account_Type", Fromdate1, Todate1, AccountType1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }

        }

        public List<Report_TrialBalance_vm> TrialBalance(DateTime? From_date, DateTime? To_date, string Account_Type)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var AccountType1 = new SqlParameter("@Account_Type", Account_Type == null ? "-1" : Account_Type);
                var val = _scifferContext.Database.SqlQuery<Report_TrialBalance_vm>("exec PROCEDURENAME @From_date, @To_date, @Account_Type", Fromdate1, Todate1, AccountType1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_GeneralLedger_vm> GeneralLedger(DateTime? From_date, DateTime? To_date, string Account_Type, string Category)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@From_date", From_date);
                var Todate1 = new SqlParameter("@To_date", To_date);
                var AccountType1 = new SqlParameter("@Account_Type", Account_Type == null ? "-1" : Account_Type);
                var Category1 = new SqlParameter("@Category", Category == null ? "-1" : Category);
                var val = _scifferContext.Database.SqlQuery<Report_GeneralLedger_vm>("exec PROCEDURENAME @From_date, @To_date, @Account_Type, @Category", Fromdate1, Todate1, AccountType1, Category1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Item_Accounting_Report_vm> ItemAccountingReport()
        {
            try
            {
                var val = _scifferContext.Database.SqlQuery<Report_Item_Accounting_Report_vm>("exec PROCEDURENAME ").ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<report_inventory_vm> InventoryReport(string entity, DateTime? from_date, DateTime? to_date, string plant_id, string sloc_id, string bucket_id, string item_id, string item_category_id, string reason_code_id, string sloc_id1, string bucket_id1)
        {
            try
            {
                DateTime dte = new DateTime(1990, 1, 1);
                var ent = new SqlParameter("@entity", entity);
                var fromdate = new SqlParameter("@from_date", from_date == null ? dte : from_date);
                var todate = new SqlParameter("@to_date", to_date == null ? dte : to_date);
                var plantid = new SqlParameter("@plant_id", plant_id == null ? "-1" : plant_id == string.Empty ? "-1" : plant_id);
                var slocid = new SqlParameter("@sloc_id", sloc_id == null ? "-1" : sloc_id);
                var bucketid = new SqlParameter("@bucket_id", bucket_id == null ? "-1" : bucket_id);
                var itemid = new SqlParameter("@item_id", item_id == null ? "-1" : item_id);
                var itemcategoryid = new SqlParameter("@item_category_id", item_category_id == null ? "-1" : item_category_id);
                var reasoncodeid = new SqlParameter("@reason_code_id", reason_code_id == null ? "-1" : reason_code_id);
                var slocid1 = new SqlParameter("@sloc_id1", sloc_id1 == null ? "-1" : sloc_id1);
                var bucketid1 = new SqlParameter("@bucket_id1", bucket_id1 == null ? "-1" : bucket_id1);
                var val = _scifferContext.Database.SqlQuery<report_inventory_vm>(
                    "exec report_inventory_report @entity,@from_date,@to_date,@plant_id,@sloc_id, @bucket_id,@item_id,@item_category_id,@reason_code_id,@sloc_id1,@bucket_id1",
                    ent, fromdate, todate, plantid, slocid, bucketid, itemid, itemcategoryid, reasoncodeid, slocid1, bucketid1).ToList();
                return val;
            }
            catch (Exception ex) { return null; }
        }
        public List<Report_InventoryledgerDetailed_vm> InventoryledgerDetailed(DateTime? From_Date, DateTime? to_Date, string Postingg_date_, string Plant, string Sloc, string Item_Code, string Item_Category, string Bucket)
        {
            try
            {
                var FromDate1 = new SqlParameter("@From_Date", From_Date);
                var toDate1 = new SqlParameter("@to_Date", to_Date);
                var Postinggdate1 = new SqlParameter("@Postingg_date_", Postingg_date_ == null ? "-1" : Postingg_date_);
                var Plant1 = new SqlParameter("@Plant", Plant == null ? "-1" : Plant);
                var Sloc1 = new SqlParameter("@Sloc", Sloc == null ? "-1" : Sloc);
                var ItemCode1 = new SqlParameter("@Item_Code", Item_Code == null ? "-1" : Item_Code);
                var ItemCategory1 = new SqlParameter("@Item_Category", Item_Category == null ? "-1" : Item_Category);
                var Bucket1 = new SqlParameter("@Bucket", Bucket == null ? "-1" : Bucket);
                var val = _scifferContext.Database.SqlQuery<Report_InventoryledgerDetailed_vm>("exec PROCEDURENAME @From_Date, @to_Date, @Postingg_date_, @Plant, @Sloc, @Item_Code, @Item_Category, @Bucket", FromDate1, toDate1, Postinggdate1, Plant1, Sloc1, ItemCode1, ItemCategory1, Bucket1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_Inventory_ledger_Summary_vm> InventoryledgerSummary(DateTime? From_Date, DateTime? to_Date)
        {
            try
            {
                var FromDate1 = new SqlParameter("@From_Date", From_Date);
                var toDate1 = new SqlParameter("@to_Date", to_Date);
                var val = _scifferContext.Database.SqlQuery<Report_Inventory_ledger_Summary_vm>("exec PROCEDURENAME @From_Date, @to_Date", FromDate1, toDate1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_InventoryCostingReport_vm> InventoryCostingReport()
        {
            try
            {
                var val = _scifferContext.Database.SqlQuery<Report_InventoryCostingReport_vm>("exec report_inventory_costing").ToList();
                HttpContext.Current.Session["InventoryCosting"] = null;
                HttpContext.Current.Session["InventoryCosting"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_InventoryValuationReport_vm> InventoryValuationReport()
        {
            try
            {
                var val = _scifferContext.Database.SqlQuery<Report_InventoryValuationReport_vm>("exec report_inventory_valuation").ToList();
                HttpContext.Current.Session["InventoryValuationReport"] = null;
                HttpContext.Current.Session["InventoryValuationReport"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_GoodsReceipt_vm> GoodsReceiptreport(DateTime? From_Date, DateTime? To_Date, string Plant, string Sloc, string Item_Code, string Item_Category, string Bucket, string Reason_Code)
        {
            try
            {
                var FromDate1 = new SqlParameter("@From_Date", From_Date);
                var ToDate1 = new SqlParameter("@To_Date", To_Date);
                var Plant1 = new SqlParameter("@Plant", Plant == null ? "-1" : Plant);
                var Sloc1 = new SqlParameter("@Sloc", Sloc == null ? "-1" : Sloc);
                var ItemCode1 = new SqlParameter("@Item_Code", Item_Code == null ? "-1" : Item_Code);
                var ItemCategory1 = new SqlParameter("@Item_Category", Item_Category == null ? "-1" : Item_Category);
                var Bucket1 = new SqlParameter("@Bucket", Bucket == null ? "-1" : Bucket);
                var ReasonCode1 = new SqlParameter("@Reason_Code", Reason_Code == null ? "-1" : Reason_Code);
                var val = _scifferContext.Database.SqlQuery<Report_GoodsReceipt_vm>("exec PROCEDURENAME @From_Date, @To_Date, @Plant, @Sloc, @Item_Code, @Item_Category, @Bucket, @Reason_Code", FromDate1, ToDate1, Plant1, Sloc1, ItemCode1, ItemCategory1, Bucket1, ReasonCode1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_GoodsIssue_vm> Goodsissuereport(DateTime? From_Date, DateTime? To_Date, string Plant, string Sloc, string Item_Code, string Item_Category, string Bucket, string Reason_Code)
        {
            try
            {
                var FromDate1 = new SqlParameter("@From_Date", From_Date);
                var ToDate1 = new SqlParameter("@To_Date", To_Date);
                var Plant1 = new SqlParameter("@Plant", Plant == null ? "-1" : Plant);
                var Sloc1 = new SqlParameter("@Sloc", Sloc == null ? "-1" : Sloc);
                var ItemCode1 = new SqlParameter("@Item_Code", Item_Code == null ? "-1" : Item_Code);
                var ItemCategory1 = new SqlParameter("@Item_Category", Item_Category == null ? "-1" : Item_Category);
                var Bucket1 = new SqlParameter("@Bucket", Bucket == null ? "-1" : Bucket);
                var ReasonCode1 = new SqlParameter("@Reason_Code", Reason_Code == null ? "-1" : Reason_Code);
                var val = _scifferContext.Database.SqlQuery<Report_GoodsIssue_vm>("exec PROCEDURENAME @From_Date, @To_Date, @Plant, @Sloc, @Item_Code, @Item_Category, @Bucket, @Reason_Code", FromDate1, ToDate1, Plant1, Sloc1, ItemCode1, ItemCategory1, Bucket1, ReasonCode1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_WithinPlanTrasfer_vm> WithinPlanTrasfer(DateTime? From_Date, DateTime? To_Date, string Plant, string Sloc, string Item_Code, string Item_Category, string Bucket, string Reason_Code)
        {
            try
            {
                var FromDate1 = new SqlParameter("@From_Date", From_Date);
                var ToDate1 = new SqlParameter("@To_Date", To_Date);
                var Plant1 = new SqlParameter("@Plant", Plant == null ? "-1" : Plant);
                var Sloc1 = new SqlParameter("@Sloc", Sloc == null ? "-1" : Sloc);
                var ItemCode1 = new SqlParameter("@Item_Code", Item_Code == null ? "-1" : Item_Code);
                var ItemCategory1 = new SqlParameter("@Item_Category", Item_Category == null ? "-1" : Item_Category);
                var Bucket1 = new SqlParameter("@Bucket", Bucket == null ? "-1" : Bucket);
                var ReasonCode1 = new SqlParameter("@Reason_Code", Reason_Code == null ? "-1" : Reason_Code);
                var val = _scifferContext.Database.SqlQuery<Report_WithinPlanTrasfer_vm>("exec PROCEDURENAME @From_Date, @To_Date, @Plant, @Sloc, @Item_Code, @Item_Category, @Bucket, @Reason_Code", FromDate1, ToDate1, Plant1, Sloc1, ItemCode1, ItemCategory1, Bucket1, ReasonCode1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_InventoryRevalautionReport_vm> InventoryRevalautionReport(DateTime? From_Date, DateTime? To_Date, string Plant, string Item_Category, string Item_Code)
        {
            try
            {
                var FromDate1 = new SqlParameter("@From_Date", From_Date);
                var ToDate1 = new SqlParameter("@To_Date", To_Date);
                var Plant1 = new SqlParameter("@Plant", Plant == null ? "-1" : Plant);
                var ItemCategory1 = new SqlParameter("@Item_Category", Item_Category == null ? "-1" : Item_Category);
                var ItemCode1 = new SqlParameter("@Item_Code", Item_Code == null ? "-1" : Item_Code);
                var val = _scifferContext.Database.SqlQuery<Report_InventoryRevalautionReport_vm>("exec PROCEDURENAME @From_Date, @To_Date, @Plant, @Item_Category, @Item_Code", FromDate1, ToDate1, Plant1, ItemCategory1, ItemCode1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_InventoryRateHistory_vm> InventoryrateHistory(string Item_Code_, string Item_Category_)
        {
            try
            {
                var ItemCode1 = new SqlParameter("@Item_Code_", Item_Code_ == null ? "-1" : Item_Code_);
                var ItemCategory1 = new SqlParameter("@Item_Category_", Item_Category_ == null ? "-1" : Item_Category_);
                var val = _scifferContext.Database.SqlQuery<Report_InventoryRateHistory_vm>("exec PROCEDURENAME @Item_Code_, @Item_Category_", ItemCode1, ItemCategory1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_MaterialRequisitionReportStatusDetailed_vm> MaterialRequisitionReportStatusDetailed(DateTime? From, DateTime? To)
        {
            try
            {
                var From1 = new SqlParameter("@From", From);
                var To1 = new SqlParameter("@To", To);
                var val = _scifferContext.Database.SqlQuery<Report_MaterialRequisitionReportStatusDetailed_vm>("exec PROCEDURENAME @From, @To", From1, To1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_MaterialRequisitionReportStatusSummary_vm> MaterialRequisitionReportStatusSummary(DateTime? From, DateTime? To)
        {
            try
            {
                var From1 = new SqlParameter("@From", From);
                var To1 = new SqlParameter("@To", To);
                var val = _scifferContext.Database.SqlQuery<Report_MaterialRequisitionReportStatusSummary_vm>("exec PROCEDURENAME @From, @To", From1, To1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_OpenMaterialRequisitionReport_vm> OpenMaterialRequisitionReport(DateTime? As_of_)
        {
            try
            {
                var Asof1 = new SqlParameter("@As_of_", As_of_);
                var val = _scifferContext.Database.SqlQuery<Report_OpenMaterialRequisitionReport_vm>("exec PROCEDURENAME @As_of_", Asof1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_QCLotSummaryReport_vm> QCLotSummaryReport(DateTime? From_Date, DateTime? To_Date, string Status, string Plant, string Item_Code, string Base_DOcument)
        {
            try
            {
                var FromDate1 = new SqlParameter("@From_Date", From_Date);
                var ToDate1 = new SqlParameter("@To_Date", To_Date);
                var Status1 = new SqlParameter("@Status", Status == null ? "-1" : Status);
                var Plant1 = new SqlParameter("@Plant", Plant == null ? "-1" : Plant);
                var ItemCode1 = new SqlParameter("@Item_Code", Item_Code == null ? "-1" : Item_Code);
                var BaseDOcument1 = new SqlParameter("@Base_DOcument", Base_DOcument == null ? "-1" : Base_DOcument);
                var val = _scifferContext.Database.SqlQuery<Report_QCLotSummaryReport_vm>("exec PROCEDURENAME @From_Date, @To_Date, @Status, @Plant, @Item_Code, @Base_DOcument", FromDate1, ToDate1, Status1, Plant1, ItemCode1, BaseDOcument1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_QCParametersReport_vm> QCParametersReport(DateTime? From_Date, DateTime? To_Date, string Status, string Plant, string Item_Code, string Base_DOcument)
        {
            try
            {
                var FromDate1 = new SqlParameter("@From_Date", From_Date);
                var ToDate1 = new SqlParameter("@To_Date", To_Date);
                var Status1 = new SqlParameter("@Status", Status == null ? "-1" : Status);
                var Plant1 = new SqlParameter("@Plant", Plant == null ? "-1" : Plant);
                var ItemCode1 = new SqlParameter("@Item_Code", Item_Code == null ? "-1" : Item_Code);
                var BaseDOcument1 = new SqlParameter("@Base_DOcument", Base_DOcument == null ? "-1" : Base_DOcument);
                var val = _scifferContext.Database.SqlQuery<Report_QCParametersReport_vm>("exec PROCEDURENAME @From_Date, @To_Date, @Status, @Plant, @Item_Code, @Base_DOcument", FromDate1, ToDate1, Status1, Plant1, ItemCode1, BaseDOcument1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_QCUsageDecisionReport_vm> QCUsageDecisionReport(DateTime? From_Date, DateTime? To_Date, string Status, string Plant, string Item_Code, string Base_DOcument)
        {
            try
            {
                var FromDate1 = new SqlParameter("@From_Date", From_Date);
                var ToDate1 = new SqlParameter("@To_Date", To_Date);
                var Status1 = new SqlParameter("@Status", Status == null ? "-1" : Status);
                var Plant1 = new SqlParameter("@Plant", Plant == null ? "-1" : Plant);
                var ItemCode1 = new SqlParameter("@Item_Code", Item_Code == null ? "-1" : Item_Code);
                var BaseDOcument1 = new SqlParameter("@Base_DOcument", Base_DOcument == null ? "-1" : Base_DOcument);
                var val = _scifferContext.Database.SqlQuery<Report_QCUsageDecisionReport_vm>("exec PROCEDURENAME @From_Date, @To_Date, @Status, @Plant, @Item_Code, @Base_DOcument", FromDate1, ToDate1, Status1, Plant1, ItemCode1, BaseDOcument1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_QCLOTShelfLifeReport_vm> QCLOTShelfLifeReport(DateTime? From_Date, DateTime? To_Date, string Status, string Plant, string Item_Code, string Base_DOcument)
        {
            try
            {
                var FromDate1 = new SqlParameter("@From_Date", From_Date);
                var ToDate1 = new SqlParameter("@To_Date", To_Date);
                var Status1 = new SqlParameter("@Status", Status == null ? "-1" : Status);
                var Plant1 = new SqlParameter("@Plant", Plant == null ? "-1" : Plant);
                var ItemCode1 = new SqlParameter("@Item_Code", Item_Code == null ? "-1" : Item_Code);
                var BaseDOcument1 = new SqlParameter("@Base_DOcument", Base_DOcument == null ? "-1" : Base_DOcument);
                var val = _scifferContext.Database.SqlQuery<Report_QCLOTShelfLifeReport_vm>("exec PROCEDURENAME @From_Date, @To_Date, @Status, @Plant, @Item_Code, @Base_DOcument", FromDate1, ToDate1, Status1, Plant1, ItemCode1, BaseDOcument1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_ShelfLifeExpiredReport_vm> ShelfLifeExpiredReport(DateTime? As_of_Date_, string Plant, string Item_Category, string Item_Code, string SLoc)
        {
            try
            {
                var AsofDate1 = new SqlParameter("@As_of_Date", As_of_Date_);
                var Plant1 = new SqlParameter("@Plant", Plant == null ? "-1" : Plant);
                var ItemCategory1 = new SqlParameter("@Item_Category", Item_Category == null ? "-1" : Item_Category);
                var ItemCode1 = new SqlParameter("@Item_Code", Item_Code == null ? "-1" : Item_Code);
                var SLoc1 = new SqlParameter("@SLoc", SLoc == null ? "-1" : SLoc);
                var val = _scifferContext.Database.SqlQuery<Report_ShelfLifeExpiredReport_vm>("exec PROCEDURENAME @As_of_Date, @Plant, @Item_Category, @Item_Code, @SLoc", AsofDate1, Plant1, ItemCategory1, ItemCode1, SLoc1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_ShelfLifeAbouttoExpireReport_vm> ShelfLifeAbouttoExpireReport(DateTime? From_Date, DateTime? To_Date, string Plant, string Item_Category, string Item_Code, string SLoc)
        {
            try
            {
                var FromDate1 = new SqlParameter("@From_Date", From_Date);
                var ToDate1 = new SqlParameter("@To_Date", To_Date);
                var Plant1 = new SqlParameter("@Plant", Plant == null ? "-1" : Plant);
                var ItemCategory1 = new SqlParameter("@Item_Category", Item_Category == null ? "-1" : Item_Category);
                var ItemCode1 = new SqlParameter("@Item_Code", Item_Code == null ? "-1" : Item_Code);
                var SLoc1 = new SqlParameter("@SLoc", SLoc == null ? "-1" : SLoc);
                var val = _scifferContext.Database.SqlQuery<Report_ShelfLifeAbouttoExpireReport_vm>("exec PROCEDURENAME @From_Date, @From_Date, @Plant, @Item_Category, @Item_Code, @SLoc", FromDate1, ToDate1, Plant1, ItemCategory1, ItemCode1, SLoc1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_BatchRevalidated_vm> BatchRevalidAtedreport(DateTime? From_Date, DateTime? To_Date, string pLANT, string iTEM_cATEGORY, string iTEM_cODE)
        {
            try
            {
                var FromDate1 = new SqlParameter("@From_Date", From_Date);
                var ToDate1 = new SqlParameter("@To_Date", To_Date);
                var pLANT1 = new SqlParameter("@pLANT", pLANT == null ? "-1" : pLANT);
                var iTEMcATEGORY1 = new SqlParameter("@iTEM_cATEGORY", iTEM_cATEGORY == null ? "-1" : iTEM_cATEGORY);
                var iTEMcODE1 = new SqlParameter("@iTEM_cODE", iTEM_cODE == null ? "-1" : iTEM_cODE);
                var val = _scifferContext.Database.SqlQuery<Report_BatchRevalidated_vm>("exec PROCEDURENAME @From_Date, @To_Date, @pLANT, @iTEM_cATEGORY, @iTEM_cODE", FromDate1, ToDate1, pLANT1, iTEMcATEGORY1, iTEMcODE1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_MATERIAL_CONSUMEDPOSTEXPIRY_vm> MATERIALcONSUMEDPOSTEXPIRY(string pLANT, string iTEM_cATEGORY, string iTEM_cODE, string Reason_code)
        {
            try
            {
                var pLANT1 = new SqlParameter("@pLANT", pLANT == null ? "-1" : pLANT);
                var iTEMcATEGORY1 = new SqlParameter("@iTEM_cATEGORY", iTEM_cATEGORY == null ? "-1" : iTEM_cATEGORY);
                var iTEMcODE1 = new SqlParameter("@iTEM_cODE", iTEM_cODE == null ? "-1" : iTEM_cODE);
                var Reasoncode1 = new SqlParameter("@Reason_code", Reason_code == null ? "-1" : Reason_code);
                var val = _scifferContext.Database.SqlQuery<Report_MATERIAL_CONSUMEDPOSTEXPIRY_vm>("exec PROCEDURENAME @pLANT, @iTEM_cATEGORY, @iTEM_cODE, @Reason_code", pLANT1, iTEMcATEGORY1, iTEMcODE1, Reasoncode1).ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_ITEM_MASTERQCPARAMETERS_vm> ITEMMASTERQCPARAMETERS()
        {
            try
            {
                var val = _scifferContext.Database.SqlQuery<Report_ITEM_MASTERQCPARAMETERS_vm>("exec PROCEDURENAME ").ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_BatchRevalidationCount_vm> BatchRevalidationCountreport()
        {
            try
            {
                var val = _scifferContext.Database.SqlQuery<Report_BatchRevalidationCount_vm>("exec PROCEDURENAME ").ToList();
                HttpContext.Current.Session["report"] = null;
                HttpContext.Current.Session["report"] = val.ToList();
                return val;
            }
            catch { return null; }
        }

        public List<Report_CustomerLedgerDetails_vm> Report_CustomerLedgerDetails_vm(DateTime? From_date, DateTime? To_date, string Customer)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@start_date", From_date);
                var Todate1 = new SqlParameter("@end_date", To_date);
                var Customer1 = new SqlParameter("@customer_id", Customer);
                var val = _scifferContext.Database.SqlQuery<Report_CustomerLedgerDetails_vm>("exec report_customer_ledger_details @customer_id, @start_date, @end_date", Customer1, Fromdate1, Todate1).ToList();
                HttpContext.Current.Session["CustomerLedgerDetails"] = null;
                HttpContext.Current.Session["CustomerLedgerDetails"] = val.ToList();
                return val;
            }
            catch { return null; }
        }

        public List<Report_LedgerDetails_vm> Report_LedgerDetails_vm(DateTime? From_date, DateTime? To_date, string Customer)
        {
            throw new NotImplementedException();
        }
        public List<Report_VendorLedgerDetails_vm> Report_VendorLedgerDetails_vm(DateTime? From_date, DateTime? To_date, string vendor)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@start_date", From_date);
                var Todate1 = new SqlParameter("@end_date", To_date);
                var vendor1 = new SqlParameter("@vendor_id", vendor);
                var val = _scifferContext.Database.SqlQuery<Report_VendorLedgerDetails_vm>("exec report_vendor_ledger_details @vendor_id, @start_date, @end_date", vendor1, Fromdate1, Todate1).ToList();
                HttpContext.Current.Session["VendorLedgerDetails"] = null;
                HttpContext.Current.Session["VendorLedgerDetails"] = val.ToList();
                return val;
            }
            catch { return null; }
        }
        public List<Report_EmployeeLedgerDetails_vm> Report_EmployeeLedgerDetails_vm(DateTime? From_date, DateTime? To_date, string employee)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@start_date", From_date);
                var Todate1 = new SqlParameter("@end_date", To_date);
                var employee1 = new SqlParameter("@employee_id", employee);
                var val = _scifferContext.Database.SqlQuery<Report_EmployeeLedgerDetails_vm>("exec report_employee_ledger_details @employee_id, @start_date, @end_date", employee1, Fromdate1, Todate1).ToList();
                HttpContext.Current.Session["EmployeeLedgerDetails"] = null;
                HttpContext.Current.Session["EmployeeLedgerDetails"] = val.ToList();
                return val;
            }
            catch
            {
                return null;
            }
        }

        public List<Report_GeneralLedgerDetails_vm> Report_GeneralLedgerDetails_vm(DateTime? From_date, DateTime? To_date, string gl_account)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@start_date", From_date);
                var Todate1 = new SqlParameter("@end_date", To_date);
                var GL_Account = new SqlParameter("@gl_ledger_id", gl_account);
                var val = _scifferContext.Database.SqlQuery<Report_GeneralLedgerDetails_vm>("exec report_general_ledger_details @gl_ledger_id, @start_date, @end_date", GL_Account, Fromdate1, Todate1).ToList();
                HttpContext.Current.Session["GeneralLedgerDetails"] = null;
                HttpContext.Current.Session["GeneralLedgerDetails"] = val.ToList();
                return val;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<Report_TrialBalance_vm> Report_TrialBalance_vm(DateTime? From_date, DateTime? To_date)
        {
            try
            {
                var Fromdate1 = new SqlParameter("@start_date", From_date);
                var Todate1 = new SqlParameter("@end_date", To_date);
                var val = _scifferContext.Database.SqlQuery<Report_TrialBalance_vm>("exec report_trial_balance @start_date, @end_date", Fromdate1, Todate1).ToList();
                HttpContext.Current.Session["TrialBalance"] = null;
                HttpContext.Current.Session["TrialBalance"] = val.ToList();
                return val;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<fin_payment_receipt_report> Payment_Receipt_Report(string entity, string bank_cash_account_id, DateTime? from_date,
      DateTime? to_date, int? cash_bank, int? in_out)
        {
            try
            {
                DateTime dte = new DateTime(1990, 1, 1);
                DateTime dte1 = new DateTime(0001, 1, 1);
                var ent = new SqlParameter("@entity", entity);
                var bank_cash_account = new SqlParameter("@bank_cash_account_id", bank_cash_account_id == null ? "-1" : bank_cash_account_id);
                var fromdate = new SqlParameter("@from_date", from_date == null ? dte : from_date == dte1 ? dte : from_date);
                var todate = new SqlParameter("@to_date", to_date == null ? dte : to_date);
                var cashbank = new SqlParameter("@cash_bank", cash_bank == null ? 0 : cash_bank);
                var inout = new SqlParameter("@in_out", in_out == null ? 0 : in_out);
                var val = _scifferContext.Database.SqlQuery<fin_payment_receipt_report>(
                   "exec report_payment_receipt @entity,@bank_cash_account_id,@from_date,@to_date,@cash_bank,@in_out",
                   ent, bank_cash_account, fromdate, todate, cashbank, inout).ToList();
                return val;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<mfg_machine_task_VM> Machine_Entry_Report(string entity, string tag_number, string item_list, string prod_order_list, string item_status_id, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                DateTime dte = new DateTime(1990, 1, 1);
                DateTime dte1 = new DateTime(0001, 1, 1);

                var ent = new SqlParameter("@entity", entity);
                var fromdate = new SqlParameter("@from_date", fromDate == null ? dte : fromDate == dte1 ? dte : fromDate);
                var todate = new SqlParameter("@to_date", toDate == null ? dte : toDate);
                var _tagnumber = new SqlParameter("@tag_number", tag_number == null || tag_number == "" ? "-1" : tag_number);
                var _item_id = new SqlParameter("@item_id", item_list == null || item_list == "" ? "-1" : item_list);
                var _prod_order_id = new SqlParameter("@prod_order_id", prod_order_list == null || prod_order_list == "" ? "-1" : prod_order_list);
                var _item_status_id = new SqlParameter("@item_status_id", item_status_id == null || item_status_id == "" ? "-1" : item_status_id);

                var val = _scifferContext.Database.SqlQuery<mfg_machine_task_VM>(
                    "exec report_tag_wise_machine_entry @entity,@from_date,@to_date,@tag_number,@item_id,@prod_order_id,@item_status_id",
                      ent, fromdate, todate, _tagnumber, _item_id, _prod_order_id, _item_status_id).ToList();

                foreach (var item in val)
                {
                    var machine_name = "";
                    var machine_id = item.machine_id.Split('/');
                    var count = 0;
                    foreach (var zz in machine_id)
                    {
                        count++;
                        var id = int.Parse(zz);
                        if (count == machine_id.Count())
                        {
                            machine_name = machine_name + _scifferContext.ref_machine.Where(x => x.machine_id == id).FirstOrDefault().machine_name;
                        }
                        else
                        {
                            machine_name = machine_name + _scifferContext.ref_machine.Where(x => x.machine_id == id).FirstOrDefault().machine_name + ", ";
                        }
                    }
                    item.machine_name = machine_name;
                }
                return val;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<accounts_report_vm> GetAccountsReport(string entity, DateTime? from_date, DateTime? to_date, string customer_category_id, string priority_id,
            string currency_id, string plant_id, string business_unit_id, string document_based_on, string days_per_interval, string no_of_interval, string entity_id,
            string tds_code_id, string show_value_by, string entity_type_id)
        {
            try
            {
                DateTime dte1 = new DateTime(0001, 1, 1);
                DateTime dte = new DateTime(1990, 1, 1);
                var ent = new SqlParameter("@entity", entity);
                var fromdate = new SqlParameter("@from_date", from_date == null ? dte : from_date == dte1 ? dte : from_date);
                var todate = new SqlParameter("@to_date", to_date == null ? dte : to_date == dte1 ? dte : to_date);
                var cus_cat = new SqlParameter("@customer_category_id", customer_category_id == null ? "-1" : customer_category_id);
                var pri = new SqlParameter("@priority_id", priority_id == null ? "-1" : priority_id);
                var currencyid = new SqlParameter("@currency_id", currency_id == null ? "-1" : currency_id);
                var plantid = new SqlParameter("@plant_id", plant_id == null ? "-1" : plant_id);
                var businessunit_id = new SqlParameter("@business_unit_id", business_unit_id == null ? "-1" : business_unit_id);
                var document_based = new SqlParameter("@document_based_on", document_based_on == null ? "-1" : document_based_on);
                var days_per = new SqlParameter("@days_per_interval", days_per_interval == null ? "-1" : days_per_interval);
                var no_ofinterval = new SqlParameter("@no_of_interval", no_of_interval == null ? "-1" : no_of_interval);
                var entityid = new SqlParameter("@entity_id", entity_id == null ? "-1" : entity_id);
                var tdscodeid = new SqlParameter("@tds_code_id", tds_code_id == null ? "-1" : tds_code_id);
                var show_valueby = new SqlParameter("@show_value_by", show_value_by == null ? "-1" : show_value_by);
                var entity_type = new SqlParameter("@entity_type_id", entity_type_id == null ? "-1" : entity_type_id);
                var val = _scifferContext.Database.SqlQuery<accounts_report_vm>(
                    "exec reports_account @entity,@from_date,@to_date, @customer_category_id,@priority_id,@currency_id,@plant_id,@business_unit_id,@document_based_on,@days_per_interval,@no_of_interval,@entity_id,@tds_code_id,@show_value_by,@entity_type_id",
                    ent, fromdate, todate, cus_cat, pri, currencyid, plantid, businessunit_id, document_based, days_per, no_ofinterval, entityid, tdscodeid, show_valueby, entity_type).ToList();
                return val;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<gstr_report_vm> GETGSTRReport(string entity, DateTime from_date, DateTime to_date, string plant_id)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            DateTime dte1 = new DateTime(0001, 1, 1);
            var ent = new SqlParameter("@entity", entity);
            var plant = new SqlParameter("@plant_id", plant_id == null ? "-1" : plant_id);
            var fromdate = new SqlParameter("@from_date", from_date == null ? dte : from_date == dte1 ? dte : from_date);
            var todate = new SqlParameter("@to_date", to_date == null ? dte : to_date);
            var val = _scifferContext.Database.SqlQuery<gstr_report_vm>(
               "exec report_gstr @entity,@from_date,@to_date,@plant_id",
               ent, fromdate, todate, plant).ToList();
            return val;
        }
        public List<dashboard_vm> GetDashBoardData(string entity)
        {
            var ent = new SqlParameter("@entity", entity);
            var val = _scifferContext.Database.SqlQuery<dashboard_vm>(
               "exec report_dashboard @entity", ent).ToList();
            return val;
        }
        public List<Report_incentive_vm> GetIncentiveReport(string entity, DateTime from_date, DateTime to_date, string plant_id)
        {
            var ent = new SqlParameter("@entity", entity);
            var fromDate = new SqlParameter("@from_date", from_date);
            var toDate = new SqlParameter("@to_date", to_date);
            var plant = new SqlParameter("@plant_id", plant_id == null ? "" : plant_id);
            var val = _scifferContext.Database.SqlQuery<Report_incentive_vm>(
                "exec report_get_incentive @entity,@from_date,@to_date,@plant_id",
                  ent, fromDate, toDate, plant).ToList();
            return val;
        }

        public List<Report_NCRejection_vm> GetNCRejecetionReport(string entity, DateTime from_date, DateTime to_date)
        {
            var ent = new SqlParameter("@entity", entity);
            var fromDate = new SqlParameter("@from_date", from_date);
            var toDate = new SqlParameter("@to_date", to_date);
            var val = _scifferContext.Database.SqlQuery<Report_NCRejection_vm>(
                "exec report_get_nc_rejection @entity,@from_date,@to_date",
                  ent, fromDate, toDate).ToList();
            return val;
        }

        //MATERIAL IN OUT DETAIL REPORTS

        public List<report_material_in_out_vm> MaterialInOutDetailReport(string entity, DateTime? from_date, DateTime? to_date, string plant_id, string item_id, string employee_id)
        {
            try
            {
                DateTime dte = new DateTime(1990, 1, 1);
                var ent = new SqlParameter("@entity", entity);
                var fromdate = new SqlParameter("@from_date", from_date == null ? dte : from_date);
                var todate = new SqlParameter("@to_date", to_date == null ? dte : to_date);
                var plantid = new SqlParameter("@plant_id", plant_id == null ? "-1" : plant_id == string.Empty ? "-1" : plant_id);
                var itemid = new SqlParameter("@item_id", item_id == null ? "-1" : item_id);
                var employeeid = new SqlParameter("@employee_id", employee_id == null ? "-1" : employee_id);

                var val = _scifferContext.Database.SqlQuery<report_material_in_out_vm>(
                    "exec Report_MaterialInOut @entity,@from_date,@to_date,@plant_id,@item_id,@employee_id",
                    ent, fromdate, todate, plantid, itemid, employeeid).ToList();
                return val;
            }
            catch (Exception ex) { return null; }
        }

        //OEE REPORT

        public List<Report_OEE_vm> OverallEquipmentEffectivenessReport(string entity, DateTime? from_date, DateTime? to_date, string shift_id, string item_id, string machine_id, string supervisor_id, string plant_id)
        {
            try
            {
                DateTime dte = new DateTime(1990, 1, 1);
                var ent = new SqlParameter("@entity", entity);
                var fromdate = new SqlParameter("@from_date", from_date == null ? dte : from_date);
                var todate = new SqlParameter("@to_date", to_date == null ? dte : to_date);
                var shiftid = new SqlParameter("@shift_id", shift_id == null ? "-1" : shift_id == string.Empty ? "-1" : shift_id);
                var itemid = new SqlParameter("@item_id", item_id == null ? "-1" : item_id);
                var machineid = new SqlParameter("@machine_id", machine_id == null ? "-1" : machine_id);
                var supervisorid = new SqlParameter("@supervisor_id", supervisor_id == null ? "-1" : supervisor_id);
                var plantid = new SqlParameter("@plant_id", plant_id == null ? "-1" : plant_id);
                var val = _scifferContext.Database.SqlQuery<Report_OEE_vm>(
                    "exec Report_OverallEquipmentEffectiveness @entity,@from_date,@to_date,@shift_id,@item_id,@machine_id,@supervisor_id,@plant_id",
                    ent, fromdate, todate, shiftid, itemid, machineid, supervisorid, plantid).ToList();
                return val;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<fixed_asset_report_vm> FixedAssetReport(string entity, DateTime? from_date, DateTime? to_date, string depreciation_type, string asset_code_id, string depreciation_area_id, string asset_class_id, string asset_group_id)
        {
            try
            {
                DateTime dte1 = new DateTime(0001, 1, 1);
                DateTime dte = new DateTime(1990, 1, 1);
                var ent = new SqlParameter("@entity", entity);
                var fromdate = new SqlParameter("@from_date", from_date == null ? dte : from_date == dte1 ? dte : from_date);
                var todate = new SqlParameter("@to_date", to_date == null ? dte : to_date == dte1 ? dte : to_date);
                var depreciationtype = new SqlParameter("@depreciation_type", depreciation_type);
                var asset_codeid = new SqlParameter("@asset_code_id", asset_code_id == null ? "-1" : asset_code_id);
                var depreciation_areaid = new SqlParameter("@depreciation_area_id", depreciation_area_id == null ? "-1" : depreciation_area_id);
                var asset_classid = new SqlParameter("@asset_class_id", asset_class_id == null ? "-1" : asset_class_id);
                var asset_groupid = new SqlParameter("@asset_group_id", asset_group_id == null ? "-1" : asset_group_id);

                var val = _scifferContext.Database.SqlQuery<fixed_asset_report_vm>(
                    "exec report_fixed_asset @entity , @from_date, @to_date ,@depreciation_type ,@asset_code_id,@depreciation_area_id ,@asset_class_id ,@asset_group_id",
                    ent, fromdate, todate, depreciationtype, asset_codeid, depreciation_areaid, asset_classid, asset_groupid).ToList();
                return val;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<report_inventory_vm> InventoryReport1(string entity, string item_id, string process_id3)
        {
            try
            {
                DateTime dte = new DateTime(1990, 1, 1);
                var ent = new SqlParameter("@entity", entity);
                var fromdate = new SqlParameter("@from_date", dte);
                var to_date = new SqlParameter("@to_date", dte);
                var plant_id = new SqlParameter("@plant_id", process_id3 == null ? "-1" : process_id3);
                var sloc_id = new SqlParameter("@sloc_id", "-1");
                var bucket_id = new SqlParameter("@bucket_id", "0");
                var itemid = new SqlParameter("@item_id", item_id == null ? "-1" : item_id);
                var item_category_id = new SqlParameter("@item_category_id", "-1");
                var reason_code_id = new SqlParameter("@reason_code_id", "-1");
                var sloc_id1 = new SqlParameter("@sloc_id1", "-1");
                var bucket_id1 = new SqlParameter("@bucket_id1", "-1");
                var process1 = new SqlParameter("@process_id3", process_id3 == null ? "-1" : process_id3);
                var val = _scifferContext.Database.SqlQuery<report_inventory_vm>(
                    "exec report_inventory_report @entity,@from_date,@to_date,@plant_id,@sloc_id,@bucket_id,@item_id,@item_category_id,@reason_code_id,@sloc_id1,@bucket_id1,@process_id3",
                    ent, fromdate, to_date, plant_id, sloc_id, bucket_id, itemid, item_category_id, reason_code_id, sloc_id1, bucket_id1, process1).ToList();
                return val;
            }
            catch (Exception ex) { return null; }
        }
        public List<ref_mfg_process> GetOprnList()
        {
            var wqry = "select * from ref_mfg_process where is_active=1 and is_blocked=0";
            var list = _scifferContext.Database.SqlQuery<ref_mfg_process>(wqry).ToList(); ;
            return list;
        }
    }
}
