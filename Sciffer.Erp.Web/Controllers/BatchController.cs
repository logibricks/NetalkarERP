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
    public class BatchController : Controller
    {
        private readonly IBatchService _batchService;

        public BatchController(IBatchService BatchService)
        {
            _batchService = BatchService;
        }

        // GET: PaymentType
        public ActionResult Index()
        {
            return View(_batchService.GetAll());
        }

        // GET: PaymentType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MFG_BATCH mFG_BATCH = null;
            if (mFG_BATCH == null)
            {
                return HttpNotFound();
            }
            return View(mFG_BATCH);
        }

        // GET: PaymentType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(MFG_BATCH mFG_BATCH)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var issaved = _batchService.Add(mFG_BATCH);
        //        if (issaved == true)
        //            return RedirectToAction("Index");
        //    }

        //    return View(mFG_BATCH);
        //}

        //// GET: PaymentType/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MFG_BATCH mFG_BATCH = _batchService.Get((int)id);
        //    if (mFG_BATCH == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(mFG_BATCH);
        //}

        // POST: PaymentType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit(MFG_BATCH mFG_BATCH)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var isedited = _batchService.Update(mFG_BATCH);
        //        if (isedited == true)
        //            return RedirectToAction("Index");
        //    }
        //    return View(mFG_BATCH);
        //}

        // GET: PaymentType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MFG_BATCH mFG_BATCH = null;
            if (mFG_BATCH == null)
            {
                return HttpNotFound();
            }
            return View(mFG_BATCH);
        }

        // POST: PaymentType/Delete/5
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
                _batchService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
