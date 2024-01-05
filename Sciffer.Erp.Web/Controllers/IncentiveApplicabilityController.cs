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
    public class IncentiveApplicabilityController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly IIncentiveApplicabilityService _incapp;

        public IncentiveApplicabilityController(IGenericService Generic, IIncentiveApplicabilityService incapp)
        {
            _Generic = Generic;
            _incapp = incapp;


        }


        // GET: IncentiveApplicability
        [CustomAuthorizeAttribute("INCENTIVE")]
        public ActionResult Index()
        {
            var incentive = _Generic.GetUserList();
            ViewBag.userlist = incentive;
            ViewBag.doc = TempData["doc_num"];
            return View();
        }

        public ActionResult GetAllUserForIncApp(string user_id)
        {
            var data = _Generic.GetAllUserForIncApp(user_id);
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult UpdateRecords(ref_mfg_operator_incentive_appl_vm vm)
        {
            var saved = _incapp.UpdateRecords(vm);
            if (saved == true)
            {
                TempData["doc_num"] = "Records";
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", new { msg = "failed" });
            }

        }

    }
}