using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class ShiftWiseProductionController : Controller
    {
        private readonly IShiftWiseProductionService _shiftwise;
        private readonly IGenericService _Generic;

        public ShiftWiseProductionController(IShiftWiseProductionService shiftwise, IGenericService Generic)
        {
            _shiftwise = shiftwise;
            _Generic = Generic;
        }

        [CustomAuthorizeAttribute("SWP")]
        // GET: ShiftWiseProduction
        public ActionResult Index()
        {
            ViewBag.num = TempData["doc_num"];
            ViewBag.DataSource = _shiftwise.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult CreateExcel(HttpPostedFileBase file)
        {
            if (file != null)
            {
                var msg = _shiftwise.AddExcel(file);
                TempData["doc_num"] = msg;
                if (msg.Contains("saved"))
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        [CustomAuthorizeAttribute("SWP")]
        public ActionResult Edit(DateTime? prod_date)
        {

            List<shift_wise_prod_detail_vm> vm = _shiftwise.Get((DateTime)prod_date);
            if (vm == null)
            {
                return HttpNotFound();
            }
            ViewBag.shift_list = new SelectList(_Generic.GetShiftlist(), "shift_id", "shift_code");         
            ViewBag.plant_list = new SelectList(_Generic.GetPlantCode(), "PLANT_ID", "PLANT_CODE");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.details = vm;
            return View(vm);
        }

        public ActionResult Save(List<shift_wise_prod_detail_vm> DepParaArr)
        {

            var isValid = _shiftwise.Add(DepParaArr);
            if (isValid.Contains("Saved"))
            {
                TempData["doc_num"] = isValid;
                return Json(isValid, JsonRequestBehavior.AllowGet);
            }
            ViewBag.shift_list = new SelectList(_Generic.GetShiftlist(), "shift_id", "shift_code");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View();
        }
    }
}