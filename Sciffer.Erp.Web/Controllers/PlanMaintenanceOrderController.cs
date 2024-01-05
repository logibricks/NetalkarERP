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
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using Newtonsoft.Json;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class PlanMaintenanceOrderController : Controller
    {
        private readonly IPlanMaintenanceOrderService _planMOService;
        private readonly IPlanMaintenanceService _planMService;
        private readonly IMachineMasterService _machineService;
        private readonly IPlantService _plantService;
        private readonly IGenericService _Generic;
        private readonly IMachineCategoryService _machinecatService;
        private readonly IMaintenanceTypeService _mtypeService;
        private readonly IParameterListService _param;
        private readonly IPlanMaintenanceService _plan;
        public PlanMaintenanceOrderController(IParameterListService param, IPlanMaintenanceOrderService planMOService, IMaintenanceTypeService mtypeService,
            IPlanMaintenanceService planMService, IMachineCategoryService machinecatService, IGenericService gen, IMachineMasterService MachineMasterService,
            IPlantService PlantService, IPlanMaintenanceService plan)
        {
            _machineService = MachineMasterService;
            _plantService = PlantService;
            _Generic = gen;
            _machinecatService = machinecatService;
            _planMService = planMService;
            _mtypeService = mtypeService;
            _planMOService = planMOService;
            _param = param;
            _plan = plan;
        }
        [CustomAuthorizeAttribute("PMO")]
        // GET: PlanMaintenanceOrder
        public ActionResult Index()
        {
            ViewBag.doc = TempData["data"];
            ViewBag.DataSource = _planMOService.GetAll();
            return View();
        }
        // GET: PlanMaintenanceOrder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plan_maintenance_order_VM plan_maintenance_order_VM = _planMOService.Get((int)id);
            if (plan_maintenance_order_VM == null)
            {
                return HttpNotFound();
            }
            ViewBag.planMaintenanceList = new SelectList(_plan.GetAll(), "plan_maintenance_id", "doc_number");
            ViewBag.mcatlist = new SelectList(_Generic.GetMachineCategoryList(), "machine_category_id", "machine_category_code");
            ViewBag.plantlist = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.mtypelist = new SelectList(_mtypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.employeeList = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.parameterList = new SelectList(_param.GetUnBlockedParameterList(), "parameter_id", "parameter_desc");
            return View(plan_maintenance_order_VM);
        }
        [CustomAuthorizeAttribute("PMO")]
        // GET: PlanMaintenanceOrder/Create
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.planMaintenanceList = new SelectList(_plan.GetAll(), "plan_maintenance_id", "doc_number");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(224), "document_numbring_id", "category");
            ViewBag.mcatlist = new SelectList(_Generic.GetMachineCategoryList(), "machine_category_id", "machine_category_code");
            ViewBag.plantlist = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.mtypelist = new SelectList(_mtypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.employeeList = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.parameterList = new SelectList(_param.GetUnBlockedParameterList(), "parameter_id", "parameter_desc");
            return View();
        }

        // POST: PlanMaintenanceOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(plan_maintenance_order_VM plan_maintenance_order_VM, FormCollection fc)
        {
            string products;
            products = fc["productdetail"];
            string[] emptyStringArray = new string[0];

            try
            {
                emptyStringArray = products.Split(new string[] { "~" }, StringSplitOptions.None);

            }
            catch (Exception ex)
            {

            }

            string products1;
            products1 = fc["productdetail1"];
            string[] emptyStringArray1 = new string[0];

            try
            {
                emptyStringArray1 = products1.Split(new string[] { "~" }, StringSplitOptions.None);

            }
            catch (Exception ex)
            {

            }

            List<ref_plan_maintenance_order_parameter> plan_maintenance_order_parameter = new List<ref_plan_maintenance_order_parameter>();

            for (int i = 0; i < emptyStringArray.Count() - 1; i++)
            {
                ref_plan_maintenance_order_parameter p_maintenance_order_parameter = new ref_plan_maintenance_order_parameter();
                p_maintenance_order_parameter.sr_no = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[1]);
                p_maintenance_order_parameter.parameter_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[2]);
                p_maintenance_order_parameter.range = emptyStringArray[i].Split(new char[] { ',' })[4];
                p_maintenance_order_parameter.actual_result = emptyStringArray[i].Split(new char[] { ',' })[5];
                p_maintenance_order_parameter.method_used = emptyStringArray[i].Split(new char[] { ',' })[6];
                p_maintenance_order_parameter.self_check = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[7]);
                p_maintenance_order_parameter.document_reference = emptyStringArray[i].Split(new char[] { ',' })[8];

                plan_maintenance_order_parameter.Add(p_maintenance_order_parameter);
            }
            plan_maintenance_order_VM.ref_plan_maintenance_order_parameter = plan_maintenance_order_parameter;


            List<ref_plan_maintenance_order_cost> plan_maintenance_order_cost = new List<ref_plan_maintenance_order_cost>();

            for (int i = 0; i < emptyStringArray1.Count() - 1; i++)
            {
                ref_plan_maintenance_order_cost p_maintenance_order_cost = new ref_plan_maintenance_order_cost();

                p_maintenance_order_cost.sr_no = int.Parse(emptyStringArray1[i].Split(new char[] { ',' })[1]);
                p_maintenance_order_cost.item_id = int.Parse(emptyStringArray1[i].Split(new char[] { ',' })[2]);
                p_maintenance_order_cost.quantity = float.Parse(emptyStringArray1[i].Split(new char[] { ',' })[5]);
                p_maintenance_order_cost.actual_quantity = float.Parse(emptyStringArray1[i].Split(new char[] { ',' })[6]);
                p_maintenance_order_cost.sloc_id = int.Parse(emptyStringArray1[i].Split(new char[] { ',' })[8]);
                p_maintenance_order_cost.bucket_id = int.Parse(emptyStringArray1[i].Split(new char[] { ',' })[9]);
                p_maintenance_order_cost.posting_date = DateTime.Parse(emptyStringArray1[i].Split(new char[] { ',' })[11]);

                plan_maintenance_order_cost.Add(p_maintenance_order_cost);
            }
            plan_maintenance_order_VM.ref_plan_maintenance_order_cost = plan_maintenance_order_cost;

            if (ModelState.IsValid)
            {
                var issaved = _planMOService.Add(plan_maintenance_order_VM);
                if (issaved == true)
                    return RedirectToAction("Index");
            }
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    var er = error.ErrorMessage;
                }
            }
            ViewBag.planMaintenanceList = new SelectList(_plan.GetAll(), "plan_maintenance_id", "doc_number");
            ViewBag.mcatlist = new SelectList(_Generic.GetMachineCategoryList(), "machine_category_id", "machine_category_code");
            ViewBag.plantlist = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.mtypelist = new SelectList(_mtypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.employeeList = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.parameterList = new SelectList(_param.GetUnBlockedParameterList(), "parameter_id", "parameter_desc");
            return View(plan_maintenance_order_VM);

        }
        [CustomAuthorizeAttribute("PMO")]
        // GET: PlanMaintenanceOrder/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plan_maintenance_order_VM plan_Maintenance_order_VM = _planMOService.Get((int)id);
            if (plan_Maintenance_order_VM == null)
            {
                return HttpNotFound();
            }
            ViewBag.error = "";
            ViewBag.planMaintenanceList = new SelectList(_plan.GetAll(), "plan_maintenance_id", "doc_number");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(224), "document_numbring_id", "category");
            ViewBag.mcatlist = new SelectList(_Generic.GetMachineCategoryList(), "machine_category_id", "machine_category_code");
            ViewBag.plantlist = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.mtypelist = new SelectList(_mtypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.employeeList = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.employeeList_attended_by = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.parameterList = new SelectList(_param.GetUnBlockedParameterList(), "parameter_id", "parameter_desc");
            return View(plan_Maintenance_order_VM);
        }

        // POST: PlanMaintenanceOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(plan_maintenance_order_VM plan_Maintenance_order_VM)
        {
            if (ModelState.IsValid)
            {
                var isedited = _planMOService.Update(plan_Maintenance_order_VM);
                if (isedited != "Error")
                {
                    TempData["data"] = isedited;
                    return RedirectToAction("Index", TempData["data"]);
                }
                ViewBag.error = "Something went wrong !";
            }
            ViewBag.planMaintenanceList = new SelectList(_plan.GetAll(), "plan_maintenance_id", "doc_number");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(224), "document_numbring_id", "category");
            ViewBag.mcatlist = new SelectList(_Generic.GetMachineCategoryList(), "machine_category_id", "machine_category_code");
            ViewBag.plantlist = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.mtypelist = new SelectList(_mtypeService.GetAll(), "maintenance_type_id", "maintenance_name");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.employeeList = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.employeeList_attended_by = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.parameterList = new SelectList(_param.GetUnBlockedParameterList(), "parameter_id", "parameter_desc");
            return View(plan_Maintenance_order_VM);
        }


        public string EditForMachinEntry(int? id, List<plan_maintenance_order_parameter_vm> item1)
        {
            var result = _planMOService.UpdatePlanMaintaince(id, item1);
            return result;
        }

        // GET: PlanMaintenanceOrder/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plan_maintenance_order_VM plan_order = _planMOService.Get((int)id);
            if (plan_order == null)
            {
                return HttpNotFound();
            }
            return View(plan_order);
        }

        // POST: PlanMaintenanceOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bool isdeleted = _planMOService.Delete(id);
            if (isdeleted == true)
            {
                return RedirectToAction("Index");
            }
            plan_maintenance_order_VM plan_order = _planMOService.Get((int)id);
            if (plan_order == null)
            {
                return HttpNotFound();
            }
            return View(plan_order);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _planMOService.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult GetMachineList(int id)
        {
            var list = _Generic.GetMachineList(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMaintenanceParameterList(int id)
        {
            var ParameterList = _planMOService.GetMaintenance_order_parameter(id);
            return Json(ParameterList,JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult GetMaintenanceParameterCostList(int id)
        {
            var ParameterList = _planMOService.GetMaintenance_order_parameter_cost(id);
            return Json(ParameterList,JsonRequestBehavior.AllowGet);
        }

        public CrystalReportPdfResult Pdf(int id, int type)
        {
            ReportDocument rd = new ReportDocument();
            try
            {
                if (type == 1)
                {
                    //var mat_in = _materialinservice.Get((int)id);

                    // var inv_stock_detail = _inventoryStockService.GetItemForStockEdit((int) id);
                    var mat_in = _planMOService.PlanMaintenanceOrder((int)id);
                    var mat_in_detail = _planMOService.PlanMaintenanceOrderDetail((int)id);
                    //  inv_stock_detail

                    DataSet ds = new DataSet("pr");

                    var mat = new List<plan_maintenance_order_VM>();
                    mat.Add(mat_in);

                    var dt1 = _Generic.ToDataTable(mat);
                    var dt2 = _Generic.ToDataTable(mat_in_detail);

                    dt1.TableName = "Plan_Maintenance_Order";
                    dt2.TableName = "Plan_Maintenance_Order_Detail";

                    ds.Tables.Add(dt1);
                    ds.Tables.Add(dt2);

                    rd.Load(Path.Combine(Server.MapPath("~/Reports/PlanMaintenanceOrderReport.rpt")));
                    rd.SetDataSource(ds);
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    string reportPath = Path.Combine(Server.MapPath("~/Reports"), "PlanMaintenanceOrderReport.rpt");
                    return new CrystalReportPdfResult(reportPath, rd);
                }
                else
                {
                    //var mat_in = _materialinservice.Get((int)id);

                    // var inv_stock_detail = _inventoryStockService.GetItemForStockEdit((int) id);
                    var mat_in = _planMOService.PlanMaintenanceOrder((int)id);
                    var mat_in_detail = _planMOService.PlanMaintenanceOrderComponentDetail((int)id);
                    //  inv_stock_detail

                    DataSet ds = new DataSet("pr");

                    var mat = new List<plan_maintenance_order_VM>();
                    mat.Add(mat_in);

                    var dt1 = _Generic.ToDataTable(mat);
                    var dt2 = _Generic.ToDataTable(mat_in_detail);

                    dt1.TableName = "Plan_Maintenance_Order";
                    dt2.TableName = "Plan_Maintenance_Order_Detail";

                    ds.Tables.Add(dt1);
                    ds.Tables.Add(dt2);


                    rd.Load(Path.Combine(Server.MapPath("~/Reports/PlanMaintenanceOrderReport_component.rpt")));
                    rd.SetDataSource(ds);
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    string reportPath = Path.Combine(Server.MapPath("~/Reports"), "PlanMaintenanceOrderReport_component.rpt");
                    return new CrystalReportPdfResult(reportPath, rd);
                }
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
