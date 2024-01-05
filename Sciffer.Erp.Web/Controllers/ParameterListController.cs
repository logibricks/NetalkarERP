using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class ParameterListController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly IParameterListService _param;
        public ParameterListController(IGenericService gen, IParameterListService param)
        {
            _Generic = gen;
            _param = param;
        }
        // GET: PrameterList
        public ActionResult Index()
        {
            ViewBag.DataSource = _param.GetAll();
            return View();
        }
        public ActionResult InlineDelete(int key)
        {
            var res = _param.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(ref_parameter_list value)
        {

            var add = _Generic.CheckDuplicate(value.parameter_code, "", "", "ParameterList", (int)value.parameter_id);
            if (add == 0)
            {
                if (value.parameter_id == 0)
                {
                    var data1 = _param.Add(value);
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _param.Update(value);
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { text = value.parameter_code }, JsonRequestBehavior.AllowGet);

            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _param.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult GetListParameter(int id)
        {
            var x = _param.Get(id);
            return Json(x, JsonRequestBehavior.AllowGet);
        }

    }
}