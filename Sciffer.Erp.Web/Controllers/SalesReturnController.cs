using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Implementation;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class SalesReturnController : Controller
    {
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
        public readonly ISalesReturnService _ISalesReturnService;
        public readonly IItemService _itemService;
        public SalesReturnController(IItemService ItemService,ISalesInvoiceService SIService, ICurrencyService currencyService, ISalesCategoryService salecategoryService, IFreightTermsService freightTermsService,
            IPaymentCycleTypeService paymentCycleType, IPaymentTermsService paymentTerms, IStateService stateService, ITerritoryService territory, IPlantService plant, IModeOfTransportService Transport,
            IUserService userService, IBusinessUnitService businessunit, ICountryService Country, IPaymentCycleService paymentService, IFormService Form, ICustomerService customerService,
            IItemService itemservice, IUOMService unit, ITaxService tax, IGenericService generic, ISalSoservice soservice, IStorageLocation storage, ITdsCodeService tdscode, ISalesReturnService ISalesReturnService)
        {
            _itemService = ItemService;
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
            _ISalesReturnService = ISalesReturnService;
        }
        public ActionResult Index()
        {
            ViewBag.doc = TempData["doc_num"];
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var details = _ISalesReturnService.Detail((int)id);
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
            ViewBag.solist = new SelectList(_SIService.GetAll().Select(a => new { si_id = a.si_id, si_number = a.si_number }).ToList(), "si_id", "si_number");
            ViewBag.solist1 = _soservice.GetSOForSI();
            ViewBag.unitlist = new SelectList(unit, "UOM_ID", "UOM_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(68), "document_numbring_id", "category");
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
            ViewBag.si_batch_list = new SelectList(_ISalesReturnService.getBuyer().sal_si_return_batch_vm.Select(a => new { batch_number = a.batch_number }), "batch_number", "batch_number");
            ViewBag.si_list = new SelectList(_ISalesReturnService.getBuyer().sal_si_vm.Select(a => new { si_id = a.si_id, si_number = a.si_number }), "si_id", "si_number");
            ViewBag.customerlist = new SelectList(_ISalesReturnService.getBuyer().CustomerVM.Select(a => new { customer_id = a.customer_id, customer_name = a.customer_name }), "customer_id", "customer_name");
            ViewBag.taxlist = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.TransportModelist = new SelectList(_Transport.GetAll(), "mode_of_transport_id", "mode_of_transport_name");
            ViewBag.GST_TDS_Code = new SelectList(_Generic.GetGSTTDSCodeList(), "gst_tds_code_id", "gst_tds_code");
            return View(details);
        }
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
            ViewBag.solist = new SelectList(_SIService.GetAll().Select(a=> new { si_id = a.si_id, si_number = a.si_number }).ToList(), "si_id", "si_number" );
            ViewBag.solist1 = _soservice.GetSOForSI();
            ViewBag.unitlist = new SelectList(unit, "UOM_ID", "UOM_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(68), "document_numbring_id", "category");
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
            ViewBag.si_batch_list = new SelectList(_ISalesReturnService.getBuyer().sal_si_return_batch_vm.Select(a => new { batch_number = a.batch_number }), "batch_number", "batch_number");
            ViewBag.si_list = new SelectList(_ISalesReturnService.getBuyer().sal_si_vm.Select(a => new { si_id = a.si_id, si_number = a.si_number }), "si_id", "si_number");
            ViewBag.customerlist = new SelectList(_ISalesReturnService.getBuyer().CustomerVM.Select(a=>new { customer_id=a.customer_id, customer_name=a.customer_name }), "customer_id", "customer_name");
            ViewBag.taxlist = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.TransportModelist = new SelectList(_Transport.GetAll(), "mode_of_transport_id", "mode_of_transport_name");
            ViewBag.GST_TDS_Code = new SelectList(_Generic.GetGSTTDSCodeList(), "gst_tds_code_id", "gst_tds_code");
            return View();
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(sal_si_return_vm sal_si_return_vm)
        {
            var isValid = "";
            if (ModelState.IsValid)
            {
                isValid = _ISalesReturnService.Add(sal_si_return_vm);
                if (isValid.Contains("Saved"))
                {
                    TempData["doc_num"] = isValid.Split('~')[1] + " Saved Successfully!";
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
            var so = _soservice.getall().Select(a => new { so_id = a.so_id, so_number = a.so_number }).ToList();
            var location = _storage.GetAll();
            ViewBag.error = "";
            ViewBag.tdslist = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.solist = new SelectList(_SIService.GetAll().Select(a => new { si_id = a.si_id, si_number = a.si_number }).ToList(), "si_id", "si_number");
            ViewBag.solist1 = _soservice.GetSOForSI();
            ViewBag.unitlist = new SelectList(unit, "UOM_ID", "UOM_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(68), "document_numbring_id", "category");
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
            ViewBag.si_batch_list = new SelectList(_ISalesReturnService.getBuyer().sal_si_return_batch_vm.Select(a => new { batch_number = a.batch_number }), "batch_number", "batch_number");
            ViewBag.si_list = new SelectList(_ISalesReturnService.getBuyer().sal_si_vm.Select(a => new { si_id = a.si_id, si_number = a.si_number }), "si_id", "si_number");
            ViewBag.customerlist = new SelectList(_ISalesReturnService.getBuyer().CustomerVM.Select(a => new { customer_id = a.customer_id, customer_name = a.customer_name }), "customer_id", "customer_name");
            ViewBag.taxlist = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.TransportModelist = new SelectList(_Transport.GetAll(), "mode_of_transport_id", "mode_of_transport_name");
            ViewBag.GST_TDS_Code = new SelectList(_Generic.GetGSTTDSCodeList(), "gst_tds_code_id", "gst_tds_code");
            return View();
        }
        public ActionResult Edit(int id)
        {
            var details = _ISalesReturnService.Detail((int)id);
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
            ViewBag.solist = new SelectList(_SIService.GetAll().Select(a => new { si_id = a.si_id, si_number = a.si_number }).ToList(), "si_id", "si_number");
            ViewBag.solist1 = _soservice.GetSOForSI();
            ViewBag.unitlist = new SelectList(unit, "UOM_ID", "UOM_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(68), "document_numbring_id", "category");
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
            ViewBag.si_batch_list = new SelectList(_ISalesReturnService.getBuyer().sal_si_return_batch_vm.Select(a => new { batch_number = a.batch_number }), "batch_number", "batch_number");
            ViewBag.si_list = new SelectList(_ISalesReturnService.getBuyer().sal_si_vm.Select(a => new { si_id = a.si_id, si_number = a.si_number }), "si_id", "si_number");
            ViewBag.customerlist = new SelectList(_ISalesReturnService.getBuyer().CustomerVM.Select(a => new { customer_id = a.customer_id, customer_name = a.customer_name }), "customer_id", "customer_name");
            ViewBag.taxlist = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.TransportModelist = new SelectList(_Transport.GetAll(), "mode_of_transport_id", "mode_of_transport_name");
            ViewBag.GST_TDS_Code = new SelectList(_Generic.GetGSTTDSCodeList(), "gst_tds_code_id", "gst_tds_code");
            return View(details);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(sal_si_return_vm sal_si_return_vm)
        {
            var isValid = "";
            if (ModelState.IsValid)
            {
                isValid = _ISalesReturnService.Add(sal_si_return_vm);
                if (isValid.Contains("Saved"))
                {
                    TempData["doc_num"] = isValid.Split('~')[1] + " Updated Successfully!";
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
            var so = _soservice.getall().Select(a => new { so_id = a.so_id, so_number = a.so_number }).ToList();
            var location = _storage.GetAll();
            ViewBag.error = "";
            ViewBag.tdslist = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.solist = new SelectList(_SIService.GetAll().Select(a => new { si_id = a.si_id, si_number = a.si_number }).ToList(), "si_id", "si_number");
            ViewBag.solist1 = _soservice.GetSOForSI();
            ViewBag.unitlist = new SelectList(unit, "UOM_ID", "UOM_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Currencylist = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(68), "document_numbring_id", "category");
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
            ViewBag.si_batch_list = new SelectList(_ISalesReturnService.getBuyer().sal_si_return_batch_vm.Select(a => new { batch_number = a.batch_number }), "batch_number", "batch_number");
            ViewBag.si_list = new SelectList(_ISalesReturnService.getBuyer().sal_si_vm.Select(a => new { si_id = a.si_id, si_number = a.si_number }), "si_id", "si_number");
            ViewBag.customerlist = new SelectList(_ISalesReturnService.getBuyer().CustomerVM.Select(a => new { customer_id = a.customer_id, customer_name = a.customer_name }), "customer_id", "customer_name");
            ViewBag.taxlist = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.TransportModelist = new SelectList(_Transport.GetAll(), "mode_of_transport_id", "mode_of_transport_name");
            ViewBag.GST_TDS_Code = new SelectList(_Generic.GetGSTTDSCodeList(), "gst_tds_code_id", "gst_tds_code");
            return View();
        }
        public ActionResult getsalesinvoicedetail(int plant_id,int buyer_id,int si_id,int consignee_id)
        {
            var list = _ISalesReturnService.getItems(plant_id, buyer_id, 0, "", si_id, "getsalesinvoicedetailbyitem", consignee_id);
            return Json(list,JsonRequestBehavior.AllowGet);
        }
      
       
        public ActionResult getsalesreturndetails(int buyer_id,int plant_id,int item_id,int consignee_id)
        {
            var sal_si = _ISalesReturnService.getItems(plant_id, buyer_id, item_id, "", 0, "getsalesreturndetail", consignee_id);
            return Json(sal_si, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult getItem(int buyer_id,int plant_id)
        {           
            var item = _ISalesReturnService.getItems(plant_id, buyer_id, 0, "", 0, "getItem",0).Select(a => new { item_id = a.item_id, item_name = a.item_name }).ToList();
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getItemforBatch(int item_id)
        {
            var item = _ISalesReturnService.getItems(0,0,item_id,"",0, "getItemforBatch",0).Select(a=> new { batch_number=a.batch_number==""?"empty": a.batch_number }).ToList();
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getItembyBatch(int plant_id,int buyer_id, int item_id,string batch_number,int consignee_id)
        {
            var item = _ISalesReturnService.getItems(plant_id, buyer_id, item_id, batch_number, 0, "getItembyBatch",consignee_id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getBatchNumber()
        {
            var item = _ISalesReturnService.getBuyer().sal_si_return_batch_vm.Select(a => new { batch_number = a.batch_number }).ToList();
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getSINumber(int buyer_id, int plant_id, int consignee_id)
        {
            var item = _ISalesReturnService.getItems(plant_id, buyer_id, 0, "", 0, "getSINumber", consignee_id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }
    }
}