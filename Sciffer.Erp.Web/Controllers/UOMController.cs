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
    public class UOMController : Controller
    {
        private readonly IUOMService _countryService;
        private readonly IGenericService _Generic;
        public UOMController(IUOMService countryService, IGenericService gen)
        {
            _countryService = countryService;
            _Generic = gen;
        }

        // GET: UOM
        [CustomAuthorizeAttribute("UOM")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _countryService.GetAll();
            return View();
        }
      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _countryService.Dispose();
            }
            base.Dispose(disposing);
        }
        public JsonResult GetUOM()
        {
            var res = _countryService.GetAll();
            return Json(new { result = res, count = res.Count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InlineDelete(int key)
        {
            var res = _countryService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(REF_UOM value)
        {

            var add = _Generic.CheckDuplicate(value.UOM_NAME, value.UOM_DESCRIPTION,"", "uom", value.UOM_ID);
            if (add == 0)
            {
                if (value.UOM_ID == 0)
                {
                    var data1 = _countryService.Add(value);                   
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _countryService.Update(value);
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { text = "duplicate" }, JsonRequestBehavior.AllowGet);

            }
        }
    }
}
