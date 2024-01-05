using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class JournalEntryController : Controller
    {


        private readonly IFinLedgerService _finLedgerService;
        private readonly IGenericService _Generic;
        private readonly IVendorService _vendorService;
        private readonly ICustomerService _customerService;
        private readonly IEmployeeService _employeeService;
        private readonly ICurrencyService _currencyService;
        private readonly ICostCenterService _costCenterService;
        public JournalEntryController(ICostCenterService CostCenterService, ICurrencyService CurrencyService, IEmployeeService EmployeeService, ICustomerService CustomerService, IVendorService VendorService, IGenericService gen, IFinLedgerService FinLedgerService)
        {
            _customerService = CustomerService;
            _employeeService = EmployeeService;

            _finLedgerService = FinLedgerService;
            _Generic = gen;
            _vendorService = VendorService;

            _currencyService = CurrencyService;
            _costCenterService = CostCenterService;
        }
        [CustomAuthorizeAttribute("JE")]
        // GET: JournalEntry
        public ActionResult Index()
        {
            ViewBag.doc = TempData["doc_num"];
            ViewBag.DataSource = _finLedgerService.getall();
            return View();
        }



        // GET: JournalEntry/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fin_ledgerVM journal_entry = _finLedgerService.Get(id);
            if (journal_entry == null)
            {
                return HttpNotFound();
            }

            ViewBag._entityTypeService = new SelectList(_Generic.GetEntityList(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");
            return View(journal_entry);
        }
        [CustomAuthorizeAttribute("JE")]
        // GET: JournalEntry/Create
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag._entityTypeService = new SelectList(_Generic.GetEntityList(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(86), "document_numbring_id", "category");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.cost_list = new SelectList(_costCenterService.GetAll(), "cost_center_id", "cost_center_code");
            return View();
        }

        // POST: JournalEntry/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(fin_ledgerVM journal_entry)
        {
            var issaved = "";
            if (ModelState.IsValid)
            {
                issaved = _finLedgerService.Add(journal_entry);
                if (issaved.Contains("Saved"))
                {
                    TempData["doc_num"] = issaved.Split('~')[1] + " Saved successfully!";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = issaved;
            ViewBag._entityTypeService = new SelectList(_Generic.GetEntityList(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(86), "document_numbring_id", "category");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.cost_list = new SelectList(_costCenterService.GetAll(), "cost_center_id", "cost_center_code");
            return View(journal_entry);
        }
        [CustomAuthorizeAttribute("JE")]
        // GET: JournalEntry/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fin_ledgerVM journal_entry = _finLedgerService.Get(id);

            if (journal_entry == null)
            {
                return HttpNotFound();
            }

            ViewBag._entityTypeService = new SelectList(_Generic.GetEntityList(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(86), "document_numbring_id", "category");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.cost_list = new SelectList(_costCenterService.GetAll(), "cost_center_id", "cost_center_code");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(86), "cancellation_reason_id", "cancellation_reason");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View(journal_entry);
        }

        // POST: JournalEntry/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(fin_ledgerVM journal_entry)
        {
            var issaved = "";
            if (ModelState.IsValid)
            {
                issaved = _finLedgerService.Add(journal_entry);
                if (issaved.Contains("Saved"))
                {
                    TempData["doc_num"] = issaved.Split('~')[1] + " Updated successfully!";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = issaved;
            ViewBag._entityTypeService = new SelectList(_Generic.GetEntityList(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(86), "document_numbring_id", "category");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.cost_list = new SelectList(_costCenterService.GetAll(), "cost_center_id", "cost_center_code");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(86), "cancellation_reason_id", "cancellation_reason");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View(journal_entry);
        }

        // GET: JournalEntry/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fin_ledgerVM journal_entry = _finLedgerService.Get(id);
            if (journal_entry == null)
            {
                return HttpNotFound();
            }
            return View(journal_entry);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _finLedgerService.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult DeleteConfirmed(int id, string cancellation_remarks, int cancellation_reason_id)
        {
            var isValid = _finLedgerService.Delete(id, cancellation_remarks, cancellation_reason_id);
            if (isValid.Contains("Cancelled"))
            {
                return Json(isValid);
            }
            return Json(isValid);
        }
    }
}
