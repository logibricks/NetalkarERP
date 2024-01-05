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
    public class StateController : Controller
    {
        private readonly IStateService db;
        private readonly ICountryService _countyService;
       private readonly IGenericService _Generic;

        public StateController(IStateService stateService, ICountryService countryService, IGenericService gen)
        {
             db= stateService;
            _countyService = countryService;
            _Generic = gen;
        }

        // GET: State
        [CustomAuthorizeAttribute("STA")]
        public ActionResult Index()
        {
            var countries = _countyService.GetAll();
            ViewBag.Countries = countries;
            ViewBag.DataSource = db.GetStateList();
            return View();
        }

        public JsonResult GetState()
        {
            var res = db.GetStateList();
            var countries = _countyService.GetAll();
            ViewBag.Countries = countries;
            return Json(new { result = res, count = res.Count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InlineDelete(int key)
        {
            var res = db.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }


        public ActionResult InlineInsert(state_vm value)
        {

            var add = _Generic.CheckDuplicate(value.state_name,"", "", "state", value.state_id);
            if (add == 0)
            {
                if (value.state_id == 0)
                {
                    var data1 = db.Add(value);
                    //var data1 = _countryService.GetAll();
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = db.Update(value);
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
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
