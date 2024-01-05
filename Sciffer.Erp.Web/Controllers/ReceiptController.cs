using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class ReceiptController : Controller
    {
       // private ScifferContext db = new ScifferContext();
        private readonly IGenericService _Generic;
        private readonly IEntityTypeService _entitytype;
        private readonly IReceiptService _Receipt;
        public ReceiptController(IGenericService generic, IEntityTypeService entitytype, IReceiptService receipt)
        {
            _Generic = generic;
            _entitytype = entitytype;
            _Receipt = receipt;
        }
        // GET: Receipt
        [CustomAuthorizeAttribute("REC")]
        public ActionResult Index()
        {
            ViewBag.num = TempData["data"];
            ViewBag.DataSource = _Receipt.GetAll(1);//1 for receipt
            return View();
        }

        // GET: Receipt/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fin_ledger_payment fin_receipt = null; 
            if (fin_receipt == null)
            {
                return HttpNotFound();
            }
            return View(fin_receipt);
        }

        // GET: Receipt/Create
        [CustomAuthorizeAttribute("REC")]
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.entity_type_list = new SelectList(_Generic.GetEntityList(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");
            ViewBag.BankAccountList = new SelectList(_Generic.GetBankAccountByBank(0), "bank_account_id", "bank_account_code");
            ViewBag.paymenttypelist = new SelectList(_Generic.GetPaymentTypeList(), "PAYMENT_TYPE_ID", "PAYMENT_TYPE_NAME");
            ViewBag.CurrencyList = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(91), "document_numbring_id", "category");
            return View();
        }

        // POST: Receipt/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(fin_ledger_paymentVM fin_receipt)
        {         
            fin_receipt.in_out = 1;
            if (ModelState.IsValid)
            {
               var issaved= _Receipt.Add(fin_receipt);
                if(issaved!="Error")
                {
                    TempData["data"] = issaved + " Saved Successfully.";
                    return RedirectToAction("Index");
                } 
            }
            ViewBag.error = "Something went wrong !";
            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.entity_type_list = new SelectList(_Generic.GetEntityList(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");
            ViewBag.BankAccountList = new SelectList(_Generic.GetBankAccountByBank(0), "bank_account_id", "bank_account_code");
            ViewBag.paymenttypelist = new SelectList(_Generic.GetPaymentTypeList(), "PAYMENT_TYPE_ID", "PAYMENT_TYPE_NAME");
            ViewBag.CurrencyList = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(91), "document_numbring_id", "category");
            return View(fin_receipt);
        }

        // GET: Receipt/Edit/5
        [CustomAuthorizeAttribute("REC")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fin_receipt = _Receipt.Get((int)id,1);
            if (fin_receipt == null)
            {
                return HttpNotFound();
            }
            ViewBag.error = "";
            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.entity_type_list = new SelectList(_Generic.GetEntityList(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");
            ViewBag.BankAccountList = new SelectList(_Generic.GetBankAccountByBank(0), "bank_account_id", "bank_account_code");
            ViewBag.paymenttypelist = new SelectList(_Generic.GetPaymentTypeList(), "PAYMENT_TYPE_ID", "PAYMENT_TYPE_NAME");
            ViewBag.CurrencyList = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(91), "document_numbring_id", "category");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(91), "cancellation_reason_id", "cancellation_reason");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View(fin_receipt);
        }

        // POST: Receipt/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(fin_ledger_paymentVM fin_receipt)
        {
            fin_receipt.in_out = 1;
            if (ModelState.IsValid)
            {
                var issaved = _Receipt.Add(fin_receipt);
                if (issaved != "Error")
                {
                    TempData["data"] = issaved + " Updated Successfully.";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = "Something went wrong !";
            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.entity_type_list = new SelectList(_Generic.GetEntityList(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");
            ViewBag.BankAccountList = new SelectList(_Generic.GetBankAccountByBank(0), "bank_account_id", "bank_account_code");
            ViewBag.paymenttypelist = new SelectList(_Generic.GetPaymentTypeList(), "PAYMENT_TYPE_ID", "PAYMENT_TYPE_NAME");
            ViewBag.CurrencyList = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(91), "document_numbring_id", "category");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(91), "cancellation_reason_id", "cancellation_reason");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View(fin_receipt);            
        }

        // GET: Receipt/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fin_ledger_payment fin_receipt = null;
            if (fin_receipt == null)
            {
                return HttpNotFound();
            }
            return View(fin_receipt);
        }

        public ActionResult DeleteConfirmed(int id, string cancellation_remarks, int cancellation_reason_id)
        {
            var isValid = _Receipt.Delete(id, cancellation_remarks, cancellation_reason_id);
            if (isValid.Contains("Cancelled"))
            {
                return Json(isValid);
            }
            return Json(isValid);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _Receipt.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
