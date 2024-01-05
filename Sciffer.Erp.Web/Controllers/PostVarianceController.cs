using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System.Net;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class PostVarianceController : Controller
    {
        private readonly IStockSheetService _stock;
        private readonly IPlantService _plant;
        private readonly IBucketService _bucket;
        private readonly IStorageLocation _storage;
        private readonly IStatusService _status;
        private readonly IGenericService _Generic;
        private readonly IDocumentNumbringService _doc;
        private readonly IPostVarianceService _post_variance;
        private readonly IUpdateStockSheetService _update_stock;
        public PostVarianceController(IStockSheetService stock, IPlantService plant, IBucketService bucket, IStorageLocation storage, 
            IStatusService status, IGenericService Generic, IPostVarianceService post_varianace, IUpdateStockSheetService update_stock,
            IDocumentNumbringService doc)
        {
            _stock = stock;
            _plant = plant;
            _bucket = bucket;
            _storage = storage;
            _status = status;
            _Generic = Generic;
            _doc = doc;
            _post_variance = post_varianace;
            _update_stock = update_stock;
        }
        // GET: PostVariance
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.status = new SelectList(_Generic.GetStatusList("POST_VARIENCE"), "status_id", "status_name");
            ViewBag.plant = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.bucket = new SelectList(_bucket.GetAll(), "bucket_id", "bucket_name");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryListByModule("POST_VARIENCE"), "document_numbring_id", "category");
            ViewBag.updateStockCountList = new SelectList(_update_stock.getall(), "update_stock_count_id", "doc_number");
            ViewBag.item_category_list = new SelectList(_Generic.GetItemCategoryList(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            return View();
        }
        [HttpPost]
        public ActionResult Create(post_variance_vm vm)
        {
            if (ModelState.IsValid)
            {
                var isValid = _post_variance.Add(vm);
                if (isValid.Contains("Saved"))
                {
                    var doc_number = isValid.Split('~')[1];
                    TempData["doc_num"] = doc_number;
                    return RedirectToAction("Index", TempData["doc_num"]);
                }
                ViewBag.error = "Something went wrong !";
            }

            ViewBag.error = "";
            ViewBag.status = new SelectList(_Generic.GetStatusList("POST_VARIENCE"), "status_id", "status_name");
            ViewBag.plant = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.bucket = new SelectList(_bucket.GetAll(), "bucket_id", "bucket_name");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryListByModule("POST_VARIENCE"), "document_numbring_id", "category");
            ViewBag.updateStockCountList = new SelectList(_update_stock.getall(), "update_stock_count_id", "doc_number");
            ViewBag.item_category_list = new SelectList(_Generic.GetItemCategoryList(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            post_variance_vm vm = _post_variance.Get((int)id);
            if (vm == null)
            {
                return HttpNotFound();
            }
            ViewBag.status = new SelectList(_Generic.GetStatusList("POST_VARIENCE"), "status_id", "status_name");
            ViewBag.plant = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.bucket = new SelectList(_bucket.GetAll(), "bucket_id", "bucket_name");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryListByModule("POST_VARIENCE"), "document_numbring_id", "category");
            ViewBag.updateStockCountList = new SelectList(_update_stock.getall(), "update_stock_count_id", "doc_number");
            ViewBag.storage = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            ViewBag.StockSheet = new SelectList(_stock.GetStockSheet(), "create_stock_sheet_id", "document_no");
            ViewBag.item_category_list = new SelectList(_Generic.GetItemCategoryList(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            return View(vm);
        }
    }
}