using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using Syncfusion.EJ.Export;
using Syncfusion.JavaScript.Models;
using Syncfusion.XlsIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Sciffer.Erp.Web.Controllers
{
    public class ToolLifeMasterController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly IToolRenewTypeService _ToolRenewType;
        private readonly IToolLifeService _ToolLife;

        public string entity { get; set; }
        public string tool_id { get; set; }
        public string tool_renew_type_id { get; set; }
        public string item_id { get; set; }
        public string machine_id { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }

        public ToolLifeMasterController(IToolLifeService toollife, IGenericService generic,IToolRenewTypeService toolrenewtype)
        {
            _Generic = generic;
            _ToolRenewType = toolrenewtype;
            _ToolLife = toollife;
        }
        // GET: ToolLifeMaster
        [CustomAuthorizeAttribute("TLMSTR")]
        public ActionResult Index()
        {
            ViewBag.Datasource = _ToolLife.GetAll();
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            ViewBag.tool_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.tool_type_list = new SelectList(_ToolRenewType.GetAll(), "tool_renew_type_id", "tool_renew_type_name");
            return View();
        }

        public ActionResult InlineDelete(int key)
        {
            var res = _ToolLife.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(ref_tool_life_VM value)
        {
            if (value.tool_life_id == 0)
            {
                var data1 = _ToolLife.Add(value);

                return Json(data1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data1 = _ToolLife.Update(value);
                return Json(data1, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ToolLifeReport()
        {
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            ViewBag.tool_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.tool_type_list = new SelectList(_ToolRenewType.GetAll(), "tool_renew_type_id", "tool_renew_type_name");
            return View();
        }

        public ActionResult Tool_Life_Report(string entity, string tool_id, string tool_renew_type_id, string item_id, string machine_id, DateTime? fromDate, DateTime? toDate)
        {
            ViewBag.datasource = _ToolLife.Tool_Life_Report(entity, tool_id, tool_renew_type_id, item_id, machine_id, fromDate, toDate);
            return PartialView("Partial_ToolLifeReport", ViewBag);
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

                if (ds.Key == "tool_id")
                {
                    if (ds.Value != "")
                    {
                        tool_id = ds.Value.ToString();
                    }

                    continue;
                }

                if (ds.Key == "tool_renew_type_id")
                {
                    if (ds.Value != "")
                    {
                        tool_renew_type_id = ds.Value.ToString();
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

                if (ds.Key == "machine_id")
                {
                    if (ds.Value != "")
                    {
                        machine_id = ds.Value.ToString();
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
            if (controller == "ToolLifeMaster")
            {
                entity = "get_tool_life_report";
            }

            datasource = _ToolLife.Tool_Life_Report(entity, tool_id, tool_renew_type_id, item_id, machine_id, fromDate, toDate);
            return datasource;
        }
    }
}