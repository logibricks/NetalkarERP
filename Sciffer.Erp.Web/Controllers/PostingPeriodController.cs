using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.ViewModel;
using System;
using Syncfusion.JavaScript.Models;
using Syncfusion.JavaScript;
using Syncfusion.EJ.Export;
using System.Web.Script.Serialization;
using System.Collections;
using System.Collections.Generic;
using Syncfusion.XlsIO;
using System.Reflection;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class PostingPeriodController : Controller
    {
        private readonly IGenericService _genericService;
        private readonly IPostingPeriodsService _posting;
        private readonly IFinancialYearService _finance;
        private readonly IFrequencyService _frequency;
        private readonly IStatusService _status;
        public PostingPeriodController(IStatusService status, IGenericService gen, IPostingPeriodsService pos, IFinancialYearService finance, IFrequencyService frequency)
        {
            _genericService = gen;
            _posting = pos;
            _finance = finance;
            _frequency = frequency;
            _status = status;
        }
        // GET: PostingPeriods
        [CustomAuthorizeAttribute("PSTPR")]
        public ActionResult Index()
        {
            ViewBag.Datasource = _posting.GetPostingPeriods();
            return View();
        }

        // GET: PostingPeriods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ref_posting_periods_vm = _posting.Get((int)id);
            if (ref_posting_periods_vm == null)
            {
                return HttpNotFound();
            }
            return View(ref_posting_periods_vm);
        }

        // GET: PostingPeriods/Create
        [CustomAuthorizeAttribute("PSTPR")]
        public ActionResult Create()
        {
            ViewBag.financiallist = new SelectList(_finance.GetAll(), "FINANCIAL_YEAR_ID", "FINANCIAL_YEAR_NAME");
            ViewBag.frequencylist = new SelectList(_frequency.GetAll(), "frequency_id", "frequency_name");
            return View();
        }

        // POST: PostingPeriods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_posting_periods_vm ref_posting_periods_vm, FormCollection fc)
        {
            ref_posting_periods_vm.codelist = fc["periodsdetail"];
            if (ModelState.IsValid)
            {
                var issaved = _posting.Add(ref_posting_periods_vm);
                if (issaved)
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.financiallist = new SelectList(_finance.GetAll(), "FINANCIAL_YEAR_ID", "FINANCIAL_YEAR_NAME");
            ViewBag.frequencylist = new SelectList(_frequency.GetAll(), "frequency_id", "frequency_name");
            return View(ref_posting_periods_vm);
        }

        // GET: PostingPeriods/Edit/5
        [CustomAuthorizeAttribute("PSTPR")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.DataSource = _posting.GetPostingPeriodsForEdit((int)id);
            ViewBag.Status = _genericService.GetStatusList("IC");
            var ref_posting_periods_vm = _posting.GetPostingPeriodsForEdit((int)id);
            if (ref_posting_periods_vm == null)
            {
                return HttpNotFound();
            }
            ViewBag.financiallist = new SelectList(_finance.GetAll(), "FINANCIAL_YEAR_ID", "FINANCIAL_YEAR_NAME");
            ViewBag.frequencylist = new SelectList(_frequency.GetAll(), "frequency_id", "frequency_name");
            return View(ref_posting_periods_vm);
        }

        // POST: PostingPeriods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_posting_periods_vm ref_posting_periods_vm, FormCollection fc, posting_periods value)
        {
            ref_posting_periods_vm.codelist = fc["periodsdetail"];
            if (ModelState.IsValid)
            {
                var isedit = _posting.Update(ref_posting_periods_vm);
                if (isedit)
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.financiallist = new SelectList(_finance.GetAll(), "FINANCIAL_YEAR_ID", "FINANCIAL_YEAR_NAME");
            ViewBag.frequencylist = new SelectList(_frequency.GetAll(), "frequency_id", "frequency_name");
            return View(ref_posting_periods_vm);
        }

        // GET: PostingPeriods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ref_posting_periods_vm = _posting.Get((int)id);
            if (ref_posting_periods_vm == null)
            {
                return HttpNotFound();
            }
            ViewBag.financiallist = new SelectList(_finance.GetAll(), "FINANCIAL_YEAR_ID", "FINANCIAL_YEAR_NAME");
            ViewBag.frequencylist = new SelectList(_frequency.GetAll(), "frequency_id", "frequency_name");
            return View(ref_posting_periods_vm);
        }

        // POST: PostingPeriods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var isdelete = _posting.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _posting.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpPost]
        public ActionResult ChangePostingStatus(posting_periods value)
        {
            var change = _posting.ChangePostingStatus(value);
            //return Json(change, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Index");
        }
    }
}
