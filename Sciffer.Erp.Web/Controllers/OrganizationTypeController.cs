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
    public class OrganizationTypeController : Controller
    {
        private readonly IOrgTypeService _countryService;
        private readonly IGenericService _Generic;
        public OrganizationTypeController(IOrgTypeService countryService, IGenericService Gen)
        {
            _countryService = countryService;
            _Generic = Gen;
        }

        // GET: Organization_Type
        [CustomAuthorizeAttribute("ORGM")]
        public ActionResult Index()
        {
            ViewBag.Datasource = _countryService.GetAll();
            return View();
        }
       

        // GET: Organization_Type/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_ORG_TYPE = _countryService.Get((int)id);
            if (rEF_ORG_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(rEF_ORG_TYPE);
        }

        public ActionResult InlineDelete(int key)
        {
            var res = _countryService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _countryService.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult InlineInsert(REF_ORG_TYPE value)
        {

            var add = _Generic.CheckDuplicate(value.ORG_TYPE_NAME,"", "", "Org", value.ORG_TYPE_ID);          
            if (add == 0)
            {
                if (value.ORG_TYPE_ID == 0)
                {
                    var data1= _countryService.Add(value);
                    //var data1 = _countryService.GetAll();
                   return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1=_countryService.Update(value);
                    // var data1 = _countryService.GetAll();
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }        
                
            }
            else
            { 
                return Json(new {text = "duplicate" }, JsonRequestBehavior.AllowGet);

            }
        }
    }
}
