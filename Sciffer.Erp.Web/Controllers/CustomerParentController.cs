using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class CustomerParentController : Controller
    {
        private readonly ICustomerParentService _customerparent;
        private readonly ICountryService _countryService;
        private readonly IStateService _stateService;
        public CustomerParentController(ICustomerParentService customerparent, IStateService stateService,ICountryService countryService)
        {
            _countryService = countryService;
            _customerparent = customerparent;
            _stateService = stateService;
        }

        // GET: Customer_Parent
        [CustomAuthorizeAttribute("CSTRP")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _customerparent.GetAll();
            return View();
        }

        // GET: Customer_Parent/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_CUSTOMER_PARENT = _customerparent.Get((int)id);
            if (rEF_CUSTOMER_PARENT == null)
            {
                return HttpNotFound();
            }
            var statelist = _stateService.GetAll();
            ViewBag.Statelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            var CountryList = _countryService.GetAll();
            ViewBag.CountryList = new SelectList(CountryList, "COUNTRY_ID", "COUNTRY_NAME");
            return View(rEF_CUSTOMER_PARENT);
        }

        // GET: Customer_Parent/Create
        [CustomAuthorizeAttribute("CSTRP")]
        public ActionResult Create()
        {
            var statelist = _stateService.GetAll();
            ViewBag.Statelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            var CountryList = _countryService.GetAll();
            ViewBag.CountryList = new SelectList(CountryList, "COUNTRY_ID", "COUNTRY_NAME");
            return View();
        }

        // POST: Customer_Parent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(REF_CUSTOMER_PARENT rEF_CUSTOMER_PARENT)
        {
            if (ModelState.IsValid)
            {
                var issaved = _customerparent.Add(rEF_CUSTOMER_PARENT);
                if (issaved)
                {
                    return RedirectToAction("Index");
                }                
            }
            var statelist = _stateService.GetAll();            
            ViewBag.Statelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            var country = _countryService.GetAll();
            ViewBag.CountryList = new SelectList(country, "COUNTRY_ID", "COUNTRY_NAME");
            return View(rEF_CUSTOMER_PARENT);
        }

        // GET: Customer_Parent/Edit/5
        [CustomAuthorizeAttribute("CSTRP")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_CUSTOMER_PARENT = _customerparent.Get((int)id);
            if (rEF_CUSTOMER_PARENT == null)
            {
                return HttpNotFound();
            }
            var statelist = _stateService.GetAll();
            ViewBag.Statelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            var CountryList = _countryService.GetAll();
            ViewBag.CountryList = new SelectList(CountryList, "COUNTRY_ID", "COUNTRY_NAME");
            return View(rEF_CUSTOMER_PARENT);
        }

        // POST: Customer_Parent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerParentVM rEF_CUSTOMER_PARENT)
        {
            if (ModelState.IsValid)
            {
                var isedit = _customerparent.Update(rEF_CUSTOMER_PARENT);
                if (isedit)
                {
                    return RedirectToAction("Index");
                }                
            }
            var statelist = _stateService.GetAll();
            ViewBag.Statelist = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            var CountryList = _countryService.GetAll();
            ViewBag.CountryList = new SelectList(CountryList, "COUNTRY_ID", "COUNTRY_NAME");
            return View(rEF_CUSTOMER_PARENT);
        }

        // GET: Customer_Parent/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_CUSTOMER_PARENT = _customerparent.Get((int)id);
            if (rEF_CUSTOMER_PARENT == null)
            {
                return HttpNotFound();
            }
            return View(rEF_CUSTOMER_PARENT);
        }

        // POST: Customer_Parent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var isdelete = _customerparent.Delete(id);
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
                _customerparent.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
