using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class OperatorIncentiveDetailController : Controller
    {

        private readonly IGenericService _Generic;
      

        public OperatorIncentiveDetailController(IGenericService Generic)
        {
            _Generic = Generic;
            
        }
        // GET: OperatorIncentiveDetail
        public ActionResult Index()
        {
            ViewBag.shiftlist = new SelectList(_Generic.GetShiftlist(), "shift_id", "shift_desc");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.machinelist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.operationlist = new SelectList(_Generic.GetOperationList(), "process_id", "process_description");
            return View();
        }

        public ActionResult GetOperatorIncentiveDetail(DateTime? from_date, DateTime? to_date, int? user_id)
        {
            var data = _Generic.GetOperatorIncentiveDetail(from_date, to_date, user_id);
            return Json(data, JsonRequestBehavior.AllowGet);

        }
    }
}