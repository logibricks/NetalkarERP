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
    public class PartyTypeController : Controller
    {
        private readonly IPartyTypeService _partyTypeService;
        public PartyTypeController(IPartyTypeService PartyTypeService)
        {
            _partyTypeService = PartyTypeService;
        }

        // GET: ref_party_type
        public ActionResult Index()
        {
            return View(_partyTypeService.GetAll());
        }

        // GET: ref_party_type/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_party_type ref_party_type = _partyTypeService.Get(id);
            if (ref_party_type == null)
            {
                return HttpNotFound();
            }
            return View(ref_party_type);
        }

        // GET: ref_party_type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ref_party_type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_party_type ref_party_type)
        {
            if (ModelState.IsValid)
            {
                var isValid = _partyTypeService.Add(ref_party_type);
                if(isValid==true)
                { 
                   return RedirectToAction("Index");
                }
            }

            return View(ref_party_type);
        }

        // GET: ref_party_type/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_party_type ref_party_type = _partyTypeService.Get(id);
            if (ref_party_type == null)
            {
                return HttpNotFound();
            }
            return View(ref_party_type);
        }

        // POST: ref_party_type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_party_type ref_party_type)
        {
            if (ModelState.IsValid)
            {
                var isValid = _partyTypeService.Update(ref_party_type);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(ref_party_type);
        }

        // GET: ref_party_type/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_party_type ref_party_type = _partyTypeService.Get(id);
            if (ref_party_type == null)
            {
                return HttpNotFound();
            }
            return View(ref_party_type);
        }

        // POST: ref_party_type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var isValid = _partyTypeService.Delete(id);
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
                _partyTypeService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
