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
    public class GenderController : Controller
    {
        private readonly IGenderService _genderService;

        public GenderController(IGenderService GenderService)
        {
            _genderService = GenderService;
        }

        // GET: Payment_Terms
        public ActionResult Index()
        {
            return View(_genderService.GetAll());
        }



        // GET: Payment_Terms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_GENDER = _genderService.Get((int)id);
            if (rEF_GENDER == null)
            {
                return HttpNotFound();
            }
            return View(rEF_GENDER);
        }

        // GET: Payment_Terms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payment_Terms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(REF_GENDER rEF_GENDER)
        {
            if (ModelState.IsValid)
            {
                var issaved = _genderService.Add(rEF_GENDER);
                if (issaved)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(rEF_GENDER);
        }

        // GET: Payment_Terms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_GENDER = _genderService.Get((int)id);
            if (rEF_GENDER == null)
            {
                return HttpNotFound();
            }
            return View(rEF_GENDER);
        }

        // POST: Payment_Terms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(REF_GENDER rEF_GENDER)
        {
            if (ModelState.IsValid)
            {
                var isedit = _genderService.Update(rEF_GENDER);
                if (isedit)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(rEF_GENDER);
        }

        // GET: Payment_Terms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_GENDER = _genderService.Get((int)id);
            if (rEF_GENDER == null)
            {
                return HttpNotFound();
            }
            return View(rEF_GENDER);
        }

        // POST: Payment_Terms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var isdelete = _genderService.Delete(id);
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
                _genderService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
