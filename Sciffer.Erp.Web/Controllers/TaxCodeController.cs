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
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class TaxCodeController : Controller
    {
        private readonly ITaxService _taxcode;
        private readonly ITaxElementService _taxelement;
        private readonly IGenericService _Generic;
        private readonly ITaxChargedOnService _TaxCharged;
        // GET: TaxCode
        public TaxCodeController(ITaxService taxcode, ITaxElementService taxelement, IGenericService gen, ITaxChargedOnService taxcharge)
        {
            _taxcode = taxcode;
            _taxelement = taxelement;
            _Generic = gen;
            _TaxCharged = taxcharge;
        }

        [CustomAuthorizeAttribute("TAXCD")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _taxcode.GetAll();
            return View();
        }
       

        // GET: TaxCode/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ref_tax = _taxcode.Get((int)id);
            if (ref_tax == null)
            {
                return HttpNotFound();
            }
            return View(ref_tax);
        }

        // GET: TaxCode/Create
        [CustomAuthorizeAttribute("TAXCD")]
        public ActionResult Create()
        {
            ViewBag.taxelementlist = _taxelement.getall();
            ViewBag.taxcharged = new SelectList(_TaxCharged.GetAll(), "tax_chargerd_on_id", "tax_chargerd_on_name");
            return View();
        }

        // POST: TaxCode/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_tax_vm ref_tax,FormCollection fc)
        {
            ref_tax.taxdetail = fc["taxdetail"];
            if (ModelState.IsValid)
            {
                var issaved = _taxcode.Add(ref_tax);
                if(issaved)
                return RedirectToAction("Index");
            }
            ViewBag.taxelementlist = _taxelement.getall();
            ViewBag.taxcharged = new SelectList(_TaxCharged.GetAll(), "tax_chargerd_on_id", "tax_chargerd_on_name");
            return View(ref_tax);
        }

        // GET: TaxCode/Edit/5
        [CustomAuthorizeAttribute("TAXCD")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ref_tax = _taxcode.Get((int)id);
            if (ref_tax == null)
            {
                return HttpNotFound();
            }
            ViewBag.taxelementlist = _taxelement.getall();
            ViewBag.taxcharged = new SelectList(_TaxCharged.GetAll(), "tax_chargerd_on_id", "tax_chargerd_on_name");
            return View(ref_tax);
        }

        // POST: TaxCode/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_tax_vm ref_tax,FormCollection fc)
        {
            ref_tax.taxdetail = fc["taxdetail"];
            ref_tax.deleteids= fc["deleteids"];
            if (ModelState.IsValid)
            {
                var isedit = _taxcode.Update(ref_tax);
                if(isedit)
                return RedirectToAction("Index");
            }
            ViewBag.taxelementlist = _taxelement.getall();
            ViewBag.taxcharged = new SelectList(_TaxCharged.GetAll(), "tax_chargerd_on_id", "tax_chargerd_on_name");
            return View(ref_tax);
        }

        // GET: TaxCode/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ref_tax = _taxcode.Get((int)id);
            if (ref_tax == null)
            {
                return HttpNotFound();
            }
            ViewBag.taxelementlist = _taxelement.GetAll();
            ViewBag.taxcharged = new SelectList(_TaxCharged.GetAll(), "tax_chargerd_on_id", "tax_chargerd_on_name");
            return View(ref_tax);
        }

     

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _taxcode.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
