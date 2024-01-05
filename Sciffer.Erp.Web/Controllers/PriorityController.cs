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
    public class PriorityController : Controller
    {
        private readonly IPriorityService _countryService;
        private readonly IGenericService _Generic;
        public PriorityController(IPriorityService countryService, IGenericService gen)
        {
            _countryService = countryService;
            _Generic = gen;
        }

        // GET: Priority
        [CustomAuthorizeAttribute("PRTY")]
        public ActionResult Index()
        {
            ViewBag.datasource = _countryService.GetAll();
            return View();
        }
      

        public ActionResult InlineDelete(int key)
        {
            var res = _countryService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InlineInsert(ref_priority_vm value)
        {

            var add = _Generic.CheckDuplicate(value.PRIORITY_NAME, value.form_id.ToString(),"", "priority", value.PRIORITY_ID);
            if (add == 0)
            {
                if (value.PRIORITY_ID == 0)
                {
                    var data1 = _countryService.Add(value);
                    //var data1 = _countryService.GetAll();
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _countryService.Update(value);
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
                _countryService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
