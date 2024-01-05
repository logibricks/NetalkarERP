using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;

namespace Sciffer.Erp.Web.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeService _gradeService;
        private readonly IGenericService _Generic;
        public GradeController(IGradeService GradeService, IGenericService gen)
        {
            _gradeService = GradeService;
            _Generic = gen;
        }
        // GET: Grade
        [CustomFilters.CustomAuthorizeAttribute("GRD")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _gradeService.GetAll();
            return View();
        }

        public ActionResult InlineDelete(int key)
        {
            var res = _gradeService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(REF_GRADE value)
        {

            var add = _Generic.CheckDuplicate(value.grade_name, value.grade_name, "", "grade", value.grade_id);
            if (add == 0)
            {
                if (value.grade_id == 0)
                {
                    var data1 = _gradeService.Add(value);
                    //var data1 = _countryService.GetAll();
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _gradeService.Update(value);
                    // var data1 = _countryService.GetAll();
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
                _gradeService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
