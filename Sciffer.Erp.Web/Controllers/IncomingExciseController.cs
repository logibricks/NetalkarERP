using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.ViewModel;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class IncomingExciseController : Controller
    {
        private IBusinessUnitService _businessUnitService;
        private ICategoryService _categoryService;
        private IPaymentCycleService _paymentCycleService;
        private ICurrencyService _currencyService;
        private IVendorService _vendorService;
        private IPlantService _plantService;
        private IStateService _stateService;
        private IItemService _itemService;
        private IPaymentTermsService _paymentTermsService;
        private IFreightTermsService _freightTermsService;
        private IPaymentCycleTypeService _paymentCycleTypeService;
        private IAccountAssignmentService _accountAssignmentService;
        private IStorageLocation _storageLocation;
        private IUOMService _uOMService;
        private ICountryService _contryService;
        private ITaxService _taxService;
        private IDocumentNumbringService _documentNumbringService;
        private IGenericService _Generic;
        private IIncomingExciseService _incomingExciseService;
        private IGrnService _GRN;
        public IncomingExciseController(IIncomingExciseService IncomingExciseService,IGenericService GenericService,
            IDocumentNumbringService DocumentNumbringService,ITaxService TaxService,ICountryService CountryService,IUOMService UOMService,
            IStorageLocation StorageLocation,IAccountAssignmentService AccountAssignmentService,
            IPaymentCycleTypeService PaymentCycleTypeService,IFreightTermsService FreightTermsService,
            IPaymentTermsService PaymentTermsService,IItemService ItemService,IStateService StateService,IPlantService PlantService,
            IVendorService VendorService,ICurrencyService CurrencyService,IBusinessUnitService BusinessUnitService, 
            ICategoryService CategoryService, IPaymentCycleService PaymentCycleService, IGrnService grn)
        {
            _GRN = grn;
            _businessUnitService = BusinessUnitService;
            _categoryService = CategoryService;
            _paymentCycleService = PaymentCycleService;
            _currencyService = CurrencyService;
            _vendorService = VendorService;
            _plantService = PlantService;
            _stateService = StateService;
            _itemService = ItemService;
            _paymentTermsService = PaymentTermsService;
            _freightTermsService = FreightTermsService;
            _paymentCycleTypeService = PaymentCycleTypeService;
            _accountAssignmentService = AccountAssignmentService;
            _storageLocation = StorageLocation;
            _uOMService = UOMService;
            _contryService = CountryService;
            _taxService = TaxService;
            _documentNumbringService = DocumentNumbringService;
            _Generic = GenericService;
            _incomingExciseService = IncomingExciseService;
        }

        // GET: IncomingExcise
        [CustomAuthorizeAttribute("INEX")]
        public ActionResult Index()
        {
            ViewBag.doc = TempData["doc_num"];
            ViewBag.ExciseList = _incomingExciseService.GetAll();
            return View();
        }

        // GET: IncomingExcise/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pur_incoming_excise_vm ref_incoming_excise = _incomingExciseService.Get((int)id);
            if (ref_incoming_excise == null)
            {
                return HttpNotFound();
            }
            ViewBag.business_unit_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(74), "document_numbring_id", "category");
            ViewBag.gross_currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freight_terms_list = new SelectList(_Generic.GetFreightTermsList(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.payment_cycle_list = new SelectList(_paymentCycleService.GetAll(), "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.payment_cycle_type_list = new SelectList(_paymentCycleTypeService.GetAll(), "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.payment_terms_list = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.billing_state_list = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.country_list = new SelectList(_contryService.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.tax_list = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(74), "cancellation_reason_id", "cancellation_reason");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("IE"), "status_id", "status_name");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View(ref_incoming_excise);
        }

        // GET: IncomingExcise/Create
        [CustomAuthorizeAttribute("INEX")]
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.business_unit_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(74), "document_numbring_id", "category");
            ViewBag.gross_currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freight_terms_list = new SelectList(_Generic.GetFreightTermsList(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.payment_cycle_list = new SelectList(_paymentCycleService.GetAll(), "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.payment_cycle_type_list = new SelectList(_paymentCycleTypeService.GetAll(), "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.payment_terms_list = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.billing_state_list = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.country_list = new SelectList(_contryService.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.tax_list = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(74), "cancellation_reason_id", "cancellation_reason");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("IE"), "status_id", "status_name");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View();
        }

        // POST: IncomingExcise/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(pur_incoming_excise_vm ref_incoming_excise)
        {
            var issaved = "";
            if (ModelState.IsValid)
            {
                issaved = _incomingExciseService.Add(ref_incoming_excise);
                if (issaved.Contains("Saved"))
                {
                    TempData["doc_num"] = issaved.Split('~')[1] + " Saved successfully!";
                    return RedirectToAction("Index", TempData["doc_num"]);
                }
            }
            ViewBag.error = issaved;
            ViewBag.business_unit_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(74), "document_numbring_id", "category");
            ViewBag.gross_currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freight_terms_list = new SelectList(_Generic.GetFreightTermsList(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.payment_cycle_list = new SelectList(_paymentCycleService.GetAll(), "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.payment_cycle_type_list = new SelectList(_paymentCycleTypeService.GetAll(), "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.payment_terms_list = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.billing_state_list = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.country_list = new SelectList(_contryService.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.tax_list = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(74), "cancellation_reason_id", "cancellation_reason");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("IE"), "status_id", "status_name");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View(ref_incoming_excise);
        }

        // GET: IncomingExcise/Edit/5
        [CustomAuthorizeAttribute("INEX")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pur_incoming_excise_vm ref_incoming_excise = _incomingExciseService.Get((int)id);
            if (ref_incoming_excise == null)
            {
                return HttpNotFound();
            }
            ViewBag.business_unit_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(74), "document_numbring_id", "category");
            ViewBag.gross_currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freight_terms_list = new SelectList(_Generic.GetFreightTermsList(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.payment_cycle_list = new SelectList(_paymentCycleService.GetAll(), "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.payment_cycle_type_list = new SelectList(_paymentCycleTypeService.GetAll(), "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.payment_terms_list = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.billing_state_list = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.country_list = new SelectList(_contryService.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.tax_list = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(74), "cancellation_reason_id", "cancellation_reason");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("IE"), "status_id", "status_name");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View(ref_incoming_excise);
        }

        // POST: IncomingExcise/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(pur_incoming_excise_vm ref_incoming_excise, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                var isValid = _incomingExciseService.Add(ref_incoming_excise);
                if (isValid != "error")
                {
                    TempData["doc_num"] = isValid.Split('~')[1] + " Updated successfully!";
                    return RedirectToAction("Index", TempData["doc_num"]);
                }
            }

            ViewBag.business_unit_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(74), "document_numbring_id", "category");
            ViewBag.gross_currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freight_terms_list = new SelectList(_Generic.GetFreightTermsList(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.payment_cycle_list = new SelectList(_paymentCycleService.GetAll(), "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.payment_cycle_type_list = new SelectList(_paymentCycleTypeService.GetAll(), "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.payment_terms_list = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.billing_state_list = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.country_list = new SelectList(_contryService.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.tax_list = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(74), "cancellation_reason_id", "cancellation_reason");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("IE"), "status_id", "status_name");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View(ref_incoming_excise);
        }

        // GET: IncomingExcise/Delete/5
      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _incomingExciseService.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult GetGrnListForIEX(int id)
        {
            var po_list = _GRN.GetGrnListForIEX(id);
            return Json(po_list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGrnProductListForIEX(int id)
        {
            var grn_list = _incomingExciseService.GetGRNProductList(id);
            return Json(grn_list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetIncomingExciseLineTax(string entity,string tax,double amt,DateTime posting_date,int tds_code_id)
        {
            var tax_list = _incomingExciseService.GetIncomingExciseTax(entity,tax,amt,posting_date,tds_code_id);
            return Json(tax_list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGrnDetailForIEX(int id)
        {
            var grn = _incomingExciseService.GetGrnDetailForIEX(id);
            return Json(grn, JsonRequestBehavior.AllowGet);
        }

        public CrystalReportPdfResult Pdf(int id)
        {
            ReportDocument rd = new ReportDocument();
            try
            {
                var IEX = _incomingExciseService.GetIEXForReport(id);
                var IEX_Detail = _incomingExciseService.GetDetailIEXForReport(id);


                var exp = "";
                double amt = 0;
                foreach (var i in IEX_Detail)
                {
                    exp = exp + i.Tax_Code + "~" + i.Ass_value + "~" + i.Purchase_value + ",";
                    amt = amt + double.Parse(i.Purchase_value.ToString());
                }

                var tax_detail = _Generic.GetTaxCalculation("calculate", exp.Remove(exp.Length - 1), amt, IEX.excise_date, 0);

                var iex_detail_id = 0;
                DataSet ds = new DataSet("IEXDataSet"); //dataset Name

                var ss = new List<pur_incoming_excise_report_vm>();
                ss.Add(IEX);

                var dt1 = _Generic.ToDataTable(ss);
                var dt2 = _Generic.ToDataTable(IEX_Detail);
                var dt3 = _Generic.ToDataTable(tax_detail);

                dt1.TableName = "IEX"; //datatable name
                dt2.TableName = "IEXDetails";
                dt3.TableName = "IEX_tax_detail";

                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);

                dt3.Columns.Add("id", typeof(System.Int32));
                dt1.Columns.Add("excise_amount", typeof(double));

                foreach (var so in IEX_Detail)
                {
                    iex_detail_id = so.iexdetail;
                }
                foreach (DataRow row in dt3.Rows)
                {
                    //need to set value to NewColumn column
                    row["id"] = iex_detail_id;   // or set it to some other value
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


                rd.Load(Path.Combine(Server.MapPath("~/Reports/IEXReport.rpt")));
                rd.SetDataSource(ds);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string reportPath = Path.Combine(Server.MapPath("~/Reports"), "IEXReport.rpt");
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
            var isValid = _incomingExciseService.Delete(id, cancellation_remarks, reason_id);
            if (isValid.Contains("Cancelled"))
            {
                return Json(isValid);
            }
            return Json(isValid);
        }

    }
  
}
