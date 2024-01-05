using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class OperatorIncentiveSummaryController : Controller
    {

        private readonly IGenericService _Generic;
        private readonly IShiftService _shift;

        public OperatorIncentiveSummaryController(IGenericService Generic, IShiftService shift)
        {
            _Generic = Generic;
            _shift = shift;


        }
        // GET: OperatorIncentiveSummary
        public ActionResult Index()
        {
            ViewBag.shiftlist = new SelectList(_shift.GetAll(), "shift_id", "shift_desc");
            return View();
        }

        public ActionResult GetOperatorIncentiveSummary(DateTime? from_date, DateTime? to_date, int? user_id)
        {
            var data = _Generic.GetOperatorIncentiveSummary(from_date, to_date, user_id);
            return Json(data, JsonRequestBehavior.AllowGet);

        }

    }
}