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
    public class SalesReportController : Controller
    {
        private readonly IItemCategoryService _itemCategoryService;
        private readonly IReportService _reportService;
        private readonly IGenericService _Generic;
        private readonly IStatusService _status;
        private readonly ICustomerCategoryService _customercategory;
        private readonly IPlantService _plant;
        private readonly ISourceService _source;
        private readonly IItemGroupService _itemgroup;
        private readonly ITerritoryService _territory;
        private readonly IItemService _item;
        private readonly ICustomerService _customer;
        private readonly ISalesRMService _salesrm;
        private readonly ICurrencyService _currency;
        public DateTime posting_date { get; set; }
        public DateTime from_date { get; set; }
        public DateTime to_date { get; set; }
        public string item_category { get; set; }
        public string priority_id { get; set; }
        public string currency_id { get; set; }
        public string plant_id { get; set; }
        public string business_unit_id { get; set; }
        public string customer_category_id { get; set; }
        public string vendor_id { get; set; }
        public string item_service_id { get; set; }
        public string customer_priority_id { get; set; }
        public string item_group_id { get; set; }
        public string item_priority_id { get; set; }
        public string territory_id { get; set; }
        public string status_id { get; set; }
        public string created_by { get; set; }
        public string entity { get; set; }
        public string source_id { get; set; }
        public string customer_id { get; set; }
        public string item_id { get; set; }
        public string sales_rm { get; set; }


        public SalesReportController(IItemCategoryService ItemCategoryService, IReportService reportService, IGenericService Generic, IStatusService status, IPlantService plant, ICustomerCategoryService customercategory,
            ISourceService source, IItemGroupService itemgroup, ITerritoryService territory, IItemService item, ICustomerService customer, ISalesRMService salesrm, ICurrencyService currency)
        {
            _itemCategoryService = ItemCategoryService;
            _reportService = reportService;
            _Generic = Generic;
            _status = status;
            _plant = plant;
            _customercategory = customercategory;
            _source = source;
            _itemgroup = itemgroup;
            _territory = territory;
            _customer = customer;
            _item = item;
            _salesrm = salesrm;
            _currency = currency;
        }
        public void ExportTreeGridModel(string TreeGridModel, string ctrlname)
        {
            string nme = "";
            ExcelExport exp1 = new ExcelExport();
            TreeGridProperties obj = ConvertTreeGridObject(TreeGridModel);
            var DataSource = _reportService.Sales_Order_Report(entity, customer_category_id, customer_priority_id,
            item_priority_id, customer_id, from_date, to_date, item_group_id, territory_id,
            item_category, status_id, created_by, plant_id, source_id, posting_date,
            currency_id, business_unit_id, item_service_id, item_id, sales_rm);
            nme = ctrlname + ".xlsx";
            exp1.Export(obj, DataSource, nme, ExcelVersion.Excel2010, new TreeGridExportSettings() { Theme = ExportTheme.BootstrapTheme });
            // exp.Export(obj, DataSource, ctrlname + ".xlsx", ExcelVersion.Excel2010, new TreeGridExportSettings() { Theme = ExportTheme.BootstrapTheme });
            //  exp.Export(obj, DataSource, ctrlname + ".xlsx", ExcelVersion.Excel2010, false, false, "bootstrap-theme");
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
                if (ds.Key == "posting_date")
                {
                    if (ds.Value != "")
                    {
                        posting_date = DateTime.Parse(ds.Value.ToString());
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
                if (ds.Key == "customer_id")
                {
                    if (ds.Value != "")
                    {
                        customer_id = ds.Value.ToString();
                    }

                    continue;
                }
                if (ds.Key == "sales_rm")
                {
                    if (ds.Value != "")
                    {
                        sales_rm = ds.Value.ToString();
                    }

                    continue;
                }
                if (ds.Key == "territory_id")
                {
                    if (ds.Value != "")
                    {
                        territory_id = ds.Value.ToString();
                    }

                    continue;
                }
                if (ds.Key == "item_id")
                {
                    if (ds.Value != "")
                    {
                        item_id = ds.Value.ToString();
                    }
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
                if (ds.Key == "customer_priority_id")
                {
                    if (ds.Value != "")
                    {
                        customer_priority_id = ds.Value.ToString();
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
                if (ds.Key == "customer_category_id")
                {
                    if (ds.Value != "")
                    {
                        customer_category_id = ds.Value.ToString();
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
        public ActionResult OpenPOReport()
        {
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            return View();
        }
        public ActionResult Get_Partial(string entity, string customer_category_id, string customer_priority_id,
           string item_priority_id, string customer_id, DateTime? from_date, DateTime? to_date, string item_group_id, string territory_id,
           string item_category, string status_id, string created_by, string plant_id, string source_id, DateTime? posting_date,
           string currency_id, string business_unit_id, string item_service_id, string item_id, string sales_rm, string partial_v)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            ViewBag.entity = entity;
            ViewBag.customer_category_id = customer_category_id;
            ViewBag.customer_priority_id = customer_priority_id;
            ViewBag.item_priority_id = item_priority_id;
            ViewBag.customer_id = customer_id;
            ViewBag.from_date = from_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(from_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.to_date = to_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(to_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.item_group_id = item_group_id;
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
            ViewBag.sales_rm = sales_rm;
            if (entity == "get_customer_payment_masterlist")
            {
                if (customer_category_id == "2")
                {
                    ViewBag.customer_category = false;
                }
                else
                {
                    ViewBag.customer_category = true;
                }
                return PartialView("Partial_CustomerPaymentMasterList", ViewBag);
            }

            else if (entity == "get_sales_price_list_report")
            {
                if (customer_category_id == "2")
                {
                    ViewBag.customer_category = false;
                }
                else
                {
                    ViewBag.customer_category = true;
                }
                return PartialView("Partial_SalesPriceList", ViewBag);
            }
            else 
            {
                return PartialView(partial_v, ViewBag);
            }

        }
        public object GetAllData(string controller)
        {
            object datasource = null;
            datasource = _reportService.Sales_Order_Report(entity, customer_category_id, customer_priority_id,
            item_priority_id, customer_id, from_date, to_date, item_group_id, territory_id,
            item_category, status_id, created_by, plant_id, source_id, posting_date,
            currency_id, business_unit_id, item_service_id, item_id, sales_rm);
            return datasource;
        }
        //get data on button click
        public ActionResult GetSalesOrderData(DataManager dm, string entity, string customer_category_id, string customer_priority_id,
           string item_priority_id, string customer_id, DateTime? from_date, DateTime? to_date, string item_group_id, string territory_id,
           string item_category, string status_id, string created_by, string plant_id, string source_id, DateTime? posting_date,
           string currency_id, string business_unit_id, string item_service_id, string item_id, string sales_rm)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            var res = _reportService.Sales_Order_Report(entity, customer_category_id == "" ? "-1" : customer_category_id, customer_priority_id == "" ? "-1" : customer_priority_id,
            item_priority_id == "" ? "-1" : item_priority_id, customer_id == "" ? "-1" : customer_id, from_date == null ? dte : from_date, to_date == null ? dte : to_date, item_group_id == "" ? "-1" : item_group_id, territory_id == "" ? "-1" : territory_id,
            item_category == "" ? "-1" : item_category, status_id == "" ? "-1" : status_id, created_by == "" ? "-1" : created_by, plant_id == "" ? "-1" : plant_id, source_id == "" ? "-1" : source_id, posting_date == null ? dte : posting_date,
            currency_id == "" ? "-1" : currency_id, business_unit_id == "" ? "-1" : business_unit_id, item_service_id == "" ? "-1" : item_service_id, item_id == "" ? "-1" : item_id, sales_rm == "" ? "-1" : sales_rm);
            ServerSideSearch sss = new ServerSideSearch();
            IEnumerable data = sss.ProcessDM(dm, res);
            return Json(new { result = data, count = res.Count() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Sales_Order_Report(string entity, string customer_category_id, string customer_priority_id,
           string item_priority_id, string customer_id, DateTime? from_date, DateTime? to_date, string item_group_id, string territory_id,
           string item_category, string status_id, string created_by, string plant_id, string source_id, DateTime? posting_date,
           string currency_id, string business_unit_id, string item_service_id, string item_id, string sales_rm)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            var data = _reportService.Sales_Order_Report(entity, customer_category_id, customer_priority_id,
            item_priority_id, customer_id, from_date, to_date, item_group_id, territory_id,
            item_category, status_id, created_by, plant_id, source_id, posting_date,
            currency_id, business_unit_id, item_service_id, item_id, sales_rm);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            if (entity == "getcustomercontactdetails")
            {
                return PartialView("Partial_CustomerContactDetails", data);// return PartialView("Partial_PurchaseOrderItemLevel", data);
            }
            else if (entity == "get_customer_payment_masterlist")
            {
                if (customer_category_id == "2")
                {
                    ViewBag.customer_category = false;
                }
                else
                {
                    ViewBag.customer_category = true;
                }
                return PartialView("Partial_CustomerPaymentMasterList", data);
            }
            else if (entity == "get_item_wise_sales_summary")
            {
                return PartialView("Partial_ItemWiseSalesSummary", data);
            }
            else if (entity == "get_item_wise_sales_details")
            {
                return PartialView("Partial_ItemWiseSalesDetail", data);
            }
            else if (entity == "get_sales_summary_report")
            {
                return PartialView("Partial_SalesSummary", data);
            }
            else if (entity == "get_sales_header_level_report")
            {
                return PartialView("Partial_SalesHeaderLevel", data);
            }
            else if (entity == "get_sales_detail_level_report")
            {
                return PartialView("Partial_SalesDetailLevel", data);
            }
            else if (entity == "get_sales_quotation_summary_report")
            {
                return PartialView("Partial_SalesQuotationSummary", data);
            }
            else if (entity == "get_sales_quotation_header_level_report")
            {
                return PartialView("Partial_SalesQuotationHeaderLevel", data);
            }
            else if (entity == "getforexsales")
            {
                return PartialView("Partial_ForexSales", data);
            }
            else if (entity == "get_sales_quotation_detail_level_report")
            {
                return PartialView("Partial_SalesQuotationDetaillevel", data);
            }
            else if (entity == "get_sales_order_summary_report")
            {
                return PartialView("Partial_SalesOrderSummary", data);
            }
            else if (entity == "get_sales_order_header_level_report")
            {
                return PartialView("Partial_SalesOrderHeaderLevel", data);
            }
            else if (entity == "get_sales_order_detail_level_report")
            {
                return PartialView("Partial_SalesOrderDetaillevel", data);
            }
            else if (entity == "get_sales_order_item_wise_plan")
            {
                return PartialView("Partial_ItemWiseSalesPlan", data);
            }
            else if (entity == "getcustomermasterdiscountdetails")
            {
                return PartialView("Partial_CustomerMasterDiscountDetails", data);
            }
            else if (entity == "getcustomerledger")
            {
                return PartialView("Partial_CustomerLedgerReport", data);
            }
            else if (entity == "get_sales_order_tracker_report")
            {
                return PartialView("Partial_SalesOrderQtyTrackerReport", data);
            }
            else if (entity == "get_collection_expected_customer_report")
            {
                return PartialView("Partial_CollectionsExpected", data);
            }
            else if (entity == "get_sales_order_item_wise_plan")
            {
                return PartialView("Partial_ItemWiseSalesPlan", data);
            }
            else if (entity == "get_open_sales_order_by_invoice")
            {
                return PartialView("Partial_OpenSalesOrder", ViewBag);
            }
            else if (entity == "get_customer_discount_detail")
            {
                return PartialView("Partial_CustomerMasterDiscountDetails", ViewBag);
            }
            else if (entity == "get_challan_summary")
            {
                return PartialView("Partial_ChallanSummaryReport", ViewBag);
            }
            else if (entity == "get_fg_balance_ready_for_dispatch")
            {
                return PartialView("Partial_FgBalanceReadyForDispatch", ViewBag);
            }
            else if (entity == "get_sales_return_summary_report")
            {
                return PartialView("Partial_SalesReturnSummary", ViewBag);
            }
            else if (entity == "get_sales_return_header_level_report")
            {
                return PartialView("Partial_SalesReturnHeaderLevel", ViewBag);
            }
            else if (entity == "get_sales_return_detail_level_report")
            {
                return PartialView("Partial_SalesReturnDetailLevel", ViewBag);
            }
            else if (entity == "get_challan_wise_sales_report")
            {
                return PartialView("Partial_ChallanWiseSalesReport", ViewBag);
            }
            else
            {
                if (customer_category_id == "2")
                {
                    ViewBag.customer_category = false;
                }
                else
                {
                    ViewBag.customer_category = true;
                }
                return PartialView("Partial_SalesPriceList", data);
            }

        }
        
        public ActionResult CustomerContactDetailsReport()
        {
            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");

            return View();
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SalesReturnSummaryReport()
        {
            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesrm.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currency.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult SalesReturnHeaderLevelReport()
        {
            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesrm.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currency.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult SalesReturnDetailLevelReport()
        {
            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesrm.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currency.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult CustomerPaymentMasterListReport()
        {
            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");

            return View();
        }
        public ActionResult SalesPriceListReport()
        {
            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.customer_priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.item_priority_list = new SelectList(_Generic.GetPriorityByForm(3), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.item_group_list = new SelectList(_itemgroup.GetAll(), "ITEM_GROUP_ID", "ITEM_GROUP_NAME");
            ViewBag.territory_list = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            return View();
        }
        public ActionResult ItemWiseSalesSummaryReport()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_item.GetAll(), "ITEM_ID", "ITEM_CODE");
            return View();
        }
        public ActionResult ForexSales()
        {
            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesrm.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currency.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult ItemWiseSalesDetailReport()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_item.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.customer_list = new SelectList(_customer.GetAll(), "CUSTOMER_ID", "CUSTOMER_CODE");
            return View();
        }
        public ActionResult CustomerLedgerReport()
        {
            ViewBag.customer_list = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            return View();
        }
        public ActionResult SalesSummaryReport()
        {
            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesrm.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currency.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult OpenSalesOrderReport()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            return View();
        }
        public ActionResult SalesheaderlevelReport()
        {
            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesrm.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currency.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult SalesDetailLevelReport()
        {
            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesrm.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currency.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult SalesQuotationSummaryReport()
        {
            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesrm.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currency.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult ChallanSummaryReport()
        {
            ViewBag.item_list = new SelectList(_Generic.GetJobWorkInItem(), "ITEM_ID", "ITEM_NAME");
            return View();
        }
        public ActionResult SalesQuotationHeaderLevelReport()
        {
            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesrm.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currency.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult SalesQuotationDetaillevelReport()
        {
            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesrm.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currency.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult FgBalanceReadyForDispatch()
        {
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(2), "ITEM_ID", "ITEM_NAME");
            return View();
        }
        public ActionResult SalesOrderSummaryReport()
        {
            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesrm.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currency.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult SalesOrderHeaderLevelReport()
        {
            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesrm.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currency.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult SalesOrderDetaillevelReport()
        {
            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesrm.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currency.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult SalesOrderItemWisePlanReport()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.item_list = new SelectList(_item.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.customer_list = new SelectList(_customer.GetAll(), "CUSTOMER_ID", "CUSTOMER_CODE");
            return View();
        }
        public ActionResult CustomerMasterDiscountDetailsReport()
        {
            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            return View();
        }
        public ActionResult SalesOrderQtyTrackerReport()
        {
            ViewBag.item_list = new SelectList(_item.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.customer_list = new SelectList(_customer.GetAll(), "CUSTOMER_ID", "CUSTOMER_CODE");
            return View();
        }
        public ActionResult CollectionsExpectedReport()
        {
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            return View();
        }
        public ActionResult ChallanWiseSalesReport()
        {
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            return View();
        }
     
        public JsonResult GetIndexData(DataManager dm, string ctrl_name)
        {

            var list = GetAllData(ctrl_name);
            IEnumerable data = (IEnumerable)list;
            DataOperations operation = new DataOperations();
            //for filtring
            if (dm.Sorted != null && dm.Sorted.Count > 0)
            {
                data = operation.PerformSorting(data, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0)
            {
                data = operation.PerformWhereFilter(data, dm.Where, dm.Where[0].Operator);
            }
            // for searching
            if (dm.Search != null && dm.Search.Count > 0)
            {
                data = operation.PerformSearching(data, dm.Search);
            }
            int count = data.Cast<Object>().Count();
            if (dm.Skip != 0)
            {
                data = operation.PerformSkip(data, dm.Skip);
            }
            if (dm.Take != 0)
            {
                data = operation.PerformTake(data, dm.Take);
            }
            return Json(new { result = data, count = count });
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
//    public class SalesReportController : Controller
//    {
//        private readonly IItemCategoryService _itemCategoryService;
//        private readonly IReportService _reportService;
//        private readonly IGenericService _Generic;
//        private readonly IStatusService _status;
//        private readonly ICustomerCategoryService _customercategory;
//        private readonly IPlantService _plant;
//        private readonly ISourceService _source;
//        private readonly IItemGroupService _itemgroup;
//        private readonly ITerritoryService _territory;
//        private readonly IItemService _item;
//        private readonly ICustomerService _customer;
//        private readonly ISalesRMService _salesrm;
//        private readonly ICurrencyService _currency;
//        public DateTime posting_date { get; set; }
//        public DateTime from_date { get; set; }
//        public DateTime to_date { get; set; }
//        public string item_category { get; set; }
//        public string priority_id { get; set; }
//        public string currency_id { get; set; }
//        public string plant_id { get; set; }
//        public string business_unit_id { get; set; }
//        public string customer_category_id { get; set; }
//        public string vendor_id { get; set; }
//        public string item_service_id { get; set; }
//        public string customer_priority_id { get; set; }
//        public string item_group_id { get; set; }
//        public string item_priority_id { get; set; }
//        public string territory_id { get; set; }
//        public string status_id { get; set; }
//        public string created_by { get; set; }
//        public string entity { get; set; }
//        public string source_id { get; set; }
//        public string customer_id { get; set; }
//        public string item_id { get; set; }
//        public string sales_rm { get; set; }


//        public SalesReportController(IItemCategoryService ItemCategoryService, IReportService reportService, IGenericService Generic, IStatusService status, IPlantService plant, ICustomerCategoryService customercategory,
//            ISourceService source, IItemGroupService itemgroup, ITerritoryService territory, IItemService item, ICustomerService customer, ISalesRMService salesrm, ICurrencyService currency)
//        {
//            _itemCategoryService = ItemCategoryService;
//            _reportService = reportService;
//            _Generic = Generic;
//            _status = status;
//            _plant = plant;
//            _customercategory = customercategory;
//            _source = source;
//            _itemgroup = itemgroup;
//            _territory = territory;
//            _customer = customer;
//            _item = item;
//            _salesrm = salesrm;
//            _currency = currency;
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
//                if (ds.Key == "customer_id")
//                {
//                    if (ds.Value != "")
//                    {
//                        customer_id = ds.Value.ToString();
//                    }

//                    continue;
//                }
//                if (ds.Key == "sales_rm")
//                {
//                    if (ds.Value != "")
//                    {
//                        sales_rm = ds.Value.ToString();
//                    }

//                    continue;
//                }
//                if (ds.Key == "territory_id")
//                {
//                    if (ds.Value != "")
//                    {
//                        territory_id = ds.Value.ToString();
//                    }

//                    continue;
//                }
//                if (ds.Key == "item_id")
//                {
//                    if (ds.Value != "")
//                    {
//                        item_id = ds.Value.ToString();
//                    }
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
//                if (ds.Key == "customer_priority_id")
//                {
//                    if (ds.Value != "")
//                    {
//                        customer_priority_id = ds.Value.ToString();
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
//                if (ds.Key == "customer_category_id")
//                {
//                    if (ds.Value != "")
//                    {
//                        customer_category_id = ds.Value.ToString();
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

//        public ActionResult Sales_Order_Report(string entity, string customer_category_id, string customer_priority_id,
//           string item_priority_id, string customer_id, DateTime? from_date, DateTime? to_date, string item_group_id, string territory_id,
//           string item_category, string status_id, string created_by, string plant_id, string source_id, DateTime? posting_date,
//           string currency_id, string business_unit_id, string item_service_id, string item_id, string sales_rm)
//        {

//            var data = _reportService.Sales_Order_Report(entity, customer_category_id, customer_priority_id,
//            item_priority_id, customer_id, from_date, to_date, item_group_id, territory_id,
//            item_category, status_id, created_by, plant_id, source_id, posting_date,
//            currency_id, business_unit_id, item_service_id,item_id,sales_rm);
//            var list = JsonConvert.SerializeObject(data);
//            ViewBag.datasource = data;
//            if (entity == "getcustomercontactdetails")
//            {
//                return PartialView("Partial_CustomerContactDetails", data);// return PartialView("Partial_PurchaseOrderItemLevel", data);
//            }
//            else if (entity == "get_customer_payment_masterlist")
//            {
//                if (customer_category_id == "2")
//                {
//                    ViewBag.customer_category = false;
//                }
//                else
//                {
//                    ViewBag.customer_category = true;
//                }
//                return PartialView("Partial_CustomerPaymentMasterList", data);
//            }
//            else if (entity == "get_item_wise_sales_summary")
//            {               
//                return PartialView("Partial_ItemWiseSalesSummary", data);
//            }
//            else if (entity == "get_item_wise_sales_details")
//            {
//                return PartialView("Partial_ItemWiseSalesDetail", data);
//            }
//            else if (entity == "get_sales_summary_report")
//            {
//                return PartialView("Partial_SalesSummary", data);
//            }
//            else if (entity == "get_sales_header_level_report")
//            {
//                return PartialView("Partial_SalesHeaderLevel", data);
//            }
//            else if (entity == "get_sales_detail_level_report")
//            {
//                return PartialView("Partial_SalesDetailLevel", data);
//            }
//            else if (entity == "get_sales_quotation_summary_report")
//            {
//                return PartialView("Partial_SalesQuotationSummary", data);
//            }
//            else if (entity == "get_sales_quotation_header_level_report")
//            {
//                return PartialView("Partial_SalesQuotationHeaderLevel", data);
//            }
//            else if (entity == "get_sales_quotation_detail_level_report")
//            {
//                return PartialView("Partial_SalesQuotationDetaillevel", data);
//            }
//            else if (entity == "get_sales_order_summary_report")
//            {
//                return PartialView("Partial_SalesOrderSummary", data);
//            }
//            else if (entity == "get_sales_order_header_level_report")
//            {
//                return PartialView("Partial_SalesOrderHeaderLevel", data);
//            }
//            else if (entity == "get_sales_order_detail_level_report")
//            {
//                return PartialView("Partial_SalesOrderDetaillevel", data);
//            }
//            else if (entity == "get_sales_order_item_wise_plan_report")
//            {
//                return PartialView("Partial_ItemWiseSalesPlan", data);
//            }
//            else if (entity == "getcustomermasterdiscountdetails")
//            {
//                return PartialView("Partial_CustomerMasterDiscountDetails", data);
//            }
//            else if(entity== "getcustomerledger")
//            {
//                return PartialView("Partial_CustomerLedgerReport", data);
//            }
//            else
//            {
//                if (customer_category_id == "2")
//                {
//                    ViewBag.customer_category = false;
//                }
//                else
//                {
//                    ViewBag.customer_category= true;
//                }
//                return PartialView("Partial_SalesPriceList", data);
//            }

//        }
//        public ActionResult CustomerContactDetailsReport()
//        {
//            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");

//            return View();
//        }

//        public ActionResult CustomerPaymentMasterListReport()
//        {
//            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");           
//            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");

//            return View();
//        }
//        public ActionResult SalesPriceListReport()
//        {
//            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");            
//            ViewBag.customer_priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.item_priority_list = new SelectList(_Generic.GetPriorityByForm(3), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.item_group_list = new SelectList(_itemgroup.GetAll(), "ITEM_GROUP_ID", "ITEM_GROUP_NAME");
//            ViewBag.territory_list = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
//            return View();
//        }
//        public ActionResult ItemWiseSalesSummaryReport()
//        {
//            ViewBag.plant_list = new SelectList(_plant.GetAll(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.item_list = new SelectList(_item.GetAll(), "ITEM_ID", "ITEM_CODE");
//            return View();
//        }
//        public ActionResult ItemWiseSalesDetailReport()
//        {
//            ViewBag.plant_list = new SelectList(_plant.GetAll(), "PLANT_ID", "PLANT_NAME");

//            ViewBag.item_list = new SelectList(_item.GetAll(), "ITEM_ID", "ITEM_CODE");
//            ViewBag.customer_list = new SelectList(_customer.GetAll(), "CUSTOMER_ID", "CUSTOMER_CODE");
//            return View();
//        }
//        public ActionResult CustomerLedgerReport()
//        {
//            ViewBag.customer_list = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
//            return View();
//        }
//        public ActionResult SalesSummaryReport()
//        {
//            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
//            ViewBag.sale_rm_list = new SelectList(_salesrm.GetAll(), "sales_rm_id", "employee_name");
//            ViewBag.territory_list = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
//            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
//            ViewBag.currency_list = new SelectList(_currency.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
//            ViewBag.plant_list = new SelectList(_plant.GetAll(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
//            return View();
//        }
//        public ActionResult SalesheaderlevelReport()
//        {
//            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
//            ViewBag.sale_rm_list = new SelectList(_salesrm.GetAll(), "sales_rm_id", "employee_name");
//            ViewBag.territory_list = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
//            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
//            ViewBag.currency_list = new SelectList(_currency.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
//            ViewBag.plant_list = new SelectList(_plant.GetAll(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
//            return View();
//        }
//        public ActionResult SalesDetailLevelReport()
//        {
//            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
//            ViewBag.sale_rm_list = new SelectList(_salesrm.GetAll(), "sales_rm_id", "employee_name");
//            ViewBag.territory_list = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
//            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
//            ViewBag.currency_list = new SelectList(_currency.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
//            ViewBag.plant_list = new SelectList(_plant.GetAll(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
//            return View();
//        }
//        public ActionResult SalesQuotationSummaryReport()
//        {
//            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
//            ViewBag.sale_rm_list = new SelectList(_salesrm.GetAll(), "sales_rm_id", "employee_name");
//            ViewBag.territory_list = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
//            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
//            ViewBag.currency_list = new SelectList(_currency.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
//            ViewBag.plant_list = new SelectList(_plant.GetAll(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
//            return View();
//        }

//        public ActionResult SalesQuotationHeaderLevelReport()
//        {
//            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
//            ViewBag.sale_rm_list = new SelectList(_salesrm.GetAll(), "sales_rm_id", "employee_name");
//            ViewBag.territory_list = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
//            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
//            ViewBag.currency_list = new SelectList(_currency.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
//            ViewBag.plant_list = new SelectList(_plant.GetAll(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
//            return View();
//        }
//        public ActionResult SalesQuotationDetaillevelReport()
//        {
//            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
//            ViewBag.sale_rm_list = new SelectList(_salesrm.GetAll(), "sales_rm_id", "employee_name");
//            ViewBag.territory_list = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
//            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
//            ViewBag.currency_list = new SelectList(_currency.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
//            ViewBag.plant_list = new SelectList(_plant.GetAll(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
//            return View();
//        }
//        public ActionResult SalesOrderSummaryReport()
//        {
//            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
//            ViewBag.sale_rm_list = new SelectList(_salesrm.GetAll(), "sales_rm_id", "employee_name");
//            ViewBag.territory_list = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
//            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
//            ViewBag.currency_list = new SelectList(_currency.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
//            ViewBag.plant_list = new SelectList(_plant.GetAll(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
//            return View();
//        }
//        public ActionResult SalesOrderHeaderLevelReport()
//        {
//            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
//            ViewBag.sale_rm_list = new SelectList(_salesrm.GetAll(), "sales_rm_id", "employee_name");
//            ViewBag.territory_list = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
//            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
//            ViewBag.currency_list = new SelectList(_currency.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
//            ViewBag.plant_list = new SelectList(_plant.GetAll(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
//            return View();
//        }
//        public ActionResult SalesOrderDetaillevelReport()
//        {
//            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
//            ViewBag.sale_rm_list = new SelectList(_salesrm.GetAll(), "sales_rm_id", "employee_name");
//            ViewBag.territory_list = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
//            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
//            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
//            ViewBag.currency_list = new SelectList(_currency.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
//            ViewBag.plant_list = new SelectList(_plant.GetAll(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
//            return View();
//        }
//        public ActionResult SalesOrderItemWisePlanReport()
//        {
//            ViewBag.plant_list = new SelectList(_plant.GetAll(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
//            ViewBag.item_list = new SelectList(_item.GetAll(), "ITEM_ID", "ITEM_CODE");
//            ViewBag.customer_list = new SelectList(_customer.GetAll(), "CUSTOMER_ID", "CUSTOMER_CODE");
//            return View();
//        }
//        public ActionResult CustomerMasterDiscountDetailsReport()
//        {
//            ViewBag.customer_category_list = new SelectList(_customercategory.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
//            ViewBag.plant_list = new SelectList(_plant.GetAll(), "PLANT_ID", "PLANT_NAME");
//            return View();
//        }

//        public object GetAllData(string controller)
//        {
//            object datasource = null;
//            if (controller == "CustomerContact")
//            {
//                entity = "getcustomercontactdetails";
//            }
//            else if (controller == "CustomerPayment")
//            {
//                entity = "get_customer_payment_masterlist";
//            }
//            else if (controller == "SalesPriceList")
//            {
//                entity = "get_sales_price_list_report";
//            }
//            else if (controller == "ItemWiseSalesSummary")
//            {
//                entity = "get_item_wise_sales_summary";
//            }
//            else if (controller == "ItemWiseSalesDetails")
//            {
//                entity = "get_item_wise_sales_details";
//            }
//            else if (controller == "SalesSummary")
//            {
//                entity = "get_sales_summary_report";
//            }
//            else if (controller == "SalesHeaderLevel")
//            {
//                entity = "get_sales_header_level_report";
//            }
//            else if (controller == "SalesDetailLevel")
//            {
//                entity = "get_sales_detail_level_report";
//            }
//            else if (controller == "SalesQuotationSummary")
//            {
//                entity = "get_sales_quotation_summary_report";
//            }
//            else if (controller == "SalesQuotationHeader")
//            {
//                entity = "get_sales_quotation_header_level_report";
//            }
//            else if (controller == "SalesQuotationDetail")
//            {
//                entity = "get_sales_quotation_detail_level_report";
//            }
//            else if (controller == "SalesOrderSummary")
//            {
//                entity = "get_sales_order_summary_report";
//            }
//            else if(controller== "CustomerLedger")
//            {
//                entity = "getcustomerledger";
//            }
//            else if (controller == "SalesOrderHeader")
//            {
//                entity = "get_sales_order_header_level_report";
//            }
//            else if (controller == "SalesOrderDetail")
//            {
//                entity = "get_sales_order_detail_level_report";
//            }
//            else if (controller == "SalesOrderItemWisePlan")
//            {
//                entity = "get_sales_order_item_wise_plan_report";
//            }
//            else if (controller == "CustomerMasterdiscountdetails")
//            {
//                entity = "getcustomermasterdiscountdetails";
//            }

//            datasource = _reportService.Sales_Order_Report(entity, customer_category_id, customer_priority_id,
//            item_priority_id, customer_id, from_date, to_date, item_group_id, territory_id,
//            item_category, status_id, created_by, plant_id, source_id, posting_date,
//            currency_id, business_unit_id, item_service_id,item_id,sales_rm);
//            return datasource;
//        }
//    }
//}









