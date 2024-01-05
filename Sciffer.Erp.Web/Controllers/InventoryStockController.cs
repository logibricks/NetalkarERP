using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System.Linq;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using Newtonsoft.Json;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class InventoryStockController : Controller
    {
        private readonly IGenericService _genericService;
        private readonly IPlantService _plantService;
        private readonly IStorageLocation _storageLocation;
        private readonly IBucketService _bucketService;
        private readonly IInventoryStockService _inventoryStockService;
        private readonly IItemService _itemService;

        public InventoryStockController(IGenericService GenericService, IPlantService PlantService, IStorageLocation StorageLocation
            , IBucketService BucketService, IInventoryStockService InventoryStockService,IItemService itemService)
        {
            _genericService = GenericService;
            _plantService = PlantService;
            _storageLocation = StorageLocation;
            _bucketService = BucketService;
            _inventoryStockService = InventoryStockService;
            _itemService = itemService;
        }
        // GET: InventoryStock
        public ActionResult Index()
        {

            ViewBag.num = TempData["data"];
            ViewBag.DataSource = _inventoryStockService.GetAll();
            return View();
        }

        // GET: InventoryStock/Create
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.category_list = new SelectList(_genericService.GetCategoryList(217), "document_numbring_id", "category");
            ViewBag.plant_list = new SelectList(_plantService.GetPlant(), "PLANT_ID", "PLANT_NAME");
            ViewBag.sloc_list = new SelectList(_storageLocation.GetAll(), "storage_location_id", "storage_location_name");
            ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            ViewBag.item_name = new SelectList(_genericService.GetItemList(), "ITEM_ID", "ITEM_NAME");
            return View();
        }

        // POST: InventoryStock/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(inv_Inventory_stock_vm inv_Inventory_stock)
        {
            if (ModelState.IsValid)
            {
                var issaved = _inventoryStockService.Add(inv_Inventory_stock);
                if (issaved != "Error")
                {
                    TempData["data"] = issaved;
                    return RedirectToAction("Index", TempData["data"]);
                }
                ViewBag.error = "Something went wrong!!";
            }


            ViewBag.category_list = new SelectList(_genericService.GetCategoryList(217), "document_numbring_id", "category");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.sloc_list = new SelectList(_storageLocation.GetAll(), "storage_location_id", "storage_location_name");
            ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            return View(inv_Inventory_stock);
        }

        // GET: InventoryStock/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inv_Inventory_stock_vm inv_Inventory_stock = _inventoryStockService.Get((int)id);
            if (inv_Inventory_stock == null)
            {
                return HttpNotFound();
            }

            ViewBag.category_list = new SelectList(_genericService.GetCategoryList(217), "document_numbring_id", "category");
            ViewBag.plant_list = new SelectList(_plantService.GetPlant(), "PLANT_ID", "PLANT_NAME");
            ViewBag.sloc_list = new SelectList(_storageLocation.GetAll(), "storage_location_id", "storage_location_name");
            ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            ViewBag.item_name = new SelectList(_genericService.GetItemList(), "ITEM_ID", "ITEM_NAME");

           
            return View(inv_Inventory_stock);
        }

        // POST: InventoryStock/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(inv_Inventory_stock_vm inv_Inventory_stock)
        {
            if (ModelState.IsValid)
            {
                var issaved = _inventoryStockService.Add(inv_Inventory_stock);
                if (issaved != "Error")
                {
                    TempData["data"] = issaved;
                    return RedirectToAction("Index", TempData["data"]);
                }
                ViewBag.error = "Something went wrong!!";
            }
            
            ViewBag.category_list = new SelectList(_genericService.GetCategoryList(218), "document_numbring_id", "category");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.sloc_list = new SelectList(_storageLocation.GetAll(), "storage_location_id", "storage_location_name");
            ViewBag.bucket_list = new SelectList(_bucketService.GetAll(), "bucket_id", "bucket_name");
            return View(inv_Inventory_stock);
        }
        public ActionResult GetItemForStock(int plant_id, int sloc_id, int bucket_id)
        {
            var it = _inventoryStockService.GetItemForStock((int)plant_id, (int)sloc_id, (int)bucket_id);

            return Json(it, JsonRequestBehavior.AllowGet);
        }


        public CrystalReportPdfResult Pdf(int id)
        {

            ReportDocument rd = new ReportDocument();
            try
            {
                var inv_stock = _inventoryStockService.Get((int)id);
                inv_stock.slocname = inv_stock.REF_STORAGE_LOCATION.description;
                inv_stock.plant_name = inv_stock.REF_PLANT.PLANT_NAME;
                inv_stock.bucket_name = inv_stock.bucket_id == 1 ? "Quality" : inv_stock.bucket_id == 2 ? "Free" : "Blocked";
                inv_stock.postingdate = DateTime.Parse(inv_stock.posting_date.ToString()).ToString("dd/MM/yyyy");
                // var inv_stock_detail = _inventoryStockService.GetItemForStockEdit((int) id);

                var inv_stock_detail = inv_stock.inv_Inventory_stock_detail_VM;
                //  inv_stock_detail



                DataSet ds = new DataSet("inv_stock");

                var inv = new List<inv_Inventory_stock_vm>();
                inv.Add(inv_stock);

                var dt1 = _genericService.ToDataTable(inv);
                var dt2 = _genericService.ToDataTable(inv_stock_detail);


                dt1.TableName = "inv_Inventory_stock";
                dt2.TableName = "inv_Inventory_stock_detail";

                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);


                rd.Load(Path.Combine(Server.MapPath("~/Reports/InventoryStockTakeReport.rpt")));
                rd.SetDataSource(ds);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string reportPath = Path.Combine(Server.MapPath("~/Reports"), "InventoryStockTakeReport.rpt");
                return new CrystalReportPdfResult(reportPath, rd);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                rd.Close();
                rd.Clone();
                rd.Dispose();
                rd = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

        }
    }
}
