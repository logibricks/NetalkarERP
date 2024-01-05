using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class CancellationReasonController : Controller
    {
        private readonly ICancellationReasonService _cancellationreason;
        private readonly IGenericService _Generic;
        private readonly IModuleFormService _moduleService;
        public CancellationReasonController(IModuleFormService module, ICancellationReasonService notificationService, IGenericService gen)
        {
            _moduleService = module;
            _cancellationreason = notificationService;
            _Generic = gen;
        }
        // GET: Country
        [CustomAuthorizeAttribute("CNTRY")]
        public ActionResult Index()
        {
            ViewBag.datasource = _cancellationreason.GetAll();
            ViewBag.modulelist = new SelectList(_moduleService.GetAll().Where(x=>x.doc_numbering_flag==true && x.is_active==true).ToList(), "module_form_id", "module_form_name");
            return View();
        }
        public ActionResult InlineDelete(int key)
        {
            var res = _cancellationreason.Delete(key);
            ViewBag.modulelist = new SelectList(_moduleService.GetAll().Where(x=>x.doc_numbering_flag==true && x.is_active==true).ToList(), "module_form_id", "module_form_name");
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(ref_cancellation_reason value)
        {
            ViewBag.modulelist = new SelectList(_moduleService.GetAll().Where(x => x.doc_numbering_flag == true && x.is_active == true).ToList(), "module_form_id", "module_form_name");
            var add = _Generic.CheckDuplicate(value.module_form_id.ToString(), "", "", "CancellationReasonController", value.cancellation_reason_id);
            if (add == 0)
            {
                if (value.cancellation_reason_id == 0)
                {
                    var data1 = _cancellationreason.Add(value);
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _cancellationreason.Update(value);
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { text = value.module_form_id }, JsonRequestBehavior.AllowGet);

            }
        }



        /*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cancellationreason.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}