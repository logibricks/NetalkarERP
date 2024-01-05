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
    public class InterPlantTransferController : Controller
    {
        private readonly IStorageLocation _storageLocation;
        private readonly IItemService _itemService;
        private readonly IUOMService _uOMService;
        private readonly IBatchService _batchService;
        private readonly ICategoryService _categoryService;
        //private readonly IPlantService _plantService;
        private readonly IGenericService _Generic;
        private readonly IBucketService _bucketservices;
        private readonly IInterPlantService _intra_plant_service; 

        public InterPlantTransferController(IGenericService gen, IPlantService PlantService, ICategoryService CategoryService,
             IBatchService BatchService, IUOMService UOMService, IStorageLocation StorageLocation, IItemService ItemService,
             IBucketService Bucketservices, IInterPlantService intra_plant_service)
        {
            _storageLocation = StorageLocation;
            _itemService = ItemService;
            _uOMService = UOMService;
            _batchService = BatchService;
            _categoryService = CategoryService;
            //_plantService = PlantService;
            _Generic = gen;
            _bucketservices = Bucketservices;
            _intra_plant_service = intra_plant_service;
        }

        // GET: PlantTransfer
        [CustomAuthorizeAttribute("WPT")]
        public ActionResult Index()
        {
            ViewBag.num = TempData["data"];
            ViewBag.DataSource = _intra_plant_service.getall();
            return View();
        }

        //SHOW FILE
        public FileResult Result(int id)
        {
            var path = ""; //_pla_transferService.Get(id).pla_attachment;
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
            InterPlaTransferVM pla_transfer = _intra_plant_service.Get((int)id);
            if (pla_transfer == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(220), "document_numbring_id", "category");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.Batch_list = new SelectList(_batchService.GetAll(), "BATCH_ID", "BATCH_NUMBER");
            ViewBag.Uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.Item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.send_loc = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            ViewBag.receive_loc = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            ViewBag.bucket_list = new SelectList(_bucketservices.GetAll(), "bucket_id", "bucket_name");
            return View(pla_transfer);
        }

        // GET: PlantTransfer/Create
        [CustomAuthorizeAttribute("WPT")]
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(220), "document_numbring_id", "category");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.Batch_list = new SelectList(_batchService.GetAll(), "item_batch_id", "batch_number");
            ViewBag.Uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.Item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.send_loc = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            ViewBag.receive_loc = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            ViewBag.bucket_list = new SelectList(_bucketservices.GetAll(), "bucket_id", "bucket_name");

            return View();
        }

        // POST: PlantTransfer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InterPlaTransferVM pla_transfer)
        {            
            if (ModelState.IsValid)
            {
                var issaved = _intra_plant_service.Add(pla_transfer);
                if (issaved.Contains("Saved"))
                {
                    var sp = issaved.Split('~')[1];
                    TempData["data"] = sp;
                    return RedirectToAction("Index");
                }
                ViewBag.error = issaved;
            }
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(220), "document_numbring_id", "category");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.Batch_list = new SelectList(_batchService.GetAll(), "item_batch_id", "batch_number");
            ViewBag.Uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.Item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.send_loc = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            ViewBag.receive_loc = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            ViewBag.bucket_list = new SelectList(_bucketservices.GetAll(), "bucket_id", "bucket_name");

            return View(pla_transfer);
        }
        public CrystalReportPdfResult Pdf(int id, int type)
        {
            ReportDocument rd = new ReportDocument();
            try
            {
                double qty = 0;
                var inter_plant = _intra_plant_service.InterPlantTransfer((int)id);
                var inter_plant_detail = _intra_plant_service.GetInterPlantTransferDetail((int)id);
                DataSet ds = new DataSet("pr");
                foreach (var i in inter_plant_detail)
                {
                    qty = qty + i.plant_qty;
                }
                //var mat = new List<inv_material_out_VM>();
                //mat.Add(mat_out);
                var dt1 = _Generic.ToDataTable(inter_plant);
                var dt2 = _Generic.ToDataTable(inter_plant_detail);

                dt1.TableName = "InterPlantTransfer";
                dt2.TableName = "InterPlantTransferDetail";
                dt1.Rows[0]["total_quantity"] = qty;
                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);


                if (type == 1)
                {
                    rd.Load(Path.Combine(Server.MapPath("~/Reports/InterPlantTransferReports.rpt")));
                }
                else
                {
                    rd.Load(Path.Combine(Server.MapPath("~/Reports/InterPlantTransferReportsa5.rpt")));
                }
                rd.SetDataSource(ds);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string reportPath = "";
                if (type == 1)
                {
                    reportPath = Path.Combine(Server.MapPath("~/Reports"), "InterPlantTransferReports.rpt");
                }
                else
                {
                    reportPath = Path.Combine(Server.MapPath("~/Reports"), "InterPlantTransferReportsa5.rpt");
                }
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