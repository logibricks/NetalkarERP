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
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IGenericService _genericService;

        public DepartmentController(IDepartmentService DepartmentService, IGenericService GenericService)
        {
            _departmentService = DepartmentService;
            _genericService = GenericService;
        }

        // GET: Payment_Terms
        [CustomAuthorizeAttribute("DPRT")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _departmentService.GetAll();
            return View();
        }


        public ActionResult InlineDelete(int key)
        {
            var res = _departmentService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(REF_DEPARTMENT value)
        {

            var add = _genericService.CheckDuplicate(value.DEPARTMENT_NAME, value.DEPARTMENT_DESCRIPTION, "", "department", value.DEPARTMENT_ID);
            if (add == 0)
            {
                if (value.DEPARTMENT_ID == 0)
                {
                    var data1 = _departmentService.Add(value);
                    //var data1 = _countryService.GetAll();
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _departmentService.Update(value);
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
                _departmentService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
