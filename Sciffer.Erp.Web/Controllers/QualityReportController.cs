using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using System;
using Syncfusion.JavaScript.Models;
using Syncfusion.EJ.Export;
using System.Web.Script.Serialization;
using System.Collections;
using System.Collections.Generic;
using Syncfusion.XlsIO;
using System.Reflection;
using Newtonsoft.Json;
using Syncfusion.JavaScript;
using Sciffer.MovieScheduling.Web.Service;

namespace Sciffer.Erp.Web.Controllers
{
    public class QualityReportController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly IReportService _reportService;
        private readonly IPlantService _plantService;
        private readonly IItemService _itemService;
        private readonly IStatusService _statusService;
        private readonly IItemCategoryService _itemCategoryService;
        private readonly IReasonDeterminationService _reasonDeterminationService;
        public DateTime from_date { get; set; }
        public DateTime to_date { get; set; }
        public string plant_id { get; set; }
        public string document_type_code { get; set; }
        public string item_id { get; set; }
        public string status_id { get; set; }
        public string sloc_id { get; set; }
        public string entity { get; set; }
        public string reason_id { get; set; }
        public QualityReportController(IGenericService generic, IReportService reportService, IPlantService plantService, IItemService itemService, IStatusService statusService, IItemCategoryService ItemCategoryService, IReasonDeterminationService ReasonDeterminationService)
        {
            _Generic = generic;
            _reportService = reportService;
            _plantService = plantService;
            _itemService = itemService;
            _statusService = statusService;
            _itemCategoryService = ItemCategoryService;
            _reasonDeterminationService = ReasonDeterminationService;
        }

        public void ExportToExcel(string GridModel, string ctrlname)
        {
            ExcelExport exp = new ExcelExport();
            GridProperties obj = ConvertGridObject(GridModel, ctrlname);
            var DataSource = GetAllData(ctrlname);
            exp.Export(obj, DataSource, ctrlname + ".xlsx", ExcelVersion.Excel2010, false, false, "bootstrap-theme");
        }
        private GridProperties ConvertGridObject(string gridProperty, string ctrlname)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            IEnumerable div = (IEnumerable)serializer.Deserialize(gridProperty, typeof(IEnumerable));
            GridProperties gridProp = new GridProperties();
            foreach (KeyValuePair<string, object> ds in div)
            {
                var property = gridProp.GetType().GetProperty(ds.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (ds.Key == "entity")
                {
                    if (ds.Value != "")
                    {
                        entity = ds.Value.ToString();
                    }
                    continue;
                }
                if (ds.Key == "from_date")
                {
                    if (ds.Value != "")
                    {
                        from_date = DateTime.Parse(ds.Value.ToString());
                    }

                    continue;
                }
                if (ds.Key == "to_date")
                {
                    if (ds.Value != "")
                    {
                        to_date = DateTime.Parse(ds.Value.ToString());
                    }

                    continue;
                }
                if (ds.Key == "plant_id")
                {
                    if (ds.Value != "")
                    {
                        plant_id = ds.Value.ToString();
                    }

                    continue;
                }
                if (ds.Key == "document_type_code")
                {
                    if (ds.Value != "")
                    {
                        document_type_code = ds.Value.ToString();
                    }

                    continue;
                }
                if (ds.Key == "item_id")
                {
                    if (ds.Value != "")
                    {
                        item_id = ds.Value.ToString();
                    }

                    continue;
                }
                if (ds.Key == "status_id")
                {
                    if (ds.Value != "")
                    {
                        status_id = ds.Value.ToString();
                    }

                    continue;
                }
                if (ds.Key == "sloc_id")
                {
                    if (ds.Value != "")
                    {
                        sloc_id = ds.Value.ToString();
                    }

                    continue;
                }
                if (ds.Key == "reason_id")
                {
                    if (ds.Value != "")
                    {
                        reason_id = ds.Value.ToString();
                    }

                    continue;
                }

                if (property != null)
                {
                    Type type = property.PropertyType;
                    string serialize = serializer.Serialize(ds.Value);
                    object value = serializer.Deserialize(serialize, type);
                    property.SetValue(gridProp, value, null);
                }
            }
            return gridProp;
        }
        public object GetAllData(string controller)
        {
            object datasource = null;            
            datasource = _reportService.report_quality(entity, from_date, to_date, plant_id, document_type_code, item_id, status_id, sloc_id, reason_id);
            return datasource;
        }
        public ActionResult Get_Partial(string entity, DateTime? from_date, DateTime? to_date, string plant_id,
            string document_type_code, string item_id, string status_id, string sloc_id ,string partial_v,string reason_id)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            ViewBag.from_date = from_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(from_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.to_date = to_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(to_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.plant_id = plant_id;
            ViewBag.document_type_code = document_type_code;
            ViewBag.item_id = item_id;
            ViewBag.status_id = status_id;
            ViewBag.sloc_id = sloc_id;
            ViewBag.reason_id = reason_id;
            ViewBag.entity = entity;
            return PartialView(partial_v, ViewBag);
        }
        public ActionResult Get_report_quality(DataManager dm, string entity, DateTime? from_date, DateTime? to_date, string plant_id, string document_type_code, string item_id, string status_id, string sloc_id,string reason_id)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            var res = _reportService.report_quality(entity, from_date == null ? dte : from_date, to_date == null ? dte : to_date, plant_id, document_type_code, item_id, status_id, sloc_id, reason_id);
            ServerSideSearch sss = new ServerSideSearch();
            IEnumerable data = sss.ProcessDM(dm, res);
            return Json(new { result = data, count = res.Count }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult QCLotSummaryReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("QA"), "status_id", "status_name");
            return View();
        }
        public ActionResult QCParametersReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("QA"), "status_id", "status_name");
            return View();
        }
        public ActionResult QCUsageDecisionReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("QA"), "status_id", "status_name");
            return View();
        }
        public ActionResult QCLOTShelfLifeReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("QA"), "status_id", "status_name");
            return View();
        }
        public ActionResult ShelfLifeExpiredReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.sloc_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            return View();
        }
        public ActionResult ShelfLifeAbouttoExpireReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.sloc_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            return View();
        }
        public ActionResult MATERIAL_CONSUMED_POST_EXPIRYReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.reason_list = new SelectList(_reasonDeterminationService.GetAll(), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            return View();
        }
        public ActionResult ITEM_MASTERQCPARAMETERSReport()
        {
            return View();
        }
        public ActionResult BatchRevalidationCountReport()
        {
            return View();
        }
        public ActionResult BatchRevalidatedReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            return View();
        }
        // GET: QualityReport
        public ActionResult Index()
        {
            return View();
        }
    }
}





//using System.Web.Mvc;
//using Sciffer.Erp.Service.Interface;
//using System;
//using Syncfusion.JavaScript.Models;
//using Syncfusion.EJ.Export;
//using System.Web.Script.Serialization;
//using System.Collections;
//using System.Collections.Generic;
//using Syncfusion.XlsIO;
//using System.Reflection;
//using Newtonsoft.Json;
//using Syncfusion.JavaScript;
//using Sciffer.MovieScheduling.Web.Service;

//namespace Sciffer.Erp.Web.Controllers
//{
//    public class QualityReportController : Controller
//    {
//        private readonly IGenericService _Generic;
//        private readonly IReportService _reportService;
//        private readonly IPlantService _plantService;
//        private readonly IItemService _itemService;
//        private readonly IStatusService _statusService;
//        private readonly IItemCategoryService _itemCategoryService;
//        private readonly IReasonDeterminationService _reasonDeterminationService;
//        public DateTime from_date { get; set; }
//        public DateTime to_date { get; set; }
//        public string plant_id { get; set; }
//        public string document_type_code { get; set; }
//        public string item_id { get; set; }
//        public string status_id { get; set; }
//        public string sloc_id { get; set; }
//        public string entity { get; set; }
//        public QualityReportController(IGenericService generic, IReportService reportService, IPlantService plantService, IItemService itemService, IStatusService statusService, IItemCategoryService ItemCategoryService, IReasonDeterminationService ReasonDeterminationService)
//        {
//            _Generic = generic;
//            _reportService = reportService;
//            _plantService = plantService;
//            _itemService = itemService;
//            _statusService = statusService;
//            _itemCategoryService = ItemCategoryService;
//            _reasonDeterminationService = ReasonDeterminationService;
//        }

//        public void ExportToExcel(string GridModel, string ctrlname)
//        {
//            ExcelExport exp = new ExcelExport();
//            GridProperties obj = ConvertGridObject(GridModel, ctrlname);
//            var DataSource = GetAllData(ctrlname);
//            exp.Export(obj, DataSource, ctrlname + ".xlsx", ExcelVersion.Excel2010, false, false, "bootstrap-theme");
//        }
//        private GridProperties ConvertGridObject(string gridProperty, string ctrlname)
//        {
//            JavaScriptSerializer serializer = new JavaScriptSerializer();
//            IEnumerable div = (IEnumerable)serializer.Deserialize(gridProperty, typeof(IEnumerable));
//            GridProperties gridProp = new GridProperties();
//            foreach (KeyValuePair<string, object> ds in div)
//            {
//                var property = gridProp.GetType().GetProperty(ds.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);

//                if (ds.Key == "from_date")
//                {
//                    if (ds.Value != "")
//                    {
//                        from_date = DateTime.Parse(ds.Value.ToString());
//                    }

//                    continue;
//                }
//                if (ds.Key == "to_date")
//                {
//                    if (ds.Value != "")
//                    {
//                        to_date = DateTime.Parse(ds.Value.ToString());
//                    }

//                    continue;
//                }
//                if (ds.Key == "plant_id")
//                {
//                    if (ds.Value != "")
//                    {
//                        plant_id = ds.Value.ToString();
//                    }

//                    continue;
//                }
//                if (ds.Key == "document_type_code")
//                {
//                    if (ds.Value != "")
//                    {
//                        document_type_code = ds.Value.ToString();
//                    }

//                    continue;
//                }
//                if (ds.Key == "item_id")
//                {
//                    if (ds.Value != "")
//                    {
//                        item_id = ds.Value.ToString();
//                    }

//                    continue;
//                }
//                if (ds.Key == "status_id")
//                {
//                    if (ds.Value != "")
//                    {
//                        status_id = ds.Value.ToString();
//                    }

//                    continue;
//                }
//                if (ds.Key == "sloc_id")
//                {
//                    if (ds.Value != "")
//                    {
//                        sloc_id = ds.Value.ToString();
//                    }

//                    continue;
//                }

//                if (property != null)
//                {
//                    Type type = property.PropertyType;
//                    string serialize = serializer.Serialize(ds.Value);
//                    object value = serializer.Deserialize(serialize, type);
//                    property.SetValue(gridProp, value, null);
//                }
//            }
//            return gridProp;
//        }
//        public object GetAllData(string controller)
//        {
//            object datasource = null;
//            if (controller == "BatchRevalidatedReport")
//            {
//                entity = "get_Batch_Revalidated_Report";
//            }
//            if (controller == "BatchRevalidationCountReport")
//            {
//                entity = "get_Batch_Revalidation_Count_report";
//            }
//            if (controller == "ITEM_MASTERQCPARAMETERSReport")
//            {
//                entity = "get_ITEM_MASTER_QC_PARAMETERS";
//            }
//            if (controller == "MATERIAL_CONSUMED_POST_EXPIRYReport")
//            {
//                entity = "get_MATERIAL_CONSUMED_POST_EXPIRY";
//            }
//            if (controller == "QCLotSummaryReport")
//            {
//                entity = "get_QC_Lot_Summary_Report";
//            }
//            if (controller == "QCParametersReport")
//            {
//                entity = "get_QC_Parameters_Report";
//            }
//            if (controller == "QCUsageDecisionReport")
//            {
//                entity = "get_QC_Usage_Decision_Report";
//            }
//            if (controller == "ShelfLifeAbouttoExpireReport")
//            {
//                entity = "get_Shelf_Life_About_to_Expire_Report";
//            }
//            if (controller == "ShelfLifeExpiredReport")
//            {
//                entity = "get_Shelf_Life_Expired_Report";
//            }
//            datasource = _reportService.report_quality(entity, from_date, to_date, plant_id, document_type_code, item_id, status_id, sloc_id);
//            return datasource;
//        }
//        public ActionResult Get_Partial(string entity, DateTime? from_date, DateTime? to_date, string plant_id, string document_type_code, string item_id, string status_id, string sloc_id)
//        {
//            DateTime dte = new DateTime(1990, 1, 1);
//            ViewBag.from_date = from_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(from_date.ToString()).ToString("yyyy-MM-dd");
//            ViewBag.to_date = to_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(to_date.ToString()).ToString("yyyy-MM-dd");
//            ViewBag.plant_id = plant_id;
//            ViewBag.document_type_code = document_type_code;
//            ViewBag.item_id = item_id;
//            ViewBag.status_id = status_id;
//            ViewBag.sloc_id = sloc_id;
//            ViewBag.entity = entity;
//            if (entity == "get_QC_Lot_Summary_Report")
//            {
//                return PartialView("Partial_QCLotSummaryReport", ViewBag);
//            }
//            else if (entity == "get_QC_Usage_Decision_Report")
//            {
//                return PartialView("Partial_QCUsageDecisionReport", ViewBag);
//            }
//            else if (entity == "get_QC_LOT_Shelf_Life_Report")
//            {
//                return PartialView("Partial_QCLOTShelfLifeReport", ViewBag);
//            }
//            else if (entity == "get_Shelf_Life_Expired_Report")
//            {
//                return PartialView("Partial_ShelfLifeExpiredReport", ViewBag);
//            }
//            else if (entity == "get_Shelf_Life_About_to_Expire_Report")
//            {
//                return PartialView("Partial_ShelfLifeAbouttoExpireReport", ViewBag);
//            }
//            else if (entity == "get_MATERIAL_CONSUMED_POST_EXPIRY")
//            {
//                return PartialView("Partial_MATERIAL_CONSUMED_POST_EXPIRYReport", ViewBag);
//            }
//            else if (entity == "get_ITEM_MASTER_QC_PARAMETERS")
//            {
//                return PartialView("Partial_ITEM_MASTERQCPARAMETERSReport", ViewBag);
//            }
//            else if (entity == "get_Batch_Revalidated_Report")
//            {
//                return PartialView("Partial_BatchRevalidatedReport", ViewBag);
//            }
//            else if (entity == "get_Batch_Revalidation_Count_report")
//            {
//                return PartialView("Partial_BatchRevalidationCountReport", ViewBag);
//            }

//            else
//            {
//                return PartialView("Partial_QCParametersReport", ViewBag);
//            }
//        }
//        public ActionResult Get_report_quality(DataManager dm, string entity, DateTime? from_date, DateTime? to_date, string plant_id, string document_type_code, string item_id, string status_id, string sloc_id)
//        {
//            DateTime dte = new DateTime(1990, 1, 1);
//            var res = _reportService.report_quality(entity, from_date == null ? dte : from_date, to_date == null ? dte : to_date, plant_id, document_type_code, item_id, status_id, sloc_id);
//            ServerSideSearch sss = new ServerSideSearch();
//            IEnumerable data = sss.ProcessDM(dm, res);
//            return Json(new { result = data, count = res.Count }, JsonRequestBehavior.AllowGet);
//        }
//        public ActionResult QCLotSummaryReport()
//        {
//            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
//            ViewBag.status_list = new SelectList(_statusService.GetAll(), "status_id", "status_name");
//            return View();
//        }
//        public ActionResult QCParametersReport()
//        {
//            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
//            ViewBag.status_list = new SelectList(_statusService.GetAll(), "status_id", "status_name");
//            return View();
//        }
//        public ActionResult QCUsageDecisionReport()
//        {
//            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
//            ViewBag.status_list = new SelectList(_statusService.GetAll(), "status_id", "status_name");
//            return View();
//        }
//        public ActionResult QCLOTShelfLifeReport()
//        {
//            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
//            ViewBag.status_list = new SelectList(_statusService.GetAll(), "status_id", "status_name");
//            return View();
//        }
//        public ActionResult ShelfLifeExpiredReport()
//        {
//            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
//            ViewBag.sloc_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
//            return View();
//        }
//        public ActionResult ShelfLifeAbouttoExpireReport()
//        {
//            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
//            ViewBag.sloc_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
//            return View();
//        }
//        public ActionResult MATERIAL_CONSUMED_POST_EXPIRYReport()
//        {
//            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
//            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
//            ViewBag.reason_list = new SelectList(_reasonDeterminationService.GetAll(), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
//            return View();
//        }
//        public ActionResult ITEM_MASTERQCPARAMETERSReport()
//        {
//            return View();
//        }
//        public ActionResult BatchRevalidationCountReport()
//        {
//            return View();
//        }
//        public ActionResult BatchRevalidatedReport()
//        {
//            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
//            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
//            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
//            return View();
//        }
//        // GET: QualityReport
//        public ActionResult Index()
//        {
//            return View();
//        }
//    }
//}