using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web.Mvc;
using Syncfusion.EJ.Export;
using Syncfusion.JavaScript.Models;
using Syncfusion.XlsIO;
using System.Web.Script.Serialization;
using System.Collections;
using System.Reflection;
using Syncfusion.JavaScript.DataSources;
using Syncfusion.JavaScript;
using Sciffer.MovieScheduling.Web.Service;

namespace Sciffer.Erp.Web.Controllers
{
    public class BankingReportController : Controller
    {
        // GET: Banking
        private readonly IReportService _reportService;
        public string entity { get; set; }
        public string bank_cash_account_id { get; set; }
        public DateTime? from_date { get; set; }
        public DateTime? to_date { get; set; }
        public int? in_out { get; set; }
        public int? cash_bank { get; set; }
        public BankingReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PaymentSummary()
        {
            return View();
        }
        public ActionResult ReceiptSummary()
        {
            return View();
        }
        public ActionResult PaymentDetail()
        {
            return View();
        }
        public ActionResult ReceiptDetail()
        {
            return View();
        }
        public ActionResult ContraEntriesDetail()
        {
            return View();
        }
        public ActionResult Get_Partial(string entity, string bank_cash_account_id, DateTime? from_date,
            DateTime? to_date, int? cash_bank, int? in_out, string partial_v)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            ViewBag.bank_cash_account_id = bank_cash_account_id;

            ViewBag.from_date = from_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(from_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.to_date = to_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(to_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.cash_bank = cash_bank;
            ViewBag.in_out = in_out;
            ViewBag.entity = entity;
           
            return PartialView(partial_v, ViewBag);
           
        }
        public ActionResult Get_Payment_Receipt_Report(DataManager dm, string entity, string bank_cash_account_id, DateTime? from_date,
           DateTime? to_date, int? cash_bank, int? in_out)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            var res = _reportService.Payment_Receipt_Report(entity, bank_cash_account_id == "" ? "-1" : bank_cash_account_id, from_date == null ? dte : from_date, to_date == null ? dte : to_date,
                cash_bank, in_out);
            ServerSideSearch sss = new ServerSideSearch();
            IEnumerable data = sss.ProcessDM(dm, res);
            return Json(new { result = data, count = res.Count }, JsonRequestBehavior.AllowGet);


        }

        public ActionResult Payment_Receipt_Report(string entity, string bank_cash_account_id, DateTime? from_date,
            DateTime? to_date, int? cash_bank, int in_out)
        {
            var data = _reportService.Payment_Receipt_Report(entity, bank_cash_account_id, from_date, to_date, cash_bank, in_out);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;//GetSelfDataSource();
            if (entity == "getpaymentsummary")
            {
                return PartialView("Partial_PaymentSummary", ViewBag);
            }
            else if (entity == "getpaymentdetail")
            {
                return PartialView("Partial_PaymentDetail", ViewBag);
            }
            else if (entity == "getreceiptdetail")
            {
                return PartialView("Partial_ReceiptDetail", ViewBag);
            }
            else if (entity == "getcontraentriesdetail")
            {
                return PartialView("Partial_ContraEntriesDetail", ViewBag);
            }
            else
            {
                return PartialView("Partial_PaymentSummary", ViewBag);
            }
        }
        public void ExportToExcel(string TreeGridModel, string ctrlname)
        {
            string nme = "";
            ExcelExport exp = new ExcelExport();
            TreeGridProperties obj = ConvertTreeGridObject(TreeGridModel);
            var DataSource = _reportService.Payment_Receipt_Report(entity, bank_cash_account_id, from_date, to_date, cash_bank, (int)in_out);
            if(in_out==1)
            {
                nme = "ReceiptSummary.xlsx";
            }
            else
            {
                nme = "PaymentSummary.xlsx";
            }
            exp.Export(obj, DataSource, nme, ExcelVersion.Excel2010, new TreeGridExportSettings() { Theme = ExportTheme.BootstrapTheme });
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
                if (ds.Key == "bank_cash_account_id")
                {
                    if (ds.Value != "")
                    {
                        bank_cash_account_id = ds.Value.ToString();
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
                if (ds.Key == "in_out")
                {
                    if (ds.Value != "")
                    {
                        in_out = int.Parse(ds.Value.ToString());
                    }
                    continue;
                }
                if (ds.Key == "cash_bank")
                {
                    if (ds.Value != "")
                    {
                        cash_bank = int.Parse(ds.Value.ToString());
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

        public void ExportToExcelNormal(string GridModel, string ctrlname)
        {
           
            GridProperties obj = ConvertGridObject(GridModel, ctrlname);
             var DataSource = _reportService.Payment_Receipt_Report(entity, bank_cash_account_id, from_date, to_date, cash_bank, in_out);
            ExcelExport exp = new ExcelExport();
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
                if (ds.Key == "bank_cash_account_id")
                {
                    if (ds.Value != "")
                    {
                        bank_cash_account_id = ds.Value.ToString();
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
                if (ds.Key == "in_out")
                {
                    if (ds.Value != "")
                    {
                        in_out = int.Parse(ds.Value.ToString());
                    }
                    continue;
                }
                if (ds.Key == "cash_bank")
                {
                    if (ds.Value != "")
                    {
                        cash_bank = int.Parse(ds.Value.ToString());
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