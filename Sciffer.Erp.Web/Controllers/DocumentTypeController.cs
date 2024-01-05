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

namespace Sciffer.Erp.Web.Controllers
{
    public class DocumentTypeController : Controller
    {
        private ScifferContext db = new ScifferContext();
        private readonly IDocumentTypeService _documentTypeService;
        public DocumentTypeController(IDocumentTypeService DocumentTypeService)
        {
            _documentTypeService = DocumentTypeService;
        }
        // GET: DocumentType
        public ActionResult Index()
        {
            return View(db.ref_document_type.ToList());
        }

        // GET: DocumentType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_document_type ref_document_type = db.ref_document_type.Find(id);
            if (ref_document_type == null)
            {
                return HttpNotFound();
            }
            return View(ref_document_type);
        }

        // GET: DocumentType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DocumentType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "document_type_id,document_type_name")] ref_document_type ref_document_type)
        {
            if (ModelState.IsValid)
            {
                db.ref_document_type.Add(ref_document_type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ref_document_type);
        }

        // GET: DocumentType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_document_type ref_document_type = db.ref_document_type.Find(id);
            if (ref_document_type == null)
            {
                return HttpNotFound();
            }
            return View(ref_document_type);
        }

        // POST: DocumentType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "document_type_id,document_type_name")] ref_document_type ref_document_type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ref_document_type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ref_document_type);
        }

        // GET: DocumentType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_document_type ref_document_type = db.ref_document_type.Find(id);
            if (ref_document_type == null)
            {
                return HttpNotFound();
            }
            return View(ref_document_type);
        }

        // POST: DocumentType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ref_document_type ref_document_type = db.ref_document_type.Find(id);
            db.ref_document_type.Remove(ref_document_type);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
