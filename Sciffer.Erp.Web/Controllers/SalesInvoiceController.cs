using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.ViewModel;
using Newtonsoft.Json;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class SalesInvoiceController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        private readonly ISalesInvoiceService _SIService;
        private readonly ISalSoservice _soservice;
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
        private readonly IStorageLocation _storage;
        public readonly ITdsCodeService _TDScodeService;
        public readonly IModeOfTransportService _Transport;
        public SalesInvoiceController(ISalesInvoiceService SIService, ICurrencyService currencyService, ISalesCategoryService salecategoryService, IFreightTermsService freightTermsService,
            IPaymentCycleTypeService paymentCycleType, IPaymentTermsService paymentTerms, IStateService stateService, ITerritoryService territory, IPlantService plant, IModeOfTransportService Transport,
            IUserService userService, IBusinessUnitService businessunit, ICountryService Country, IPaymentCycleService paymentService, IFormService Form, ICustomerService customerService,
            IItemService itemservice, IUOMService unit, ITaxService tax, IGenericService generic, ISalSoservice soservice, IStorageLocation storage, ITdsCodeService tdscode)
        {
            _Transport = Transport;
            _TDScodeService = tdscode;
            _Tax = tax;
            _SIService = SIService;
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
            _Generic = generic;
            _soservice = soservice;
            _storage = storage;
        }

        // GET: SalesInvoice
        [CustomAuthorizeAttribute("SALI")]
        public ActionResult Index()
        {
            ViewBag.doc = TempData["doc_num"];
            ViewBag.DataSource = _SIService.GetAll();
            return View();
        }

        // GET: SalesInvoice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sonvm = _SIService.Get((int)id);
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
            var tax = _Tax.GetAll();
            var so = _soservice.GetAll();
            var location = _storage.GetAll();
            ViewBag.tax = _Tax.GetAll();
            ViewBag.item = _itemservice.GetItemList();
            ViewBag.buyer = sonvm.REF_CUSTOMER.CUSTOMER_CODE + "-" + sonvm.REF_CUSTOMER.CUSTOMER_NAME;
            ViewBag.consignee = sonvm.REF_CUSTOMER1.CUSTOMER_CODE + "-" + sonvm.REF_CUSTOMER1.CUSTOMER_NAME;
            ViewBag.datasource = _customerService.GetCustomerList();
            ViewBag.locationlist = new SelectList(location, "storage_location_id", "storage_location_name");
            ViewBag.solist = new SelectList(so, "SO_ID", "SO_NUMBER");
            ViewBag.unitlist = new SelectList(unit, "UOM_ID", "UOM_NAME");
            ViewBag.itemlist = new SelectList(item, "ITEM_ID", "ITEM_NAME");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(6), "document_numbring_id", "category");
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
            ViewBag.createdbylist = new SelectList(user, "USER_ID", "USER_NAME");
            ViewBag.billinglist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.corlist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.formlist = new SelectList(frm, "FORM_ID", "FORM_NAME");
            ViewBag.buyernamelist = new SelectList(customer, "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.buyercodelist = new SelectList(customer, "CUSTOMER_ID", "CUSTOMER_CODE");
            ViewBag.consigneenamelist = new SelectList(customer, "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.consigneecodelist = new SelectList(customer, "CUSTOMER_ID", "CUSTOMER_CODE");
            ViewBag.taxlist = new SelectList(tax, "TAX_ID", "TAX_CODE");
            ViewBag.TransportModelist = new SelectList(_Transport.GetAll(), "mode_of_transport_id", "mode_of_transport_name");
            ViewBag.GST_TDS_Code = new SelectList(_Generic.GetGSTTDSCodeList(), "gst_tds_code_id", "gst_tds_code");
            return View(sonvm);
        }
        public JsonResult getsoid(string id)
        {
            int idd = _soservice.GetSOId(id);
            return Json(idd, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorizeAttribute("SALI")]
        public ActionResult Create()
        {
            var currencylist = _currencyService.GetAll();
            var freightterms = _freightTermsService.GetAll();
            var plant = _plant.GetAll();
            var paymentcycletype = _paymentCycleType.GetAll();
            var paymentcycle = _paymentService.GetAll();
            var statelist = _stateService.GetAll();
            var territory = _territory.GetAll();
            var user = _userService.GetAll();
            var country = _Country.GetAll();
            var frm = _Form.GetAll();
            var unit = _Unit.GetAll();
            var so = _soservice.getall().Select(a => new { so_id = a.so_id, so_number = a.so_number }).ToList();
            var location = _storage.GetAll();
            ViewBag.error = "";
            ViewBag.tdslist = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.solist = new SelectList(so, "so_id", "so_number");
            ViewBag.solist1 = _soservice.GetSOForSI();
            ViewBag.unitlist = new SelectList(unit, "UOM_ID", "UOM_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(66), "document_numbring_id", "category");
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
            ViewBag.createdbylist = new SelectList(user, "USER_ID", "USER_NAME");
            ViewBag.billinglist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.corlist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.formlist = new SelectList(frm, "FORM_ID", "FORM_NAME");
            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.taxlist = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.TransportModelist = new SelectList(_Transport.GetAll(), "mode_of_transport_id", "mode_of_transport_name");
            ViewBag.GST_TDS_Code = new SelectList(_Generic.GetGSTTDSCodeList(), "gst_tds_code_id", "gst_tds_code");
            return View();
        }

        // POST: SalesInvoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(sal_si_vm sAL_SI)
        {
            var isValid = "";
            if (ModelState.IsValid)
            {
                isValid = _SIService.Add(sAL_SI);
                if (isValid.Contains("Saved"))
                {
                    TempData["doc_num"] = isValid.Split('~')[1] + " Saved Successfully.";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = isValid;
            var currencylist = _currencyService.GetAll();
            var freightterms = _freightTermsService.GetAll();
            var plant = _plant.GetAll();
            var paymentcycletype = _paymentCycleType.GetAll();
            var paymentcycle = _paymentService.GetAll();
            var statelist = _stateService.GetAll();
            var territory = _territory.GetAll();
            var user = _userService.GetAll();
            var country = _Country.GetAll();
            var frm = _Form.GetAll();
            var unit = _Unit.GetAll();
            var so = _soservice.GetAll();
            var location = _storage.GetAll();
            ViewBag.tdslist = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.solist = new SelectList(so, "so_id", "so_number");
            ViewBag.solist1 = _soservice.GetSOForSI();
            ViewBag.unitlist = new SelectList(unit, "UOM_ID", "UOM_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(66), "document_numbring_id", "category");
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
            ViewBag.createdbylist = new SelectList(user, "USER_ID", "USER_NAME");
            ViewBag.billinglist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.corlist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.formlist = new SelectList(frm, "FORM_ID", "FORM_NAME");
            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.taxlist = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.TransportModelist = new SelectList(_Transport.GetAll(), "mode_of_transport_id", "mode_of_transport_name");
            ViewBag.GST_TDS_Code = new SelectList(_Generic.GetGSTTDSCodeList(), "gst_tds_code_id", "gst_tds_code");
            return View();
        }

        // GET: SalesInvoice/Edit/5
        [CustomAuthorizeAttribute("SALI")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sonvm = _SIService.Get((int)id);
            var currencylist = _currencyService.GetAll();
            var freightterms = _freightTermsService.GetAll();
            var plant = _plant.GetAll();
            var paymentcycletype = _paymentCycleType.GetAll();
            var paymentcycle = _paymentService.GetAll();
            var statelist = _stateService.GetAll();
            var territory = _territory.GetAll();
            var user = _userService.GetAll();
            var country = _Country.GetAll();
            var frm = _Form.GetAll();
            var unit = _Unit.GetAll();
            var so = _soservice.GetAll();
            var location = _storage.GetAll();
            ViewBag.tdslist = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.solist = new SelectList(so, "so_id", "so_number");
            ViewBag.solist1 = _soservice.GetSOForSI();
            ViewBag.unitlist = new SelectList(unit, "UOM_ID", "UOM_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(66), "document_numbring_id", "category");
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
            ViewBag.createdbylist = new SelectList(user, "USER_ID", "USER_NAME");
            ViewBag.billinglist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.corlist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.formlist = new SelectList(frm, "FORM_ID", "FORM_NAME");
            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.taxlist = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.TransportModelist = new SelectList(_Transport.GetAll(), "mode_of_transport_id", "mode_of_transport_name");
            ViewBag.GST_TDS_Code = new SelectList(_Generic.GetGSTTDSCodeList(), "gst_tds_code_id", "gst_tds_code");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(75), "cancellation_reason_id", "cancellation_reason");

            return View(sonvm);
        }

        // POST: SalesInvoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(sal_si_vm sAL_SI)
        {

            if (ModelState.IsValid)
            {
                var isedit = _SIService.Add(sAL_SI);
                if (isedit.Contains("Saved"))
                {
                    TempData["doc_num"] = isedit.Split('~')[1] + " Updated Sucessfully";
                    return RedirectToAction("Index");
                }

            }
            var currencylist = _currencyService.GetAll();
            var freightterms = _freightTermsService.GetAll();
            var plant = _plant.GetAll();
            var paymentcycletype = _paymentCycleType.GetAll();
            var paymentcycle = _paymentService.GetAll();
            var statelist = _stateService.GetAll();
            var territory = _territory.GetAll();
            var user = _userService.GetAll();
            var country = _Country.GetAll();
            var frm = _Form.GetAll();
            var unit = _Unit.GetAll();
            var so = _soservice.GetAll();
            var location = _storage.GetAll();
            ViewBag.tdslist = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.solist = new SelectList(so, "so_id", "so_number");
            ViewBag.solist1 = _soservice.GetSOForSI();
            ViewBag.unitlist = new SelectList(unit, "UOM_ID", "UOM_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(66), "document_numbring_id", "category");
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
            ViewBag.createdbylist = new SelectList(user, "USER_ID", "USER_NAME");
            ViewBag.billinglist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.corlist = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.formlist = new SelectList(frm, "FORM_ID", "FORM_NAME");
            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.taxlist = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.TransportModelist = new SelectList(_Transport.GetAll(), "mode_of_transport_id", "mode_of_transport_name");
            ViewBag.GST_TDS_Code = new SelectList(_Generic.GetGSTTDSCodeList(), "gst_tds_code_id", "gst_tds_code");
            return View();
        }

        // GET: SalesInvoice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sal_si_vm sAL_SI = _SIService.Get((int)id);
            if (sAL_SI == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        // POST: SalesInvoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var isvalid = _SIService.Delete(id);
            if (isvalid == true)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _SIService.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult GetSoProductDetail(int id)
        {
            var vm = _soservice.GetSOProductDetail(id);
            var list = JsonConvert.SerializeObject(vm,
             Formatting.None,
                      new JsonSerializerSettings()
                      {
                          ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                      });

            return Content(list, "application/json");
        }

        public ActionResult GetSOForSI(int id)
        {
            var vm = _soservice.GetSOForSI(id);
            var list = JsonConvert.SerializeObject(vm,
             Formatting.None,
                      new JsonSerializerSettings()
                      {
                          ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                      });

            return Content(list, "application/json");
        }
        public CrystalReportPdfResult SunsidiaryPdf(int id)
        {
            ReportDocument rd = new ReportDocument();
            try
            {
                var challan = _SIService.GetSIDetailForReportChallan(id, "getsubsidarychallan");
                DataSet ds = new DataSet("si");
                var dt = _Generic.ToDataTable(challan);
                dt.TableName = "SUBSIDIARY";
                ds.Tables.Add(dt);
                rd.Load(Path.Combine(Server.MapPath("~/Reports/Sales_Subsidiary.rpt")));
                rd.SetDataSource(ds);
                var status = "";
                foreach (var j in challan)
                {
                    if (j.sal_gross_value == 0)
                    {
                        status = "CANCELLED";
                    }
                }
                rd.SetParameterValue("p1", status);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string reportPath = Path.Combine(Server.MapPath("~/Reports"), "Sales_Subsidiary.rpt");
                return new CrystalReportPdfResult(reportPath, rd);
            }
            catch (Exception ex)
            {
                //--------------Log4Net
                log4net.GlobalContext.Properties["user"] = 0;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
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

        public CrystalReportPdfResult DeliveryChallanPdf(int id)
        {
            ReportDocument rd = new ReportDocument();
            try
            {
                var salsi = _SIService.GetSIDetailForReport(id, "getsalesheaderforreport");
                var sal_si_detail = _SIService.GetSIProductDetailForSI(id);
                var challan = _SIService.GetSIDetailForReportChallan(id, "getinvoicechallan");
                var exp = "";
                double amt = 0;
                var tax_type = 0;
                var batch_number = "<table>";
                var qty = "<table>";
                var balance_qty = "<table>";
                var hsn_code = "<table>";
                var item_name = "<table>";
                var posting_date = "<table>";
                var dis_quantity = "<table>";

                foreach (var j in challan)
                {
                    batch_number = batch_number + "<tr><td>" + j.customer_chalan_no + " </td></tr> ";
                    qty = qty + "<tr><td align='right'>" + j.qty + " </td></tr> ";
                    balance_qty = balance_qty + "<tr><td align='right'>" + j.balance_qty + " </td></tr> ";
                    hsn_code = hsn_code + "<tr><td>" + j.hsn_code + " </td></tr> ";
                    item_name = item_name + "<tr><td>" + j.item_name + " </td></tr> ";
                    posting_date = posting_date + "<tr><td>" + j.posting_date + " </td></tr>";
                    dis_quantity = dis_quantity + "<tr><td align='right'>" + j.dis_quantity + " </td></tr>";
                }
                batch_number = batch_number + "<table>";
                qty = qty + "<table>";
                balance_qty = balance_qty + "<table>";
                hsn_code = hsn_code + "<table>";
                item_name = item_name + "<table>";
                posting_date = posting_date + "<table>";
                dis_quantity = dis_quantity + "<table>";

                foreach (var i in sal_si_detail)
                {
                    exp = exp + i.tax_id + "~" + (Convert.ToDouble(i.quantity) * Convert.ToDouble(i.rate)) + "~" + 0 + "~" + salsi.round_off + ",";
                    amt = amt + (Convert.ToDouble(i.quantity) * Convert.ToDouble(i.rate));
                    if (i.igst_tax_rate == 0)
                    {
                        tax_type = 1;
                    }
                    else
                    {
                        tax_type = 2;
                    }
                }
                var tax_detail = _Generic.GetTaxCalculation("calculatetaxfordeliverychallan", exp.Remove(exp.Length - 1), amt, salsi.si_date, salsi.tds_code_id); //calculatetaxforsalesreport
                var si_detail_id = 0;
                DataSet ds = new DataSet("sal_si");
                var sal_si = new List<sal_si_report_vm>();
                sal_si.Add(salsi);
                var dt1 = _Generic.ToDataTable(sal_si);
                var dt2 = _Generic.ToDataTable(sal_si_detail);
                var dt3 = _Generic.ToDataTable(tax_detail);
                DataRow[] drPaytable = dt3.Select("tax_name like '%Grand Total%'");
                DataRow[] drPaytable1 = dt3.Select("tax_name like '%Total Tax Value%'");

                dt1.TableName = "sal_si";
                dt2.TableName = "sal_si_detail";
                dt3.TableName = "si_tax_detail";
                double sum = 0;
                double sum1 = 0;

                for (int i = 0; i < drPaytable.Length; i++)
                {
                    sum = sum + double.Parse(drPaytable[0].ItemArray[1].ToString());
                }
                for (int i = 0; i < drPaytable1.Length; i++)
                {
                    sum1 = sum1 + double.Parse(drPaytable1[0].ItemArray[1].ToString());
                }

                dt1.Rows[0]["sal_gross_value"] = sum;
                dt1.Rows[0]["excise_amount"] = sum1;
                dt1.Rows[0]["customer_chalan_no"] = batch_number;
                dt1.Rows[0]["qty"] = qty;
                dt1.Rows[0]["balance_qty"] = balance_qty;
                dt1.Rows[0]["posting_date"] = posting_date;
                dt1.Rows[0]["dis_quantity"] = dis_quantity;
                dt1.Rows[0]["hsn_code"] = hsn_code;
                dt1.Rows[0]["item_name"] = item_name;

                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);
                dt3.Columns.Add("id", typeof(System.Int32));
                foreach (var so in sal_si_detail)
                {
                    si_detail_id = so.si_detail_id;
                }
                foreach (DataRow row in dt3.Rows)
                {
                    //need to set value to NewColumn column
                    row["id"] = si_detail_id;   // or set it to some other value
                }
                ds.Tables.Add(dt3);

                if (tax_type == 1)
                {
                    rd.Load(Path.Combine(Server.MapPath("~/Reports/SalInvDeliveryChallanReport.rpt")));
                }
                else
                {
                    rd.Load(Path.Combine(Server.MapPath("~/Reports/SalInvDeliveryChallanReport.rpt")));
                }
                for (int i = 0; i < rd.Subreports.Count; i++)
                {
                    ReportDocument sr = rd.Subreports[i];
                    sr.SetDataSource(ds);
                }
                var status = "";
                if (salsi.sal_gross_value == 0)
                {
                    status = "CANCELLED";
                }
                rd.SetParameterValue("p1", "Original(For Buyer)");
                rd.SetParameterValue("p2", "Duplicate(For Transporter)");
                rd.SetParameterValue("p3", "Triplicate for supplier");
                rd.SetParameterValue("p4", "Extra Copy");
                //rd.SetParameterValue("p5", status);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string reportPath = Path.Combine(Server.MapPath("~/Reports"), tax_type == 1 ? "SalInvDeliveryChallanReport.rpt" : "SalInvDeliveryChallanReport.rpt");
                return new CrystalReportPdfResult(reportPath, rd);
                // return 1;
            }
            catch (Exception ex)
            {
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


        public CrystalReportPdfResult Pdf(int id)
        {
            ReportDocument rd = new ReportDocument();
            try
            {
                var salsi = _SIService.GetSIDetailForReport(id, "getsalesheaderforreport");
                var sal_si_detail = _SIService.GetSIProductDetailForSI(id);
                var challan = _SIService.GetSIDetailForReportChallan(id, "getinvoicechallan");
                var exp = "";
                double amt = 0;
                var tax_type = 0;
                var batch_number = "<table>";
                var qty = "<table>";
                var balance_qty = "<table>";
                var hsn_code = "<table>";
                var item_name = "<table>";
                var posting_date = "<table>";
                var dis_quantity = "<table>";

                foreach (var j in challan)
                {
                    batch_number = batch_number + "<tr><td>" + j.customer_chalan_no + " </td></tr> ";
                    qty = qty + "<tr style='text-align:right'><td style='text-align:right'>" + j.qty + " </td></tr> ";
                    balance_qty = balance_qty + "<tr><td align='right'>" + j.balance_qty + " </td></tr> ";
                    hsn_code = hsn_code + "<tr><td>" + j.hsn_code + " </td></tr> ";
                    item_name = item_name + "<tr><td>" + j.item_name + " </td></tr> ";
                    posting_date = posting_date + "<tr><td>" + j.posting_date + " </td></tr>";
                    dis_quantity = dis_quantity + "<tr><td align='right'>" + j.dis_quantity + " </td></tr>";
                }
                batch_number = batch_number + "<table>";
                qty = qty + "<table'>";
                balance_qty = balance_qty + "<table>";
                hsn_code = hsn_code + "<table>";
                item_name = item_name + "<table>";
                posting_date = posting_date + "<table>";
                dis_quantity = dis_quantity + "<table>";

                foreach (var i in sal_si_detail)
                {
                    exp = exp + i.tax_id + "~" + i.assessable_value + "~" + i.sales_value + "~" + salsi.round_off + ",";
                    amt = amt + i.gross_value;
                    if (i.igst_tax_rate == 0)
                    {
                        tax_type = 1;
                    }
                    else
                    {
                        tax_type = 2;
                    }
                }
                var tax_detail = _Generic.GetTaxCalculation("calculatetaxforsalesreport", exp.Remove(exp.Length - 1), amt, salsi.si_date, salsi.tds_code_id);
                var si_detail_id = 0;
                DataSet ds = new DataSet("sal_si");
                var sal_si = new List<sal_si_report_vm>();
                sal_si.Add(salsi);
                var dt1 = _Generic.ToDataTable(sal_si);
                var dt2 = _Generic.ToDataTable(sal_si_detail);
                var dt3 = _Generic.ToDataTable(tax_detail);
                DataRow[] drPaytable = dt3.Select("tax_name like '%Grand Total%'");
                DataRow[] drPaytable1 = dt3.Select("tax_name like '%Total Tax Value%'");

                dt1.TableName = "sal_si";
                dt2.TableName = "sal_si_detail";
                dt3.TableName = "si_tax_detail";
                double sum = 0;
                double sum1 = 0;

                for (int i = 0; i < drPaytable.Length; i++)
                {
                    sum = sum + double.Parse(drPaytable[0].ItemArray[1].ToString());
                }
                for (int i = 0; i < drPaytable1.Length; i++)
                {
                    sum1 = sum1 + double.Parse(drPaytable1[0].ItemArray[1].ToString());
                }

                dt1.Rows[0]["sal_gross_value"] = sum;
                dt1.Rows[0]["excise_amount"] = sum1;
                dt1.Rows[0]["customer_chalan_no"] = batch_number;
                dt1.Rows[0]["qty"] = qty;
                dt1.Rows[0]["balance_qty"] = balance_qty;
                dt1.Rows[0]["posting_date"] = posting_date;
                dt1.Rows[0]["dis_quantity"] = dis_quantity;
                dt1.Rows[0]["hsn_code"] = hsn_code;
                dt1.Rows[0]["item_name"] = item_name;

                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);
                dt3.Columns.Add("id", typeof(System.Int32));
                foreach (var so in sal_si_detail)
                {
                    si_detail_id = so.si_detail_id;
                }
                foreach (DataRow row in dt3.Rows)
                {
                    //need to set value to NewColumn column
                    row["id"] = si_detail_id;   // or set it to some other value
                }
                ds.Tables.Add(dt3);

                if (tax_type == 1)
                {
                    rd.Load(Path.Combine(Server.MapPath("~/Reports/SalSISubReportNew.rpt")));
                }
                else
                {
                    rd.Load(Path.Combine(Server.MapPath("~/Reports/IGSTSalSISubReport.rpt")));
                }
                for (int i = 0; i < rd.Subreports.Count; i++)
                {
                    ReportDocument sr = rd.Subreports[i];
                    sr.SetDataSource(ds);
                }
                var status = "";
                if (salsi.sal_gross_value == 0)
                {
                    status = "CANCELLED";
                }
                rd.SetParameterValue("p1", "Original(For Buyer)");
                rd.SetParameterValue("p2", "Duplicate(For Transporter)");
                rd.SetParameterValue("p3", "Triplicate for supplier");
                rd.SetParameterValue("p4", "Extra Copy");
                rd.SetParameterValue("p5", status);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string reportPath = Path.Combine(Server.MapPath("~/Reports"), tax_type == 1 ? "SalSISubReportNew.rpt" : "IGSTSalSISubReport.rpt");
                return new CrystalReportPdfResult(reportPath, rd);
                // return 1;
            }
            catch (Exception ex)
            {
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
        public CrystalReportPdfResult ShipToParty_Pdf(int id)
        {
            ReportDocument rd = new ReportDocument();
            try
            {
                var salsi = _SIService.GetSIDetailForReport(id, "getsalesheaderforreport");
                var sal_si_detail = _SIService.GetSIProductDetailForSI(id);
                var challan = _SIService.GetSIDetailForReportChallan(id, "getinvoicechallan");
                var exp = "";
                double amt = 0;
                var tax_type = 0;
                var batch_number = "<table>";
                var qty = "<table>";
                var balance_qty = "<table>";
                var hsn_code = "<table>";
                var item_name = "<table>";
                var posting_date = "<table>";
                var dis_quantity = "<table>";

                foreach (var j in challan)
                {
                    batch_number = batch_number + "<tr><td>" + j.customer_chalan_no + " </td></tr> ";
                    qty = qty + "<tr style='text-align:right'><td style='text-align:right'>" + j.qty + " </td></tr> ";
                    balance_qty = balance_qty + "<tr><td align='right'>" + j.balance_qty + " </td></tr> ";
                    hsn_code = hsn_code + "<tr><td>" + j.hsn_code + " </td></tr> ";
                    item_name = item_name + "<tr><td>" + j.item_name + " </td></tr> ";
                    posting_date = posting_date + "<tr><td>" + j.posting_date + " </td></tr>";
                    dis_quantity = dis_quantity + "<tr><td align='right'>" + j.dis_quantity + " </td></tr>";
                }
                batch_number = batch_number + "<table>";
                qty = qty + "<table'>";
                balance_qty = balance_qty + "<table>";
                hsn_code = hsn_code + "<table>";
                item_name = item_name + "<table>";
                posting_date = posting_date + "<table>";
                dis_quantity = dis_quantity + "<table>";

                foreach (var i in sal_si_detail)
                {
                    exp = exp + i.tax_id + "~" + i.assessable_value + "~" + i.sales_value + "~" + salsi.round_off + ",";
                    amt = amt + i.gross_value;
                    if (i.igst_tax_rate == 0)
                    {
                        tax_type = 1;
                    }
                    else
                    {
                        tax_type = 2;
                    }
                }
                var tax_detail = _Generic.GetTaxCalculation("calculatetaxforsalesreport", exp.Remove(exp.Length - 1), amt, salsi.si_date, salsi.tds_code_id);
                var si_detail_id = 0;
                DataSet ds = new DataSet("sal_si");
                var sal_si = new List<sal_si_report_vm>();
                sal_si.Add(salsi);
                var dt1 = _Generic.ToDataTable(sal_si);
                var dt2 = _Generic.ToDataTable(sal_si_detail);
                var dt3 = _Generic.ToDataTable(tax_detail);
                DataRow[] drPaytable = dt3.Select("tax_name like '%Grand Total%'");
                DataRow[] drPaytable1 = dt3.Select("tax_name like '%Total Tax Value%'");

                dt1.TableName = "sal_si";
                dt2.TableName = "sal_si_detail";
                dt3.TableName = "si_tax_detail";
                double sum = 0;
                double sum1 = 0;

                for (int i = 0; i < drPaytable.Length; i++)
                {
                    sum = sum + double.Parse(drPaytable[0].ItemArray[1].ToString());
                }
                for (int i = 0; i < drPaytable1.Length; i++)
                {
                    sum1 = sum1 + double.Parse(drPaytable1[0].ItemArray[1].ToString());
                }

                dt1.Rows[0]["sal_gross_value"] = sum;
                dt1.Rows[0]["excise_amount"] = sum1;
                dt1.Rows[0]["customer_chalan_no"] = batch_number;
                dt1.Rows[0]["qty"] = qty;
                dt1.Rows[0]["balance_qty"] = balance_qty;
                dt1.Rows[0]["posting_date"] = posting_date;
                dt1.Rows[0]["dis_quantity"] = dis_quantity;
                dt1.Rows[0]["hsn_code"] = hsn_code;
                dt1.Rows[0]["item_name"] = item_name;

                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);
                dt3.Columns.Add("id", typeof(System.Int32));
                foreach (var so in sal_si_detail)
                {
                    si_detail_id = so.si_detail_id;
                }
                foreach (DataRow row in dt3.Rows)
                {
                    //need to set value to NewColumn column
                    row["id"] = si_detail_id;   // or set it to some other value
                }
                ds.Tables.Add(dt3);

                //if (tax_type == 1)
                //{
                //    rd.Load(Path.Combine(Server.MapPath("~/Reports/SalSISubReportNew.rpt")));
                //}
                //else
                //{

                rd.Load(Path.Combine(Server.MapPath("~/Reports/IGSTSalSISubReportShipToParty.rpt")));

                //}
                for (int i = 0; i < rd.Subreports.Count; i++)
                {
                    ReportDocument sr = rd.Subreports[i];
                    sr.SetDataSource(ds);
                }
                var status = "";
                if (salsi.sal_gross_value == 0)
                {
                    status = "CANCELLED";
                }
                rd.SetParameterValue("p1", "Original(For Buyer)");
                rd.SetParameterValue("p2", "Duplicate(For Transporter)");
                rd.SetParameterValue("p3", "Triplicate for supplier");
                rd.SetParameterValue("p4", "Extra Copy");
                rd.SetParameterValue("p5", status);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string reportPath = Path.Combine(Server.MapPath("~/Reports"), tax_type == 1 ? "SalSISubReportNew.rpt" : "IGSTSalSISubReportShipToParty.rpt");
                return new CrystalReportPdfResult(reportPath, rd);
                // return 1;
            }
            catch (Exception ex)
            {
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

        public CrystalReportPdfResult ScrapSalesInvoice(int id)
        {
            ReportDocument rd = new ReportDocument();
            try
            {
                var salsi = _SIService.GetSIDetailForReport(id, "getsalesheaderforreport");
                var sal_si_detail = _SIService.GetSIProductDetailForSI(id);
                var challan = _SIService.GetSIDetailForReportChallan(id, "getinvoicechallan");
                var exp = "";
                double amt = 0;
                var tax_type = 0;
                var batch_number = "<table>";
                var qty = "<table>";
                var balance_qty = "<table>";
                var hsn_code = "<table>";
                var item_name = "<table>";
                var posting_date = "<table>";
                var dis_quantity = "<table>";
                foreach (var j in challan)
                {
                    batch_number = batch_number + "<tr><td>" + j.customer_chalan_no + " </td></tr> ";
                    qty = qty + "<tr><td align='right'>" + j.qty + " </td></tr> ";
                    balance_qty = balance_qty + "<tr><td align='right'>" + j.balance_qty + " </td></tr> ";
                    hsn_code = hsn_code + "<tr><td>" + j.hsn_code + " </td></tr> ";
                    item_name = item_name + "<tr><td>" + j.item_name + " </td></tr> ";
                    posting_date = posting_date + "<tr><td>" + j.posting_date + " </td></tr>";
                    dis_quantity = dis_quantity + "<tr><td align='right'>" + j.dis_quantity + " </td></tr>";
                }
                batch_number = batch_number + "<table>";
                qty = qty + "<table>";
                balance_qty = balance_qty + "<table>";
                hsn_code = hsn_code + "<table>";
                item_name = item_name + "<table>";
                posting_date = posting_date + "<table>";
                dis_quantity = dis_quantity + "<table>";

                foreach (var i in sal_si_detail)
                {
                    exp = exp + i.tax_id + "~" + i.assessable_value + "~" + i.sales_value + "~" + salsi.round_off + ",";
                    amt = amt + i.gross_value;
                    if (i.igst_tax_rate == 0)
                    {
                        tax_type = 1;
                    }
                    else
                    {
                        tax_type = 2;
                    }
                }
                var tax_detail = _Generic.GetTaxCalculation("calculatetaxforsalesreportscrap", exp.Remove(exp.Length - 1), amt, salsi.si_date, salsi.tds_code_id);
                var si_detail_id = 0;
                DataSet ds = new DataSet("sal_si");
                var sal_si = new List<sal_si_report_vm>();
                sal_si.Add(salsi);
                var dt1 = _Generic.ToDataTable(sal_si);
                var dt2 = _Generic.ToDataTable(sal_si_detail);
                var dt3 = _Generic.ToDataTable(tax_detail);
                DataRow[] drPaytable = dt3.Select("tax_name like '%Grand Total%'");
                DataRow[] drPaytable1 = dt3.Select("tax_name like '%Total Tax Value%'");
                DataRow[] IGSTtable = dt3.Select("tax_name like '%IGST%'");
                DataRow[] SGSTtable = dt3.Select("tax_name like '%SGST%'");
                DataRow[] CGSTtable = dt3.Select("tax_name like '%CGST%'");
                DataRow[] TCStable = dt3.Select("tax_name like '%TCS%'");
                dt1.TableName = "sal_si";
                dt2.TableName = "sal_si_detail";
                dt3.TableName = "si_tax_detail";
                double sum = 0;
                double sum1 = 0;
                double cgst = 0;
                double igst = 0;
                double sgst = 0;
                double tcs = 0;
                for (int i = 0; i < drPaytable.Length; i++)
                {
                    sum = sum + double.Parse(drPaytable[0].ItemArray[1].ToString());
                }
                for (int i = 0; i < drPaytable1.Length; i++)
                {
                    sum1 = sum1 + double.Parse(drPaytable1[0].ItemArray[1].ToString());
                }
                for (int i = 0; i < IGSTtable.Length; i++)
                {
                    igst = igst + double.Parse(IGSTtable[0].ItemArray[1].ToString());
                }
                for (int i = 0; i < SGSTtable.Length; i++)
                {
                    sgst = sgst + double.Parse(SGSTtable[0].ItemArray[1].ToString());
                }
                for (int i = 0; i < CGSTtable.Length; i++)
                {
                    cgst = cgst + double.Parse(CGSTtable[0].ItemArray[1].ToString());
                }
                for (int i = 0; i < TCStable.Length; i++)
                {
                    tcs = tcs + double.Parse(TCStable[0].ItemArray[1].ToString());
                }
                dt1.Rows[0]["sal_gross_value"] = sum;
                dt1.Rows[0]["excise_amount"] = sum1;
                dt1.Rows[0]["customer_chalan_no"] = batch_number;
                dt1.Rows[0]["qty"] = qty;
                dt1.Rows[0]["balance_qty"] = balance_qty;
                dt1.Rows[0]["posting_date"] = posting_date;
                dt1.Rows[0]["dis_quantity"] = dis_quantity;
                dt1.Rows[0]["hsn_code"] = hsn_code;
                dt1.Rows[0]["item_name"] = item_name;
                dt1.Rows[0]["cgst_total"] = cgst;
                dt1.Rows[0]["sgst_total"] = sgst;
                dt1.Rows[0]["igst_total"] = igst;
                dt1.Rows[0]["tcs_total"] = tcs;
                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);
                dt3.Columns.Add("id", typeof(System.Int32));
                foreach (var so in sal_si_detail)
                {
                    si_detail_id = so.si_detail_id;
                }
                foreach (DataRow row in dt3.Rows)
                {
                    //need to set value to NewColumn column
                    row["id"] = si_detail_id;   // or set it to some other value
                }
                ds.Tables.Add(dt3);
                // tax_type = 2;
                if (tax_type == 1)
                {
                    rd.Load(Path.Combine(Server.MapPath("~/Reports/SalesInvoiceScrapSubReportGST.rpt")));
                }
                else
                {
                    rd.Load(Path.Combine(Server.MapPath("~/Reports/SalesInvoiceScrapSubReport.rpt")));
                }


                for (int i = 0; i < rd.Subreports.Count; i++)
                {
                    ReportDocument sr = rd.Subreports[i];
                    sr.SetDataSource(ds);
                }
                var status = "";
                if (salsi.sal_gross_value == 0)
                {
                    status = "CANCELLED";
                }
                rd.SetParameterValue("p1", "Original(For Buyer)");
                rd.SetParameterValue("p2", "Duplicate(For Transporter)");
                rd.SetParameterValue("p3", "Triplicate for supplier");
                rd.SetParameterValue("p4", "Extra Copy");
                rd.SetParameterValue("p5", status);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string reportPath = "";

                if (tax_type == 1)
                {
                    reportPath = Path.Combine(Server.MapPath("~/Reports"), "SalesInvoiceScrapSubReportGST.rpt");
                }
                else
                {
                    reportPath = Path.Combine(Server.MapPath("~/Reports"), "SalesInvoiceScrapSubReport.rpt");
                }
                return new CrystalReportPdfResult(reportPath, rd);
                // return 1;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                //rd.Close();
                //rd.Dispose();
                //GC.Collect();
                rd.Close();
                rd.Clone();
                rd.Dispose();
                rd = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

        }

        public CrystalReportPdfResult ScrapSalesInvoiceShipToParty(int id)
        {
            ReportDocument rd = new ReportDocument();
            try
            {
                var salsi = _SIService.GetSIDetailForReport(id, "getsalesheaderforreport");
                var sal_si_detail = _SIService.GetSIProductDetailForSI(id);
                var challan = _SIService.GetSIDetailForReportChallan(id, "getinvoicechallan");
                var exp = "";
                double amt = 0;
                var tax_type = 0;
                var batch_number = "<table>";
                var qty = "<table>";
                var balance_qty = "<table>";
                var hsn_code = "<table>";
                var item_name = "<table>";
                var posting_date = "<table>";
                var dis_quantity = "<table>";
                foreach (var j in challan)
                {
                    batch_number = batch_number + "<tr><td>" + j.customer_chalan_no + " </td></tr> ";
                    qty = qty + "<tr><td align='right'>" + j.qty + " </td></tr> ";
                    balance_qty = balance_qty + "<tr><td align='right'>" + j.balance_qty + " </td></tr> ";
                    hsn_code = hsn_code + "<tr><td>" + j.hsn_code + " </td></tr> ";
                    item_name = item_name + "<tr><td>" + j.item_name + " </td></tr> ";
                    posting_date = posting_date + "<tr><td>" + j.posting_date + " </td></tr>";
                    dis_quantity = dis_quantity + "<tr><td align='right'>" + j.dis_quantity + " </td></tr>";
                }
                batch_number = batch_number + "<table>";
                qty = qty + "<table>";
                balance_qty = balance_qty + "<table>";
                hsn_code = hsn_code + "<table>";
                item_name = item_name + "<table>";
                posting_date = posting_date + "<table>";
                dis_quantity = dis_quantity + "<table>";

                foreach (var i in sal_si_detail)
                {
                    exp = exp + i.tax_id + "~" + i.assessable_value + "~" + i.sales_value + "~" + salsi.round_off + ",";
                    amt = amt + i.gross_value;
                    if (i.igst_tax_rate == 0)
                    {
                        tax_type = 1;
                    }
                    else
                    {
                        tax_type = 2;
                    }
                }
                var tax_detail = _Generic.GetTaxCalculation("calculatetaxforsalesreportscrap", exp.Remove(exp.Length - 1), amt, salsi.si_date, salsi.tds_code_id);
                var si_detail_id = 0;
                DataSet ds = new DataSet("sal_si");
                var sal_si = new List<sal_si_report_vm>();
                sal_si.Add(salsi);
                var dt1 = _Generic.ToDataTable(sal_si);
                var dt2 = _Generic.ToDataTable(sal_si_detail);
                var dt3 = _Generic.ToDataTable(tax_detail);
                DataRow[] drPaytable = dt3.Select("tax_name like '%Grand Total%'");
                DataRow[] drPaytable1 = dt3.Select("tax_name like '%Total Tax Value%'");
                DataRow[] IGSTtable = dt3.Select("tax_name like '%IGST%'");
                DataRow[] SGSTtable = dt3.Select("tax_name like '%SGST%'");
                DataRow[] CGSTtable = dt3.Select("tax_name like '%CGST%'");
                DataRow[] TCStable = dt3.Select("tax_name like '%TCS%'");
                dt1.TableName = "sal_si";
                dt2.TableName = "sal_si_detail";
                dt3.TableName = "si_tax_detail";
                double sum = 0;
                double sum1 = 0;
                double cgst = 0;
                double igst = 0;
                double sgst = 0;
                double tcs = 0;
                for (int i = 0; i < drPaytable.Length; i++)
                {
                    sum = sum + double.Parse(drPaytable[0].ItemArray[1].ToString());
                }
                for (int i = 0; i < drPaytable1.Length; i++)
                {
                    sum1 = sum1 + double.Parse(drPaytable1[0].ItemArray[1].ToString());
                }
                for (int i = 0; i < IGSTtable.Length; i++)
                {
                    igst = igst + double.Parse(IGSTtable[0].ItemArray[1].ToString());
                }
                for (int i = 0; i < SGSTtable.Length; i++)
                {
                    sgst = sgst + double.Parse(SGSTtable[0].ItemArray[1].ToString());
                }
                for (int i = 0; i < CGSTtable.Length; i++)
                {
                    cgst = cgst + double.Parse(CGSTtable[0].ItemArray[1].ToString());
                }
                for (int i = 0; i < TCStable.Length; i++)
                {
                    tcs = tcs + double.Parse(TCStable[0].ItemArray[1].ToString());
                }
                dt1.Rows[0]["sal_gross_value"] = sum;
                dt1.Rows[0]["excise_amount"] = sum1;
                dt1.Rows[0]["customer_chalan_no"] = batch_number;
                dt1.Rows[0]["qty"] = qty;
                dt1.Rows[0]["balance_qty"] = balance_qty;
                dt1.Rows[0]["posting_date"] = posting_date;
                dt1.Rows[0]["dis_quantity"] = dis_quantity;
                dt1.Rows[0]["hsn_code"] = hsn_code;
                dt1.Rows[0]["item_name"] = item_name;
                dt1.Rows[0]["cgst_total"] = cgst;
                dt1.Rows[0]["sgst_total"] = sgst;
                dt1.Rows[0]["igst_total"] = igst;
                dt1.Rows[0]["tcs_total"] = tcs;
                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);
                dt3.Columns.Add("id", typeof(System.Int32));
                foreach (var so in sal_si_detail)
                {
                    si_detail_id = so.si_detail_id;
                }
                foreach (DataRow row in dt3.Rows)
                {
                    //need to set value to NewColumn column
                    row["id"] = si_detail_id;   // or set it to some other value
                }
                ds.Tables.Add(dt3);
                // tax_type = 2;
                if (tax_type == 1)
                {
                    rd.Load(Path.Combine(Server.MapPath("~/Reports/SalesInvoiceScrapSubReportGSTForShipToParty.rpt")));
                }
                else
                {
                    rd.Load(Path.Combine(Server.MapPath("~/Reports/SalesInvoiceScrapSubReportForShipToParty.rpt")));
                }

                for (int i = 0; i < rd.Subreports.Count; i++)
                {
                    ReportDocument sr = rd.Subreports[i];
                    sr.SetDataSource(ds);
                }
                var status = "";
                if (salsi.sal_gross_value == 0)
                {
                    status = "CANCELLED";
                }
                rd.SetParameterValue("p1", "Original(For Buyer)");
                rd.SetParameterValue("p2", "Duplicate(For Transporter)");
                rd.SetParameterValue("p3", "Triplicate for supplier");
                rd.SetParameterValue("p4", "Extra Copy");
                rd.SetParameterValue("p5", status);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                string reportPath = tax_type != 1
                    ? Path.Combine(Server.MapPath("~/Reports"), "SalesInvoiceScrapSubReportForShipToParty.rpt")
                    : Path.Combine(Server.MapPath("~/Reports"), "SalesInvoiceScrapSubReportGSTForShipToParty.rpt");

                return new CrystalReportPdfResult(reportPath, rd);

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                //rd.Close();
                //rd.Dispose();
                //GC.Collect();
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
            var ListQuotation = _soservice.GetSOForSalesInvoice(id);
            // ViewBag.QuotationNumber = new SelectList(ListQuotation, "QUOTATION_ID", "quotation_No_Date");
            return Json(new { item = ListQuotation }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult gettagbatchforsalesinvoice(int item_id, int plant_id, int sloc_id, int bucket_id, int customer_id)
        {
            var taxcalculation = _SIService.gettagbatchforsalesinvoice((int)item_id, (int)plant_id, (int)sloc_id, (int)bucket_id, (int)customer_id);

            return Json(taxcalculation, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteConfirmed(int id, string cancellation_remarks, int reason_id)
        {
            var isValid = _soservice.Delete(id, cancellation_remarks, reason_id);
            if (isValid.Contains("Cancelled"))
            {
                return Json(isValid);
            }
            return Json(isValid);
        }

    }
}
