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
    public class PlantMaintenanceReportController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly IReportService _reportService;
        private readonly IMaintenanceTypeService _mtypeService;
        public string entity { get; set; }
        public string plant_id { get; set; }
        public string machine_id { get; set; }
        public string maintenance_type_id { get; set; }
        public string frequency_id { get; set; }
        public string status_id { get; set; }
        public DateTime? from_date { get; set; }
        public DateTime? to_date { get; set; }
        public string item_id { get; set; }
        public string auto_manual { get; set; }
        public string notification_type { get; set; }
        public string employee_id { get; set; }
        public PlantMaintenanceReportController(IGenericService generic, IReportService reportService, IMaintenanceTypeService mtypeService)
        {
            _Generic = generic;
            _reportService = reportService;
            _mtypeService = mtypeService;
        }
       
        public ActionResult Get_Partial(string entity, string plant_id, string machine_id, string maintenance_type_id, string frequency_id,
         string status_id, DateTime? from_date, DateTime? to_date,string item_id,string auto_manual,string notification_type,string employee_id
            , string partial_v, string machine_category_id)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            ViewBag.plant_id = plant_id;
            ViewBag.machine_id = machine_id;
            ViewBag.maintenance_type_id = maintenance_type_id;
            ViewBag.frequency_id = frequency_id;
            ViewBag.status_id = status_id;
            ViewBag.from_date = from_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(from_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.to_date = to_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(to_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.item_id = item_id;
            ViewBag.auto_manual = auto_manual;
            ViewBag.notification_type = notification_type;
            ViewBag.employee_id = employee_id;
            ViewBag.entity = entity;
           return PartialView(partial_v, ViewBag);
        }
        public object GetAllData(string controller)
        {
            object datasource = null;
            datasource = _reportService.MaintenancePlanCycleandFrequencyReport(entity, plant_id, machine_id, maintenance_type_id, frequency_id, status_id, from_date, to_date, item_id, auto_manual, notification_type, employee_id);
            return datasource;
        }
        public ActionResult GetMaintenancePlanCycleandFrequencyReport(DataManager dm, string entity, string plant_id, string machine_id, string maintenance_type_id, string frequency_id, string status_id, DateTime? from_date, DateTime? to_date, string item_id, string auto_manual, string notification_type, string employee_id)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            var res = _reportService.MaintenancePlanCycleandFrequencyReport(entity, plant_id == "" ? "-1" : plant_id, machine_id == "" ? "-1" : machine_id, maintenance_type_id == "" ? "-1" : maintenance_type_id, frequency_id == "" ? "-1" : frequency_id, status_id == "" ? "-1" : status_id, from_date == null ? dte : from_date, to_date == null ? dte : to_date, item_id == "" ? "-1" : item_id, auto_manual == "" ? "-1" : auto_manual, notification_type == "" ? "-1" : notification_type, employee_id == "" ? "-1" : employee_id);
            ServerSideSearch sss = new ServerSideSearch();
            IEnumerable data = sss.ProcessDM(dm, res);
            return Json(new { result = data, count = res.Count() }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult MaintenancePlanCycleandFrequencyReport(string entity, string plant_id, string machine_id, string maintenance_type_id, string frequency_id, string status_id, DateTime? from_date, DateTime? to_date, string item_id, string auto_manual, string notification_type, string employee_id)
        {
            var data = _reportService.MaintenancePlanCycleandFrequencyReport(entity, plant_id, machine_id, maintenance_type_id, frequency_id, status_id, from_date, to_date, item_id, auto_manual, notification_type, employee_id);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;//GetSelfDataSource();

            if (entity == "getmaintenanceplancycleandfrequencyreport")
            {

                return PartialView("Partial_Get_Maintenance_Plan_Cycle_and_Frequencyreport", ViewBag);
            }
            else if (entity == "getmaintenanceplanschedulereport")
            {
                return PartialView("Partial_Maintenance_Plan_Schedule_Report", ViewBag);
            }
            else if (entity == "getmaintenanceplanparameterreport")
            {
                return PartialView("Partial_Maintenance_Plan_Parameter_Report", ViewBag);
            }
            else if (entity == "getmaintenanceplancomponentsreport")
            {
                return PartialView("Partial_Maintenance_Plan_Components_Report", ViewBag);
            }
            else if (entity == "getmaintenanceorderheaderlevelreport")
            {
                return PartialView("Partial_Maintenance_order_Header_Level_Report", ViewBag);
            }
            else if (entity == "getopenmaintenanceorderreport")
            {
                return PartialView("Partial_Open_Maintenance_Order_Report", ViewBag);
            }
            else if (entity == "getmaintenanceorderparameterreport")
            {
                return PartialView("Partial_Maintenance_order_Parameter_Report", ViewBag);
            }
            else if (entity == "get_malfunction_notification_report")
            {
                return PartialView("Partial_MalfunctionNotificationReport", ViewBag);
            }
            else if (entity == "get_malfunction_time_summary_report")
            {
                return PartialView("Partial_MalfunctionTimeSummaryReport", ViewBag);
            }
            else if (entity == "get_breakdown_time_summary_report")
            {
                return PartialView("Partial_BreakdownTimeSummaryReport", ViewBag);
            }
            else if (entity == "get_open_notification_report")
            {
                return PartialView("Partial_OPENNotificationReport", ViewBag);
            }
            else
            {
                return PartialView("Partial_Maintenance_order_Cost", ViewBag);
            }



        }
        public ActionResult Get_Maintenance_Plan_Cycle_and_Frequencyreport()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.mtypelist = new SelectList(_mtypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            return View();
        }
        public ActionResult Maintenance_Plan_Schedule_Report()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.mtypelist = new SelectList(_mtypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            return View();
        }
        public ActionResult Maintenance_Plan_Parameter_Report()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.mtypelist = new SelectList(_mtypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            return View();
        }
        public ActionResult Maintenance_Plan_Components_Report()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.mtypelist = new SelectList(_mtypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            return View();
        }
        public ActionResult Maintenance_order_Header_Level_Report()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.mtypelist = new SelectList(_mtypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            return View();
        }
        public ActionResult Open_Maintenance_Order_Report()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.mtypelist = new SelectList(_mtypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            return View();
        }
        public ActionResult Maintenance_order_Parameter_Report()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.mtypelist = new SelectList(_mtypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            return View();
        }
        public ActionResult Maintenance_order_Cost()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.mtypelist = new SelectList(_mtypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            return View();
        }
        public ActionResult MalfunctionNotificationReport()
        {
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            ViewBag.notificationtypelist = new SelectList(_Generic.GetNotificationType(), "NOTIFICATION_ID", "NOTIFICATION_TYPE");
            ViewBag.employeelist = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            return View();
        }
        public ActionResult MalfunctionTimeSummaryReport()
        {
            ViewBag.mcatlist = new SelectList(_Generic.GetMachineCategoryList(), "machine_category_id", "machine_category_code");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            ViewBag.notificationtypelist = new SelectList(_Generic.GetNotificationType(), "NOTIFICATION_ID", "NOTIFICATION_TYPE");
            return View();
        }
        public ActionResult BreakdownTimeSummaryReport()
        {
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            ViewBag.notificationtypelist = new SelectList(_Generic.GetNotificationType(), "NOTIFICATION_ID", "NOTIFICATION_TYPE");
            ViewBag.mcatlist = new SelectList(_Generic.GetMachineCategoryList(), "machine_category_id", "machine_category_code");
            return View();
        }
        public ActionResult OPENNotificationReport()
        {
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "plant_id", "PLANT_NAME");
            ViewBag.notificationtypelist = new SelectList(_Generic.GetNotificationType(), "NOTIFICATION_ID", "NOTIFICATION_TYPE");
            ViewBag.employeelist = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            return View();
        }
        // GET: PlantMaintenanceReport
        public ActionResult Index()
        {
            return View();
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
                if (ds.Key == "entity")
                {
                    if (ds.Value != "")
                    {
                        entity = ds.Value.ToString();
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
                if (ds.Key == "maintenance_type_id")
                {
                    if (ds.Value != "")
                    {
                        maintenance_type_id = ds.Value.ToString();
                    }
                    continue;
                }
                if (ds.Key == "frequency_id")
                {
                    if (ds.Value != "")
                    {
                        frequency_id = ds.Value.ToString();
                    }
                    continue;
                }
                if (ds.Key == "status_id")
                {
                    if (ds.Value != "")
                    {
                        status_id = ds.Value.ToString();
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
                if (ds.Key == "item_id")
                {
                    if (ds.Value != "")
                    {
                        item_id = ds.Value.ToString();
                    }
                    continue;
                }
                if (ds.Key == "auto_manual")
                {
                    if (ds.Value != "")
                    {
                        auto_manual = ds.Value.ToString();
                    }
                    continue;
                }
                if (ds.Key == "notification_type")
                {
                    if (ds.Value != "")
                    {
                        notification_type = ds.Value.ToString();
                    }
                    continue;
                }
                if (ds.Key == "employee_id")
                {
                    if (ds.Value != "")
                    {
                        employee_id = ds.Value.ToString();
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