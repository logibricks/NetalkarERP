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

namespace Sciffer.Erp.Web.Controllers
{
    public class CustomerContactsController : Controller
    {
        private ScifferContext db = new ScifferContext();

        // GET: CustomerContacts
        public ActionResult Index()
        {
            var rEF_CUSTOMER_CONTACTS = db.REF_CUSTOMER_CONTACTS.Include(r => r.REF_CUSTOMER);
            return View(rEF_CUSTOMER_CONTACTS.ToList());
        }

        // GET: CustomerContacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_CUSTOMER_CONTACTS rEF_CUSTOMER_CONTACTS = db.REF_CUSTOMER_CONTACTS.Find(id);
            if (rEF_CUSTOMER_CONTACTS == null)
            {
                return HttpNotFound();
            }
            return View(rEF_CUSTOMER_CONTACTS);
        }

        // GET: CustomerContacts/Create
        public ActionResult Create()
        {
            ViewBag.CUSTOMER_ID = new SelectList(db.REF_CUSTOMER, "CUSTOMER_ID", "CUSTOMER_CODE");
            return View();
        }

        // POST: CustomerContacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CUSTOMER_CONTACT_ID,CUSTOMER_ID,CONTACT_NAME,DESIGNATION,MOBILE_NO,PHONE_NO,SEND_SMS_FLAG,SEND_EMAIL_FLAG")] REF_CUSTOMER_CONTACTS rEF_CUSTOMER_CONTACTS)
        {
            if (ModelState.IsValid)
            {
                db.REF_CUSTOMER_CONTACTS.Add(rEF_CUSTOMER_CONTACTS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CUSTOMER_ID = new SelectList(db.REF_CUSTOMER, "CUSTOMER_ID", "CUSTOMER_CODE", rEF_CUSTOMER_CONTACTS.CUSTOMER_ID);
            return View(rEF_CUSTOMER_CONTACTS);
        }

        // GET: CustomerContacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_CUSTOMER_CONTACTS rEF_CUSTOMER_CONTACTS = db.REF_CUSTOMER_CONTACTS.Find(id);
            if (rEF_CUSTOMER_CONTACTS == null)
            {
                return HttpNotFound();
            }
            ViewBag.CUSTOMER_ID = new SelectList(db.REF_CUSTOMER, "CUSTOMER_ID", "CUSTOMER_CODE", rEF_CUSTOMER_CONTACTS.CUSTOMER_ID);
            return View(rEF_CUSTOMER_CONTACTS);
        }

        // POST: CustomerContacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CUSTOMER_CONTACT_ID,CUSTOMER_ID,CONTACT_NAME,DESIGNATION,MOBILE_NO,PHONE_NO,SEND_SMS_FLAG,SEND_EMAIL_FLAG")] REF_CUSTOMER_CONTACTS rEF_CUSTOMER_CONTACTS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rEF_CUSTOMER_CONTACTS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CUSTOMER_ID = new SelectList(db.REF_CUSTOMER, "CUSTOMER_ID", "CUSTOMER_CODE", rEF_CUSTOMER_CONTACTS.CUSTOMER_ID);
            return View(rEF_CUSTOMER_CONTACTS);
        }

        // GET: CustomerContacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_CUSTOMER_CONTACTS rEF_CUSTOMER_CONTACTS = db.REF_CUSTOMER_CONTACTS.Find(id);
            if (rEF_CUSTOMER_CONTACTS == null)
            {
                return HttpNotFound();
            }
            return View(rEF_CUSTOMER_CONTACTS);
        }

        // POST: CustomerContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            REF_CUSTOMER_CONTACTS rEF_CUSTOMER_CONTACTS = db.REF_CUSTOMER_CONTACTS.Find(id);
            db.REF_CUSTOMER_CONTACTS.Remove(rEF_CUSTOMER_CONTACTS);
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
