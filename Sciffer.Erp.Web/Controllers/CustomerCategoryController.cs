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
    public class CustomerCategoryController : Controller
    {
        private readonly ICustomerCategoryService _countryService;
        private readonly IGenericService _Generic;
        public CustomerCategoryController(ICustomerCategoryService countryService, IGenericService gen)
        {
            _countryService = countryService;
            _Generic = gen;
        }

        // GET: Customer_Category
        [CustomAuthorizeAttribute("CSTRC")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _countryService.GetAll();
            return View();
        }
       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _countryService.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult InlineInsert(REF_CUSTOMER_CATEGORY value)
        {

            var add = _Generic.CheckDuplicate(value.CUSTOMER_CATEGORY_NAME,"", "", "customercategory", value.CUSTOMER_CATEGORY_ID);
            if (add == 0)
            {
                if (value.CUSTOMER_CATEGORY_ID == 0)
                {
                    var data1 = _countryService.Add(value);
                    //var data1 = _countryService.GetAll();
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _countryService.Update(value);
                    // var data1 = _countryService.GetAll();
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
