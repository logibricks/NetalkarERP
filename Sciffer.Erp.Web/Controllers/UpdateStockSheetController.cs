using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Sciffer.Erp.Web.Controllers
{
    public class UpdateStockSheetController : Controller
    {
        private readonly IUpdateStockSheetService _stock;
        private readonly IStockSheetService _create;
        private readonly IPlantService _plant;
        private readonly IBucketService _bucket;
        private readonly IStorageLocation _storage;
        private readonly IStatusService _status;
        private readonly IGenericService _Generic;
        private readonly IDocumentNumbringService _doc;
        public UpdateStockSheetController(IUpdateStockSheetService stock, IPlantService plant, IBucketService bucket, IStorageLocation storage, IStatusService status, IGenericService Generic,
            IDocumentNumbringService doc, IStockSheetService create, IUpdateStockSheetService update)
        {
            _stock = stock;
            _plant = plant;
            _bucket = bucket;
            _storage = storage;
            _status = status;
            _Generic = Generic;
            _doc = doc;
            _create = create;
        }

        // GET: UpdateStockSheet
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.status = new SelectList(_Generic.GetStatusList("UPT_STOCK"), "status_id", "status_name");
            ViewBag.plant = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.bucket = new SelectList(_bucket.GetAll(), "bucket_id", "bucket_name");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryListByModule("UPT_STOCK"), "document_numbring_id", "category");
            ViewBag.StockSheet = new SelectList(_create.GetStockSheet(), "create_stock_sheet_id", "document_no");
            ViewBag.item_category_list = new SelectList(_Generic.GetItemCategoryList(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            return View();
        }

        [HttpPost]
        public ActionResult Create(update_stock_count_vm vm)
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
                ViewBag.error = "Something went wrong !";
            }

            ViewBag.error = "";
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

            update_stock_count_vm vm = _stock.Get((int)id);
            if (vm == null)
            {
                return HttpNotFound();
            }
            ViewBag.status = new SelectList(_Generic.GetStatusList("UPT_STOCK"), "status_id", "status_name");
            ViewBag.plant = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.bucket = new SelectList(_bucket.GetAll(), "bucket_id", "bucket_name");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryListByModule("UPT_STOCK"), "document_numbring_id", "category");
            ViewBag.storage = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            ViewBag.StockSheet = new SelectList(_create.GetStockSheet(), "create_stock_sheet_id", "document_no");
            ViewBag.item_category_list = new SelectList(_Generic.GetItemCategoryList(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            return View(vm);
        }
        
        public ActionResult GetUpdateStockDocList(int id)
        {
            var data = _Generic.GetUpdateStockDocList(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateStockSheetDetails(int id)
        {
            var data = _stock.Get(id);
            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            var data1 = new ContentResult
            {
                Content = serializer.Serialize(data),
                ContentType = "application/json"
            };
            return data1;
        }
    }
}