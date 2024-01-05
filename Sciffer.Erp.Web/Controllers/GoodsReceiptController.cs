using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using Newtonsoft.Json;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class GoodsReceiptController : Controller
    {
        private readonly IGoodsReceiptService _goodsReceiptService;
        private readonly IItemService _itemService;
        private readonly IUOMService _UOMService;
        private readonly IGeneralLedgerService _generalLedgerService;
        private readonly ICategoryService _categoryService;
        private readonly IPlantService _plantService;
        private readonly IStorageLocation _storageLocationService;
        private readonly IReasonDeterminationService _reasonDeterminationService;
        private readonly IGenericService _Generic;
        private readonly IBatchService _batchService;
        private readonly IBucketService _bucketService;
        private readonly IGrnService _grnService;
        private readonly IMachineMasterService _machiService;
        public GoodsReceiptController(IMachineMasterService machineService, IGrnService grnservice, IBucketService bucket, IBatchService batch, IGenericService gen, IGoodsReceiptService GoodsReceiptService , IItemService ItemService, IUOMService UOMService, IGeneralLedgerService GeneralLedgerService, ICategoryService CategoryService, IPlantService PlantService, IStorageLocation StorageLocationService, IReasonDeterminationService ReasonDeterminationService)
        {
            _goodsReceiptService = GoodsReceiptService;
            _itemService = ItemService;
            _UOMService = UOMService;
            _generalLedgerService = GeneralLedgerService;
            _categoryService = CategoryService;
            _plantService = PlantService;
            _storageLocationService = StorageLocationService;
            _reasonDeterminationService = ReasonDeterminationService;
            _Generic = gen;
            _batchService = batch;
            _bucketService = bucket;
            _grnService = grnservice;
            _machiService = machineService;
        }
        [CustomAuthorizeAttribute("GR")]
        public ActionResult Index()
        {
            ViewBag.num = TempData["data"];
            ViewBag.DataSource = _goodsReceiptService.getall();
            return View();
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GOODS_RECEIPT_VM gOODS_RECEIPT_VM = _goodsReceiptService.GetDetails((int)id);
            if (gOODS_RECEIPT_VM == null)
            {
                return HttpNotFound();
            }

            var u = _UOMService.GetAll();
            var c = _categoryService.GetAll();
            var r = _reasonDeterminationService.GetReasonListByCode("GOODS_RECEIPT");
            var ba = _batchService.GetAll();
            var bu = _bucketService.GetAll();
            ViewBag.reasonlist = new SelectList(r, "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.glcodelist = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(79), "document_numbring_id", "category");
            ViewBag.unitlist = new SelectList(u, "UOM_ID", "UOM_NAME");
            ViewBag.batchlist = new SelectList(_batchService.GetAll(), "item_batch_id", "batch_number");
            ViewBag.bucketlist = new SelectList(bu, "bucket_id", "bucket_name");
            //ViewBag.grnList = new SelectList(_grnService.GetGrnList(), "grn_id", "grn_number");
            ViewBag.machineList = new SelectList(_machiService.GetAll(), "machine_id", "machine_name");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("GR"), "status_id", "status_name");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(79), "cancellation_reason_id", "cancellation_reason");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");

            return View(gOODS_RECEIPT_VM);
        }

        [CustomAuthorizeAttribute("GR")]
        public ActionResult Create()
        {
            var u = _UOMService.GetAll();
            var c = _categoryService.GetAll();
            var r = _reasonDeterminationService.GetReasonListByCode("GOODS_RECEIPT");
            ViewBag.error = "";
            var bu = _bucketService.GetAll();
            ViewBag.reasonlist = new SelectList(r, "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");          
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.glcodelist = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(79), "document_numbring_id", "category");
            ViewBag.unitlist = new SelectList(u, "UOM_ID", "UOM_NAME");
            ViewBag.batchlist = new SelectList(_batchService.GetAll(), "item_batch_id", "batch_number");
            ViewBag.bucketlist = new SelectList(bu, "bucket_id", "bucket_name");
           // ViewBag.grnList = new SelectList(_grnService.GetGrnList(), "grn_id", "grn_number");
            ViewBag.machineList = new SelectList(_machiService.GetAll(), "machine_id", "machine_name");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(79), "cancellation_reason_id", "cancellation_reason");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("GR"), "status_id", "status_name");

            return View();
        }

        // POST:
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GOODS_RECEIPT_VM gOODS_RECEIPT_VM)
        {
            var issaved = "";
            if (ModelState.IsValid)
            {
                issaved = _goodsReceiptService.Add(gOODS_RECEIPT_VM);
                if (issaved.Contains("Saved"))
                {
                    TempData["data"] = issaved.Split('~')[1];
                    return RedirectToAction("Index");
                }               
            }
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    var er = error.ErrorMessage;
                }
            }
            ViewBag.error = issaved;
            var u = _UOMService.GetAll();
            var c = _categoryService.GetAll();
            var r = _reasonDeterminationService.GetReasonListByCode("GOODS_RECEIPT");
            var ba = _batchService.GetAll();
            var bu = _bucketService.GetAll();
            ViewBag.reasonlist = new SelectList(r, "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.glcodelist = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(79), "document_numbring_id", "category");
            ViewBag.unitlist = new SelectList(u, "UOM_ID", "UOM_NAME");
            ViewBag.batchlist = new SelectList(_batchService.GetAll(), "item_batch_id", "batch_number");
            ViewBag.bucketlist = new SelectList(bu, "bucket_id", "bucket_name");
            ViewBag.machineList = new SelectList(_machiService.GetAll(), "machine_id", "machine_name");
            //ViewBag.grnList = new SelectList(_grnService.GetGrnList(), "grn_id", "grn_number");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(79), "cancellation_reason_id", "cancellation_reason");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("GR"), "status_id", "status_name");

            return View(gOODS_RECEIPT_VM);
        }

        // GET:
        [CustomAuthorizeAttribute("GR")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GOODS_RECEIPT_VM gOODS_RECEIPT_VM = _goodsReceiptService.Get((int)id);
            if (gOODS_RECEIPT_VM == null)
            {
                return HttpNotFound();
            }

            var u = _UOMService.GetAll();
            var c = _categoryService.GetAll();
            var r = _reasonDeterminationService.GetReasonListByCode("GOODS_RECEIPT");
            var ba = _batchService.GetAll();
            var bu = _bucketService.GetAll();
            ViewBag.reasonlist = new SelectList(r, "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.glcodelist = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(79), "document_numbring_id", "category");
            ViewBag.unitlist = new SelectList(u, "UOM_ID", "UOM_NAME");
            ViewBag.batchlist = new SelectList(_batchService.GetAll(), "item_batch_id", "batch_number");
            ViewBag.bucketlist = new SelectList(bu, "bucket_id", "bucket_name");
            //ViewBag.grnList = new SelectList(_grnService.GetGrnList(), "grn_id", "grn_number");
            ViewBag.machineList = new SelectList(_machiService.GetAll(), "machine_id", "machine_name");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(79), "cancellation_reason_id", "cancellation_reason");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("GR"), "status_id", "status_name");

            return View(gOODS_RECEIPT_VM);
        }



        // POST: 
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GOODS_RECEIPT_VM gOODS_RECEIPT_VM)
        {
            if (ModelState.IsValid)
            {
                var isedited = _goodsReceiptService.Update(gOODS_RECEIPT_VM);
                if (isedited == true)
                    return RedirectToAction("Index");
            }

            var u = _UOMService.GetAll();
            var c = _categoryService.GetAll();
            var r = _reasonDeterminationService.GetReasonListByCode("GOODS_RECEIPT");
            //var ba = _batchService.GetAll();
            var bu = _bucketService.GetAll();
            ViewBag.reasonlist = new SelectList(r, "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.glcodelist = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(79), "document_numbring_id", "category");
            ViewBag.unitlist = new SelectList(u, "UOM_ID", "UOM_NAME");
            ViewBag.batchlist = new SelectList(_batchService.GetAll(), "item_batch_id", "batch_number");
            ViewBag.bucketlist = new SelectList(bu, "bucket_id", "bucket_name");
            //ViewBag.grnList = new SelectList(_grnService.GetGrnList(), "grn_id", "grn_number");
            ViewBag.machineList = new SelectList(_machiService.GetAll(), "machine_id", "machine_name");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(79), "cancellation_reason_id", "cancellation_reason");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("GR"), "status_id", "status_name");

            return View(gOODS_RECEIPT_VM);
        }

        // GET:
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GOODS_RECEIPT_VM gOODS_RECEIPT = _goodsReceiptService.Get((int)id);
            if (gOODS_RECEIPT == null)
            {
                return HttpNotFound();
            }
            return View(gOODS_RECEIPT);
        }

        // POST: 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bool isdeleted = _goodsReceiptService.Delete(id);
            if (isdeleted == true)
            {
                return RedirectToAction("Index");
            }
            GOODS_RECEIPT_VM gOODS_RECEIPT = _goodsReceiptService.Get((int)id);
            if (gOODS_RECEIPT == null)
            {
                return HttpNotFound();
            }
            return View(gOODS_RECEIPT);

        }

        public ActionResult DeleteConfirmed(int id, string cancellation_remarks, int reason_id)
        {
            var isValid = _goodsReceiptService.Delete(id, cancellation_remarks, reason_id);
            if (isValid.Contains("Cancelled"))
            {
                return Json(isValid);
            }
            return Json(isValid);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _goodsReceiptService.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult BatchList(int item_id , int plant_id)
        {
            var val = _batchService.GetBatchNumberUsingPlant(item_id, plant_id);
            //var val = _batchService.GetBatchNumber(item_id);
            var list = JsonConvert.SerializeObject(val,
              Formatting.None,
                       new JsonSerializerSettings()
                       {
                           ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                       });

            return Content(list, "application/json");
        }

        public ActionResult GetItemDetails(int item_id)
        {
            var batchcategory = _itemService.GetItemsDetail(item_id);
            var list = JsonConvert.SerializeObject(batchcategory,
            Formatting.None,
                      new JsonSerializerSettings()
                      {
                          ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                      });

            return Content(list, "application/json");
        }
        public ActionResult GetMAP(int item_id, int plant_id)
        {
            double x = _goodsReceiptService.GetMAP(item_id, plant_id);
            return Json(x, JsonRequestBehavior.AllowGet);
        }
        
    }
}
