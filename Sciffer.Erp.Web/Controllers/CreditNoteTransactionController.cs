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
    public class CreditNoteTransactionController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        private readonly ICreditDebitNoteTransactionService _creditnotetransservice;
        private readonly ICreditDebitNoteService _creditnoteservice;
        private readonly IGenericService _Generic;
        private readonly ICurrencyService _currencyService;
        private readonly ICostCenterService _costCenterService;
        private readonly ITaxService _Tax;
        private readonly IItemTypeService _itemtype;
        private readonly ICustomerService _customer;
        private readonly IVendorService _vendor;
        private readonly ISACService _sACService;
        private readonly ISACService _saclist;
        private readonly IPaymentTermsService _PaymentTermsService;
        private readonly IPaymentCycleTypeService _PaymentCycleTypeService;
        private readonly IPaymentCycleService _PaymentCycleService;
        private readonly IStateService _StateService;
        private readonly ICountryService _CountryService;
        private readonly IPlantService _plantservice;
        private readonly IStatusService _statusService;
        private readonly IEmployeeService _empservice;
        private readonly ITdsCodeService _TdsCodeService;



        public CreditNoteTransactionController(ICreditDebitNoteService credit, IGenericService generic, ICurrencyService currency, IItemTypeService itemtype,
         ICostCenterService cosetcenter, ITaxService tax, ICustomerService customer, IVendorService vendor, ISACService sACService, IPaymentTermsService PaymentTermsService,
         IPaymentCycleTypeService PaymentCycleTypeService, IPaymentCycleService PaymentCycleService, IStateService StateService, ICreditDebitNoteTransactionService creditnotetransservice,
          ICountryService CountryService, IPlantService plantservice, IStatusService statusService, IEmployeeService empservice, ITdsCodeService TdsCodeService)
        {
            _creditnoteservice = credit;
            _Generic = generic;
            _currencyService = currency;
            _costCenterService = cosetcenter;
            _Tax = tax;
            _itemtype = itemtype;
            _customer = customer;
            _vendor = vendor;
            _sACService = sACService;
            _PaymentTermsService = PaymentTermsService;
            _PaymentCycleTypeService = PaymentCycleTypeService;
            _PaymentCycleService = PaymentCycleService;
            _CountryService = CountryService;
            _StateService = StateService;
            _plantservice = plantservice;
            _statusService = statusService;
            _empservice = empservice;
            _creditnotetransservice = creditnotetransservice;

            _TdsCodeService = TdsCodeService;
        }

        // GET: CreditNoteTransaction
        public ActionResult Index()
        {
            ViewBag.doc = TempData["doc_num"];
            ViewBag.DataSource = _creditnotetransservice.GetAll(1);
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fin_credit_debit_note_vm fin_credit_debit_note = _creditnotetransservice.Get((int)id);
            if (fin_credit_debit_note == null)
            {
                return HttpNotFound();
            }
            ViewBag.currency_list = new SelectList(_currencyService.GetCurrency2(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryListByModule("CN"), "document_numbring_id", "category");
            ViewBag.cost_list = new SelectList(_costCenterService.GetAll(), "cost_center_id", "cost_center_code");
            ViewBag.taxlist = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.gl_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag._entityTypeService = new SelectList(_Generic.GetEntityList().Where(x => x.ENTITY_TYPE_ID != 7)
                              .ToList(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");
            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            return View(fin_credit_debit_note);
        }

        // GET: CreditNote/Create
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.currency_list = new SelectList(_currencyService.GetCurrency2(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryListByModule("CN"), "document_numbring_id", "category");
            ViewBag.cost_list = new SelectList(_costCenterService.GetAll(), "cost_center_id", "cost_center_code");
            ViewBag.taxlist = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.gl_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag._entityTypeService = new SelectList(_Generic.GetEntityList().Where(x => x.ENTITY_TYPE_ID != 7).ToList(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");
            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.itemtype = new SelectList(_itemtype.GetAll(), "item_type_id", "item_type_name");
            ViewBag.customer_list = _customer.GetCustomerList();
            ViewBag.vendor_list = _vendor.GetVendorDetail();
            ViewBag.sac_list = new SelectList(_sACService.GetAll().Select(a => new { sac_id = a.sac_id, sac_code = a.sac_code + "/" + a.sac_description }), "sac_id", "sac_code");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.plant_list = _plantservice.GetAll();
            ViewBag.business_unit_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.payment_terms_list = new SelectList(_PaymentTermsService.GetAll().Where(a => a.is_blocked == false).Select(a => new { a.payment_terms_id, payment_terms_code = a.payment_terms_code + "/" + a.payment_terms_description }), "payment_terms_id", "payment_terms_code");
            ViewBag.payment_cycle_type_list = _PaymentCycleTypeService.GetAll();
            ViewBag.payment_cycle_list = _PaymentCycleService.GetAll();
            ViewBag.state_list = new SelectList(_StateService.GetAll().Where(a => a.is_blocked == false), "state_id", "state_name");
            ViewBag.country_list = new SelectList(_CountryService.GetAll().Where(a => a.is_blocked == false), "country_id", "country_name");
            ViewBag.employee_currency = _currencyService.GetCurrency2();
            ViewBag.employee_list = _empservice.GetAll();
            ViewBag.GST_CustomerType_List = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.tds_list = _TdsCodeService.GetAll().Select(a => new { a.tds_code_id, tds_code = a.tds_code + "/" + a.tds_code_description }).ToList();
            return View();
        }

        // POST: CreditNote/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(fin_credit_debit_note_vm fin_credit_debit_note)
        {
            var issaved = "";
            fin_credit_debit_note.credit_debit_id = 1;
            if (ModelState.IsValid)
            {
                issaved = _creditnotetransservice.Add(fin_credit_debit_note);
                if (issaved.Contains("Saved"))
                {
                    TempData["doc_num"] = issaved.Split('~')[1];
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = issaved;
            ViewBag.currency_list = new SelectList(_currencyService.GetCurrency2(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryListByModule("CN"), "document_numbring_id", "category");
            ViewBag.cost_list = new SelectList(_costCenterService.GetAll(), "cost_center_id", "cost_center_code");
            ViewBag.taxlist = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.gl_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag._entityTypeService = new SelectList(_Generic.GetEntityList().Where(x => x.ENTITY_TYPE_ID != 7).ToList(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");

            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.itemtype = new SelectList(_itemtype.GetAll(), "item_type_id", "item_type_name");
            ViewBag.customer_list = _customer.GetCustomerList();
            ViewBag.vendor_list = _vendor.GetVendorDetail();
            ViewBag.sac_list = new SelectList(_sACService.GetAll().Select(a => new { sac_id = a.sac_id, sac_code = a.sac_code + "/" + a.sac_description }), "sac_id", "sac_code");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.plant_list = _plantservice.GetAll();
            ViewBag.business_unit_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.payment_terms_list = new SelectList(_PaymentTermsService.GetAll().Where(a => a.is_blocked == false).Select(a => new { a.payment_terms_id, payment_terms_code = a.payment_terms_code + "/" + a.payment_terms_description }), "payment_terms_id", "payment_terms_code");
            ViewBag.payment_cycle_type_list = _PaymentCycleTypeService.GetAll();
            ViewBag.payment_cycle_list = _PaymentCycleService.GetAll();
            ViewBag.state_list = new SelectList(_StateService.GetAll().Where(a => a.is_blocked == false), "state_id", "state_name");
            ViewBag.country_list = new SelectList(_CountryService.GetAll().Where(a => a.is_blocked == false), "country_id", "country_name");
            ViewBag.employee_currency = _currencyService.GetCurrency2();
            ViewBag.employee_list = _empservice.GetAll();
            ViewBag.GST_CustomerType_List = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.tds_list = _TdsCodeService.GetAll().Select(a => new { a.tds_code_id, tds_code = a.tds_code + "/" + a.tds_code_description }).ToList();
            return View(fin_credit_debit_note);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(fin_credit_debit_note_vm fin_credit_debit_note)
        {
            var issaved = "";
            fin_credit_debit_note.credit_debit_id = 1;
            if (ModelState.IsValid)
            {
                issaved = _creditnotetransservice.Add(fin_credit_debit_note);
                if (issaved.Contains("Saved"))
                {
                    TempData["doc_num"] = issaved.Split('~')[1];
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = issaved;
            ViewBag.currency_list = new SelectList(_currencyService.GetCurrency2(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryListByModule("CN"), "document_numbring_id", "category");
            ViewBag.cost_list = new SelectList(_costCenterService.GetAll(), "cost_center_id", "cost_center_code");
            ViewBag.taxlist = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.gl_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag._entityTypeService = new SelectList(_Generic.GetEntityList().Where(x => x.ENTITY_TYPE_ID != 7).ToList(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");

            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.itemtype = new SelectList(_itemtype.GetAll(), "item_type_id", "item_type_name");
            ViewBag.customer_list = _customer.GetCustomerList();
            ViewBag.vendor_list = _vendor.GetVendorDetail();
            ViewBag.sac_list = new SelectList(_sACService.GetAll().Select(a => new { sac_id = a.sac_id, sac_code = a.sac_code + "/" + a.sac_description }), "sac_id", "sac_code");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.plant_list = _plantservice.GetAll();
            ViewBag.business_unit_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.payment_terms_list = new SelectList(_PaymentTermsService.GetAll().Where(a => a.is_blocked == false).Select(a => new { a.payment_terms_id, payment_terms_code = a.payment_terms_code + "/" + a.payment_terms_description }), "payment_terms_id", "payment_terms_code");
            ViewBag.payment_cycle_type_list = _PaymentCycleTypeService.GetAll();
            ViewBag.payment_cycle_list = _PaymentCycleService.GetAll();
            ViewBag.state_list = new SelectList(_StateService.GetAll().Where(a => a.is_blocked == false), "state_id", "state_name");
            ViewBag.country_list = new SelectList(_CountryService.GetAll().Where(a => a.is_blocked == false), "country_id", "country_name");
            ViewBag.employee_currency = _currencyService.GetCurrency2();
            ViewBag.employee_list = _empservice.GetAll();
            ViewBag.GST_CustomerType_List = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.tds_list = _TdsCodeService.GetAll().Select(a => new { a.tds_code_id, tds_code = a.tds_code + "/" + a.tds_code_description }).ToList();
            return View(fin_credit_debit_note);
        }
        // GET: CreditNote/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fin_credit_debit_note_vm fin_credit_debit_note = _creditnotetransservice.Get((int)id);
            if (fin_credit_debit_note == null)
            {
                return HttpNotFound();
            }
            ViewBag.currency_list = new SelectList(_currencyService.GetCurrency2(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryListByModule("CN"), "document_numbring_id", "category");
            ViewBag.cost_list = new SelectList(_costCenterService.GetAll(), "cost_center_id", "cost_center_code");
            ViewBag.taxlist = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.gl_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag._entityTypeService = new SelectList(_Generic.GetEntityList().Where(x => x.ENTITY_TYPE_ID != 7).ToList(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");
            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.itemtype = new SelectList(_itemtype.GetAll(), "item_type_id", "item_type_name");
            ViewBag.customer_list = _customer.GetCustomerList();
            ViewBag.vendor_list = _vendor.GetVendorDetail();
            ViewBag.sac_list = new SelectList(_sACService.GetAll().Select(a => new { sac_id = a.sac_id, sac_code = a.sac_code + "/" + a.sac_description }), "sac_id", "sac_code");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.plant_list = _plantservice.GetAll();
            ViewBag.business_unit_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.payment_terms_list = new SelectList(_PaymentTermsService.GetAll().Where(a => a.is_blocked == false).Select(a => new { a.payment_terms_id, payment_terms_code = a.payment_terms_code + "/" + a.payment_terms_description }), "payment_terms_id", "payment_terms_code");
            ViewBag.payment_cycle_type_list = _PaymentCycleTypeService.GetAll();
            ViewBag.payment_cycle_list = _PaymentCycleService.GetAll();
            ViewBag.state_list = new SelectList(_StateService.GetAll().Where(a => a.is_blocked == false), "state_id", "state_name");
            ViewBag.country_list = new SelectList(_CountryService.GetAll().Where(a => a.is_blocked == false), "country_id", "country_name");
            ViewBag.employee_currency = _currencyService.GetCurrency2();
            ViewBag.employee_list = _empservice.GetAll();
            ViewBag.GST_CustomerType_List = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.tds_list = _TdsCodeService.GetAll().Select(a => new { a.tds_code_id, tds_code = a.tds_code + "/" + a.tds_code_description }).ToList();
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONReason("CN"), "cancellation_reason_id", "cancellation_reason");
            return View(fin_credit_debit_note);
        }

      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _creditnotetransservice.Dispose();
            }
            base.Dispose(disposing);
        }
        public CrystalReportPdfResult Pdf(int id)
        {
            ReportDocument rd = new ReportDocument();
            try
            {

                var cd_note = _creditnotetransservice.GetCDNForReport(id);
                var cd_note_detail = _creditnotetransservice.GetCDNDetailsForReport(id); 
                DataSet ds = new DataSet("CreditDebitNoteDataset");
                var fin_credit_debit_note = new List<fin_credit_debit_note_vm>();
                fin_credit_debit_note.Add(cd_note);
                var dt1 = _Generic.ToDataTable(fin_credit_debit_note);
                var dt2 = _Generic.ToDataTable(cd_note_detail);              
                dt1.TableName = "fin_credit_debit_note";
                dt2.TableName = "fin_credit_debit_note_detail";
                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);
                rd.Load(Path.Combine(Server.MapPath("~/Reports/CreditDebitSubReport.rpt")));
                for (int i = 0; i < rd.Subreports.Count; i++)
                {
                    ReportDocument sr = rd.Subreports[i];
                    sr.SetDataSource(ds);
                }
               // rd.SetDataSource(ds);
                rd.SetParameterValue("p1", "Origional");
                rd.SetParameterValue("p2", "Duplicate");
                rd.SetParameterValue("p3", "Triplicate");
                rd.SetParameterValue("p4", "Credit Note");
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string reportPath = Path.Combine(Server.MapPath("~/Reports"), "CreditDebitSubReport.rpt");
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
            var isValid = _creditnotetransservice.Delete(id, cancellation_remarks, reason_id);
            if (isValid.Contains("Cancelled"))
            {
                return Json(isValid);
            }
            return Json(isValid);
        }
    }
}