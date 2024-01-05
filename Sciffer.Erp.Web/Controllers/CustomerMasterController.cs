using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.ViewModel;
using System;
using Syncfusion.JavaScript.Models;
using Syncfusion.JavaScript;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using Excel;
using System.IO;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class CustomerMasterController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ICurrencyService _currencyService;
        private readonly ICustomerCategoryService _customercategoryService;
        private readonly ICustomerParentService _customerParetntService;
        private readonly IFreightTermsService _freightTermsService;
        private readonly IOrgTypeService _orgService;
        private readonly IPaymentCycleTypeService _paymentCycleType;
        private readonly IPaymentTermsService _paymentTerms;
        private readonly IPaymentCycleService _paymentService;
        private readonly IStateService _stateService;
        private readonly ITerritoryService _territory;
        private readonly IPriorityService _priority;
        private readonly IUserService _userService;
        private readonly IGeneralLedgerService _GeneralledgerService;
        private readonly ICountryService _CustomerService;
        private readonly IGenericService _Generic;
        private readonly IItemCategoryService _itemCategoryService;
        private readonly IItemGroupService _itemGroupService;
        private readonly ITdsCodeService _tdsCodeService;
        string[] error = new string[30000];
        string errorMessage = "";
        int errorList = 0;
        string Message = "";
        public CustomerMasterController(IItemCategoryService ItemCategoryService,ICustomerService customerService,ICurrencyService currencyService, ICustomerCategoryService customercategoryService, 
            ICustomerParentService customerParetntService, IFreightTermsService freightTermsService, IOrgTypeService orgService, IPaymentCycleTypeService paymentCycleType,
           IPaymentTermsService paymentTerms, IPaymentCycleService paymentService, IStateService stateService,ITerritoryService territory, IPriorityService priority, ITdsCodeService TdsCodeService,
           IUserService userService, IGeneralLedgerService GeneralledgerService,ICountryService CustomerService, IGenericService genericService, IItemGroupService ItemGroupService)
        {
            _Generic = genericService;
            _customerService = customerService;
            _currencyService = currencyService;
            _customercategoryService = customercategoryService;
            _customerParetntService = customerParetntService;
            _freightTermsService = freightTermsService;
            _orgService = orgService;
            _paymentCycleType = paymentCycleType;
            _paymentService = paymentService;
            _paymentTerms = paymentTerms;
            _stateService = stateService;
            _territory = territory;
            _priority = priority;
            _userService = userService;
            _GeneralledgerService = GeneralledgerService;
            _CustomerService = CustomerService;
            _itemCategoryService = ItemCategoryService;
            _itemGroupService = ItemGroupService;
            _tdsCodeService = TdsCodeService;
        }

        //get partial view for customer contact
        public PartialViewResult GetPartial(int count = 0)
        {
            ViewBag.Count = count;
            return PartialView("_ContactView");
        }
       
        //get partial view for ATTRIBUTE
        public PartialViewResult GetPartialAttribute(int count1 = 0)
        {
            ViewBag.Count1 = count1;
            return PartialView("_AttributeView");
        }

        [CustomAuthorizeAttribute("CUSM")]
        public ActionResult Index()
        {
            ViewBag.doc = TempData["saved"];
            ViewBag.customerlist = _customerService.GetCustomerList();
            return View();
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            REF_CUSTOMER_VM customerentity = _customerService.Get((int)id);
            //  customerentity.ATTRIBUTE_VM = 
            var currencylist = _currencyService.GetAll();
            var customercategorylist = _customercategoryService.GetAll();
            var customerParentList = _customerParetntService.GetAll();
            var freightterms = _freightTermsService.GetAll();
            var org = _orgService.GetAll();
            var paymentcycletype = _paymentCycleType.GetAll();
            var paymentterms = _Generic.GetPaymentTermsList();
            var paymentcycle = _paymentService.GetAll();
            var statelist = _stateService.GetAll();
            var territory = _territory.GetAll();
            var priority = _Generic.GetPriorityByForm(1);
            var user = _userService.GetAll();
            var country = _CustomerService.GetAll();
            //ViewBag.generalleder = _Generic.GetLedgerAccount(2);
            var gl = _Generic.GetLedgerAccount(2);
            ViewBag.generalleder = new SelectList(gl, "gl_ledger_id", "gl_ledger_name");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.item_category = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.item_group = new SelectList(_itemGroupService.GetAll(), "ITEM_GROUP_ID", "ITEM_GROUP_NAME");
            ViewBag.tds_list = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.categorylist = new SelectList(customercategorylist, "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            AutocompleteProperties auto = new AutocompleteProperties();
            auto.DataSource = new SelectList(customercategorylist, "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            auto.FilterType = FilterOperatorType.Contains;
            ViewBag.categorylist = new SelectList(customercategorylist, "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            //ViewData["auto"] = auto;
            ViewBag.datasource = _CustomerService.GetAll();
            ViewBag.parentlist = new SelectList(customerParentList, "CUSTOMER_PARENT_ID", "customer_parent_code_name");
            ViewBag.freightlist = new SelectList(freightterms, "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.orglist = new SelectList(org, "ORG_TYPE_ID", "ORG_TYPE_NAME");
            ViewBag.paymentcyclelist = new SelectList(paymentcycle, "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.paymentcycletype = new SelectList(paymentcycletype, "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.paymenttermslist = new SelectList(paymentterms, "payment_terms_id", "payment_terms_description");
            ViewBag.prioritylist = new SelectList(priority, "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.billingstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.corrstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.territorylist = new SelectList(territory, "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.salesexlist = new SelectList(_Generic.GetSalesRM(), "employee_id", "employee_name");
            ViewBag.createdbylist = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.billinglist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.corlist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.bank_list = new SelectList(_Generic.GetBankforSearchDropdown(), "bank_id", "bank_code");
            ViewBag.GST_CustomerType_List = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.GST_TDS_Code = new SelectList(_Generic.GetGSTTDSCodeList(), "gst_tds_code_id", "gst_tds_code");
            return View(customerentity);
        }

        // GET: Customer/Create
        [CustomAuthorizeAttribute("CUSM")]
        public ActionResult Create()
        {
            ViewBag.error = "";
            var currencylist = _currencyService.GetAll();
            var customercategorylist = _customercategoryService.GetAll();
            var customerParentList = _customerParetntService.GetAll();
            var freightterms = _freightTermsService.GetAll();
            var org = _orgService.GetAll();
            var paymentcycletype = _paymentCycleType.GetAll();
            var paymentterms = _Generic.GetPaymentTermsList();
            var paymentcycle = _paymentService.GetAll();
            var statelist = _stateService.GetAll();
            var territory = _territory.GetAll();
            var priority = _Generic.GetPriorityByForm(1);
            var user = _userService.GetAll();
            var country = _CustomerService.GetAll();
            var gl = _Generic.GetLedgerAccount(2);
            ViewBag.generalleder = new SelectList(gl, "gl_ledger_id", "gl_ledger_name");
            //ViewBag.generalleder = _Generic.GetLedgerAccount(2);
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.item_category = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.item_group = new SelectList(_itemGroupService.GetAll(), "ITEM_GROUP_ID", "ITEM_GROUP_NAME");
            ViewBag.tds_list = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.categorylist = new SelectList(customercategorylist, "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            AutocompleteProperties auto = new AutocompleteProperties();
            auto.DataSource = new SelectList(customercategorylist, "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            auto.FilterType = FilterOperatorType.Contains;
            ViewBag.categorylist = new SelectList(customercategorylist, "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            //ViewData["auto"] = auto;
            ViewBag.datasource = _CustomerService.GetAll();
            ViewBag.parentlist = new SelectList(customerParentList, "CUSTOMER_PARENT_ID", "customer_parent_code_name");
            ViewBag.freightlist = new SelectList(freightterms, "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");           
            ViewBag.orglist = new SelectList(org, "ORG_TYPE_ID", "ORG_TYPE_NAME");
            ViewBag.paymentcyclelist = new SelectList(paymentcycle, "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.paymentcycletype = new SelectList(paymentcycletype, "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.paymenttermslist = new SelectList(paymentterms, "payment_terms_id", "payment_terms_description");
            ViewBag.prioritylist = new SelectList(priority, "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.billingstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.corrstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.territorylist = new SelectList(territory, "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.salesexlist = new SelectList(_Generic.GetSalesRM(), "employee_id", "employee_name");
            ViewBag.createdbylist = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.billinglist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.corlist=new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.bank_list = new SelectList(_Generic.GetBankforSearchDropdown(), "bank_id", "bank_code");
            ViewBag.GST_CustomerType_List = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.GST_TDS_Code = new SelectList(_Generic.GetGSTTDSCodeList(), "gst_tds_code_id", "gst_tds_code");
            return View();

        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(REF_CUSTOMER_VM rEF_CUSTOMER, FormCollection fc)
        {
            rEF_CUSTOMER.CREATED_ON = DateTime.Now;
            var issaved = "";
            if (ModelState.IsValid)
            {
                issaved = _customerService.Add(rEF_CUSTOMER);
                if(issaved=="Saved")
                {
                    TempData["saved"] = "Saved Successfully.";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = issaved;
            var currencylist = _currencyService.GetAll();
            var customercategorylist = _customercategoryService.GetAll();
            var customerParentList = _customerParetntService.GetAll();
            var freightterms = _freightTermsService.GetAll();
            var org = _orgService.GetAll();
            var paymentcycletype = _paymentCycleType.GetAll();
            var paymentterms = _Generic.GetPaymentTermsList();
            var paymentcycle = _paymentService.GetAll();
            var statelist = _stateService.GetAll();
            var territory = _territory.GetAll();
            var priority = _Generic.GetPriorityByForm(1);
            var user = _userService.GetAll();
            var gl = _Generic.GetLedgerAccount(2);
            ViewBag.generalleder = new SelectList(gl, "gl_ledger_id", "gl_ledger_name");
            //ViewBag.generalleder = _Generic.GetLedgerAccount(2);
            var country = _CustomerService.GetAll();
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.item_category = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.item_group = new SelectList(_itemGroupService.GetAll(), "ITEM_GROUP_ID", "ITEM_GROUP_NAME");
            ViewBag.tds_list = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.categorylist = new SelectList(customercategorylist, "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.parentlist = new SelectList(customerParentList, "CUSTOMER_PARENT_ID", "customer_parent_code_name");
            ViewBag.freightlist = new SelectList(freightterms, "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");          
            ViewBag.orglist = new SelectList(org, "ORG_TYPE_ID", "ORG_TYPE_NAME");
            ViewBag.paymentcyclelist = new SelectList(paymentcycle, "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.paymentcycletype = new SelectList(paymentcycletype, "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.paymenttermslist = new SelectList(paymentterms, "payment_terms_id", "payment_terms_description");
            ViewBag.prioritylist = new SelectList(priority, "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.billingstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.corrstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.territorylist = new SelectList(territory, "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.salesexlist = new SelectList(_Generic.GetSalesRM(), "employee_id", "employee_name");
            ViewBag.createdbylist = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.billinglist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.corlist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.bank_list = new SelectList(_Generic.GetBankforSearchDropdown(), "bank_id", "bank_code");
            ViewBag.GST_CustomerType_List = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.GST_TDS_Code = new SelectList(_Generic.GetGSTTDSCodeList(), "gst_tds_code_id", "gst_tds_code");
            return View();
        }

        // GET: Customer/Edit/5
        [CustomAuthorizeAttribute("CUSM")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.error = "";
            var customerViewModel = _customerService.Get((int)id);
            var currencylist = _currencyService.GetAll();
            var customercategorylist = _customercategoryService.GetAll();
            var customerParentList = _customerParetntService.GetAll();
            var freightterms = _freightTermsService.GetAll();
            var org = _orgService.GetAll();
            var paymentcycletype = _paymentCycleType.GetAll();
            var paymentterms = _Generic.GetPaymentTermsList();
            var paymentcycle = _paymentService.GetAll();
            var statelist = _stateService.GetAll();
            var territory = _territory.GetAll();
            var priority = _Generic.GetPriorityByForm(1);
            var user = _userService.GetAll();
            var gl = _Generic.GetLedgerAccount(2);
            ViewBag.generalleder = new SelectList(gl, "gl_ledger_id", "gl_ledger_name");        
            var country = _CustomerService.GetAll();
            ViewBag.item_category = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.item_group = new SelectList(_itemGroupService.GetAll(), "ITEM_GROUP_ID", "ITEM_GROUP_NAME");
            ViewBag.tds_list = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(customercategorylist, "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.parentlist = new SelectList(customerParentList, "CUSTOMER_PARENT_ID", "customer_parent_code_name");
            ViewBag.freightlist = new SelectList(freightterms, "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");           
            ViewBag.orglist = new SelectList(org, "ORG_TYPE_ID", "ORG_TYPE_NAME");
            ViewBag.paymentcyclelist = new SelectList(paymentcycle, "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.paymentcycletype = new SelectList(paymentcycletype, "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.paymenttermslist = new SelectList(paymentterms, "payment_terms_id", "payment_terms_description");
            ViewBag.prioritylist = new SelectList(priority, "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.billingstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.corrstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.territorylist = new SelectList(territory, "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.salesexlist = new SelectList(_Generic.GetSalesRM(), "employee_id", "employee_name");
            ViewBag.createdbylist = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.billinglist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.corlist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.bank_list = new SelectList(_Generic.GetBankforSearchDropdown(), "bank_id", "bank_code");
            ViewBag.GST_CustomerType_List = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.GST_TDS_Code = new SelectList(_Generic.GetGSTTDSCodeList(), "gst_tds_code_id", "gst_tds_code");
            return View(customerViewModel);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(REF_CUSTOMER_VM rEF_CUSTOMER)
        {
            var issaved = "";
            if (ModelState.IsValid)
            {
                issaved = _customerService.Add(rEF_CUSTOMER);
                if (issaved == "Saved")
                {
                    TempData["saved"] = "Saved Successfully.";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = issaved;
            var customerViewModel = _customerService.Get((int)rEF_CUSTOMER.CUSTOMER_ID);
            var currencylist = _currencyService.GetAll();
            var customercategorylist = _customercategoryService.GetAll();
            var customerParentList = _customerParetntService.GetAll();
            var freightterms = _freightTermsService.GetAll();
            var org = _orgService.GetAll();
            var paymentcycletype = _paymentCycleType.GetAll();
            var paymentterms = _Generic.GetPaymentTermsList();
            var paymentcycle = _paymentService.GetAll();
            var statelist = _stateService.GetAll();
            var territory = _territory.GetAll();
            var priority = _Generic.GetPriorityByForm(1);
            var user = _userService.GetAll();
            var country = _CustomerService.GetAll();
            var gl = _Generic.GetLedgerAccount(2);
            ViewBag.generalleder = new SelectList(gl, "gl_ledger_id", "gl_ledger_name");
            ViewBag.item_category = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.item_group = new SelectList(_itemGroupService.GetAll(), "ITEM_GROUP_ID", "ITEM_GROUP_NAME");
            ViewBag.tds_list = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(customercategorylist, "CUSTOMER_CATEGORY_ID", "CUSTOMER_CATEGORY_NAME");
            ViewBag.parentlist = new SelectList(customerParentList, "CUSTOMER_PARENT_ID", "customer_parent_code_name");
            ViewBag.freightlist = new SelectList(freightterms, "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.orglist = new SelectList(org, "ORG_TYPE_ID", "ORG_TYPE_NAME");
            ViewBag.paymentcyclelist = new SelectList(paymentcycle, "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.paymentcycletype = new SelectList(paymentcycletype, "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.paymenttermslist = new SelectList(paymentterms, "payment_terms_id", "payment_terms_description");
            ViewBag.prioritylist = new SelectList(priority, "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.billingstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.corrstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.territorylist = new SelectList(territory, "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.salesexlist = new SelectList(_Generic.GetSalesRM(), "employee_id", "employee_name");
            ViewBag.createdbylist = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.billinglist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.corlist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.bank_list = new SelectList(_Generic.GetBankforSearchDropdown(), "bank_id", "bank_code");
            ViewBag.GST_CustomerType_List = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.GST_TDS_Code = new SelectList(_Generic.GetGSTTDSCodeList(), "gst_tds_code_id", "gst_tds_code");
            return View(customerViewModel);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_CUSTOMER = _customerService.Get((int)id);
            if (rEF_CUSTOMER == null)
            {
                return HttpNotFound();
            }
            return View(rEF_CUSTOMER);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var isdelete = _customerService.Delete(id);
            if(isdelete)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _customerService.Dispose();
            }
            base.Dispose(disposing);
        }


        [HttpPost]
        public ActionResult UploadFiles()
        {
            for (int m = 0; m < Request.Files.Count; m++)
            {
                HttpPostedFileBase file = Request.Files[m];
                if (file.ContentLength > 0)
                {

                    string extension = System.IO.Path.GetExtension(file.FileName);
                    string path1 = string.Format("{0}/{1}", Server.MapPath("~/Uploads"), file.FileName);
                    if (System.IO.File.Exists(path1))
                        System.IO.File.Delete(path1);

                    file.SaveAs(path1);
                    FileStream stream = System.IO.File.Open(path1, FileMode.Open, FileAccess.Read);
                    IExcelDataReader excelReader;
                    if (extension == ".xls")
                    {
                        excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else
                    {
                        excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }

                    excelReader.IsFirstRowAsColumnNames = true;
                    System.Data.DataSet result = excelReader.AsDataSet();
                    int contcol = 0, custcol = 0, glcol = 0, procol = 0;
                    string uploadtype = Request.Params[0];
                    List<customer_excel> customer_excel = new List<customer_excel>();
                    List<contact_excel> contact_excel = new List<contact_excel>();
                    List<item_excel> item_excel = new List<item_excel>();
                    List<gl_excel> gl_excel = new List<gl_excel>();
                    List<duplicateglexcle> duplicateglexcle = new List<duplicateglexcle>();
                    if (result.Tables.Count == 0)
                    {
                        errorList++;
                        error[error.Length - 1] = "File is Empty!";
                        errorMessage = "File is Empty!";
                    }
                    else
                    {
                        foreach (DataTable sheet in result.Tables)
                        {
                            if (sheet.TableName == "CustomerDetails")
                            {
                                string[] CustomerColumnArray = new string[sheet.Columns.Count];
                                var TempSheetColumn = from DataColumn Tempcolumn in sheet.Columns select Tempcolumn;
                                foreach (var ary1 in TempSheetColumn)
                                {
                                    CustomerColumnArray[custcol] = ary1.ToString();
                                    custcol = custcol + 1;
                                }

                                if (sheet != null)
                                {
                                    var TempSheetRows = from DataRow TempRow in sheet.Rows select TempRow;
                                    foreach (var ary in TempSheetRows)
                                    {
                                        var a = ary.ItemArray;
                                        if (ValidateCustomerExcelColumns(CustomerColumnArray) == true)
                                        {
                                            string sr_no = a[Array.IndexOf(CustomerColumnArray, "SrNo")].ToString();
                                            if (sr_no != "")
                                            {
                                                if (customer_excel.Count != 0)
                                                {
                                                    customer_excel IDVM = new customer_excel();
                                                    var customer_category = a[Array.IndexOf(CustomerColumnArray, "CustomerCategory")].ToString();
                                                    if (customer_category == "")
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = customer_category.ToString();
                                                        errorMessage = "Add customer category.";
                                                    }
                                                    else
                                                    {
                                                        var customer_category_id = _Generic.GetCustomerCategoryId(customer_category);
                                                        if (customer_category_id == 0)
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = customer_category.ToString();
                                                            errorMessage = customer_category + " not found.";
                                                        }
                                                        else
                                                        {
                                                            IDVM.CUSTOMER_CATEGORY_ID = customer_category_id;
                                                        }
                                                    }
                                                    var customer_code = a[Array.IndexOf(CustomerColumnArray, "CustomerCode")].ToString();
                                                    var custDBCode = _Generic.GetCustomerId(customer_code);
                                                    if (custDBCode == 0)
                                                    {
                                                        var customerCode = customer_excel.Where(x => x.CUSTOMER_CODE == customer_code).FirstOrDefault();
                                                        if (customerCode == null)
                                                        {
                                                            IDVM.CUSTOMER_CODE = customer_code;
                                                            IDVM.CUSTOMER_NAME = a[Array.IndexOf(CustomerColumnArray, "CustomerName")].ToString();

                                                            var customer_display_name = a[Array.IndexOf(CustomerColumnArray, "CustomerDisplayName")].ToString();
                                                            if (customer_display_name == "")
                                                            {
                                                                IDVM.CUSTOMER_DISPLAY_NAME = IDVM.CUSTOMER_NAME;
                                                            }
                                                            else
                                                            {
                                                                IDVM.CUSTOMER_DISPLAY_NAME = customer_display_name;
                                                            }
                                                            var org_type = a[Array.IndexOf(CustomerColumnArray, "OrganizationType")].ToString();
                                                            if (org_type == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = org_type.ToString();
                                                                errorMessage = "Add Org Type.";
                                                            }
                                                            else
                                                            {
                                                                var org_type_id = _Generic.GetOrgId(org_type);
                                                                if (org_type_id == 0)
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = org_type.ToString();
                                                                    errorMessage = org_type + " not found.";
                                                                }
                                                                else
                                                                {
                                                                    IDVM.ORG_TYPE_ID = org_type_id;
                                                                }
                                                            }
                                                            var billing_add = a[Array.IndexOf(CustomerColumnArray, "BillingAddress")].ToString();
                                                            if (billing_add == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = billing_add.ToString();
                                                                errorMessage = "Add billing Address.";
                                                            }
                                                            else
                                                            {
                                                                IDVM.BILLING_ADDRESS = billing_add;
                                                            }
                                                            var billing_city = a[Array.IndexOf(CustomerColumnArray, "BillingCity")].ToString();
                                                            if (billing_city == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = billing_city.ToString();
                                                                errorMessage = "Add billing City.";
                                                            }
                                                            else
                                                            {
                                                                IDVM.BILLING_CITY = billing_city;
                                                            }
                                                            var billing_pincode = a[Array.IndexOf(CustomerColumnArray, "BillingPinCode")].ToString();
                                                            if (billing_pincode == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = billing_pincode.ToString();
                                                                errorMessage = "Add Pincode.";
                                                            }
                                                            else
                                                            {
                                                                IDVM.BILLING_PINCODE = int.Parse(billing_pincode);
                                                            }
                                                            var state = a[Array.IndexOf(CustomerColumnArray, "BillingState")].ToString();
                                                            if (state == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = state.ToString();
                                                                errorMessage = "Add billing State.";
                                                            }
                                                            else
                                                            {
                                                                var billing_state_id = _Generic.GetStateID(state);
                                                                if (billing_state_id == 0)
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = state.ToString();
                                                                    errorMessage = state + " not found.";
                                                                }
                                                                else
                                                                {
                                                                    IDVM.BILLING_STATE_ID = billing_state_id;
                                                                }
                                                            }
                                                            var salesRM = a[Array.IndexOf(CustomerColumnArray, "SalesRM")].ToString();
                                                            if (salesRM == "")
                                                            {
                                                                IDVM.SALES_EXEC_ID = 0;
                                                            }
                                                            else
                                                            {
                                                                var salesRMid = _Generic.GetSalesRMID(salesRM);
                                                                if (salesRMid == 0)
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = salesRM.ToString();
                                                                    errorMessage = salesRM + " not found.";
                                                                }
                                                                else
                                                                {
                                                                    IDVM.SALES_EXEC_ID = salesRMid;
                                                                }
                                                            }
                                                            var freightTerms = a[Array.IndexOf(CustomerColumnArray, "FreightTerms")].ToString();
                                                            if (freightTerms == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = freightTerms.ToString();
                                                                errorMessage = "Add Freight Terms.";
                                                            }
                                                            else
                                                            {
                                                                var freight_term_id = _Generic.GetFreightId(freightTerms);
                                                                if (freight_term_id == 0)
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = freightTerms.ToString();
                                                                    errorMessage = freightTerms + " not Found.";
                                                                }
                                                                else
                                                                {
                                                                    IDVM.FREIGHT_TERMS_ID = freight_term_id;
                                                                }
                                                            }
                                                            var linkToParent = a[Array.IndexOf(CustomerColumnArray, "LinktoParent")].ToString();
                                                            if (linkToParent.ToLower() == "yes")
                                                            {
                                                                IDVM.HAS_PARENT = true;
                                                                var customer_parent_name = a[Array.IndexOf(CustomerColumnArray, "ParentName")].ToString();
                                                                if (customer_parent_name == "")
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = customer_parent_name;
                                                                    errorMessage = "Add only yes or no in customer sheet.";
                                                                }
                                                                else
                                                                {
                                                                    IDVM.CUSTOMER_PARENT_ID = _Generic.GetCustomerParentId(customer_parent_name);
                                                                }
                                                            }
                                                            else if (linkToParent.ToLower() == "no" || linkToParent == "")
                                                            {
                                                                IDVM.HAS_PARENT = false;
                                                            }
                                                            else
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = IDVM.HAS_PARENT.ToString();
                                                                errorMessage = "Add only yes or no in customer sheet.";
                                                            }
                                                            var territory = a[Array.IndexOf(CustomerColumnArray, "Territory")].ToString();
                                                            if (territory == "")
                                                            {
                                                                IDVM.TERRITORY_ID = 0;
                                                            }
                                                            else
                                                            {
                                                                var territory_id = _Generic.GetTerritoryId(territory);
                                                                if (territory_id == 0)
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = territory.ToString();
                                                                    errorMessage = territory + " not found.";
                                                                }
                                                                else
                                                                {
                                                                    IDVM.TERRITORY_ID = territory_id;
                                                                }
                                                            }

                                                            var priority = a[Array.IndexOf(CustomerColumnArray, "Priority")].ToString();
                                                            if (priority == "")
                                                            {
                                                                IDVM.PRIORITY_ID = 0;
                                                            }
                                                            else
                                                            {
                                                                var priority_id = _Generic.GetPriorityId(priority);
                                                                if (priority_id == 0)
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = priority.ToString();
                                                                    errorMessage = priority + " not found.";
                                                                }
                                                                else
                                                                {
                                                                    IDVM.PRIORITY_ID = priority_id;
                                                                }
                                                            }
                                                            var WhetherSameAsBilling = a[Array.IndexOf(CustomerColumnArray, "WhetherSameAsBilling")].ToString();
                                                            if (WhetherSameAsBilling.ToLower() == "no")
                                                            {
                                                                IDVM.CORR_ADDRESS = a[Array.IndexOf(CustomerColumnArray, "CorrespondenceAddress")].ToString();
                                                                IDVM.CORR_CITY = a[Array.IndexOf(CustomerColumnArray, "CorrespondenceCity")].ToString();
                                                                var corr_state = a[Array.IndexOf(CustomerColumnArray, "CorrespondenceState")].ToString();
                                                                IDVM.CORR_STATE_ID = _Generic.GetStateID(corr_state);
                                                                var corr_pincode = a[Array.IndexOf(CustomerColumnArray, "CorrespondencePinCode")].ToString();
                                                                if (corr_pincode == "")
                                                                {
                                                                    IDVM.CORR_PINCODE = 0;
                                                                }
                                                                else
                                                                {
                                                                    IDVM.CORR_PINCODE = int.Parse(corr_pincode);
                                                                }

                                                            }
                                                            else if (WhetherSameAsBilling.ToLower() == "yes" || WhetherSameAsBilling == "")
                                                            {
                                                                IDVM.CORR_ADDRESS = IDVM.BILLING_ADDRESS;
                                                                IDVM.CORR_CITY = IDVM.BILLING_CITY;
                                                                IDVM.CORR_STATE_ID = IDVM.BILLING_STATE_ID;
                                                                IDVM.CORR_PINCODE = IDVM.BILLING_PINCODE;
                                                            }
                                                            else
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = WhetherSameAsBilling.ToString();
                                                                errorMessage = "Add only yes or no WhetherSameAsBilling.";
                                                            }

                                                            IDVM.EMAIL_ID_PRIMARY = a[Array.IndexOf(CustomerColumnArray, "EmailID")].ToString();
                                                            IDVM.std_code = a[Array.IndexOf(CustomerColumnArray, "StdCode")].ToString();
                                                            IDVM.TELEPHONE_PRIMARY = a[Array.IndexOf(CustomerColumnArray, "Phone")].ToString();
                                                            IDVM.WEBSITE_ADDRESS = a[Array.IndexOf(CustomerColumnArray, "WebsiteAddress")].ToString();
                                                            IDVM.FAX = a[Array.IndexOf(CustomerColumnArray, "Fax")].ToString();

                                                            var currency = a[Array.IndexOf(CustomerColumnArray, "DefaultCurrency")].ToString();
                                                            if (currency == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = currency.ToString();
                                                                errorMessage = "Add Currency.";
                                                            }
                                                            else
                                                            {
                                                                var currency_id = _Generic.GetCurrencyId(currency);
                                                                if (currency_id == 0)
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = currency.ToString();
                                                                    errorMessage = currency + " not found.";
                                                                }
                                                                else
                                                                {
                                                                    IDVM.CREDIT_LIMIT_CURRENCY_ID = currency_id;
                                                                }
                                                            }
                                                            var paymentTerms = a[Array.IndexOf(CustomerColumnArray, "PaymentTerms")].ToString();
                                                            if (paymentTerms == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = paymentTerms.ToString();
                                                                errorMessage = "Add Payment terms.";
                                                            }
                                                            else
                                                            {
                                                                var payment_terms_id = _Generic.GetPaymentTermId(paymentTerms);
                                                                if (payment_terms_id == 0)
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = paymentTerms.ToString();
                                                                    errorMessage = paymentTerms + " not found.";
                                                                }
                                                                else
                                                                {
                                                                    IDVM.PAYMENT_TERMS_ID = payment_terms_id;
                                                                }
                                                            }
                                                            var paymentCycleType = a[Array.IndexOf(CustomerColumnArray, "PaymentCycleType")].ToString();
                                                            if (paymentCycleType == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = paymentCycleType.ToString();
                                                                errorMessage = "Add Payment Cycle Type! .";
                                                            }
                                                            else
                                                            {
                                                                var payment_cycle_type_id = _Generic.GetPaymentCycleTypeId(paymentCycleType);
                                                                if (payment_cycle_type_id == 0)
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = paymentCycleType.ToString();
                                                                    errorMessage = paymentCycleType + " not find! .";
                                                                }
                                                                else
                                                                {
                                                                    IDVM.PAYMENT_CYCLE_TYPE_ID = payment_cycle_type_id;
                                                                    var payment_cycle = _Generic.GetPaymentCycle(IDVM.PAYMENT_CYCLE_TYPE_ID);
                                                                    var payment_cycle_data = a[Array.IndexOf(CustomerColumnArray, "PaymentCycle")].ToString();
                                                                    if (payment_cycle_data == "")
                                                                    {
                                                                        errorList++;
                                                                        error[error.Length - 1] = payment_cycle_data.ToString();
                                                                        errorMessage = "Add Payment Cycle Type.";
                                                                    }
                                                                    else
                                                                    {
                                                                        var payment_cycle_data_id = payment_cycle.Where(x => x.PAYMENT_CYCLE_NAME.ToLower() == payment_cycle_data.ToLower()).FirstOrDefault();
                                                                        if (payment_cycle_data_id != null)
                                                                        {
                                                                            IDVM.PAYMENT_CYCLE_ID = payment_cycle_data_id.PAYMENT_CYCLE_ID;
                                                                        }
                                                                        else
                                                                        {
                                                                            errorList++;
                                                                            error[error.Length - 1] = payment_cycle_data.ToString();
                                                                            errorMessage = payment_cycle_data + " not find! .";
                                                                        }
                                                                    }

                                                                }
                                                            }
                                                            var credit_limit = a[Array.IndexOf(CustomerColumnArray, "CreditLimit")].ToString();
                                                            if (credit_limit == "")
                                                            {
                                                                IDVM.CREDIT_LIMIT = 0;
                                                            }
                                                            else
                                                            {
                                                                IDVM.CREDIT_LIMIT = Double.Parse(credit_limit);
                                                            }
                                                            var tds_applicable = a[Array.IndexOf(CustomerColumnArray, "TDSApplicable")].ToString();
                                                            if (tds_applicable.ToLower() == "yes")
                                                            {
                                                                IDVM.TDS_APPLICABLE = true;
                                                                var tds_code = a[Array.IndexOf(CustomerColumnArray, "TDSSectionCode")].ToString();
                                                                if (tds_code == "")
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = tds_code;
                                                                    errorMessage = "TDS code is Blank!!";
                                                                }
                                                                else
                                                                {
                                                                    var tds_id = _Generic.GetTdsCodeId(tds_code);
                                                                    IDVM.tds_id = tds_id;
                                                                }

                                                            }
                                                            else if (tds_applicable.ToLower() == "no" || tds_applicable == "")
                                                            {
                                                                IDVM.TDS_APPLICABLE = false;
                                                                IDVM.tds_id = 0;
                                                            }
                                                            else
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = tds_applicable;
                                                                errorMessage = "Added yes or no only!";
                                                            }
                                                            var pan_no = a[Array.IndexOf(CustomerColumnArray, "PANNo")].ToString();
                                                            if (pan_no == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = pan_no.ToString();
                                                                errorMessage = "Add Pan No! .";
                                                            }
                                                            else
                                                            {
                                                                IDVM.pan_no = pan_no;
                                                            }
                                                            IDVM.vat_tin_no = a[Array.IndexOf(CustomerColumnArray, "VATTINNo")].ToString();
                                                            IDVM.cst_tin_no = a[Array.IndexOf(CustomerColumnArray, "CSTTINNo")].ToString();
                                                            IDVM.service_tax_no = a[Array.IndexOf(CustomerColumnArray, "ServiceTaxNo")].ToString();
                                                            IDVM.gst_no = a[Array.IndexOf(CustomerColumnArray, "GSTNo")].ToString();
                                                            IDVM.ecc_no = a[Array.IndexOf(CustomerColumnArray, "ECCNo")].ToString();

                                                            var bank_code = a[Array.IndexOf(CustomerColumnArray, "BankCode")].ToString();
                                                            IDVM.bank_id = _Generic.GetBankId(bank_code);
                                                            //IDVM.bank_account_code = a[Array.IndexOf(CustomerColumnArray, "BankAccountCode")].ToString();
                                                            //var account_type = a[Array.IndexOf(CustomerColumnArray, "AccountType")].ToString();
                                                            //IDVM.account_type_id = _Generic.GetAccountTypeId(account_type);
                                                            IDVM.bank_account_no = a[Array.IndexOf(CustomerColumnArray, "AccountNumber")].ToString();
                                                            IDVM.ifsc_code = a[Array.IndexOf(CustomerColumnArray, "IFSCCode")].ToString();
                                                            IDVM.commisionerate = a[Array.IndexOf(CustomerColumnArray, "Comissionerate")].ToString();
                                                            IDVM.range = a[Array.IndexOf(CustomerColumnArray, "Range")].ToString();
                                                            IDVM.division = a[Array.IndexOf(CustomerColumnArray, "Division")].ToString();

                                                            var overall_discount = a[Array.IndexOf(CustomerColumnArray, "Overall%Discount")].ToString();
                                                            if (overall_discount == "")
                                                            {
                                                                IDVM.OVERALL_DISCOUNT = 0;
                                                            }
                                                            else
                                                            {
                                                                IDVM.OVERALL_DISCOUNT = Double.Parse(overall_discount);
                                                            }
                                                            IDVM.ADDITIONAL_INFO = a[Array.IndexOf(CustomerColumnArray, "AdditionalInfo")].ToString();
                                                            var blocked = a[Array.IndexOf(CustomerColumnArray, "Blocked")].ToString();
                                                            if (blocked.ToLower() == "yes")
                                                            {
                                                                IDVM.IS_BLOCKED = true;
                                                            }
                                                            else if (blocked.ToLower() == "no" || blocked == "")
                                                            {
                                                                IDVM.IS_BLOCKED = false;
                                                            }
                                                            else
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = blocked;
                                                                errorMessage = "Add only yes or no blocked .";
                                                            }
                                                            IDVM.CREATED_BY = 1;
                                                            IDVM.CREATED_ON = DateTime.Now;
                                                            customer_excel.Add(IDVM);
                                                        }
                                                        else
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = customer_code;
                                                            errorMessage = "Customer Code is duplicate!";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = customer_code;
                                                        errorMessage = "Customer Code already exist!";
                                                    }

                                                }
                                                else
                                                {
                                                    customer_excel IDVM = new customer_excel();
                                                    var customer_category = a[Array.IndexOf(CustomerColumnArray, "CustomerCategory")].ToString();
                                                    if (customer_category == "")
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = customer_category.ToString();
                                                        errorMessage = "Add customer category.";
                                                    }
                                                    else
                                                    {
                                                        var customer_category_id = _Generic.GetCustomerCategoryId(customer_category);
                                                        if (customer_category_id == 0)
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = customer_category.ToString();
                                                            errorMessage = customer_category + " not found.";
                                                        }
                                                        else
                                                        {
                                                            IDVM.CUSTOMER_CATEGORY_ID = customer_category_id;
                                                        }
                                                    }
                                                    var customerCode = a[Array.IndexOf(CustomerColumnArray, "CustomerCode")].ToString();
                                                    var CustCode = _Generic.GetCustomerId(customerCode);
                                                    if (CustCode == 0)
                                                    {
                                                        IDVM.CUSTOMER_CODE = customerCode;
                                                        IDVM.CUSTOMER_NAME = a[Array.IndexOf(CustomerColumnArray, "CustomerName")].ToString();

                                                        var customer_display_name = a[Array.IndexOf(CustomerColumnArray, "CustomerDisplayName")].ToString();
                                                        if (customer_display_name == "")
                                                        {
                                                            IDVM.CUSTOMER_DISPLAY_NAME = IDVM.CUSTOMER_NAME;
                                                        }
                                                        else
                                                        {
                                                            IDVM.CUSTOMER_DISPLAY_NAME = customer_display_name;
                                                        }
                                                        var org_type = a[Array.IndexOf(CustomerColumnArray, "OrganizationType")].ToString();
                                                        if (org_type == "")
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = org_type.ToString();
                                                            errorMessage = "Add Org Type.";
                                                        }
                                                        else
                                                        {
                                                            var org_type_id = _Generic.GetOrgId(org_type);
                                                            if (org_type_id == 0)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = org_type.ToString();
                                                                errorMessage = org_type + " not found.";
                                                            }
                                                            else
                                                            {
                                                                IDVM.ORG_TYPE_ID = org_type_id;
                                                            }
                                                        }
                                                        var billing_add = a[Array.IndexOf(CustomerColumnArray, "BillingAddress")].ToString();
                                                        if (billing_add == "")
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = billing_add.ToString();
                                                            errorMessage = "Add billing Address.";
                                                        }
                                                        else
                                                        {
                                                            IDVM.BILLING_ADDRESS = billing_add;
                                                        }
                                                        var billing_city = a[Array.IndexOf(CustomerColumnArray, "BillingCity")].ToString();
                                                        if (billing_city == "")
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = billing_city.ToString();
                                                            errorMessage = "Add billing City.";
                                                        }
                                                        else
                                                        {
                                                            IDVM.BILLING_CITY = billing_city;
                                                        }
                                                        var billing_pincode = a[Array.IndexOf(CustomerColumnArray, "BillingPinCode")].ToString();
                                                        if (billing_pincode == "")
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = billing_pincode.ToString();
                                                            errorMessage = "Add Pincode.";
                                                        }
                                                        else
                                                        {
                                                            IDVM.BILLING_PINCODE = int.Parse(billing_pincode);
                                                        }
                                                        var state = a[Array.IndexOf(CustomerColumnArray, "BillingState")].ToString();
                                                        if (state == "")
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = state.ToString();
                                                            errorMessage = "Add billing State.";
                                                        }
                                                        else
                                                        {
                                                            var billing_state_id = _Generic.GetStateID(state);
                                                            if (billing_state_id == 0)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = state.ToString();
                                                                errorMessage = state + " not found.";
                                                            }
                                                            else
                                                            {
                                                                IDVM.BILLING_STATE_ID = billing_state_id;
                                                            }
                                                        }
                                                        var salesRM = a[Array.IndexOf(CustomerColumnArray, "SalesRM")].ToString();
                                                        if (salesRM == "")
                                                        {
                                                            IDVM.SALES_EXEC_ID = 0;
                                                        }
                                                        else
                                                        {
                                                            var salesRMid = _Generic.GetSalesRMID(salesRM);
                                                            if (salesRMid == 0)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = salesRM.ToString();
                                                                errorMessage = salesRM + " not found.";
                                                            }
                                                            else
                                                            {
                                                                IDVM.SALES_EXEC_ID = salesRMid;
                                                            }
                                                        }
                                                        var freightTerms = a[Array.IndexOf(CustomerColumnArray, "FreightTerms")].ToString();
                                                        if (freightTerms == "")
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = freightTerms.ToString();
                                                            errorMessage = "Add Freight Terms.";
                                                        }
                                                        else
                                                        {
                                                            var freight_term_id = _Generic.GetFreightId(freightTerms);
                                                            if (freight_term_id == 0)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = freightTerms.ToString();
                                                                errorMessage = freightTerms + " not Found.";
                                                            }
                                                            else
                                                            {
                                                                IDVM.FREIGHT_TERMS_ID = freight_term_id;
                                                            }
                                                        }
                                                        var linkToParent = a[Array.IndexOf(CustomerColumnArray, "LinktoParent")].ToString();
                                                        if (linkToParent.ToLower() == "yes")
                                                        {
                                                            IDVM.HAS_PARENT = true;
                                                            var customer_parent_name = a[Array.IndexOf(CustomerColumnArray, "ParentName")].ToString();
                                                            if (customer_parent_name == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = customer_parent_name;
                                                                errorMessage = "Add only yes or no in customer sheet.";
                                                            }
                                                            else
                                                            {
                                                                IDVM.CUSTOMER_PARENT_ID = _Generic.GetCustomerParentId(customer_parent_name);
                                                            }
                                                        }
                                                        else if (linkToParent.ToLower() == "no" || linkToParent == "")
                                                        {
                                                            IDVM.HAS_PARENT = false;
                                                        }
                                                        else
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = IDVM.HAS_PARENT.ToString();
                                                            errorMessage = "Add only yes or no in customer sheet.";
                                                        }
                                                        var territory = a[Array.IndexOf(CustomerColumnArray, "Territory")].ToString();
                                                        if (territory == "")
                                                        {
                                                            IDVM.TERRITORY_ID = 0;
                                                        }
                                                        else
                                                        {
                                                            var territory_id = _Generic.GetTerritoryId(territory);
                                                            if (territory_id == 0)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = territory.ToString();
                                                                errorMessage = territory + " not found.";
                                                            }
                                                            else
                                                            {
                                                                IDVM.TERRITORY_ID = territory_id;
                                                            }
                                                        }

                                                        var priority = a[Array.IndexOf(CustomerColumnArray, "Priority")].ToString();
                                                        if (priority == "")
                                                        {
                                                            IDVM.PRIORITY_ID = 0;
                                                        }
                                                        else
                                                        {
                                                            var priority_id = _Generic.GetPriorityId(priority);
                                                            if (priority_id == 0)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = priority.ToString();
                                                                errorMessage = priority + " not found.";
                                                            }
                                                            else
                                                            {
                                                                IDVM.PRIORITY_ID = priority_id;
                                                            }
                                                        }
                                                        var WhetherSameAsBilling = a[Array.IndexOf(CustomerColumnArray, "WhetherSameAsBilling")].ToString();
                                                        if (WhetherSameAsBilling.ToLower() == "no")
                                                        {
                                                            IDVM.CORR_ADDRESS = a[Array.IndexOf(CustomerColumnArray, "CorrespondenceAddress")].ToString();
                                                            IDVM.CORR_CITY = a[Array.IndexOf(CustomerColumnArray, "CorrespondenceCity")].ToString();
                                                            var corr_state = a[Array.IndexOf(CustomerColumnArray, "CorrespondenceState")].ToString();
                                                            IDVM.CORR_STATE_ID = _Generic.GetStateID(corr_state);
                                                            var corr_pincode = a[Array.IndexOf(CustomerColumnArray, "CorrespondencePinCode")].ToString();
                                                            if (corr_pincode == "")
                                                            {
                                                                IDVM.CORR_PINCODE = 0;
                                                            }
                                                            else
                                                            {
                                                                IDVM.CORR_PINCODE = int.Parse(corr_pincode);
                                                            }

                                                        }
                                                        else if (WhetherSameAsBilling.ToLower() == "yes" || WhetherSameAsBilling == "")
                                                        {
                                                            IDVM.CORR_ADDRESS = IDVM.BILLING_ADDRESS;
                                                            IDVM.CORR_CITY = IDVM.BILLING_CITY;
                                                            IDVM.CORR_STATE_ID = IDVM.BILLING_STATE_ID;
                                                            IDVM.CORR_PINCODE = IDVM.BILLING_PINCODE;
                                                        }
                                                        else
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = WhetherSameAsBilling.ToString();
                                                            errorMessage = "Add only yes or no WhetherSameAsBilling.";
                                                        }

                                                        IDVM.EMAIL_ID_PRIMARY = a[Array.IndexOf(CustomerColumnArray, "EmailID")].ToString();
                                                        IDVM.std_code = a[Array.IndexOf(CustomerColumnArray, "StdCode")].ToString();
                                                        IDVM.TELEPHONE_PRIMARY = a[Array.IndexOf(CustomerColumnArray, "Phone")].ToString();
                                                        IDVM.WEBSITE_ADDRESS = a[Array.IndexOf(CustomerColumnArray, "WebsiteAddress")].ToString();
                                                        IDVM.FAX = a[Array.IndexOf(CustomerColumnArray, "Fax")].ToString();

                                                        var currency = a[Array.IndexOf(CustomerColumnArray, "DefaultCurrency")].ToString();
                                                        if (currency == "")
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = currency.ToString();
                                                            errorMessage = "Add Currency.";
                                                        }
                                                        else
                                                        {
                                                            var currency_id = _Generic.GetCurrencyId(currency);
                                                            if (currency_id == 0)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = currency.ToString();
                                                                errorMessage = currency + " not found.";
                                                            }
                                                            else
                                                            {
                                                                IDVM.CREDIT_LIMIT_CURRENCY_ID = currency_id;
                                                            }
                                                        }
                                                        var paymentTerms = a[Array.IndexOf(CustomerColumnArray, "PaymentTerms")].ToString();
                                                        if (paymentTerms == "")
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = paymentTerms.ToString();
                                                            errorMessage = "Add Payment terms.";
                                                        }
                                                        else
                                                        {
                                                            var payment_terms_id = _Generic.GetPaymentTermId(paymentTerms);
                                                            if (payment_terms_id == 0)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = paymentTerms.ToString();
                                                                errorMessage = paymentTerms + " not found.";
                                                            }
                                                            else
                                                            {
                                                                IDVM.PAYMENT_TERMS_ID = payment_terms_id;
                                                            }
                                                        }
                                                        var paymentCycleType = a[Array.IndexOf(CustomerColumnArray, "PaymentCycleType")].ToString();
                                                        if (paymentCycleType == "")
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = paymentCycleType.ToString();
                                                            errorMessage = "Add Payment Cycle Type! .";
                                                        }
                                                        else
                                                        {
                                                            var payment_cycle_type_id = _Generic.GetPaymentCycleTypeId(paymentCycleType);
                                                            if (payment_cycle_type_id == 0)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = paymentCycleType.ToString();
                                                                errorMessage = paymentCycleType + " not find! .";
                                                            }
                                                            else
                                                            {
                                                                IDVM.PAYMENT_CYCLE_TYPE_ID = payment_cycle_type_id;
                                                                var payment_cycle = _Generic.GetPaymentCycle(IDVM.PAYMENT_CYCLE_TYPE_ID);
                                                                var payment_cycle_data = a[Array.IndexOf(CustomerColumnArray, "PaymentCycle")].ToString();
                                                                if (payment_cycle_data == "")
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = payment_cycle_data.ToString();
                                                                    errorMessage = "Add Payment Cycle Type.";
                                                                }
                                                                else
                                                                {
                                                                    var payment_cycle_data_id = payment_cycle.Where(x => x.PAYMENT_CYCLE_NAME.ToLower() == payment_cycle_data.ToLower()).FirstOrDefault();
                                                                    if (payment_cycle_data_id != null)
                                                                    {
                                                                        IDVM.PAYMENT_CYCLE_ID = payment_cycle_data_id.PAYMENT_CYCLE_ID;
                                                                    }
                                                                    else
                                                                    {
                                                                        errorList++;
                                                                        error[error.Length - 1] = payment_cycle_data.ToString();
                                                                        errorMessage = payment_cycle_data + " not find! .";
                                                                    }
                                                                }

                                                            }
                                                        }
                                                        var credit_limit = a[Array.IndexOf(CustomerColumnArray, "CreditLimit")].ToString();
                                                        if (credit_limit == "")
                                                        {
                                                            IDVM.CREDIT_LIMIT = 0;
                                                        }
                                                        else
                                                        {
                                                            IDVM.CREDIT_LIMIT = Double.Parse(credit_limit);
                                                        }
                                                        var tds_applicable = a[Array.IndexOf(CustomerColumnArray, "TDSApplicable")].ToString();
                                                        if (tds_applicable.ToLower() == "yes")
                                                        {
                                                            IDVM.TDS_APPLICABLE = true;
                                                            var tds_code = a[Array.IndexOf(CustomerColumnArray, "TDSSectionCode")].ToString();
                                                            if (tds_code == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = tds_code;
                                                                errorMessage = "TDS code is Blank!!";
                                                            }
                                                            else
                                                            {
                                                                var tds_id = _Generic.GetTdsCodeId(tds_code);
                                                                IDVM.tds_id = tds_id;
                                                            }

                                                        }
                                                        else if (tds_applicable.ToLower() == "no" || tds_applicable == "")
                                                        {
                                                            IDVM.TDS_APPLICABLE = false;
                                                            IDVM.tds_id = 0;
                                                        }
                                                        else
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = tds_applicable;
                                                            errorMessage = "Added yes or no only!";
                                                        }
                                                        var pan_no = a[Array.IndexOf(CustomerColumnArray, "PANNo")].ToString();
                                                        if (pan_no == "")
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = pan_no.ToString();
                                                            errorMessage = "Add Pan No! .";
                                                        }
                                                        else
                                                        {
                                                            IDVM.pan_no = pan_no;
                                                        }
                                                        IDVM.vat_tin_no = a[Array.IndexOf(CustomerColumnArray, "VATTINNo")].ToString();
                                                        IDVM.cst_tin_no = a[Array.IndexOf(CustomerColumnArray, "CSTTINNo")].ToString();
                                                        IDVM.service_tax_no = a[Array.IndexOf(CustomerColumnArray, "ServiceTaxNo")].ToString();
                                                        IDVM.gst_no = a[Array.IndexOf(CustomerColumnArray, "GSTNo")].ToString();
                                                        IDVM.ecc_no = a[Array.IndexOf(CustomerColumnArray, "ECCNo")].ToString();

                                                        var bank_code = a[Array.IndexOf(CustomerColumnArray, "BankCode")].ToString();
                                                        IDVM.bank_id = _Generic.GetBankId(bank_code);
                                                        //IDVM.bank_account_code = a[Array.IndexOf(CustomerColumnArray, "BankAccountCode")].ToString();
                                                        //var account_type = a[Array.IndexOf(CustomerColumnArray, "AccountType")].ToString();
                                                        //IDVM.account_type_id = _Generic.GetAccountTypeId(account_type);
                                                        IDVM.bank_account_no = a[Array.IndexOf(CustomerColumnArray, "AccountNumber")].ToString();
                                                        IDVM.ifsc_code = a[Array.IndexOf(CustomerColumnArray, "IFSCCode")].ToString();
                                                        IDVM.commisionerate = a[Array.IndexOf(CustomerColumnArray, "Comissionerate")].ToString();
                                                        IDVM.range = a[Array.IndexOf(CustomerColumnArray, "Range")].ToString();
                                                        IDVM.division = a[Array.IndexOf(CustomerColumnArray, "Division")].ToString();

                                                        var overall_discount = a[Array.IndexOf(CustomerColumnArray, "Overall%Discount")].ToString();
                                                        if (overall_discount == "")
                                                        {
                                                            IDVM.OVERALL_DISCOUNT = 0;
                                                        }
                                                        else
                                                        {
                                                            IDVM.OVERALL_DISCOUNT = Double.Parse(overall_discount);
                                                        }
                                                        IDVM.ADDITIONAL_INFO = a[Array.IndexOf(CustomerColumnArray, "AdditionalInfo")].ToString();
                                                        var blocked = a[Array.IndexOf(CustomerColumnArray, "Blocked")].ToString();
                                                        if (blocked.ToLower() == "yes")
                                                        {
                                                            IDVM.IS_BLOCKED = true;
                                                        }
                                                        else if (blocked.ToLower() == "no" || blocked == "")
                                                        {
                                                            IDVM.IS_BLOCKED = false;
                                                        }
                                                        else
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = blocked;
                                                            errorMessage = "Add only yes or no blocked .";
                                                        }
                                                        IDVM.CREATED_BY = 1;
                                                        IDVM.CREATED_ON = DateTime.Now;
                                                        customer_excel.Add(IDVM);
                                                    }
                                                    else
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = customerCode;
                                                        errorMessage = "Customer Code is duplicate!";
                                                    }


                                                }
                                            }

                                        }

                                    }
                                }

                            }
                            else if (sheet.TableName == "Contact" && errorMessage == "")
                            {
                                string[] ContactColumnArray = new string[sheet.Columns.Count];
                                var TempSheetColumn = from DataColumn Tempcolumn in sheet.Columns select Tempcolumn;
                                foreach (var ary1 in TempSheetColumn)
                                {
                                    ContactColumnArray[contcol] = ary1.ToString();
                                    contcol = contcol + 1;
                                }

                                if (sheet != null)
                                {
                                    var TempSheetRows = from DataRow TempRow in sheet.Rows select TempRow;
                                    foreach (var ary in TempSheetRows)
                                    {
                                        var a = ary.ItemArray;
                                        if (ValidateContactExcelColumns(ContactColumnArray) == true)
                                        {
                                            string sr_no = a[Array.IndexOf(ContactColumnArray, "SrNo")].ToString();
                                            if (sr_no != "")
                                            {
                                                if (contact_excel.Count != 0)
                                                {
                                                    contact_excel CE = new contact_excel();
                                                    CE.CUSTOMER_CODE = a[Array.IndexOf(ContactColumnArray, "CustomerCode")].ToString();
                                                    var customerCode = customer_excel.Where(x => x.CUSTOMER_CODE == CE.CUSTOMER_CODE).FirstOrDefault();
                                                    if (customerCode != null)
                                                    {
                                                        CE.CONTACT_NAME = a[Array.IndexOf(ContactColumnArray, "ContactName")].ToString();
                                                        CE.DESIGNATION = a[Array.IndexOf(ContactColumnArray, "Designation")].ToString();
                                                        CE.EMAIL_ADDRESS = a[Array.IndexOf(ContactColumnArray, "EmailAddress")].ToString();
                                                        CE.MOBILE_NO = a[Array.IndexOf(ContactColumnArray, "MobileNumber")].ToString();
                                                        CE.PHONE_NO = a[Array.IndexOf(ContactColumnArray, "PhoneNumber")].ToString();
                                                        var SendSMSFlag = a[Array.IndexOf(ContactColumnArray, "SendSMSFlag")].ToString();
                                                        if (SendSMSFlag.ToLower() == "yes")
                                                        {
                                                            CE.SEND_SMS_FLAG = true;
                                                        }
                                                        else if (SendSMSFlag.ToLower() == "no" || SendSMSFlag == "")
                                                        {
                                                            CE.SEND_SMS_FLAG = false;
                                                        }
                                                        else
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = SendSMSFlag;
                                                            errorMessage = "Add only yes or no send sns flag.";
                                                        }
                                                        var SendEmailFlag = a[Array.IndexOf(ContactColumnArray, "SendEmailFlag")].ToString();
                                                        if (SendEmailFlag.ToLower() == "yes")
                                                        {
                                                            CE.SEND_EMAIL_FLAG = true;
                                                        }
                                                        else if (SendEmailFlag.ToLower() == "no" || SendSMSFlag == "")
                                                        {
                                                            CE.SEND_EMAIL_FLAG = false;
                                                        }
                                                        else
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = SendEmailFlag;
                                                            errorMessage = "Add only yes or no send email flag.";
                                                        }

                                                        contact_excel.Add(CE);
                                                    }
                                                    else
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = CE.CUSTOMER_CODE;
                                                        errorMessage = "Customer Code not find!";
                                                    }

                                                }
                                                else
                                                {
                                                    contact_excel CE = new contact_excel();
                                                    CE.CUSTOMER_CODE = a[Array.IndexOf(ContactColumnArray, "CustomerCode")].ToString();
                                                    var customer_code = customer_excel.Where(x => x.CUSTOMER_CODE == CE.CUSTOMER_CODE).FirstOrDefault();
                                                    if (customer_code == null)
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = CE.CUSTOMER_CODE + "Contact Sheet";
                                                        errorMessage = "Customer Code not find in Contact!";
                                                    }
                                                    else
                                                    {
                                                        CE.CONTACT_NAME = a[Array.IndexOf(ContactColumnArray, "ContactName")].ToString();
                                                        CE.DESIGNATION = a[Array.IndexOf(ContactColumnArray, "Designation")].ToString();
                                                        CE.EMAIL_ADDRESS = a[Array.IndexOf(ContactColumnArray, "EmailAddress")].ToString();
                                                        CE.MOBILE_NO = a[Array.IndexOf(ContactColumnArray, "MobileNumber")].ToString();
                                                        CE.PHONE_NO = a[Array.IndexOf(ContactColumnArray, "PhoneNumber")].ToString();
                                                        var SendSMSFlag = a[Array.IndexOf(ContactColumnArray, "SendSMSFlag")].ToString();
                                                        if (SendSMSFlag.ToLower() == "yes")
                                                        {
                                                            CE.SEND_SMS_FLAG = true;
                                                        }
                                                        else if (SendSMSFlag.ToLower() == "yes")
                                                        {
                                                            CE.SEND_SMS_FLAG = false;
                                                        }
                                                        else
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = SendSMSFlag;
                                                            errorMessage = "Add only yes or no send sms flag.";
                                                        }
                                                        var SendEmailFlag = a[Array.IndexOf(ContactColumnArray, "SendEmailFlag")].ToString();
                                                        if (SendEmailFlag.ToLower() == "yes")
                                                        {
                                                            CE.SEND_EMAIL_FLAG = true;
                                                        }
                                                        else if (SendEmailFlag.ToLower() == "no")
                                                        {
                                                            CE.SEND_EMAIL_FLAG = false;
                                                        }
                                                        else
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = SendEmailFlag;
                                                            errorMessage = "Add only yes or no send email flag .";
                                                        }
                                                        contact_excel.Add(CE);
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else if (sheet.TableName == "ProdGroup" && errorMessage == "")
                            {
                                string[] ProCategoryColumnArray = new string[sheet.Columns.Count];
                                var TempSheetColumn = from DataColumn Tempcolumn in sheet.Columns select Tempcolumn;
                                foreach (var ary1 in TempSheetColumn)
                                {
                                    ProCategoryColumnArray[procol] = ary1.ToString();
                                    procol = procol + 1;
                                }

                                if (sheet != null)
                                {
                                    var TempSheetRows = from DataRow TempRow in sheet.Rows select TempRow;
                                    foreach (var ary in TempSheetRows)
                                    {
                                        var a = ary.ItemArray;
                                        if (ValidateProCategoryExcelColumns(ProCategoryColumnArray) == true)
                                        {
                                            string sr_no = a[Array.IndexOf(ProCategoryColumnArray, "SrNo")].ToString();
                                            if (sr_no != "")
                                            {
                                                if (item_excel.Count != 0)
                                                {
                                                    item_excel IE = new item_excel();
                                                    var customerCode = a[Array.IndexOf(ProCategoryColumnArray, "CustomerCode")].ToString();
                                                    var customer_code = customer_excel.Where(x => x.CUSTOMER_CODE == customerCode).FirstOrDefault();
                                                    if (customer_code == null)
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = IE.CUSTOMER_CODE;
                                                        errorMessage = "Customer Code not find!";
                                                    }
                                                    else
                                                    {
                                                        var item_category = a[Array.IndexOf(ProCategoryColumnArray, "ProdGroup")].ToString();
                                                        if (item_category == "")
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = item_category;
                                                            errorMessage = "Add Item Category!";
                                                        }
                                                        else
                                                        {
                                                            var item_category_id = _Generic.GetItemGroupId(item_category);
                                                            if (item_category_id == 0)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = item_category;
                                                                errorMessage = item_category + " not found!";
                                                            }
                                                            else
                                                            {
                                                                var duplicate_customer_Code = item_excel.Where(x => x.CUSTOMER_CODE == customerCode).FirstOrDefault();
                                                                if (duplicate_customer_Code == null)
                                                                {
                                                                    IE.CUSTOMER_CODE = customerCode;
                                                                    IE.item_category_id = item_category_id;

                                                                }
                                                                else
                                                                {
                                                                    if (duplicate_customer_Code.item_category_id == item_category_id)
                                                                    {

                                                                        errorList++;
                                                                        error[error.Length - 1] = item_category;
                                                                        errorMessage = item_category + " duplicate in procategory!";
                                                                    }
                                                                    else
                                                                    {
                                                                        IE.CUSTOMER_CODE = customerCode;
                                                                        IE.item_category_id = item_category_id;
                                                                    }
                                                                }
                                                            }

                                                        }
                                                        var rate = a[Array.IndexOf(ProCategoryColumnArray, "DiscRate")].ToString();
                                                        if (rate == "")
                                                        {
                                                            IE.rate = 0;
                                                        }
                                                        else
                                                        {
                                                            IE.rate = Double.Parse(rate);
                                                        }

                                                    }
                                                    item_excel.Add(IE);
                                                }
                                                else
                                                {
                                                    item_excel IE = new item_excel();
                                                    IE.CUSTOMER_CODE = a[Array.IndexOf(ProCategoryColumnArray, "CustomerCode")].ToString();
                                                    var customer_code = customer_excel.Where(x => x.CUSTOMER_CODE == IE.CUSTOMER_CODE).FirstOrDefault();
                                                    if (customer_code == null)
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = IE.CUSTOMER_CODE;
                                                        errorMessage = "Customer Code not find!";
                                                    }
                                                    else
                                                    {
                                                        IE.CUSTOMER_CODE = a[Array.IndexOf(ProCategoryColumnArray, "CustomerCode")].ToString();
                                                        var item_category = a[Array.IndexOf(ProCategoryColumnArray, "ProdGroup")].ToString();
                                                        if (item_category == "")
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = item_category;
                                                            errorMessage = "Add Item Category!";
                                                        }
                                                        else
                                                        {
                                                            var item_category_id = _Generic.GetItemGroupId(item_category);
                                                            if (item_category_id == 0)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = item_category;
                                                                errorMessage = item_category + " not found!";
                                                            }
                                                            else
                                                            {
                                                                IE.item_category_id = item_category_id;
                                                            }

                                                        }

                                                    }
                                                    item_excel.Add(IE);
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                            else if (sheet.TableName == "GLAccount" && errorMessage == "")
                            {
                                string[] GLColumnArray = new string[sheet.Columns.Count];
                                var TempSheetColumn = from DataColumn Tempcolumn in sheet.Columns select Tempcolumn;
                                foreach (var ary1 in TempSheetColumn)
                                {
                                    GLColumnArray[glcol] = ary1.ToString();
                                    glcol = glcol + 1;
                                }

                                if (sheet != null)
                                {
                                    var TempSheetRows = from DataRow TempRow in sheet.Rows select TempRow;
                                    var customerExcelCount = customer_excel.Count;
                                    foreach (var ary in TempSheetRows)
                                    {
                                        var a = ary.ItemArray;
                                        if (ValidateGLExcelColumns(GLColumnArray) == true)
                                        {
                                            string sr_no = a[Array.IndexOf(GLColumnArray, "SrNo")].ToString();
                                            if (sr_no != "")
                                            {
                                                if (gl_excel.Count != 0)
                                                {
                                                    gl_excel GE = new gl_excel();

                                                    var customer_code = a[Array.IndexOf(GLColumnArray, "CustomerCode")].ToString();
                                                    var customerCode = customer_excel.Where(x => x.CUSTOMER_CODE == customer_code).FirstOrDefault();
                                                    if (customer_code == null)
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = GE.CUSTOMER_CODE;
                                                        errorMessage = "GL Customer Code not find!";
                                                    }
                                                    else
                                                    {

                                                        var gl_account_type = a[Array.IndexOf(GLColumnArray, "GLAccountType")].ToString();
                                                        if (gl_account_type == "")
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = gl_account_type;
                                                            errorMessage = "Add gl_account type!";
                                                        }
                                                        else
                                                        {
                                                            var gl_account_type_id = _Generic.GetLedgerAccountTypeId(gl_account_type, 1);
                                                            if (gl_account_type_id != 0)
                                                            {
                                                                var duplicate_customer_code = gl_excel.Where(x => x.CUSTOMER_CODE == customer_code).ToList();
                                                                if (duplicate_customer_code.Count == 0)
                                                                {
                                                                    GE.CUSTOMER_CODE = customer_code;
                                                                    GE.ledger_account_type_id = gl_account_type_id;
                                                                    duplicateglexcle dupli = new duplicateglexcle();
                                                                    dupli.CUSTOMER_CODE = GE.CUSTOMER_CODE;
                                                                    duplicateglexcle.Add(dupli);
                                                                    customerExcelCount--;
                                                                }
                                                                else if (duplicate_customer_code.Count < 2)
                                                                {
                                                                    if (duplicate_customer_code.FirstOrDefault().ledger_account_type_id == gl_account_type_id)
                                                                    {
                                                                        errorList++;
                                                                        error[error.Length - 1] = gl_account_type;
                                                                        errorMessage = gl_account_type + " duplicate!";
                                                                    }
                                                                    else
                                                                    {

                                                                        GE.CUSTOMER_CODE = customer_code;
                                                                        GE.ledger_account_type_id = gl_account_type_id;
                                                                        var removedupli = duplicateglexcle.Where(x => x.CUSTOMER_CODE == GE.CUSTOMER_CODE).FirstOrDefault();
                                                                        duplicateglexcle.Remove(removedupli);

                                                                    }
                                                                }
                                                                else if (duplicate_customer_code.Count >= 2)
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = gl_account_type + " more than 2 prod category";
                                                                    errorMessage = gl_account_type + " duplicate!";
                                                                }

                                                            }
                                                            else
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = gl_account_type;
                                                                errorMessage = gl_account_type + " not found!";
                                                            }
                                                        }

                                                        var ledgerCode = a[Array.IndexOf(GLColumnArray, "LedgerCode")].ToString();
                                                        if (ledgerCode == "")
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = ledgerCode;
                                                            errorMessage = "Add Ledger Code.";
                                                        }
                                                        else
                                                        {
                                                            var ledger_code_id = _Generic.GetGLId(ledgerCode);
                                                            if (ledger_code_id == 0)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = ledgerCode;
                                                                errorMessage = ledgerCode + " not found!";
                                                            }
                                                            else
                                                            {
                                                                GE.gl_ledger_id = ledger_code_id;
                                                            }
                                                        }
                                                    }
                                                    gl_excel.Add(GE);
                                                }
                                                else
                                                {
                                                    gl_excel GE = new Domain.ViewModel.gl_excel();
                                                    var customer_code = a[Array.IndexOf(GLColumnArray, "CustomerCode")].ToString();
                                                    var customerCode = customer_excel.Where(x => x.CUSTOMER_CODE == customer_code).FirstOrDefault();
                                                    if (customer_code == null)
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = GE.CUSTOMER_CODE;
                                                        errorMessage = "GL Customer Code not find!";
                                                    }
                                                    else
                                                    {

                                                        GE.CUSTOMER_CODE = customer_code;
                                                        var gl_account_type = a[Array.IndexOf(GLColumnArray, "GLAccountType")].ToString();
                                                        if (gl_account_type == "")
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = gl_account_type;
                                                            errorMessage = "Add gl_account type!";
                                                        }
                                                        else
                                                        {
                                                            var gl_account_type_id = _Generic.GetLedgerAccountTypeId(gl_account_type, 1);
                                                            if (gl_account_type_id != 0)
                                                            {
                                                                GE.ledger_account_type_id = gl_account_type_id;
                                                            }
                                                            else
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = gl_account_type;
                                                                errorMessage = gl_account_type + " not found!";
                                                            }
                                                        }

                                                        var ledgerCode = a[Array.IndexOf(GLColumnArray, "LedgerCode")].ToString();
                                                        if (ledgerCode == "")
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = ledgerCode;
                                                            errorMessage = "Add Ledger Code.";
                                                        }
                                                        else
                                                        {
                                                            var ledger_code_id = _Generic.GetGLId(ledgerCode);
                                                            if (ledger_code_id == 0)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = ledgerCode;
                                                                errorMessage = ledgerCode + " not found!";
                                                            }
                                                            else
                                                            {
                                                                GE.gl_ledger_id = ledger_code_id;
                                                            }
                                                        }

                                                    }
                                                    duplicateglexcle dupli = new duplicateglexcle();
                                                    dupli.CUSTOMER_CODE = GE.CUSTOMER_CODE;
                                                    duplicateglexcle.Add(dupli);
                                                    customerExcelCount--;
                                                    gl_excel.Add(GE);
                                                }

                                            }

                                        }
                                    }
                                    if (customerExcelCount != 0)
                                    {
                                        errorList++;
                                        error[error.Length - 1] = "GL is mandatory for all Customer Code.";
                                        errorMessage = "GL is mandatory for all Customer Code.";
                                    }
                                }

                            }

                        }
                    }
                    excelReader.Close();
                    if (errorMessage == "")
                    {
                        if (duplicateglexcle.Count == 0)
                        {
                            var isSucess = _customerService.AddExcel(customer_excel, contact_excel, item_excel, gl_excel);
                            if (isSucess)
                            {
                                errorMessage = "success";
                                return Json(errorMessage, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                errorMessage = "Failed";
                            }
                        }
                        else
                        {
                            errorList++;
                            error[error.Length - 1] = "Add control account and down payment account in GL Account Type";
                            errorMessage = "Add control account and down payment account in GL Account Type";
                        }

                    }

                }
                else
                {
                    errorList++;
                    error[error.Length - 1] = "Select File to Upload.";
                    errorMessage = "Select File to Upload.";
                }
            }
            //return Json(new { Status = Message, text = errorList, error = error, errorMessage = errorMessage }, JsonRequestBehavior.AllowGet);
            return Json(errorList + " - " + errorMessage, JsonRequestBehavior.AllowGet);
        }
        public Boolean ValidateCustomerExcelColumns(string[] CustomerColumnArray)
        {
            if (CustomerColumnArray.Contains("SrNo") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("CustomerCategory") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("CustomerCode") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("CustomerName") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("CustomerDisplayName") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("OrganizationType") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("BillingAddress") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("BillingCity") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("BillingPinCode") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("BillingState") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("SalesRM") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("FreightTerms") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("LinktoParent") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("ParentName") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("Territory") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("Priority") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("WhetherSameAsBilling") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("CorrespondenceAddress") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("CorrespondenceCity") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("CorrespondencePinCode") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("CorrespondenceState") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("EmailID") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("StdCode") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("Phone") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("WebsiteAddress") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("Fax") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("DefaultCurrency") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("PaymentTerms") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("PaymentCycleType") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("PaymentCycle") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("CreditLimit") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("TDSApplicable") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("TDSSectionCode") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("PANNo") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("VATTINNo") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("CSTTINNo") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("ServiceTaxNo") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("GSTNo") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("ECCNo") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("BankCode") == false)
            {
                return false;
            }
            //if (CustomerColumnArray.Contains("BankAccountCode") == false)
            //{
            //    return false;
            //}
            //if (CustomerColumnArray.Contains("AccountType") == false)
            //{
            //    return false;
            //}
            if (CustomerColumnArray.Contains("AccountNumber") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("IFSCCode") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("Comissionerate") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("Range") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("Division") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("Overall%Discount") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("AdditionalInfo") == false)
            {
                return false;
            }
            if (CustomerColumnArray.Contains("Blocked") == false)
            {
                return false;
            }
            return true;
        }

        public Boolean ValidateContactExcelColumns(string[] ContactColumnArray)
        {
            if (ContactColumnArray.Contains("SrNo") == false)
            {
                return false;
            }
            if (ContactColumnArray.Contains("CustomerCode") == false)
            {
                return false;
            }
            if (ContactColumnArray.Contains("ContactName") == false)
            {
                return false;
            }
            if (ContactColumnArray.Contains("Designation") == false)
            {
                return false;
            }
            if (ContactColumnArray.Contains("EmailAddress") == false)
            {
                return false;
            }
            if (ContactColumnArray.Contains("MobileNumber") == false)
            {
                return false;
            }
            if (ContactColumnArray.Contains("PhoneNumber") == false)
            {
                return false;
            }
            if (ContactColumnArray.Contains("SendSMSFlag") == false)
            {
                return false;
            }
            if (ContactColumnArray.Contains("SendEmailFlag") == false)
            {
                return false;
            }
            return true;
        }

        public Boolean ValidateProCategoryExcelColumns(string[] ProCategoryColumnArray)
        {
            if (ProCategoryColumnArray.Contains("SrNo") == false)
            {
                return false;
            }
            if (ProCategoryColumnArray.Contains("CustomerCode") == false)
            {
                return false;
            }
            if (ProCategoryColumnArray.Contains("ProdGroup") == false)
            {
                return false;
            }
            if (ProCategoryColumnArray.Contains("DiscRate") == false)
            {
                return false;
            }
            return true;
        }
        public Boolean ValidateGLExcelColumns(string[] GLColumnArray)
        {
            if (GLColumnArray.Contains("SrNo") == false)
            {
                return false;
            }
            if (GLColumnArray.Contains("GLAccountType") == false)
            {
                return false;
            }
            if (GLColumnArray.Contains("LedgerCode") == false)
            {
                return false;
            }
            return true;
        }
    }
}
