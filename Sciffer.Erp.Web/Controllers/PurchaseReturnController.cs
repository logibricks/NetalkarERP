using CrystalDecisions.CrystalReports.Engine;
using Newtonsoft.Json;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class PurchaseReturnController : Controller
    {
        // GET: PurchaseInvoice
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
        private readonly IPurchaseReturnService _purchaseReturnService;
        private readonly IBucketService _bucketService;
        
        public PurchaseReturnController(IGenericService gen, IGrnService GrnService, IPaymentCycleTypeService PaymentCycleTypeService,
            ICountryService CountryService, IFormService FormService, IUOMService IUOMService, IPurchaseOrderService purchase,
            IPaymentCycleService PaymentCycleService, IPaymentTermsService PaymentTermsService, IStateService StateService,
            IFreightTermsService FreightTermsService, ICurrencyService CurrencyService, IBatchNumberingService batchService,
            IBucketService bucketService, IStorageLocation storageLocation, ICostCenterService costCenter, IPurchaseInvoiceService purchaseInvoice,
            ITdsCodeService tdsCode, IPurchaseReturnService PurchaseReturnService, IBucketService BucketService)
        {
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
            _purchaseReturnService = PurchaseReturnService;
            _bucketService = BucketService;
        }

        [CustomAuthorizeAttribute("PRRTN")]
        public ActionResult Index()
        {
            ViewBag.doc = TempData["doc_num"];
            ViewBag.dataSource = _purchaseReturnService.GetAll();
            return View();
        }

        [CustomAuthorizeAttribute("PRRTN")]
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(76), "document_numbring_id", "category");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freight_list = new SelectList(_freightTermsService.GetAll(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.payment_cycle_list = new SelectList(_paymentCycleService.GetAll(), "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.payment_terms_list = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.state_list = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("PUR_RETN"), "status_id", "status_name");
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
            ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            ViewBag.gst_list = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(pur_pi_return_vm pur_pi)
        {
            var issaved = "";
            if (ModelState.IsValid)
            {
                issaved = _purchaseReturnService.Add(pur_pi);
                if (issaved.Contains("Saved"))
                {
                    TempData["doc_num"] = issaved.Split('~')[1];
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = issaved;
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(76), "document_numbring_id", "category");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freight_list = new SelectList(_freightTermsService.GetAll(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.payment_cycle_list = new SelectList(_paymentCycleService.GetAll(), "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.payment_terms_list = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.state_list = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("PUR_RETN"), "status_id", "status_name");
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
            ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            ViewBag.gst_list = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            return View(pur_pi);
        }
        public JsonResult GetPiListForPI_return(int id)
        {
            var vm = _purchaseReturnService.GetPiListForPI_return(id);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPiForPI_return(int id)
        {
            var vm = _purchaseReturnService.PiforPireturn(id);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public JsonResult forPurchaseReturnPI(int pi_id, int item_id, int bucket_id, int storage_location_id,int plant_id)
        {
            var bucket = _purchaseReturnService.forPurchaseReturnPI(pi_id, item_id, bucket_id, storage_location_id, plant_id);
            return Json(bucket, JsonRequestBehavior.AllowGet);
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


        [CustomAuthorizeAttribute("PRRTN")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pur_pi_return_vm pur_pi = _purchaseReturnService.Get(id);
            if (pur_pi == null)
            {
                return HttpNotFound();
            }
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(76), "document_numbring_id", "category");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freight_list = new SelectList(_freightTermsService.GetAll(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.payment_cycle_list = new SelectList(_paymentCycleService.GetAll(), "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.payment_terms_list = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.state_list = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("PUR_RETN"), "status_id", "status_name");
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
            ViewBag.pi_list = new SelectList(_purchaseInvoice.GetAll(), "pi_id", "document_no");
            ViewBag.gst_list = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(76), "cancellation_reason_id", "cancellation_reason");
            return View(pur_pi);
        }

        public CrystalReportPdfResult Pdf(int id)
        {
            ReportDocument rd = new ReportDocument();
            try
            {

                var cd_note = _purchaseReturnService.GetPurchaseReturnheaderReport(id);
                var cd_note_detail = _purchaseReturnService.GetPurchaseReturnDetailsForReport(id);
                //DataSet ds = new DataSet("CreditDebitNoteDataset");

                System.Data.DataSet ds = new System.Data.DataSet("PuerchaseReturnDataset");

                //var exp = "";
                //double amt = 0;
                //foreach (var i in cd_note_detail)
                //{
                //    exp = exp + i.tax_id + "~" + i.assessable_value + "~" + i.purchase_value + ",";
                //    amt = amt + double.Parse(i.purchase_value.ToString());
                //}
                //var tax_detail = _Generic.GetTaxCalculation("calculatetax", exp.Remove(exp.Length - 1), amt, cd_note.posting_date, 0);

                var fin_credit_debit_note = new List<pur_pi_return_vm>();
                fin_credit_debit_note.Add(cd_note);
                var dt1 = _Generic.ToDataTable(fin_credit_debit_note);
                var dt2 = _Generic.ToDataTable(cd_note_detail);
                //var dt3 = _Generic.ToDataTable(tax_detail);
                dt1.TableName = "purchase_return";
                dt2.TableName = "purchase_return_detail";
                //dt3.TableName = "pur_po_detail";
                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);
                //ds.Tables.Add(dt3);

                rd.Load(Path.Combine(Server.MapPath("~/Reports/PurchaseReturn5.rpt")));
                //for (int i = 0; i < rd.Subreports.Count; i++)
                //{
                //    ReportDocument sr = rd.Subreports[i];
                //    sr.SetDataSource(ds);
                //}

                rd.SetDataSource(ds);
                ////rd.SetParameterValue("p1", "Origional");
                ////rd.SetParameterValue("p2", "Duplicate");
                ////rd.SetParameterValue("p3", "Triplicate");
                ////rd.SetParameterValue("p4", "Credit Note");
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string reportPath = Path.Combine(Server.MapPath("~/Reports"), "PurchaseReturn5.rpt");
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
                rd.Dispose();
                GC.Collect();
            }
            // return 1;

        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pur_pi_return_vm pur_pi = _purchaseReturnService.Get(id);
            if (pur_pi == null)
            {
                return HttpNotFound();
            }           
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(76), "document_numbring_id", "category");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freight_list = new SelectList(_freightTermsService.GetAll(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.payment_cycle_list = new SelectList(_paymentCycleService.GetAll(), "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.payment_terms_list = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.state_list = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("PUR_RETN"), "status_id", "status_name");
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
            ViewBag.pi_list = new SelectList(_purchaseInvoice.GetAll(), "pi_id", "document_no");
            ViewBag.gst_list = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(76), "cancellation_reason_id", "cancellation_reason");
            return View(pur_pi);
        }
        public ActionResult GetPiforPiReturn(int vendor_id)
        {
            var po = _purchaseReturnService.GetPiforPiReturn(vendor_id);
            return Json(po, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PurchaseReturnDetails(string entity,int buyer_id, int plant_id, string item_id, string sloc_id, string bucket_id, int pi_id)
        {
            var result = _purchaseReturnService.GetPurchasereturnDetail(entity, buyer_id, plant_id, item_id, sloc_id, bucket_id, pi_id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //public CrystalReportPdfResult Pdf(int id)
        //{
        //    var pi = _purchaseInvoice.GetPIDetailForReport(id);
        //    var pur_pi_detail = _purchaseInvoice.GetPIProductDetailForPI(id);
        //    var exp = "";
        //    double amt = 0;
        //    foreach (var i in pur_pi_detail)
        //    {
        //        exp = exp + i.tax_id + "~" + i.assesable_value + "~" + i.purchase_value + ",";
        //        amt = amt + i.purchase_value;
        //    }
        //    var tax_detail = _Generic.GetTaxCalculation(exp.Remove(exp.Length - 1), amt, pi.posting_date, (int)pi.tds_code_id);

        //    var pi_detail_id = 0;
        //    DataSet ds = new DataSet("pur_pi");
        //    var pur_pi = new List<pur_pi_VM>();
        //    pur_pi.Add(pi);
        //    var dt1 = _Generic.ToDataTable(pur_pi);
        //    var dt2 = _Generic.ToDataTable(pur_pi_detail);
        //    var dt3 = _Generic.ToDataTable(tax_detail);
        //    DataRow[] drPaytable = dt3.Select("tax_name like '%Excise%'");
        //    dt1.TableName = "pur_pi";
        //    dt2.TableName = "pur_pi_detail";
        //    dt3.TableName = "pi_tax_detail";
        //    double sum = 0;
        //    for (int i = 0; i < drPaytable.Length; i++)
        //    {
        //        sum = sum + double.Parse(drPaytable[0].ItemArray[1].ToString());
        //    }
        //    //dt1.Rows[0]["excise_amount"] = sum;
        //    ds.Tables.Add(dt1);
        //    ds.Tables.Add(dt2);
        //    dt3.Columns.Add("id", typeof(System.Int32));
        //    foreach (var so in pur_pi_detail)
        //    {
        //        pi_detail_id = so.pi_detail_id;
        //    }
        //    foreach (DataRow row in dt3.Rows)
        //    {
        //        //need to set value to NewColumn column
        //        row["id"] = pi_detail_id;   // or set it to some other value

        //    }
        //    ds.Tables.Add(dt3);
        //    ReportDocument rd = new ReportDocument();
        //    rd.Load(Path.Combine(Server.MapPath("~/Reports/PurchaseInvoicReport.rpt")));
        //    for (int i = 0; i < rd.Subreports.Count; i++)
        //    {
        //        ReportDocument sr = rd.Subreports[i];
        //        sr.SetDataSource(ds);
        //    }

        //    rd.SetParameterValue("p1", "1st Report");
        //    rd.SetParameterValue("p2", "2nd Report");
        //    rd.SetParameterValue("p3", "3rd Report");
        //    rd.SetParameterValue("p4", "4th Report");
        //    rd.SetParameterValue("p5", "5th Report");
        //    rd.SetParameterValue("p6", "6th Report");
        //    Response.Buffer = false;
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    string reportPath = Path.Combine(Server.MapPath("~/Reports"), "piSubReport.rpt");
        //    return new CrystalReportPdfResult(reportPath, rd);
        //    // return 1;

        //}

        public ActionResult DeleteConfirmed(int id, string cancellation_remarks, int reason_id)
        {
            var isValid = _purchaseReturnService.Delete(id, cancellation_remarks, reason_id);
            if (isValid.Contains("Cancelled"))
            {
                return Json(isValid);
            }
            return Json(isValid);
        }
    }
}