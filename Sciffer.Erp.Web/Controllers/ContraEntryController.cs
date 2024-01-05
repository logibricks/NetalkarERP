using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Web.Controllers
{
    public class ContraEntryController : Controller
    {
        private readonly IDocumentNumbringService _documentNumbringService;
        private readonly IGenericService _Generic;
        private readonly ICurrencyService _currencyService;
        private readonly IBankService _bankService;
        private readonly IContraEntryService _contraEntryService;
        private IBankAccountService _bankAccountService;
        public ContraEntryController(IBankAccountService BankAccountService, IDocumentNumbringService DocumentNumbringService, IGenericService GenericService,
            ICurrencyService CurrencyService, IBankService BankService, IContraEntryService ContraEntryService)
        {
            _documentNumbringService = DocumentNumbringService;
            _Generic = GenericService;
            _currencyService = CurrencyService;
            _bankService = BankService;
            _contraEntryService = ContraEntryService;
            _bankAccountService = BankAccountService;
        }
        // GET: ContraEntry
        public ActionResult Index()
        {
            ViewBag.doc = TempData["doc_num"];
            ViewBag.DataSource = _contraEntryService.GetAll();
            return View();
        }


        // GET: ContraEntry/Create
        public ActionResult Create()
        {
            ViewBag.document_list = new SelectList(_Generic.GetCategoryList(92), "document_numbring_id", "category");
            ViewBag.currency_list = new SelectList(_currencyService.GetCurrency2(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.bank_list = new SelectList(_Generic.GetBankAccountByBank(0), "bank_account_id", "bank_account_code");
            ViewBag.cash_list = new SelectList(_Generic.GetCashAccount(), "cash_account_id", "cash_account_desc");
            return View();
        }

        // POST: ContraEntry/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(fin_contra_entry_vm contra_entry)
        {
            if (ModelState.IsValid)
            {
                var isvalid = _contraEntryService.Add(contra_entry);
                if (isvalid != "error")
                {
                    if (contra_entry.contra_entry_id == 0)
                    {
                        TempData["doc_num"] = isvalid + " Saved successfully!";
                    }
                    else
                    {
                        TempData["doc_num"] = isvalid + " Updated successfully!";
                    }
                    return RedirectToAction("Index");
                }
            }
            ViewBag.document_list = new SelectList(_Generic.GetCategoryList(92), "document_numbring_id", "category");
            ViewBag.currency_list = new SelectList(_currencyService.GetCurrency2(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.bank_list = new SelectList(_Generic.GetBankAccountByBank(0), "bank_account_id", "bank_account_code");
            ViewBag.cash_list = new SelectList(_Generic.GetCashAccount(), "cash_account_id", "cash_account_desc");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(92), "cancellation_reason_id", "cancellation_reason");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View(contra_entry);
        }

        // GET: ContraEntry/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fin_contra_entry_vm contra_entry = _contraEntryService.Get((int)id);
            if (contra_entry == null)
            {
                return HttpNotFound();
            }
            ViewBag.document_list = new SelectList(_Generic.GetCategoryList(92), "document_numbring_id", "category");
            ViewBag.currency_list = new SelectList(_currencyService.GetCurrency2(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.bank_list = new SelectList(_Generic.GetBankAccountByBank(0), "bank_account_id", "bank_account_code");
            ViewBag.cash_list = new SelectList(_Generic.GetCashAccount(), "cash_account_id", "cash_account_desc");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(92), "cancellation_reason_id", "cancellation_reason");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View(contra_entry);
        }

        // POST: ContraEntry/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(fin_contra_entry_vm contra_entry)
        {
            if (ModelState.IsValid)
            {
                var isvalid = _contraEntryService.Add(contra_entry);
                if (isvalid != "error")
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.document_list = new SelectList(_Generic.GetCategoryList(92), "document_numbring_id", "category");
            ViewBag.currency_list = new SelectList(_currencyService.GetCurrency2(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.bank_list = new SelectList(_Generic.GetBankAccountByBank(0), "bank_account_id", "bank_account_code");
            ViewBag.cash_list = new SelectList(_Generic.GetCashAccount(), "cash_account_id", "cash_account_desc");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(92), "cancellation_reason_id", "cancellation_reason");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View(contra_entry);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //_contraEntryService.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult DeleteConfirmed(int id, string cancellation_remarks, int cancellation_reason_id)
        {
            var isValid = _contraEntryService.Delete(id, cancellation_remarks, cancellation_reason_id);
            if (isValid.Contains("Cancelled"))
            {
                return Json(isValid);
            }
            return Json(isValid);
        }
    }
}
