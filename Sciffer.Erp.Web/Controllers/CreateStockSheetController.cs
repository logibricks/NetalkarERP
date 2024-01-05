using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using Syncfusion.XlsIO;
using System;
using System.IO;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Sciffer.Erp.Web.Controllers
{
    public class CreateStockSheetController : Controller
    {
        private readonly IStockSheetService _stock;
        private readonly IPlantService _plant;
        private readonly IBucketService _bucket;
        private readonly IStorageLocation _storage;
        private readonly IStatusService _status;
        private readonly IGenericService _Generic;
        private readonly IDocumentNumbringService _doc;
        private readonly IUpdateStockSheetService _update;
        public CreateStockSheetController(IStockSheetService stock, IPlantService plant, IBucketService bucket, IStorageLocation storage, IStatusService status, IGenericService Generic,
            IDocumentNumbringService doc, IUpdateStockSheetService update)
        {
            _stock = stock;
            _plant = plant;
            _bucket = bucket;
            _storage = storage;
            _status = status;
            _Generic = Generic;
            _doc = doc;
            _update = update;
        }
        // GET: StockSheet
        public ActionResult Index()
        {
            ViewBag.num = TempData["doc_num"];
            return View();
        }

        // GET: Create
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.status = new SelectList(_Generic.GetStatusList("CRT_STOCK"), "status_id", "status_name");
            ViewBag.plant = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.bucket = new SelectList(_bucket.GetAll(), "bucket_id", "bucket_name");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryListByModule("CRT_STOCK"), "document_numbring_id", "category");
            ViewBag.item_category_list = new SelectList(_Generic.GetItemCategoryList(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            return View();
        }

        [HttpPost]
        public ActionResult Create(create_stock_sheet_vm vm)
        {
            if (ModelState.IsValid)
            {
                var isValid = _stock.Add(vm);
                if (isValid.Contains("Saved"))
                {
                    var doc_number = isValid.Split('~')[1];
                    TempData["doc_num"] = doc_number;
                    return RedirectToAction("Index", TempData["doc_num"]);
                }
                ViewBag.error = isValid;
            }
            ViewBag.status = new SelectList(_Generic.GetStatusList("CRT_STOCK"), "statis_id", "status_name");
            ViewBag.plant = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.bucket = new SelectList(_bucket.GetAll(), "bucket_id", "bucket_name");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryListByModule("CRT_STOCK"), "document_numbring_id", "category");
            ViewBag.item_category_list = new SelectList(_Generic.GetItemCategoryList(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            create_stock_sheet_vm vm = _stock.Get((int)id);
            if (vm == null)
            {
                return HttpNotFound();
            }
            ViewBag.status = new SelectList(_Generic.GetStatusList("CRT_STOCK"), "status_id", "status_name");
            ViewBag.plant = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.bucket = new SelectList(_bucket.GetAll(), "bucket_id", "bucket_name");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryListByModule("CRT_STOCK"), "document_numbring_id", "category");
            ViewBag.storage = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            ViewBag.item_category_list = new SelectList(_Generic.GetItemCategoryList(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            return View(vm);
        }

        public ActionResult Get_StockQuantity_Details(int id, string form)
        {
            var grater_than_zero1 = _stock.StockQuantity_Graterthan_Zero(id, form);
            var stock_less_is_null1 = _stock.StockQuantity_Lessthan_Zero(id, form);
            var equal_zero1 = _stock.StockQuantity_Equal_Zero(id, form);            

            object stock_detail1;
            if (form == "CRT_STOCK")
            {
                stock_detail1 = _stock.Get(id);
            }
            else
            {
                stock_detail1 = _update.Get(id);
            }

            return Json(new { grater_than_zero1, stock_less_is_null1, equal_zero1, stock_detail1 }, JsonRequestBehavior.AllowGet);
        }

        public void ExportToExcel(int create_stock_sheet_id)
        {
            var grater_than_zero = _stock.StockQuantity_Graterthan_Zero(create_stock_sheet_id, "CRT_STOCK");
            var less_than_zero = _stock.StockQuantity_Lessthan_Zero(create_stock_sheet_id, "CRT_STOCK");
            var equal_zero = _stock.StockQuantity_Equal_Zero(create_stock_sheet_id, "CRT_STOCK");
            var create_stock_sheet = _stock.create_stock_sheet(create_stock_sheet_id);

            ExcelPackage excel = new ExcelPackage();
            var workSheet1 = excel.Workbook.Worksheets.Add("Create Stock Sheet");
            var workSheet2 = excel.Workbook.Worksheets.Add("Stock Quantity Greater than Zero");
            var workSheet3 = excel.Workbook.Worksheets.Add("No Transaction");
            var workSheet4 = excel.Workbook.Worksheets.Add("Stock Quantity Equals to zero");


            //adding col to Excel work sheet 1
            //workSheet1.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet1.Row(2).Style.Font.Bold = true;
            workSheet1.Cells[1, 3].Value = "Create Stock Sheet";
            workSheet1.Cells[2, 3].Value = "Company Name";
            workSheet1.Cells[3, 3].Value = "Plant Name";
            workSheet1.Cells[4, 3].Value = "Storage Location Name";
            workSheet1.Cells[5, 3].Value = "Bucket Name";
            workSheet1.Cells[6, 3].Value = "Date";
            workSheet1.Cells[7, 3].Value = "Document Number";
            var doc_number = "";
            // putting data in first sheet
            foreach (var item in create_stock_sheet)
            {
                workSheet1.Cells[2, 4].Value = "Netalkar Power Transmisssion";
                workSheet1.Cells[3, 4].Value = item.plant_name;
                workSheet1.Cells[4, 4].Value = item.sloc_name;
                workSheet1.Cells[5, 4].Value = item.bucket_name;
                workSheet1.Cells[6, 4].Value = item.document_date1;
                workSheet1.Cells[7, 4].Value = item.document_no;
                doc_number = item.document_no;
            }


            //adding col to Excel work sheet 2
            workSheet2.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet2.Row(1).Style.Font.Bold = true;
            workSheet2.Cells[1, 1].Value = "S.No";
            workSheet2.Cells[1, 2].Value = "Item";
            workSheet2.Cells[1, 3].Value = "UOM";
            workSheet2.Cells[1, 4].Value = "Batch";
            workSheet2.Cells[1, 5].Value = "Quantity";

            //putting data into excel sheet 2
            int recordIndex1 = 2;
            foreach (var item in grater_than_zero)
            {
                workSheet2.Cells[recordIndex1, 1].Value = (recordIndex1 - 1).ToString();
                workSheet2.Cells[recordIndex1, 2].Value = item.item_code;
                workSheet2.Cells[recordIndex1, 3].Value = item.UOM;
                workSheet2.Cells[recordIndex1, 4].Value = item.batch_number;
                // workSheet2.Cells[recordIndex1, 5].Value = item.actual_qty;
                recordIndex1++;
            }



            //adding col to Excel work sheet 3
            workSheet3.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet3.Row(1).Style.Font.Bold = true;
            workSheet3.Cells[1, 1].Value = "S.No";
            workSheet3.Cells[1, 2].Value = "Item";
            workSheet3.Cells[1, 3].Value = "UOM";
            workSheet3.Cells[1, 4].Value = "Batch";
            workSheet3.Cells[1, 5].Value = "Quantity";

            //putting data into excel sheet 3
            int recordIndex2 = 2;
            foreach (var item in less_than_zero)
            {
                workSheet3.Cells[recordIndex2, 1].Value = (recordIndex2 - 1).ToString();
                workSheet3.Cells[recordIndex2, 2].Value = item.item_code;
                workSheet3.Cells[recordIndex2, 3].Value = item.UOM;
                workSheet3.Cells[recordIndex2, 4].Value = item.batch_number;
                //workSheet3.Cells[recordIndex2, 5].Value = item.actual_qty;
                recordIndex2++;
            }


            //adding col to Excel work sheet 4
            workSheet4.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet4.Row(1).Style.Font.Bold = true;
            workSheet4.Cells[1, 1].Value = "S.No";
            workSheet4.Cells[1, 2].Value = "Item";
            workSheet4.Cells[1, 3].Value = "UOM";
            workSheet4.Cells[1, 4].Value = "Batch";
            workSheet4.Cells[1, 5].Value = "Quantity";


            //putting data into excel sheet 4
            int recordIndex3 = 2;
            foreach (var item in equal_zero)
            {
                workSheet4.Cells[recordIndex3, 1].Value = (recordIndex2 - 1).ToString();
                workSheet4.Cells[recordIndex3, 2].Value = item.item_code;
                workSheet4.Cells[recordIndex3, 3].Value = item.UOM;
                workSheet4.Cells[recordIndex3, 4].Value = item.batch_number;
                //workSheet4.Cells[recordIndex3, 5].Value = item.actual_qty;
                recordIndex3++;
            }

            string excelName = "StockSheetFor" + doc_number + DateTime.Now;
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=" + excelName + ".xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }


        }
    }
}