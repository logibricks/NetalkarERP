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
    public class PurchaseReportController : Controller
    {
        private readonly IItemCategoryService _itemCategoryService;
        private readonly IReportService _reportService;
        private readonly IGenericService _Generic;
        private readonly IStatusService _status;
        private readonly IVendorCategoryService _vendorcategory;
        private readonly IPlantService _plant;
        private readonly ISourceService _source;
        private readonly IItemGroupService _itemgroup;
        private readonly ITerritoryService _territory;
        public DateTime posting_date { get; set; }
        public DateTime from_date { get; set; }
        public DateTime to_date { get; set; }
        public string item_category { get; set; }
        public string priority_id { get; set; }
        public string currency_id { get; set; }
        public string plant_id { get; set; }
        public string business_unit_id { get; set; }
        public string vendor_category_id { get; set; }
        public string vendor_id { get; set; }
        public string item_service_id { get; set; }
        public string vendor_priority_id { get; set; }
        public string item_group_id { get; set; }
        public string item_priority_id { get; set; }
        public string territory_id { get; set; }
        public string status_id { get; set; }
        public string created_by { get; set; }
        public string entity { get; set; }
        public string source_id { get; set; }
        public string item_id { get; set; }
        public PurchaseReportController(IItemCategoryService ItemCategoryService, IReportService reportService, IGenericService Generic, IStatusService status, IPlantService plant, IVendorCategoryService vendorcategory,
            ISourceService source, IItemGroupService itemgroup, ITerritoryService territory)
        {
            _itemCategoryService = ItemCategoryService;
            _reportService = reportService;
            _Generic = Generic;
            _status = status;
            _plant = plant;
            _vendorcategory = vendorcategory;
            _source = source;
            _itemgroup = itemgroup;
            _territory = territory;

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
                if (ds.Key == "posting_date")
                {
                    if (ds.Value != "")
                    {
                        posting_date = DateTime.Parse(ds.Value.ToString());
                    }

                    continue;
                }
                if (ds.Key == "item_category")
                {
                    if (ds.Value != "")
                    {
                        item_category = ds.Value.ToString();
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
                if (ds.Key == "source_id")
                {
                    if (ds.Value != "")
                    {
                        source_id = ds.Value.ToString();
                    }

                    continue;
                }
                if (ds.Key == "vendor_priority_id")
                {
                    if (ds.Value != "")
                    {
                        vendor_priority_id = ds.Value.ToString();
                    }
                    continue;
                }
                if (ds.Key == "item_priority_id")
                {
                    if (ds.Value != "")
                    {
                        item_priority_id = ds.Value.ToString();
                    }
                    continue;
                }

                if (ds.Key == "item_group_id")
                {
                    if (ds.Value != "")
                    {
                        item_group_id = ds.Value.ToString();
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
                if (ds.Key == "vendor_category_id")
                {
                    if (ds.Value != "")
                    {
                        vendor_category_id = ds.Value.ToString();
                    }
                    continue;
                }
                if (ds.Key == "vendor_id")
                {
                    if (ds.Value != "")
                    {
                        vendor_id = ds.Value.ToString();
                    }
                    continue;
                }
                if (ds.Key == "item_service_id")
                {
                    if (ds.Value != "")
                    {
                        item_service_id = ds.Value.ToString();
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
        public ActionResult Get_Partial(string entity, string vendor_category_id, string vendor_priority_id,
        string item_priority_id, string vendor_id, DateTime? from_date, DateTime? to_date, string item_group_id, string territory_id,
        string item_category, string status_id, string created_by, string plant_id, string source_id, DateTime? posting_date,
        string currency_id, string business_unit_id, string item_service_id,string item_id, string partial_v)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            ViewBag.vendor_category_id = vendor_category_id;
            ViewBag.vendor_priority_id = vendor_priority_id;
            ViewBag.item_priority_id = item_priority_id;
            ViewBag.vendor_id = vendor_id;
            ViewBag.from_date = from_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(from_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.to_date = to_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(to_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.territory_id = territory_id;
            ViewBag.item_category = item_category;
            ViewBag.status_id = status_id;
            ViewBag.created_by = created_by;
            ViewBag.plant_id = plant_id;
            ViewBag.source_id = source_id;
            ViewBag.posting_date = posting_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(posting_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.currency_id = currency_id;
            ViewBag.business_unit_id = business_unit_id;
            ViewBag.item_service_id = item_service_id;
            ViewBag.item_id = item_id;
            ViewBag.entity = entity;
                return PartialView(partial_v, ViewBag);
        }
        public object GetAllData(string controller)
        {
            object datasource = null;
            datasource = _reportService.Purchase_Order_Report(entity, vendor_category_id, vendor_priority_id,
            item_priority_id, vendor_id, from_date, to_date, item_group_id, territory_id,
            item_category, status_id, created_by, plant_id, source_id, posting_date,
            currency_id, business_unit_id, item_service_id, item_id);
            return datasource;
        }
        public ActionResult Get_Purchase_Order_Report(DataManager dm, string entity, string vendor_category_id, string vendor_priority_id,
         string item_priority_id, string vendor_id, DateTime? from_date, DateTime? to_date, string item_group_id, string territory_id,
         string item_category, string status_id, string created_by, string plant_id, string source_id, DateTime? posting_date,
         string currency_id, string business_unit_id, string item_service_id,string item_id)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            var res = _reportService.Purchase_Order_Report(entity, vendor_category_id == "" ? "-1" : vendor_category_id, vendor_priority_id == "" ? "-1" : vendor_priority_id,
                item_priority_id == "" ? "-1" : item_priority_id, vendor_id == "" ? "-1" : vendor_id, from_date == null ? dte : from_date, to_date == null ? dte : to_date,
                item_group_id == "" ? "-1" : item_group_id, territory_id == "" ? "-1" : territory_id, item_category == "" ? "-1" : item_category, status_id == "" ? "-1" : status_id,
                created_by == "" ? "-1" : created_by, plant_id == "" ? "-1" : plant_id, source_id == "" ? "-1" : source_id, posting_date == null ? dte : posting_date,
                currency_id == "" ? "-1" : currency_id, business_unit_id == "" ? "-1" : business_unit_id, item_service_id == "" ? "-1" : item_service_id, item_id == "" ? "-1" : item_id);
            ServerSideSearch sss = new ServerSideSearch();
            IEnumerable data = sss.ProcessDM(dm, res);
            return Json(new { result = data, count = res.Count() }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OpenPOReport()
        {
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            return View();
        }
        public ActionResult LDReport()
        {
            ViewBag.vendor_category_list = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.vendor_list_code = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            return View();
        }
        public ActionResult Paymentsexpectedtovendors()
        {
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.vendor_category_list = new SelectList(_vendorcategory.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
            return View();

        }
        public ActionResult PurchaseOrderItemLevelReport()
        {
            ViewBag.vendor_category_list = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.vendor_list_code = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            return View();
        }
        public ActionResult PurchaseOrderHeaderReport()
        {
            ViewBag.vendor_category_list = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.vendor_list_code = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            return View();
        }
        public ActionResult PurchaseInvoiceHeaderReport()
        {
            ViewBag.vendor_category_list = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.vendor_list_code = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            return View();
        }
        public ActionResult PurchaseInvoiceItemLevelReport()
        {
            ViewBag.vendor_category_list = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.vendor_list_code = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            return View();
        }
        public ActionResult GRNHeaderReport()
        {
            ViewBag.vendor_category_list = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.vendor_list_code = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            return View();
        }
        public ActionResult PurchaseOrderSummaryReport()
        {
            ViewBag.vendor_category_list = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult GRNItemLevelReport()
        {
            ViewBag.vendor_category_list = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.vendor_list_code = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");

            return View();
        }
        public ActionResult GRNSummaryReport()
        {
            ViewBag.vendor_category_list = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult PurchaseInvoiceSummaryReport()
        {
            ViewBag.vendor_category_list = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult GRNPendingforExciseReport()
        {
            return View();
        }
        public ActionResult GRNPendingforPurchaseInvoiceReport()
        {
            return View();
        }

        public ActionResult PurchaseReturnHeaderReport()
        {
            ViewBag.vendor_category_list = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.vendor_list_code = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            return View();
        }
        public ActionResult PurchaseReturnItemLevelReport()
        {
            ViewBag.vendor_category_list = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.vendor_list_code = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            return View();
        }
        public ActionResult PurchaseReturnSummaryReport()
        {
            ViewBag.vendor_category_list = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult Purchase_Order_Report(string entity, string vendor_category_id, string vendor_priority_id,
        string item_priority_id, string vendor_id, DateTime? from_date, DateTime? to_date, string item_group_id, string territory_id,
        string item_category, string status_id, string created_by, string plant_id, string source_id, DateTime? posting_date,
        string currency_id, string business_unit_id, string item_service_id,string item_id)
        {
            var data = _reportService.Purchase_Order_Report(entity, vendor_category_id, vendor_priority_id,
            item_priority_id, vendor_id, from_date, to_date, item_group_id, territory_id,
            item_category, status_id, created_by, plant_id, source_id, posting_date,
            currency_id, business_unit_id, item_service_id,item_id);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            if (entity == "getopenporeport")
            {
                return PartialView("Partial_OpenPOReport", data);// return PartialView("Partial_PurchaseOrderItemLevel", data);
            }
            else if (entity == "getpurchaseorderheaderlevelreport")
            {
                return PartialView("Partial_PurchaseOrderHeader", data);
            }
            else if (entity == "getpossummaryreport")
            {
                return PartialView("Partial_PurchaseOrderSummary", data);
            }
            else if (entity == "getgrnheaderlevelreport")
            {
                return PartialView("Partial_GRNHeader", data);
            }
            else if (entity == "getgrnitemlevelreport")
            {
                return PartialView("Partial_GRNItemLevel", data);
            }

            else if (entity == "getgrnsummaryreport")
            {
                return PartialView("Partial_GRNSummary", data);
            }
            else if (entity == "getpotopiitemleveltracker")
            {
                return PartialView("Partial_POToPITrackerReport", data);
            }
            else if (entity == "getpisummaryreport")
            {
                return PartialView("Partial_PurchaseInvoiceSummary", data);
            }
            else if (entity == "getpiheaderlevelreport")
            {
                return PartialView("Partial_PurchaseInvoiceHeader", data);
            }
            else if (entity == "getgrnpendingforiex")
            {
                return PartialView("Partial_GRNPendingforExcise", data);
            }
            else if (entity == "getpritemlevelreport")
            {
                return PartialView("Partial_PurchaseReturnItemLevel", data);
            }
            else if (entity == "getprheaderlevelreport")
            {
                return PartialView("Partial_PurchaseReturnHeader", data);
            }
            else if (entity == "getprsummaryreport")
            {
                return PartialView("Partial_PurchaseReturnSummary", data);
            }
            else if (entity == "getpurchaserequisitionsummary")
            {
                return PartialView("Partial_PurchaseRequisitionSummary", data);
            }
            else if (entity == "getgrnpendingforpi")
            {
                return PartialView("Partial_GRNPendingforPurchaseInvoiceReport", data);
            }
            else if (entity == "getldreport")
            {
                return PartialView("Partial_LDReport", data);
            }
            else if (entity == "getpaymentsexpectedtovendors")
            {
                return PartialView("Partial_Paymentsexpectedtovendors", data);
            }
            else if (entity == "getpiitemlevelreport")
            {
                if (item_service_id == "2")
                {
                    ViewBag.item_service_id = false;
                }
                else
                {
                    ViewBag.item_service_id = true;
                }
                return PartialView("Partial_PurchaseInvoiceItemLevel", data);
            }
            else if (entity == "get_pur_requisition_summary")
            {
                //if (item_category == "2")
                //{
                //    ViewBag.item_category = false;
                //}
                //else
                //{
                //    ViewBag.item_category = true;
                //}
                return PartialView("Partial_PurchaseRequisitionSummary", data);
            }
            else if (entity == "get_pur_requisition_detailed")
            {
                if (item_category == "2")
                {
                    ViewBag.item_category = false;
                }
                else
                {
                    ViewBag.item_category = true;
                }
                return PartialView("Partial_PurchaseRequisitionDetailed", data);
            }
            else if (entity == "getvendorcontactdetails")
            {
                if (vendor_category_id == "2")
                {
                    ViewBag.vendor_category_id = false;
                }
                else
                {
                    ViewBag.vendor_category_id = true;
                }
                return PartialView("Partial_VendorContactDetails", data);
            }
            else if (entity == "get_vendor_payment_masterlist")
            {
                if (vendor_category_id == "2")
                {
                    ViewBag.vendor_category_id = false;
                }
                else
                {
                    ViewBag.vendor_category_id = true;
                }
                return PartialView("Partial_VendorPaymentMasterList", data);
            }
            else if (entity == "get_vendor_masterdiscount_details")
            {
                ViewBag.vendor_category_id = true;
                return PartialView("Partial_VendorMasterDiscountDetails", data);
            }
            else if (entity == "get_vendor_purchase_price_list_report")
            {
                if (vendor_category_id == "2")
                {
                    ViewBag.vendor_category_id = false;
                }
                else
                {
                    ViewBag.vendor_category_id = true;
                }
                return PartialView("Partial_PurchasePriceList", data);
            }
            else if (entity == "getopenprreport")
            {
                if (item_category == "2")
                {
                    ViewBag.item_category = false;
                }
                else
                {
                    ViewBag.item_category = true;
                }
                return PartialView("Partial_OpenPRReport", data);
            }
            else
            {
                if (item_service_id == "2")
                {
                    ViewBag.item_service_id = false;
                }
                else
                {
                    ViewBag.item_service_id = true;
                }
                return PartialView("Partial_PurchaseOrderItemLevel", data);
            }

        }
        public ActionResult PurchaseRequisitionSummaryReport()
        {
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("PRCRQ"), "status_id", "status_name");
            return View();
        }
        public ActionResult PurchaseRequisitionDetailedReport()
        {
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.vendor_category_list = new SelectList(_vendorcategory.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
            ViewBag.plant_list = new SelectList(_plant.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.source_list = new SelectList(_source.GetAll(), "SOURCE_ID", "SOURCE_NAME");
            return View();
        }
        public ActionResult VendorContactDetailsReport()
        {
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.vendor_category_list = new SelectList(_vendorcategory.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
            return View();
        }
        public ActionResult VendorPaymentMasterListReport()
        {
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.vendor_category_list = new SelectList(_vendorcategory.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
            return View();
        }
        public ActionResult VendorMasterDiscountDetailsReport()
        {
            return View();
        }
        public ActionResult POToPITrackerReport()
        {
            return View();
        }
        public ActionResult PurchasePriceListReport()
        {
            ViewBag.vendor_category_list = new SelectList(_vendorcategory.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
            ViewBag.vendor_priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.item_priority_list = new SelectList(_Generic.GetPriorityByForm(3), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.item_group_list = new SelectList(_itemgroup.GetAll(), "ITEM_GROUP_ID", "ITEM_GROUP_NAME");

            return View();
        }
        public ActionResult OpenPRReport()
        {
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.plant_list = new SelectList(_plant.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            return View();
        }
       
        public void ExportTreeGridModel(string TreeGridModel, string ctrlname)
        {
            string nme = "";
            ExcelExport exp1 = new ExcelExport();
            TreeGridProperties obj = ConvertTreeGridObject(TreeGridModel);
            var DataSource = _reportService.Purchase_Order_Report(entity, vendor_category_id, vendor_priority_id,
            item_priority_id, vendor_id, from_date, to_date, item_group_id, territory_id,
            item_category, status_id, created_by, plant_id, source_id, posting_date,
            currency_id, business_unit_id, item_service_id, item_id);
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
                if (ds.Key == "posting_date")
                {
                    if (ds.Value != "")
                    {
                        posting_date = DateTime.Parse(ds.Value.ToString());
                    }

                    continue;
                }
                if (ds.Key == "item_category")
                {
                    if (ds.Value != "")
                    {
                        item_category = ds.Value.ToString();
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
                if (ds.Key == "status_id")
                {
                    if (ds.Value != "")
                    {
                        status_id = ds.Value.ToString();
                    }

                    continue;
                }
                if (ds.Key == "source_id")
                {
                    if (ds.Value != "")
                    {
                        source_id = ds.Value.ToString();
                    }

                    continue;
                }
                if (ds.Key == "vendor_priority_id")
                {
                    if (ds.Value != "")
                    {
                        vendor_priority_id = ds.Value.ToString();
                    }
                    continue;
                }
                if (ds.Key == "item_priority_id")
                {
                    if (ds.Value != "")
                    {
                        item_priority_id = ds.Value.ToString();
                    }
                    continue;
                }

                if (ds.Key == "item_group_id")
                {
                    if (ds.Value != "")
                    {
                        item_group_id = ds.Value.ToString();
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
                if (ds.Key == "vendor_category_id")
                {
                    if (ds.Value != "")
                    {
                        vendor_category_id = ds.Value.ToString();
                    }
                    continue;
                }
                if (ds.Key == "vendor_id")
                {
                    if (ds.Value != "")
                    {
                        vendor_id = ds.Value.ToString();
                    }
                    continue;
                }
                if (ds.Key == "item_service_id")
                {
                    if (ds.Value != "")
                    {
                        item_service_id = ds.Value.ToString();
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






//using System.Web.Mvc;
//using Sciffer.Erp.Service.Interface;
//using System;
//using Syncfusion.JavaScript.Models;
//using Syncfusion.EJ.Export;
//using System.Web.Script.Serialization;
//using System.Collections;
//using System.Collections.Generic;
//using Syncfusion.XlsIO;
//using System.Reflection;
//using Newtonsoft.Json;

//namespace Sciffer.Erp.Web.Controllers
//{
//    public class PurchaseReportController : Controller
//    {
//        private readonly IItemCategoryService _itemCategoryService;
//        private readonly IReportService _reportService;
//        private readonly IGenericService _Generic;
//        private readonly IStatusService _status;
//        private readonly IVendorCategoryService _vendorcategory;
//        private readonly IPlantService _plant;
//        private readonly ISourceService _source;
//        private readonly IItemGroupService _itemgroup;
//        private readonly ITerritoryService _territory;
//        public DateTime posting_date { get; set; }
//        public DateTime from_date { get; set; }
//        public DateTime to_date { get; set; }
//        public string item_category { get; set; }
//        public string priority_id { get; set; }
//        public string currency_id { get; set; }
//        public string plant_id { get; set; }
//        public string business_unit_id { get; set; }
//        public string vendor_category_id { get; set; }
//        public string vendor_id { get; set; }
//        public string item_service_id { get; set; }
//        public string vendor_priority_id { get; set; }
//        public string item_group_id { get; set; }
//        public string item_priority_id { get; set; }
//        public string territory_id { get; set; }
//        public string status_id { get; set; }
//        public string created_by { get; set; }
//        public string entity { get; set; }
//        public string source_id { get; set; }
//        public PurchaseReportController(IItemCategoryService ItemCategoryService, IReportService reportService, IGenericService Generic, IStatusService status, IPlantService plant, IVendorCategoryService vendorcategory,
//            ISourceService source, IItemGroupService itemgroup, ITerritoryService territory)
//        {
//            _itemCategoryService = ItemCategoryService;
//            _reportService = reportService;
//            _Generic = Generic;
//            _status = status;
//            _plant = plant;
//            _vendorcategory = vendorcategory;
//            _source = source;
//            _itemgroup = itemgroup;
//            _territory = territory;

//        }
//        public void ExportToExcel(string GridModel, string ctrlname)
//        {
//            ExcelExport exp = new ExcelExport();
//            GridProperties obj = ConvertGridObject(GridModel, ctrlname);
//            var DataSource = GetAllData(ctrlname);
//            exp.Export(obj, DataSource, ctrlname + ".xlsx", ExcelVersion.Excel2010, false, false, "bootstrap-theme");
//        }
//        private GridProperties ConvertGridObject(string gridProperty, string ctrlname)
//        {
//            JavaScriptSerializer serializer = new JavaScriptSerializer();
//            IEnumerable div = (IEnumerable)serializer.Deserialize(gridProperty, typeof(IEnumerable));
//            GridProperties gridProp = new GridProperties();
//            foreach (KeyValuePair<string, object> ds in div)
//            {
//                var property = gridProp.GetType().GetProperty(ds.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);

//                if (ds.Key == "posting_date")
//                {
//                    if (ds.Value != "")
//                    {
//                        posting_date = DateTime.Parse(ds.Value.ToString());
//                    }

//                    continue;
//                }
//                if (ds.Key == "item_category")
//                {
//                    if (ds.Value != "")
//                    {
//                        item_category = ds.Value.ToString();
//                    }

//                    continue;
//                }
//                if (ds.Key == "status_id")
//                {
//                    if (ds.Value != "")
//                    {
//                        status_id = ds.Value.ToString();
//                    }

//                    continue;
//                }
//                if (ds.Key == "source_id")
//                {
//                    if (ds.Value != "")
//                    {
//                        source_id = ds.Value.ToString();
//                    }

//                    continue;
//                }
//                if (ds.Key == "vendor_priority_id")
//                {
//                    if (ds.Value != "")
//                    {
//                        vendor_priority_id = ds.Value.ToString();
//                    }
//                    continue;
//                }
//                if (ds.Key == "item_priority_id")
//                {
//                    if (ds.Value != "")
//                    {
//                        item_priority_id = ds.Value.ToString();
//                    }
//                    continue;
//                }

//                if (ds.Key == "item_group_id")
//                {
//                    if (ds.Value != "")
//                    {
//                        item_group_id = ds.Value.ToString();
//                    }
//                    continue;
//                }

//                if (ds.Key == "priority_id")
//                {
//                    if (ds.Value != "")
//                    {
//                        priority_id = ds.Value.ToString();
//                    }
//                    continue;
//                }
//                if (ds.Key == "currency_id")
//                {
//                    if (ds.Value != "")
//                    {
//                        currency_id = ds.Value.ToString();
//                    }
//                    continue;
//                }
//                if (ds.Key == "plant_id")
//                {
//                    if (ds.Value != "")
//                    {
//                        plant_id = ds.Value.ToString();
//                    }
//                    continue;
//                }
//                if (ds.Key == "business_unit_id")
//                {
//                    if (ds.Value != "")
//                    {
//                        business_unit_id = ds.Value.ToString();
//                    }
//                    continue;
//                }
//                if (ds.Key == "from_date")
//                {
//                    if (ds.Value != "")
//                    {
//                        from_date = DateTime.Parse(ds.Value.ToString());
//                    }
//                    continue;
//                }
//                if (ds.Key == "to_date")
//                {
//                    if (ds.Value != "")
//                    {
//                        to_date = DateTime.Parse(ds.Value.ToString());
//                    }

//                    continue;
//                }
//                if (ds.Key == "vendor_category_id")
//                {
//                    if (ds.Value != "")
//                    {
//                        vendor_category_id = ds.Value.ToString();
//                    }
//                    continue;
//                }
//                if (ds.Key == "vendor_id")
//                {
//                    if (ds.Value != "")
//                    {
//                        vendor_id = ds.Value.ToString();
//                    }
//                    continue;
//                }
//                if (ds.Key == "item_service_id")
//                {
//                    if (ds.Value != "")
//                    {
//                        item_service_id = ds.Value.ToString();
//                    }
//                    continue;
//                }
//                if (property != null)
//                {
//                    Type type = property.PropertyType;
//                    string serialize = serializer.Serialize(ds.Value);
//                    object value = serializer.Deserialize(serialize, type);
//                    property.SetValue(gridProp, value, null);
//                }
//            }
//            return gridProp;
//        }
//        public ActionResult OpenPOReport()
//        {
//            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
//            return View();
//        }
//        public ActionResult PurchaseOrderItemLevelReport()
//        {
//            ViewBag.vendor_category_list = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
//            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
//            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
//            ViewBag.vendor_list_code = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
//            return View();
//        }
//        public ActionResult PurchaseOrderHeaderReport()
//        {
//            ViewBag.vendor_category_list = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
//            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
//            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
//            ViewBag.vendor_list_code = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
//            return View();
//        }
//        public ActionResult PurchaseInvoiceHeaderReport()
//        {
//            ViewBag.vendor_category_list = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
//            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
//            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
//            ViewBag.vendor_list_code = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
//            return View();
//        }
//        public ActionResult PurchaseInvoiceItemLevelReport()
//        {
//            ViewBag.vendor_category_list = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
//            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
//            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
//            ViewBag.vendor_list_code = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
//            return View();
//        }
//        public ActionResult GRNHeaderReport()
//        {
//            ViewBag.vendor_category_list = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
//            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
//            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
//            ViewBag.vendor_list_code = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
//            return View();
//        }
//        public ActionResult PurchaseOrderSummaryReport()
//        {
//            ViewBag.vendor_category_list = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
//            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
//            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
//            return View();
//        }
//        public ActionResult GRNItemLevelReport()
//        {
//            ViewBag.vendor_category_list = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
//            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
//            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
//            ViewBag.vendor_list_code = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
//            return View();
//        }
//        public ActionResult GRNSummaryReport()
//        {
//            ViewBag.vendor_category_list = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
//            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
//            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
//            return View();
//        }
//        public ActionResult PurchaseInvoiceSummaryReport()
//        {
//            ViewBag.vendor_category_list = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
//            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
//            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
//            return View();
//        }
//        public ActionResult GRNPendingforExciseReport()
//        {
//            return View();
//        }
//        public ActionResult PurchaseReturnHeaderReport()
//        {
//            ViewBag.vendor_category_list = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
//            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
//            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
//            ViewBag.vendor_list_code = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
//            return View();
//        }
//        public ActionResult PurchaseReturnItemLevelReport()
//        {
//            ViewBag.vendor_category_list = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
//            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
//            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
//            ViewBag.vendor_list_code = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
//            return View();
//        }
//        public ActionResult PurchaseReturnSummaryReport()
//        {
//            ViewBag.vendor_category_list = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
//            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
//            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
//            return View();
//        }
//        public ActionResult Purchase_Order_Report(string entity, string vendor_category_id, string vendor_priority_id,
//        string item_priority_id, string vendor_id, DateTime? from_date, DateTime? to_date, string item_group_id, string territory_id,
//        string item_category, string status_id, string created_by, string plant_id, string source_id, DateTime? posting_date,
//        string currency_id, string business_unit_id, string item_service_id)
//        {
//            var data = _reportService.Purchase_Order_Report(entity, vendor_category_id, vendor_priority_id,
//            item_priority_id, vendor_id, from_date, to_date, item_group_id, territory_id,
//            item_category, status_id, created_by, plant_id, source_id, posting_date,
//            currency_id, business_unit_id, item_service_id);
//            var list = JsonConvert.SerializeObject(data);
//            ViewBag.datasource = data;
//            if (entity == "getopenporeport")
//            {
//                return PartialView("Partial_OpenPOReport", data);// return PartialView("Partial_PurchaseOrderItemLevel", data);
//            }
//            else if (entity == "getpurchaseorderheaderlevelreport")
//            {
//                return PartialView("Partial_PurchaseOrderHeader", data);
//            }
//            else if (entity == "getpossummaryreport")
//            {
//                return PartialView("Partial_PurchaseOrderSummary", data);
//            }
//            else if (entity == "getgrnheaderlevelreport")
//            {
//                return PartialView("Partial_GRNHeader", data);
//            }
//            else if (entity == "getgrnitemlevelreport")
//            {
//                return PartialView("Partial_GRNItemLevel", data);
//            }
//            else if (entity == "getgrnsummaryreport")
//            {
//                return PartialView("Partial_GRNSummary", data);
//            }
//            else if (entity == "getpisummaryreport")
//            {
//                return PartialView("Partial_PurchaseInvoiceSummary", data);
//            }
//            else if (entity == "getpiheaderlevelreport")
//            {
//                return PartialView("Partial_PurchaseInvoiceHeader", data);
//            }
//            else if (entity == "getgrnpendingforiex")
//            {
//                return PartialView("Partial_GRNPendingforExcise", data);
//            }
//            else if (entity == "getpritemlevelreport")
//            {
//                return PartialView("Partial_PurchaseReturnItemLevel", data);
//            }
//            else if (entity == "getprheaderlevelreport")
//            {
//                return PartialView("Partial_PurchaseReturnHeader", data);
//            }
//            else if (entity == "getprsummaryreport")
//            {
//                return PartialView("Partial_PurchaseReturnSummary", data);
//            }
//            else if (entity == "getpiitemlevelreport")
//            {
//                if (item_service_id == "2")
//                {
//                    ViewBag.item_service_id = false;
//                }
//                else
//                {
//                    ViewBag.item_service_id = true;
//                }
//                return PartialView("Partial_PurchaseInvoiceItemLevel", data);
//            }
//            else if (entity == "get_pur_requisition_summary")
//            {
//                if (item_category == "2")
//                {
//                    ViewBag.item_category = false;
//                }
//                else
//                {
//                    ViewBag.item_category = true;
//                }
//                return PartialView("Partial_PurchaseRequisitionSummary", data);
//            }
//            else if (entity == "get_pur_requisition_detailed")
//            {
//                if (item_category == "2")
//                {
//                    ViewBag.item_category = false;
//                }
//                else
//                {
//                    ViewBag.item_category = true;
//                }
//                return PartialView("Partial_PurchaseRequisitionDetailed", data);
//            }
//            else if (entity == "getvendorcontactdetails")
//            {
//                if (vendor_category_id == "2")
//                {
//                    ViewBag.vendor_category_id = false;
//                }
//                else
//                {
//                    ViewBag.vendor_category_id = true;
//                }
//                return PartialView("Partial_VendorContactDetails", data);
//            }
//            else if (entity == "get_vendor_payment_masterlist")
//            {
//                if (vendor_category_id == "2")
//                {
//                    ViewBag.vendor_category_id = false;
//                }
//                else
//                {
//                    ViewBag.vendor_category_id = true;
//                }
//                return PartialView("Partial_VendorPaymentMasterList", data);
//            }
//            else if (entity == "get_vendor_masterdiscount_details")
//            {
//                ViewBag.vendor_category_id = true;
//                return PartialView("Partial_VendorMasterDiscountDetails", data);
//            }
//            else if (entity == "get_vendor_purchase_price_list_report")
//            {
//                if (vendor_category_id == "2")
//                {
//                    ViewBag.vendor_category_id = false;
//                }
//                else
//                {
//                    ViewBag.vendor_category_id = true;
//                }
//                return PartialView("Partial_PurchasePriceList", data);
//            }
//            else if (entity == "getopenprreport")
//            {
//                if (item_category == "2")
//                {
//                    ViewBag.item_category = false;
//                }
//                else
//                {
//                    ViewBag.item_category = true;
//                }
//                return PartialView("Partial_OpenPRReport", data);
//            }
//            else
//            {
//                if (item_service_id == "2")
//                {
//                    ViewBag.item_service_id = false;
//                }
//                else
//                {
//                    ViewBag.item_service_id = true;
//                }
//                return PartialView("Partial_PurchaseOrderItemLevel", data);
//            }

//        }
//        public ActionResult PurchaseRequisitionSummaryReport()
//        {
//            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
//            ViewBag.status_list = new SelectList(_Generic.GetStatusList("PO"), "status_id", "status_name");
//            return View();
//        }
//        public ActionResult PurchaseRequisitionDetailedReport()
//        {
//            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
//            ViewBag.vendor_category_list = new SelectList(_vendorcategory.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
//            ViewBag.plant_list = new SelectList(_plant.GetAll(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.source_list = new SelectList(_source.GetAll(), "SOURCE_ID", "SOURCE_NAME");
//            return View();
//        }
//        public ActionResult VendorContactDetailsReport()
//        {
//            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.vendor_category_list = new SelectList(_vendorcategory.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
//            return View();
//        }
//        public ActionResult VendorPaymentMasterListReport()
//        {
//            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.vendor_category_list = new SelectList(_vendorcategory.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
//            return View();
//        }
//        public ActionResult VendorMasterDiscountDetailsReport()
//        {
//            return View();
//        }
//        public ActionResult PurchasePriceListReport()
//        {
//            ViewBag.vendor_category_list = new SelectList(_vendorcategory.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
//            ViewBag.vendor_priority_list = new SelectList(_Generic.GetPriorityByForm(2), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.item_priority_list = new SelectList(_Generic.GetPriorityByForm(3), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.item_group_list = new SelectList(_itemgroup.GetAll(), "ITEM_GROUP_ID", "ITEM_GROUP_NAME");

//            return View();
//        }
//        public ActionResult OpenPRReport()
//        {
//            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
//            return View();
//        }
//        public object GetAllData(string controller)
//        {
//            object datasource = null;
//            if (controller == "OpenPO")
//            {
//                entity = "getopenporeport";
//            }
//            else if (controller == "Purchase_Order_Item_Level")
//            {
//                entity = "getpurchaseorderitemlevelreport";
//            }
//            else if (controller == "Purchase_Order_Header_Report")
//            {
//                entity = "getpurchaseorderheaderlevelreport";
//            }
//            else if (controller == "Purchase_Order_Summary")
//            {
//                entity = "getpossummaryreport";
//            }
//            else if (controller == "GRN_Header_Report")
//            {
//                entity = "getgrnheaderlevelreport";
//            }
//            else if (controller == "GRN_Item_Level_Report")
//            {
//                entity = "getgrnitemlevelreport";
//            }
//            else if (controller == "GRN_Summary_Report")
//            {
//                entity = "getgrnsummaryreport";
//            }
//            else if (controller == "Purchase_Invoice_Summary_Report")
//            {
//                entity = "getpisummaryreport";
//            }
//            else if (controller == "Purchase_Invoice_Header_Report")
//            {
//                entity = "getpiheaderlevelreport";
//            }
//            else if (controller == "Purchase_Invoice_Item_Level")
//            {
//                entity = "getpiitemlevelreport";
//            }
//            else if (controller == "Grn_Pending_For_Excise")
//            {
//                entity = "getgrnpendingforiex";
//            }
//            else if (controller == "Purchase_Return_Header_Report")
//            {
//                entity = "getprheaderlevelreport";
//            }
//            else if (controller == "Purchase_Return_Item_Level_Report")
//            {
//                entity = "getpritemlevelreport";
//            }
//            else if (controller == "Purchase_Return_Summary_Report")
//            {
//                entity = "getprsummaryreport";
//            }
//            else if (controller == "PRSummary")
//            {
//                entity = "get_pur_requisition_summary";
//            }
//            else if (controller == "PRDetailed")
//            {
//                entity = "get_pur_requisition_detailed";
//            }
//            else if (controller == "VendorContactDetail")
//            {
//                entity = "getvendorcontactdetails";
//            }
//            else if (controller == "VendorPaymentMasterList")
//            {
//                entity = "get_vendor_payment_masterlist";
//            }
//            else if (controller == "VendorMasterDiscountDetails")
//            {
//                entity = "get_vendor_masterdiscount_details";
//            }
//            else if (controller == "PurchasePricelist")
//            {
//                entity = "get_vendor_purchase_price_list_report";
//            }
//            else if (controller == "OpenPRReport")
//            {
//                entity = "getopenprreport";
//            }
//            datasource = _reportService.Purchase_Order_Report(entity, vendor_category_id, vendor_priority_id,
//            item_priority_id, vendor_id, from_date, to_date, item_group_id, territory_id,
//            item_category, status_id, created_by, plant_id, source_id, posting_date,
//            currency_id, business_unit_id, item_service_id);
//            return datasource;
//        }
//    }
//}