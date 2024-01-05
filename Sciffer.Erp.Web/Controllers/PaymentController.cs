using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Web.CustomFilters;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using Syncfusion.JavaScript;
using Sciffer.MovieScheduling.Web.Service;
using System.Collections;

namespace Sciffer.Erp.Web.Controllers
{
    public class PaymentController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        private readonly IGenericService _Generic;
        private readonly IEntityTypeService _entitytype;
        private readonly IReceiptService _Receipt;
        public PaymentController(IGenericService generic, IEntityTypeService entitytype, IReceiptService receipt)
        {
            _Generic = generic;
            _entitytype = entitytype;
            _Receipt = receipt;
        }
        // GET: Receipt
        [CustomAuthorizeAttribute("PAY")]
        public ActionResult Index()
        {
            ViewBag.num = TempData["data"];
           // ViewBag.DataSource = _Receipt.GetAll(2);//2 for payment
            return View();
        }
        public JsonResult GetLang(DataManager dm)
        {
            var res = _Receipt.GetAll(2);

            int count = res.Count;
            ServerSideSearch sss = new ServerSideSearch();
            IEnumerable data = sss.ProcessDM(dm, res);
            return Json(new { result = data, count = count });
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
        [CustomAuthorizeAttribute("PAY")]
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.currency_list = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.BankAccountList = new SelectList(_Generic.GetBankAccountByBank(0), "bank_account_id", "bank_account_code");
            ViewBag.paymenttypelist = new SelectList(_Generic.GetPaymentTypeList(), "PAYMENT_TYPE_ID", "PAYMENT_TYPE_NAME");
            ViewBag.CurrencyList = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(90), "document_numbring_id", "category");
            ViewBag.entity_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            return View();
            return View();
        }

        // POST: Receipt/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(fin_ledger_paymentVM fin_receipt)
        {
            fin_receipt.in_out = 2;
            if (ModelState.IsValid)
            {
                var issaved = _Receipt.Add(fin_receipt);
                if (issaved != "Error")
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
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(90), "document_numbring_id", "category");
            return View(fin_receipt);
        }

        // GET: Receipt/Edit/5
        [CustomAuthorizeAttribute("PAY")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fin_receipt = _Receipt.Get((int)id,2);
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
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(90), "document_numbring_id", "category");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(90), "cancellation_reason_id", "cancellation_reason");
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
            fin_receipt.in_out = 2;
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
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(90), "document_numbring_id", "category");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(90), "cancellation_reason_id", "cancellation_reason");
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

        // POST: Receipt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // fin_receipt fin_receipt = db.fin_receipt.Find(id);
            //db.fin_receipt.Remove(fin_receipt);
            // db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _Receipt.Dispose();
            }
            base.Dispose(disposing);
        }
        public CrystalReportPdfResult Pdf(int id)
        {
            ReportDocument rd = new ReportDocument();
            try
            {
                var pr = _Receipt.GetPaymentReceiptHeader(id);
                var prd = _Receipt.GetPaymentReceiptDetail(id);
                DataSet ds = new DataSet("Payment_Receipt");
                var po = new List<fin_payment_receipt_vm>();
                po.Add(pr);
                var dt1 = _Generic.ToDataTable(po);
                var dt2 = _Generic.ToDataTable(prd);
                dt1.TableName = "fin_ledger_payment";
                dt2.TableName = "fin_ledger_payment_detail";
                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);
                rd.Load(Path.Combine(Server.MapPath("~/Reports/PaymentReport.rpt")));
                rd.SetDataSource(ds);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string reportPath = "";
                reportPath = Path.Combine(Server.MapPath("~/Reports"), "PaymentReport.rpt");
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
                rd.Clone();
                rd.Dispose();
                rd = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

        }
    }
}
