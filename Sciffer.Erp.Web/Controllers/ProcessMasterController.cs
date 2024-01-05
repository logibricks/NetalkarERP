using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Implementation;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class ProcessMasterController : Controller
    {
        private readonly ProcessMasterService _processMaster;
        private readonly IGenericService _Generic;
        private readonly IQualityParameterService _QualityParameter;

        public ProcessMasterController(ProcessMasterService processMaster, IGenericService gen)
        {
            _processMaster = processMaster;
            _Generic = gen;
        }

        public ActionResult InlineDelete(int key)
        {
            var res = _processMaster.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorizeAttribute("PRS-MST")]
        // GET: Currency 
        public ActionResult Index()
        {
            //ViewBag.COUNTRY_ID = _countyService.GetAll();
            ViewBag.DataSource = _processMaster.GetAll();
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _processMaster.Dispose();
            }
            base.Dispose(disposing);
        }

        [CustomAuthorizeAttribute("PRS-MST")]
        // GET: QualityParameter/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QualityParameter/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_mfg_process_vm value)
        {
            ref_mfg_process_vm result = null;

            result = _processMaster.Add(value);
            if (result != null)
            {
                return RedirectToAction("Index");
            }

            return View(result);
        }

        [CustomAuthorizeAttribute("PRS-MST")]
        // GET: QualityParameter/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ref_mfg_process_vm ref_mfg_process_vm = _processMaster.Get(id);
            if (ref_mfg_process_vm == null)
            {
                return HttpNotFound();
            }
            return View(ref_mfg_process_vm);
        }

        // POST: QualityParameter/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_mfg_process_vm vm)
        {
            ref_mfg_process_vm result = null;

            result = _processMaster.Update(vm);
            if (result != null)
            {
                return RedirectToAction("Index");
            }

            return View(result);
        }
    }
}