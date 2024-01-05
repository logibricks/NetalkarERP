using System;
using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.ViewModel;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using System.Web;
using System.IO;
using Excel;
using System.Data;
using System.Text.RegularExpressions;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class VendorMasterController : Controller
    {
        private readonly IVendorService _vendorService;
        private readonly ICurrencyService _currencyService;
        private readonly IVendorCategoryService _vendorcategoryService;
        private readonly IVendorParentService _vendorParetntService;
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
        private readonly IBankService _bankService;
        private
        string[] error = new string[30000];
        string errorMessage = "";
        int errorList = 0;

        public VendorMasterController(IBankService bankService, IItemCategoryService ItemCategoryService, IVendorService vendorService, ICurrencyService currencyService, IVendorCategoryService vendorcategoryService,
            IVendorParentService vendorParetntService, IFreightTermsService freightTermsService, IOrgTypeService orgService, IPaymentCycleTypeService paymentCycleType,
           IPaymentTermsService paymentTerms, IPaymentCycleService paymentService, IStateService stateService, ITerritoryService territory, IPriorityService priority, ITdsCodeService TdsCodeService,
           IUserService userService, IGeneralLedgerService GeneralledgerService, ICountryService CustomerService, IGenericService gen, IItemGroupService ItemGroupService)
        {
            _vendorService = vendorService;
            _currencyService = currencyService;
            _vendorcategoryService = vendorcategoryService;
            _vendorParetntService = vendorParetntService;
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
            _Generic = gen;
            _itemCategoryService = ItemCategoryService;
            _itemGroupService = ItemGroupService;
            _tdsCodeService = TdsCodeService;
            _bankService = bankService;
        }

        // GET: Vendor
        [CustomAuthorizeAttribute("VNDM")]
        public ActionResult Index()
        {
            ViewBag.doc = TempData["saved"];
            ViewBag.datasource = _vendorService.GetVendorDetail();
            return View();
        }

        //get partial view for VENDOR contact
        public PartialViewResult GetPartialvendor(int count = 0)
        {
            ViewBag.Count = count;
            return PartialView("_VendorContactView");
        }
        //get partial view for ATTRIBUTE
        public PartialViewResult GetPartialAttributevendor(int attributecount = 0)
        {
            ViewBag.attributecount = attributecount;
            return PartialView("_VendorAttributeView");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vendorViewModel = _vendorService.Get((int)id);
            var currencylist = _currencyService.GetAll();
            var vendorcategorylist = _vendorcategoryService.GetAll();
            var vendorParentList = _vendorParetntService.GetAll();
            var freightterms = _freightTermsService.GetAll();
            var org = _orgService.GetAll();
            var paymentcycletype = _paymentCycleType.GetAll();
            var paymentterms = _paymentTerms.GetAll();
            var paymentcycle = _paymentService.GetAll();
            var statelist = _stateService.GetAll();
            var priority = _Generic.GetPriorityByForm(2);
            var user = _userService.GetAll();
            var country = _CustomerService.GetAll();
            var generalLedger = _Generic.GetLedgerAccount(2);
            ViewBag.generalleder = new SelectList(generalLedger, "gl_ledger_id", "gl_ledger_name");
            ViewBag.item_category = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.item_group = new SelectList(_itemGroupService.GetAll(), "ITEM_GROUP_ID", "ITEM_GROUP_NAME");
            ViewBag.tds_list = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
            ViewBag.parentlist = new SelectList(vendorParentList, "VENDOR_PARENT_ID", "VENDOR_PARENT_NAME");
            ViewBag.freightlist = new SelectList(freightterms, "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.orglist = new SelectList(org, "ORG_TYPE_ID", "ORG_TYPE_NAME");
            ViewBag.paymentcyclelist = new SelectList(paymentcycle, "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.paymentcycletype = new SelectList(paymentcycletype, "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.paymenttermslist = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.prioritylist = new SelectList(priority, "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.billingstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.corrstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.createdbylist = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.billinglist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.corlist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.banklist = new SelectList(_Generic.GetBankforSearchDropdown(), "bank_id", "bank_code");
            ViewBag.GST_CustomerType_List = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.GST_TDS_Code = new SelectList(_Generic.GetGSTTDSCodeList(), "gst_tds_code_id", "gst_tds_code");
            return View(vendorViewModel);
        }

        // GET: Vendor/Create
        [CustomAuthorizeAttribute("VNDM")]
        public ActionResult Create()
        {
            var currencylist = _currencyService.GetAll();
            var vendorcategorylist = _vendorcategoryService.GetAll();
            var vendorParentList = _vendorParetntService.GetAll();
            var freightterms = _freightTermsService.GetAll();
            var org = _orgService.GetAll();
            var paymentcycletype = _paymentCycleType.GetAll();
            var paymentterms = _paymentTerms.GetAll();
            var paymentcycle = _paymentService.GetAll();
            var statelist = _stateService.GetAll();
            var priority = _Generic.GetPriorityByForm(2);
            var user = _userService.GetAll();
            var country = _CustomerService.GetAll();
            var generalLedger = _Generic.GetLedgerAccount(2);
            ViewBag.error = "";
            ViewBag.generalleder = new SelectList(generalLedger, "gl_ledger_id", "gl_ledger_name");
            ViewBag.generalleder = new SelectList(generalLedger, "gl_ledger_id", "gl_ledger_name");
            ViewBag.item_category = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.item_group = new SelectList(_itemGroupService.GetAll(), "ITEM_GROUP_ID", "ITEM_GROUP_NAME");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
            ViewBag.parentlist = new SelectList(vendorParentList, "VENDOR_PARENT_ID", "VENDOR_PARENT_NAME");
            ViewBag.freightlist = new SelectList(freightterms, "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.orglist = new SelectList(org, "ORG_TYPE_ID", "ORG_TYPE_NAME");
            ViewBag.paymentcyclelist = new SelectList(paymentcycle, "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.paymentcycletype = new SelectList(paymentcycletype, "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.tds_list = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.paymenttermslist = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.prioritylist = new SelectList(priority, "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.billingstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.corrstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.createdbylist = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.billinglist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.corlist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.banklist = new SelectList(_Generic.GetBankforSearchDropdown(), "bank_id", "bank_code");
            ViewBag.GST_CustomerType_List = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.GST_TDS_Code = new SelectList(_Generic.GetGSTTDSCodeList(), "gst_tds_code_id", "gst_tds_code");
            ViewBag.NewVendorCode = _vendorcategoryService.GetVendorCode();
            return View();
        }

        //public ActionResult GetAttributeVendor(int id)
        //{
        //    return Json(vm, JsonRequestBehavior.AllowGet);
        //}
        // POST: Vendor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(REF_VENDOR_VM rEF_VENDOR)
        {
            rEF_VENDOR.CREATED_BY = 1;
            rEF_VENDOR.CREATED_ON = DateTime.Now;
            var issaved = "";
            if (ModelState.IsValid)
            {
                issaved = _vendorService.Add(rEF_VENDOR);
                if (issaved == "Saved")
                {
                    TempData["saved"] = "Saved Successfully.";
                    return RedirectToAction("Index");
                }
            }
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    var str = error.ErrorMessage;
                }
            }
            ViewBag.error = issaved;
            var currencylist = _currencyService.GetAll();
            var vendorcategorylist = _vendorcategoryService.GetAll();
            var vendorParentList = _vendorParetntService.GetAll();
            var freightterms = _freightTermsService.GetAll();
            var org = _orgService.GetAll();
            var paymentcycletype = _paymentCycleType.GetAll();
            var paymentterms = _paymentTerms.GetAll();
            var paymentcycle = _paymentService.GetAll();
            var statelist = _stateService.GetAll();
            var priority = _Generic.GetPriorityByForm(2);
            var user = _userService.GetAll();
            var country = _CustomerService.GetAll();
            var generalLedger = _Generic.GetLedgerAccount(2);
            ViewBag.generalleder = new SelectList(generalLedger, "gl_ledger_id", "gl_ledger_name");
            ViewBag.item_category = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.item_group = new SelectList(_itemGroupService.GetAll(), "ITEM_GROUP_ID", "ITEM_GROUP_NAME");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
            ViewBag.parentlist = new SelectList(vendorParentList, "VENDOR_PARENT_ID", "VENDOR_PARENT_NAME");
            ViewBag.freightlist = new SelectList(freightterms, "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.orglist = new SelectList(org, "ORG_TYPE_ID", "ORG_TYPE_NAME");
            ViewBag.paymentcyclelist = new SelectList(paymentcycle, "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.paymentcycletype = new SelectList(paymentcycletype, "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.tds_list = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.paymenttermslist = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.prioritylist = new SelectList(priority, "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.billingstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.corrstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.createdbylist = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.billinglist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.corlist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.banklist = new SelectList(_Generic.GetBankforSearchDropdown(), "bank_id", "bank_code");
            ViewBag.GST_CustomerType_List = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.GST_TDS_Code = new SelectList(_Generic.GetGSTTDSCodeList(), "gst_tds_code_id", "gst_tds_code");
            return View();
        }

        // GET: Vendor/Edit/5
        [CustomAuthorizeAttribute("VNDM")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.error = "";
            var vendorViewModel = _vendorService.Get((int)id);
            // return View(customerViewModel);
            var currencylist = _currencyService.GetAll();
            var vendorcategorylist = _vendorcategoryService.GetAll();
            var vendorParentList = _vendorParetntService.GetAll();
            var freightterms = _freightTermsService.GetAll();
            var org = _orgService.GetAll();
            var paymentcycletype = _paymentCycleType.GetAll();
            var paymentterms = _paymentTerms.GetAll();
            var paymentcycle = _paymentService.GetAll();
            var statelist = _stateService.GetAll();
            var priority = _Generic.GetPriorityByForm(2);
            var user = _userService.GetAll();
            var country = _CustomerService.GetAll();
            var generalLedger = _Generic.GetLedgerAccount(2);
            ViewBag.generalleder = new SelectList(generalLedger, "gl_ledger_id", "gl_ledger_name");
            ViewBag.item_category = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.item_group = new SelectList(_itemGroupService.GetAll(), "ITEM_GROUP_ID", "ITEM_GROUP_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
            ViewBag.parentlist = new SelectList(vendorParentList, "VENDOR_PARENT_ID", "VENDOR_PARENT_NAME");
            ViewBag.freightlist = new SelectList(freightterms, "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.orglist = new SelectList(org, "ORG_TYPE_ID", "ORG_TYPE_NAME");
            ViewBag.paymentcyclelist = new SelectList(paymentcycle, "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.paymentcycletype = new SelectList(paymentcycletype, "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.tds_list = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.paymenttermslist = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.prioritylist = new SelectList(priority, "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.billingstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.corrstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.createdbylist = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.billinglist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.corlist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.banklist = new SelectList(_Generic.GetBankforSearchDropdown(), "bank_id", "bank_code");
            ViewBag.GST_CustomerType_List = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.GST_TDS_Code = new SelectList(_Generic.GetGSTTDSCodeList(), "gst_tds_code_id", "gst_tds_code");
            return View(vendorViewModel);
        }

        // POST: Vendor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(REF_VENDOR_VM rEF_VENDOR, FormCollection fc)
        {
            var issaved = "";
            if (ModelState.IsValid)
            {
                rEF_VENDOR.CREATED_ON = DateTime.Now;
                issaved = _vendorService.Add(rEF_VENDOR);
                if (issaved == "Saved")
                {
                    TempData["saved"] = "Saved Successfully.";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = issaved;
            var currencylist = _currencyService.GetAll();
            var vendorcategorylist = _vendorcategoryService.GetAll();
            var vendorParentList = _vendorParetntService.GetAll();
            var freightterms = _freightTermsService.GetAll();
            var org = _orgService.GetAll();
            var paymentcycletype = _paymentCycleType.GetAll();
            var paymentterms = _paymentTerms.GetAll();
            var paymentcycle = _paymentService.GetAll();
            var statelist = _stateService.GetAll();
            var priority = _Generic.GetPriorityByForm(2);
            var user = _userService.GetAll();
            var country = _CustomerService.GetAll();
            var generalLedger = _Generic.GetLedgerAccount(2);
            ViewBag.generalleder = new SelectList(generalLedger, "gl_ledger_id", "gl_ledger_name");
            ViewBag.item_category = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.item_group = new SelectList(_itemGroupService.GetAll(), "ITEM_GROUP_ID", "ITEM_GROUP_NAME");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetVendorCategory(), "VENDOR_CATEGORY_ID", "VENDOR_CATEGORY_NAME");
            ViewBag.parentlist = new SelectList(vendorParentList, "VENDOR_PARENT_ID", "VENDOR_PARENT_NAME");
            ViewBag.freightlist = new SelectList(freightterms, "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.orglist = new SelectList(org, "ORG_TYPE_ID", "ORG_TYPE_NAME");
            ViewBag.paymentcyclelist = new SelectList(paymentcycle, "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.paymentcycletype = new SelectList(paymentcycletype, "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.tds_list = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.paymenttermslist = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.prioritylist = new SelectList(priority, "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.billingstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.corrstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.createdbylist = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.billinglist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.corlist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.banklist = new SelectList(_Generic.GetBankforSearchDropdown(), "bank_id", "bank_code");
            ViewBag.GST_CustomerType_List = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.GST_TDS_Code = new SelectList(_Generic.GetGSTTDSCodeList(), "gst_tds_code_id", "gst_tds_code");
            return View(rEF_VENDOR);
        }

        // GET: Vendor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_VENDOR = _vendorService.Get((int)id);
            if (rEF_VENDOR == null)
            {
                return HttpNotFound();
            }
            return View(rEF_VENDOR);
        }

        // POST: Vendor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var isdelete = _vendorService.Delete(id);
            if (isdelete)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _vendorService.Dispose();
            }
            base.Dispose(disposing);
        }

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
                    int contcol = 0, vencol = 0, glcol = 0, procol = 0;
                    string uploadtype = Request.Params[0];
                    List<vendor_excel> vendor_excel = new List<vendor_excel>();
                    List<vendor_contact_excel> vendor_contact_excel = new List<vendor_contact_excel>();
                    List<vendor_item_excel> item_excel = new List<vendor_item_excel>();
                    List<vendor_gl_excel> gl_excel = new List<vendor_gl_excel>();
                    List<vendor_duplicateglexcle> duplicateglexcle = new List<vendor_duplicateglexcle>();
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

                            if (sheet.TableName == "VendorDetails")
                            {
                                string[] VendorColumnArray = new string[sheet.Columns.Count];
                                var TempSheetColumn = from DataColumn Tempcolumn in sheet.Columns select Tempcolumn;
                                foreach (var ary1 in TempSheetColumn)
                                {
                                    VendorColumnArray[vencol] = ary1.ToString();
                                    vencol = vencol + 1;
                                }

                                if (sheet != null)
                                {
                                    var TempSheetRows = from DataRow TempRow in sheet.Rows select TempRow;
                                    foreach (var ary in TempSheetRows)
                                    {
                                        var a = ary.ItemArray;
                                        if (ValidateVendorExcelColumns(VendorColumnArray) == true)
                                        {
                                            string sr_no = a[Array.IndexOf(VendorColumnArray, "SrNo")].ToString();
                                            if (sr_no != "")
                                            {
                                                if (vendor_excel.Count != 0)
                                                {
                                                    vendor_excel IDVM = new vendor_excel();
                                                    var Vendor_category = a[Array.IndexOf(VendorColumnArray, "VendorCategory")].ToString();
                                                    if (Vendor_category == "")
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = Vendor_category.ToString();
                                                        errorMessage = "Add Vendor category.";
                                                    }
                                                    else
                                                    {
                                                        var Vendor_category_id = _Generic.GetVendorCategoryId(Vendor_category);
                                                        if (Vendor_category_id == 0)
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = Vendor_category.ToString();
                                                            errorMessage = Vendor_category + " not found.";
                                                        }
                                                        else
                                                        {
                                                            IDVM.VENDOR_CATEGORY_ID = Vendor_category_id;
                                                        }
                                                    }
                                                    var vendor_code = a[Array.IndexOf(VendorColumnArray, "VendorCode")].ToString();
                                                    var venDBCode = _Generic.GetVendorId(vendor_code);
                                                    if (venDBCode == 0)
                                                    {
                                                        var vendorCode = vendor_excel.Where(x => x.VENDOR_CODE == vendor_code).FirstOrDefault();
                                                        if (vendorCode == null)
                                                        {
                                                            IDVM.VENDOR_CODE = vendor_code;
                                                            IDVM.VENDOR_NAME = a[Array.IndexOf(VendorColumnArray, "VendorName")].ToString();
                                                            var Vendor_display_name = a[Array.IndexOf(VendorColumnArray, "VendorDisplayName")].ToString();
                                                            if (Vendor_display_name == "")
                                                            {
                                                                IDVM.VENDOR_DISPLAY_NAME = IDVM.VENDOR_NAME;
                                                            }
                                                            else
                                                            {
                                                                IDVM.VENDOR_DISPLAY_NAME = Vendor_display_name;
                                                            }
                                                            var org_type = a[Array.IndexOf(VendorColumnArray, "OrganizationType")].ToString();
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
                                                            var billing_add = a[Array.IndexOf(VendorColumnArray, "BillingAddress")].ToString();
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
                                                            var billing_city = a[Array.IndexOf(VendorColumnArray, "BillingCity")].ToString();
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
                                                            var billing_pincode = a[Array.IndexOf(VendorColumnArray, "BillingPinCode")].ToString();
                                                            if (billing_pincode == "" || billing_pincode.Length < 6)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = billing_pincode.ToString();
                                                                errorMessage = "Add Pincode.";
                                                            }
                                                            else if (billing_pincode.Length == 6)
                                                            {
                                                                IDVM.BILLING_PINCODE = int.Parse(billing_pincode);
                                                            }
                                                            var state = a[Array.IndexOf(VendorColumnArray, "BillingState")].ToString();
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
                                                            var priority = a[Array.IndexOf(VendorColumnArray, "Priority")].ToString();
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
                                                            var freightTerms = a[Array.IndexOf(VendorColumnArray, "FreightTerms")].ToString();
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
                                                            var linkToParent = a[Array.IndexOf(VendorColumnArray, "LinktoParent")].ToString();
                                                            if (linkToParent.ToLower() == "yes")
                                                            {
                                                                IDVM.HAS_PARENT = true;
                                                                var Vendor_parent_name = a[Array.IndexOf(VendorColumnArray, "ParentName")].ToString();
                                                                if (Vendor_parent_name == "")
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = Vendor_parent_name.ToString();
                                                                    errorMessage = Vendor_parent_name + " not Found.";
                                                                }
                                                                else
                                                                {
                                                                    IDVM.VENDOR_PARENT_ID = _Generic.GetVendorParentId(Vendor_parent_name);
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
                                                                errorMessage = "Add only yes or no in Vendor sheet.";
                                                            }
                                                            var WhetherSameAsBilling = a[Array.IndexOf(VendorColumnArray, "WhetherSameAsBilling")].ToString();
                                                            if (WhetherSameAsBilling.ToLower() == "no")
                                                            {
                                                                IDVM.CORR_ADDRESS = a[Array.IndexOf(VendorColumnArray, "CorrespondenceAddress")].ToString();
                                                                IDVM.CORR_CITY = a[Array.IndexOf(VendorColumnArray, "CorrespondenceCity")].ToString();
                                                                var corr_state = a[Array.IndexOf(VendorColumnArray, "CorrespondenceState")].ToString();
                                                                IDVM.CORR_STATE_ID = _Generic.GetStateID(corr_state);
                                                                var corr_pincode = a[Array.IndexOf(VendorColumnArray, "CorrespondencePinCode")].ToString();
                                                                if (corr_pincode == "" || corr_pincode.Length < 6)
                                                                {
                                                                    IDVM.CORR_PINCODE = 0;
                                                                }
                                                                else if (corr_pincode.Length == 6)
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
                                                                errorMessage = "Add only yes or no in WhetherSameAsBilling .";
                                                            }

                                                            IDVM.EMAIL_ID_PRIMARY = a[Array.IndexOf(VendorColumnArray, "EmailID")].ToString();
                                                            IDVM.phone_code = a[Array.IndexOf(VendorColumnArray, "StdCode")].ToString();
                                                            IDVM.TELEPHONE_PRIMARY = a[Array.IndexOf(VendorColumnArray, "Phone")].ToString();
                                                            IDVM.FAX = a[Array.IndexOf(VendorColumnArray, "Fax")].ToString();
                                                            IDVM.WEBSITE_ADDRESS = a[Array.IndexOf(VendorColumnArray, "WebsiteAddress")].ToString();
                                                            var currency = a[Array.IndexOf(VendorColumnArray, "DefaultCurrency")].ToString();
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
                                                            var paymentTerms = a[Array.IndexOf(VendorColumnArray, "PaymentTerms")].ToString();
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
                                                            var paymentCycleType = a[Array.IndexOf(VendorColumnArray, "PaymentCycleType")].ToString();
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
                                                                    var payment_cycle_data = a[Array.IndexOf(VendorColumnArray, "PaymentCycle")].ToString();
                                                                    if (payment_cycle_data == "")
                                                                    {
                                                                        errorList++;
                                                                        error[error.Length - 1] = payment_cycle_data.ToString();
                                                                        errorMessage = "Add Payment Cycle Type.";
                                                                    }
                                                                    else
                                                                    {
                                                                        var payment_cycle_data_id = payment_cycle.Where(x => x.PAYMENT_CYCLE_NAME == payment_cycle_data).FirstOrDefault();
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
                                                            var credit_limit = a[Array.IndexOf(VendorColumnArray, "CreditLimit")].ToString();
                                                            if (credit_limit == "")
                                                            {
                                                                IDVM.CREDIT_LIMIT = 0;
                                                            }
                                                            else
                                                            {
                                                                IDVM.CREDIT_LIMIT = Double.Parse(credit_limit);
                                                            }
                                                            var tds_applicable = a[Array.IndexOf(VendorColumnArray, "TDSApplicable")].ToString();
                                                            if (tds_applicable.ToLower() == "yes")
                                                            {
                                                                IDVM.TDS_APPLICABLE = true;
                                                                var tds_code = a[Array.IndexOf(VendorColumnArray, "TDSSectionCode")].ToString();
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

                                                            var pan_no = a[Array.IndexOf(VendorColumnArray, "PANNo")].ToString();
                                                            if (pan_no == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = pan_no.ToString();
                                                                errorMessage = "Add Pan No! .";
                                                            }
                                                            else
                                                            {
                                                                if (Regex.IsMatch(pan_no, @"[A-Z]{5}[0-9]{4}[A-Z]{1}"))
                                                                {
                                                                    IDVM.pan_no = pan_no;
                                                                }
                                                                else
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = pan_no.ToString() + " Add valid number";
                                                                    errorMessage = "Add valid Pan No! .";
                                                                }

                                                            }
                                                            IDVM.vat_tin_no = a[Array.IndexOf(VendorColumnArray, "VATTINNo")].ToString();
                                                            IDVM.cst_tin_no = a[Array.IndexOf(VendorColumnArray, "CSTTINNo")].ToString();
                                                            IDVM.service_tax_no = a[Array.IndexOf(VendorColumnArray, "ServiceTaxNo")].ToString();
                                                            IDVM.gst_no = a[Array.IndexOf(VendorColumnArray, "GSTNo")].ToString();
                                                            IDVM.ecc_no = a[Array.IndexOf(VendorColumnArray, "ECCNo")].ToString();
                                                            var bank_code = a[Array.IndexOf(VendorColumnArray, "BankCode")].ToString();
                                                            IDVM.bank_id = _Generic.GetBankId(bank_code);
                                                            //IDVM.bank_account_code = a[Array.IndexOf(VendorColumnArray, "BankAccountCode")].ToString();
                                                            //var account_type = a[Array.IndexOf(VendorColumnArray, "AccountType")].ToString();
                                                            //IDVM.account_type_id = _Generic.GetAccountTypeId(account_type);
                                                            IDVM.bank_account_number = a[Array.IndexOf(VendorColumnArray, "AccountNumber")].ToString();
                                                            IDVM.ifsc_code = a[Array.IndexOf(VendorColumnArray, "IFSCCode")].ToString();
                                                            var overall_discount = a[Array.IndexOf(VendorColumnArray, "Overall%Discount")].ToString();
                                                            if (overall_discount == "")
                                                            {
                                                                IDVM.overall_discount = 0;
                                                            }
                                                            else
                                                            {
                                                                IDVM.overall_discount = Double.Parse(overall_discount);
                                                            }

                                                            IDVM.ADDITIONAL_INFO = a[Array.IndexOf(VendorColumnArray, "AdditionalInfo")].ToString();
                                                            var blocked = a[Array.IndexOf(VendorColumnArray, "Blocked")].ToString();
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
                                                            IDVM.CREATED_ON = DateTime.Now;
                                                            IDVM.CREATED_BY = 1;
                                                            vendor_excel.Add(IDVM);
                                                        }
                                                        else
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = vendor_code;
                                                            errorMessage = "Vendor Code is duplicate!";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = vendor_code;
                                                        errorMessage = "Vendor Code already exist!";
                                                    }

                                                }
                                                else
                                                {
                                                    vendor_excel IDVM = new vendor_excel();
                                                    var Vendor_category = a[Array.IndexOf(VendorColumnArray, "VendorCategory")].ToString();
                                                    if (Vendor_category == "")
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = Vendor_category.ToString();
                                                        errorMessage = "Add Vendor category.";
                                                    }
                                                    else
                                                    {
                                                        var Vendor_category_id = _Generic.GetVendorCategoryId(Vendor_category);
                                                        if (Vendor_category_id == 0)
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = Vendor_category.ToString();
                                                            errorMessage = Vendor_category + " not found.";
                                                        }
                                                        else
                                                        {
                                                            IDVM.VENDOR_CATEGORY_ID = Vendor_category_id;
                                                        }
                                                    }
                                                    var VendorCode = a[Array.IndexOf(VendorColumnArray, "VendorCode")].ToString();
                                                    var VenCode = _Generic.GetVendorId(VendorCode);
                                                    if (VenCode == 0)
                                                    {
                                                        IDVM.VENDOR_CODE = VendorCode;
                                                        IDVM.VENDOR_NAME = a[Array.IndexOf(VendorColumnArray, "VendorName")].ToString();
                                                        var Vendor_display_name = a[Array.IndexOf(VendorColumnArray, "VendorDisplayName")].ToString();
                                                        if (Vendor_display_name == "")
                                                        {
                                                            IDVM.VENDOR_DISPLAY_NAME = IDVM.VENDOR_NAME;
                                                        }
                                                        else
                                                        {
                                                            IDVM.VENDOR_DISPLAY_NAME = Vendor_display_name;
                                                        }
                                                        var org_type = a[Array.IndexOf(VendorColumnArray, "OrganizationType")].ToString();
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
                                                        var billing_add = a[Array.IndexOf(VendorColumnArray, "BillingAddress")].ToString();
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
                                                        var billing_city = a[Array.IndexOf(VendorColumnArray, "BillingCity")].ToString();
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
                                                        var billing_pincode = a[Array.IndexOf(VendorColumnArray, "BillingPinCode")].ToString();
                                                        if (billing_pincode == "" || billing_pincode.Length < 6)
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = billing_pincode.ToString();
                                                            errorMessage = "Add Pincode.";
                                                        }
                                                        else if (billing_pincode.Length == 6)
                                                        {
                                                            IDVM.BILLING_PINCODE = int.Parse(billing_pincode);
                                                        }
                                                        var state = a[Array.IndexOf(VendorColumnArray, "BillingState")].ToString();
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
                                                        var priority = a[Array.IndexOf(VendorColumnArray, "Priority")].ToString();
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
                                                        var freightTerms = a[Array.IndexOf(VendorColumnArray, "FreightTerms")].ToString();
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
                                                        var linkToParent = a[Array.IndexOf(VendorColumnArray, "LinktoParent")].ToString();
                                                        if (linkToParent.ToLower() == "yes")
                                                        {
                                                            IDVM.HAS_PARENT = true;
                                                            var Vendor_parent_name = a[Array.IndexOf(VendorColumnArray, "ParentName")].ToString();
                                                            if (Vendor_parent_name == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = Vendor_parent_name.ToString();
                                                                errorMessage = Vendor_parent_name + " not Found.";
                                                            }
                                                            else
                                                            {
                                                                IDVM.VENDOR_PARENT_ID = _Generic.GetVendorParentId(Vendor_parent_name);
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
                                                            errorMessage = "Add only yes or no in Vendor sheet.";
                                                        }
                                                        //IDVM.CREATED_ON = DateTime.Now;
                                                        //IDVM.CREATED_BY = 1;
                                                        var WhetherSameAsBilling = a[Array.IndexOf(VendorColumnArray, "WhetherSameAsBilling")].ToString();
                                                        if (WhetherSameAsBilling.ToLower() == "no")
                                                        {
                                                            IDVM.CORR_ADDRESS = a[Array.IndexOf(VendorColumnArray, "CorrespondenceAddress")].ToString();
                                                            IDVM.CORR_CITY = a[Array.IndexOf(VendorColumnArray, "CorrespondenceCity")].ToString();
                                                            var corr_state = a[Array.IndexOf(VendorColumnArray, "CorrespondenceState")].ToString();
                                                            IDVM.CORR_STATE_ID = _Generic.GetStateID(corr_state);
                                                            var corr_pincode = a[Array.IndexOf(VendorColumnArray, "CorrespondencePinCode")].ToString();
                                                            if (corr_pincode == "" || corr_pincode.Length < 6)
                                                            {
                                                                IDVM.CORR_PINCODE = 0;
                                                            }
                                                            else if (corr_pincode.Length == 6)
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
                                                            errorMessage = "Add only yes or no in WhetherSameAsBilling .";
                                                        }

                                                        IDVM.EMAIL_ID_PRIMARY = a[Array.IndexOf(VendorColumnArray, "EmailID")].ToString();
                                                        IDVM.phone_code = a[Array.IndexOf(VendorColumnArray, "StdCode")].ToString();
                                                        IDVM.TELEPHONE_PRIMARY = a[Array.IndexOf(VendorColumnArray, "Phone")].ToString();
                                                        IDVM.FAX = a[Array.IndexOf(VendorColumnArray, "Fax")].ToString();
                                                        IDVM.WEBSITE_ADDRESS = a[Array.IndexOf(VendorColumnArray, "WebsiteAddress")].ToString();
                                                        var currency = a[Array.IndexOf(VendorColumnArray, "DefaultCurrency")].ToString();
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
                                                        var paymentTerms = a[Array.IndexOf(VendorColumnArray, "PaymentTerms")].ToString();
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
                                                        var paymentCycleType = a[Array.IndexOf(VendorColumnArray, "PaymentCycleType")].ToString();
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
                                                                var payment_cycle_data = a[Array.IndexOf(VendorColumnArray, "PaymentCycle")].ToString();
                                                                if (payment_cycle_data == "")
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = payment_cycle_data.ToString();
                                                                    errorMessage = "Add Payment Cycle Type.";
                                                                }
                                                                else
                                                                {
                                                                    var payment_cycle_data_id = payment_cycle.Where(x => x.PAYMENT_CYCLE_NAME == payment_cycle_data).FirstOrDefault();
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
                                                        var credit_limit = a[Array.IndexOf(VendorColumnArray, "CreditLimit")].ToString();
                                                        if (credit_limit == "")
                                                        {
                                                            IDVM.CREDIT_LIMIT = 0;
                                                        }
                                                        else
                                                        {
                                                            IDVM.CREDIT_LIMIT = Double.Parse(credit_limit);
                                                        }
                                                        var tds_applicable = a[Array.IndexOf(VendorColumnArray, "TDSApplicable")].ToString();
                                                        if (tds_applicable.ToLower() == "yes")
                                                        {
                                                            IDVM.TDS_APPLICABLE = true;
                                                            var tds_code = a[Array.IndexOf(VendorColumnArray, "TDSSectionCode")].ToString();
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
                                                        var pan_no = a[Array.IndexOf(VendorColumnArray, "PANNo")].ToString();
                                                        if (pan_no == "")
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = pan_no.ToString();
                                                            errorMessage = "Add Pan No! .";
                                                        }
                                                        else
                                                        {
                                                            if (Regex.IsMatch(pan_no, @"[A-Z]{5}[0-9]{4}[A-Z]{1}"))
                                                            {
                                                                IDVM.pan_no = pan_no;
                                                            }
                                                            else
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = pan_no.ToString() + " Add valid number";
                                                                errorMessage = "Add valid Pan No! .";
                                                            }

                                                        }
                                                        IDVM.vat_tin_no = a[Array.IndexOf(VendorColumnArray, "VATTINNo")].ToString();
                                                        IDVM.cst_tin_no = a[Array.IndexOf(VendorColumnArray, "CSTTINNo")].ToString();
                                                        IDVM.service_tax_no = a[Array.IndexOf(VendorColumnArray, "ServiceTaxNo")].ToString();
                                                        IDVM.gst_no = a[Array.IndexOf(VendorColumnArray, "GSTNo")].ToString();
                                                        IDVM.ecc_no = a[Array.IndexOf(VendorColumnArray, "ECCNo")].ToString();

                                                        var bank_code = a[Array.IndexOf(VendorColumnArray, "BankCode")].ToString();
                                                        IDVM.bank_id = _Generic.GetBankId(bank_code);
                                                        //IDVM.bank_account_code = a[Array.IndexOf(VendorColumnArray, "BankAccountCode")].ToString();
                                                        //var account_type = a[Array.IndexOf(VendorColumnArray, "AccountType")].ToString();
                                                        //IDVM.account_type_id = _Generic.GetAccountTypeId(account_type);
                                                        IDVM.bank_account_number = a[Array.IndexOf(VendorColumnArray, "AccountNumber")].ToString();
                                                        IDVM.ifsc_code = a[Array.IndexOf(VendorColumnArray, "IFSCCode")].ToString();

                                                        var overall_discount = a[Array.IndexOf(VendorColumnArray, "Overall%Discount")].ToString();
                                                        if (overall_discount == "")
                                                        {
                                                            IDVM.overall_discount = 0;
                                                        }
                                                        else
                                                        {
                                                            IDVM.overall_discount = Double.Parse(overall_discount);
                                                        }

                                                        IDVM.ADDITIONAL_INFO = a[Array.IndexOf(VendorColumnArray, "AdditionalInfo")].ToString();
                                                        var blocked = a[Array.IndexOf(VendorColumnArray, "Blocked")].ToString();
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
                                                        IDVM.CREATED_ON = DateTime.Now;
                                                        IDVM.CREATED_BY = 1;
                                                        vendor_excel.Add(IDVM);
                                                    }
                                                    else
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = VendorCode;
                                                        errorMessage = "Vendor Code is duplicate!";
                                                    }
                                                }
                                            }

                                        }
                                        else
                                        {
                                            errorList++;
                                            error[error.Length - 1] = "Check Headers name.";
                                            errorMessage = "Check header !";
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
                                                if (vendor_contact_excel.Count != 0)
                                                {
                                                    vendor_contact_excel CE = new vendor_contact_excel();
                                                    CE.VENDOR_CODE = a[Array.IndexOf(ContactColumnArray, "VendorCode")].ToString();
                                                    var VendorCode = vendor_excel.Where(x => x.VENDOR_CODE == CE.VENDOR_CODE).FirstOrDefault();
                                                    if (VendorCode != null)
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

                                                        vendor_contact_excel.Add(CE);
                                                    }
                                                    else
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = CE.VENDOR_CODE;
                                                        errorMessage = "Vendor Code not find!";
                                                    }

                                                }
                                                else
                                                {
                                                    vendor_contact_excel CE = new vendor_contact_excel();
                                                    CE.VENDOR_CODE = a[Array.IndexOf(ContactColumnArray, "VendorCode")].ToString();
                                                    var Vendor_code = vendor_excel.Where(x => x.VENDOR_CODE == CE.VENDOR_CODE).FirstOrDefault();
                                                    if (Vendor_code == null)
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = CE.VENDOR_CODE + "Contact Sheet";
                                                        errorMessage = "Vendor Code not find in Contact!";
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
                                                        vendor_contact_excel.Add(CE);
                                                    }

                                                }
                                            }

                                        }
                                        else
                                        {
                                            errorList++;
                                            error[error.Length - 1] = "Check Headers name.";
                                            errorMessage = "Check header !";
                                        }
                                    }
                                }
                            }
                            else if (sheet.TableName == "ProdCategory" && errorMessage == "")
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
                                                    vendor_item_excel IE = new vendor_item_excel();
                                                    var VendorCode = a[Array.IndexOf(ProCategoryColumnArray, "VendorCode")].ToString();
                                                    var Vendor_code = vendor_excel.Where(x => x.VENDOR_CODE == VendorCode).FirstOrDefault();
                                                    if (Vendor_code == null)
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = IE.VENDOR_CODE;
                                                        errorMessage = "Vendor Code not find!";
                                                    }
                                                    else
                                                    {
                                                        var item_category = a[Array.IndexOf(ProCategoryColumnArray, "ProdCategory")].ToString();
                                                        if (item_category == "")
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = item_category;
                                                            errorMessage = "Add Item Category!";
                                                        }
                                                        else
                                                        {
                                                            var item_category_id = _Generic.GetItemCategoryId(item_category);
                                                            if (item_category_id == 0)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = item_category;
                                                                errorMessage = item_category + " not found!";
                                                            }
                                                            else
                                                            {
                                                                var duplicate_Vendor_Code = item_excel.Where(x => x.VENDOR_CODE == VendorCode).FirstOrDefault();
                                                                if (duplicate_Vendor_Code == null)
                                                                {
                                                                    IE.VENDOR_CODE = VendorCode;
                                                                    IE.item_category_id = item_category_id;

                                                                }
                                                                else
                                                                {
                                                                    if (duplicate_Vendor_Code.item_category_id == item_category_id)
                                                                    {

                                                                        errorList++;
                                                                        error[error.Length - 1] = item_category;
                                                                        errorMessage = item_category + " duplicate in procategory!";
                                                                    }
                                                                    else
                                                                    {
                                                                        IE.VENDOR_CODE = VendorCode;
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
                                                    vendor_item_excel IE = new vendor_item_excel();
                                                    IE.VENDOR_CODE = a[Array.IndexOf(ProCategoryColumnArray, "VendorCode")].ToString();
                                                    var Vendor_code = vendor_excel.Where(x => x.VENDOR_CODE == IE.VENDOR_CODE).FirstOrDefault();
                                                    if (Vendor_code == null)
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = IE.VENDOR_CODE;
                                                        errorMessage = "Vendor Code not find!";
                                                    }
                                                    else
                                                    {
                                                        IE.VENDOR_CODE = a[Array.IndexOf(ProCategoryColumnArray, "VendorCode")].ToString();
                                                        var item_category = a[Array.IndexOf(ProCategoryColumnArray, "ProdGroup")].ToString();
                                                        if (item_category == "")
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = item_category;
                                                            errorMessage = "Add Item Category!";
                                                        }
                                                        else
                                                        {
                                                            var item_category_id = _Generic.GetItemCategoryId(item_category);
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
                                        else
                                        {
                                            errorList++;
                                            error[error.Length - 1] = "Check Headers name.";
                                            errorMessage = "Check header !";
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
                                    var customerExcelCount = vendor_excel.Count;
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
                                                    vendor_gl_excel GE = new vendor_gl_excel();
                                                    var Vendor_code = a[Array.IndexOf(GLColumnArray, "VendorCode")].ToString();
                                                    var VendorCode = vendor_excel.Where(x => x.VENDOR_CODE == Vendor_code).FirstOrDefault();
                                                    if (Vendor_code == null)
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = GE.VENDOR_CODE;
                                                        errorMessage = "GL Vendor Code not find!";
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
                                                            var gl_account_type_id = _Generic.GetLedgerAccountTypeId(gl_account_type, 2);
                                                            if (gl_account_type_id != 0)
                                                            {
                                                                var duplicate_Vendor_code = gl_excel.Where(x => x.VENDOR_CODE == Vendor_code).ToList();
                                                                if (duplicate_Vendor_code.Count == 0)
                                                                {
                                                                    GE.VENDOR_CODE = Vendor_code;
                                                                    GE.ledger_account_type_id = gl_account_type_id;
                                                                    vendor_duplicateglexcle dupli = new vendor_duplicateglexcle();
                                                                    dupli.VENDOR_CODE = GE.VENDOR_CODE;
                                                                    duplicateglexcle.Add(dupli);
                                                                    customerExcelCount--;
                                                                }
                                                                else if (duplicate_Vendor_code.Count < 2)
                                                                {
                                                                    if (duplicate_Vendor_code.FirstOrDefault().ledger_account_type_id == gl_account_type_id)
                                                                    {
                                                                        errorList++;
                                                                        error[error.Length - 1] = gl_account_type;
                                                                        errorMessage = gl_account_type + " duplicate!";
                                                                    }
                                                                    else
                                                                    {

                                                                        GE.VENDOR_CODE = Vendor_code;
                                                                        GE.ledger_account_type_id = gl_account_type_id;
                                                                        var removedupli = duplicateglexcle.Where(x => x.VENDOR_CODE == GE.VENDOR_CODE).FirstOrDefault();
                                                                        duplicateglexcle.Remove(removedupli);
                                                                    }
                                                                }
                                                                else if (duplicate_Vendor_code.Count >= 2)
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
                                                    vendor_gl_excel GE = new vendor_gl_excel();
                                                    var Vendor_code = a[Array.IndexOf(GLColumnArray, "VendorCode")].ToString();
                                                    var VendorCode = vendor_excel.Where(x => x.VENDOR_CODE == Vendor_code).FirstOrDefault();
                                                    if (Vendor_code == null)
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = GE.VENDOR_CODE;
                                                        errorMessage = "GL Vendor Code not find!";
                                                    }
                                                    else
                                                    {
                                                        GE.VENDOR_CODE = Vendor_code;
                                                        var gl_account_type = a[Array.IndexOf(GLColumnArray, "GLAccountType")].ToString();
                                                        if (gl_account_type == "")
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = gl_account_type;
                                                            errorMessage = "Add gl_account type!";
                                                        }
                                                        else
                                                        {
                                                            var gl_account_type_id = _Generic.GetLedgerAccountTypeId(gl_account_type, 2);
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
                                                    vendor_duplicateglexcle dupli = new vendor_duplicateglexcle();
                                                    dupli.VENDOR_CODE = GE.VENDOR_CODE;
                                                    duplicateglexcle.Add(dupli);
                                                    customerExcelCount--;
                                                    gl_excel.Add(GE);
                                                }

                                            }

                                        }
                                        else
                                        {
                                            errorList++;
                                            error[error.Length - 1] = "Check Headers name.";
                                            errorMessage = "Check header name !";
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
                            var isSucess = _vendorService.AddExcel(vendor_excel, vendor_contact_excel, item_excel, gl_excel);
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
        public Boolean ValidateVendorExcelColumns(string[] VendorColumnArray)
        {
            if (VendorColumnArray.Contains("SrNo") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("VendorCategory") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("VendorCode") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("VendorName") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("VendorDisplayName") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("OrganizationType") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("BillingAddress") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("BillingCity") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("BillingPinCode") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("BillingCountry") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("BillingState") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("Priority") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("FreightTerms") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("LinktoParent") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("ParentName") == false)
            {
                return false;
            }
            //if (VendorColumnArray.Contains("CreatedOn") == false)
            //{
            //    return false;
            //}
            if (VendorColumnArray.Contains("WhetherSameAsBilling") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("CorrespondenceAddress") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("CorrespondenceCity") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("CorrespondencePinCode") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("CorrespondenceCountry") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("CorrespondenceState") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("EmailID") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("StdCode") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("Phone") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("Fax") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("WebsiteAddress") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("DefaultCurrency") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("PaymentTerms") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("PaymentCycleType") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("PaymentCycle") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("CreditLimit") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("TDSApplicable") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("TDSSectionCode") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("PANNo") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("VATTINNo") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("CSTTINNo") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("ServiceTaxNo") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("GSTNo") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("ECCNo") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("BankCode") == false)
            {
                return false;
            }
            //if (VendorColumnArray.Contains("BankAccountCode") == false)
            //{
            //    return false;
            //}
            //if (VendorColumnArray.Contains("AccountType") == false)
            //{
            //    return false;
            //}
            if (VendorColumnArray.Contains("AccountNumber") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("IFSCCode") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("Overall%Discount") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("AdditionalInfo") == false)
            {
                return false;
            }
            if (VendorColumnArray.Contains("Blocked") == false)
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
            if (ContactColumnArray.Contains("VendorCode") == false)
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
            if (ProCategoryColumnArray.Contains("VendorCode") == false)
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
            if (GLColumnArray.Contains("VendorCode") == false)
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
