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
    public class AccountAssignmentController : Controller
    {
        private readonly IAccountAssignmentService _accountAssignmentService;
        public AccountAssignmentController(IAccountAssignmentService AccountAssignmentService)
        {
            _accountAssignmentService = AccountAssignmentService;
        }

        // GET: AccountAssignment
        public ActionResult Index()
        {
            return View(_accountAssignmentService.GetAll());
        }

        // GET: AccountAssignment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_account_assignment po_account_asignment = _accountAssignmentService.Get(id);
            if (po_account_asignment == null)
            {
                return HttpNotFound();
            }
            return View(po_account_asignment);
        }

        // GET: AccountAssignment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountAssignment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_account_assignment account_asignment)
        {
            if (ModelState.IsValid)
            {
                var isValid = _accountAssignmentService.Add(account_asignment);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(account_asignment);
        }

        // GET: AccountAssignment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_account_assignment account_asignment = _accountAssignmentService.Get(id);
            if (account_asignment == null)
            {
                return HttpNotFound();
            }
            return View(account_asignment);
        }

        // POST: AccountAssignment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_account_assignment account_asignment)
        {
            if (ModelState.IsValid)
            {
                var isValid = _accountAssignmentService.Update(account_asignment);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(account_asignment);
        }

        // GET: AccountAssignment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_account_assignment po_account_asignment = _accountAssignmentService.Get(id);
            if (po_account_asignment == null)
            {
                return HttpNotFound();
            }
            return View(po_account_asignment);
        }

        // POST: AccountAssignment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var isValid = _accountAssignmentService.Delete(id);
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
                _accountAssignmentService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
