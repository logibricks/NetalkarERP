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
    public class FreightTermsController : Controller
    {
        private readonly IFreightTermsService _FreightService;
        private readonly IGenericService _Generic;
        public FreightTermsController(IFreightTermsService FreightService, IGenericService grn)
        {
            _FreightService = FreightService;
            _Generic = grn;
        }

        [CustomAuthorizeAttribute("FRGHT")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _FreightService.GetAll();
            return View();
        }
       

        public ActionResult InlineDelete(int key)
        {
            var res = _FreightService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(REF_FREIGHT_TERMS value)
        {

            var add = _Generic.CheckDuplicate(value.FREIGHT_TERMS_NAME,"", "", "freight", value.FREIGHT_TERMS_ID);
            if (add == 0)
            {
                if (value.FREIGHT_TERMS_ID == 0)
                {
                    var data1 = _FreightService.Add(value);
                    //var data1 = _countryService.GetAll();
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _FreightService.Update(value);
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
                _FreightService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
