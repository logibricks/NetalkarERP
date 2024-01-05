using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
namespace Sciffer.Erp.Web.Controllers
{
    public class ShiftwiseProductionMasterController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly IMachineMasterService _machineService;
        private readonly IShiftwiseProductionMasterService _shiftwiseproduction;
        public ShiftwiseProductionMasterController(IGenericService gen, IMachineMasterService machineService, IShiftwiseProductionMasterService shiftwiseproduction)
        {
            _Generic = gen;
            _machineService = machineService;
            _shiftwiseproduction = shiftwiseproduction;
        }

        // GET: ShiftwiseProductionMaster
        public ActionResult Index()
        {

            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryListForShiftWise("SWPM"), "document_numbring_id", "category");
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");

            var categoryAll = _Generic.GetCategoryListForShiftWise("SWPM");
            ViewBag.categoryAllList = categoryAll;
            ViewBag.categorylist = new SelectList(categoryAll, "document_numbring_id", "category");
            ViewBag.shift_list = new SelectList(_Generic.GetShiftlist(), "shift_id", "shift_code");
            ViewBag.machineList = new SelectList(_machineService.GetAll(), "machine_id", "machine_name");
            ViewBag.machineAllList = _machineService.GetAll();
            ViewBag.itemlist = new SelectList(_Generic.GetItemListRMCategorywise(), "ITEM_ID", "ITEM_NAME");
            ViewBag.operationlist = new SelectList(_Generic.GetOperationList(), "process_id", "process_description");
            return View();
        }

        // POST:
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        public ActionResult Create(mfg_shiftwise_production_master_vm item1)
        {
            if (ModelState.IsValid)
            {
                var issaved = _shiftwiseproduction.Add(item1);
                if (issaved.Contains("Duplicate"))
                {
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(issaved);
                    return Json(json, JsonRequestBehavior.AllowGet);
                }
                if (issaved.Contains("Saved"))
                {
                    //var sp = issaved.Split('~')[1];
                    TempData["data"] = issaved;
                    return RedirectToAction("Index");
                }
                ViewBag.error = issaved;
            }
            return View(item1);
        }

        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            mfg_shiftwise_production_master_vm shift_production = _shiftwiseproduction.Get(id);
            if (shift_production == null)
            {
                return HttpNotFound();
            }

            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryListForShiftWise("SWPM"), "document_numbring_id", "category");
            ViewBag.shift_list = new SelectList(_Generic.GetShiftlist(), "shift_id", "shift_code");
            ViewBag.machineList = new SelectList(_machineService.GetAll(), "machine_id", "machine_name");
            ViewBag.itemlist = new SelectList(_Generic.GetItemListRMCategorywise(), "ITEM_ID", "ITEM_NAME");
            ViewBag.operationlist = new SelectList(_Generic.GetOperationList(), "process_id", "process_description");
            return View(shift_production);
        }


        [HttpPost]
        public ActionResult Edit(mfg_shiftwise_production_master_vm item1, List<shiftwiseProductionDetails> newItem)
        {

            var issaved = _shiftwiseproduction.Update(item1, newItem);
            if (issaved.Contains("Updated"))
            {
                //var sp = issaved.Split('~')[1];           
                TempData["data"] = issaved;
                return RedirectToAction("Index");
            }
            ViewBag.error = issaved;

            return View(item1);
        }
        
        public ActionResult GetAllShiftwiseProduction(string entity, DateTime? posting_date, int? plant_id, int? shift_id, int? process_id, int? machine_id, string item_id)
        {
            try
            {
                List<shiftwiseProductionDetails> shiftwiseproduction = null;

                shiftwiseproduction = _shiftwiseproduction.GetAllShiftwiseProduction(entity, posting_date, plant_id, shift_id, process_id, machine_id, item_id);
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(shiftwiseproduction);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public int GetQty(string entity, int? plant_id, int shift_id, int? process_id, int? machine_id, string item_id, int? operator_id)
        {
            int result = 0;
            result = _shiftwiseproduction.GetQty(entity, plant_id, shift_id, process_id, machine_id, item_id, operator_id);

            return result;
        }

        public int GetDuplicate(DateTime postingDate, int plantId, int shiftId)
        {
            var result = _shiftwiseproduction.CheckDuplicate(postingDate, plantId, shiftId);

            return result;
        }

        public ActionResult GetRecordByDetailsId(int Id)
        {
            List<shiftwiseProductionDetails> shiftwiseproduction = null;

            shiftwiseproduction = _shiftwiseproduction.GetDetails(Id);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(shiftwiseproduction);
            return Json(json, JsonRequestBehavior.AllowGet);

        }
    }
}