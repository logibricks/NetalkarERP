using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class DepreciationrunController : Controller
    {
        private readonly IFrequencyService _frequency;
        private readonly IFinancialYearService _finance;
        private readonly IGenericService _Generic;
        private readonly IDepreciationRunService _deprun;

        public DepreciationrunController(IGenericService GenericService, IFrequencyService frequency, IFinancialYearService finance, IDepreciationRunService deprun)
        {

            _Generic = GenericService;
            _frequency = frequency;
            _finance = finance;
            _deprun = deprun;

        }

        // GET: Depreciationrun
        public ActionResult Index()
        {
            ViewBag.num = TempData["doc_num"];
           //ViewBag.DataSource = _DepreciationAreaService.GetAll();
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.frequencylist = new SelectList(_frequency.GetAll().Where(a => a.frequency_name != "Quarterly"), "frequency_id", "frequency_name");
            ViewBag.financiallist = new SelectList(_finance.GetAll().Where(a => a.FINANCIAL_YEAR_NAME != "FY2017-FY2018"), "FINANCIAL_YEAR_ID", "FINANCIAL_YEAR_NAME");
            ViewBag.dep_area_list = _Generic.GetDepList().Select(a => new { a.dep_area_id,  a.dep_area_code }).ToList();
            ViewBag.category_list = new SelectList(_Generic.GetCategoryListByModule("DEPRUN"), "document_numbring_id", "category");
            ViewBag.assetclassList = _Generic.GetAssetClass().Select(a => new { a.asset_class_id, asset_class_code = a.asset_class_code + "/" + a.asset_class_des }).ToList();
            ViewBag.depareadetails = _Generic.GetDepListForDepRun();
            return View();
        }

        public ActionResult Save(ref_depreciation_run_vm deprun)
        {         
                var issaved = _deprun.Add(deprun);
                if (issaved.Contains("Saved"))
                {

                    TempData["doc_num"] = issaved;
                    return Json(issaved, JsonRequestBehavior.AllowGet);
                }

            ViewBag.frequencylist = new SelectList(_frequency.GetAll().Where(a => a.frequency_name != "Quarterly"), "frequency_id", "frequency_name");
            ViewBag.financiallist = new SelectList(_finance.GetAll().Where(a => a.FINANCIAL_YEAR_NAME != "FY2017-FY2018"), "FINANCIAL_YEAR_ID", "FINANCIAL_YEAR_NAME");
            ViewBag.dep_area_list = _Generic.GetDepList().Select(a => new { a.dep_area_id, a.dep_area_code }).ToList();
            ViewBag.category_list = new SelectList(_Generic.GetCategoryListByModule("DEPRUN"), "document_numbring_id", "category");
            ViewBag.assetclassList = _Generic.GetAssetClass().Select(a => new { a.asset_class_id, asset_class_code = a.asset_class_code + "/" + a.asset_class_des }).ToList();
            ViewBag.depareadetails = _Generic.GetDepListForDepRun();
            return View();
        }
    }
}