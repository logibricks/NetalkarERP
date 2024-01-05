using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;

namespace Sciffer.Erp.Web.Controllers
{
    public class SourceController : Controller
    {
        private readonly ISourceService _sourceService;

        public SourceController(ISourceService sourceService)
        {
            _sourceService = sourceService;
        }
        // GET: Source
        public ActionResult Index()
        {
            return View(_sourceService.GetAll());
        }

        // GET: Source/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_SOURCE rEF_SOURCE = _sourceService.Get((int)id);
            if (rEF_SOURCE == null)
            {
                return HttpNotFound();
            }
            return View(rEF_SOURCE);
        }

        // GET: Source/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Source/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(REF_SOURCE rEF_SOURCE)
        {
            if (ModelState.IsValid)
            {
                var isAdded = _sourceService.Add(rEF_SOURCE);
                if (isAdded)
                    return RedirectToAction("Index");
            }

            return View(rEF_SOURCE);
        }

        // GET: Source/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_SOURCE rEF_SOURCE = _sourceService.Get((int)id);
            if (rEF_SOURCE == null)
            {
                return HttpNotFound();
            }
            return View(rEF_SOURCE);
        }

        // POST: Source/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(REF_SOURCE rEF_SOURCE)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _sourceService.Update(rEF_SOURCE);
                if (isUpdated)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(rEF_SOURCE);
        }

        // GET: Source/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_SOURCE rEF_SOURCE = _sourceService.Get((int)id);
            if (rEF_SOURCE == null)
            {
                return HttpNotFound();
            }
            return View(rEF_SOURCE);
        }

        // POST: Source/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var isDeleted = _sourceService.Delete(id);
            if (isDeleted)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _sourceService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
