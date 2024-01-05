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
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class InternalReconcileController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly IEntityTypeService _entityTypeService;
        private readonly IFinInternalReconcileService _finInternalReconcileService;
        public InternalReconcileController(IGenericService GenericService, IEntityTypeService EntityTypeService, IFinInternalReconcileService FinInternalReconcileService)
        {
            _entityTypeService = EntityTypeService;
            _Generic = GenericService;
            _finInternalReconcileService = FinInternalReconcileService;
        }
        [CustomAuthorizeAttribute("IR")]
        // GET: FinInternalReconcile
        public ActionResult Index()
        {
            ViewBag.doc = TempData["doc_num"];
            ViewBag.dataSource = _finInternalReconcileService.GetAll();
            return View();
        }


        [CustomAuthorizeAttribute("IR")]
        // GET: FinInternalReconcile/Create
        public ActionResult Create()
        {
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(89), "document_numbring_id", "category");
            ViewBag.entity_type_list = new SelectList(_Generic.GetEntityList(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");
            return View();
        }

        // POST: FinInternalReconcile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(fin_internal_reconcile_vm fin_internal_reconcile)
        {
            if (ModelState.IsValid)
            {
                var issaved = _finInternalReconcileService.Add(fin_internal_reconcile);
                if (issaved.Contains("Saved"))
                {
                    TempData["doc_num"] = issaved.Split('~')[1];
                    return RedirectToAction("Index");
                }
            }
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(89), "document_numbring_id", "category");
            ViewBag.entity_type_list = new SelectList(_Generic.GetEntityList(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");

            return View(fin_internal_reconcile);
        }
        [CustomAuthorizeAttribute("IR")]
        // GET: FinInternalReconcile/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fin_internal_reconcile_vm fin_internal_reconcile = _finInternalReconcileService.Get((int)id);
            if (fin_internal_reconcile == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(89), "document_numbring_id", "category");
            ViewBag.entity_type_list = new SelectList(_Generic.GetEntityList(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(92), "cancellation_reason_id", "cancellation_reason");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View(fin_internal_reconcile);
        }

        // POST: FinInternalReconcile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(fin_internal_reconcile_vm fin_internal_reconcile)
        {
            if (ModelState.IsValid)
            {
                var isValid = _finInternalReconcileService.Add(fin_internal_reconcile);
                return RedirectToAction("Index");
            }
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(89), "document_numbring_id", "category");
            ViewBag.entity_type_list = new SelectList(_Generic.GetEntityList(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(92), "cancellation_reason_id", "cancellation_reason");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View(fin_internal_reconcile);
        }
        public ActionResult forInternalReconcileDetail(int entity_type_id, int entity_id, DateTime from_date)
        {
            var reconcile = _finInternalReconcileService.forInternalReconcileDetail(entity_type_id, entity_id, from_date);
            return Json(reconcile, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteConfirmed(int id, string cancellation_remarks, int cancellation_reason_id)
        {
            var isValid = _finInternalReconcileService.Delete(id, cancellation_remarks, cancellation_reason_id);
            if (isValid.Contains("Cancelled"))
            {
                return Json(isValid);
            }
            return Json(isValid);
        }
    }
}
