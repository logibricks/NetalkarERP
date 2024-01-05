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


namespace Sciffer.Erp.Web.Controllers
{
    public class GSTRReportController : Controller
    {

        private readonly IPlantService _plant;
        private readonly IReportService _reportService;
        private readonly IGenericService _Generic;
        // GET: GSTRReport
        public DateTime from_date { get; set; }
        public DateTime to_date { get; set; }
        public string plant_id { get; set; }
        public string entity { get; set; }
        public GSTRReportController(IPlantService plant, IReportService reportService, IGenericService generic)
        {
            _plant = plant;
            _reportService = reportService;
            _Generic = generic;
        }
        public ActionResult GSTR1Report()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.ctrlname = "GSTR1ReportSummary";
            return View();
        }
        public ActionResult GSTR1RCMReport()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.ctrlname = "GSTR1RCMReport";
            return View();
        }
        public ActionResult GSTR1ReportDetail()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.ctrlname = "GSTR1ReportDetails";
            return View();
        }
        public ActionResult GSTR2Report()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.ctrlname = "GSTR2ReportSummary";
            return View();
        }
        public ActionResult GSTR3BReport()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.ctrlname = "GSTR3BReportDetails";
            return View();
        }
        public ActionResult GSTR_Report(string entity, DateTime from_date, DateTime to_date, string plant_id,string partial_v)
        {
            var data = _reportService.GETGSTRReport(entity, from_date, to_date, plant_id);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            if (entity == "getgstr1report")
            {
                ViewBag.ctrlname = "GSTR1ReportSummary";
            }
            else if (entity == "getgstr1reportdetail")
            {
                ViewBag.ctrlname = "GSTR1ReportDetails";

            }
            else if (entity == "getgstrrcm")
            {
                ViewBag.ctrlname = "GSTR1RCM";

            }

            else if (entity == "getgstr2report")
            {
                ViewBag.ctrlname = "GSTR2ReportSummary";

            }
            else
            {
                ViewBag.ctrlname = "GSTR3BReport";

            }
            return PartialView(partial_v, data);// return PartialView("Partial_PurchaseOrderItemLevel", data);

        }
        public ActionResult Index()
        {
            return View();
        }
        public void queryCellInfo(object currentCell)
        {
            IRange range = (IRange)currentCell;
            if (range.Column == 3)
            {// here 3 is the index of Freight columns 
                if (range.Value.ToString() == "")
                {
                    range.EntireRow.CellStyle.Font.Color = ExcelKnownColors.Dark_blue;

                    range.EntireRow.CellStyle.Font.Bold = true;
                }

            }
            if (range.Column == 1)
            {
                if (range.Value.Contains("Total"))
                {
                    range.EntireRow.CellStyle.Font.Color = ExcelKnownColors.Black;
                    range.EntireRow.CellStyle.Font.Bold = true;
                }
            }
        }
        public void ExportToExcel(string GridModel, string ctrlname)
        {
            ExcelExport exp = new ExcelExport();
            GridProperties obj = ConvertGridObject(GridModel, ctrlname);
            ExcelEngine excel = new ExcelEngine();
            IApplication application = excel.Excel;
            var DataSource = _reportService.GETGSTRReport(entity, from_date, to_date, plant_id);
            IWorkbook workbook = application.Workbooks.Create(2);
            if (ctrlname == "GSTR3BReport")
            {
                obj.ServerExcelQueryCellInfo = queryCellInfo;
            }
            workbook = _Generic.GetExcelWorkBook(obj, DataSource, ctrlname);
            workbook.SaveAs(ctrlname + ".xlsx", HttpContext.ApplicationInstance.Response, ExcelDownloadType.PromptDialog, ExcelHttpContentType.Excel2013);
        }
        private GridProperties ConvertGridObject(string gridProperty, string ctrlname)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            IEnumerable div = (IEnumerable)serializer.Deserialize(gridProperty, typeof(IEnumerable));
            GridProperties gridProp = new GridProperties();
            foreach (KeyValuePair<string, object> ds in div)
            {
                var property = gridProp.GetType().GetProperty(ds.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);


                if (ds.Key == "plant_id")
                {
                    if (ds.Value != "")
                    {
                        plant_id = ds.Value.ToString();
                    }
                    continue;
                }

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
                if (ds.Key == "entity")
                {
                    if (ds.Value != "")
                    {
                        entity = ds.Value.ToString();
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
    }
}