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
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class PlantController : Controller
    {
        private readonly IPlantService _plantservice;
        private readonly IStateService _stateservice;
        private readonly ICountryService _countryservice;
        private readonly IGenericService _genericservice;
        public PlantController(IPlantService PlantService, IStateService StateService, ICountryService CountryService,IGenericService GenericService)
        {
            _plantservice = PlantService;
            _stateservice = StateService;
            _countryservice = CountryService;
            _genericservice = GenericService;
        }

        // GET: Plant
        [CustomAuthorizeAttribute("PLNT")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _plantservice.GetPlantList();
            return View();
        }
       

        // GET: Plant/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var statelist = _stateservice.GetAll();
            ViewBag.StateNames = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            var countrylist = _countryservice.GetAll();
            ViewBag.CountryNames = new SelectList(countrylist, "COUNTRY_ID", "COUNTRY_NAME");
            var rEF_PLANT = _plantservice.Get((int)id);

            if (rEF_PLANT == null)
            {
                return HttpNotFound();
            }
            return View(rEF_PLANT);
        }

        // GET: Plant/Create
        [CustomAuthorizeAttribute("PLNT")]
        public ActionResult Create()
        {
            REF_PLANT_VM VM = new REF_PLANT_VM();
            var statelist = _stateservice.GetAll();
            ViewBag.StateNames = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            var countrylist = _countryservice.GetAll();
            ViewBag.CountryNames = new SelectList(countrylist, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.CountryCodes = new SelectList(countrylist, "COUNTRY_ID", "COUNTRY_CODE");
            return View(VM);
        }

        // POST: Plant/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(REF_PLANT_VM rEF_PLANT)
        {
            if (ModelState.IsValid)
            {
                var output = _plantservice.Add(rEF_PLANT);
               if(output==true)
                return RedirectToAction("Index");
            }

            var statelist = _stateservice.GetAll();
            ViewBag.StateNames = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            var countrylist = _countryservice.GetAll();
            ViewBag.CountryNames = new SelectList(countrylist, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.CountryCodes = new SelectList(countrylist, "COUNTRY_ID", "COUNTRY_CODE");

            return View(rEF_PLANT);
        }

        // GET: Plant/Edit/5
        [CustomAuthorizeAttribute("PLNT")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var statelist = _stateservice.GetAll();
            ViewBag.StateNames = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            var countrylist = _countryservice.GetAll();
            ViewBag.CountryNames = new SelectList(countrylist, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.CountryCodes = new SelectList(countrylist, "COUNTRY_ID", "COUNTRY_CODE");
            var rEF_PLANT = _plantservice.Get((int)id);

            if (rEF_PLANT == null)
            {
                return HttpNotFound();
            }
            return View(rEF_PLANT);
        }

        // POST: Plant/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( REF_PLANT_VM rEF_PLANT)
        {
            if (ModelState.IsValid)
            {
                var isedit = _plantservice.Update(rEF_PLANT);
                if(isedit==true)
                return RedirectToAction("Index");
            }

            var statelist = _stateservice.GetAll();
            ViewBag.StateNames = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            var countrylist = _countryservice.GetAll();
            ViewBag.CountryNames = new SelectList(countrylist, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.CountryCodes = new SelectList(countrylist, "COUNTRY_ID", "COUNTRY_CODE");
            return View(rEF_PLANT);
        }

        // GET: Plant/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_PLANT = _plantservice.Get((int)id);
            if (rEF_PLANT == null)
            {
                return HttpNotFound();
            }
            return View(rEF_PLANT);
        }

        // POST: Plant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //REF_PLANT rEF_PLANT = db.REF_PLANT.Find(id);
            //db.REF_PLANT.Remove(rEF_PLANT);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _plantservice.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
