using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Web.CustomFilters;
using Sciffer.Erp.Domain.ViewModel;
using System.Net;

namespace Sciffer.Erp.Web.Controllers
{
    public class IssuePermitController : Controller
    {
        private readonly IIssuePermitService _countryService;
        private readonly IGenericService _Generic;
        private readonly ICategoryService _Category;
        private readonly IPermitTemplateService _permit;

        public IssuePermitController(IIssuePermitService issue, IGenericService gen, ICategoryService Category, IPermitTemplateService permit)
        {
            _countryService = issue;
            _Generic = gen;
            _Category = Category;
            _permit = permit;
        }


        // GET: Country
        //[CustomAuthorizeAttribute("CNTRY")]
        public ActionResult Index()
        {
            ViewBag.num = TempData["data"];
            ViewBag.datasource = _countryService.GetAll();
            return View();
        }
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.categorylist= new SelectList(_Generic.GetCategoryList(228), "document_numbring_id", "category");
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.templist = new SelectList(_Generic.GetTempList(), "permit_template_id", "permit_template_no");
            ViewBag.categorypermit = new SelectList(_permit.GetAll(), "permit_template_id", "permit_category");
            ViewBag.checkpointlist = new SelectList(_Generic.GetCheckpointList(0), "permit_template_id", "checkpoints");
            ViewBag.idealScenariolist = new SelectList(_Generic.GetScenarioList(0), "check_point_id", "ideal_scenario");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_permit_issue_VM vm)
        {
            var issaved = "";
            if (ModelState.IsValid)
            {
                issaved = _countryService.Add(vm);
                if (issaved != "Error")
                {
                    TempData["data"] = issaved;
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = issaved;
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(228), "document_numbring_id", "category");
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.templist = new SelectList(_Generic.GetTempList(), "permit_template_id", "permit_template_no");
            ViewBag.categorypermit = new SelectList(_permit.GetAll(), "permit_template_id", "permit_category");
            ViewBag.checkpointlist = new SelectList(_Generic.GetCheckpointList(0), "check_point_id", "checkpoints");
            ViewBag.idealScenariolist = new SelectList(_Generic.GetScenarioList(0), "check_point_id", "ideal_scenario");
            return View(vm);
        }
        public ActionResult Edit(int id)
        {
            ref_permit_issue_VM jobworkin = _countryService.Get(id);

            if (jobworkin == null)
            {
                return HttpNotFound();
            }

            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(228), "document_numbring_id", "category");
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.templist = new SelectList(_Generic.GetTempList(), "permit_template_id", "permit_template_no");
            ViewBag.categorypermit = new SelectList(_permit.GetAll(), "permit_template_id", "permit_category");
            return View(jobworkin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_permit_issue_VM vm)
        {
            var issaved = "";
            if (ModelState.IsValid)
            {
                issaved = _countryService.Update(vm);
                if (issaved.Contains("Saved"))
                {
                    TempData["data"] = issaved;
                    return RedirectToAction("Index");
                }
            }
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(228), "document_numbring_id", "category");
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.templist = new SelectList(_Generic.GetTempList(), "permit_template_id", "permit_template_no");
            ViewBag.categorypermit = new SelectList(_permit.GetAll(), "permit_template_id", "permit_category");
            return View(vm);
        }
        public ActionResult Details()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ref_permit_issue_VM vm)
        {
            var issaved = "";
            if (ModelState.IsValid)
            {
                issaved = _countryService.Add(vm);
                if (issaved.Contains("Saved"))
                {
                    TempData["data"] = issaved;
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = issaved;
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(228), "document_numbring_id", "category");
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.templist = new SelectList(_Generic.GetTempList(), "permit_template_id", "permit_template_no");
            ViewBag.categorypermit = new SelectList(_permit.GetAll(), "permit_template_id", "permit_category");
            ViewBag.checkpointlist = new SelectList(_Generic.GetCheckpointList(0), "check_point_id", "checkpoints");
            ViewBag.idealScenariolist = new SelectList(_Generic.GetScenarioList(0), "check_point_id", "ideal_scenario");
            return View(vm);

         
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_permit_issue_VM ref_Machine = _countryService.Get((int) id);
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
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _countryService.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}