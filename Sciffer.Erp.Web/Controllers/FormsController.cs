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
    public class FormsController : Controller
    {
        private readonly IFormService _form;
        private readonly IGenericService _Generic;
        public FormsController(IFormService form, IGenericService gen)
        {
            _form = form;
            _Generic = gen;
        }


        // GET: Forms
        [CustomAuthorizeAttribute("FOR")]
        public ActionResult Index()
        {
            ViewBag.Datasource = _form.GetAll();
            return View();
        }
        

        public ActionResult InlineDelete(int key)
        {
            var res = _form.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(REF_FORM value)
        {
            var add = _Generic.CheckDuplicate(value.FORM_NAME,"","", "form", value.FORM_ID);
            if (add == 0)
            {
                if (value.FORM_ID == 0)
                {
                    var data1 = _form.Add(value);
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _form.Update(value);
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
                _form.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
