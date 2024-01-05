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
    public class MultiMachiningController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly IMultiMachiningService _multimac;

        public MultiMachiningController(IGenericService Generic, IMultiMachiningService multimac)
        {
            _Generic = Generic;
            _multimac = multimac;
        }
        // GET: MultiMachining
        [CustomAuthorizeAttribute("MULM")]
        public ActionResult Index()
        {
            ViewBag.doc = TempData["doc_num"];
            return View();
        }
        [CustomAuthorizeAttribute("MULM")]
        // GET: MultiMachining
        public ActionResult Create()
        {
            ViewBag.machinelist = _Generic.GetMachineList(0);
            return View();
        }

        public ActionResult GetAllMachineForMultiMac(string entity)
        {
            var data = _Generic.GetAllMachineForMultiMac(entity);
            return Json(data);

        }

        public ActionResult DeleteMultiMachine(int group_id)
        {
            var data = _multimac.DeleteMultiMachine(group_id);
            return Json(data);

        }
        public ActionResult UpdateRecords(ref_mfg_multi_machining_vm vm)
        {
            var saved = _multimac.UpdateRecords(vm);
            if (saved == true)
            {
                TempData["doc_num"] = "Records";
                return RedirectToAction("Create");
                
            }
            else
            {
                return RedirectToAction("Create", new { msg = "failed" });
            }

        }

    }
}