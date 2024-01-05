using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using System;
using Syncfusion.JavaScript.Models;
using Syncfusion.EJ.Export;
using System.Web.Script.Serialization;
using System.Collections;
using System.Collections.Generic;
using Syncfusion.XlsIO;
using System.Reflection;
using Newtonsoft.Json;
using Syncfusion.JavaScript;
using Sciffer.MovieScheduling.Web.Service;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using OfficeOpenXml;
using System.Linq;

namespace Sciffer.Erp.Web.Controllers
{
    public class ProductionReportController : Controller
    {

        private readonly IGenericService _Generic;
        private readonly IReportService _reportService;
        private readonly IProductionService _Production;
        private readonly IMachineEntryService _MachineEntry;
        public readonly IShiftService _shift;
        private readonly IItemCategoryService _ItemCategory;
        private readonly IOperatorOperationMappingService _mappingService;
        public DateTime from_date { get; set; }
        public DateTime to_date { get; set; }
        public string plant_id { get; set; }
        public string machine_id { get; set; }
        public string tag_id { get; set; }
        public string item_id { get; set; }
        public string tag_number { get; set; }
        public string prod_order_id { get; set; }
        public string item_status_id { get; set; }
        public string user_id { get; set; }
        public string process_id { get; set; }
        public string entity { get; set; }
        public string shift_id { get; set; }
        public string supervisor_id { get; set; }
        public string is_qc_remark { get; set; }
        public ProductionReportController(IGenericService generic, IReportService reportService, IProductionService production, IMachineEntryService machineentry, IShiftService Shift, IItemCategoryService itemCategory , IOperatorOperationMappingService MappingService)
        {
            _Generic = generic;
            _reportService = reportService;
            _Production = production;
            _MachineEntry = machineentry;
            _shift = Shift;
            _ItemCategory = itemCategory;
            _mappingService = MappingService;
        }
        public void ExportToExcel(string GridModel, string ctrlname)
        {
            ExcelExport exp = new ExcelExport();
            GridProperties obj = ConvertGridObject(GridModel, ctrlname);
            var DataSource = GetAllData(ctrlname);
            exp.Export(obj, DataSource, ctrlname + ".xlsx", ExcelVersion.Excel2010, false, false, "bootstrap-theme");
        }
        private GridProperties ConvertGridObject(string gridProperty, string ctrlname)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            IEnumerable div = (IEnumerable)serializer.Deserialize(gridProperty, typeof(IEnumerable));
            GridProperties gridProp = new GridProperties();
            foreach (KeyValuePair<string, object> ds in div)
            {
                var property = gridProp.GetType().GetProperty(ds.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);

                if (ds.Key == "from_date")
                {
                    if (ds.Value != "")
                    {
                        from_date = DateTime.Parse(ds.Value.ToString());
                    }

                    continue;
                }
                if (ds.Key == "to_date")
                {
                    if (ds.Value != "")
                    {
                        to_date = DateTime.Parse(ds.Value.ToString());
                    }

                    continue;
                }
                if (ds.Key == "plant_id")
                {
                    if (ds.Value != "")
                    {
                        plant_id = ds.Value.ToString();
                    }

                    continue;
                }
                if (ds.Key == "machine_id")
                {
                    if (ds.Value != "")
                    {
                        machine_id = ds.Value.ToString();
                    }

                    continue;
                }
                if (ds.Key == "tag_id")
                {
                    if (ds.Value != "")
                    {
                        tag_id = ds.Value.ToString();
                    }

                    continue;
                }
                if (ds.Key == "item_id")
                {
                    if (ds.Value != "")
                    {
                        item_id = ds.Value.ToString();
                    }

                    continue;
                }
                if (ds.Key == "tag_number")
                {
                    if (ds.Value != "")
                    {
                        tag_number = ds.Value.ToString();
                    }

                    continue;
                }
                if (ds.Key == "prod_order_id")
                {
                    if (ds.Value != "")
                    {
                        prod_order_id = ds.Value.ToString();
                    }

                    continue;
                }
                if (ds.Key == "item_status_id")
                {
                    if (ds.Value != "")
                    {
                        item_status_id = ds.Value.ToString();
                    }

                    continue;
                }
                if (ds.Key == "user_id")
                {
                    if (ds.Value != "")
                    {
                        user_id = ds.Value.ToString();
                    }

                    continue;
                }
                if (ds.Key == "process_id")
                {
                    if (ds.Value != "")
                    {
                        process_id = ds.Value.ToString();
                    }

                    continue;
                }
                if (ds.Key == "shift_id")
                {
                    if (ds.Value != "")
                    {
                        shift_id = ds.Value.ToString();
                    }

                    continue;
                }
                if (ds.Key == "is_qc_remark")
                {
                    if (ds.Value != "")
                    {
                        is_qc_remark = ds.Value.ToString();
                    }

                    continue;
                }
                if (property != null)
                {
                    Type type = property.PropertyType;
                    string serialize = serializer.Serialize(ds.Value);
                    object value = serializer.Deserialize(serialize, type);
                    property.SetValue(gridProp, value, null);
                }
            }
            return gridProp;
        }
        public ActionResult Get_Partial(string entity, DateTime? from_date, DateTime? to_date, string plant_id, string machine_id, string tag_id, string item_id, string tag_number, string prod_order_id, string item_status_id, string user_id, string process_id, string shift_id, string is_qc_remark)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            ViewBag.from_date = from_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(from_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.to_date = to_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(to_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.plant_id = plant_id;
            ViewBag.machine_id = machine_id;
            ViewBag.tag_id = tag_id;
            ViewBag.item_id = item_id;
            ViewBag.tag_number = tag_number;
            ViewBag.prod_order_id = prod_order_id;
            ViewBag.item_status_id = item_status_id;
            ViewBag.user_id = user_id;
            ViewBag.process_id = process_id;
            ViewBag.entity = entity;
            ViewBag.shift_id = shift_id;
            ViewBag.is_qc_remark = is_qc_remark;

            if (entity == "get_tag_history_report")
            {
                return PartialView("Partial_TagHistoryReport", ViewBag);
            }
            else if (entity == "get_User_wise_production_report")
            {
                return PartialView("Partial_Userwiseproductionreport", ViewBag);
            }
            else if (entity == "get_Produced_Qty_Report_With_Tag_Numbers")
            {
                return PartialView("Partial_ProducedQtyReportWithTagNumbers", ViewBag);
            }
            else if (entity == "get_wip_report_summary")
            {
                return PartialView("Partial_WIPReportSummary", ViewBag);
            }
            else if (entity == "get_WIP_Report_Ageing_Report_Summary")
            {
                return PartialView("Partial_WIPReportAgeingReportSummary", ViewBag);
            }
            else if (entity == "get_Daily_Production_Report_Shiftwise_and_Machine_Wise")
            {
                return PartialView("Partial_DailyProductionReport_ShiftwiseandMachineWise", ViewBag);
            }
            else if (entity == "get_Daily_Production_Report_Shiftwise_and_Item_Wise")
            {
                return PartialView("Partial_DailyProductionReport_ShiftwiseandItemWise", ViewBag);
            }
            else if (entity == "get_FinalFGOutputReport")
            {
                return PartialView("Partial_FinalFGOutputReport", ViewBag);
            }
            else if (entity == "get_QC_Paramaters_Status")
            {
                return PartialView("Partial_QCParamatersStatus", ViewBag);
            }
            else if (entity == "get_Quality_QC_Paramaters_Status")
            {
                return PartialView("Partial_QualityQCParamatersStatus", ViewBag);
            }
            else if (entity == "get_wip_report_detailed")
            {
                return PartialView("Partial_WIPReportDetailed", ViewBag);
            }
            else if (entity == "getReOrderLevel")
            {
                return PartialView("Partial_ROLReport", ViewBag);
            }
            else if (entity == "get_Production_report_Summary")
            {
                return PartialView("Partial_ProductionreportSummary", ViewBag);
            }
            else if (entity == "get_incentive_report")
            {
                return PartialView("Partial_IncentiveReport", ViewBag);
            }
            else if (entity == "get_tag_number_available_for_production_recipt")
            {
                return PartialView("Partial_TagNumberAvailableForProductionRecipt", ViewBag);
            }
            else if (entity == "get_tag_number_ready_for_sale")
            {
                return PartialView("Partial_TagNumberReadyForSale", ViewBag);
            }
            else if (entity == "get_breakdown_Order_partial")
            {
                return PartialView("PV_Breakdown_Order", ViewBag);
            }
            else if (entity == "get_machine_wise_tag_status")
            {
                return PartialView("Partial_MachineWiseTagStatus", ViewBag);
            }
            else if (entity == "Operation_Machine_wise_Skill_Matrix")
            {
                return PartialView("Partial_OperationMachinewiseSkillMatrix", ViewBag);
            }
            else if (entity == "Operator_wise_Skill_Matrix")
            {
                return PartialView("Partial_OperatorwiseSkillMatrix", ViewBag);
            }
            else
            {
                return PartialView("Partial_PaymentSummary", ViewBag);
            }

        }
        public ActionResult Get_report_production(DataManager dm, string entity, DateTime? from_date, DateTime? to_date, string plant_id, string machine_id, string tag_id, string item_id, string tag_number, string prod_order_id, string item_status_id, string user_id, string shift_id, string is_qc_remark, string process_id)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            var res = _reportService.report_production(entity, from_date == null ? dte : from_date, to_date == null ? dte : to_date, plant_id, machine_id, tag_id, item_id, tag_number, prod_order_id, item_status_id, user_id, process_id, shift_id, is_qc_remark);
            ServerSideSearch sss = new ServerSideSearch();
            IEnumerable data = sss.ProcessDM(dm, res);
            return Json(new { result = data, count = res.Count }, JsonRequestBehavior.AllowGet);
        }
        public object GetAllData(string controller)
        {
            object datasource = null;
            if (controller == "WIPReportSummary")
            {
                entity = "get_wip_report_summary";
            }
            if (controller == "WIPReportDetailed")
            {
                entity = "get_wip_report_detailed";
            }
            if (controller == "WIPReportAgeingReportSummary")
            {
                entity = "get_WIP_Report_Ageing_Report_Summary";
            }
            if (controller == "VariablePayReport")
            {
                entity = "get_VariablePayReport";
            }
            if (controller == "Userwiseproductionreport")
            {
                entity = "get_User_wise_production_report";
            }
            if (controller == "ProducedQtyReportWithTagNumbers")
            {
                entity = "get_Produced_Qty_Report_With_Tag_Numbers";
            }
            if (controller == "TagHistoryReport")
            {
                entity = "get_tag_history_report";
            }
            if (controller == "QCParamatersStatus")
            {
                entity = "get_QC_Paramaters_Status";
            }
            if (controller == "QualityQCParamatersStatus")
            {
                entity = "get_Quality_QC_Paramaters_Status";
            }
            if (controller == "ProductionreportSummary")
            {
                entity = "get_Production_report_Summary";
            }
            if (controller == "DailyProductionReport_ShiftwiseandMachineWise")
            {
                entity = "get_Daily_Production_Report_Shiftwise_and_Machine_Wise";
            }
            if (controller == "DailyProductionReport_ShiftwiseandItemWise")
            {
                entity = "get_Daily_Production_Report_Shiftwise_and_Item_Wise";
            }
            if (controller == "DailyProductionReport_ShiftwiseandItemWise")
            {
                entity = "get_Daily_Production_Report_Shiftwise_and_Item_Wise";
            }
            if (controller == "ProductionReportIncentive")
            {
                entity = "get_incentive_report";
            }
            if (controller == "FinalFGOutputReport")
            {
                entity = "get_FinalFGOutputReport";
            }
            if (controller == "TagNumberReadyForSale")
            {
                entity = "get_tag_number_ready_for_sale";
            }
            if (controller == "TagNumberAvailableForProductionRecipt")
            {
                entity = "get_tag_number_available_for_production_recipt";
            }
            if (controller == "BreakdownOrder")
            {
                entity = "get_breakdown_Order_partial";
            }
            if (controller == "MachineWiseTagStatus")
            {
                entity = "get_machine_wise_tag_status";
            }
            if (controller == "ProductionReport")
            {
                entity = "getReOrderLevel";
            }
            if (controller == "OperationMachinewiseSkillMatrix")
            {
                entity = "Operation_Machine_wise_Skill_Matrix";
            }
            if (controller == "OperatorwiseSkillMatrix")
            {
                entity = "Operator_wise_Skill_Matrix";
            }

            datasource = _reportService.report_production(entity, from_date, to_date,
            plant_id, machine_id, tag_id, item_id, tag_number, prod_order_id,
            item_status_id, user_id, process_id, shift_id, is_qc_remark);
            return datasource;
        }
        public ActionResult report_production(string entity, DateTime? from_date, DateTime? to_date, string plant_id, string machine_id, string tag_id, string item_id, string tag_number, string prod_order_id, string item_status_id, string user_id, string process_id, string shift_id, string is_qc_remark)
        {
            var data = _reportService.report_production(entity, from_date, to_date, plant_id, machine_id, tag_id, item_id, tag_number, prod_order_id, item_status_id, user_id, process_id, shift_id, is_qc_remark);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            if (entity == "get_wip_report_summary")
            {
                return PartialView("Partial_WIPReportSummary", data);
            }
            else if (entity == "get_WIP_Report_Ageing_Report_Summary")
            {
                return PartialView("Partial_WIPReportAgeingReportSummary", data);
            }
            else if (entity == "get_User_wise_production_report")
            {
                return PartialView("Partial_Userwiseproductionreport", data);
            }
            else if (entity == "get_Produced_Qty_Report_With_Tag_Numbers")
            {
                return PartialView("Partial_ProducedQtyReportWithTagNumbers", data);
            }
            else if (entity == "get_tag_history_report")
            {
                return PartialView("Partial_TagHistoryReport", data);
            }
            else if (entity == "get_Production_report_Summary")
            {
                return PartialView("Partial_ProductionreportSummary", data);
            }
            else if (entity == "get_QC_Paramaters_Status")
            {
                return PartialView("Partial_QCParamatersStatus", data);
            }
            else if (entity == "get_Quality_QC_Paramaters_Status")
            {
                return PartialView("Partial_QualityQCParamatersStatus", data);
            }
            else if (entity == "get_Daily_Production_Report_Shiftwise_and_Machine_Wise")
            {
                return PartialView("Partial_DailyProductionReport_ShiftwiseandMachineWise", data);
            }
            else if (entity == "get_Daily_Production_Report_Shiftwise_and_Item_Wise")
            {
                return PartialView("Partial_DailyProductionReport_ShiftwiseandItemWise", data);
            }
            else if (entity == "get_VariablePayReport")
            {
                return PartialView("Partial_VariablePayReport", data);
            }
            else if (entity == "get_FinalFGOutputReport")
            {
                return PartialView("Partial_FinalFGOutputReport", data);
            }
            else if (entity == "get_incentive_report")
            {
                return PartialView("Partial_IncentiveReport", data);
            }
            else if (entity == "get_machine_wise_tag_status")
            {
                return PartialView("Partial_MachineWiseTagStatus", ViewBag);
            }
            else
            {
                return PartialView("Partial_WIPReportDetailed", data);
            }
        }
        public ActionResult MachineWiseTagStatus()
        {
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.oprlist = new SelectList(_Generic.GetOperationList(), "process_id", "process_description");
            ViewBag.shift = new SelectList(_shift.GetShiftList(), "shift_id", "shift_desc");
            return View();
        }

        public void ExportMachineWiseTagStatus(string entity, DateTime? from_date, DateTime? to_date, string plant_id, string machine_id, string tag_id, string item_id, string tag_number, string prod_order_id, string item_status_id, string user_id, string process_id, string shift_id, string is_qc_remark)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            var data = _reportService.report_production(entity, from_date == null ? dte : from_date, to_date == null ? dte : to_date, plant_id, machine_id, tag_id, item_id, tag_number, prod_order_id, item_status_id, user_id, process_id, shift_id, is_qc_remark);
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Tag Number Status");

            workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            workSheet.Row(1).Style.Font.Bold = true;

            workSheet.Cells[1, 1].Value = "S.No";
            workSheet.Cells[1, 2].Value = "Tag Number";
            workSheet.Cells[1, 3].Value = "Item";
            workSheet.Cells[1, 4].Value = "Status";
            workSheet.Cells[1, 5].Value = "Operator";
            workSheet.Cells[1, 6].Value = "Date";
            workSheet.Cells[1, 7].Value = "Task Time";
            workSheet.Cells[1, 8].Value = "Machine Desc";

            int recordIndex = 2;
            foreach (var item in data)
            {
                workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();
                workSheet.Cells[recordIndex, 2].Value = item.tag_no;
                workSheet.Cells[recordIndex, 3].Value = item.in_item_desc;
                workSheet.Cells[recordIndex, 4].Value = item.status_name.ToString();
                workSheet.Cells[recordIndex, 5].Value = item.user_code;
                workSheet.Cells[recordIndex, 6].Value = item.DatePart;
                workSheet.Cells[recordIndex, 7].Value = item.task_time_report;
                workSheet.Cells[recordIndex, 8].Value = item.machine_desc;
                recordIndex++;
            }

            string excelFileName = "Tag Status -" + from_date.Value.ToShortDateString() + " - " + to_date.Value.ToShortDateString();

            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=" + excelFileName + ".xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
        public ActionResult WIPReportSummary()
        {

            ViewBag.oprlist = new SelectList(_Generic.GetOperationList(), "process_id", "process_description");
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View();
        }
        public ActionResult ROLReport()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_Generic.GetItemListForReOrder(), "ITEM_ID", "ITEM_NAME");
            ViewBag.item_category_list = new SelectList(_ItemCategory.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            return View();
        }
        public ActionResult WIPReportDetailed()
        {
            ViewBag.oprlist = new SelectList(_Generic.GetOperationList(), "process_id", "process_description");
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View();
        }
        public ActionResult WIPReportAgeingReportSummary()
        {
            ViewBag.oprlist = new SelectList(_Generic.GetOperationList(), "process_id", "process_description");
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View();
        }
        public ActionResult Userwiseproductionreport()
        {

            ViewBag.userlist = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.oprlist = new SelectList(_Generic.GetOperationList(), "process_id", "process_description");
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            ViewBag.shift_list = new SelectList(_Generic.GetShiftlist(), "shift_id", "shift_code");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View();
        }
        public ActionResult ProducedQtyReportWithTagNumbers()
        {

            ViewBag.userlist = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.oprlist = new SelectList(_Generic.GetOperationList(), "process_id", "process_description");
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View();
        }
        public ActionResult TagHistoryReport()
        {
            ViewBag.production_order_list = new SelectList(_Production.GetAll(), "prod_order_id", "prod_order_no");
            ViewBag.item_list = new SelectList(_Generic.GetRMAndFGItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.status_list = new SelectList(_MachineEntry.GetAllStatus(), "machine_task_status_id", "machine_task_status_name");
            return View();
        }
        public ActionResult ProductionreportSummary()
        {
            ViewBag.oprlist = new SelectList(_Generic.GetOperationList(), "process_id", "process_description");
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View();
        }
        public ActionResult VariablePayReport()
        {
            return View();
        }
        public ActionResult QCParamatersStatus()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            ViewBag.oprlist = new SelectList(_Generic.GetOperationList(), "process_id", "process_description");

            return View();
        }
        public ActionResult QualityQCParamatersStatus()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            return View();
        }
        public ActionResult DailyProductionReport_ShiftwiseandMachineWise()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            return View();
        }
        public ActionResult DailyProductionReport_ShiftwiseandItemWise()
        {
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View();
        }
        public ActionResult FinalFGOutputReport()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            return View();
        }
        public ActionResult TagNumberAvailableForProductionRecipt()
        {
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(2), "ITEM_ID", "ITEM_NAME");
            return View();
        }
        public ActionResult TagNumberReadyForSale()
        {
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(2), "ITEM_ID", "ITEM_NAME");
            return View();
        }

        public ActionResult Breakdown_Order()
        {
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            // ViewBag. = new SelectList(_Generic.GetJobWorkInItem(), "ITEM_ID", "ITEM_NAME");
            return View();
        }


        public ActionResult ExportData(string entity, DateTime from_date, DateTime to_date, string plant_id)
        {
            var query = _reportService.GetIncentiveReport(entity, from_date, to_date, plant_id);
            var grid = new GridView();
            grid.DataSource = query;
            grid.DataBind();
            Response.ClearContent();
            Response.ContentType = "application/vnd.openxmlformats-     officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=IncentiveReport.xls");
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("IncentiveReport", "ProductionReport");
        }

        public ActionResult IncentiveReport()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            return View();
        }

        public ActionResult GetIncentiveReport_Partial(string entity, DateTime from_date, DateTime to_date, string plant_id)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            ViewBag.from_date = from_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(from_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.to_date = to_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(to_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.plant_id = plant_id;
            ViewBag.entity = entity;
            return PartialView("Partial_IncentiveReport", ViewBag);
        }

        public ActionResult GetIncentiveReport(DataManager dm, string entity, DateTime from_date, DateTime to_date, string plant_id)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            var res = _reportService.GetIncentiveReport(entity, from_date, to_date, plant_id);
            ServerSideSearch sss = new ServerSideSearch();
            IEnumerable data = sss.ProcessDM(dm, res);
            return Json(new { result = data, count = res.Count }, JsonRequestBehavior.AllowGet);
        }


        public void ExportToExcelIncentive(string GridModel, string ctrlname)
        {
            ExcelExport exp = new ExcelExport();
            GridProperties obj = ConvertGridObject(GridModel, ctrlname);
            var DataSource = GetIncentiveData(ctrlname);
            exp.Export(obj, DataSource, ctrlname + ".xlsx", ExcelVersion.Excel2010, false, false, "bootstrap-theme");
        }

        public object GetIncentiveData(string ctrlname)
        {
            var entity = "get_incentive_report";
            object datasource = _reportService.GetIncentiveReport(entity, from_date, to_date, plant_id);
            return datasource;
        }

        public ActionResult DailyRejectionNCReport()
        {
            return View();
        }

        public ActionResult GetDailyNCRejection_Partial(string entity, DateTime from_date, DateTime to_date)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            ViewBag.from_date = from_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(from_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.to_date = to_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(to_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.entity = entity;
            return PartialView("Partial_DailyRejectionNCReport", ViewBag);
        }

        public ActionResult GetNCRejecetionReport(DataManager dm, string entity, DateTime from_date, DateTime to_date)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            var res = _reportService.GetNCRejecetionReport(entity, from_date, to_date);
            ServerSideSearch sss = new ServerSideSearch();
            IEnumerable data = sss.ProcessDM(dm, res);
            return Json(new { result = data, count = res.Count }, JsonRequestBehavior.AllowGet);
        }

        public void ExportToExcelNCRejection(string GridModel, string ctrlname)
        {
            ExcelExport exp = new ExcelExport();
            GridProperties obj = ConvertGridObject(GridModel, ctrlname);
            var DataSource = GetNCRejectionData(ctrlname);
            exp.Export(obj, DataSource, ctrlname + ".xlsx", ExcelVersion.Excel2010, false, false, "bootstrap-theme");
        }

        public object GetNCRejectionData(string ctrlname)
        {
            var entity = "get_daily_nc_rejection_report";
            object datasource = _reportService.GetNCRejecetionReport(entity, from_date, to_date);
            return datasource;
        }

        public ActionResult Index()
        {
            return View();
        }

        //OEE------------------------------------------------------------------------------------------------

        public ActionResult OverallEquipmentEffectivenessDetailReport()
        {
            ViewBag.shift_list = new SelectList(_Generic.GetShiftlist(), "shift_id", "shift_code");
            ViewBag.item_list = new SelectList(_Generic.GetItemList().Where(a => a.ITEM_CATEGORY_ID == 3), "ITEM_ID", "ITEM_NAME");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.operator_list = new SelectList(_Generic.Get_User_Production_Supervisor(), "user_id", "user_name");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            return View();
        }

        public void ExportToExcelOEE(string GridModel, string ctrlname)
        {
            ExcelExport exp = new ExcelExport();
            GridProperties obj = ConvertGridObject(GridModel, ctrlname);
            var DataSource = GetAllDataOEE(ctrlname);
            exp.Export(obj, DataSource, ctrlname + ".xlsx", ExcelVersion.Excel2010, false, false, "bootstrap-theme");
        }

        public object GetAllDataOEE(string controller)
        {
            object datasource = null;
            if (controller == "OverallEquipmentEffectivenessReport")
            {
                entity = "getoeedetailreport";
            }

            DateTime dte = new DateTime(1990, 1, 1);
            datasource = _reportService.OverallEquipmentEffectivenessReport(entity, from_date == null ? dte : from_date, to_date == null ? dte : to_date, shift_id == "" ? "-1" : shift_id, item_id == "" ? "-1" : item_id, machine_id == "" ? "-1" : machine_id, supervisor_id == "" ? "-1" : supervisor_id, plant_id == "" ? "-1" : plant_id);
            return datasource;
        }

        public ActionResult OverallEquipmentEffectivenessReport(DataManager dm, string entity, DateTime? from_date, DateTime? to_date, string shift_id, string item_id, string machine_id, string supervisor_id, string plant_id)
        {
            try
            {
                DateTime dte = new DateTime(1990, 1, 1);
                var res = _reportService.OverallEquipmentEffectivenessReport(entity, from_date == null ? dte : from_date, to_date == null ? dte : to_date, shift_id == "" ? "-1" : shift_id, item_id == "" ? "-1" : item_id, machine_id == "" ? "-1" : machine_id, supervisor_id == "" ? "-1" : supervisor_id, plant_id == "" ? "-1" : plant_id);
                ServerSideSearch sss = new ServerSideSearch();
                IEnumerable data = sss.ProcessDM(dm, res);
                return Json(new { result = data, count = res.Count }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
                return null;
            }

        }

        public ActionResult Get_PartialM(string entity, DateTime? from_date, DateTime? to_date, string shift_id, string item_id, string machine_id, string supervisor_id, string plant_id)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            ViewBag.from_date = from_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(from_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.to_date = to_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(to_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.shift_id = shift_id;
            ViewBag.item_id = item_id;
            ViewBag.machine_id = machine_id;
            ViewBag.supervisor_id = supervisor_id;
            ViewBag.plant_id = plant_id;
            ViewBag.entity = entity;


            return PartialView("Partial_OEEDetailReport", ViewBag);

        }

        public ActionResult OperationMachinewiseSkillMatrix()
        {

            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            ViewBag.operator_list = new SelectList(_Generic.GetOperatorList(), "user_id", "user_name");
            ViewBag.processlist = new SelectList(_mappingService.GetProcessList(), "process_id", "process_description");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            // ViewBag. = new SelectList(_Generic.GetJobWorkInItem(), "ITEM_ID", "ITEM_NAME");
            return View();
        }
        public ActionResult OperatorwiseSkillMatrix()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            ViewBag.operator_list = new SelectList(_Generic.GetOperatorList(), "user_id", "user_name");

            // ViewBag. = new SelectList(_Generic.GetJobWorkInItem(), "ITEM_ID", "ITEM_NAME");
            return View();
        }
    }
}