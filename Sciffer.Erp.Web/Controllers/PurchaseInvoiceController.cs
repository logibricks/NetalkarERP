using CrystalDecisions.CrystalReports.Engine;
using Newtonsoft.Json;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class PurchaseInvoiceController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
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
        private readonly ICostCenterService _costCenter;
        private readonly IPurchaseInvoiceService _purchaseInvoice;
        private readonly ITdsCodeService _tdsCode;
        private readonly IItemTypeService _itemTypeService;
        public PurchaseInvoiceController(IGenericService gen, IGrnService GrnService, IPaymentCycleTypeService PaymentCycleTypeService,
            ICountryService CountryService, IFormService FormService, IUOMService IUOMService, IPurchaseOrderService purchase,
            IPaymentCycleService PaymentCycleService, IPaymentTermsService PaymentTermsService, IStateService StateService,
            IFreightTermsService FreightTermsService, ICurrencyService CurrencyService, IBatchNumberingService batchService,
            IBucketService bucketService, IStorageLocation storageLocation, ICostCenterService costCenter,IPurchaseInvoiceService purchaseInvoice,
            ITdsCodeService tdsCode, IItemTypeService ItemTypeService)
        {
            _itemTypeService = ItemTypeService;
            _tdsCode = tdsCode;
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
            _costCenter = costCenter;
            _purchaseInvoice = purchaseInvoice;

        }
        [CustomAuthorizeAttribute("PRPI")]
        public ActionResult Index()
        {
            ViewBag.doc = TempData["doc_num"];
            ViewBag.dataSource = _purchaseInvoice.GetAll();
            return View();
        }
        [CustomAuthorizeAttribute("PRPI")]
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(75), "document_numbring_id", "category");
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
            ViewBag.costCenterList = new SelectList(_costCenter.GetCostCenter(), "cost_center_id", "cost_center_code");
            ViewBag.tdslist = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.state_list = new SelectList(_stateService.GetStateList().Select(a => new { state_id = a.state_id, state_name = a.state_ut_code + " - " + a.state_name }), "state_id", "state_name");
            ViewBag.gst_list = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.item_type = new SelectList(_itemTypeService.GetAll(), "item_type_id", "item_type_name");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("PI"), "status_id", "status_name");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(75), "cancellation_reason_id", "cancellation_reason");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(pur_pi_VM pur_pi)
        {
            var issaved = "";
            if (ModelState.IsValid)
            {
                issaved = _purchaseInvoice.Add(pur_pi);
                if (issaved.Contains("Saved"))
                {
                    TempData["doc_num"] = issaved.Split('~')[1] + " Saved successfully!";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = issaved;
            var po_list = _Purchase.GetPOList(pur_pi.item_service_id, pur_pi.vendor_id);
            ViewBag.po_list = new SelectList(po_list, "po_id", "po_no");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(75), "document_numbring_id", "category");
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
            ViewBag.costCenterList = new SelectList(_costCenter.GetCostCenter(), "cost_center_id", "cost_center_code");
            ViewBag.tdslist = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.state_list = new SelectList(_stateService.GetStateList().Select(a => new { state_id = a.state_id, state_name = a.state_ut_code + " - " + a.state_name }), "state_id", "state_name");
            ViewBag.gst_list = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.item_type = new SelectList(_itemTypeService.GetAll(), "item_type_id", "item_type_name");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("PI"), "status_id", "status_name");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(75), "cancellation_reason_id", "cancellation_reason");
            return View(pur_pi);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(pur_pi_VM pur_pi)
        {
            var issaved = "";
            if (ModelState.IsValid)
            {
                issaved = _purchaseInvoice.Add(pur_pi);
                if (issaved.Contains("Saved"))
                {
                    TempData["doc_num"] = issaved.Split('~')[1] + " Updated successfully!";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = issaved;
            pur_pi_VM pi = _purchaseInvoice.Get(pur_pi.pi_id);
            if (pur_pi == null)
            {
                return HttpNotFound();
            }
            var po_list = _Purchase.GetPOList(pur_pi.item_service_id, pur_pi.vendor_id);
            ViewBag.po_list = new SelectList(po_list, "po_id", "po_no");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(75), "document_numbring_id", "category");
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
            ViewBag.costCenterList = new SelectList(_costCenter.GetCostCenter(), "cost_center_id", "cost_center_code");
            ViewBag.tdslist = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.state_list = new SelectList(_stateService.GetStateList().Select(a => new { state_id = a.state_id, state_name = a.state_ut_code + " - " + a.state_name }), "state_id", "state_name");
            ViewBag.gst_list = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.item_type = new SelectList(_itemTypeService.GetAll(), "item_type_id", "item_type_name");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("PI"), "status_id", "status_name");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(75), "cancellation_reason_id", "cancellation_reason");

            return View(pi);
        }
        public ActionResult GetGRNListForPI(int id)
        {
            var vm = _purchaseInvoice.GetGRNListForPI(id);
            var list = JsonConvert.SerializeObject(vm,
             Formatting.None,
                      new JsonSerializerSettings()
                      {
                          ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                      });

            return Content(list, "application/json");
        }
        public ActionResult GetPOList(int id)
        {
            var vm = _Purchase.GetBalancePOList(id);
            var list = JsonConvert.SerializeObject(vm,
             Formatting.None,
                      new JsonSerializerSettings()
                      {
                          ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                      });

            return Content(list, "application/json");
        }
        [CustomAuthorizeAttribute("PRPI")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pur_pi_VM pur_pi = _purchaseInvoice.Get(id);
            if (pur_pi == null)
            {
                return HttpNotFound();
            }
            var po_list = _Purchase.GetPOList(pur_pi.item_service_id, pur_pi.vendor_id);
            ViewBag.po_list = new SelectList(po_list, "po_id", "po_no");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(75), "document_numbring_id", "category");
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
            ViewBag.costCenterList = new SelectList(_costCenter.GetCostCenter(), "cost_center_id", "cost_center_code");
            ViewBag.tdslist = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.state_list = new SelectList(_stateService.GetStateList().Select(a => new { state_id = a.state_id, state_name = a.state_ut_code + " - " + a.state_name }), "state_id", "state_name");
            ViewBag.gst_list = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.item_type = new SelectList(_itemTypeService.GetAll(), "item_type_id", "item_type_name");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("PI"), "status_id", "status_name");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(75), "cancellation_reason_id", "cancellation_reason");
            return View(pur_pi);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pur_pi_VM pur_pi = _purchaseInvoice.Get(id);
            if (pur_pi == null)
            {
                return HttpNotFound();
            }
            var po_list = _Purchase.GetPOList(pur_pi.item_service_id, pur_pi.vendor_id);
            ViewBag.po_list = new SelectList(po_list, "po_id", "po_no");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(75), "document_numbring_id", "category");
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
            ViewBag.costCenterList = new SelectList(_costCenter.GetCostCenter(), "cost_center_id", "cost_center_code");
            ViewBag.tdslist = new SelectList(_Generic.GetTDSCodeList(), "tds_code_id", "tds_code_description");
            ViewBag.state_list = new SelectList(_stateService.GetStateList().Select(a => new { state_id = a.state_id, state_name = a.state_ut_code + " - " + a.state_name }), "state_id", "state_name");
            ViewBag.gst_list = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.item_type = new SelectList(_itemTypeService.GetAll(), "item_type_id", "item_type_name");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(75), "cancellation_reason_id", "cancellation_reason");
            return View(pur_pi);
        }
        public CrystalReportPdfResult Pdf(int id)
        {
            ReportDocument rd = new ReportDocument();
            try
            {

                var pi = _purchaseInvoice.GetPIForReport(id);
                var pur_pi_detail = _purchaseInvoice.GetPIDetailsForReport(id);
                var exp = "";
                double amt = 0;
                foreach (var i in pur_pi_detail)
                {
                    exp = exp + i.tax_id + "~" + i.assessable_value + "~" + i.purchase_value + "~" + i.round_off + ",";
                    amt = amt + (double)i.purchase_value;
                }
                var tax_detail = _Generic.GetTaxCalculation("calculatetaxforsalesreport", exp.Remove(exp.Length - 1), amt, (DateTime)pi.pi_date, (int)pi.tds_code_id);

                var pi_detail_id = 0;
                DataSet ds = new DataSet("pur_pi");
                var pur_pi = new List<pur_pi_report_vm>();
                pur_pi.Add(pi);
                var dt1 = _Generic.ToDataTable(pur_pi);
                var dt2 = _Generic.ToDataTable(pur_pi_detail);
                var dt3 = _Generic.ToDataTable(tax_detail);
                DataRow[] drPaytable = dt3.Select("tax_name like '%Total Tax Value%'");
                dt1.TableName = "pur_pi";
                dt2.TableName = "pur_pi_detail";
                dt3.TableName = "pur_pi_tax";
                double sum = 0;
                for (int i = 0; i < drPaytable.Length; i++)
                {
                    sum = sum + double.Parse(drPaytable[0].ItemArray[1].ToString());
                }
                dt1.Rows[0]["total_tax_value"] = sum;
                var N = 1;
                dt2.Columns.Add("id", typeof(System.Int32));
                foreach (DataRow row in dt2.Rows)
                {
                    //need to set value to NewColumn column
                    row["id"] = N;   // or set it to some other value
                    //row["pi_detail_id"] = N;
                    N = N + 1;
                }
                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);
                dt3.Columns.Add("id", typeof(System.Int32));
                foreach (var so in pur_pi_detail)
                {
                    pi_detail_id = so.pi_detail_id;
                }
                foreach (DataRow row in dt3.Rows)
                {
                    //need to set value to NewColumn column
                    row["id"] = N - 1;   // or set it to some other value

                }
                ds.Tables.Add(dt3);

                rd.Load(Path.Combine(Server.MapPath("~/Reports/PurchaseRcmReport.rpt")));
                rd.SetDataSource(ds);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string reportPath = Path.Combine(Server.MapPath("~/Reports"), "PurchaseRcmReport.rpt");
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
                rd.Dispose();
                GC.Collect();
            }
            // return 1;

        }
        public ActionResult DeleteConfirmed(int id, string cancellation_remarks, int reason_id)
        {
            var isValid = _purchaseInvoice.Delete(id, cancellation_remarks, reason_id);
            if (isValid.Contains("Cancelled"))
            {
                return Json(isValid);
            }
            return Json(isValid);
        }
    }
}