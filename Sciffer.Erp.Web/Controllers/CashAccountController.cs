using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Web.Controllers
{
    public class CashAccountController : Controller
    {
        private ScifferContext db = new ScifferContext();

        private readonly ICashAccountService _CashAccount;
        private readonly IGenericService _Generic;
        private readonly ICurrencyService _currency;
        public CashAccountController(IGenericService generic, ICurrencyService currency, ICashAccountService CashAccount)
        {
            _Generic = generic;
            _currency = currency;
            _CashAccount = CashAccount;
        }

        // GET: CashAccount
        public ActionResult Index()
        {
            ViewBag.DataSource = _CashAccount.getall();
            return View();
        }

        // GET: CashAccount/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_cash_account ref_cash_account = db.ref_cash_account.Find(id);
            if (ref_cash_account == null)
            {
                return HttpNotFound();
            }
            return View(ref_cash_account);
        }

        // GET: CashAccount/Create
        public ActionResult Create()
        {
            ViewBag.currencylist = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");            
            ViewBag.gllist = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            return View();
        }

        // POST: CashAccount/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_cash_account ref_cash_account)
        {
            if (ModelState.IsValid)
            {
                var ISSAVED = _CashAccount.Add(ref_cash_account);
                if (ISSAVED == "Saved")
                {
                    return RedirectToAction("Index");
                }       
            }

            ViewBag.currencylist = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.gllist = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");

            return View(ref_cash_account);
        }

        // GET: CashAccount/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_cash_account ref_cash_account = db.ref_cash_account.Find(id);
            if (ref_cash_account == null)
            {
                return HttpNotFound();
            }
          
            ViewBag.currencylist = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.gllist = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            return View(ref_cash_account);
        }
        
        // POST: CashAccount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_cash_account ref_cash_account)
        {
            if (ModelState.IsValid)
            {
                var ISSAVED = _CashAccount.Add(ref_cash_account);
                if (ISSAVED == "Saved")
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.currencylist = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.gllist = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            return View(ref_cash_account);
        }

        // GET: CashAccount/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_cash_account ref_cash_account = db.ref_cash_account.Find(id);
            if (ref_cash_account == null)
            {
                return HttpNotFound();
            }
            return View(ref_cash_account);
        }

        // POST: CashAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ref_cash_account ref_cash_account = db.ref_cash_account.Find(id);
            db.ref_cash_account.Remove(ref_cash_account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
