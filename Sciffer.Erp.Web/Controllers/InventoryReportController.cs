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
using Syncfusion.JavaScript;
using System.Linq;
using Sciffer.MovieScheduling.Web.Service;
namespace Sciffer.Erp.Web.Controllers
{
    public class InventoryReportController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly IStorageLocation _storageLocation;
        private readonly IBucketService _bucketService;
        private readonly IReportService _reportService;
        private readonly IReasonDeterminationService _reasonDeterminationService;
        private readonly IItemCategoryService _ItemCategory;
        private readonly IPlantService _plantService;
        private readonly IItemService _itemService;
        private readonly IItemCategoryService _itemCategoryService;
        public string plant_id { get; set; }
        public string entity { get; set; }
        public DateTime? from_date { get; set; }
        public DateTime? to_date { get; set; }
        public string sloc_id { get; set; }
        public string bucket_id { get; set; }
        public string item_id { get; set; }
        public string item_category_id { get; set; }
        public string reason_code_id { get; set; }
        public string bucket_id1 { get; set; }
        public string sloc_id1 { get; set; }
        public string reason_id { get; set; }
        public string employee_id { get; set; }
        public InventoryReportController(IGenericService generic, IStorageLocation sloc, IBucketService bucket, IReportService reportService,
            IReasonDeterminationService reason, IItemCategoryService ItemCategory, IPlantService plantService, IItemService itemService, IItemCategoryService itemCategoryService)
        {
            _Generic = generic;
            _storageLocation = sloc;
            _bucketService = bucket;
            _reportService = reportService;
            _reasonDeterminationService = reason;
            _ItemCategory = ItemCategory;
            _plantService = plantService;
            _itemService = itemService;
            _itemCategoryService = itemCategoryService;
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
                if (ds.Key == "plant_id")
                {
                    if (ds.Value != "")
                    {
                        plant_id = ds.Value.ToString();
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
                if (ds.Key == "sloc_id")
                {
                    if (ds.Value != "")
                    {
                        sloc_id = ds.Value.ToString();
                    }

                    continue;
                }
                if (ds.Key == "bucket_id")
                {
                    if (ds.Value != "")
                    {
                        bucket_id = ds.Value.ToString();
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
                if (ds.Key == "item_category_id")
                {
                    if (ds.Value != "")
                    {
                        item_category_id = ds.Value.ToString();
                    }
                    continue;
                }

                if (ds.Key == "reason_code_id")
                {
                    if (ds.Value != "")
                    {
                        reason_code_id = ds.Value.ToString();
                    }
                    continue;
                }

                if (ds.Key == "bucket_id1")
                {
                    if (ds.Value != "")
                    {
                        bucket_id1 = ds.Value.ToString();
                    }
                    continue;
                }
                if (ds.Key == "sloc_id1")
                {
                    if (ds.Value != "")
                    {
                        sloc_id1 = ds.Value.ToString();
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
        public ActionResult Get_Partial(string entity, DateTime? from_date, DateTime? to_date, string plant_id,
            string sloc_id, string bucket_id, string item_id, string item_category_id, string reason_code_id,
            string sloc_id1, string bucket_id1, string reason_id, string partial_v)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            ViewBag.from_date = from_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(from_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.to_date = to_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(to_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.plant_id = plant_id;
            ViewBag.sloc_id = sloc_id;
            ViewBag.bucket_id = bucket_id;
            ViewBag.item_id = item_id;
            ViewBag.item_category_id = item_category_id;
            ViewBag.reason_code_id = reason_code_id;
            ViewBag.sloc_id1 = sloc_id1;
            ViewBag.bucket_id1 = bucket_id1;
            ViewBag.reason_id = reason_id;
            ViewBag.entity = entity;

            return PartialView(partial_v, ViewBag);

        }
        public object GetAllData(string controller)
        {
            object datasource = null;
            datasource = _reportService.InventoryReport(entity, from_date, to_date, plant_id, sloc_id, bucket_id, item_id,
                item_category_id, reason_code_id, sloc_id1, bucket_id1);
            return datasource;
        }
        public ActionResult GetInventoryReport(DataManager dm, string entity, DateTime? from_date, DateTime? to_date, string plant_id, string sloc_id, string bucket_id, string item_id, string item_category_id, string reason_code_id, string sloc_id1, string bucket_id1)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            var res = _reportService.InventoryReport(entity, from_date == null ? dte : from_date, to_date == null ? dte : to_date,
                plant_id == "" ? "-1" : plant_id, sloc_id == "" ? "-1" : sloc_id, bucket_id == "" ? "-1" : bucket_id, item_id == "" ? "-1" : item_id,
                item_category_id == "" ? "-1" : item_category_id, reason_code_id == "" ? "-1" : reason_code_id, sloc_id1 == "" ? "-1" : sloc_id1,
                bucket_id1 == "" ? "-1" : bucket_id1);
            ServerSideSearch sss = new ServerSideSearch();
            IEnumerable data = sss.ProcessDM(dm, res);
            return Json(new { result = data, count = res.Count() }, JsonRequestBehavior.AllowGet);

        }


        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ItemAccountingReport()
        {
            return View();
        }
        public ActionResult InventoryledgerDetailedReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
          //  ViewBag.sloc_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
           // ViewBag.item_list = new SelectList(_Generic.GetItemList().Where(x=>x.ITEM_ID==1).ToList(), "ITEM_ID", "ITEM_NAME"); //new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            return View();
        }
        public ActionResult StockRegister()
        {
            ViewBag.item_list = new SelectList(_Generic.GetJobWorkInItem(), "ITEM_ID", "ITEM_NAME");
            return View();
        }
        public ActionResult InventoryledgerSummaryReport()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            //ViewBag.sloc_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            //ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_CODE");
            //ViewBag.item_category_list = new SelectList(_itemCategoryService.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            //ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            return View();
        }
        public ActionResult InventoryCascadeReport()
        {
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            return View();
        }
        public ActionResult GoodsReceiptReport()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.item_category_list = new SelectList(_ItemCategory.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            ViewBag.reason_list = new SelectList(_reasonDeterminationService.GetReasonListByCode("GOODS_RECEIPT"), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            return View();
        }
        public ActionResult GoodsIssueReport()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.item_category_list = new SelectList(_ItemCategory.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            ViewBag.reason_list = new SelectList(_reasonDeterminationService.GetReasonListByCode("GOODS_ISSUE"), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            return View();
        }
        public ActionResult WithinPlanTrasferReport()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.item_category_list = new SelectList(_ItemCategory.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            ViewBag.reason_list = new SelectList(_reasonDeterminationService.GetReasonListByCode("GOODS_ISSUE"), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            return View();
        }
        public ActionResult InventoryRevalautionReport()
        {
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.item_category_list = new SelectList(_ItemCategory.GetAll(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            return View();
        }
        public ActionResult MaterialRequisitionReportStatusDetailedReport()
        {
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.employee_list = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            return View();
        }

        public ActionResult MaterialRequisitionReportStatusSummaryReport()
        {
            return View();
        }
        public ActionResult OpenMaterialRequisitionReport()
        {
            return View();
        }
        public ActionResult InventoryValuationReport()
        {
            return View();
        }
        public ActionResult InventoryCostingReport()
        {
            return View();
        }
        public ActionResult StockSummaryAsOnDateReport()
        {
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            return View();
        }

        public ActionResult InventoryReport(string entity, DateTime? from_date, DateTime? to_date, string plant_id, string sloc_id, string bucket_id, string item_id, string item_category_id, string reason_code_id, string sloc_id1, string bucket_id1)
        {
            var data = _reportService.InventoryReport(entity, from_date, to_date, plant_id, sloc_id, bucket_id, item_id, item_category_id, reason_code_id, sloc_id1, bucket_id1);
            ViewBag.datasource = data;
            if (entity == "getinventorycascadereport")
            {
                return PartialView("Partial_InventoryCascade", data);
            }
            else if (entity == "getinventoryledgersummary")
            {
                return PartialView("Partial_InventoryledgerSummary", data);
            }
            else if (entity == "getitemaccountingreport")
            {
                return PartialView("Partial_ItemAccountingReport", data);
            }
            else if (entity == "getgoodsreceiptdetail")
            {
                return PartialView("Partial_GoodsReceipt", data);
            }
            else if (entity == "getwithinplanttransferreport")
            {
                return PartialView("Partial_WithinPlanTransfer", data);
            }
            else if (entity == "getinventoryrevaluationreport")
            {
                return PartialView("Partial_InventoryRevalaution", data);
            }
            else if (entity == "getmrndetailreport")
            {
                return PartialView("Partial_MaterialRequisitionReportStatusDetailed", data);
            }
            else if (entity == "getmrnstatussummaryreport")
            {
                return PartialView("Partial_MaterialRequisitionReportStatusSummary", data);
            }
            else if (entity == "getopenmrnreport")
            {
                return PartialView("Partial_OpenMaterialRequisition", data);
            }
            else if (entity == "getinventoryvaluationreport")
            {
                return PartialView("Partial_InventoryValuation", data);
            }
            else if (entity == "getinventorycostingreport")
            {
                return PartialView("Partial_InventoryCosting", data);
            }
            else if (entity == "getgoodsreceiptdetail")
            {
                return PartialView("Partial_GoodsReceiptReport", data);
            }
            else if (entity == "getinventorycascadereport")
            {
                return PartialView("Partial_InventoryCascade", data);
            }
            else if (entity == "getstocksummaryasondate")
            {
                return PartialView("Partial_StockSummaryAsOnDateReport", data);
            }
            else
            {
                return PartialView("Partial_GoodsIssue", data);
            }

        }

        //MATERIAL IN OUT DETAIL REPORTS

        public ActionResult MaterialInDetailReport()
        {
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.employee_list = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            return View();
        }
        public ActionResult MaterialOutDetailReport()
        {
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.employee_list = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            return View();
        }
        public ActionResult MaterialOutInTrackerReport()
        {
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            return View();
        }
        public ActionResult OpenMaterialOutwardReport()
        {
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.employee_list = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            return View();
        }
        public ActionResult MaterialInOutDetailReport1(string entity, DateTime? from_date, DateTime? to_date, string plant_id, string item_id, string employee_id)
        {
            var data = _reportService.MaterialInOutDetailReport(entity, from_date, to_date, plant_id, item_id, employee_id);
            ViewBag.datasource = data;
            if (entity == "getmaterialindetailreport")
            {
                return PartialView("Partial_MaterialInDetailReport", data);
            }
            if (entity == "getmaterialoutintrackerreport")
            {
                return PartialView("Partial_MaterialOutInTrackerReport", data);
            }
            if (entity == "getmaterialoutwarddetailreport")
            {
                return PartialView("Partial_MaterialOutWardDetailReport", data);
            }
            else
            {
                return PartialView("Partial_MaterialOutDetailReport", data);
            }
        }

        public ActionResult MaterialInOutDetailReport(DataManager dm, string entity, DateTime? from_date, DateTime? to_date, string plant_id, string item_id, string employee_id)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            var res = _reportService.MaterialInOutDetailReport(entity, from_date == null ? dte : from_date, to_date == null ? dte : to_date, plant_id == "" ? "-1" : plant_id, item_id == "" ? "-1" : item_id, employee_id == "" ? "-1" : employee_id);
            ServerSideSearch sss = new ServerSideSearch();
            IEnumerable data = sss.ProcessDM(dm, res);
            return Json(new { result = data, count = res.Count() }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Get_PartialM(string entity, DateTime? from_date, DateTime? to_date, string plant_id, string item_id, string employee_id, string partial_v)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            ViewBag.from_date = from_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(from_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.to_date = to_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(to_date.ToString()).ToString("yyyy-MM-dd");
            ViewBag.plant_id = plant_id;
            ViewBag.item_id = item_id;
            ViewBag.employee_id = employee_id;
            ViewBag.reason_code_id = reason_code_id;
            ViewBag.entity = entity;

            return PartialView(partial_v, ViewBag);

        }

        public object GetAllDataForMIO(string controller)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            object datasource = null;
            datasource = _reportService.MaterialInOutDetailReport(entity, from_date == null ? dte : from_date, to_date == null ? dte : to_date, plant_id == "" ? "-1" : plant_id, item_id == "" ? "-1" : item_id, employee_id == "" ? "-1" : employee_id);
            return datasource;
        }

        public void ExportToExcelMIO(string GridModel, string ctrlname)
        {
            ExcelExport exp = new ExcelExport();
            GridProperties obj = ConvertGridObject(GridModel, ctrlname);
            var DataSource = GetAllDataForMIO(ctrlname);
            exp.Export(obj, DataSource, ctrlname + ".xlsx", ExcelVersion.Excel2010, false, false, "bootstrap-theme");
        }
        public ActionResult GetInventory1(DataManager dm, string entity, string item_id, string opt)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            var res = _reportService.InventoryReport1(entity, item_id == "" ? "-1" : item_id,
                opt == null ? "-1" : opt);
            ServerSideSearch sss = new ServerSideSearch();
            IEnumerable data = sss.ProcessDM(dm, res);
            return Json(new { result = data, count = res.Count() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult QualityControlParamReport()
        {
            ViewBag.item_list = new SelectList(_Generic.GetItemListRMCategorywise(), "ITEM_ID", "ITEM_NAME");
            ViewBag.opt = new SelectList(_reportService.GetOprnList(), "process_id", "process_name");
            return View();
        }

        public ActionResult OperatorQualityParamReport()
        {
            ViewBag.item_list = new SelectList(_Generic.GetItemListRMCategorywise(), "ITEM_ID", "ITEM_NAME");
            ViewBag.opt = new SelectList(_reportService.GetOprnList(), "process_id", "process_name");
            return View();
        }
        public void ExportToExcel3(string GridModel, string ctrlname)
        {
            ExcelExport exp3 = new ExcelExport();
            GridProperties obj = ConvertGridObject(GridModel, ctrlname);
            var DataSource = GetAllData3(ctrlname);
            exp3.Export(obj, DataSource, ctrlname + ".xlsx", ExcelVersion.Excel2010, false, false, "bootstrap-theme");
        }
        public object GetAllData3(string controller)
        {
            object datasource = null;
            if (controller == "Quality")
            {
                entity = "getwqualityvontrolReport";
            }
            if (controller == "Operator")
            {
                entity = "getOperatorQualityParamReport";
            }
            datasource = _reportService.InventoryReport1(entity, item_id, plant_id);
            return datasource;
        }
    }
}






