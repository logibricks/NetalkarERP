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
    public class DivisionController : Controller
    {
        private readonly IDivisionService _divisionService;
        private readonly IGenericService _Generic;

        public DivisionController(IDivisionService DivisionService, IGenericService gen)
        {
            _divisionService = DivisionService;
            _Generic = gen;
        }

        // GET: Payment_Terms
        [CustomAuthorizeAttribute("DIV")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _divisionService.GetAll();
            return View(_divisionService.GetAll());
        }

        public ActionResult InlineDelete(int key)
        {
            var res = _divisionService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(REF_DIVISION value)
        {

            var add = _Generic.CheckDuplicate(value.DIVISION_NAME, value.DIVISION_NAME, "", "division", value.DIVISION_ID);
            if (add == 0)
            {
                if (value.DIVISION_ID == 0)
                {
                    var data1 = _divisionService.Add(value);
                    //var data1 = _countryService.GetAll();
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _divisionService.Update(value);
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
                _divisionService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
