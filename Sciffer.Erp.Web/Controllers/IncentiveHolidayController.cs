using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Implementation;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class IncentiveHolidayController : Controller
    {
        private readonly IIncetiveHolidayService _iincetiveholidayservice;

        public IncentiveHolidayController(IIncetiveHolidayService iincetiveholidayservice)
        {
            _iincetiveholidayservice = iincetiveholidayservice;
        }

        public ActionResult InlineDelete(DateTime date)
        {
            var res = _iincetiveholidayservice.Delete(date);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(ref_mfg_incentive_holiday value)
        {
            var data1 = _iincetiveholidayservice.Add(value);
            return Json(data1, JsonRequestBehavior.AllowGet);
        }

        // GET: IncentiveHoliday
        [CustomAuthorizeAttribute("INCENTIVE")]
        public ActionResult Index()
        {
            ViewBag.Datasource = _iincetiveholidayservice.GetAll();
            return View();
        }
    }
}