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
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IGenericService _Generic;
        public CategoryController(ICategoryService categoryService, IGenericService gen)
        {
            _categoryService = categoryService;
            _Generic = gen;
        }

        // GET: Category
        public ActionResult Index()
        {
            ViewBag.DataSource = _categoryService.GetAll();
            return View(_categoryService.GetAll());
        }
      

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_CATEGORY rEF_CATEGORY = _categoryService.Get(id);
            if (rEF_CATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(rEF_CATEGORY);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(REF_CATEGORY rEF_CATEGORY)
        {
            if (ModelState.IsValid)
            {
             var isValid=   _categoryService.Add(rEF_CATEGORY);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(rEF_CATEGORY);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_CATEGORY rEF_CATEGORY = _categoryService.Get(id);
            if (rEF_CATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(rEF_CATEGORY);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(REF_CATEGORY rEF_CATEGORY)
        {
            if (ModelState.IsValid)
            {
             var isValid=   _categoryService.Update(rEF_CATEGORY);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(rEF_CATEGORY);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_CATEGORY rEF_CATEGORY = _categoryService.Get(id);
            if (rEF_CATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(rEF_CATEGORY);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
          var isValid=  _categoryService.Delete(id);
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
                _categoryService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
