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
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class OperatorQualityParameterController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly IOperatorQualityParameterService _qualityParameter;

        public OperatorQualityParameterController(IGenericService generic,IOperatorQualityParameterService QualityParameter)
        {
            _Generic = generic;
            _qualityParameter = QualityParameter;
        }

        [CustomAuthorizeAttribute("OQP")]
        // GET: OperatorQualityParameter
        public ActionResult Index()
        {
            ViewBag.num = TempData["data"];
            ViewBag.DataSource = _qualityParameter.GetAll();
            return View();
        }

        // GET: OperatorQualityParameter/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ids = id.Split('$');
            mfg_op_qc_parameter_VM mfg_op_qc_parameter = _qualityParameter.Get(int.Parse(ids[0]), int.Parse(ids[1]));
            if (mfg_op_qc_parameter == null)
            {
                return HttpNotFound();
            }
            ViewBag.item_id = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.machine_id = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View(mfg_op_qc_parameter);
        }


        [CustomAuthorizeAttribute("OQP")]
        // GET: OperatorQualityParameter/Create
        public ActionResult Create()
        {
            ViewBag.item_id = new SelectList(_Generic.GetItemListOnlyRMCategory(3), "ITEM_ID", "ITEM_NAME");
            ViewBag.machine_id = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.OperationList = new SelectList(_Generic.GetOperationList(), "process_id", "process_description");
            return View();
        }

        // POST: OperatorQualityParameter/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(mfg_op_qc_parameter_VM vm)
        {
            if (ModelState.IsValid)
            {
                var issaved = _qualityParameter.Add(vm);
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

            ViewBag.item_id = new SelectList(_Generic.GetItemListOnlyRMCategory(3), "ITEM_ID", "ITEM_NAME");
            ViewBag.machine_id = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View(vm);
        }

        [CustomAuthorizeAttribute("OQP")]
        // GET: OperatorQualityParameter/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ids = id.Split('$');
            mfg_op_qc_parameter_VM mfg_op_qc_parameter = _qualityParameter.Get(int.Parse(ids[0]), int.Parse(ids[1]));
            if (mfg_op_qc_parameter == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemList = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.MachineList = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.OperationList = new SelectList(_Generic.GetOperationList(), "process_id", "process_description");
            return View(mfg_op_qc_parameter);
        }

        // POST: OperatorQualityParameter/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(mfg_op_qc_parameter_VM vm)
        {
            if (ModelState.IsValid)
            {
                var issaved = _qualityParameter.Add(vm);
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

        // GET: OperatorQualityParameter/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    mfg_op_qc_parameter_VM mfg_op_qc_parameter = _qualityParameter.Delete((int)id);
        //    if (mfg_op_qc_parameter == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(mfg_op_qc_parameter);
        //}

        //// POST: OperatorQualityParameter/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    mfg_op_qc_parameter mfg_op_qc_parameter = _qualityParameter.Delete((int)id);
        //    db.mfg_op_qc_parameter.Remove(mfg_op_qc_parameter);
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
