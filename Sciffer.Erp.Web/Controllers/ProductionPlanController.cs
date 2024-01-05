using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class ProductionPlanController : Controller
    {
        private readonly IProductionPlanService _prodplan;
        private readonly IGenericService _Generic;

        public ProductionPlanController(IProductionPlanService prodplan, IGenericService Generic)
        {
            _prodplan = prodplan;
            _Generic = Generic;
        }
        // GET: ProductionPlan
        public ActionResult Index()
        {
            ViewBag.num = TempData["doc_num"];
            ViewBag.DataSource = _prodplan.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult CreateExcel(HttpPostedFileBase file)
        {
            if (file != null)
            {
                var msg = _prodplan.AddExcel(file);
                TempData["doc_num"] = msg;
                if (msg.Contains("saved"))
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(DateTime? prod_date)
        {

            List <prod_plan_detail_vm> vm = _prodplan.Get((DateTime) prod_date);
            if (vm == null)
            {
                return HttpNotFound();
            }
            ViewBag.shift_list = new SelectList(_Generic.GetShiftlist(), "shift_id", "shift_code");
            ViewBag.operator_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.details = vm;
            return View(vm);
        }

        public ActionResult Save( List<prod_plan_detail_vm> DepParaArr)
        {
           
                var isValid = _prodplan.Add(DepParaArr);
                if (isValid.Contains("Saved"))
                {
                    TempData["doc_num"] = isValid;
                    return Json(isValid, JsonRequestBehavior.AllowGet);
                }
            ViewBag.shift_list = new SelectList(_Generic.GetShiftlist(), "shift_id", "shift_code");
            ViewBag.operator_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View();
        }
    }
}