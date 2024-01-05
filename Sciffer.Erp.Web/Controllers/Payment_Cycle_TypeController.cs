using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;

namespace Sciffer.Erp.Web.Controllers
{
    public class Payment_Cycle_TypeController : Controller
    {
        private readonly IPaymentCycleTypeService _countryService;

        public Payment_Cycle_TypeController(IPaymentCycleTypeService countryService)
        {
            _countryService = countryService;
        }

        // GET: Payment_Cycle_Type
        public ActionResult Index()
        {
            return View(_countryService.GetAll());
        }

        // GET: Payment_Cycle_Type/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_PAYMENT_CYCLE_TYPE = _countryService.Get((int)id);
            if (rEF_PAYMENT_CYCLE_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(rEF_PAYMENT_CYCLE_TYPE);
        }

        // GET: Payment_Cycle_Type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payment_Cycle_Type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(REF_PAYMENT_CYCLE_TYPE rEF_PAYMENT_CYCLE_TYPE)
        {
            if (ModelState.IsValid)
            {
                var issaved = _countryService.Add(rEF_PAYMENT_CYCLE_TYPE);
                if (issaved)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(rEF_PAYMENT_CYCLE_TYPE);
        }

        // GET: Payment_Cycle_Type/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_PAYMENT_CYCLE_TYPE = _countryService.Get((int)id);
            if (rEF_PAYMENT_CYCLE_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(rEF_PAYMENT_CYCLE_TYPE);
        }

        // POST: Payment_Cycle_Type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(REF_PAYMENT_CYCLE_TYPE rEF_PAYMENT_CYCLE_TYPE)
        {
            if (ModelState.IsValid)
            {
                var isedit = _countryService.Update(rEF_PAYMENT_CYCLE_TYPE);
                if(isedit)
                {
                    return RedirectToAction("Index");
                }                
            }
            return View(rEF_PAYMENT_CYCLE_TYPE);
        }

        // GET: Payment_Cycle_Type/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_PAYMENT_CYCLE_TYPE = _countryService.Get((int)id);
            if (rEF_PAYMENT_CYCLE_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(rEF_PAYMENT_CYCLE_TYPE);
        }

        // POST: Payment_Cycle_Type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var isdelete = _countryService.Delete(id);
            if (isdelete)
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
