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
    public class ModuleFormController : Controller
    {
        private readonly IModuleService _moduleService;
        private readonly IModuleFormService _moduleformService;
        public ModuleFormController(IModuleService module, IModuleFormService mf)
        {
            _moduleService = module;
            _moduleformService = mf;
        }
       
        // GET: ModuleForm
        public ActionResult Index()
        {
            ViewBag.datasource = _moduleformService.GetModuleForm();
            return View();
        }

        // GET: ModuleForm/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_module_form ref_module_form = _moduleformService.Get((int)id);
            if (ref_module_form == null)
            {
                return HttpNotFound();
            }
            return View(ref_module_form);
        }

        // GET: ModuleForm/Create
        public ActionResult Create()
        {
            ViewBag.modulelist = new SelectList(_moduleService.GetAll(), "module_id", "module_name");
            return View();
        }

        // POST: ModuleForm/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_module_form ref_module_form)
        {
            if (ModelState.IsValid)
            {
                var issaved = _moduleformService.Add(ref_module_form);
                if(issaved)
                return RedirectToAction("Index");
            }

            ViewBag.modulelist = new SelectList(_moduleService.GetAll(), "module_id", "module_name", ref_module_form.module_id);
            return View(ref_module_form);
        }

        // GET: ModuleForm/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_module_form ref_module_form = _moduleformService.Get((int)id);
            if (ref_module_form == null)
            {
                return HttpNotFound();
            }
            ViewBag.modulelist = new SelectList(_moduleService.GetAll(), "module_id", "module_name");
            return View(ref_module_form);
        }

        // POST: ModuleForm/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_module_form ref_module_form)
        {
            if (ModelState.IsValid)
            {
                var isedit = _moduleformService.Update(ref_module_form);
                if(isedit)
                return RedirectToAction("Index");
            }
            ViewBag.modulelist = new SelectList(_moduleService.GetAll(), "module_id", "module_name");
            return View(ref_module_form);
        }

        // GET: ModuleForm/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_module_form ref_module_form = _moduleformService.Get((int)id);
            if (ref_module_form == null)
            {
                return HttpNotFound();
            }
            return View(ref_module_form);
        }

        // POST: ModuleForm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var isdelete = _moduleformService.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _moduleformService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
