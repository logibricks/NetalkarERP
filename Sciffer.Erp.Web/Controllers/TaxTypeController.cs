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
    public class TaxTypeController : Controller
    {
        private ITaxTypeService _taxTypeService;
        private readonly IGenericService _Generic;
        public TaxTypeController(ITaxTypeService TaxTypeService, IGenericService gen)
        {
            _taxTypeService = TaxTypeService;
            _Generic = gen;
        }

        // GET: TaxType
        [CustomAuthorizeAttribute("TAXT")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _taxTypeService.GetAll();
            return View();
        }
       
        public ActionResult InlineInsert(ref_tax_type value)
        {

            var add = _Generic.CheckDuplicate(value.tax_type_name,"", "", "taxtype", value.tax_type_id);
            if (add == 0)
            {
                if (value.tax_type_id == 0)
                {
                    var data1 = _taxTypeService.Add(value);
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _taxTypeService.Update(value);
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { text = "duplicate" }, JsonRequestBehavior.AllowGet);

            }
        }
      
       

        public ActionResult InlineDelete(int key)
        {
            var res = _taxTypeService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

       

        // GET: TaxType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_tax_type ref_tax_type = _taxTypeService.Get(id);
            if (ref_tax_type == null)
            {
                return HttpNotFound();
            }
            return View(ref_tax_type);
        }

        // GET: TaxType/Create
        [CustomAuthorizeAttribute("TAXT")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaxType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.


        // GET: TaxType/Edit/5
        [CustomAuthorizeAttribute("TAXT")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_tax_type ref_tax_type = _taxTypeService.Get(id);
            if (ref_tax_type == null)
            {
                return HttpNotFound();
            }
            return View(ref_tax_type);
        }

        // POST: TaxType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
       

        // GET: TaxType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_tax_type ref_tax_type = _taxTypeService.Get(id);
            if (ref_tax_type == null)
            {
                return HttpNotFound();
            }
            return View(ref_tax_type);
        }

        // POST: TaxType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var isValid = _taxTypeService.Delete(id);
            if (isValid == true)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _taxTypeService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
