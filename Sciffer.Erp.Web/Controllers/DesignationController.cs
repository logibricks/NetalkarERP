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
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class DesignationController : Controller
    {
        private readonly IDesignationService _designationService;
        private readonly IGenericService _genericService;
        public DesignationController(IDesignationService DesignationService, IGenericService GenericService)
        {
            _designationService = DesignationService;
            _genericService = GenericService;
        }
        [CustomAuthorizeAttribute("DSNG")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _designationService.GetAll();
            return View();
        }

        public ActionResult InlineDelete(int key)
        {
            var res = _designationService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(REF_DESIGNATION value)
        {

            var add = _genericService.CheckDuplicate(value.designation_name, value.designation_code,"", "designation", value.designation_id);
            if (add == 0)
            {
                if (value.designation_id == 0)
                {
                    var data1 = _designationService.Add(value);
                    //var data1 = _countryService.GetAll();
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _designationService.Update(value);
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
                _designationService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
