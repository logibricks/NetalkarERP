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
    public class PaymentTermsController : Controller
    {
        private readonly IPaymentTermsService _customerService;
        private readonly IPaymentTermsDueDateService _paymentTermsDueDate;
        private readonly IGenericService _Generic;
        public PaymentTermsController(IPaymentTermsService customerService, IPaymentTermsDueDateService PaymentTermsDueDate, IGenericService gen)
        {
            _customerService = customerService;
            _paymentTermsDueDate = PaymentTermsDueDate;
            _Generic = gen;
        }

        public ActionResult Index()
        {
            ViewBag.DataSource = _customerService.GetPaymentTerms();
            var DueDates = _paymentTermsDueDate.GetAll();
            ViewBag.DueDateNames = new SelectList(DueDates, "PAYMENT_TERMS_DUE_DATE_ID", "PAYMENT_TERMS_DUE_DATE_NAME");
            return View();
        }
      

        public ActionResult InlineDelete(int key)
        {
            var res = _customerService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(payment_terms_vm value)
        {

            var add = _Generic.CheckDuplicate(value.payment_terms_code, value.payment_terms_description,"", "paymentterms", value.payment_terms_id);
            if (add == 0)
            {
                if (value.payment_terms_id == 0)
                {
                    var data1 = _customerService.Add(value);
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _customerService.Update(value);
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
                _customerService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
