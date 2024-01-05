using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class MapToolOperationController : Controller
    {
        private readonly IMapToolOperationService _mapToolOperation;
        private readonly IGenericService _generic;

        public MapToolOperationController(IMapToolOperationService MapToolOperation, IGenericService Generic)
        {
            _mapToolOperation = MapToolOperation;
            _generic = Generic;
        }

        [CustomAuthorizeAttribute("COTP")]
        // GET: MapToolOperation
        public ActionResult Index()
        {
            ViewBag.num = TempData["data"];
            ViewBag.DataSource = _mapToolOperation.GetAll();
            return View();
        }

        // GET: MapToolOperation/Details/5
        public ActionResult Details(int? id)
        {
            
            return View();
        }

        [CustomAuthorizeAttribute("COTP")]
        // GET: MapToolOperation/Create
        public ActionResult Create()
        {
            ViewBag.tool_id = new SelectList(_mapToolOperation.GetItemToolList(), "tool_id", "ITEM_NAME");
            ViewBag.crankshaft_id = new SelectList(_mapToolOperation.GetItemCrankshaftList(), "crankshaft_id", "ITEM_NAME");
            ViewBag.process_id = new SelectList(_generic.GetOperationList(), "process_id", "process_description");
            ViewBag.tool_usage_type_id = new SelectList(_mapToolOperation.GetToolUsagetypeList(), "tool_usage_type_id", "tool_usage_type_name");
            ViewBag.tool_category_id = new SelectList(_mapToolOperation.GetToolCatagoryList(), "tool_category_id", "tool_category_name");
            return View();
        }

        // POST: MapToolOperation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_tool_operation_map_vm vm)
        {
            if (ModelState.IsValid)
            {
                var issaved = _mapToolOperation.Add(vm);
                if (issaved != "Error")
                {
                    TempData["data"] = issaved;
                    return RedirectToAction("Index", TempData["data"]);
                }
                ViewBag.error = "Something went wrong !";
            }
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    var er = error.ErrorMessage;
                }
            }

            ViewBag.tool_id = new SelectList(_mapToolOperation.GetItemToolList(), "tool_id", "ITEM_NAME");
            ViewBag.crankshaft_id = new SelectList(_mapToolOperation.GetItemCrankshaftList(), "crankshaft_id", "ITEM_NAME");
            ViewBag.process_id = new SelectList(_generic.GetOperationList(), "process_id", "process_description");
            ViewBag.tool_usage_type_id = new SelectList(_mapToolOperation.GetToolUsagetypeList(), "tool_usage_type_id", "tool_usage_type_name");
            ViewBag.tool_category_id = new SelectList(_mapToolOperation.GetToolCatagoryList(), "tool_category_id", "tool_category_name");
            return View(vm);
        }


        [CustomAuthorizeAttribute("COTP")]
        //GET: MapToolOperation/Edit/5//
        public ActionResult Edit(int? id)
        
        {

            ref_tool_operation_map_vm ref_tool_operation_map = _mapToolOperation.Get((int)id);
            ViewBag.tool_id = new SelectList(_mapToolOperation.GetItemToolList(), "tool_id", "ITEM_NAME");
            ViewBag.crankshaft_id = new SelectList(_mapToolOperation.GetItemCrankshaftList(), "crankshaft_id", "ITEM_NAME");
            ViewBag.process_id = new SelectList(_generic.GetOperationList(), "process_id", "process_description");
            ViewBag.tool_usage_type_id = new SelectList(_mapToolOperation.GetToolUsagetypeList(), "tool_usage_type_id", "tool_usage_type_name");
            ViewBag.tool_category_id = new SelectList(_mapToolOperation.GetToolCatagoryList(), "tool_category_id", "tool_category_name");
            return View(ref_tool_operation_map);
        }

        // POST: MapToolOperation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "tool_operation_map_id,process_id,process_description,ITEM_CATEGORY_ID,ITEM_NAME_CRANKSHAFT,ITEM_ID,ITEM_NAME,tool_usage_type_id,tool_usage_type_name,tool_category_id,tool_category_name,is_blocked")] ref_tool_operation_map_vm ref_tool_operation_map_VM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //    }
        //    return View(ref_tool_operation_map_VM);
        //}

        // GET: MapToolOperation/Delete/5
        public ActionResult Delete(int? id)
        {
           
            return View();
        }

        // POST: MapToolOperation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            
        }
    }
}
