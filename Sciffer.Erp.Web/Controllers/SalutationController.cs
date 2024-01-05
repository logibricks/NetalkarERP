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

namespace Sciffer.Erp.Web.Controllers
{
    public class SalutationController : Controller
    {
        private readonly ISalutationService _salutationService;

        public SalutationController(ISalutationService SalutationService)
        {
            _salutationService = SalutationService;
        }

        public ActionResult Index()
        {
            return View(_salutationService.GetAll());
        }

        // GET: Salutation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_SALUTATION rEF_SALUTATION = null;
            if (rEF_SALUTATION == null)
            {
                return HttpNotFound();
            }
            return View(rEF_SALUTATION);
        }

        // GET: Salutation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Salutation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(REF_SALUTATION rEF_SALUTATION)
        {
            if (ModelState.IsValid)
            {
                var issaved = _salutationService.Add(rEF_SALUTATION);
                if (issaved == true)
                    return RedirectToAction("Index");
            }

            return View(rEF_SALUTATION);
        }

        // GET: Salutation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_SALUTATION rEF_SALUTATION = _salutationService.Get((int)id);
            if (rEF_SALUTATION == null)
            {
                return HttpNotFound();
            }
            return View(rEF_SALUTATION);
        }

        // POST: Salutation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(REF_SALUTATION rEF_SALUTATION)
        {
            if (ModelState.IsValid)
            {
                var isedited = _salutationService.Update(rEF_SALUTATION);
                if (isedited == true)
                    return RedirectToAction("Index");
            }
            return View(rEF_SALUTATION);
        }

        // GET: Salutation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_SALUTATION rEF_SALUTATION = null;
            if (rEF_SALUTATION == null)
            {
                return HttpNotFound();
            }
            return View(rEF_SALUTATION);
        }


        // POST: Salutation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _salutationService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
