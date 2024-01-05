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
    public class MaterialRequisitionIndentController : Controller
    {

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
        private readonly IMaterialRequisitionIndentService _request;
        private readonly ILoginService _login;
        public MaterialRequisitionIndentController(IGenericService gen, IStorageLocation StorageLocation, IEmployeeService EmployeeService, ICostCenterService CostCenterService, IMachineMasterService MachineMasterService,
            IUOMService IUOMService, IPlantService PlantService, IItemService IItemService, IReasonDeterminationService ReasonDeterminationService,
            IStatusService StatusService, IMaintenanceTypeService MaintenanceTypeService, IMaterialRequisionNoteService MaterialRequisionNoteService, 
            IMaterialRequisitionIndentService Request, ILoginService login)
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
            _request = Request;
            _login = login;

        }

        // GET: MaterialRequisitionIndent
        public ActionResult Index()
        {
            ViewBag.doc = TempData["doc_num"];
            ViewBag.mrn_list = _request.GetAll();
            int create_user = int.Parse(Session["User_Id"].ToString());
            var open_cnt2 = _login.GetApprovedMRICount(create_user);
            Session["open_count3"] = open_cnt2;

            var user_id = int.Parse(Session["User_Id"].ToString());
            var checkoperator = _login.CheckOperatorLogin(user_id, "PROD_OP");
            if (checkoperator == true)
            {
                ViewBag.role = "Operator";
            }
            else
            {
                ViewBag.role = "Not Operator";
            }

            return View();
        }

        public ActionResult Create(int machine_id = 0, int crankshaft_id = 0)
        {
            int user_id = int.Parse(Session["User_Id"].ToString());
            var checkoperator = _login.CheckOperatorLogin(user_id, "PROD_OP");
            if (checkoperator == true)
            {
                ViewBag.role = "Operator";
            }
            else
            {
                ViewBag.role = "Not Operator";
            }

            ViewBag.user_id = user_id;
            ViewBag.machine_id = machine_id;
            ViewBag.crankshaft_id = crankshaft_id;
            //Use new function for category list
            ViewBag.category_list = new SelectList(_Generic.GetCategoryListByModule("MRI"), "document_numbring_id", "category");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantCode(), "PLANT_ID", "PLANT_CODE");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("MRI"), "status_id", "status_name");
            ViewBag.requirement_by_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "item_id", "item_name");
            ViewBag.crankshaft_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");

            ViewBag.uom_list = new SelectList(_uOMService.GetAll(), "uom_id", "uom_name");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.reason_list = new SelectList(_reasonDeterminationService.GetAll(), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            ViewBag.cost_center_list = new SelectList(_Generic.GetCostCenter(), "cost_center_id", "cost_center_code");
            ViewBag.type_list = new SelectList(_maintenanceTypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            ViewBag.toolusagelist = new SelectList(_request.GetToolUsageTypeList(), "tool_usage_type_id", "tool_usage_type_name");
            ViewBag.toolcategorylist = new SelectList(_request.GetToolCategoryList(), "tool_category_id", "tool_category_name");
            ViewBag.process_list = new SelectList(_request.GetOperationList(user_id), "process_id", "process_description");
            return View();
        }

        public ActionResult GetToolOperationMappedList(int crankshaft_id = 0, int tool_usage_type_id = 0, int tool_category_id = 0, int process_id = 0)
        {
            var data = _request.GetToolOperationMappedList(crankshaft_id, tool_usage_type_id, tool_category_id, process_id);
            return Json(data);
        }

        // POST: MaterialRequisionNote/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(material_requision_note_vm material_requision_note, FormCollection fc)
        {

            if (ModelState.IsValid)
            {
                var isValid = _request.Add(material_requision_note);
                if (isValid != "error")
                {
                    TempData["doc_num"] = isValid;
                    return RedirectToAction("Index", TempData["doc_num"]);
                }
            }

            int user_id = int.Parse(Session["User_Id"].ToString());
            ViewBag.user_id = user_id;
            ViewBag.machine_id = material_requision_note.machine1[0];
            ViewBag.crankshaft_id = material_requision_note.item_id1[0];
            //Use new function for category list
            ViewBag.category_list = new SelectList(_Generic.GetCategoryListByModule("MRI"), "document_numbring_id", "category");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantCode(), "PLANT_ID", "PLANT_CODE");
            //ViewBag.status_list = new SelectList(_Generic.GetStatusList("MRN"), "status_id", "status_name");

            ViewBag.status_list = new SelectList(_Generic.GetStatusList("MRI"), "status_id", "status_name");
            ViewBag.requirement_by_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "item_id", "item_name");
            ViewBag.crankshaft_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");

            ViewBag.uom_list = new SelectList(_uOMService.GetAll(), "uom_id", "uom_name");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.reason_list = new SelectList(_reasonDeterminationService.GetAll(), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            ViewBag.cost_center_list = new SelectList(_Generic.GetCostCenter(), "cost_center_id", "cost_center_code");
            ViewBag.type_list = new SelectList(_maintenanceTypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            ViewBag.toolusagelist = new SelectList(_request.GetToolUsageTypeList(), "tool_usage_type_id", "tool_usage_type_name");
            ViewBag.toolcategorylist = new SelectList(_request.GetToolCategoryList(), "tool_category_id", "tool_category_name");
            ViewBag.process_list = new SelectList(_request.GetOperationList(user_id), "process_id", "process_description");

            var checkoperator = _login.CheckOperatorLogin(user_id, "PROD_OP");
            if (checkoperator == true)
            {
                ViewBag.role = "Operator";
            }
            else
            {
                ViewBag.role = "Not Operator";
            }

            return View(material_requision_note);
        }

        // GET: MaterialRequisionIndent/Edit/
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            material_requision_note_vm material_requision_note = _request.Get((int)id);
            if (material_requision_note == null)
            {
                return HttpNotFound();
            }
            int user_id = int.Parse(Session["User_Id"].ToString());
            //Use new function for category list
            ViewBag.category_list = new SelectList(_Generic.GetCategoryListByModule("MRI"), "document_numbring_id", "category");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantCode(), "PLANT_ID", "PLANT_CODE");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("MRN"), "status_id", "status_name");
            ViewBag.requirement_by_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "item_id", "item_name");
            ViewBag.crankshaft_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");

            ViewBag.uom_list = new SelectList(_uOMService.GetAll(), "uom_id", "uom_name");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.reason_list = new SelectList(_reasonDeterminationService.GetAll(), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            ViewBag.cost_center_list = new SelectList(_Generic.GetCostCenter(), "cost_center_id", "cost_center_code");
            ViewBag.type_list = new SelectList(_maintenanceTypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            ViewBag.toolusagelist = new SelectList(_request.GetToolUsageTypeList(), "tool_usage_type_id", "tool_usage_type_name");
            ViewBag.toolcategorylist = new SelectList(_request.GetToolCategoryList(), "tool_category_id", "tool_category_name");
            ViewBag.process_list = new SelectList(_request.GetOperationList(user_id), "process_id", "process_description");
            ViewBag.sloc_list = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");

            var checkoperator = _login.CheckOperatorLogin(user_id, "PROD_OP");
            if (checkoperator == true)
            {
                ViewBag.role = "Operator";
            }
            else
            {
                ViewBag.role = "Not Operator";
            }

            return View(material_requision_note);
        }

        // POST: MaterialRequisionIndent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(material_requision_note_vm material_requision_note)
        {
            if (ModelState.IsValid)
            {
                var isValid = _request.Add(material_requision_note);
                if (isValid != "error")
                {
                    TempData["doc_num"] = isValid;
                    return RedirectToAction("Index", TempData["doc_num"]);
                }
            }
            int user_id = int.Parse(Session["User_Id"].ToString());
            ViewBag.user_id = user_id;
            ViewBag.machine_id = material_requision_note.machine1[0];
            ViewBag.crankshaft_id = material_requision_note.item_id1[0];
            //Use new function for category list
            ViewBag.category_list = new SelectList(_Generic.GetCategoryListByModule("MRI"), "document_numbring_id", "category");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantCode(), "PLANT_ID", "PLANT_CODE");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("MRN"), "status_id", "status_name");
            ViewBag.requirement_by_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "item_id", "item_name");
            ViewBag.crankshaft_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");

            ViewBag.uom_list = new SelectList(_uOMService.GetAll(), "uom_id", "uom_name");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.reason_list = new SelectList(_reasonDeterminationService.GetAll(), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            ViewBag.cost_center_list = new SelectList(_Generic.GetCostCenter(), "cost_center_id", "cost_center_code");
            ViewBag.type_list = new SelectList(_maintenanceTypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            ViewBag.toolusagelist = new SelectList(_request.GetToolUsageTypeList(), "tool_usage_type_id", "tool_usage_type_name");
            ViewBag.toolcategorylist = new SelectList(_request.GetToolCategoryList(), "tool_category_id", "tool_category_name");
            ViewBag.process_list = new SelectList(_request.GetOperationList(user_id), "process_id", "process_description");

            var checkoperator = _login.CheckOperatorLogin(user_id, "PROD_OP");
            if (checkoperator == true)
            {
                ViewBag.role = "Operator";
            }
            else
            {
                ViewBag.role = "Not Operator";
            }

            return View(material_requision_note);
        }

        public JsonResult MaterialRequisionIndentupdatestatusseen()
        {
            var result = _request.materialRequisionIndentupdatestatusseen();
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}