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
    public class MaritalStatusController : Controller
    {
        private readonly IMaritalStatusService _maritalStatus_context;
        public MaritalStatusController(IMaritalStatusService MaritalStatusService)
        {
            _maritalStatus_context = MaritalStatusService;
        }

        // GET: MaritalStatus
        public ActionResult Index()
        {
            return View(_maritalStatus_context.GetAll());
        }

        // GET: MaritalStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_MARITAL_STATUS rEF_MARITAL_STATUS = _maritalStatus_context.Get(id);
            if (rEF_MARITAL_STATUS == null)
            {
                return HttpNotFound();
            }
            return View(rEF_MARITAL_STATUS);
        }

        // GET: MaritalStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MaritalStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(REF_MARITAL_STATUS rEF_MARITAL_STATUS)
        {
            if (ModelState.IsValid)
            {
             var isValid=   _maritalStatus_context.Add(rEF_MARITAL_STATUS);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(rEF_MARITAL_STATUS);
        }

        // GET: MaritalStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_MARITAL_STATUS rEF_MARITAL_STATUS = _maritalStatus_context.Get(id);
            if (rEF_MARITAL_STATUS == null)
            {
                return HttpNotFound();
            }
            return View(rEF_MARITAL_STATUS);
        }

        // POST: MaritalStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MARITAL_STATUS_ID,MARITAL_STATUS_NAME")] REF_MARITAL_STATUS rEF_MARITAL_STATUS)
        {
            if (ModelState.IsValid)
            {
              var isValid=   _maritalStatus_context.Update(rEF_MARITAL_STATUS);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(rEF_MARITAL_STATUS);
        }

        // GET: MaritalStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_MARITAL_STATUS rEF_MARITAL_STATUS = _maritalStatus_context.Get(id);
            if (rEF_MARITAL_STATUS == null)
            {
                return HttpNotFound();
            }
            return View(rEF_MARITAL_STATUS);
        }

        // POST: MaritalStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
        var isValid=    _maritalStatus_context.Delete(id);
            if (isValid == true)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _maritalStatus_context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
