using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;

namespace Sciffer.Erp.Web.Controllers
{
    public class Payment_CycleController : Controller
    {
        private readonly IPaymentCycleService _countryService;
        private readonly IPaymentCycleTypeService _paymentTypeSerivice;

        public Payment_CycleController(IPaymentCycleService countryService, IPaymentCycleTypeService paymentTypeSerivice)
        {
            _countryService = countryService;
            _paymentTypeSerivice = paymentTypeSerivice;
        }

        // GET: Payment_Cycle
        public ActionResult Index()
        {
            return View(_countryService.GetAll());
        }

        // GET: Payment_Cycle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_PAYMENT_CYCLE = _countryService.Get((int)id);
            if (rEF_PAYMENT_CYCLE == null)
            {
                return HttpNotFound();
            }
            return View(rEF_PAYMENT_CYCLE);
        }

        // GET: Payment_Cycle/Create
        public ActionResult Create()
        {
            var paymentlist = _paymentTypeSerivice.GetAll();
            ViewBag.Paymentlist = new SelectList(paymentlist, "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            return View();
        }

        // POST: Payment_Cycle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(REF_PAYMENT_CYCLE rEF_PAYMENT_CYCLE)
        {
            if (ModelState.IsValid)
            {
                var issaved = _countryService.Add(rEF_PAYMENT_CYCLE);
                if (issaved)
                {
                    return RedirectToAction("Index");
                }               
            }
            //ViewBag.PAYMENT_CYCLE_TYPE_ID = new SelectList(db.REF_PAYMENT_CYCLE_TYPE, "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME", rEF_PAYMENT_CYCLE.PAYMENT_CYCLE_TYPE_ID);
            return View(rEF_PAYMENT_CYCLE);
        }

        // GET: Payment_Cycle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_PAYMENT_CYCLE = _countryService.Get((int)id);
            var paymentlist = _paymentTypeSerivice.GetAll();
            ViewBag.Paymentlist = new SelectList(paymentlist, "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            return View(rEF_PAYMENT_CYCLE);
        }

        // POST: Payment_Cycle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(REF_PAYMENT_CYCLE rEF_PAYMENT_CYCLE)
        {
            if (ModelState.IsValid)
            {
                var isedit = _countryService.Update(rEF_PAYMENT_CYCLE);
                if (isedit)
                {
                    return RedirectToAction("Index");
                }               
            }
            //ViewBag.PAYMENT_CYCLE_TYPE_ID = new SelectList(db.REF_PAYMENT_CYCLE_TYPE, "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME", rEF_PAYMENT_CYCLE.PAYMENT_CYCLE_TYPE_ID);
            return View(rEF_PAYMENT_CYCLE);
        }

        // GET: Payment_Cycle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_PAYMENT_CYCLE = _countryService.Get((int)id);
            if (rEF_PAYMENT_CYCLE == null)
            {
                return HttpNotFound();
            }
            return View(rEF_PAYMENT_CYCLE);
        }

        // POST: Payment_Cycle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var isdelete = _countryService.Delete(id);
            if(isdelete)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _countryService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
