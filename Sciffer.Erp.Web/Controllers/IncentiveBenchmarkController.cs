using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
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
    public class IncentiveBenchmarkController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly IIncentiveBenchmarkService _incentivebenchmark;

        public IncentiveBenchmarkController(IGenericService Generic, IIncentiveBenchmarkService incentivebenchmark)
        {
            _Generic = Generic;
            _incentivebenchmark = incentivebenchmark;
        }
        // GET: IncentiveBenchmark
        [CustomAuthorizeAttribute("INCENTIVE")]
        public ActionResult Index()
        {

            ViewBag.doc = TempData["doc_num"];
            ViewBag.DataSource = _Generic.GetInsentiveBenchmarkDetail("incentive_benchmark").OrderByDescending(a=>a.mfg_incentive_benchmark_id).ToList();
            return View();
        }

        public ActionResult Create(int? id)
        {

            if(id != null)
            {
                ref_mfg_incentive_benchmark_vm incentive_benchmark = _incentivebenchmark.Get((int)id);
               
                ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
                ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
                ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
                ViewBag.operation_list = new SelectList(_Generic.GetOperationList(), "process_id", "process_description");
                return View(incentive_benchmark);
            }

            else
            {
                ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
                ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
                ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
                ViewBag.operation_list = new SelectList(_Generic.GetOperationList(), "process_id", "process_description");
                return View();
            }
           
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_mfg_incentive_benchmark_vm incben)
        {
            var data1 = _incentivebenchmark.Add(incben);
            TempData["doc_num"] = "Incentive Benchmark";
            return RedirectToAction("Index");

        }

        // GET: IncentiveBenchmark/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_mfg_incentive_benchmark_vm ref_mfg_incentive_benchmark_vm = _incentivebenchmark.Get((int)id);
            if (ref_mfg_incentive_benchmark_vm == null)
            {
                return HttpNotFound();
            }
            return View(ref_mfg_incentive_benchmark_vm);
        }

     
        public ActionResult DeleteConfirmed(int id)
        {
            bool isdeleted = _incentivebenchmark.Delete(id);
            if (isdeleted == true)
            {
                return RedirectToAction("Index");
            }
            ref_mfg_incentive_benchmark_vm ref_mfg_incentive_benchmark_vm = _incentivebenchmark.Get((int)id);
            if (ref_mfg_incentive_benchmark_vm == null)
            {
                return HttpNotFound();
            }
            return View(ref_mfg_incentive_benchmark_vm);

        }

        public ActionResult GetMachineListByProcess(int process_id)
        {
            var data = new SelectList(_Generic.GetMachineListByProcess(process_id), "machine_id", "machine_name");
            return Json(data);
        }

    }
}