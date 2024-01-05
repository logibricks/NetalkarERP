using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Web.CustomFilters;
using Sciffer.Erp.Domain.ViewModel;
using System.Net;

namespace Sciffer.Erp.Web.Controllers
{
    public class DepreciationAreaController : Controller
    {
        private readonly IDepreciationAreaService _DepreciationAreaService;
        private readonly IGenericService _Generic;
        private readonly IFrequencyService _frequency;
        private readonly IFinancialYearService _finance;
        public DepreciationAreaController(IDepreciationAreaService DepreciationAreaService, IGenericService gen, IFrequencyService frequency, IFinancialYearService finance)
        {
            _DepreciationAreaService = DepreciationAreaService;
            _Generic = gen;
            _frequency = frequency;
            _finance = finance;
        }
        // GET: DepreciationArea
        public ActionResult Index()
        {
            ViewBag.num = TempData["doc_num"];
            ViewBag.DataSource = _DepreciationAreaService.GetAll();

            return View();
        }

        public ActionResult Create()
        {
            ViewBag.frequencylist = new SelectList(_frequency.GetAll().Where(a => a.frequency_name != "Quarterly"), "frequency_id", "frequency_name");
            ViewBag.financiallist = new SelectList(_finance.GetAll().Where(a => a.FINANCIAL_YEAR_NAME != "FY2017-FY2018"), "FINANCIAL_YEAR_ID", "FINANCIAL_YEAR_NAME");
            return View();
        }

        [HttpPost]
        public ActionResult Create(ref_dep_area_vm depdata, List<ref_dep_posting_period_vm> DepParaArr)
        {
            var add = _Generic.CheckDuplicate(depdata.dep_area_code, "", "", "DepreciationArea", depdata.dep_area_id);
            if (add == 0)
            {

                var issaved = _DepreciationAreaService.Add(depdata, DepParaArr);
                if (issaved.Contains("Saved"))
                {

                   // TempData["doc_num"] = issaved;
                    return Json(issaved, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                var msg = depdata.dep_area_code + " already exists!!";
                //TempData["doc_num"] = msg;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            ViewBag.machineList = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.machineList1 = _Generic.GetMachineList(0);
            ViewBag.assetclassList = _Generic.GetAssetClass().Select(a => new { a.asset_class_id, asset_class_code = a.asset_class_code + "/" + a.asset_class_des }).ToList();
            ViewBag.assetgroupList = _Generic.GetAssetGroup().Select(a => new { a.asset_group_id, asset_group_code = a.asset_group_code + "/" + a.asset_group_des }).ToList();
            ViewBag.statusList = _Generic.GetStatusList("ASSETMASDT").Select(a => new { a.status_id, a.status_name }).ToList();
            ViewBag.plantList = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.countryList = _Generic.GetCountryList().Select(a => new { a.COUNTRY_ID, a.COUNTRY_NAME }).ToList();
            ViewBag.priorityList = new SelectList(_Generic.GetPriorityByForm(4), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.businessunitList = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.vendorList = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.costcenterList = _Generic.GetCostCenter().Select(a => new { a.cost_center_id, cost_center_code = a.cost_center_code + "/" + a.cost_center_description }).ToList();
            return View();
        }


        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_dep_area_vm vm = _DepreciationAreaService.Get((int)id);
            if (vm == null)
            {
                return HttpNotFound();
            }
            ViewBag.frequencylist = new SelectList(_frequency.GetAll().Where(a => a.frequency_name != "Quarterly"), "frequency_id", "frequency_name");
            ViewBag.financiallist = new SelectList(_finance.GetAll().Where(a => a.FINANCIAL_YEAR_NAME != "FY2017-FY2018"), "FINANCIAL_YEAR_ID", "FINANCIAL_YEAR_NAME");
            return View(vm);
        }

    }
}