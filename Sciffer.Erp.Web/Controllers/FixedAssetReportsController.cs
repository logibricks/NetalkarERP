using Sciffer.Erp.Service.Interface;
using Sciffer.MovieScheduling.Web.Service;
using Syncfusion.JavaScript;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class FixedAssetReportsController : Controller
    {
        private readonly IPlantService _plantService;
        private readonly IItemService _itemService;
        private readonly IGenericService _Generic;
        private readonly IReportService _reportService;
        public FixedAssetReportsController(IPlantService plantService, IItemService itemService, IGenericService generic, IReportService reportService)
        {
            _plantService = plantService;
            _itemService = itemService;
            _Generic = generic;
            _reportService = reportService;
        }
        // GET: FixedAssetReports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Get_Partial(string entity,
        DateTime? from_date, DateTime? to_date,
        string depreciation_type, string asset_code, string depreciation_area, string asset_class, string asset_group, string partial_v)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            ViewBag.entity = entity;
            ViewBag.from_date = from_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(from_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.to_date = to_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(to_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.depreciation_type = partial_v;
            ViewBag.asset_code_id = asset_code;
            ViewBag.depreciation_area_id = depreciation_area;
            ViewBag.asset_class_id = asset_class;
            ViewBag.asset_group_id = asset_group;
            ViewBag.partial_v = partial_v;

            if (entity == "getFixAssetBlockReport")
            {
                if (partial_v == "Partial_FixedAssetReport")
                {
                    return PartialView("Partial_FixedAssetReport", ViewBag);
                }
                else
                {
                    return PartialView("Partial_FixedAssetBlockReport", ViewBag);
                }
            }
            else if (entity == "getAssetLedgerReport")
            {
                return PartialView("Partial_AssetLedger", ViewBag);
            }

            else if (entity == "getListofAdditionsReport")
            {
                return PartialView("Partial_ListofAdditions", ViewBag);
            }
            else if (entity == "getListofDeletionsReport")
            {
                return PartialView("Partial_ListofDeletions", ViewBag);
            }
            else
            {
                return PartialView(partial_v, ViewBag);
            }

        }

        public ActionResult FixedAssetReport(DataManager dm, string entity, DateTime? from_date, DateTime? to_date, string depreciation_type, string asset_code_id, string depreciation_area_id, string asset_class_id, string asset_group_id)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            var res = _reportService.FixedAssetReport(entity, from_date, to_date, depreciation_type,
                asset_code_id == "" ? "-1" : asset_code_id,
                depreciation_area_id == null ? "-1" : depreciation_area_id,
                asset_class_id == null ? "-1" : asset_class_id,
                asset_group_id == null ? "-1" : asset_group_id);
            ServerSideSearch sss = new ServerSideSearch();
            IEnumerable data = sss.ProcessDM(dm, res);
            return Json(new { result = data, count = res.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FixedAssetBlockReport()
        {
            return View();
        }

        public ActionResult AssetLedgerReport()
        {
            ViewBag.dep_area_list = new SelectList(_Generic.GetDepList(), "dep_area_id", "dep_area_code");
            ViewBag.asset_master_data_list = new SelectList(_Generic.GetAssetMasterData(), "asset_master_data_id", "asset_master_data_code");
            return View();
        }

        public ActionResult ListofAdditionsReport()
        {
            ViewBag.asset_class_list = new SelectList(_Generic.GetAssetClass(), "asset_class_id", "asset_class_des");
            ViewBag.asset_group_list = new SelectList(_Generic.GetAssetGroup(), "asset_group_id", "asset_group_des");
            return View();
        }

        public ActionResult ListofDeletionReport()
        {
            ViewBag.asset_class_list = new SelectList(_Generic.GetAssetClass(), "asset_class_id", "asset_class_des");
            ViewBag.asset_group_list = new SelectList(_Generic.GetAssetGroup(), "asset_group_id", "asset_group_des");
            return View();
        }


    }
}