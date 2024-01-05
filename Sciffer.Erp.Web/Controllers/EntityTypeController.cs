using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;

namespace Sciffer.Erp.Web.Controllers
{
    public class Entity_TypeController : Controller
    {
        private readonly IEntityTypeService _EntityService;

        public Entity_TypeController(IEntityTypeService EntityService)
        {
            _EntityService = EntityService;
        }

        // GET: Entity_Type
        public ActionResult Index()
        {
            return View(_EntityService.GetAll());
        }

        // GET: Entity_Type/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_ENTITY_TYPE = _EntityService.Get((int)id);
            if (rEF_ENTITY_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(rEF_ENTITY_TYPE);
        }

        // GET: Entity_Type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Entity_Type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(REF_ENTITY_TYPE rEF_ENTITY_TYPE)
        {
            if (ModelState.IsValid)
            {
                var isvalid = _EntityService.Add(rEF_ENTITY_TYPE);
                if (isvalid)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(rEF_ENTITY_TYPE);
        }

        // GET: Entity_Type/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_ENTITY_TYPE = _EntityService.Get((int)id);
            if (rEF_ENTITY_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(rEF_ENTITY_TYPE);
        }

        // POST: Entity_Type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(REF_ENTITY_TYPE rEF_ENTITY_TYPE)
        {
            if (ModelState.IsValid)
            {
                var isvalid = _EntityService.Update(rEF_ENTITY_TYPE);
                if (isvalid)
                {
                    return RedirectToAction("Index");
                }
                
            }
            return View(rEF_ENTITY_TYPE);
        }

        // GET: Entity_Type/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_ENTITY_TYPE = _EntityService.Get((int)id);
            if (rEF_ENTITY_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(rEF_ENTITY_TYPE);
        }

        // POST: Entity_Type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var rEF_ENTITY_TYPE = _EntityService.Delete(id);
           if (rEF_ENTITY_TYPE)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _EntityService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
