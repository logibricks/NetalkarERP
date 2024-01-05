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
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class PlanMaintenanceController : Controller
    {
        private readonly IPlanMaintenanceService _planMService;
        private readonly IMachineMasterService _machineService;        
        private readonly IPlantService _plantService;       
        private readonly IGenericService _Generic;       
        private readonly IMachineCategoryService _machinecatService;
        private readonly IMaintenanceTypeService _mtypeService;
        private readonly IStatusService _statusService;
        private readonly IParameterListService _param;
        private readonly IUOMService _uom;

        public PlanMaintenanceController(IUOMService uom, IParameterListService param, IStatusService statusService, IMaintenanceTypeService mtypeService,IPlanMaintenanceService planMService,IMachineCategoryService machinecatService,IGenericService gen, IMachineMasterService MachineMasterService, IPlantService PlantService)
        {
            _machineService = MachineMasterService;            
            _plantService = PlantService;            
            _Generic = gen;           
            _machinecatService = machinecatService;
            _planMService = planMService;
            _mtypeService = mtypeService;
            _statusService = statusService;
            _param = param;
            _uom = uom;
        }
        [CustomAuthorizeAttribute("MP")]
        // GET: PlanMaintenance
        public ActionResult Index()
        {
            ViewBag.doc = TempData["doc_num"];
            ViewBag.DataSource = _planMService.GetAll();
            return View();
        }

        // GET: PlanMaintenance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_plan_maintenance_VM ref_plan_maintenance_VM = _planMService.Get((int)id);
            if (ref_plan_maintenance_VM == null)
            {
                return HttpNotFound();
            }
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(225), "document_numbring_id", "category");
            ViewBag.mcatlist = new SelectList(_Generic.GetMachineCategoryList(), "machine_category_id", "machine_category_code");
            ViewBag.plantlist = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.mtypelist = new SelectList(_mtypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.employeeList = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.parameterList = new SelectList(_param.GetUnBlockedParameterList(), "parameter_id", "parameter_desc");
            ViewBag.uomList = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            return View(ref_plan_maintenance_VM);
        }
        [CustomAuthorizeAttribute("MP")]
        // GET: PlanMaintenance/Create
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(225), "document_numbring_id", "category");
            ViewBag.mcatlist = new SelectList(_Generic.GetMachineCategoryList(), "machine_category_id", "machine_category_code");
            ViewBag.plantlist = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.mtypelist = new SelectList(_mtypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.employeeList = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.parameterList = new SelectList(_param.GetUnBlockedParameterList(), "parameter_id", "parameter_desc");
            ViewBag.uomList = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            return View();
        }

        // POST: PlanMaintenance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_plan_maintenance_VM ref_Plan_maintenance_VM)
        {
           
            if (ModelState.IsValid)
            {
                var issaved = _planMService.Add(ref_Plan_maintenance_VM);
                if (issaved !="Error")
                {
                    TempData["doc_num"] = issaved;
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
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(225), "document_numbring_id", "category");
            ViewBag.mcatlist = new SelectList(_Generic.GetMachineCategoryList(), "machine_category_id", "machine_category_code");
            ViewBag.plantlist = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.mtypelist = new SelectList(_mtypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.employeeList = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.parameterList = new SelectList(_param.GetUnBlockedParameterList(), "parameter_id", "parameter_desc");
            ViewBag.uomList = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            return View(ref_Plan_maintenance_VM);

        }
        [CustomAuthorizeAttribute("MP")]
        // GET: PlanMaintenance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_plan_maintenance_VM ref_Plan_maintenance_VM = _planMService.Get((int)id);
            if (ref_Plan_maintenance_VM == null)
            {
                return HttpNotFound();
            }
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(225), "document_numbring_id", "category");
            ViewBag.mcatlist = new SelectList(_Generic.GetMachineCategoryList(), "machine_category_id", "machine_category_code");
            ViewBag.plantlist = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.mtypelist = new SelectList(_mtypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.employeeList = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.parameterList = new SelectList(_param.GetUnBlockedParameterList(), "parameter_id", "parameter_desc");
            ViewBag.uomList = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            return View(ref_Plan_maintenance_VM);
        }

        // POST: PlanMaintenance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_plan_maintenance_VM ref_Plan_maintenance_VM)
        {            
            if (ModelState.IsValid)
            {
                var issaved = _planMService.Add(ref_Plan_maintenance_VM);
                if(issaved != "Error")
                {
                    TempData["doc_num"] = issaved;
                    return RedirectToAction("Index");
                }
                ViewBag.error = issaved;
            }
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(225), "document_numbring_id", "category");
            ViewBag.mcatlist = new SelectList(_Generic.GetMachineCategoryList(), "machine_category_id", "machine_category_code");
            ViewBag.plantlist = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.mtypelist = new SelectList(_mtypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.employeeList = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.parameterList = new SelectList(_param.GetUnBlockedParameterList(), "parameter_id", "parameter_desc");
            ViewBag.uomList = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            return View(ref_Plan_maintenance_VM);
        }


        // GET: PlanMaintenance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_plan_maintenance_VM ref_plan = _planMService.Get((int)id);
            if (ref_plan == null)
            {
                return HttpNotFound();
            }
            return View(ref_plan);
        }

        // POST: PlanMaintenance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bool isdeleted = _planMService.Delete(id);
            if (isdeleted == true)
            {
                return RedirectToAction("Index");
            }
            ref_plan_maintenance_VM ref_plan = _planMService.Get((int)id);
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
                _planMService.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult GetMachineList(int id)
        {
            var list = _Generic.GetMachineList(id);
            return Json(list, JsonRequestBehavior.AllowGet);
         }
    }

}
