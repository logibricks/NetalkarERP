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
    public class CurrencyController : Controller
    {
        private readonly ICurrencyService _currencyService;
        private readonly ICountryService _countyService;
        private readonly IGenericService _Generic;

        public CurrencyController(ICurrencyService currencyService, ICountryService countryService, IGenericService gen)
        {
            _currencyService = currencyService;
            _countyService = countryService;
            _Generic = gen;
        }
      
       

        public ActionResult InlineDelete(int key)
        {
            var res = _currencyService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(REF_CURRENCYVM value)
        {

            var add = _Generic.CheckDuplicate(value.CURRENCY_NAME, value.CURRENCY_COUNTRY_ID.ToString(),"", "currency", value.CURRENCY_ID);
            if (add == 0)
            {
                if (value.CURRENCY_ID == 0)
                {
                    var data1 = _currencyService.Add(value);
                    //var data1 = _countryService.GetAll();
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _currencyService.Update(value);
                    // var data1 = _countryService.GetAll();
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { text = "duplicate" }, JsonRequestBehavior.AllowGet);

            }
        }

        // GET: Currency
        [CustomAuthorizeAttribute("CRNCY")]
        public ActionResult Index()
        {
            ViewBag.COUNTRY_ID = _countyService.GetAll();
            ViewBag.DataSource = _currencyService.GetAll();
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _currencyService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
