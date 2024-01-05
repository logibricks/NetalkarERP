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
using Sciffer.Erp.Domain.ViewModel;
using Syncfusion.EJ.Export;
using Syncfusion.JavaScript.Models;
using System.Web.Script.Serialization;
using System.Collections;
using Syncfusion.XlsIO;
using System.Reflection;
using System.IO;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class CompanyDetailsController : Controller
    {
        
        private readonly ICompanyService _companyservice;
        private readonly IStateService _stateservice;
        private readonly ICountryService _countryservice;
        private readonly IGenericService _genericservice;
        public readonly ICurrencyService _currencyservice;
        public readonly IOrgTypeService _orgTypeService;

        public CompanyDetailsController(IOrgTypeService OrgTypeService, ICompanyService CompanyService,IStateService StateService,ICountryService CountryService,IGenericService GenericService,ICurrencyService CurrencyService)
            {
            _companyservice = CompanyService;
            _stateservice = StateService;
            _countryservice = CountryService;
            _genericservice = GenericService;
            _currencyservice = CurrencyService;
            _orgTypeService = OrgTypeService;
            }


        // GET: Company
        [CustomAuthorizeAttribute("CMPYD")]
        public ActionResult Index()
        {
            ViewBag.datasource = _companyservice.GetCompanyDetail();
            ViewBag.hid = false;
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }
       
       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var countrylist = _countryservice.GetAll();
            var statelist = _stateservice.GetAll();
            var currencylist = _currencyservice.GetAll();
            // var statelist = _stateservice.GetAll().Where(c=>c.COUNTRY_ID);
            ViewBag.CountryNames = new SelectList(countrylist, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.StateNames = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.Currencies = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.stdcodes = new SelectList(countrylist, "COUNTRY_ID", "country_code");
            ViewBag.org_list = new SelectList(_orgTypeService.GetAll(), "ORG_TYPE_ID", "ORG_TYPE_NAME");
            var rEF_COMPANY = _companyservice.Get((int)id);
            if (rEF_COMPANY == null)
            {
                return HttpNotFound();
            }
            return View(rEF_COMPANY);
        }

        // GET: Company/Create
        [CustomAuthorizeAttribute("CMPYD")]
        public ActionResult Create()
        {
            var count = _companyservice.GetCompanyDetail().Count;
            if (count >= 1)
            {
                return RedirectToAction("Index");
            }
            REF_COMPANY_VM VM = new REF_COMPANY_VM();
            var countrylist = _countryservice.GetAll();
            var statelist = _stateservice.GetAll();
            var currencylist = _currencyservice.GetAll(); ;
            // var statelist = _stateservice.GetAll().Where(c=>c.COUNTRY_ID);
            ViewBag.CountryNames = new SelectList(countrylist, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.StateNames = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.Currencies = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.stdcodes = new SelectList(countrylist, "COUNTRY_ID", "country_code");
            ViewBag.org_list = new SelectList(_orgTypeService.GetAll(), "ORG_TYPE_ID", "ORG_TYPE_NAME");
            return View(VM);
        }

        // POST: Company/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(REF_COMPANY_VM rEF_COMPANY)
        {
            if (ModelState.IsValid)
            {
                var ISSAVED = _companyservice.Add(rEF_COMPANY);
                if (ISSAVED==true)
                {
                    return RedirectToAction("Index");
                }
            }
            var countrylist = _countryservice.GetAll();
            var statelist = _stateservice.GetAll();
            var currencylist = _currencyservice.GetAll();
            // var statelist = _stateservice.GetAll().Where(c=>c.COUNTRY_ID);
            ViewBag.CountryNames = new SelectList(countrylist, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.StateNames = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.Currencies = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.stdcodes = new SelectList(countrylist, "COUNTRY_ID", "country_code");
            ViewBag.org_list = new SelectList(_orgTypeService.GetAll(), "ORG_TYPE_ID", "ORG_TYPE_NAME");
            return View(rEF_COMPANY);
        }

        // GET: Company/Edit/5
        [CustomAuthorizeAttribute("CMPYD")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var countrylist = _countryservice.GetAll();
            var statelist = _stateservice.GetAll();
            var currencylist = _currencyservice.GetAll();
            // var statelist = _stateservice.GetAll().Where(c=>c.COUNTRY_ID);
            ViewBag.CountryNames = new SelectList(countrylist, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.StateNames = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.Currencies = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.stdcodes = new SelectList(countrylist, "COUNTRY_ID", "country_code");
            ViewBag.org_list = new SelectList(_orgTypeService.GetAll(), "ORG_TYPE_ID", "ORG_TYPE_NAME");
            var rEF_COMPANY = _companyservice.Get((int)id);
            if (rEF_COMPANY == null)
            {
                return HttpNotFound();
            }
            return View(rEF_COMPANY);
        }

        // POST: Company/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(REF_COMPANY_VM rEF_COMPANY)
        {
            if (ModelState.IsValid)
            {
                var ISEDIT = _companyservice.Update(rEF_COMPANY);
                if(ISEDIT==true)
                {
                    return RedirectToAction("Index");
                }
                
            }
            var countrylist = _countryservice.GetAll();
            var statelist = _stateservice.GetAll();
            var currencylist = _currencyservice.GetAll();
            // var statelist = _stateservice.GetAll().Where(c=>c.COUNTRY_ID);
            ViewBag.CountryNames = new SelectList(countrylist, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.StateNames = new SelectList(statelist, "STATE_ID", "STATE_NAME");
            ViewBag.Currencies = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.stdcodes = new SelectList(countrylist, "COUNTRY_ID", "country_code");
            ViewBag.org_list = new SelectList(_orgTypeService.GetAll(), "ORG_TYPE_ID", "ORG_TYPE_NAME");
            rEF_COMPANY = _companyservice.Get((int)rEF_COMPANY.COMPANY_ID);
            if (rEF_COMPANY == null)
            {
                return HttpNotFound();
            }
            return View(rEF_COMPANY);
        }

        // GET: Company/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_COMPANY rEF_COMPANY = null;
            if (rEF_COMPANY == null)
            {
                return HttpNotFound();
            }
            return View(rEF_COMPANY);
        }

        // POST: Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           
            return RedirectToAction("Index");
        }


        public ActionResult GetStateNamesFromCountry(int c)
        {
            var StateList = _genericservice.GetState(c);
            return Json(StateList, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _companyservice.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
