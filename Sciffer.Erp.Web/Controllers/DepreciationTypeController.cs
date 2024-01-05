using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System.Linq;
using Newtonsoft.Json;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Data;
using CrystalDecisions.Shared;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class DepreciationTypeController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly IDepreciationTypeService _depreciationtypeService;
        private readonly IFrequencyService _frequency;
        public DepreciationTypeController( IGenericService gen, IDepreciationTypeService DepreciationTypeService , IFrequencyService frequency)
        {
            _Generic = gen;
            _depreciationtypeService = DepreciationTypeService;
            _frequency = frequency;
        }
        // GET: DepreciationType
        public ActionResult Index()
        {
            ViewBag.num = TempData["doc_num"];
            ViewBag.DataSource = _depreciationtypeService.getall();
            return View();
        }
        public ActionResult Create()
        {
            ViewBag.frequencylist = new SelectList(_frequency.GetAll().Where(a => a.frequency_name != "Quarterly"), "frequency_id", "frequency_name");
            ViewBag.depareaList = new SelectList(_Generic.GetDepList(), "dep_area_id", "dep_area_code");
            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var sonvm = _depreciationtypeService.Get((int)id);
            ViewBag.depareaList = new SelectList(_Generic.GetDepList(), "dep_area_id", "dep_area_code");
            ViewBag.frequencylist = new SelectList(_frequency.GetAll().Where(a => a.frequency_name != "Quarterly"), "frequency_id", "frequency_name");
            return View(sonvm);
        }
        [HttpPost]
        public ActionResult Create(ref_dep_type_vm add_depre)
        {
            if (ModelState.IsValid)
            {
                var isValid = _depreciationtypeService.Add(add_depre);
                if (isValid != "error")
                {
                    TempData["doc_num"] = isValid;
                    return RedirectToAction("Index");
                }
            }
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    var err = error.ErrorMessage;
                }
            }
            ViewBag.depareaList = new SelectList(_Generic.GetDepList(), "dep_area_id", "dep_area_code");
            return View();
        }
    }
}