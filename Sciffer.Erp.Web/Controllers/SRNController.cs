using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class SRNController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly ISRNService _SrnService;
        public SRNController(IGenericService Generic, ISRNService SrnService)
        {
            _Generic = Generic;
            _SrnService = SrnService;
        }
        // GET: SRN
        [CustomAuthorizeAttribute("SRN")]
        public ActionResult Index()
        {

           
            ViewBag.doc = ""; 
            ViewBag.DataSource = _SrnService.GetAll();
            return View();
        }
        [CustomAuthorizeAttribute("SRN")]
        public ActionResult Create()
        {
            ViewBag.error = ""; 
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryListByModule("SRN"), "document_numbring_id", "category");
            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freight_list = new SelectList(_Generic.GetFreightTermsList(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.payment_cycle_list = new SelectList(_Generic.GetPaymentCycle(0), "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.payment_terms_list = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
           
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("GRN"), "status_id", "status_name");    
            ViewBag.country_list = new SelectList(_Generic.GetCountryList(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.payment_cycle_type_list = new SelectList(_Generic.GetPaymentCycleTypeList(), "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.uom_list = new SelectList(_Generic.GetUOMList(), "UOM_ID", "UOM_NAME");
            ViewBag.tax_list = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.storageLocationList = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "description");
            ViewBag.state_list = new SelectList(_Generic.GetState(0).Select(a => new { state_id = a.STATE_ID, state_name = a.STATE_NAME + "/" + a.state_ut_code }), "state_id", "state_name");
            ViewBag.gst_list = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(73), "cancellation_reason_id", "cancellation_reason");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View();
        }
        [HttpPost]
        public ActionResult Add(pur_srn_vm vm,List<pur_srn_detail_vm> detail)
        {
            var srn = _SrnService.Add(vm, detail);
            if(srn.Contains("Saved"))
            {
                TempData["data"] = srn.Split('~')[1] + " Saved Successfully!";
               // return RedirectToAction("Index");
                return Json(srn.Split('~')[1] + " Saved Successfully!", JsonRequestBehavior.AllowGet);
            }
            ViewBag.error = srn;
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryListByModule("SRN"), "document_numbring_id", "category");
            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freight_list = new SelectList(_Generic.GetFreightTermsList(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.payment_cycle_list = new SelectList(_Generic.GetPaymentCycle(0), "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.payment_terms_list = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.state_list = new SelectList(_Generic.GetState(0), "STATE_ID", "STATE_NAME");
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("GRN"), "status_id", "status_name");
            ViewBag.country_list = new SelectList(_Generic.GetCountryList(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.payment_cycle_type_list = new SelectList(_Generic.GetPaymentCycleTypeList(), "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.uom_list = new SelectList(_Generic.GetUOMList(), "UOM_ID", "UOM_NAME");
            ViewBag.tax_list = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.storageLocationList = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "description");
            ViewBag.state_list = new SelectList(_Generic.GetState(0).Select(a => new { state_id = a.STATE_ID, state_name = a.STATE_NAME + "/" + a.state_ut_code }), "state_id", "state_name");
            ViewBag.gst_list = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(73), "cancellation_reason_id", "cancellation_reason");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pur_srn_vm pur_srn = _SrnService.Get(id);
            if (pur_srn == null)
            {
                return HttpNotFound();
            }
            ViewBag.error = "";
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryListByModule("SRN"), "document_numbring_id", "category");
            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freight_list = new SelectList(_Generic.GetFreightTermsList(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.payment_cycle_list = new SelectList(_Generic.GetPaymentCycle(0), "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.payment_terms_list = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.state_list = new SelectList(_Generic.GetState(0), "STATE_ID", "STATE_NAME");
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("GRN"), "status_id", "status_name");
            ViewBag.country_list = new SelectList(_Generic.GetCountryList(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.payment_cycle_type_list = new SelectList(_Generic.GetPaymentCycleTypeList(), "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.uom_list = new SelectList(_Generic.GetUOMList(), "UOM_ID", "UOM_NAME");
            ViewBag.tax_list = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.storageLocationList = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "description");
            ViewBag.state_list = new SelectList(_Generic.GetState(0).Select(a => new { state_id = a.STATE_ID, state_name = a.STATE_NAME + "/" + a.state_ut_code }), "state_id", "state_name");
            ViewBag.gst_list = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(73), "cancellation_reason_id", "cancellation_reason");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View(pur_srn);
        }
    }
}