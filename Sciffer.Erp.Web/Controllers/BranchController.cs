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

namespace Sciffer.Erp.Web.Controllers
{
    public class BranchController : Controller
    {
        private readonly IBranchService _branchService;
        private readonly IGenericService _Generic;
        public BranchController(IBranchService branchService, IGenericService gen)
        {
            _branchService = branchService;
            _Generic = gen;
        }

        // GET: Branch
        public ActionResult Index()
        {
            ViewBag.DataSource = _branchService.GetAll();
            return View();
        }

        public ActionResult InlineDelete(int key)
        {
            var res = _branchService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(REF_BRANCH value)
        {

            var add = _Generic.CheckDuplicate(value.BRANCH_DESCRIPTION, value.BRANCH_NAME, "", "BRANCH", value.BRANCH_ID);
            if (add == 0)
            {
                if (value.BRANCH_ID == 0)
                {
                    var data1 = _branchService.Add(value);
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _branchService.Update(value);
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
                _branchService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
