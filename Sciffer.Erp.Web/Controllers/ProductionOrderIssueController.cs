using Newtonsoft.Json;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class ProductionOrderIssueController : Controller
    {
        private readonly IProdOrderIssueService _prodIssueService;
        private readonly IGenericService _Generic;
        private readonly IPlantService _plant;
        private readonly IProductionService _prodService;
        private readonly IStorageLocation _storage;
        public ProductionOrderIssueController(IStorageLocation storage, IProdOrderIssueService prodIssueService, IGenericService generic, IPlantService plant,
            IProductionService prodService)
        {
            _prodIssueService = prodIssueService;
            _Generic = generic;
            _plant = plant;
            _prodService = prodService;
            _storage = storage;
        }
        [CustomAuthorizeAttribute("PRDI")]
        public ActionResult Index()
        {
            ViewBag.num = TempData["data"];
            ViewBag.DataSource = _prodIssueService.GetAll();
            return View();
        }
        [CustomAuthorizeAttribute("PRDI")]
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(96), "document_numbring_id", "category");
            ViewBag.PlantList = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.ProdOrdList = new SelectList(_prodService.GetActivePOListForIssue(), "prod_order_id", "prod_order_no");
            ViewBag.StorageLocationList = new SelectList(_storage.getstoragelist(), "storage_location_id", "storage_location_name");
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProdIssueVM vm)
        {
            if (ModelState.IsValid)
            {
                var issaved = _prodIssueService.Add(vm);
                if (issaved.Contains("Saved"))
                {
                    var sp = issaved.Split('~')[1];
                    TempData["data"] = sp;
                    return RedirectToAction("Index");
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

            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(96), "document_numbring_id", "category");
            ViewBag.PlantList = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.ProdOrdList = new SelectList(_prodService.GetActivePOListForIssue(), "prod_order_id", "prod_order_no");
            ViewBag.StorageLocationList = new SelectList(_storage.getstoragelist(), "storage_location_id", "storage_location_name");
            return View(vm);
        }
        public ActionResult TagProductionOrderIssue(int id)
        {
            var vm = _prodIssueService.GetTagProductionOrderIssue(id);
            var list = JsonConvert.SerializeObject(vm,
             Formatting.None,
                      new JsonSerializerSettings()
                      {
                          ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                      });

            return Content(list, "application/json");
        }
        public ActionResult NonTagProductionOrderIssue(int id)
        {
            var vm = _prodIssueService.GetNonTagProductionOrderIssue(id);
            var list = JsonConvert.SerializeObject(vm,
             Formatting.None,
                      new JsonSerializerSettings()
                      {
                          ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                      });

            return Content(list, "application/json");
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProdIssueVM gOODS_RECEIPT_VM = _prodIssueService.GetDetails((int)id);
            if (gOODS_RECEIPT_VM == null)
            {
                return HttpNotFound();
            }
            //ViewBag.error = "";
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(96), "document_numbring_id", "category");
            ViewBag.PlantList = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.ProdOrdList = new SelectList(_prodService.GetAll(), "prod_order_id", "prod_order_no");
            ViewBag.StorageLocationList = new SelectList(_storage.getstoragelist(), "storage_location_id", "storage_location_name");
            return View(gOODS_RECEIPT_VM);
        }

        public ActionResult ClumpsumBatchQuantity(string sloc_id, string plant_id, string item_id, string bucket_id, string entity_id, int? reason_id)
        {
            var vm = _prodIssueService.getClumpsumBatchQuantity(sloc_id, plant_id, item_id, bucket_id, entity_id,reason_id);
            var list = JsonConvert.SerializeObject(vm,
            Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });

            return Content(list, "application/json");
        }
        public ActionResult ItemForProdGoodsIssue(string sloc_id, string plant_id, string item_id, string bucket_id, int? reason_id)
        {
            var vm = _prodIssueService.GetItemForProdGoodsIssue(sloc_id, plant_id, item_id,bucket_id,reason_id);
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