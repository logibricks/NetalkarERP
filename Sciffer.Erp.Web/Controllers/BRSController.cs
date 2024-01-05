using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using System;

namespace Sciffer.Erp.Web.Controllers
{
    public class BRSController : Controller
    {
       
        private readonly IGenericService _Generic;
        private readonly ICurrencyService _currencyService;
        private readonly IFinBankRecoService _BankReco;
        // GET: BRS
        public BRSController(IGenericService generic, ICurrencyService currency, IFinBankRecoService bankreco)
        {
            _Generic = generic;
            _currencyService = currency;
            _BankReco = bankreco;
        }
        public ActionResult Index()
        {
            ViewBag.doc = TempData["doc_no"];
            ViewBag.datasource = _BankReco.GetAll();
            return View();
        }

        // GET: BRS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fin_bank_reco_vm fin_bank_reco_vm =_BankReco.Get((int)id);
            if (fin_bank_reco_vm == null)
            {
                return HttpNotFound();
            }
            return View(fin_bank_reco_vm);
        }

        // GET: BRS/Create
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.currency_list = new SelectList(_currencyService.GetCurrency2(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.document_list = new SelectList(_Generic.GetCategoryList(93), "document_numbring_id", "category");
            ViewBag.bank_list = new SelectList(_Generic.GetBankAccountByBank(0), "bank_account_id", "bank_account_code");
            return View();
        }

        // POST: BRS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(fin_bank_reco_vm fin_bank_reco_vm)
        {
            var issaved = "";
            if (ModelState.IsValid)
            {
                issaved = _BankReco.Add(fin_bank_reco_vm);
                if(issaved.Contains("Saved"))
                {
                    TempData["doc_no"] = issaved.Split('~')[1];
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = issaved;
            ViewBag.currency_list = new SelectList(_currencyService.GetCurrency2(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.document_list = new SelectList(_Generic.GetCategoryList(93), "document_numbring_id", "category");
            ViewBag.bank_list = new SelectList(_Generic.GetBankAccountByBank(0), "bank_account_id", "bank_account_code");
            return View(fin_bank_reco_vm);
        }

        // GET: BRS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fin_bank_reco_vm fin_bank_reco_vm = _BankReco.Get((int)id);
            if (fin_bank_reco_vm == null)
            {
                return HttpNotFound();
            }
            ViewBag.currency_list = new SelectList(_currencyService.GetCurrency2(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.document_list = new SelectList(_Generic.GetCategoryList(93), "document_numbring_id", "category");
            ViewBag.bank_list = new SelectList(_Generic.GetBankAccountByBank(0), "bank_account_id", "bank_account_code");
            return View(fin_bank_reco_vm);
        }

        // POST: BRS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(fin_bank_reco_vm fin_bank_reco_vm)
        {
            if (ModelState.IsValid)
            {               
                return RedirectToAction("Index");
            }
            ViewBag.currency_list = new SelectList(_currencyService.GetCurrency2(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.document_list = new SelectList(_Generic.GetCategoryList(93), "document_numbring_id", "category");
            ViewBag.bank_list = new SelectList(_Generic.GetBankAccountByBank(0), "bank_account_id", "bank_account_code");
            return View(fin_bank_reco_vm);
        }

        // GET: BRS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fin_bank_reco fin_bank_reco_vm = null;
            if (fin_bank_reco_vm == null)
            {
                return HttpNotFound();
            }
            return View(fin_bank_reco_vm);
        }

        // POST: BRS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
           
            return RedirectToAction("Index");
        }
        public ActionResult GetPaymentReceiptForBRS(int id,DateTime start_date,DateTime end_date)
        {
            var reco = _BankReco.GetPaymentReceiptForBRS(id, start_date,end_date);
            return Json(reco, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _BankReco.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
