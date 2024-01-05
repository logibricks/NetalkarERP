using Sciffer.Erp.Service.Interface;
using Syncfusion.EJ.Export;
using Syncfusion.JavaScript.Models;
using Syncfusion.XlsIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Sciffer.Erp.Web.Controllers
{
    public class MachineEntryReportController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly IProductionService _Production;
        private readonly IMachineEntryService _MachineEntry;
        private readonly IMachineEntryForQCService _MachineEntryForQc;
        private readonly IItemService _ItemService;
        private readonly IReportService _Report;

        public string entity { get; set; }
        public string tag_number { get; set; }
        public string item_list { get; set; }
        public string prod_order_list { get; set; }
        public string item_status_id { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }


        public MachineEntryReportController(IGenericService Generic, IProductionService production, IMachineEntryService machineentry, IMachineEntryForQCService machineentryforqc,
            IItemService itemservice, IReportService report)
        {
            _Generic = Generic;
            _Production = production;
            _MachineEntry = machineentry;
            _MachineEntryForQc = machineentryforqc;
            _ItemService = itemservice;
            _Report = report;
        }
        // GET: MachineEntryReport
        public ActionResult Index()
        {
            ViewBag.production_order_list = new SelectList(_Production.GetAll(), "prod_order_id", "prod_order_no");
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            ViewBag.status_list = new SelectList(_MachineEntry.GetAllStatus(), "machine_task_status_id", "machine_task_status_name");
            return View();
        }

        public ActionResult Machine_Entry_Report(string entity, string tag_number, string item_list, string prod_order_list, string item_status_id, DateTime? fromDate, DateTime? toDate)
        {
            var data = _Report.Machine_Entry_Report(entity, tag_number, item_list, prod_order_list, item_status_id, fromDate, toDate);
            ViewBag.datasource = data;
            return PartialView("Partial_TagNumberReport", ViewBag);
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

                if (ds.Key == "tag_number")
                {
                    if (ds.Value != "")
                    {
                        tag_number = ds.Value.ToString();
                    }

                    continue;
                }

                if (ds.Key == "ITEM_ID")
                {
                    if (ds.Value != "")
                    {
                        item_list = ds.Value.ToString();
                    }

                    continue;
                }

                if (ds.Key == "prod_order_list")
                {
                    if (ds.Value != "")
                    {
                        prod_order_list = ds.Value.ToString();
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

                if (ds.Key == "fromDate")
                {
                    if (ds.Value != "")
                    {
                        fromDate = DateTime.Parse(ds.Value.ToString());
                    }

                    continue;
                }

                if (ds.Key == "toDate")
                {
                    if (ds.Value != "")
                    {
                        toDate = DateTime.Parse(ds.Value.ToString());
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

        public object GetAllData(string controller)
        {
            object datasource = null;
            if (controller == "MachineEntryReport")
            {
                entity = "get_tag_number_report";
            }

            datasource = _Report.Machine_Entry_Report(entity, tag_number, item_list, prod_order_list, item_status_id, fromDate, toDate);
            return datasource;
        }

        public ActionResult CreateNewTagNumber()
        {
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View();
        }

        public ActionResult GenerateNewTagNumber(string tag_number, string item_id, string machine_list)
        {
            DateTime dt = DateTime.Now.AddDays(1);
            var message = _MachineEntry.GenerateNewTagNumber(tag_number, item_id, machine_list);
            return Json(message);
        }


    }
}