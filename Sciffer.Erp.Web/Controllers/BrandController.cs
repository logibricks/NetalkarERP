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
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly IGenericService _Generic;
        public BrandController(IBrandService brandService, IGenericService gen)
        {
            _brandService = brandService;
            _Generic = gen;
        }

        // GET: Brand
        [CustomAuthorizeAttribute("BRND")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _brandService.GetAll();
            return View();
        }
        public JsonResult GetBrand()
        {
            var res = _brandService.GetAll();
            return Json(new { result = res, count = res.Count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InlineDelete(int key)
        {
            var res = _brandService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(REF_BRAND value)
        {

            var add = _Generic.CheckDuplicate(value.BRAND_NAME,"", "", "brand", (int)value.BRAND_ID);
            if (add == 0)
            {
                if (value.BRAND_ID == 0)
                {
                    var data1 = _brandService.Add(value);
                    //var data1 = _countryService.GetAll();
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _brandService.Update(value);
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
                _brandService.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Validate(REF_BRAND order)
        {
            if (!ModelState.IsValid)
            {
                List<string> errorlist = new List<string>();
                foreach (ModelState modelState in ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        errorlist.Add(error.ErrorMessage);
                    }
                }
                return Content(new JavaScriptSerializer().Serialize(errorlist));
            }
            return Content("true");

        }
    }
}
