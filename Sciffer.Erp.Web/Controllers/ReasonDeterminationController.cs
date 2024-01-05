using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using System;
using Syncfusion.JavaScript.Models;
using Syncfusion.EJ.Export;
using System.Web.Script.Serialization;
using System.Collections;
using System.Collections.Generic;
using Syncfusion.XlsIO;
using System.Reflection;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class ReasonDeterminationController : Controller
    {
        private readonly IReasonDeterminationService _reasonDeterminationService;
        private readonly IGenericService _Generic;
        public ReasonDeterminationController(IReasonDeterminationService ReasonDeterminationService, IGenericService gen)
        {
            _reasonDeterminationService = ReasonDeterminationService;
            _Generic = gen;
        }

        [CustomAuthorizeAttribute("RSNDT")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _reasonDeterminationService.GetReasonList(0);
            return View();
        }
      

        public ActionResult InlineDelete(int key)
        {
            var res = _reasonDeterminationService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InlineInsert(reasonvm value)
        {

            var add = _Generic.CheckDuplicate(value.REASON_DETERMINATION_NAME, value.REASON_DETERMINATION_TYPE.ToString(),"", "reason", value.REASON_DETERMINATION_ID);
            if (add == 0)
            {
                if (value.REASON_DETERMINATION_ID == 0)
                {
                    var data1 = _reasonDeterminationService.Add(value);
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _reasonDeterminationService.Update(value);
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { text = "duplicate" }, JsonRequestBehavior.AllowGet);

            }
        }
      

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _reasonDeterminationService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
