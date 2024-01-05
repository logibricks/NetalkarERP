using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using Newtonsoft.Json;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class ProductionReceiptController : Controller
    {
        private readonly IProductionReceiptService _receipt;
        private readonly IPlantService _plantService;
        private readonly IGenericService _Generic;
        private readonly IUOMService _uom;
        private readonly IProductionService _prodService;
        private readonly ITagService _tag;
        private readonly IBatchService _batch;
        private readonly IStorageLocation _sloc;
        public ProductionReceiptController(IProductionService prodService, IProductionReceiptService receipt,IPlantService PlantService, 
            IGenericService gen, IUOMService uom , ITagService tag, IBatchService batch, IStorageLocation sloc)
        {
            _receipt = receipt;
            _plantService = PlantService;
            _Generic = gen;
            _uom = uom;
            _prodService = prodService;
            _tag = tag;
            _batch = batch;
            _sloc = sloc;
        }
        [CustomAuthorizeAttribute("PRDR")]
        // GET: PlanMaintenance
        public ActionResult Index()
        {
            ViewBag.num = TempData["data"];
            ViewBag.DataSource = _receipt.GetAll();
            return View();
        }

        // GET: PlanMaintenance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            prod_receipt_VM prod_receipt_VM = _receipt.GetDetails((int)id);
            if (prod_receipt_VM == null)
            {
                return HttpNotFound();
            }
            ViewBag.plantlist = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(97), "document_numbring_id", "category");
            //ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.ProdOrdList = new SelectList(_prodService.GetActivePOListForReceipt(), "prod_order_id", "prod_order_no");
           // ViewBag.tagList = new SelectList(_tag.GetAll(), "tag_id", "tag_no");
            ViewBag.UOM = _uom.GetAll();
            //ViewBag.tagList = new SelectList(_tag.GetAll(), "tag_id", "tag_no");
            //ViewBag.batchList = new SelectList(_batch.GetAll(), "item_batch_id", "batch_number");
            ViewBag.StorageList = new SelectList(_sloc.getstoragelist(), "storage_location_id", "storage_location_name");
            return View(prod_receipt_VM);
        }
        public JsonResult GetProductionOrderList(int item_id, string entity_id)
        {
            var data = new SelectList(_prodService.GetProductionOrderList(item_id, entity_id), "prod_order_id", "prod_order_no");
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorizeAttribute("PRDR")]
        // GET: PlanMaintenance/Create
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.plantlist = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(97), "document_numbring_id", "category");
            ViewBag.itemlist = new SelectList(_Generic.GetCatWiesItemList(2), "ITEM_ID", "ITEM_NAME");
            ViewBag.ProdOrdList = new SelectList(_prodService.GetActivePOListForReceipt(), "prod_order_id", "prod_order_no");
            //ViewBag.tagList = new SelectList(_tag.GetAll(), "tag_id", "tag_no");
            //ViewBag.batchList = new SelectList(_batch.GetAll(), "item_batch_id", "batch_number");
            ViewBag.UOM = _uom.GetAll();
            return View();
        }

        // POST: PlanMaintenance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(prod_receipt_VM prod_Receipt_VM)
        {
            if (ModelState.IsValid)
            {
                var issaved = _receipt.Add(prod_Receipt_VM);
                if (issaved != null)
                {
                    if (issaved.Contains("Saved"))
                    {
                        var sp = issaved.Split('~')[1];
                        TempData["data"] = sp;
                        return RedirectToAction("Index");
                    }
                }
                ViewBag.error = issaved;
            }
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    var er = error.ErrorMessage;
                }
            }
            ViewBag.plantlist = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(97), "document_numbring_id", "category");
            ViewBag.itemlist = new SelectList(_Generic.GetCatWiesItemList(2), "ITEM_ID", "ITEM_NAME");
            ViewBag.ProdOrdList = new SelectList(_prodService.GetActivePOListForReceipt(), "prod_order_id", "prod_order_no");
            ViewBag.UOM = _uom.GetAll();
            //ViewBag.tagList = new SelectList(_tag.GetAll(), "tag_id", "tag_no");
            //ViewBag.batchList = new SelectList(_batch.GetAll(), "item_batch_id", "batch_number");
            return View();

        }
        [CustomAuthorizeAttribute("PRDR")]
        //GET: PlanMaintenance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            prod_receipt_VM prod_receipt_VM = _receipt.Get((int)id);
            if (prod_receipt_VM == null)
            {
                return HttpNotFound();
            }
            ViewBag.plantlist = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(97), "document_numbring_id", "category");
            ViewBag.itemlist = new SelectList(_Generic.GetCatWiesItemList(2), "ITEM_ID", "ITEM_NAME");
            ViewBag.ProdOrdList = new SelectList(_prodService.GetActivePOListForReceipt(), "prod_order_id", "prod_order_no");
            //ViewBag.tagList = new SelectList(_tag.GetAll(), "tag_id", "tag_no");
            ViewBag.UOM = _uom.GetAll();
            //ViewBag.tagList = new SelectList(_tag.GetAll(), "tag_id", "tag_no");
            //ViewBag.batchList = new SelectList(_batch.GetAll(), "item_batch_id", "batch_number");
            //ViewBag.StorageList = new SelectList(_sloc.getstoragelist(), "storage_location_id", "storage_location_name");
            return View(prod_receipt_VM);
        }

        // POST: PlanMaintenance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(prod_receipt_VM prod_Receipt_VM)
        {
            if (ModelState.IsValid)
            {
                var issaved = _receipt.Add(prod_Receipt_VM);
                if (issaved != "Error")
                {
                    TempData["data"] = issaved;
                    return RedirectToAction("Index", TempData["data"]);
                }
                ViewBag.error = "Something went wrong !";
            }
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    var er = error.ErrorMessage;
                }
            }
            ViewBag.plantlist = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(97), "document_numbring_id", "category");
            ViewBag.itemlist = new SelectList(_Generic.GetCatWiesItemList(2), "ITEM_ID", "ITEM_NAME");
            ViewBag.ProdOrdList = new SelectList(_prodService.GetActivePOListForReceipt(), "prod_order_id", "prod_order_no");
            ViewBag.UOM = _uom.GetAll();
            //ViewBag.tagList = new SelectList(_tag.GetAll(), "tag_id", "tag_no");
            //ViewBag.batchList = new SelectList(_batch.GetAll(), "item_batch_id", "batch_number");
            return View();
        }

        // POST: PlanMaintenance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bool isdeleted = _receipt.Delete(id);
            if (isdeleted == true)
            {
                return RedirectToAction("Index");
            }
            prod_receipt_VM ref_plan = _receipt.Get((int)id);
            if (ref_plan == null)
            {
                return HttpNotFound();
            }
            return View(ref_plan);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _receipt.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult ProductionOrderItems(string id)
        {
            var vm = _receipt.GetProductionOrderItems(id);
            var list = JsonConvert.SerializeObject(vm,
             Formatting.None,
                      new JsonSerializerSettings()
                      {
                          ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                      });

            return Content(list, "application/json");
        }
    }

}
