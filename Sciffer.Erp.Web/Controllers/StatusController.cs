using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using System;
using Syncfusion.JavaScript.Models;
using Syncfusion.EJ.Export;
using System.Web.Script.Serialization;
using System.Collections;
using System.Collections.Generic;
using Syncfusion.XlsIO;
using System.Reflection;
using Sciffer.Erp.Domain.Model;

namespace Sciffer.Erp.Web.Controllers
{
    public class StatusController : Controller
    {
        private readonly IStatusService _statusService;
        private readonly IGenericService _Generic;
        public StatusController(IStatusService StatusService, IGenericService gen)
        {
            _statusService = StatusService;
            _Generic = gen;
        }

        // GET: Status
        public ActionResult Index()
        {
            ViewBag.DataSource = _statusService.GetAll();
            return View(_statusService.GetAll());
        }
      

        // GET: Status/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_status rEF_STATUS = _statusService.Get((int)id);
            if (rEF_STATUS == null)
            {
                return HttpNotFound();
            }
            return View(rEF_STATUS);
        }

        // GET: Status/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Status/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_status rEF_STATUS)
        {
            if (ModelState.IsValid)
            {
                var isValid = _statusService.Add(rEF_STATUS);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(rEF_STATUS);
        }

        // GET: Status/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_status rEF_STATUS = _statusService.Get((int)id);
            if (rEF_STATUS == null)
            {
                return HttpNotFound();
            }
            return View(rEF_STATUS);
        }

        // POST: Status/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_status rEF_STATUS)
        {
            if (ModelState.IsValid)
            {
                var isValid = _statusService.Update(rEF_STATUS);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(rEF_STATUS);
        }

        // GET: Status/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_status rEF_STATUS = _statusService.Get((int)id);
            if (rEF_STATUS == null)
            {
                return HttpNotFound();
            }
            return View(rEF_STATUS);
        }

        // POST: Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var isValid = _statusService.Delete(id);
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
                _statusService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
