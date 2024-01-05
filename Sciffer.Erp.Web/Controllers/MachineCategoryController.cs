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
    public class MachineCategoryController : Controller
    {
        private readonly IMachineCategoryService _machinecatservice;
        private readonly IGenericService _Generic;
        public MachineCategoryController(IMachineCategoryService machinecatservice, IGenericService Generic)
        {
            _machinecatservice = machinecatservice;
            _Generic = Generic;
        }

        // GET: MachineCategory
        [CustomAuthorizeAttribute("MACAT")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _machinecatservice.GetAll();
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _machinecatservice.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult InlineInsert(ref_machine_category value)
        {

            var add = _Generic.CheckDuplicate(value.machine_category_code, "", "", "customercategory", value.machine_category_id);
            if (add == 0)
            {
                if (value.machine_category_id == 0)
                {
                    var data1 = _machinecatservice.Add(value);
                    //var data1 = _countryService.GetAll();
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _machinecatservice.Update(value);
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
