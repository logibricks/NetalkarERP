using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using System;
using System.Linq;
using System.Collections.Generic;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class ValidationController : Controller
    {
      //  private ScifferContext db = new ScifferContext();
        private readonly IGenericService _Generic;
        private readonly IValidationService _Validation;
        public ValidationController(IGenericService gen, IValidationService validation)
        {
            _Generic = gen;
            _Validation = validation;
        }
        // GET: Validation
        [CustomAuthorizeAttribute("VALDM")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _Validation.GetAll();
            return View();
        }

        // GET: Validation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_validation ref_validation = null; ;
            if (ref_validation == null)
            {
                return HttpNotFound();
            }
            return View(ref_validation);
        }

        // GET: Validation/Create
        [CustomAuthorizeAttribute("VALDM")]
        public ActionResult Create()
        {
            var count = _Validation.GetAll().Count;
            if (count >= 1)
            {
                return RedirectToAction("Index");
            }
            ViewBag.generalleder = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            return View();
        }

        // POST: Validation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_validation ref_validation,FormCollection fc)
        {

            string ledgerdetail="";
            ledgerdetail = fc["ledgeraccounttype"];
            string[] emptyStringArray = new string[0];
            try
            {
                emptyStringArray = ledgerdetail.Split(new string[] { "~" }, StringSplitOptions.None);
            }
            catch
            {

            }
            List<ref_validation_gl> GlList = new List<ref_validation_gl>();
            for (int i = 0; i < emptyStringArray.Count() - 1; i++)
            {
                ref_validation_gl gl = new ref_validation_gl();
                gl.gl_ledger_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[1]);
                gl.ledger_account_type_id= int.Parse(emptyStringArray[i].Split(new char[] { ',' })[0]);
                GlList.Add(gl);
                // t11.Rows.Add(1, int.Parse(emptyStringArray[i].Split(new char[] { ',' })[1]), int.Parse(emptyStringArray[i].Split(new char[] { ',' })[0]));
            }
            ref_validation.ref_validation_gl = GlList;
            if (ModelState.IsValid)
            {
                var issaved = _Validation.Add(ref_validation);
                if(issaved)
                return RedirectToAction("Index");
            }
            ViewBag.generalleder = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            return View(ref_validation);
        }

        // GET: Validation/Edit/5
        [CustomAuthorizeAttribute("VALDM")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_validation ref_validation = _Validation.Get((int)id);
            if (ref_validation == null)
            {
                return HttpNotFound();
            }
            ViewBag.generalleder = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            return View(ref_validation);
        }

        // POST: Validation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_validation ref_validation, FormCollection fc)
        {
            string ledgerdetail = "";
            ledgerdetail = fc["ledgeraccounttype"];
            string[] emptyStringArray = new string[0];
            try
            {
                emptyStringArray = ledgerdetail.Split(new string[] { "~" }, StringSplitOptions.None);
            }
            catch
            {

            }
            List<ref_validation_gl> GlList = new List<ref_validation_gl>();
            for (int i = 0; i < emptyStringArray.Count() - 1; i++)
            {
                ref_validation_gl gl = new ref_validation_gl();
                gl.gl_ledger_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[1]);
                gl.ledger_account_type_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[0]);
                GlList.Add(gl);
                // t11.Rows.Add(1, int.Parse(emptyStringArray[i].Split(new char[] { ',' })[1]), int.Parse(emptyStringArray[i].Split(new char[] { ',' })[0]));
            }
            ref_validation.ref_validation_gl = GlList;
            if (ModelState.IsValid)
            {
                var issaved = _Validation.Add(ref_validation);
                if (issaved)
                    return RedirectToAction("Index");
            }
            ViewBag.generalleder = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            return View(ref_validation);
        }

        // GET: Validation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_validation ref_validation = null;
            if (ref_validation == null)
            {
                return HttpNotFound();
            }
            return View(ref_validation);
        }

        // POST: Validation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           // ref_validation ref_validation = db.ref_validation.Find(id);
           // db.ref_validation.Remove(ref_validation);
           // db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _Generic.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
