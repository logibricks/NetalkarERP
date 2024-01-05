using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.ViewModel;
using Newtonsoft.Json;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Data;
using System.Linq;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class GRNController : Controller
    {
        private readonly ICurrencyService _currencyService;
        private readonly IFreightTermsService _freightTermsService;
        private readonly IStateService _stateService;
        private readonly IPaymentTermsService _paymentTermsService;
        private readonly IPaymentCycleService _paymentCycleService;
        private readonly IUOMService _uOMService;
        private readonly IPurchaseOrderService _Purchase;
        private readonly IFormService _formService;
        private readonly ICountryService _countryService;
        private readonly IPaymentCycleTypeService _paymentCycleTypeService;
        private readonly IGrnService _grnService;
        private readonly IBucketService _bucketservice;
        private readonly IBatchNumberingService _batchService;
        private readonly IGenericService _Generic;
        private readonly IStorageLocation _storageLocation;
        private readonly IItemService _itemService;
        public GRNController(IGenericService gen, IGrnService GrnService, IPaymentCycleTypeService PaymentCycleTypeService,
            ICountryService CountryService, IFormService FormService, IUOMService IUOMService, IPurchaseOrderService purchase,
            IPaymentCycleService PaymentCycleService, IPaymentTermsService PaymentTermsService, IStateService StateService,
            IFreightTermsService FreightTermsService, ICurrencyService CurrencyService, IBatchNumberingService batchService,
            IBucketService bucketService, IStorageLocation storageLocation)
        {

            _Purchase = purchase;
            _currencyService = CurrencyService;
            _freightTermsService = FreightTermsService;
            _stateService = StateService;
            _paymentTermsService = PaymentTermsService;
            _paymentCycleService = PaymentCycleService;
            _uOMService = IUOMService;
            _formService = FormService;
            _countryService = CountryService;
            _paymentCycleTypeService = PaymentCycleTypeService;
            _grnService = GrnService;
            _Generic = gen;
            _batchService = batchService;
            _bucketservice = bucketService;
            _storageLocation = storageLocation;
        }
        // GET: GRN
        [CustomAuthorizeAttribute("GRN")]
        public ActionResult Index()
        {
            ViewBag.doc = TempData["doc_num"];
            ViewBag.DataSource = _grnService.getall().Take(100);
            return View();
        }


        // GET: GRN/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pur_grnVM pur_grn = _grnService.Get(id);
            if (pur_grn == null)
            {
                return HttpNotFound();
            }
            // ViewBag.po_list = _Purchase.GetPOList(1);
            var po_list = _Purchase.GetPOList(1, pur_grn.vendor_id);
            ViewBag.po_list = new SelectList(po_list, "po_id", "po_no");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(73), "document_numbring_id", "category");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freight_list = new SelectList(_freightTermsService.GetAll(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.payment_cycle_list = new SelectList(_paymentCycleService.GetAll(), "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.payment_terms_list = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.state_list = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("GRN"), "status_id", "status_name");
            ViewBag.form_list = new SelectList(_formService.GetAll(), "FORM_ID", "FORM_NAME");
            ViewBag.country_list = new SelectList(_countryService.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.payment_cycle_type_list = new SelectList(_paymentCycleTypeService.GetAll(), "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.tax_list = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.bucketList = new SelectList(_bucketservice.GetAll(), "bucket_id", "bucket_name");
            ViewBag.batchList = new SelectList(_batchService.GetAll(), "batch_no_id", "to_number");
            ViewBag.storageLocationList = new SelectList(_storageLocation.getstoragelist(), "storage_location_id", "storage_location_name");
            ViewBag.state_list = new SelectList(_stateService.GetStateList().Select(a => new { state_id = a.state_id, state_name = a.state_name + "/" + a.state_ut_code }), "state_id", "state_name");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(73), "cancellation_reason_id", "cancellation_reason");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View(pur_grn);
        }

        // GET: GRN/Create
        [CustomAuthorizeAttribute("GRN")]
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.bucketList = new SelectList(_bucketservice.GetAll(), "bucket_id", "bucket_name");
            ViewBag.batchList = new SelectList(_batchService.GetAll(), "batch_no_id", "to_number");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(73), "document_numbring_id", "category");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freight_list = new SelectList(_freightTermsService.GetAll(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.payment_cycle_list = new SelectList(_paymentCycleService.GetAll(), "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.payment_terms_list = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.state_list = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("GRN"), "status_id", "status_name");
            ViewBag.form_list = new SelectList(_formService.GetAll(), "FORM_ID", "FORM_NAME");
            ViewBag.country_list = new SelectList(_countryService.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.payment_cycle_type_list = new SelectList(_paymentCycleTypeService.GetAll(), "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.tax_list = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.storageLocationList = new SelectList(_storageLocation.getstoragelist(), "storage_location_id", "description");
            ViewBag.state_list = new SelectList(_stateService.GetStateList().Select(a => new { state_id = a.state_id, state_name = a.state_name + "/" + a.state_ut_code }), "state_id", "state_name");
            ViewBag.gst_list = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(73), "cancellation_reason_id", "cancellation_reason");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View();
        }

        // POST: GRN/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(pur_grnVM pur_grn)
        {
            var issaved = "";
            if (ModelState.IsValid)
            {
                issaved = _grnService.Add(pur_grn);
                if (issaved.Contains("Saved"))
                {
                    TempData["doc_num"] = issaved.Split('~')[1];
                    return RedirectToAction("Index", TempData["doc_num"]);
                }
            }
            ViewBag.error = issaved;
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(73), "document_numbring_id", "category");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freight_list = new SelectList(_freightTermsService.GetAll(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.payment_cycle_list = new SelectList(_paymentCycleService.GetAll(), "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.payment_terms_list = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.state_list = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("GRN"), "status_id", "status_name");
            ViewBag.form_list = new SelectList(_formService.GetAll(), "FORM_ID", "FORM_NAME");
            ViewBag.country_list = new SelectList(_countryService.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.payment_cycle_type_list = new SelectList(_paymentCycleTypeService.GetAll(), "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.tax_list = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.bucketList = new SelectList(_bucketservice.GetAll(), "bucket_id", "bucket_name");
            ViewBag.batchList = new SelectList(_batchService.GetAll(), "batch_no_id", "to_number");
            ViewBag.storageLocationList = new SelectList(_storageLocation.getstoragelist(), "storage_location_id", "storage_location_name");
            ViewBag.state_list = new SelectList(_stateService.GetStateList().Select(a => new { state_id = a.state_id, state_name = a.state_name + "/" + a.state_ut_code }), "state_id", "state_name");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.gst_list = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(73), "cancellation_reason_id", "cancellation_reason");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View(pur_grn);
        }
        public ActionResult GetPOProductForGRN(int id, DateTime? posting_date)
        {
            var vm = _Purchase.GetPOProductForGRN("getpodetailforgrn", id, posting_date);
            var list = JsonConvert.SerializeObject(vm,
             Formatting.None,
                      new JsonSerializerSettings()
                      {
                          ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                      });

            return Content(list, "application/json");
        }
        public ActionResult GetBatchNumering(int item_category, int plant_id)
        {
            var batchcategory = _batchService.GetCategory(item_category, plant_id);
            var list = JsonConvert.SerializeObject(batchcategory,
             Formatting.None,
                      new JsonSerializerSettings()
                      {
                          ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                      });

            return Content(list, "application/json");
        }
        public ActionResult GetPOList(int vendor_id)
        {
            var po_list = _Purchase.GetPOList(1, vendor_id);
            return Json(new { item = po_list }, JsonRequestBehavior.AllowGet);
        }
        // GET: GRN/Edit/5
        [CustomAuthorizeAttribute("GRN")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pur_grnVM pur_grn = _grnService.Get(id);
            if (pur_grn == null)
            {
                return HttpNotFound();
            }
            var po_list = _Purchase.GetPOList(1, pur_grn.vendor_id);
            ViewBag.po_list = new SelectList(po_list, "po_id", "po_no");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(73), "document_numbring_id", "category");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freight_list = new SelectList(_freightTermsService.GetAll(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.payment_cycle_list = new SelectList(_paymentCycleService.GetAll(), "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.payment_terms_list = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.state_list = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("GRN"), "status_id", "status_name");
            ViewBag.form_list = new SelectList(_formService.GetAll(), "FORM_ID", "FORM_NAME");
            ViewBag.country_list = new SelectList(_countryService.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.payment_cycle_type_list = new SelectList(_paymentCycleTypeService.GetAll(), "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.tax_list = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.bucketList = new SelectList(_bucketservice.GetAll(), "bucket_id", "bucket_name");
            ViewBag.batchList = new SelectList(_batchService.GetAll(), "batch_no_id", "to_number");
            ViewBag.storageLocationList = new SelectList(_storageLocation.getstoragelist(), "storage_location_id", "storage_location_name");
            ViewBag.state_list = new SelectList(_stateService.GetStateList().Select(a => new { state_id = a.state_id, state_name = a.state_name + "/" + a.state_ut_code }), "state_id", "state_name");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.gst_list = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(73), "cancellation_reason_id", "cancellation_reason");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View(pur_grn);
        }

        // POST: GRN/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(pur_grnVM pur_grn, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                var isValid = _grnService.Add(pur_grn);
                if (isValid != "error")
                {
                    TempData["doc_num"] = isValid;
                    return RedirectToAction("Index", TempData["doc_num"]);
                }

            }
            var po_list = _Purchase.GetPOList(1, pur_grn.vendor_id);
            ViewBag.po_list = new SelectList(po_list, "po_id", "po_no");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(73), "document_numbring_id", "category");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freight_list = new SelectList(_freightTermsService.GetAll(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.payment_cycle_list = new SelectList(_paymentCycleService.GetAll(), "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.payment_terms_list = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.state_list = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("GRN"), "status_id", "status_name");
            ViewBag.form_list = new SelectList(_formService.GetAll(), "FORM_ID", "FORM_NAME");
            ViewBag.country_list = new SelectList(_countryService.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.payment_cycle_type_list = new SelectList(_paymentCycleTypeService.GetAll(), "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.tax_list = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.bucketList = new SelectList(_bucketservice.GetAll(), "bucket_id", "bucket_name");
            ViewBag.batchList = new SelectList(_batchService.GetAll(), "batch_no_id", "to_number");
            ViewBag.storageLocationList = new SelectList(_storageLocation.getstoragelist(), "storage_location_id", "storage_location_name");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.gst_list = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(73), "cancellation_reason_id", "cancellation_reason");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View(pur_grn);
        }

        // GET: GRN/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pur_grnVM pur_grn = _grnService.Get(id);
            if (pur_grn == null)
            {
                return HttpNotFound();
            }
            return View(pur_grn);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _grnService.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult GetPOForGRN(int id)
        {
            var vm = _grnService.GetQuotationForGRN(id);
            var list = JsonConvert.SerializeObject(vm,
             Formatting.None,
                      new JsonSerializerSettings()
                      {
                          ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                      });

            return Content(list, "application/json");

        }
        public ActionResult GetBatchForExpiraryDate(string batch)
        {
            DateTime? batchdate = _grnService.GetBatchForExpiraryDate(batch);
            var list = JsonConvert.SerializeObject(batchdate,
            Formatting.None,
                     new JsonSerializerSettings()
                     {
                         ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                     });

            return Content(list, "application/json"); ;
        }
        public CrystalReportPdfResult Pdf(int id)
        {
            ReportDocument rd = new ReportDocument();
            try
            {
                var grn = _grnService.GetGrnForReport(id);
                var grn_detail = _grnService.GetGrnDetailForReport(id);

                DataSet ds = new DataSet("grn");
                var pur_grn = new List<pur_grnViewModel>();
                pur_grn.Add(grn);
                var dt1 = _Generic.ToDataTable(pur_grn);
                var dt2 = _Generic.ToDataTable(grn_detail);
                dt1.TableName = "pur_grn";
                dt2.TableName = "pur_grn_detail";
                double sum = 0;
                foreach (var g in grn_detail)
                {
                    sum = sum + g.quantity;
                }
                dt1.Rows[0]["total_qty"] = sum;
                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);


                rd.Load(Path.Combine(Server.MapPath("~/Reports/GRNReport.rpt")));
                rd.SetDataSource(ds);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string reportPath = Path.Combine(Server.MapPath("~/Reports"), "GRNReport.rpt");
                Console.WriteLine(reportPath);
                return new CrystalReportPdfResult(reportPath, rd);
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
        public ActionResult DeleteConfirmed(int id, string cancellation_remarks, int reason_id)
        {
            var isValid = _grnService.Delete(id, cancellation_remarks, reason_id);
            if (isValid.Contains("Cancelled"))
            {
                return Json(isValid);
            }
            return Json(isValid);
        }
    }
}
