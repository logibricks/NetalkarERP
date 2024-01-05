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
    public class BusinessUnitController : Controller
    {
        private readonly IBusinessUnitService _businessService;
        private readonly IGenericService _Generic;
        public BusinessUnitController(IBusinessUnitService businessService, IGenericService gen)
        {
            _businessService = businessService;
            _Generic = gen;
        }

        // GET: BusinessUnit
        [CustomAuthorizeAttribute("BU")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _businessService.GetAll();
            return View();
        }
       

        public ActionResult InlineDelete(int key)
        {
            var res = _businessService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(REF_BUSINESS_UNIT value)
        {

            var add = _Generic.CheckDuplicate(value.BUSINESS_UNIT_NAME, value.BUSINESS_UNIT_DESCRIPTION,"", "businessunit", value.BUSINESS_UNIT_ID);
            if (add == 0)
            {
                if (value.BUSINESS_UNIT_ID == 0)
                {
                    var data1 = _businessService.Add(value);
                    //var data1 = _countryService.GetAll();
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _businessService.Update(value);
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
                _businessService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
