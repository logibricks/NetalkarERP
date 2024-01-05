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
    public class FrequencyController : Controller
    {
        private readonly IFrequencyService _frequencyService;
        private readonly IGenericService _Generic;
        public FrequencyController(IFrequencyService FrequencyService, IGenericService gen)
        {
            _frequencyService = FrequencyService;
            _Generic = gen;
        }

        // GET: Frequency
        public ActionResult Index()
        {
            ViewBag.DataSource = _frequencyService.GetAll();
            return View();
        }
      
        // GET: Frequency/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_frequency ref_frequency = _frequencyService.Get(id);
            if (ref_frequency == null)
            {
                return HttpNotFound();
            }
            return View(ref_frequency);
        }

        // GET: Frequency/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Frequency/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_frequency ref_frequency)
        {
            if (ModelState.IsValid)
            {
                var isValid = _frequencyService.Add(ref_frequency);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(ref_frequency);
        }

        // GET: Frequency/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_frequency ref_frequency = _frequencyService.Get(id);
            if (ref_frequency == null)
            {
                return HttpNotFound();
            }
            return View(ref_frequency);
        }

        // POST: Frequency/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_frequency ref_frequency)
        {
            if (ModelState.IsValid)
            {
                var isValid = _frequencyService.Update(ref_frequency);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(ref_frequency);
        }

        // GET: Frequency/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_frequency ref_frequency = _frequencyService.Get(id);
            if (ref_frequency == null)
            {
                return HttpNotFound();
            }
            return View(ref_frequency);
        }

        // POST: Frequency/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var isValid = _frequencyService.Delete(id);
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
                _frequencyService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
