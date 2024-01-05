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
    public class FinancialYearController : Controller
    {
        private readonly IFinancialYearService _financeService;
        private readonly IGenericService _Generic;
        public FinancialYearController(IFinancialYearService financeService, IGenericService gen)
        {
            _financeService = financeService;
            _Generic = gen;
        }

        // GET: FinancialYear
        [CustomAuthorizeAttribute("FY")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _financeService.GetAll();
            ViewBag.financelist = new SelectList(_financeService.GetAll(),"FINANCIAL_YEAR_ID","FINANCIAL_YEAR_NAME");
            return View();
        }
       

        public ActionResult InlineDelete(int key)
        {
            var res = _financeService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(REF_FINANCIAL_YEAR value)
        {

            var add = _Generic.CheckDuplicate(value.FINANCIAL_YEAR_NAME, value.FINANCIAL_YEAR_FROM.ToString()+","+value.FINANCIAL_YEAR_TO.ToString(),"", "financialyear", value.FINANCIAL_YEAR_ID);
            if (add == 0)
            {
                if (value.FINANCIAL_YEAR_ID == 0)
                {
                    var data1 = _financeService.Add(value);
                    //var data1 = _countryService.GetAll();
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _financeService.Update(value);
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
                _financeService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
