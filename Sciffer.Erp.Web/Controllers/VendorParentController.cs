using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class VendorParentController : Controller
    {
        private readonly IVendorParentService _vendorService;
        private readonly IStateService _stateService;
        private readonly ICountryService _countryService;
        public VendorParentController(IVendorParentService vendorService, IStateService stateService,ICountryService countryService)
        {
            _vendorService = vendorService;
            _countryService = countryService;
            _stateService = stateService;
        }

        // GET: Vendor_Parent
        [CustomAuthorizeAttribute("VNDRP")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _vendorService.GetAll();
            return View();
        }

        // GET: Vendor_Parent/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_VENDOR_PARENT = _vendorService.Get((int)id);
            if (rEF_VENDOR_PARENT == null)
            {
                return HttpNotFound();
            }
            var state = _stateService.GetAll();
            ViewBag.statelist = new SelectList(state, "STATE_ID", "STATE_NAME", rEF_VENDOR_PARENT.REGD_OFFICE_STATE_ID);
            var country = _countryService.GetAll();
            ViewBag.CountryList = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            return View(rEF_VENDOR_PARENT);
        }

        // GET: Vendor_Parent/Create
        [CustomAuthorizeAttribute("VNDRP")]
        public ActionResult Create()
        {
            var state = _stateService.GetAll();
            ViewBag.statelist = new SelectList(state, "STATE_ID", "STATE_NAME");
            var country = _countryService.GetAll();
            ViewBag.CountryList = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            return View();
        }

        // POST: Vendor_Parent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(REF_VENDOR_PARENT rEF_VENDOR_PARENT)
        {
            if (ModelState.IsValid)
            {
                var isaved = _vendorService.Add(rEF_VENDOR_PARENT);
                if (isaved)
                {
                    return RedirectToAction("Index");
                }
                return View();
            }

            var state = _stateService.GetAll();
            ViewBag.statelist = new SelectList(state, "STATE_ID", "STATE_NAME",rEF_VENDOR_PARENT.REGD_OFFICE_STATE_ID);
            var country = _countryService.GetAll();
            ViewBag.CountryList = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            return View(rEF_VENDOR_PARENT);
        }

        // GET: Vendor_Parent/Edit/5
        [CustomAuthorizeAttribute("VNDRP")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_VENDOR_PARENT =_vendorService.Get((int)id);
            if (rEF_VENDOR_PARENT == null)
            {
                return HttpNotFound();
            }
            var state = _stateService.GetAll();
            ViewBag.statelist = new SelectList(state, "STATE_ID", "STATE_NAME", rEF_VENDOR_PARENT.REGD_OFFICE_STATE_ID);
            var country = _countryService.GetAll();
            ViewBag.CountryList = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            return View(rEF_VENDOR_PARENT);
        }

        // POST: Vendor_Parent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VendorParentVM rEF_VENDOR_PARENT)
        {
            if (ModelState.IsValid)
            {
                var isedit = _vendorService.Update(rEF_VENDOR_PARENT);
                if (isedit)
                {
                    return RedirectToAction("Index");
                }               
            }
            var state = _stateService.GetAll();
            ViewBag.statelist = new SelectList(state, "STATE_ID", "STATE_NAME", rEF_VENDOR_PARENT.REGD_OFFICE_STATE_ID);
            var country = _countryService.GetAll();
            ViewBag.CountryList = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            return View(rEF_VENDOR_PARENT);
        }

        // GET: Vendor_Parent/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var  rEF_VENDOR_PARENT = _vendorService.Get((int)id);
            if (rEF_VENDOR_PARENT == null)
            {
                return HttpNotFound();
            }
            return View(rEF_VENDOR_PARENT);
        }

        // POST: Vendor_Parent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var isdelete = _vendorService.Delete(id);
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
                _vendorService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
