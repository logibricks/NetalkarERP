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
    public class PaymentTypeController : Controller
    {
        private readonly IPaymentTypeService _payment;
        private readonly IGenericService _Generic;

        public PaymentTypeController(IPaymentTypeService payment, IGenericService gen)
        {
            _payment = payment;
            _Generic = gen;
        }

        // GET: BusinessUnit
        public ActionResult Index()
        {
            ViewBag.DataSource = _payment.GetAll();
            return View();
        }
       

        public ActionResult InlineDelete(int key)
        {
            var res = _payment.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(REF_PAYMENT_TYPE value)
        {

            var add = _Generic.CheckDuplicate(value.PAYMENT_TYPE_NAME,"", "", "paymenttype", value.PAYMENT_TYPE_ID);
            if (add == 0)
            {
                if (value.PAYMENT_TYPE_ID == 0)
                {
                    var data1 = _payment.Add(value);
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _payment.Update(value);
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { text = "duplicate" }, JsonRequestBehavior.AllowGet);

            }
        }
        
      

        // GET: PaymentType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_PAYMENT_TYPE rEF_PAYMENT_TYPE = _payment.Get((int)id);
            if (rEF_PAYMENT_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(rEF_PAYMENT_TYPE);
        }

        // GET: PaymentType/Create
        public ActionResult Create()
        {
            return View();
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _payment.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
