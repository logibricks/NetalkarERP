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
    public class ModuleController : Controller
    {
        private readonly IModuleService _moduleService;
        public ModuleController(IModuleService module)
        {
            _moduleService = module;
           
        }
        // GET: Module
        public ActionResult Index()
        {
            ViewBag.datasource = _moduleService.GetAll();
            return View();
        }
       
        // GET: Module/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_module ref_module =_moduleService.Get((int)id);
            if (ref_module == null)
            {
                return HttpNotFound();
            }
            return View(ref_module);
        }

        // GET: Module/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Module/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_module ref_module)
        {
            if (ModelState.IsValid)
            {
                var issaved = _moduleService.Add(ref_module);
                if(issaved)
                {
                    return RedirectToAction("Index");
                }               
            }

            return View(ref_module);
        }

        // GET: Module/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_module ref_module = _moduleService.Get((int)id);
            if (ref_module == null)
            {
                return HttpNotFound();
            }
            return View(ref_module);
        }

        // POST: Module/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_module ref_module)
        {
            if (ModelState.IsValid)
            {
                var isedit = _moduleService.Update(ref_module);
                if(isedit)
                {
                    return RedirectToAction("Index");
                }               
            }
            return View(ref_module);
        }

        // GET: Module/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_module ref_module = _moduleService.Get((int)id);
            if (ref_module == null)
            {
                return HttpNotFound();
            }
            return View(ref_module);
        }

        // POST: Module/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var isdelete = _moduleService.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _moduleService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
