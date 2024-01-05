using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.ViewModel;
using System.Linq;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class CreditNoteController : Controller
    {
        private readonly ICreditDebitNoteService _creditnoteservice;
        private readonly IGenericService _Generic;
        private readonly ICurrencyService _currencyService;
        private readonly ICostCenterService _costCenterService;
        private readonly ITaxService _Tax;
        public CreditNoteController(ICreditDebitNoteService credit, IGenericService generic, ICurrencyService currency, 
            ICostCenterService cosetcenter, ITaxService tax)
        {
            _creditnoteservice = credit;
            _Generic = generic;
            _currencyService = currency;
            _costCenterService = cosetcenter;
            _Tax = tax;
        }
        [CustomAuthorizeAttribute("CN")]
        // GET: CreditNote
        public ActionResult Index()
        {
            ViewBag.doc = TempData["doc_num"];
            ViewBag.DataSource = _creditnoteservice.GetAll(1);
            return View();
        }

        // GET: CreditNote/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fin_credit_debit_note_vm fin_credit_debit_note = _creditnoteservice.Get((int)id);
            if (fin_credit_debit_note == null)
            {
                return HttpNotFound();
            }
            ViewBag.currency_list = new SelectList(_currencyService.GetCurrency2(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(88), "document_numbring_id", "category");
            ViewBag.cost_list = new SelectList(_costCenterService.GetAll(), "cost_center_id", "cost_center_code");
            ViewBag.taxlist = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.gl_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag._entityTypeService = new SelectList(_Generic.GetEntityList().Where(x => x.ENTITY_TYPE_ID != 7)
                              .ToList(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");
            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            return View(fin_credit_debit_note);
        }
        [CustomAuthorizeAttribute("CN")]
        // GET: CreditNote/Create
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.currency_list = new SelectList(_currencyService.GetCurrency2(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(88), "document_numbring_id", "category");
            ViewBag.cost_list = new SelectList(_costCenterService.GetAll(), "cost_center_id", "cost_center_code");
            ViewBag.taxlist = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.gl_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag._entityTypeService = new SelectList(_Generic.GetEntityList().Where(x => x.ENTITY_TYPE_ID != 7)
                              .ToList(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");
            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
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
            var ddd = fin_credit_debit_note.user_description[0];
            fin_credit_debit_note.credit_debit_id = 1;
            if (ModelState.IsValid)
            {
                issaved = _creditnoteservice.Add(fin_credit_debit_note);
                if(issaved.Contains("Saved"))
                {
                    TempData["doc_num"] = issaved.Split('~')[1];
                    return RedirectToAction("Index");
                }                
            }
            ViewBag.error = issaved;
            ViewBag.currency_list = new SelectList(_currencyService.GetCurrency2(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(88), "document_numbring_id", "category");       
            ViewBag._entityTypeService = new SelectList(_Generic.GetEntityList().Where(x => x.ENTITY_TYPE_ID != 7)
                              .ToList(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");         
            return View(fin_credit_debit_note);
        }
        [CustomAuthorizeAttribute("CN")]
        // GET: CreditNote/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fin_credit_debit_note_vm fin_credit_debit_note = _creditnoteservice.Get((int)id);
            if (fin_credit_debit_note == null)
            {
                return HttpNotFound();
            }
            ViewBag.currency_list = new SelectList(_currencyService.GetCurrency2(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(88), "document_numbring_id", "category");
            ViewBag._entityTypeService = new SelectList(_Generic.GetEntityList().Where(x => x.ENTITY_TYPE_ID != 7)
                              .ToList(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");
            return View(fin_credit_debit_note);
        }

        // POST: CreditNote/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(fin_credit_debit_note_vm fin_credit_debit_note)
        {
            var issaved = "";
            if (ModelState.IsValid)
            {
                issaved = _creditnoteservice.Add(fin_credit_debit_note);
                if (issaved.Contains("Saved"))
                {
                    TempData["doc_num"] = issaved.Split('~')[1];
                    return RedirectToAction("Index");
                }
            }
            ViewBag.currency_list = new SelectList(_currencyService.GetCurrency2(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(88), "document_numbring_id", "category");
            ViewBag.cost_list = new SelectList(_costCenterService.GetAll(), "cost_center_id", "cost_center_code");
            ViewBag.taxlist = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.gl_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag._entityTypeService = new SelectList(_Generic.GetEntityList().Where(x => x.ENTITY_TYPE_ID != 7)
                             .ToList(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");
            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            return View(fin_credit_debit_note);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _creditnoteservice.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
