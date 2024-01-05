using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;

namespace Sciffer.Erp.Web.Controllers
{
    public class TDS_SectionController : Controller
    {
        private readonly ITDSSectionService _countryService;

        public TDS_SectionController(ITDSSectionService countryService)
        {
            _countryService = countryService;
        }

        // GET: TDS_Section
        public ActionResult Index()
        {
            return View(_countryService.GetAll());
        }

        // GET: TDS_Section/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_TDS_SECTION = _countryService.Get((int)id);
            if (rEF_TDS_SECTION == null)
            {
                return HttpNotFound();
            }
            return View(rEF_TDS_SECTION);
        }

        // GET: TDS_Section/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TDS_Section/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(REF_TDS_SECTION rEF_TDS_SECTION)
        {
              if (ModelState.IsValid)
                {
                    var issaved = _countryService.Add(rEF_TDS_SECTION);
                    if (issaved)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View(rEF_TDS_SECTION);   
        }

        // GET: TDS_Section/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_TDS_SECTION = _countryService.Get((int)id);
            if (rEF_TDS_SECTION == null)
            {
                return HttpNotFound();
            }
            return View(rEF_TDS_SECTION);
        }

        // POST: TDS_Section/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(REF_TDS_SECTION rEF_TDS_SECTION)
        {
            if (ModelState.IsValid)
            {
                var isedit = _countryService.Update(rEF_TDS_SECTION);
                if (isedit)
                {
                    return RedirectToAction("Index");
                }
            }
           
            return View(rEF_TDS_SECTION);
        }

        // GET: TDS_Section/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_TDS_SECTION = _countryService.Get((int)id);
            if (rEF_TDS_SECTION == null)
            {
                return HttpNotFound();
            }
            return View(rEF_TDS_SECTION);
        }

        // POST: TDS_Section/Delete/5
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
