using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Web.CustomFilters;
using Sciffer.Erp.Domain.ViewModel;
using System.Net;

namespace Sciffer.Erp.Web.Controllers
{
    public class PermitTemplateController : Controller
    {
        private readonly IPermitTemplateService _countryService;
        private readonly IGenericService _Generic;
        public PermitTemplateController(IPermitTemplateService countryService, IGenericService gen)
        {
            _countryService = countryService;
            _Generic = gen;
        }


        // GET: Country
        //[CustomAuthorizeAttribute("CNTRY")]
        public ActionResult Index()
        {
            ViewBag.datasource = _countryService.GetAll();
            return View();
        }
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ref_permit_template_VM vm)
         {           
            if (ModelState.IsValid)
            {
                var isValid = _countryService.Add(vm);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }          
            return View(vm);
        }
        public ActionResult Details()
        {

            return View();
        }

        public ActionResult Edit(int id)
        {
            Ref_permit_template_VM journal_entry = _countryService.Get(id);

            if (journal_entry == null)
            {
                return HttpNotFound();
            }
            return View(journal_entry);
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ref_permit_template_VM vm)
        {

            if (ModelState.IsValid)
            {
                var isValid = _countryService.Add(vm);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(vm);
        }



        public ActionResult Update(Ref_permit_template_VM vm)
        {
            if (ModelState.IsValid)
            {
                var isValid = _countryService.Update(vm);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(vm);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ref_permit_template_VM ref_Machine = _countryService.Get((int) id);
            if (ref_Machine == null)
            {
                return HttpNotFound();
            }
            return View(ref_Machine);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            /* bool isdeleted = _countryService.Delete(id);
             if (isdeleted == true)
             {
                 return RedirectToAction("Index");
             }
            Ref_permit_template_VM ref_Machine = _countryService.Get((int) id);
            if (ref_Machine == null)
             {
                 return HttpNotFound();
             }
             return View(ref_Machine);*/
            return RedirectToAction("Index");

        }

        public ActionResult InlineDelete(int key)
        {
            var res = _countryService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _countryService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}