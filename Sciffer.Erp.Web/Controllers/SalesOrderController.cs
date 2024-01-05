using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System.Linq;
using Newtonsoft.Json;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Data;
using CrystalDecisions.Shared;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class SalesOrderController : Controller
    {
        private readonly IStorageLocation _storageLocation;
        private readonly ISalSoservice _SOService;
        private readonly ICurrencyService _currencyService;
        private readonly ISalesCategoryService _salecategoryService;
        private readonly IFreightTermsService _freightTermsService;
        private readonly IPaymentCycleTypeService _paymentCycleType;
        private readonly IPaymentTermsService _paymentTerms;
        private readonly IPaymentCycleService _paymentService;
        private readonly IStateService _stateService;
        private readonly ITerritoryService _territory;
        private readonly IPlantService _plant;
        private readonly IUserService _userService;
        private readonly IBusinessUnitService _businessunit;
        private readonly ICountryService _Country;
        private readonly IFormService _Form;
        private readonly ICustomerService _customerService;
        private readonly IItemService _itemservice;
        private readonly IUOMService _Unit;
        private readonly ITaxService _Tax;
        private readonly IGenericService _Generic;
        private readonly ISalQuotationService _quotation;
      
        public SalesOrderController(IStorageLocation StorageLocation, IGenericService gen, ISalSoservice soService, ICurrencyService currencyService, ISalesCategoryService salecategoryService, IFreightTermsService freightTermsService,
            IPaymentCycleTypeService paymentCycleType, IPaymentTermsService paymentTerms, IStateService stateService, ITerritoryService territory, IPlantService plant,
            IUserService userService, IBusinessUnitService businessunit, ICountryService Country, IPaymentCycleService paymentService, IFormService Form, ICustomerService customerService,
            IItemService itemservice, IUOMService unit, ITaxService tax, ISalQuotationService quotation)
        {
            _quotation = quotation;
            _Tax = tax;
            _SOService = soService;
            _Country = Country;
            _Unit = unit;
            _businessunit = businessunit;
            _userService = userService;
            _plant = plant;
            _territory = territory;
            _stateService = stateService;
            _paymentService = paymentService;
            _paymentTerms = paymentTerms;
            _paymentCycleType = paymentCycleType;
            _freightTermsService = freightTermsService;
            _salecategoryService = salecategoryService;
            _currencyService = currencyService;
            _Form = Form;
            _customerService = customerService;
            _itemservice = itemservice;
            _Generic = gen;
            _storageLocation = StorageLocation;
        }
        // GET: SalesOrder
        [CustomAuthorizeAttribute("SAL0")]
        public ActionResult Index()
        {
            ViewBag.doc = TempData["doc_num"];
            ViewBag.DataSource = _SOService.getall();
            return View();
        }
       
        // GET: SalesOrder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sonvm = _SOService.Get((int)id);
            var tax = _Tax.GetAll();
            var currencylist = _currencyService.GetAll();
            var salescategorylist = _salecategoryService.GetAll();
            var business = _businessunit.GetAll();
            var freightterms = _freightTermsService.GetAll();
            var plant = _plant.GetAll();
            var paymentcycletype = _paymentCycleType.GetAll();
            var paymentterms = _paymentTerms.GetAll();
            var paymentcycle = _paymentService.GetAll();
            var statelist = _stateService.GetAll();
            var territory = _territory.GetAll();
            var user = _userService.GetAll();
            var country = _Country.GetAll();
            var frm = _Form.GetAll();
            var customer = _customerService.GetAll();
            var item = _itemservice.GetAll();
            var unit = _Unit.GetAll();
            ViewBag.customer_detail = sonvm.REF_CUSTOMER.CUSTOMER_CODE + "-" + sonvm.REF_CUSTOMER.CUSTOMER_NAME;
            if (sonvm.consignee_id != null)
            {
                if (sonvm.consignee_id != 0)
                {
                    ViewBag.customer1_detail = sonvm.REF_CUSTOMER1.CUSTOMER_CODE + "-" + sonvm.REF_CUSTOMER1.CUSTOMER_NAME;
                }
            }
            ViewBag.datasource = _customerService.GetCustomerList();
            ViewBag.item = _itemservice.GetItemList();
            ViewBag.tax = _Tax.GetAll();
            ViewBag.unitlist = new SelectList(unit, "UOM_ID", "UOM_NAME");
            ViewBag.itemlist = new SelectList(item, "ITEM_ID", "ITEM_NAME");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(salescategorylist, "SALES_CATEGORY_ID", "SALES_CATEGORY_NAME");
            ViewBag.plantlist = new SelectList(plant, "PLANT_ID", "PLANT_NAME");
            ViewBag.freightlist = new SelectList(freightterms, "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.businesslist = new SelectList(business, "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.paymentcyclelist = new SelectList(paymentcycle, "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.paymentcycletype = new SelectList(paymentcycletype, "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.paymenttermslist = new SelectList(paymentterms, "payment_terms_id", "payment_terms_code");
            ViewBag.billingstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.corrstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.territorylist = new SelectList(territory, "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.salesexlist = new SelectList(_Generic.GetSalesRM(), "employee_id", "employee_name");
            ViewBag.createdbylist = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.billinglist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.corlist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.formlist = new SelectList(frm, "FORM_ID", "FORM_NAME");
            ViewBag.buyernamelist = new SelectList(customer, "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.buyercodelist = new SelectList(customer, "CUSTOMER_ID", "CUSTOMER_CODE");
            ViewBag.consigneenamelist = new SelectList(customer, "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.consigneecodelist = new SelectList(customer, "CUSTOMER_ID", "CUSTOMER_CODE");
            ViewBag.taxlist = new SelectList(tax, "tax_id", "tax_code");
            ViewBag.storagelist = new SelectList(_storageLocation.GetAll(), "storage_location_id", "storage_location_name");
            ViewBag.tds_list = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.GST_TDS_Code = new SelectList(_Generic.GetGSTTDSCodeList(), "gst_tds_code_id", "gst_tds_code");
            return View(sonvm);
        }

        // GET: SalesOrder/Create
        [CustomAuthorizeAttribute("SAL0")]
        public ActionResult Create()
        {
            var currencylist = _currencyService.GetAll();
            var freightterms = _freightTermsService.GetAll();           
            var paymentcycletype = _paymentCycleType.GetAll();
            var paymentcycle = _paymentService.GetAll();
            var statelist = _stateService.GetAll();
            var territory = _territory.GetAll();
            var user = _userService.GetAll();
            var country = _Country.GetAll();
            var frm = _Form.GetAll();            
            var unit = _Unit.GetAll();
            ViewBag.error = "";
            ViewBag.QuotationList = new SelectList(_quotation.GetQuotationDeatilForSo(), "QUOTATION_ID", "QUOTATION_NUMBER");
            ViewBag.unitlist = new SelectList(unit, "UOM_ID", "UOM_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(65), "document_numbring_id", "category");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.freightlist = new SelectList(freightterms, "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.businesslist = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.paymentcyclelist = new SelectList(paymentcycle, "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.paymentcycletype = new SelectList(paymentcycletype, "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.paymenttermslist = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.billingstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.corrstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.territorylist = new SelectList(territory, "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.salesexlist = new SelectList(_Generic.GetSalesRM(), "employee_id", "employee_name");
            ViewBag.createdbylist = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.billinglist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.corlist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.formlist = new SelectList(frm, "FORM_ID", "FORM_NAME");
            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.taxlist = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.tds_list = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.GST_TDS_Code = new SelectList(_Generic.GetGSTTDSCodeList(), "gst_tds_code_id", "gst_tds_code");
            return View();
        }

        // POST: SalesOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( sal_so_vm sAL_SO)
        {
            if (ModelState.IsValid)
            {
                var isValid = _SOService.Add(sAL_SO);
                if (isValid != "error")
                {
                    TempData["doc_num"] = isValid;
                    return RedirectToAction("Index");
                }
            }
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    var err = error.ErrorMessage;
                }
            }
            var currencylist = _currencyService.GetAll();
            var freightterms = _freightTermsService.GetAll();
            var paymentcycletype = _paymentCycleType.GetAll();
            var paymentcycle = _paymentService.GetAll();
            var statelist = _stateService.GetAll();
            var territory = _territory.GetAll();
            var user = _userService.GetAll();
            var country = _Country.GetAll();
            var frm = _Form.GetAll();
            var unit = _Unit.GetAll();
            ViewBag.error = "Something went wrong !";
            ViewBag.QuotationList = new SelectList(_quotation.GetQuotationDeatilForSo(), "QUOTATION_ID", "QUOTATION_NUMBER");
            ViewBag.unitlist = new SelectList(unit, "UOM_ID", "UOM_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(65), "document_numbring_id", "category");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.freightlist = new SelectList(freightterms, "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.businesslist = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.paymentcyclelist = new SelectList(paymentcycle, "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.paymentcycletype = new SelectList(paymentcycletype, "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.paymenttermslist = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.billingstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.corrstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.territorylist = new SelectList(territory, "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.salesexlist = new SelectList(_Generic.GetSalesRM(), "employee_id", "employee_name");
            ViewBag.createdbylist = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.billinglist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.corlist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.formlist = new SelectList(frm, "FORM_ID", "FORM_NAME");
            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.taxlist = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.tds_list = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.GST_TDS_Code = new SelectList(_Generic.GetGSTTDSCodeList(), "gst_tds_code_id", "gst_tds_code");
            return View();
        }

        // GET: SalesOrder/Edit/5
        [CustomAuthorizeAttribute("SAL0")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sonvm = _SOService.Get((int)id);
            var currencylist = _currencyService.GetAll();
            var freightterms = _freightTermsService.GetAll();
            var paymentcycletype = _paymentCycleType.GetAll();
            var paymentcycle = _paymentService.GetAll();
            var statelist = _stateService.GetAll();
            var territory = _territory.GetAll();
            var user = _userService.GetAll();
            var country = _Country.GetAll();
            var frm = _Form.GetAll();
            var unit = _Unit.GetAll();
            ViewBag.error = "";
            ViewBag.unitlist = new SelectList(unit, "UOM_ID", "UOM_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(65), "document_numbring_id", "category");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.freightlist = new SelectList(freightterms, "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.businesslist = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.paymentcyclelist = new SelectList(paymentcycle, "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.paymentcycletype = new SelectList(paymentcycletype, "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.paymenttermslist = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.billingstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.corrstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.territorylist = new SelectList(territory, "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.salesexlist = new SelectList(_Generic.GetSalesRM(), "employee_id", "employee_name");
            ViewBag.createdbylist = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.billinglist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.corlist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.formlist = new SelectList(frm, "FORM_ID", "FORM_NAME");
            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.taxlist = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.tds_list = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.GST_TDS_Code = new SelectList(_Generic.GetGSTTDSCodeList(), "gst_tds_code_id", "gst_tds_code");
            return View(sonvm);
        }
        // POST: SalesOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(sal_so_vm sAL_SO)
        { 
            if (ModelState.IsValid)
            {
                var isValid = _SOService.Add(sAL_SO);
                if (isValid != "error")
                {
                    TempData["doc_num"] = isValid;
                    return RedirectToAction("Index");
                }

            }
            var currencylist = _currencyService.GetAll();
            var freightterms = _freightTermsService.GetAll();
            var paymentcycletype = _paymentCycleType.GetAll();
            var paymentcycle = _paymentService.GetAll();
            var statelist = _stateService.GetAll();
            var territory = _territory.GetAll();
            var user = _userService.GetAll();
            var country = _Country.GetAll();
            var frm = _Form.GetAll();
            var unit = _Unit.GetAll();
            ViewBag.error = "Something went wrong !";
            ViewBag.unitlist = new SelectList(unit, "UOM_ID", "UOM_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(65), "document_numbring_id", "category");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.freightlist = new SelectList(freightterms, "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.businesslist = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.paymentcyclelist = new SelectList(paymentcycle, "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.paymentcycletype = new SelectList(paymentcycletype, "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.paymenttermslist = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.billingstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.corrstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.territorylist = new SelectList(territory, "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.salesexlist = new SelectList(_Generic.GetSalesRM(), "employee_id", "employee_name");
            ViewBag.createdbylist = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.billinglist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.corlist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.formlist = new SelectList(frm, "FORM_ID", "FORM_NAME");
            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.taxlist = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.tds_list = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.GST_TDS_Code = new SelectList(_Generic.GetGSTTDSCodeList(), "gst_tds_code_id", "gst_tds_code");
            return View();
        }

        // GET: SalesOrder/Delete/5
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //SAL_SO sAL_SO = db.SAL_SO.Find(id);
            //if (sAL_SO == null)
            //{
            //    return HttpNotFound();
            //}
            return View();
        }

        // POST: SalesOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //SAL_SO sAL_SO = db.SAL_SO.Find(id);
            //db.SAL_SO.Remove(sAL_SO);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _SOService.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult GetQuotationProductDetail(string code)
        {
            var vm = _quotation.GetQuotationProductDetail(code);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetQuotationForSO(int id)
        {
            var vm = _quotation.GetQuotationForSO(id);           
            var list = JsonConvert.SerializeObject(vm,
             Formatting.None,
                      new JsonSerializerSettings()
                      {
                          ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                      });

            return Content(list, "application/json");
        }
        public CrystalReportPdfResult Pdf(int id)
        {
            ReportDocument rd = new ReportDocument();
            try
            {
                var sal_so = _SOService.GetSoReport(id);
                var sal_so_detail = _SOService.GetSOProductList(id);
                var exp = "";
                double amt = 0;
                foreach (var i in sal_so_detail)
                {
                    exp = exp + i.tax_id + "~" + i.assessable_value + "~" + i.sales_value + ",";
                    amt = amt + i.sales_value;
                }
                var tax_detail = _Generic.GetTaxCalculation("calculatetax", exp.Remove(exp.Length - 1), amt, sal_so.so_date, 0);
                var so_detail_id = 0;
                DataSet ds = new DataSet("SAL_SO");
                var salso = new List<sal_so_report_vm>();
                salso.Add(sal_so);
                var dt1 = _Generic.ToDataTable(salso);
                var dt2 = _Generic.ToDataTable(sal_so_detail);
                var dt3 = _Generic.ToDataTable(tax_detail);
                dt1.TableName = "SAL_SO";
                dt2.TableName = "SAL_SO_DETAIL";
                dt3.TableName = "so_tax_detail";
                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);
                dt3.Columns.Add("id", typeof(System.Int32));
                dt1.Columns.Add("excise_amount", typeof(double));
                foreach (var so in sal_so_detail)
                {
                    so_detail_id = so.so_detail_id;
                }
                foreach (DataRow row in dt3.Rows)
                {
                    //need to set value to NewColumn column
                    row["id"] = so_detail_id;   // or set it to some other value
                                                // break;
                }
                DataRow[] drPaytable = dt3.Select("tax_name like '%Excise%'");
                double sum = 0;
                for (int i = 0; i < drPaytable.Length; i++)
                {
                    sum = sum + double.Parse(drPaytable[0].ItemArray[1].ToString());
                }
                dt1.Rows[0]["excise_amount"] = sum;
                ds.Tables.Add(dt3);
              
                rd.Load(Path.Combine(Server.MapPath("~/Reports/SalesOrderReport.rpt")));
                rd.SetDataSource(ds);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string reportPath = Path.Combine(Server.MapPath("~/Reports"), "SalesOrderReport.rpt");
                return new CrystalReportPdfResult(reportPath, rd);
            }
            catch (Exception ex)
            {
                //--------------Log4Net
              
                return null;
            }
            finally
            {
                rd.Close();
                rd.Clone();
                rd.Dispose();
                rd = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public ActionResult GetQuotationNumber(int id)
        {
            var ListQuotation = _quotation.GetQuotationDeatilForSo(id);
           // ViewBag.QuotationNumber = new SelectList(ListQuotation, "QUOTATION_ID", "quotation_No_Date");
            return Json(new { item = ListQuotation }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CloseConfirmed(int id, string remarks)
        {
            var isValid = _SOService.Close(id, remarks);
            return Json(isValid);
        }
    }
}
