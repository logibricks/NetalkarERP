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
    public class ExchangeRateController : Controller
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly ICurrencyService _currencyService;
        private readonly IGenericService _Generic;
        public ExchangeRateController(ICurrencyService CurrencyService, IExchangeRateService ExchangeRateService, IGenericService gen)
        {
            _currencyService = CurrencyService;
            _exchangeRateService = ExchangeRateService;
            _Generic = gen;
        }

        // GET: ExchangeRate
        [CustomAuthorizeAttribute("EXCR")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _exchangeRateService.GetExchanagelist();
            ViewBag.currency_list1 = new SelectList(_currencyService.GetCurrency1(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.currency_list2 = new SelectList(_currencyService.GetCurrency2(), "CURRENCY_ID", "CURRENCY_NAME");
            return View();
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var t1 = _exchangeRateService.Get((int)id);
            var t2 = _currencyService.Get((int)id);
            if (t1 == null)
            {
                return HttpNotFound();
            }
            return View(t1);
        }

        public ActionResult InlineDelete(int key)
        {
            var res = _exchangeRateService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }




        public ActionResult InlineInsert(ref_exchangerate_vm value)
        {

            var add = _Generic.CheckDuplicate(value.currency1.ToString()+","+ value.currency2.ToString(), value.from_date.ToString(),"", "exchangerate", value.exchange_rate_id);
            if (add == 0)
            {
                if (value.exchange_rate_id == 0)
                {
                    var data1 = _exchangeRateService.Add(value);
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _exchangeRateService.Update(value);
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
                _exchangeRateService.Dispose();
            }
            base.Dispose(disposing);
        }
       
    }
}
