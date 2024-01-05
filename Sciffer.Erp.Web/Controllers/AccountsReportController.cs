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
using Syncfusion.JavaScript.DataSources;
using System.Linq;
using Sciffer.MovieScheduling.Web.Service;

namespace Sciffer.Erp.Web.Controllers
{
    public class AccountsReportController : Controller
    {
        // GET: AccountsReport
        private readonly IReportService _reportService;
        private readonly IGenericService _Generic;
        private readonly IVendorCategoryService _vendorCategoryService;
        private readonly ICurrencyService _currencyService;
        private readonly IPlantService _plantService;
        private readonly IBusinessUnitService _businessUnitService;
        private readonly ICustomerCategoryService _customerCategoryService;
        private readonly IFinancialTemplateService _Fanencial;
        public DateTime from_date { get; set; }
        public DateTime to_date { get; set; }
        public string entity { get; set; }
        public string entity_name { get; set; }
        public string customer_category_id { get; set; }
        public string priority_id { get; set; }
        public string currency_id { get; set; }
        public string plant_id { get; set; }
        public string business_unit_id { get; set; }
        public string document_based_on { get; set; }
        public string days_per_interval { get; set; }
        public string no_of_interval { get; set; }
        public string entity_id { get; set; }
        public string tds_code_id { get; set; }
        public string show_value_by { get; set; }
        public string entity_type_id { get; set; }
        public AccountsReportController(IReportService reportService, IGenericService Generic, IVendorCategoryService vendorCategoryService, IFinancialTemplateService Fanencial,
            ICurrencyService currencyService, IPlantService plantService, IBusinessUnitService businessUnitService, ICustomerCategoryService customerCategoryService, IStatusService status)
        {
            _reportService = reportService;
            _Generic = Generic;
            _vendorCategoryService = vendorCategoryService;
            _currencyService = currencyService;
            _plantService = plantService;
            _businessUnitService = businessUnitService;
            _customerCategoryService = customerCategoryService;
            _Fanencial = Fanencial;
        }
        public ActionResult ARAgeingReport()
        {
            return View();
        }
        public ActionResult APAgeingReport()
        {
            return View();
        }
        public ActionResult CustomerLedgerReport()
        {
            ViewBag.customer_list = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.ctrlname = "CustomerLedger";
            ViewBag.entity_code = "Customer Code";
            ViewBag.entity_name = "Customer Name";
            return View();
        }
        public ActionResult TCSReport()
        {
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            return View();
        }
        public ActionResult VendorLedgerReport()
        {
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.ctrlname = "VendorLedger";
            ViewBag.entity_code = "Vendor Code";
            ViewBag.entity_name = "Vendor Name";
            return View();
        }
        public ActionResult TemplateGlMapping()
        {
            ViewBag.TemplateList = new SelectList(_Fanencial.getall().Where(x => x.is_blocked == false), "template_id", "template_name");
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Balancesheet()
        {
            ViewBag.TemplateList = new SelectList(_Fanencial.getall().Where(x => x.is_blocked == false), "template_id", "template_name");
            return View();
        }
        public ActionResult EmployeeLedgerReport()
        {
            ViewBag.employee_list = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.ctrlname = "EmployeeLedger";
            ViewBag.entity_code = "Employee Code";
            ViewBag.entity_name = "Employee Name";
            return View();
        }
        public ActionResult SalesContributionReport()
        {
            ViewBag.vendor_category_list = new SelectList(_vendorCategoryService.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_ID");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.customer_category_list = new SelectList(_customerCategoryService.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_ID");
            return View();
        }
        public ActionResult trialbalacereport()
        {
            return View();
        }
      
        public ActionResult TDSPayableReport()
        {
            ViewBag.tds_list = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            return View();
        }
        public ActionResult TDSReceivableReport()
        {
            ViewBag.tds_list = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            return View();
        }
        public ActionResult CustomerTrialBalanceReport()
        {
            return View();
        }
        public ActionResult VendorTrialBalanceReport()
        {
            return View();
        }
        public ActionResult EmployeeTrialBalanceReport()
        {
            return View();
        }
        public ActionResult GeneralLedgerDetailsReport()
        {
            ViewBag.gl_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            return View();
        }


        public ActionResult TrialBalanceReport()
        {

            return View();
        }
        public ActionResult PNLReport()
        {
            ViewBag.TemplateList = new SelectList(_Fanencial.getall().Where(x => x.is_blocked == false), "template_id", "template_name");
            return View();
        }
        public void ExportTreeGridModel(string TreeGridModel, string ctrlname)
        {
            string nme = "";
            ExcelExport exp1 = new ExcelExport();
            TreeGridProperties obj = ConvertTreeGridObject(TreeGridModel);
            var DataSource = _reportService.GetAccountsReport(entity, from_date, to_date, customer_category_id, priority_id,
             currency_id, plant_id, business_unit_id, document_based_on, days_per_interval, no_of_interval, entity_id,
             tds_code_id, show_value_by, entity_type_id);
            nme = ctrlname + ".xlsx";
            exp1.Export(obj, DataSource, nme, ExcelVersion.Excel2010, new TreeGridExportSettings() { Theme = ExportTheme.BootstrapTheme });

        }
        private TreeGridProperties ConvertTreeGridObject(string gridProperty)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            IEnumerable div = (IEnumerable)serializer.Deserialize(gridProperty, typeof(IEnumerable));
            TreeGridProperties gridProp = new TreeGridProperties();
            foreach (KeyValuePair<string, object> ds in div)
            {
                var property = gridProp.GetType().GetProperty(ds.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (ds.Key == "entity")
                {
                    if (ds.Value != "")
                    {
                        entity = ds.Value.ToString();
                    }
                    continue;
                }
                if (ds.Key == "customer_category_id")
                {
                    if (ds.Value != "")
                    {
                        customer_category_id = ds.Value.ToString();
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
                    if (ds.Value != null)
                    {
                        to_date = DateTime.Parse(ds.Value.ToString());
                    }
                    continue;
                }
                if (ds.Key == "business_unit_id")
                {
                    if (ds.Value != "")
                    {
                        business_unit_id = ds.Value.ToString();
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
                if (ds.Key == "priority_id")
                {
                    if (ds.Value != "")
                    {
                        priority_id = ds.Value.ToString();
                    }
                    continue;
                }
                if (ds.Key == "currency_id")
                {
                    if (ds.Value != "")
                    {
                        currency_id = ds.Value.ToString();
                    }
                    continue;
                }
                if (ds.Key == "document_based_on")
                {
                    if (ds.Value != "")
                    {
                        document_based_on = ds.Value.ToString();
                    }
                    continue;
                }
                if (ds.Key == "days_per_interval")
                {
                    if (ds.Value != "")
                    {
                        days_per_interval = ds.Value.ToString();
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
                range.Value.Replace("----------", "     ");
            }
        }
        public void ExportToExcel(string GridModel, string ctrlname)
        {
            ExcelExport exp = new ExcelExport();
            ExcelEngine excel = new ExcelEngine();
            IApplication application = excel.Excel;
            IWorkbook workbook = application.Workbooks.Create(2);
            GridProperties obj = ConvertGridObject(GridModel, ctrlname);
            var DataSource = _reportService.GetAccountsReport(entity, from_date, to_date, customer_category_id, priority_id,
              currency_id, plant_id, business_unit_id, document_based_on, days_per_interval, no_of_interval, entity_id,
              tds_code_id, show_value_by, entity_type_id);
            if (entity == "gettdspayable")
            {
                ctrlname = "TdsPayableReport";
            }
            if (entity == "gettdsreceivable")
            {
                ctrlname = "TdsReceivableReport";
            }
            if (entity == "getapageingreport")
            {
                obj.ServerExcelQueryCellInfo = queryCellInfo;
            }
            if (entity == "getarageingreport")
            {
                obj.ServerExcelQueryCellInfo = queryCellInfo;
            }
            if (entity == "getpnlbalancesheet")
            {
                obj.ServerExcelQueryCellInfo = queryCellInfo;
            }
            workbook = _Generic.GetExcelWorkBook(obj, DataSource, ctrlname);
            workbook.SaveAs(ctrlname + ".xlsx", HttpContext.ApplicationInstance.Response, ExcelDownloadType.PromptDialog, ExcelHttpContentType.Excel2013);
        }
        //public void ExportToExcel(string GridModel, string ctrlname)
        //{
        //    ExcelExport exp = new ExcelExport();
        //    ExcelEngine excel = new ExcelEngine();
        //    IApplication application = excel.Excel;
        //    GridProperties obj = ConvertGridObject(GridModel, ctrlname);
        //    IWorkbook workbook = application.Workbooks.Create(2);
        //    var DataSource = _reportService.GetAccountsReport(entity, from_date, to_date, customer_category_id, priority_id,
        //     currency_id, plant_id, business_unit_id, document_based_on, days_per_interval, no_of_interval, entity_id,
        //     tds_code_id, show_value_by, entity_type_id);
        //    if (entity == "gettdspayable")
        //    {
        //        ctrlname = "TdsPayableReport";
        //    }
        //    if (entity == "gettdsreceivable")
        //    {
        //        ctrlname = "TdsReceivableReport";
        //    }
        //    if (entity == "getapageingreport")
        //    {
        //        obj.ServerExcelQueryCellInfo = queryCellInfo;
        //    }
        //    if (entity == "getarageingreport")
        //    {
        //        obj.ServerExcelQueryCellInfo = queryCellInfo;
        //    }
        //  //  exp.Export(obj, DataSource, ctrlname + ".xlsx", ExcelVersion.Excel2010, false, false, "bootstrap-theme");           
        //    workbook = _Generic.GetExcelWorkBook(obj, DataSource, ctrlname);
        //    workbook.SaveAs(ctrlname + ".xlsx", HttpContext.ApplicationInstance.Response, ExcelDownloadType.PromptDialog, ExcelHttpContentType.Excel2013);
        //}
        private GridProperties ConvertGridObject(string gridProperty, string ctrlname)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            IEnumerable div = (IEnumerable)serializer.Deserialize(gridProperty, typeof(IEnumerable));
            GridProperties gridProp = new GridProperties();
            foreach (KeyValuePair<string, object> ds in div)
            {
                var property = gridProp.GetType().GetProperty(ds.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);

                if (ds.Key == "entity")
                {
                    if (ds.Value != "")
                    {
                        entity = ds.Value.ToString();
                    }

                    continue;
                }
                if (ds.Key == "entity_name")
                {
                    if (ds.Value != "")
                    {
                        entity_name = ds.Value.ToString();
                    }

                    continue;
                }
                if (ds.Key == "priority_id")
                {
                    if (ds.Value != "")
                    {
                        priority_id = ds.Value.ToString();
                    }

                    continue;
                }
                if (ds.Key == "currency_id")
                {
                    if (ds.Value != "")
                    {
                        currency_id = ds.Value.ToString();
                    }
                    continue;
                }
                if (ds.Key == "entity_type_id")
                {
                    if (ds.Value != "")
                    {
                        entity_type_id = ds.Value.ToString();
                    }

                    continue;
                }

                if (ds.Key == "priority_id")
                {
                    if (ds.Value != "")
                    {
                        priority_id = ds.Value.ToString();
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
                if (ds.Key == "business_unit_id")
                {
                    if (ds.Value != "")
                    {
                        business_unit_id = ds.Value.ToString();
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
                if (ds.Key == "customer_category_id")
                {
                    if (ds.Value != "")
                    {
                        customer_category_id = ds.Value.ToString();
                    }
                    continue;
                }
                if (ds.Key == "entity_id")
                {
                    if (ds.Value != "")
                    {
                        entity_id = ds.Value.ToString();
                    }
                    continue;
                }
                if (ds.Key == "tds_code_id")
                {
                    if (ds.Value != "")
                    {
                        tds_code_id = ds.Value.ToString();
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

        public ActionResult Get_Partial(string entity, DateTime? from_date, DateTime? to_date, string customer_category_id, string priority_id,
          string currency_id, string plant_id, string business_unit_id, string document_based_on, string days_per_interval, 
          string no_of_interval, string entity_id,
          string tds_code_id, string show_value_by, string entity_type_id,string partial_v)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            ViewBag.tds_code_id = tds_code_id;
            ViewBag.from_date = from_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(from_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.to_date = to_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(to_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.plant_id = plant_id;
            ViewBag.customer_category_id = customer_category_id;
            ViewBag.priority_id = priority_id;
            ViewBag.currency_id = currency_id;
            ViewBag.business_unit_id = business_unit_id;
            ViewBag.document_based_on = document_based_on;
            ViewBag.business_unit_id = business_unit_id;
            ViewBag.days_per_interval = days_per_interval;
            ViewBag.no_of_interval = no_of_interval;
            ViewBag.customer_id = currency_id;
            ViewBag.entity_type_id = entity_type_id;
            ViewBag.entity_id = entity_id;
            ViewBag.entity = entity;
         
            if (entity == "getapageingreport")
            {
                ViewBag.ctrlname = "APAgeingReport";
                ViewBag.entity_code = "Vendor Code";
                ViewBag.entity_name = "Vendor Name";
                return PartialView("Partial_APARSummary", ViewBag);
            }
            else if (entity == "tcsreport")
            {
                ViewBag.ctrlname = "TCS Report";
                return PartialView("Partial_TCSReport", ViewBag);
            }
            else if (entity == "getarageingreport")
            {
                ViewBag.ctrlname = "ARAgeingReport";
                ViewBag.entity_code = "Customer Code";
                ViewBag.entity_name = "Customer Name";
                return PartialView("Partial_APARSummary", ViewBag);
            }
          
            else if (entity == "getallledger")
            {
                if (entity_type_id == "1")
                {
                    ViewBag.ctrlname = "CustomerLedger";
                    ViewBag.entity_code = "Customer Code";
                    ViewBag.entity_name = "Customer Name";
                }
                else if (entity_type_id == "2")
                {
                    ViewBag.ctrlname = "VendorLedger";
                    ViewBag.entity_code = "Vendor Code";
                    ViewBag.entity_name = "Vendor Name";
                }
                else
                {
                    ViewBag.ctrlname = "EmployeeLedger";
                    ViewBag.entity_code = "Employee Code";
                    ViewBag.entity_name = "Employee Name";
                }
               
            }
           
            else
            {
                return PartialView(partial_v, ViewBag);
            }
            return PartialView(partial_v, ViewBag);
        }
        public JsonResult GetAccountsData(DataManager dm, string entity, DateTime? from_date, DateTime? to_date, string customer_category_id, string priority_id,
          string currency_id, string plant_id, string business_unit_id, string document_based_on, string days_per_interval, string no_of_interval, string entity_id,
          string tds_code_id, string show_value_by, string entity_type_id)
        {

            var res = _reportService.GetAccountsReport(entity, from_date, to_date, customer_category_id, priority_id,
             currency_id, plant_id, business_unit_id, document_based_on, days_per_interval, no_of_interval, entity_id,
             tds_code_id == "" ? "-1" : tds_code_id, show_value_by, entity_type_id);
            ServerSideSearch sss = new ServerSideSearch();
            IEnumerable data = sss.ProcessDM(dm, res);
            return Json(new { result = data, count = res.Count() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAccountsReport(string entity, DateTime? from_date, DateTime? to_date, string customer_category_id, string priority_id,
          string currency_id, string plant_id, string business_unit_id, string document_based_on, string days_per_interval, string no_of_interval, string entity_id,
          string tds_code_id, string show_value_by, string entity_type_id)
        {
            var data = _reportService.GetAccountsReport(entity, from_date, to_date, customer_category_id, priority_id,
             currency_id, plant_id, business_unit_id, document_based_on, days_per_interval, no_of_interval, entity_id,
             tds_code_id, show_value_by, entity_type_id);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            if (entity == "getarageingreport")
            {
                ViewBag.ctrlname = "ARAgeingReport";
                ViewBag.entity_code = "Customer Code";
                ViewBag.entity_name = "Customer Name";
                return PartialView("Partial_ARAPAgeingreport", data);// return PartialView("Partial_PurchaseOrderItemLevel", data);
            }
            else if(entity== "tcsreport")
            {
                return PartialView("Partial_TCSReport", data);
            }
            else if (entity == "getapageingreport")
            {
                ViewBag.ctrlname = "APAgeingReport";
                ViewBag.entity_code = "Vendor Code";
                ViewBag.entity_name = "Vendor Name";
                return PartialView("Partial_ARAPAgeingreport", data);// return PartialView("Partial_PurchaseOrderItemLevel", data);
            }
            else if (entity == "getallledger")
            {
                if (entity_type_id == "1")
                {
                    ViewBag.ctrlname = "CustomerLedger";
                    ViewBag.entity_code = "Customer Code";
                    ViewBag.entity_name = "Customer Name";
                }
                else if (entity_type_id == "2")
                {
                    ViewBag.ctrlname = "VendorLedger";
                    ViewBag.entity_code = "Vendor Code";
                    ViewBag.entity_name = "Vendor Name";
                }
                else
                {
                    ViewBag.ctrlname = "EmployeeLedger";
                    ViewBag.entity_code = "Employee Code";
                    ViewBag.entity_name = "Employee Name";
                }
                return PartialView("Partial_CustomerLedgerReport", data);// return PartialView("Partial_PurchaseOrderItemLevel", data);
            }
            else if (entity == "getpnl")
            {
                return PartialView("Partial_PNL_Report", data);
            }
            else if (entity == "getbalancesheet")
            {
                return PartialView("Partial_Balance_Sheet", data);
            }
            else
            {
                return PartialView("Partial_ARAPAgeingreport", data);// return PartialView("Partial_PurchaseOrderItemLevel", data);
            }
        }
    }
}





