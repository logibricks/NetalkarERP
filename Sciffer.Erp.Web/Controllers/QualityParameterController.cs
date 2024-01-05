using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class QualityParameterController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly IQualityParameterService _QualityParameter;

        public QualityParameterController(IGenericService generic,IQualityParameterService qualityparameter)
        {
            _Generic = generic;
            _QualityParameter = qualityparameter;
        }

        [CustomAuthorizeAttribute("QCP")]
        // GET: QualityParameter
        public ActionResult Index()
        {
            ViewBag.num = TempData["data"];
            ViewBag.DataSource = _QualityParameter.GetAll();
            return View();
        }

        // GET: QualityParameter/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ids = id.Split('$');
            mfg_qc_qc_parameter_VM mfg_qc_qc_parameter = _QualityParameter.Get(int.Parse(ids[0]), int.Parse(ids[1]));
            if (mfg_qc_qc_parameter == null)
            {
                return HttpNotFound();
            }
            ViewBag.item_id = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.machine_id = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View(mfg_qc_qc_parameter);
        }

        [CustomAuthorizeAttribute("QCP")]
        // GET: QualityParameter/Create
        public ActionResult Create()
        {
            ViewBag.item_id = new SelectList(_Generic.GetItemListOnlyRMCategory(3), "ITEM_ID", "ITEM_NAME");
            ViewBag.machine_id = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.OperationList = new SelectList(_Generic.GetOperationList(), "process_id", "process_description");
            return View();
        }

        // POST: QualityParameter/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(mfg_qc_qc_parameter_VM vm)
        {
            if (ModelState.IsValid)
            {
                var issaved = _QualityParameter.Add(vm);
                if (issaved != "Error")
                {
                    TempData["data"] = issaved;
                    return RedirectToAction("Index", TempData["data"]);
                }
                ViewBag.error = "Something went wrong !";
            }
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    var er = error.ErrorMessage;
                }
            }

            ViewBag.item_id = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.machine_id = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View(vm);
        }

        [CustomAuthorizeAttribute("QCP")]
        // GET: QualityParameter/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ids = id.Split('$');
            mfg_qc_qc_parameter_VM mfg_qc_qc_parameter = _QualityParameter.Get(int.Parse(ids[0]),int.Parse(ids[1]));
            if (mfg_qc_qc_parameter == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemList = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.MachineList = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.OperationList = new SelectList(_Generic.GetOperationList(), "process_id", "process_description");
            return View(mfg_qc_qc_parameter);
        }

        // POST: QualityParameter/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(mfg_qc_qc_parameter_VM vm)
        {
            if (ModelState.IsValid)
            {
                var issaved = _QualityParameter.Add(vm);
                if (issaved == "Saved")
                {
                    TempData["data"] = issaved;
                    return RedirectToAction("Index", TempData["data"]);
                }
                ViewBag.error = "Something went wrong !";
            }
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    var er = error.ErrorMessage;
                }
            }
            ViewBag.item_id = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.machine_id = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View(vm);
        }

        //// GET: QualityParameter/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    mfg_qc_qc_parameter_VM mfg_qc_qc_parameter_VM = db.mfg_qc_qc_parameter_VM.Find(id);
        //    if (mfg_qc_qc_parameter_VM == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(mfg_qc_qc_parameter_VM);
        //}

        //// POST: QualityParameter/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    mfg_qc_qc_parameter_VM mfg_qc_qc_parameter_VM = db.mfg_qc_qc_parameter_VM.Find(id);
        //    db.mfg_qc_qc_parameter_VM.Remove(mfg_qc_qc_parameter_VM);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
