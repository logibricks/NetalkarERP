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
    public class BankController : Controller
    {
        private readonly IBankService _bankService;
        private readonly IGenericService _Generic;
        public BankController(IBankService bank, IGenericService gen)
        {
            _bankService = bank;
            _Generic = gen;
        }

        // GET: Bank
        [CustomAuthorizeAttribute("BNK")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _bankService.GetAll();
            return View();
        }
       
      
        public ActionResult InlineDelete(int key)
        {
            var res = _bankService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(ref_bank value)
        {

            var add = _Generic.CheckDuplicate(value.bank_name, value.bank_code,"", "bank", value.bank_id);
            if (add == 0)
            {
                if (value.bank_id == 0)
                {
                    var data1 = _bankService.Add(value);                   
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _bankService.Update(value);
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
                _bankService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
