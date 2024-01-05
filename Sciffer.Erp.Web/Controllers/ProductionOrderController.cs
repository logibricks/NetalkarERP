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
    public class ProductionOrderController : Controller
    {
        private readonly IProductionService _productionService;
        private readonly IItemService _itemService;
        private readonly IGenericService _genericService;
        private readonly IShiftService _shiftService;
        private readonly IStorageLocation _storageLocation;
        private readonly IPlantService _plantService;
        private readonly IUOMService _uOMService;

        public ProductionOrderController(IUOMService uom, IProductionService productionService, IItemService itemService, IGenericService _Generic, IShiftService Shift, IStorageLocation StorageLocation, IPlantService PlantService)
        {
            _productionService = productionService;
            _itemService = itemService;
            _genericService = _Generic;
            _shiftService = Shift;
            _storageLocation = StorageLocation;
            _plantService = PlantService;
            _uOMService = uom;
        }
        // GET: ProductionOrder
        [CustomAuthorizeAttribute("PRDO")]
        public ActionResult Index()
        {
           // DateTime oneYearAgo = DateTime.Now.AddDays(-365);
            ViewBag.num = TempData["data"];
            ViewBag.ProductionOrder = _productionService.GetAll();            
            return View();
        }
        [CustomAuthorizeAttribute("PRDO")]
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.categorylist = new SelectList(_genericService.GetCategoryList(95), "document_numbring_id", "category");
            ViewBag.ITEM = new SelectList(_genericService.GetCatWiesItemList(2), "ITEM_ID", "ITEM_NAME");
            ViewBag.SHIFT = new SelectList(_shiftService.GetAll(), "shift_id", "shift_code");
            ViewBag.Uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.PLANT = new SelectList(_genericService.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.machine_list = _productionService.GetMachineList();
            return View();
        }

        public ActionResult GetProcessSequenceByProcessSequenceId(int process_sequence_id)
        {
            var data = _productionService.GetProcessSequenceByProcessSequenceId(process_sequence_id);
            return Json(data);
        }

        public ActionResult Getprocess_seq(int item)
        {
            var data = _productionService.Getprocess_seq(item);
            SelectList d = new SelectList(data, "process_seq_alt_id", "seq_name");
            return Json(d);
        }
        
        public ActionResult GetBOMForItem(int out_item_id)
        {
            var data = _productionService.GetBOMForItem(out_item_id);
            SelectList d = new SelectList(data, "mfg_bom_id", "mfg_bom_name");
            return Json(d);
        }

        public ActionResult GetBOMGridData(int bom_id, double itemquantity)
        {
            var data = _productionService.GetBOMGridData(bom_id, itemquantity);
            return Json(data);
        }

        public ActionResult GetTagNumbers(string machine, int prod_order_id)
        {
            var data = _productionService.GetTagNumbers(machine, prod_order_id);
            return Json(data);
        }

        public ActionResult UpdateTagNumbers(string source, string destination, int prod_order_id)
        {
            var data = _productionService.UpdateTagNumbers(source, destination, prod_order_id);
            return Json(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(mfg_prod_order_VM mfg_prod_order_VM, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                var output = _productionService.Add(mfg_prod_order_VM);
               
                if (output != "Error")
                {
                    TempData["data"] = output;
                    return RedirectToAction("Index", TempData["data"]);
                }
                ViewBag.error = "Something went wrong !";
            }

            ViewBag.categorylist = new SelectList(_genericService.GetCategoryList(95), "document_numbring_id", "category");
            ViewBag.ITEM = new SelectList(_genericService.GetCatWiesItemList(2), "ITEM_ID", "ITEM_NAME");
            ViewBag.SHIFT = new SelectList(_shiftService.GetAll(), "shift_id", "shift_code");
            ViewBag.BOM = new SelectList(_productionService.GetAllBom(), "mfg_bom_id", "mfg_bom_name");
            ViewBag.Uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.PLANT = new SelectList(_genericService.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.machine_list = _productionService.GetMachineList();
            return View(mfg_prod_order_VM);
        }

        public ActionResult GetProductionOrderDetail(int? id)
        {
            //mfg_prod_order_VM mfg_prod_order_VM = _productionService.Get((int)id);
            var vm = _productionService.Get((int)id);
            var list = JsonConvert.SerializeObject(vm,
             Formatting.None,
                      new JsonSerializerSettings()
                      {
                          ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                      });

            return Content(list, "application/json");
        }

        [CustomAuthorizeAttribute("PRDO")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mfg_prod_order_VM mfg_prod_order_VM = _productionService.Get((int)id);
            if (mfg_prod_order_VM == null)
            {
                return HttpNotFound();
            }
            ViewBag.categorylist = new SelectList(_genericService.GetCategoryList(95), "document_numbring_id", "category");
            ViewBag.ITEM = new SelectList(_genericService.GetItemList(), "ITEM_ID", "ITEM_NAME");

            ViewBag.SHIFT = new SelectList(_shiftService.GetAll(), "shift_id", "shift_code");
            ViewBag.BOM = new SelectList(_productionService.GetAllBom(), "mfg_bom_id", "mfg_bom_name");
            ViewBag.Uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.PLANT = new SelectList(_genericService.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.machine_list = _productionService.GetMachineList();
            ViewBag.sequence_list = _productionService.GetProcessSequence((int)id);
            ViewBag.Machine_List1 = new SelectList(_productionService.GetMachineList(), "machine_id", "machine_code");

            return View(mfg_prod_order_VM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(mfg_prod_order_VM mfg_prod_order_VM, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                var isedited = _productionService.Update(mfg_prod_order_VM);
                if (isedited == true)
                    return RedirectToAction("Index");
            }

            ViewBag.categorylist = new SelectList(_genericService.GetCategoryList(95), "document_numbring_id", "category");
            ViewBag.ITEM = new SelectList(_genericService.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.SHIFT = new SelectList(_shiftService.GetAll(), "shift_id", "shift_code");
            ViewBag.BOM = new SelectList(_productionService.GetAllBom(), "mfg_bom_id", "mfg_bom_name");
            ViewBag.Uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.PLANT = new SelectList(_genericService.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.machine_list = _productionService.GetMachineList();
            ViewBag.sequence_list = _productionService.GetProcessSequence((int)mfg_prod_order_VM.prod_order_id);
            return View(mfg_prod_order_VM);
        }

        //------------------------------------------------Operation sequence----------------------------------------------
        public ActionResult GetOperationsByOperationSequenceId(int process_seq_alt_id)
        {
            var data = _productionService.GetOperationsByOperationSequenceId(process_seq_alt_id);
            return Json(data);
        }
        //Get mapped machine listb by process id
        public ActionResult GetMappedMachinesByProcessId(int process_id)
        {
            var data = _productionService.GetMappedMachinesByProcessId(process_id);
            return Json(data);
        }

        //Update process sequence by production order id
        public ActionResult UpdateProcessSequence(int id, string process_sequence)
        {
            var data = _productionService.UpdateProcessSequence(id, process_sequence);
            return Json(data);
        }

        //Update process sequence by process sequence id
        public ActionResult UpdateProcessSequenceById(int process_sequence_id, string process_sequence)
        {
            var data = _productionService.UpdateProcessSequenceById(process_sequence_id, process_sequence);
            return Json(data);
        }
    }
}