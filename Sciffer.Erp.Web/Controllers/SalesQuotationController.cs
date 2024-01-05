using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.ViewModel;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class SalesQuotationController : Controller
    {
        private readonly ISalQuotationService _quotationService;
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
        private readonly IStorageLocation _storage;
        private readonly IGenericService _Generic;
        public SalesQuotationController(ISalQuotationService quotationService, ICurrencyService currencyService, ISalesCategoryService salecategoryService, IFreightTermsService freightTermsService,
            IPaymentCycleTypeService paymentCycleType, IPaymentTermsService paymentTerms, IStateService stateService, ITerritoryService territory, IPlantService plant,
            IUserService userService, IBusinessUnitService businessunit, ICountryService Country, IPaymentCycleService paymentService, IFormService Form, ICustomerService customerService,
            IItemService itemservice, IUOMService unit, ITaxService tax, IStorageLocation storage, IGenericService gen)
        {
            _Tax = tax;
            _quotationService = quotationService;
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
            _storage = storage;
            _Generic = gen;
        }
        // GET: Quotation
        [CustomAuthorizeAttribute("SALQ")]
        public ActionResult Index()
        {
            ViewBag.doc = TempData["doc_num"];
            ViewBag.DataSource = _quotationService.getall();
            return View();
        }
       public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var quotationvm = _quotationService.Get((int)id);
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
            ViewBag.tax = _Tax.GetAll();
            ViewBag.customer_detail = quotationvm.REF_CUSTOMER.CUSTOMER_CODE + "-" + quotationvm.REF_CUSTOMER.CUSTOMER_NAME;
            if (quotationvm.CONSIGNEE_ID != null)
            {
                if (quotationvm.CONSIGNEE_ID != 0)
                {
                    ViewBag.customer1_detail = quotationvm.REF_CUSTOMER1.CUSTOMER_CODE + "-" + quotationvm.REF_CUSTOMER1.CUSTOMER_NAME;
                }
            }
            ViewBag.locationlist = new SelectList(_storage.GetAll(), "storage_location_id", "storage_location_name");
            ViewBag.buyer_detail = quotationvm.BUYER_CODE + "-" + quotationvm.BUYER_NAME;
            ViewBag.consignee_detail = quotationvm.CONSIGNEE_CODE + "-" + quotationvm.CONSIGNEE_NAME;
            ViewBag.datasource = _customerService.GetCustomerList();
            ViewBag.item = _itemservice.GetItemList();
            ViewBag.unitlist = new SelectList(unit, "UOM_ID", "UOM_NAME");
            ViewBag.itemlist = new SelectList(item, "ITEM_ID", "ITEM_NAME");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(3), "document_numbring_id", "category");
            ViewBag.plantlist = new SelectList(plant, "PLANT_ID", "PLANT_NAME");
            ViewBag.freightlist = new SelectList(freightterms, "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.businesslist = new SelectList(business, "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.paymentcyclelist = new SelectList(paymentcycle, "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.paymentcycletype = new SelectList(paymentcycletype, "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.paymenttermslist = new SelectList(paymentterms, "PAYMENT_TERMS_ID", "PAYMENT_TERMS_CODE");
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
            ViewBag.taxlist = new SelectList(tax, "TAX_ID", "TAX_CODE");
            
            return View(quotationvm);
        }

        // GET: Quotation/Create
        [CustomAuthorizeAttribute("SALQ")]
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
            ViewBag.unitlist = new SelectList(unit, "UOM_ID", "UOM_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(64), "document_numbring_id", "category");
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
            return View();
        }

        // POST: Quotation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SAL_QUOTATION_VM sAL_QUOTATION)
        {
            if (ModelState.IsValid)
            {
                var isValid = _quotationService.Add(sAL_QUOTATION);
                if (isValid != "error")
                {
                    TempData["doc_num"] = isValid;
                    return RedirectToAction("Index", TempData["doc_num"]);
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
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(64), "document_numbring_id", "category");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.freightlist = new SelectList(freightterms, "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.businesslist = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.paymentcyclelist = new SelectList(paymentcycle, "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.paymentcycletype = new SelectList(paymentcycletype, "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.paymenttermslist = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.billingstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.corrstatelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.territorylist = new SelectList(territory, "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.createdbylist = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.salesexlist = new SelectList(_Generic.GetSalesRM(), "employee_id", "employee_name");
            ViewBag.billinglist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.corlist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.formlist = new SelectList(frm, "FORM_ID", "FORM_NAME");
            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.taxlist = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            return View();
        }

        // GET: Quotation/Edit/5
        [CustomAuthorizeAttribute("SALQ")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.error = "";
            var quotationvm = _quotationService.Get((int)id);
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
            ViewBag.unitlist = new SelectList(unit, "UOM_ID", "UOM_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(64), "document_numbring_id", "category");
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
            return View(quotationvm);
        }

        // POST: Quotation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SAL_QUOTATION_VM sAL_QUOTATION)
        { 
           
            if (ModelState.IsValid)
            {
                var isedit = _quotationService.Add(sAL_QUOTATION);
                if (isedit != "error")
                {
                    TempData["doc_num"] = isedit;
                    return RedirectToAction("Index", TempData["doc_num"]);
                }

            }
            ViewBag.error = "Something went wrong !";
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
            ViewBag.unitlist = new SelectList(unit, "UOM_ID", "UOM_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(64), "document_numbring_id", "category");
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
            return View();
        }

        // GET: Quotation/Delete/5
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //SAL_QUOTATION sAL_QUOTATION = db.SAL_QUOTATION.Find(id);
            //if (sAL_QUOTATION == null)
            //{
            //    return HttpNotFound();
            //}
            return View();
        }

        // POST: Quotation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //SAL_QUOTATION sAL_QUOTATION = db.SAL_QUOTATION.Find(id);
            //db.SAL_QUOTATION.Remove(sAL_QUOTATION);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _quotationService.Dispose();
            }
            base.Dispose(disposing);
        }
        public CrystalReportPdfResult Pdf(int id)
        {
            ReportDocument rd = new ReportDocument();
            try
            {
                var quotation = _quotationService.GetQuotationDetailForReport(id);
                var quotation_detail = _quotationService.GetQuotationProductForReport(id);
                var exp = "";
                double amt = 0;
                foreach (var i in quotation_detail)
                {
                    exp = exp + i.tax_id + "~" + i.assessable_value + "~" + i.sales_value + ",";
                    amt = amt + i.sales_value;
                }
                var tax_detail = _Generic.GetTaxCalculation("calculatetax", exp.Remove(exp.Length - 1), amt, quotation.quotation_date, 0);
                var quotation_detail_id = 0;
                DataSet ds = new DataSet("SAL_SO");
                var salquotation = new List<sal_quotation_report_vm>();
                salquotation.Add(quotation);
                var dt1 = _Generic.ToDataTable(salquotation);
                var dt2 = _Generic.ToDataTable(quotation_detail);
                var dt3 = _Generic.ToDataTable(tax_detail);
                dt1.TableName = "sal_quotation";
                dt2.TableName = "sal_quotation_detail";
                dt3.TableName = "sq_tax_detail";
                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);
                dt3.Columns.Add("id", typeof(System.Int32));
                dt1.Columns.Add("excise_amount", typeof(double));
                foreach (var so in quotation_detail)
                {
                    quotation_detail_id = (int)so.quotation_detail_id;
                }
                foreach (DataRow row in dt3.Rows)
                {
                    //need to set value to NewColumn column
                    row["id"] = quotation_detail_id;   // or set it to some other value
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
               
                rd.Load(Path.Combine(Server.MapPath("~/Reports/SalQuotationReport.rpt")));
                rd.SetDataSource(ds);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string reportPath = Path.Combine(Server.MapPath("~/Reports"), "SalQuotationReport.rpt");
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

    }
}
