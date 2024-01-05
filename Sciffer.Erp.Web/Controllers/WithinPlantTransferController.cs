using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.ViewModel;
using System.Linq;
using Newtonsoft.Json;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class WithinPlantTransferController : Controller
    {
        private readonly IStorageLocation _storageLocation;
        private readonly IItemService _itemService;
        private readonly IUOMService _uOMService;
        private readonly IBatchService _batchService;
        private readonly IPlantTransferService _pla_transferService;
        private readonly ICategoryService _categoryService;
        private readonly IPlantService _plantService;
        private readonly IGenericService _Generic;
        private readonly IBucketService _bucketservices;

        public WithinPlantTransferController(IGenericService gen, IPlantService PlantService, ICategoryService CategoryService, IPlantTransferService pla_transferService, IBatchService BatchService, IUOMService UOMService, IStorageLocation StorageLocation, IItemService ItemService, IBucketService Bucketservices)
        {
            _storageLocation = StorageLocation;
            _itemService = ItemService;
            _uOMService = UOMService;
            _batchService = BatchService;
            _pla_transferService = pla_transferService;
            _categoryService = CategoryService;
            _plantService = PlantService;
            _Generic = gen;
            _bucketservices = Bucketservices;
        }
        [CustomAuthorizeAttribute("WPT")]
        // GET: PlantTransfer
        public ActionResult Index()
        {
            ViewBag.num = TempData["data"];
            ViewBag.DataSource = _pla_transferService.getall();
            return View();
        }
      
        //SHOW FILE
        public FileResult Result(int id)
        {
            var path = _pla_transferService.Get(id).pla_attachment;
            if (path != "No File")
            {
                var arr = path.Split('/');
                var filesname = arr.Last();
                byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                Response.AppendHeader("Content-Disposition", "inline; filename=" + filesname);
                return File(path, filesname);
            }
            return null;
        }
       
        // GET: PlantTransfer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pla_transferVM pla_transfer = _pla_transferService.GetDetails((int)id);
            if (pla_transfer == null)
            {
                return HttpNotFound();
            }          
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(82), "document_numbring_id", "category");          
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.Batch_list = new SelectList(_batchService.GetAll(), "BATCH_ID", "BATCH_NUMBER");
            ViewBag.Uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.Item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.send_loc = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            ViewBag.receive_loc = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            ViewBag.bucket_list = new SelectList(_bucketservices.GetAll(), "bucket_id", "bucket_name");
            return View(pla_transfer);
        }
        [CustomAuthorizeAttribute("WPT")]
        // GET: PlantTransfer/Create
        public ActionResult Create()
        {
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(82), "document_numbring_id", "category");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.Batch_list = new SelectList(_batchService.GetAll(), "BATCH_ID", "BATCH_NUMBER");
            ViewBag.Uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.Item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.send_loc = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            ViewBag.receive_loc = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            ViewBag.bucket_list = new SelectList(_bucketservices.GetAll(), "bucket_id", "bucket_name");
            ViewBag.error = "";
            return View();
        }
       
        // POST: PlantTransfer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(pla_transferVM pla_transfer)
        {
            if (ModelState.IsValid)
            {
                var issaved = _pla_transferService.Add(pla_transfer);
                if (issaved.Contains("Saved"))
                {
                    var sp = issaved.Split('~')[1];
                    TempData["doc_num"] = sp;
                    return RedirectToAction("Index");
                }
                ViewBag.error = "Something went wrong!!";
            }
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(82), "document_numbring_id", "category");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.Batch_list = new SelectList(_batchService.GetAll(), "BATCH_ID", "BATCH_NUMBER");
            ViewBag.Uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.Item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.send_loc = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            ViewBag.receive_loc = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            ViewBag.bucket_list = new SelectList(_bucketservices.GetAll(), "bucket_id", "bucket_name");
           // ViewBag.error = "";
            return View();
        }
        [CustomAuthorizeAttribute("WPT")]
        // GET: PlantTransfer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pla_transferVM pla_transfer = _pla_transferService.Get((int)id);
            if (pla_transfer == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(82), "document_numbring_id", "category");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.Batch_list = new SelectList(_batchService.GetAll(), "BATCH_ID", "BATCH_NUMBER");
            ViewBag.Uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.Item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.send_loc = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            ViewBag.receive_loc = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            ViewBag.bucket_list = new SelectList(_bucketservices.GetAll(), "bucket_id", "bucket_name");
            return View(pla_transfer);
        }

        // POST: PlantTransfer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        

        // GET: PlantTransfer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pla_transferVM pla_transfer = _pla_transferService.Get((int)id);
            if (pla_transfer == null)
            {
                return HttpNotFound();
            }
            return View(pla_transfer);
        }

        // POST: PlantTransfer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           bool isValid = _pla_transferService.Delete(id);
            if (isValid == true)
            {
                return RedirectToAction("Index");
            }
           
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _pla_transferService.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult getnontagitemforplanttransfer(int item_id, int plant_id, int sloc_id, int bucket_id)
        {
            var taxcalculation = _pla_transferService.getnontagitemforplanttransfer((int)item_id, (int)plant_id, (int)sloc_id, (int)bucket_id);

            return Json(taxcalculation, JsonRequestBehavior.AllowGet);
        }
        public ActionResult gettagitemforplanttransfer(int item_id, int plant_id, int sloc_id, int bucket_id)
        {
            var taxcalculation = _pla_transferService.gettagitemforplanttransfer((int)item_id, (int)plant_id, (int)sloc_id, (int)bucket_id);

            return Json(taxcalculation, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetItemsDetail(int item_id)
        {
            var batchcategory = _Generic.GetItemsDetail(item_id);
            var list = JsonConvert.SerializeObject(batchcategory,
            Formatting.None,
                      new JsonSerializerSettings()
                      {
                          ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                      });

            return Content(list, "application/json");
        }
    }
}
