using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Web.CustomFilters;
using Newtonsoft.Json;

namespace Sciffer.Erp.Web.Controllers
{
    public class MaterialRequisitionNoteController : Controller
    {
        private ScifferContext db = new ScifferContext();
        private readonly IStatusService _statusService;
        private readonly IVendorService _vendorService;
        private readonly IPlantService _plantService;
        private readonly IItemService _itemService;
        private readonly IUOMService _uOMService;
        private readonly IGenericService _Generic;
        private readonly IStorageLocation _storageLocation;
        private readonly IEmployeeService _employeeService;
        private readonly ICostCenterService _costCenterService;
        private readonly IMachineMasterService _machineMasterService;
        private readonly IReasonDeterminationService _reasonDeterminationService;
        private readonly IMaintenanceTypeService _maintenanceTypeService;
        private readonly IMaterialRequisionNoteService _materialRequisionNoteService;
        private readonly ILoginService _login;
        public MaterialRequisitionNoteController(IGenericService gen, IStorageLocation StorageLocation, IEmployeeService EmployeeService, ICostCenterService CostCenterService, IMachineMasterService MachineMasterService,
            IUOMService IUOMService, IPlantService PlantService, IItemService IItemService, IReasonDeterminationService ReasonDeterminationService,
            IStatusService StatusService, IMaintenanceTypeService MaintenanceTypeService, IMaterialRequisionNoteService MaterialRequisionNoteService, ILoginService login)
        {
            _statusService = StatusService;
            _itemService = IItemService;
            _plantService = PlantService;
            _uOMService = IUOMService;
            _Generic = gen;
            _storageLocation = StorageLocation;
            _employeeService = EmployeeService;
            _costCenterService = CostCenterService;
            _machineMasterService = MachineMasterService;
            _reasonDeterminationService = ReasonDeterminationService;
            _maintenanceTypeService = MaintenanceTypeService;
            _materialRequisionNoteService = MaterialRequisionNoteService;
            _login = login;
        }

        // GET: MaterialRequisionNote
        [CustomAuthorizeAttribute("MRN")]
        public ActionResult Index()
        {
            ViewBag.doc = TempData["doc_num"];
            ViewBag.mrn_list = _materialRequisionNoteService.GetAll();
            //int create_user = int.Parse(Session["User_Id"].ToString());
            //var open_cnt2 = _login.GetApprovedMRNCount(create_user);
            //Session["open_count3"] = open_cnt2;
            return View();
        }

        // GET: MaterialRequisionNote/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            material_requision_note material_requision_note = db.material_requision_note.Find(id);
            if (material_requision_note == null)
            {
                return HttpNotFound();
            }
            return View(material_requision_note);
        }

        // GET: MaterialRequisionNote/Create
        [CustomAuthorizeAttribute("MRN")]
        public ActionResult Create()
        {
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(80), "document_numbring_id", "category");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantCode(), "PLANT_ID", "PLANT_CODE");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("MRN"), "status_id", "status_name");
            ViewBag.requirement_by_list = new SelectList(_Generic.GetEmployeeCode(), "employee_id", "employee_code");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "item_id", "item_name");
            ViewBag.uom_list = new SelectList(_uOMService.GetAll(), "uom_id", "uom_name");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.reason_list = new SelectList(_reasonDeterminationService.GetAll(), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            ViewBag.cost_center_list = new SelectList(_Generic.GetCostCenter(), "cost_center_id", "cost_center_code");
            ViewBag.type_list = new SelectList(_maintenanceTypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            return View();
        }

        // POST: MaterialRequisionNote/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(material_requision_note_vm material_requision_note)
        {
            if (ModelState.IsValid)
            {
                var isValid = _materialRequisionNoteService.Add(material_requision_note);
                if (isValid != "error")
                {
                    TempData["doc_num"] = isValid;
                    return RedirectToAction("Index");
                }
            }

            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(80), "document_numbring_id", "category");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantCode(), "PLANT_ID", "PLANT_CODE");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("MRN"), "status_id", "status_name");
            ViewBag.requirement_by_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "item_id", "item_name");
            ViewBag.uom_list = new SelectList(_uOMService.GetAll(), "uom_id", "uom_name");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.reason_list = new SelectList(_reasonDeterminationService.GetAll(), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            ViewBag.cost_center_list = new SelectList(_Generic.GetCostCenter(), "cost_center_id", "cost_center_code");
            ViewBag.type_list = new SelectList(_maintenanceTypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            return View(material_requision_note);
        }

        // GET: MaterialRequisionNote/Edit/5
        [CustomAuthorizeAttribute("MRN")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            material_requision_note_vm material_requision_note = _materialRequisionNoteService.Get((int)id);
            if (material_requision_note == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(80), "document_numbring_id", "category");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantCode(), "PLANT_ID", "PLANT_CODE");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("MRN"), "status_id", "status_name");
            ViewBag.requirement_by_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "item_id", "item_name");
            ViewBag.uom_list = new SelectList(_uOMService.GetAll(), "uom_id", "uom_name");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.reason_list = new SelectList(_reasonDeterminationService.GetAll(), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            ViewBag.cost_center_list = new SelectList(_Generic.GetCostCenter(), "cost_center_id", "cost_center_code");
            ViewBag.type_list = new SelectList(_maintenanceTypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            ViewBag.sloc_list = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            return View(material_requision_note);
        }

        // POST: MaterialRequisionNote/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(material_requision_note_vm material_requision_note)
        {
            if (ModelState.IsValid)
            {
                var isValid = _materialRequisionNoteService.Add(material_requision_note);
                if (isValid != "error")
                {
                    TempData["doc_num"] = isValid;
                    return RedirectToAction("Index", TempData["doc_num"]);
                }
            }
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(80), "document_numbring_id", "category");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantCode(), "PLANT_ID", "PLANT_CODE");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("MRN"), "status_id", "status_name");
            ViewBag.requirement_by_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "item_id", "item_name");
            ViewBag.uom_list = new SelectList(_uOMService.GetAll(), "uom_id", "uom_name");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.reason_list = new SelectList(_reasonDeterminationService.GetAll(), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            ViewBag.cost_center_list = new SelectList(_Generic.GetCostCenter(), "cost_center_id", "cost_center_code");
            ViewBag.type_list = new SelectList(_maintenanceTypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            ViewBag.sloc_list = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            return View(material_requision_note);
        }

        // GET: MaterialRequisionNote/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            material_requision_note material_requision_note = db.material_requision_note.Find(id);
            if (material_requision_note == null)
            {
                return HttpNotFound();
            }
            return View(material_requision_note);
        }

        // POST: MaterialRequisionNote/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            material_requision_note material_requision_note = db.material_requision_note.Find(id);
            db.material_requision_note.Remove(material_requision_note);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult MRNDetail(int? id)
        {
            //mfg_prod_order_VM mfg_prod_order_VM = _productionService.Get((int)id);
            var vm = _materialRequisionNoteService.Get((int)id);
            var list = JsonConvert.SerializeObject(vm,
             Formatting.None,
                      new JsonSerializerSettings()
                      {
                          ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                      });

            return Content(list, "application/json");
        }
        public ActionResult ApprovedMRN()
        {
               
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Pending", Value = "0" });
            items.Add(new SelectListItem { Text = "Approved", Value = "1" });
            items.Add(new SelectListItem { Text = "Rejected", Value = "2" });
            List<material_requision_note_vm> po = new List<material_requision_note_vm>();
            foreach (var i in items)
            {
                material_requision_note_vm pp = new material_requision_note_vm();
                pp.approval_status = int.Parse(i.Value);
                pp.approval_status_name = i.Text;
                po.Add(pp);
            }

            var data = _materialRequisionNoteService.GetPendigApprovedList();
            ViewBag.datasource = data;
            ViewBag.status_list = po;
            return View();
        }
        [HttpPost]
        public ActionResult ChangeApprovedStatus(material_requision_note_vm value)
        {
            var change = _materialRequisionNoteService.ChangeApprovedStatus(value);
            return RedirectToAction("Index");
        }
        public ActionResult GetApprovedStatus(int id)
        {     
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Pending", Value = "0" });
            items.Add(new SelectListItem { Text = "Approved", Value = "1" });
            items.Add(new SelectListItem { Text = "Rejected", Value = "2" });
            List<material_requision_note_vm> po = new List<material_requision_note_vm>();
            foreach (var i in items)
            {
                material_requision_note_vm pp = new material_requision_note_vm();
                pp.approval_status = int.Parse(i.Value);
                pp.approval_status_name = i.Text;
                po.Add(pp);
            }
            var data = _materialRequisionNoteService.GetPendigApprovedList();
            ViewBag.datasource = data;
            ViewBag.status_list = po;
            return PartialView("Partial_ApprovalStatus", data);
        }
        public ActionResult GetMaterialRate(int plant_id, int item_id)
        {
            var vm = _materialRequisionNoteService.GetMaterialRate(plant_id, item_id);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult materialRequisionNoteupdatestatusseen()
        //{
        //    var result = _materialRequisionNoteService.materialRequisionNoteupdatestatusseen();
        //    var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;
        //}


        }
    }
