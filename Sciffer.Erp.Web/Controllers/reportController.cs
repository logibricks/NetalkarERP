using Newtonsoft.Json;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Sciffer.Erp.Web.Controllers
{
    public class reportController : Controller
    {
        private readonly ICustomerCategoryService _customerCategoryService;
        private readonly ISalesRMService _salesRMService;
        private readonly ITerritoryService _territoryService;
        private readonly IPriorityService _priorityService;
        private readonly IGeneralLedgerService _generalLedgerService;
        private readonly ICurrencyService _currencyService;
        private readonly IPlantService _plantService;
        private readonly IBusinessUnitService _businessUnitService;
        private readonly IReportService _reportService;
        private readonly IGenericService _Generic;
        private readonly IItemService _itemService;
        private readonly ICustomerService _customerService;
        private readonly IItemGroupService _itemGroupService;
        private readonly IVendorCategoryService _vendorCategoryService;
        private readonly IVendorService _vendorService;
        private readonly IItemCategoryService _itemCategoryService;
        private readonly IStatusService _statusService;
        private readonly IBucketService _bucketService;
        private readonly IReasonDeterminationService _reasonDeterminationService;
        private readonly IAccountTypesService _accountTypesService;
        private readonly IEntityTypeService _entityTypeService;
        private readonly ICategoryService _categoryService;
        private readonly ITdsCodeService _tdsCodeService;
        private readonly IMachineMasterService _machineMasterService;
        private readonly IEmployeeService _employeeService;
        private readonly IStorageLocation _storageLocation;
        public reportController(IEmployeeService EmployeeService, ICategoryService CategoryService,IReportService ReportService, IBusinessUnitService BusinessUnitService, IPlantService PlantService, ICurrencyService CurrencyService, 
            IGeneralLedgerService GeneralLedgerService, IPriorityService PriorityService, ITerritoryService TerritoryService, ISalesRMService SalesRMService, 
            ICustomerCategoryService CustomerCategoryService, IGenericService generic, IItemService ItemService, ICustomerService CustomerService,
            IItemGroupService ItemGroupService, IVendorCategoryService VendorCategoryService, IVendorService VendorService,
            IItemCategoryService ItemCategoryService, IStatusService StatusService, IBucketService BucketService, 
            IReasonDeterminationService ReasonDeterminationService, IAccountTypesService AccountTypesService, IEntityTypeService EntityTypeService,
            ITdsCodeService TdsCodeService, IMachineMasterService MachineMasterService, IStorageLocation StorageLocation)
        {
            _machineMasterService = MachineMasterService;
            _categoryService = CategoryService;
            _customerCategoryService = CustomerCategoryService;
            _salesRMService = SalesRMService;
            _territoryService = TerritoryService;
            _priorityService = PriorityService;
            _generalLedgerService = GeneralLedgerService;
            _currencyService = CurrencyService;
            _plantService = PlantService;
            _businessUnitService = BusinessUnitService;
            _reportService = ReportService;
            _Generic = generic;
            _itemService = ItemService;
            _customerService = CustomerService;
            _itemGroupService = ItemGroupService;
            _vendorCategoryService = VendorCategoryService;
            _vendorService = VendorService;
            _itemCategoryService = ItemCategoryService;
            _statusService = StatusService;
            _bucketService = BucketService;
            _reasonDeterminationService = ReasonDeterminationService;
            _accountTypesService = AccountTypesService;
            _entityTypeService = EntityTypeService;
            _tdsCodeService = TdsCodeService;
            _employeeService = EmployeeService;
            _storageLocation = StorageLocation;
        }
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult GetSalesSummaryReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date,string plant)
        {
           
            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
           
            ViewBag.datasource = data;
            return PartialView("Partial_Report", data);
        }
        public ActionResult SalesSummaryReport()
        {
            ViewBag.customer_category_list = new SelectList(_customerCategoryService.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesRMService.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territoryService.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult GetSalesheaderlevelReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {

            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_SalesHeaderLevel", data);
        }
        public ActionResult SalesheaderlevelReport()
        {
            ViewBag.customer_category_list = new SelectList(_customerCategoryService.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesRMService.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territoryService.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult GetSalesDetailLevelReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {

            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_SalesDetailLevel", data);
        }
        public ActionResult SalesDetailLevelReport()
        {
            ViewBag.customer_category_list = new SelectList(_customerCategoryService.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesRMService.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territoryService.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }

        public ActionResult GetCustomerContactDetailsReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {

            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_CustomerContactDetails", data);
        }
        public ActionResult CustomerContactDetailsReport()
        {
            ViewBag.customer_category_list = new SelectList(_customerCategoryService.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesRMService.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territoryService.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }

        public ActionResult GetCustomerPaymentMasterListReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {

            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_CustomerPaymentMasterList", data);
        }
        public ActionResult CustomerPaymentMasterListReport()
        {
            ViewBag.customer_category_list = new SelectList(_customerCategoryService.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesRMService.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territoryService.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult GetClientLedgerReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {

            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_ClientLedger", data);
        }
        public ActionResult ClientLedgerReport()
        {
            ViewBag.customer_category_list = new SelectList(_customerCategoryService.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesRMService.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territoryService.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult GetForexSalesReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {

            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_ForexSales", data);
        }
        public ActionResult ForexSalesReport()
        {
            ViewBag.customer_category_list = new SelectList(_customerCategoryService.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesRMService.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territoryService.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult GetSalesQuotationSummaryReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {

            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_SalesQuotationSummary", data);
        }
        public ActionResult SalesQuotationSummaryReport()
        {
            ViewBag.customer_category_list = new SelectList(_customerCategoryService.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesRMService.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territoryService.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult GetSalesQuotationHeaderLevelReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {

            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_SalesQuotationHeaderLevel", data);
        }
        public ActionResult SalesQuotationHeaderLevelReport()
        {
            ViewBag.customer_category_list = new SelectList(_customerCategoryService.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesRMService.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territoryService.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult GetSalesQuotationDetaillevelReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {

            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_SalesQuotationDetaillevel", data);
        }
        public ActionResult SalesQuotationDetaillevelReport()
        {
            ViewBag.customer_category_list = new SelectList(_customerCategoryService.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesRMService.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territoryService.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult GetSalesOrderSummaryReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {

            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_SalesOrderSummary", data);
        }
        public ActionResult SalesOrderSummaryReport()
        {
            ViewBag.customer_category_list = new SelectList(_customerCategoryService.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesRMService.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territoryService.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult GetSalesOrderHeaderLevelReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {

            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_SalesOrderHeaderLevel", data);
        }
        public ActionResult SalesOrderHeaderLevelReport()
        {
            ViewBag.customer_category_list = new SelectList(_customerCategoryService.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesRMService.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territoryService.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult GetSalesOrderDetaillevelReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {

            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_SalesOrderDetaillevel", data);
        }
        public ActionResult SalesOrderDetaillevelReport()
        {
            ViewBag.customer_category_list = new SelectList(_customerCategoryService.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesRMService.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territoryService.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult GetSalesOrderItemWisePlanReport(string item, string customer, DateTime? start_date, DateTime? end_date, string plant)
        {

            //var data = _reportService.SalesSummaryReport(item, customer, start_date, end_date, plant);
            //var list = JsonConvert.SerializeObject(data);
            //ViewBag.datasource = data;
            return PartialView("Partial_SalesOrderItemWisePlan", null);
        }
        public ActionResult SalesOrderItemWisePlanReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.customer_list = new SelectList(_customerService.GetAll(), "CUSTOMER_ID", "CUSTOMER_CODE");
            return View();
        }
        public ActionResult GetItemWiseSalesSummaryReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {

            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_ItemWiseSalesSummary", data);
        }
        public ActionResult ItemWiseSalesSummaryReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            return View();
        }
        public ActionResult GetItemWiseSalesDetailReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {

            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_ItemWiseSalesDetail", data);
        }
        public ActionResult ItemWiseSalesDetailReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.customer_list = new SelectList(_customerService.GetAll(), "CUSTOMER_ID", "CUSTOMER_CODE");
            return View();
        }
        public ActionResult GetSalesOrderQtyTrackerReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {

            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_SalesOrderQtyTracker", data);
        }
        public ActionResult SalesOrderQtyTrackerReport()
        {
            ViewBag.customer_category_list = new SelectList(_customerCategoryService.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesRMService.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territoryService.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult GetCollectionsReceivedReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {

            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_CollectionsReceivedReport", data);
        }
        public ActionResult CollectionsReceivedReport()
        {
            ViewBag.customer_category_list = new SelectList(_customerCategoryService.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesRMService.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territoryService.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult GetCollectionsExpectedReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {

            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_CollectionsExpected", data);
        }
        public ActionResult CollectionsExpectedReport()
        {
            ViewBag.customer_category_list = new SelectList(_customerCategoryService.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesRMService.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territoryService.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult GetCustomerMasterDiscountDetailsReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {

            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_CustomerMasterDiscountDetails", data);
        }
        public ActionResult CustomerMasterDiscountDetailsReport()
        {
            ViewBag.customer_category_list = new SelectList(_customerCategoryService.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesRMService.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territoryService.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult GetSalesPriceListReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {

            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_SalesPriceList", data);
        }
        public ActionResult SalesPriceListReport()
        {
            ViewBag.customer_category_list = new SelectList(_customerCategoryService.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.item_group_list = new SelectList(_itemGroupService.GetAll(), "ITEM_GROUP_ID", "ITEM_GROUP_NAME");
            ViewBag.territory_list = new SelectList(_territoryService.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            return View();
        }
        public ActionResult GetOpenSalesOrderReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {

            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_OpenSalesOrder", data);
        }
        public ActionResult OpenSalesOrderReport()
        {
            ViewBag.customer_category_list = new SelectList(_customerCategoryService.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.sale_rm_list = new SelectList(_salesRMService.GetAll(), "sales_rm_id", "employee_name");
            ViewBag.territory_list = new SelectList(_territoryService.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.generalledger_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult GetVendorContactDetailsReport(string vendor_category_id ,string priority_id)
        {

            //var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            //var list = JsonConvert.SerializeObject(data);
            //ViewBag.datasource = data;
            return PartialView("Partial_VendorContactDetails", null);
        }
        public ActionResult VendorContactDetailsReport()
        {
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.vendor_category_list = new SelectList(_vendorCategoryService.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_ID");
            return View();
        }
        public ActionResult GetVENDORPaymentMasterListReport(string vendor_category_id, string priority_id)
        {

            //var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            //var list = JsonConvert.SerializeObject(data);
            //ViewBag.datasource = data;
            return PartialView("Partial_VENDORPaymentMasterList", null);
        }
        public ActionResult VENDORPaymentMasterListReport()
        {
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.vendor_category_list = new SelectList(_vendorCategoryService.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_ID");
            return View();
        }
        public ActionResult GetVendorLedgerReport(string vendor_code, string vendor_name)
        {

            //var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            //var list = JsonConvert.SerializeObject(data);
            //ViewBag.datasource = data;
            return PartialView("Partial_VendorLedger", null);
        }
        public ActionResult VendorLedgerReport()
        {
            ViewBag.vendor_list_code = new SelectList(_vendorService.GetAll(), "VENDOR_ID", "VENDOR_CODE");
            ViewBag.vendor_list_name = new SelectList(_vendorService.GetAll(), "VENDOR_ID", "VENDOR_NAME");
            return View();
        }

        public ActionResult GetVendorMasterDiscountDetailsReport()
        {

            //var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            //var list = JsonConvert.SerializeObject(data);
            //ViewBag.datasource = data;
            return PartialView("Partial_VendorMasterDiscountDetails", null);
        }
        public ActionResult VendorMasterDiscountDetailsReport()
        {
            return View();
        }
        public ActionResult GetPurchasePriceListReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {
            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_PurchasePriceList", data);
        }
        public ActionResult PurchasePriceListReport()
        {
            ViewBag.customer_category_list = new SelectList(_customerCategoryService.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.item_group_list = new SelectList(_itemGroupService.GetAll(), "ITEM_GROUP_ID", "ITEM_GROUP_NAME");
            ViewBag.territory_list = new SelectList(_territoryService.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            return View();
        }
        public ActionResult GetPurchaseRequisitionSummaryReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {
            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_PurchaseRequisitionSummary", data);
        }
        public ActionResult PurchaseRequisitionSummaryReport()
        {
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.status_list = new SelectList(_statusService.GetAll(), "status_id", "status_name");
            return View();
        }
        public ActionResult GetPurchaseRequisitionDetailedReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {
            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_PurchaseRequisitionDetailed", data);
        }
        public ActionResult PurchaseRequisitionDetailedReport()
        {
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.vendor_category_list = new SelectList(_vendorCategoryService.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_ID");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            return View();
        }
        public ActionResult GetOpenPRReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {
            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_OpenPRReport", data);
        }
        public ActionResult OpenPRReport()
        {
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            return View();
        }
        public ActionResult GetPurchaseOrderSummaryReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {
            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_PurchaseOrderSummary", data);
        }
        public ActionResult PurchaseOrderSummaryReport()
        {
            ViewBag.vendor_category_list = new SelectList(_vendorCategoryService.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_ID");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult GetPurchaseOrderHeaderReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {
            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_PurchaseOrderHeader", data);
        }
        public ActionResult PurchaseOrderHeaderReport()
        {
            ViewBag.vendor_category_list = new SelectList(_vendorCategoryService.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_ID");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.vendor_list_code = new SelectList(_vendorService.GetAll(), "VENDOR_ID", "VENDOR_CODE");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            return View();
        }
        public ActionResult GetPurchaseOrderItemLevelReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {
            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_PurchaseOrderItemLevel", data);
        }
        public ActionResult PurchaseOrderItemLevelReport()
        {
            ViewBag.vendor_category_list = new SelectList(_vendorCategoryService.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_ID");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.vendor_list_code = new SelectList(_vendorService.GetAll(), "VENDOR_ID", "VENDOR_CODE");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            return View();
        }
        public ActionResult GetOpenPOReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {
            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_OpenPOReport", data);
        }
        public ActionResult OpenPOReport()
        {
            ViewBag.vendor_category_list = new SelectList(_vendorCategoryService.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_ID");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.vendor_list_code = new SelectList(_vendorService.GetAll(), "VENDOR_ID", "VENDOR_CODE");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            return View();
        }
        public ActionResult GetGRNSummaryReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {
            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_GRNSummary", data);
        }
        public ActionResult GRNSummaryReport()
        {
            ViewBag.vendor_category_list = new SelectList(_vendorCategoryService.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_ID");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.vendor_list_code = new SelectList(_vendorService.GetAll(), "VENDOR_ID", "VENDOR_CODE");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            return View();
        }
        public ActionResult GetGRNHeaderReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {
            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_GRNHeader", data);
        }
        public ActionResult GRNHeaderReport()
        {
            ViewBag.vendor_category_list = new SelectList(_vendorCategoryService.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_ID");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.vendor_list_code = new SelectList(_vendorService.GetAll(), "VENDOR_ID", "VENDOR_CODE");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            return View();
        }
        public ActionResult GetGRNItemLevelReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {
            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_GRNItemLevel", data);
        }
        public ActionResult GRNItemLevelReport()
        {
            ViewBag.vendor_category_list = new SelectList(_vendorCategoryService.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_ID");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.vendor_list_code = new SelectList(_vendorService.GetAll(), "VENDOR_ID", "VENDOR_CODE");
            return View();
        }
        public ActionResult GetGRNPendingforExciseReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {
            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_GRNPendingforExcise", data);
        }
        public ActionResult GRNPendingforExciseReport()
        {
            ViewBag.vendor_category_list = new SelectList(_vendorCategoryService.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_ID");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.vendor_list_code = new SelectList(_vendorService.GetAll(), "VENDOR_ID", "VENDOR_CODE");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            return View();
        }
        public ActionResult GetGRNPendingforPurchaseInvoiceReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {
            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_GRNPendingforPurchaseInvoice", data);
        }
        public ActionResult GRNPendingforPurchaseInvoiceReport()
        {
            return View();
        }
        public ActionResult GetPurchaseInvoiceSummaryReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {
            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_PurchaseInvoiceSummary", data);
        }
        public ActionResult PurchaseInvoiceSummaryReport()
        {
            ViewBag.vendor_category_list = new SelectList(_vendorCategoryService.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_ID");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            return View();
        }
        public ActionResult GetPurchaseInvoiceHeaderReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {
            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_PurchaseInvoiceHeader", data);
        }
        public ActionResult PurchaseInvoiceHeaderReport()
        {
            ViewBag.vendor_category_list = new SelectList(_vendorCategoryService.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_ID");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.vendor_list_code = new SelectList(_vendorService.GetAll(), "VENDOR_ID", "VENDOR_CODE");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            return View();
        }
        public ActionResult GetPurchaseInvoiceItemLevelReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {
            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_PurchaseInvoiceItemLevel", data);
        }
        public ActionResult PurchaseInvoiceItemLevelReport()
        {
            ViewBag.vendor_category_list = new SelectList(_vendorCategoryService.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_ID");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.vendor_list_code = new SelectList(_vendorService.GetAll(), "VENDOR_ID", "VENDOR_CODE");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            return View();
        }
        public ActionResult GetLDreportReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {
            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_LDreport", data);
        }
        public ActionResult LDReport()
        {
            ViewBag.vendor_category_list = new SelectList(_vendorCategoryService.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_ID");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.vendor_list_code = new SelectList(_vendorService.GetAll(), "VENDOR_ID", "VENDOR_CODE");
            return View();
        }
        public ActionResult GetPurchaseReturnSummaryReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {
            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_PurchaseReturnSummary", data);
        }
        public ActionResult PurchaseReturnSummaryReport()
        {
            ViewBag.vendor_category_list = new SelectList(_vendorCategoryService.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_ID");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            return View();
        }
        public ActionResult GetPurchaseReturnHeaderReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {
            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_PurchaseReturnHeader", data);
        }
        public ActionResult PurchaseReturnHeaderReport()
        {
            ViewBag.vendor_category_list = new SelectList(_vendorCategoryService.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_ID");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.vendor_list_code = new SelectList(_vendorService.GetAll(), "VENDOR_ID", "VENDOR_CODE");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            return View();
        }
        public ActionResult GetPurchaseReturnItemLevelReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {
            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_PurchaseReturnItemLevel", data);
        }
        public ActionResult PurchaseReturnItemLevelReport()
        {
            ViewBag.vendor_category_list = new SelectList(_vendorCategoryService.GetAll(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_ID");
            ViewBag.priority_list = new SelectList(_Generic.GetPriorityByForm(1), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.business_list = new SelectList(_businessUnitService.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.vendor_list_code = new SelectList(_vendorService.GetAll(), "VENDOR_ID", "VENDOR_CODE");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            return View();
        }
        public ActionResult GetPOtoPaymentTrackerReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {
            var data = _reportService.SalesSummaryReport(customer_category_id, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_POtoPaymentTracker", data);
        }
        public ActionResult POtoPaymentTrackerReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            return View();
        }
        public ActionResult GetSalesContributionReport(string cus_category, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date, string plant)
        {
            var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            var list = JsonConvert.SerializeObject(data);
            ViewBag.datasource = data;
            return PartialView("Partial_SalesContribution", data);
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
        public ActionResult GetARAgeingReport(string customer_category, DateTime asofdate, DateTime duedate, DateTime documentdate, DateTime postingdate)
        {
           // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);
            
           // ViewBag.datasource = data;
            return PartialView("Partial_ARAgeing");
        }
        public ActionResult ARAgeingReport()
        {
            
            ViewBag.customer_category_list = new SelectList(_customerCategoryService.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            return View();
        }
        public ActionResult GetAPAgeingReport(string customer_category, DateTime asofdate, DateTime duedate, DateTime documentdate, DateTime postingdate)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_APAgeing");
        }
        public ActionResult APAgeingReport()
        {
            ViewBag.customer_category_list = new SelectList(_customerCategoryService.GetAll(), "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            return View();
        }
        public ActionResult GetItemAccountingReport()
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_ItemAccounting");
        }
        public ActionResult ItemAccountingReport()
        {
            return View();
        }
            public ActionResult InventoryCascadeReport()
        {
            ViewBag.sloc_list = new SelectList(_storageLocation.GetAll(), "storage_location_id", "storage_location_name");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            return View();
        }
        public ActionResult GetInventoryledgerDetailedReport(DateTime from_date, DateTime to_date, DateTime posting_date , string plant, string sloc, string itemcode, string itemcategory, string bucket)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_InventoryledgerDetailed");
        }
        public ActionResult InventoryledgerDetailedReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.sloc_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            return View();
        }
        public ActionResult GetInventoryledgerSummaryReport(DateTime from_date, DateTime to_date)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_InventoryledgerSummary");
        }
        public ActionResult InventoryledgerSummaryReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.sloc_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            return View();
        }
        public ActionResult GetInventoryCostingReport()
        {
            var data = _reportService.InventoryCostingReport();
            ViewBag.datasource = data;
            return PartialView("Partial_InventoryCosting");
        }
        public ActionResult InventoryCostingReport()
        {
            return View();
        }
        public ActionResult GetInventoryValuationReport()
        {
            var data = _reportService.InventoryValuationReport();
            ViewBag.datasource = data;
            return PartialView("Partial_InventoryValuation");
        }
        public ActionResult InventoryValuationReport()
        {
            return View();
        }
        public ActionResult GetGoodsReceiptReport(DateTime from_date, DateTime to_date, DateTime posting_date, string plant, string sloc, string itemcode, string itemcategory, string bucket,string reason)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_GoodsReceipt");
        }
        public ActionResult GoodsReceiptReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.sloc_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            ViewBag.reason_list = new SelectList(_reasonDeterminationService.GetAll(), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            return View();
        }
        public ActionResult GetGoodsIssueReport(DateTime from_date, DateTime to_date, DateTime posting_date, string plant, string sloc, string itemcode, string itemcategory, string bucket, string reason)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_GoodsIssue");
        }
        public ActionResult GoodsIssueReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.sloc_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            ViewBag.reason_list = new SelectList(_reasonDeterminationService.GetAll(), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            return View();
        }
        public ActionResult GetWithinPlanTrasferReport(DateTime from_date, DateTime to_date, DateTime posting_date, string plant, string sloc, string itemcode, string itemcategory, string bucket, string reason)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_WithinPlanTrasfer");
        }
        public ActionResult WithinPlanTrasferReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.sloc_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            ViewBag.reason_list = new SelectList(_reasonDeterminationService.GetAll(), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            return View();
        }
        public ActionResult GetInventoryRevalautionReport(DateTime from_date, DateTime to_date, string plant, string itemcode, string itemcategory)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_InventoryRevalaution");
        }
        public ActionResult InventoryRevalautionReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.sloc_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            ViewBag.reason_list = new SelectList(_reasonDeterminationService.GetAll(), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            return View();
        }
        public ActionResult GetInventoryRateHistoryReport(string itemcode, string itemcategory)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_InventoryRateHistory");
        }
        public ActionResult InventoryRateHistoryReport()
        {
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            return View();
        }
        public ActionResult GetMaterialRequisitionReportStatusDetailedReport(DateTime from_date, DateTime to_date)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_MaterialRequisitionReportStatusDetailed");
        }
        public ActionResult MaterialRequisitionReportStatusDetailedReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.sloc_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            ViewBag.reason_list = new SelectList(_reasonDeterminationService.GetAll(), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            return View();
        }
        public ActionResult GetMaterialRequisitionReportStatusSummaryReport(DateTime from_date, DateTime to_date)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_MaterialRequisitionReportStatusSummary");
        }
        public ActionResult MaterialRequisitionReportStatusSummaryReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.sloc_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            ViewBag.reason_list = new SelectList(_reasonDeterminationService.GetAll(), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            return View();
        }
        public ActionResult GetOpenMaterialRequisitionReport(DateTime report_date, DateTime as_of_date)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_OpenMaterialRequisition");
        }
        public ActionResult OpenMaterialRequisitionReport()
        {
            return View();
        }
        public ActionResult GetQCLotSummaryReport(DateTime from_date, DateTime to_date,string plant,string item_code,string status)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_QCLotSummary");
        }
        public ActionResult QCLotSummaryReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.status_list = new SelectList(_statusService.GetAll(), "status_id", "status_name");
            return View();
        }
        public ActionResult GetQCParametersReport(DateTime from_date, DateTime to_date, string plant, string item_code, string status)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_QCParameters");
        }
        public ActionResult QCParametersReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.status_list = new SelectList(_statusService.GetAll(), "status_id", "status_name");
            return View();
        }
        public ActionResult GetQCUsageDecisionReport(DateTime from_date, DateTime to_date, string plant, string item_code, string status)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_QCUsageDecision");
        }
        public ActionResult QCUsageDecisionReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.status_list = new SelectList(_statusService.GetAll(), "status_id", "status_name");
            return View();
        }
        public ActionResult GetQCLOTShelfLifeReport(DateTime from_date, DateTime to_date, string plant, string item_code, string status)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_QCLOTShelfLife");
        }
        public ActionResult QCLOTShelfLifeReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.status_list = new SelectList(_statusService.GetAll(), "status_id", "status_name");
            return View();
        }
        public ActionResult GetShelfLifeExpiredReport(DateTime asofdate, string plant,string itemcategory, string item_code, string sloc)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_ShelfLifeExpired");
        }
        public ActionResult ShelfLifeExpiredReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.sloc_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            return View();
        }
        public ActionResult GetShelfLifeAbouttoExpireReport(DateTime fromdate, DateTime todate, string plant, string itemcategory, string item_code, string sloc)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_ShelfLifeAbouttoExpire");
        }
        public ActionResult ShelfLifeAbouttoExpireReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.sloc_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            return View();
        }
        public ActionResult GetBatchRevalidatedReport(DateTime fromdate, DateTime todate, string itemcategory, string item_code)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_BatchRevalidated");
        }
        public ActionResult BatchRevalidatedReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            return View();
        }
        public ActionResult GetMATERIAL_CONSUMED_POST_EXPIRYReport(DateTime fromdate, DateTime todate, string itemcategory, string item_code,string reason)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_MATERIAL_CONSUMED_POST_EXPIRY");
        }
        public ActionResult MATERIAL_CONSUMED_POST_EXPIRYReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.reason_list = new SelectList(_reasonDeterminationService.GetAll(), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            return View();
        }
        public ActionResult GetITEM_MASTERQCPARAMETERSReport()
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_ITEM_MASTERQCPARAMETERS");
        }
        public ActionResult ITEM_MASTERQCPARAMETERSReport()
        {
            return View();
        }
        public ActionResult GetBatchRevalidationCountReport()
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_BatchRevalidationCount");
        }
        public ActionResult BatchRevalidationCountReport()
        {
            return View();
        }
        public ActionResult GetLedgerDetailsReport(DateTime fromdate, DateTime todate, string accounttype)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_LedgerDetails");
        }
        public ActionResult LedgerDetailsReport()
        {
            ViewBag.vendor_list = new SelectList(_vendorService.GetVendorList(),"VENDOR_ID", "VENDOR_NAME");
            ViewBag.customer_list = new SelectList(_customerService.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_CODE");
            ViewBag.employee_list = new SelectList(_employeeService.GetEmployeeList(), "employee_id", "employee_code");
            return View();
        }
       
        public ActionResult GetTrialBalanceReport(DateTime fromdate, DateTime todate)
        {
             var data = _reportService.Report_TrialBalance_vm(fromdate, todate);
            ViewBag.datasource = data;
            return PartialView("Partial_TrialBalance",JsonRequestBehavior.AllowGet);
        }
        public ActionResult TrialBalanceReport()
        {
            return View();
        }
        public ActionResult GetGeneralLedgerReport(DateTime fromdate, DateTime todate, string accounttype,string category)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_GeneralLedger");
        }
        public ActionResult GeneralLedgerReport()
        {
            ViewBag.account_type_list = new SelectList(_entityTypeService.GetAll(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");
            return View();
        }
        public ActionResult GetProfitLossAccountReport(DateTime fromdate, DateTime todate, string account)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_ProfitLossAccount");
        }
        public ActionResult ProfitLossAccountReport()
        {
            return View();
        }
        public ActionResult GetBalanceSHeetReport(DateTime fromdate, DateTime todate, string account)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_BalanceSHeet");
        }
        public ActionResult BalanceSHeetReport()
        {
            return View();
        }
        public ActionResult GetProfitLossComparisonReport(DateTime fromdate, DateTime todate, DateTime fromdate1, DateTime todate1, string account)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_ProfitLossComparison");
        }
        public ActionResult ProfitLossComparisonReport()
        {
            return View();
        }
        public ActionResult GetBalanceSHeetComparisonReport(DateTime fromdate, DateTime todate)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_BalanceSHeetComparison");
        }
        public ActionResult BalanceSHeetComparisonReport()
        {
            return View();
        }
        public ActionResult GetBudgetActualReport(DateTime fromdate, DateTime todate)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_BudgetActual");
        }
        public ActionResult BudgetActualReport()
        {
            return View();
        }
        public ActionResult GetCostCenterReport(DateTime fromdate, DateTime todate)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_CostCenter");
        }
        public ActionResult CostCenterReport()
        {
            return View();
        }
        public ActionResult GetJournalListingReport(DateTime fromdate, DateTime todate,int number,string category)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_JournalListing");
        }
        public ActionResult JournalListingReport()
        {
            ViewBag.category_list = new SelectList(_categoryService.GetAll(), "CATEGORY_ID", "CATEGORY_NAME");
            return View();
        }
        public ActionResult GetTDSPayableReport(DateTime fromdate, DateTime todate,  string tdscode)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_TDSPayable");
        }
        public ActionResult TDSPayableReport()
        {
            ViewBag.tds_list = new SelectList(_tdsCodeService.GetAll(), "tds_code_id", "tds_code");
            return View();
        }
        public ActionResult GetTDSReport(DateTime fromdate, DateTime todate, string tdscode)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_TDS");
        }
        public ActionResult GetTDSReport()
        {
            ViewBag.tds_list = new SelectList(_tdsCodeService.GetAll(), "tds_code_id", "tds_code");
            return View();
        }
        public ActionResult GetWIPSummaryReport(DateTime asof, string plant, string machine)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_WIPSummary");
        }
        public ActionResult WIPSummaryReport()
        {
            ViewBag.tds_list = new SelectList(_tdsCodeService.GetAll(), "tds_code_id", "tds_code");
            ViewBag.machine_list = new SelectList(_machineMasterService.GetAll(), "machine_id", "machine_code");
            return View();
        }
        public ActionResult GetWIPReportDetailedReport(DateTime asof, string plant, string machine)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_WIPReportDetailed");
        }
        public ActionResult WIPReportDetailedReport()
        {
            ViewBag.tds_list = new SelectList(_tdsCodeService.GetAll(), "tds_code_id", "tds_code");
            ViewBag.machine_list = new SelectList(_machineMasterService.GetAll(), "machine_id", "machine_code");
            return View();
        }
        public ActionResult GetWIPReportAgeingReportSummaryReport(DateTime asof, string plant, string machine)
        {
            // var data = _reportService.SalesSummaryReport(cus_category, sales_rm_id, territory_id, priority_id, control_account_id, currency_id, business_unit_id, start_date, end_date, plant);

            // ViewBag.datasource = data;
            return PartialView("Partial_WIPReportAgeingReportSummary");
        }
        public ActionResult WIPReportAgeingReportSummaryReport()
        {
            ViewBag.tds_list = new SelectList(_tdsCodeService.GetAll(), "tds_code_id", "tds_code");
            ViewBag.machine_list = new SelectList(_machineMasterService.GetAll(), "machine_id", "machine_code");
            return View();
        }
        public ActionResult GetCustomerLedgerDetailsReport(DateTime fromdate, DateTime todate, string customer)
        {
             var data = _reportService.Report_CustomerLedgerDetails_vm(fromdate, todate, customer);
             ViewBag.datasource = data;
            return PartialView("Partial_CustomerLedgerDetails");
        }
        public ActionResult CustomerLedgerDetailsReport()
        {
            ViewBag.customer_list = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            return View();
        }
        public ActionResult GetVendorLedgerDetailsReport(DateTime fromdate, DateTime todate, string vendor)
        {
            var data = _reportService.Report_VendorLedgerDetails_vm(fromdate, todate, vendor);
            ViewBag.datasource = data;
            return PartialView("Partial_VendorLedgerDetails");
        }
        public ActionResult VendorLedgerDetailsReport()
        {
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            return View();
        }
        public ActionResult GetEmployeeLedgerDetailsReport(DateTime fromdate, DateTime todate, string employee)
        {
            var data = _reportService.Report_EmployeeLedgerDetails_vm(fromdate, todate, employee);
            ViewBag.datasource = data;
            return PartialView("Partial_EmployeeLedgerDetails");
        }
        public ActionResult EmployeeLedgerDetailsReport()
        {
            ViewBag.employee_list = new SelectList(_Generic.GetEmployeeCode(), "employee_id", "employee_code");
            return View();
        }
        public ActionResult GetGeneralLedgerDetailsReport(DateTime fromdate, DateTime todate, string gl_account)
        {
            var data = _reportService.Report_GeneralLedgerDetails_vm(fromdate, todate, gl_account);
            ViewBag.datasource = data;
            return PartialView("Partial_GeneralLedgerDetails");
        }
        public ActionResult GeneralLedgerDetailsReport()
        {
            ViewBag.gl_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            return View();
        }
    }
}