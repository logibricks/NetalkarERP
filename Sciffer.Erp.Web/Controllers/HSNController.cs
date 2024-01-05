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
using Syncfusion.XlsIO;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class HSNController : Controller
    {
        private readonly IHSNCodeMasterService _hsncodemasterservice;
        private readonly IGenericService _Generic;

        public HSNController(IHSNCodeMasterService hsn, IGenericService gen)
        {
            _hsncodemasterservice = hsn;
            _Generic = gen;
        }

        [CustomAuthorizeAttribute("HSN")]
        public ActionResult Index()
        {
            ViewBag.within_state = new SelectList(_Generic.GetWithinState(1), "tax_id", "tax_code");
            ViewBag.inter_state = new SelectList(_Generic.GetWithinState(2), "tax_id", "tax_code");
            ViewBag.DataSource = _hsncodemasterservice.GetAll();
            return View();
        }

        public ActionResult InlineDelete(int key)
        {
            var res = _hsncodemasterservice.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(ref_hsn_code value)
        {

            var add = _Generic.CheckDuplicate(value.hsn_code, value.hsn_code_description, "", "HSN", value.hsn_code_id);
            if (add == 0)
            {
                if (value.hsn_code_id == 0)
                {
                    var data1 = _hsncodemasterservice.Add(value);
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _hsncodemasterservice.Update(value);
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
                _hsncodemasterservice.Dispose();
            }
            base.Dispose(disposing);
        }
    }


}

