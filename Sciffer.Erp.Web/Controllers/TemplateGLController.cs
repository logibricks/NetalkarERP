using System;
using Sciffer.Erp.Service.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Web.CustomFilters;
using Sciffer.Erp.Domain.Model;

namespace Sciffer.Erp.Web.Controllers
{
    public class TemplateGLController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly IFinancialTemplateService _Fanencial;
        public TemplateGLController(IGenericService Generic, IFinancialTemplateService Fanencial)
        {
            _Generic = Generic;
            _Fanencial = Fanencial;


        }
        public ActionResult Index()
        {
            ViewBag.GlList = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.TemplateList = new SelectList(_Fanencial.getall().Where(x=>x.is_blocked==false), "template_id", "template_name");
            ViewBag.TemplateGroupList = _Fanencial.GetGroupName().Where(x => x.is_active == true && x.main_heading==false).Select(a=>new {a.group_name,a.template_detail_id,a.template_id });
            ViewBag.msg = TempData["data"];
            return View();
        }
        public ActionResult UpdateRecords(ref_fin_template_gl_mapping_vm vm)
        {
            var saved = _Fanencial.AddTemplateGLMapping(vm);
            if (saved == true)
            {
                TempData["data"] = " Saved Successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["data"] = "Some Thing went wrong.";
                return RedirectToAction("Index");

            }

        }
        public ActionResult GetGroupByTemplate(int id)
        {
            var vm = _Fanencial.GetGroupByTemplate(id);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
    }
}