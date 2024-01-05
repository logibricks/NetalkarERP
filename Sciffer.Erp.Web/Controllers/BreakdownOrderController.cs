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
    public class BreakdownOrderController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly IParameterListService _param;
        private readonly IUOMService _uom;
        private readonly IPlanBreakdownOrderService _breakdownOrder;
        private readonly INotificationService _notification;
        private readonly IIssuePermitService _issuePermit;
        public BreakdownOrderController(IGenericService Generic, IParameterListService param, IUOMService uom, IPlanBreakdownOrderService breakdownOrder,
            INotificationService notification, IIssuePermitService issuePermit)
        {
            _Generic = Generic;
            _param = param;
            _uom = uom;
            _breakdownOrder = breakdownOrder;
            _notification = notification;
            _issuePermit = issuePermit;
        }
        [CustomAuthorizeAttribute("PBO")]
        // GET: BreakdownOrder
        public ActionResult Index()
        {
            ViewBag.num = TempData["doc_num"];
           // ViewBag.DataSource = _breakdownOrder.GetAll();
            return View();
        }
        [CustomAuthorizeAttribute("PBO")]
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.permitList = new SelectList(_issuePermit.GetAll(), "permit_id", "permit_no");
            ViewBag.notificationList = new SelectList(_notification.GetAll(0).Select(a => new { notification_id = a.notification_id, doc_number = a.category_name + "/" + a.doc_number + "/" + string.Format("{0:dd/MM/yyyy}", a.notification_date) + "/" + a.notification_type + "/" + a.notification_description }), "notification_id", "doc_number");
            ViewBag.employeeList = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.uomList = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.parameterList = new SelectList(_param.GetAll().Where(x => x.is_blocked == false).ToList(), "parameter_id", "parameter_desc");
            ViewBag.mcatlist = new SelectList(_Generic.GetMachineCategoryList(), "machine_category_id", "machine_category_code");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(227), "document_numbring_id", "category");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.employeeList = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.RMItemList = new SelectList(_Generic.GetRMItemList(), "ITEM_ID", "ITEM_NAME");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_plan_breakdown_order_VM VM)
        {
            if (ModelState.IsValid)
            {
                var issaved = _breakdownOrder.Add(VM);
                if (issaved != "Error")
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
            ViewBag.permitList = new SelectList(_issuePermit.GetAll(), "permit_id", "permit_no");
            ViewBag.notificationList = new SelectList(_notification.GetAll(0).Select(a => new { notification_id = a.notification_id, doc_number = a.category_name + "/" + a.doc_number + "/" + string.Format("{0:dd/MM/yyyy}", a.notification_date) + "/" + a.notification_type + "/" + a.notification_description }), "notification_id", "doc_number");
            ViewBag.employeeList = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.uomList = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.parameterList = new SelectList(_param.GetAll().Where(x=>x.is_blocked==false).ToList(), "parameter_id", "parameter_desc");
            ViewBag.mcatlist = new SelectList(_Generic.GetMachineCategoryList(), "machine_category_id", "machine_category_code");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(227), "document_numbring_id", "category");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.employeeList = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.RMItemList = new SelectList(_Generic.GetRMItemList(), "ITEM_ID", "ITEM_NAME");
            return View(VM);
        }
        [CustomAuthorizeAttribute("PBO")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_plan_breakdown_order_VM vm = _breakdownOrder.Get((int)id);
          
            if (vm == null)
            {
                return HttpNotFound();
            }
            ViewBag.error = "";
            ViewBag.permitList = new SelectList(_issuePermit.GetAll(), "permit_id", "permit_no");
            ViewBag.notificationList = new SelectList(_notification.GetAll(0).Select(a => new { notification_id = a.notification_id, doc_number = a.category_name + "/" + a.doc_number + "/" + string.Format("{0:dd/MM/yyyy}", a.notification_date) + "/" + a.notification_type + "/" + a.notification_description }), "notification_id", "doc_number");
            ViewBag.employeeList = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.uomList = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.parameterList = new SelectList(_param.GetAll().Where(x => x.is_blocked == false).ToList(), "parameter_id", "parameter_desc");
            ViewBag.mcatlist = new SelectList(_Generic.GetMachineCategoryList(), "machine_category_id", "machine_category_code");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(227), "document_numbring_id", "category");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.employeeList = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.RMItemList = new SelectList(_Generic.GetRMItemList(), "ITEM_ID", "ITEM_NAME");
            //  var userlist1 = _Generic.GetUserList().Where(a => a.user_id == user_id).ToList();
            // ViewBag.userlist1 = new SelectList(userlist1, "user_id", "user_name");
            ViewBag.userlist = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.machinelist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.userlist = new SelectList(_Generic.GetAttendedByList(id), "user_id", "user_name");


            return View(vm);
        }

        // POST: PlanMaintenanceOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_plan_breakdown_order_VM vm)
        {
            if (ModelState.IsValid)
            {
                //ref_plan_breakdown_order_VM VM = new ref_plan_breakdown_order_VM();
                //vm.employee_id = VM.under_taken_by_id;
             
                var isedited = _breakdownOrder.Add(vm);
                if (isedited != "Error")
                {
                    TempData["doc_num"] = isedited;
                    return RedirectToAction("Index", TempData["data"]);
                }
                ViewBag.error = "Something went wrong !";
            }
            ViewBag.permitList = new SelectList(_issuePermit.GetAll(), "permit_id", "permit_no");
            ViewBag.notificationList = new SelectList(_notification.GetAll(0).Select(a => new { notification_id = a.notification_id, doc_number = a.category_name + "/" + a.doc_number + "/" + string.Format("{0:dd/MM/yyyy}", a.notification_date) + "/" + a.notification_type + "/" + a.notification_description }), "notification_id", "doc_number");
            ViewBag.employeeList = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.uomList = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.parameterList = new SelectList(_param.GetAll().Where(x => x.is_blocked == false).ToList(), "parameter_id", "parameter_desc");
            ViewBag.mcatlist = new SelectList(_Generic.GetMachineCategoryList(), "machine_category_id", "machine_category_code");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(227), "document_numbring_id", "category");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.employeeList = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.userlist = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            ViewBag.machinelist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");

            return View(vm);
        }


        //public ActionResult Get_BreakOrderPartial(string entity, DateTime? from_date, DateTime? to_date, string item_id, string machine_id,string partial_v)
        //{
        //    DateTime dte = new DateTime(1990, 1, 1);
        //    ViewBag.from_date = from_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(from_date.ToString()).ToString("yyyy-MM-dd");
        //    ViewBag.to_date = to_date == null ? DateTime.Parse(dte.ToString()).ToString("yyyy-MM-dd") : DateTime.Parse(to_date.ToString()).ToString("yyyy-MM-dd");
        //    ViewBag.item_id = item_id;
        //    ViewBag.machine_id = machine_id;          
        //    ViewBag.entity = entity;
        //    return PartialView(partial_v, ViewBag);

        //}


        // GET: PlanMaintenanceOrder/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_plan_breakdown_order_VM plan_order = _breakdownOrder.Get((int)id);
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
            bool isdeleted = _breakdownOrder.Delete(id);
            if (isdeleted == true)
            {
                return RedirectToAction("Index");
            }
            ref_plan_breakdown_order_VM plan_order = _breakdownOrder.Get((int)id);
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
                //_breakdownOrder.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}