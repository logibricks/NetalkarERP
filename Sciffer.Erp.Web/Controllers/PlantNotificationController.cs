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
namespace Sciffer.Erp.Web.Controllers
{
    public class PlantNotificationController : Controller
    {
        private readonly IPlantNotificationService _Notificatione;
        private readonly IGenericService _Generic;
        private readonly IPlanMaintenanceOrderService _planMaintenanceOrder;
        public PlantNotificationController(IPlantNotificationService Notificatione, IGenericService gen, IPlanMaintenanceOrderService planMaintenanceOrder)
        {
            _Notificatione = Notificatione;
            _Generic = gen;
            _planMaintenanceOrder = planMaintenanceOrder;
        }
        public ActionResult Index()
        {
            ViewBag.doc = TempData["saved"];
            ViewBag.DataSource = _Notificatione.GetAll();
            return View();
        }
        // GET: PlantNotification/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_plant_notification ref_plant_notification = _Notificatione.Get((int)id);
            if (ref_plant_notification == null)
            {
                return HttpNotFound();
            }
            return View(ref_plant_notification);
        }
        // GET: plantNotification/Create
        public ActionResult Create()
        {
            ViewBag.PlanMaintenanceOrderList = new SelectList(_planMaintenanceOrder.GetAll(), "plan_maintenance_order_id", "order_no");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(229), "document_numbring_id", "category");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.employeelist = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.notificationtypelist = new SelectList(_Generic.GetNotificationType(), "NOTIFICATION_ID", "NOTIFICATION_TYPE");
            ViewBag.machinelist = new SelectList(_Generic.GetMachineList(1), "machine_id", "machine_code");
            ViewBag.machinedesc = new SelectList(_Generic.GetMachineList(1), "machine_id", "machine_name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_plant_notification ref_plant_notification)
        {
            if (ModelState.IsValid)
            {

                var s = _Notificatione.Add(ref_plant_notification);
                if (s != "Error")
                {
                    TempData["saved"] = s;
                    return RedirectToAction("Index");
                }
            }
            ViewBag.PlanMaintenanceOrderList = new SelectList(_planMaintenanceOrder.GetAll(), "plan_maintenance_order_id", "order_no");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(229), "document_numbring_id", "category");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.employeelist = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.notificationtypelist = new SelectList(_Generic.GetNotificationType(), "NOTIFICATION_ID", "NOTIFICATION_TYPE");
            ViewBag.machinelist = new SelectList(_Generic.GetMachineList(1), "machine_id", "machine_code");
            ViewBag.machinedesc = new SelectList(_Generic.GetMachineList(1), "machine_id", "machine_name");
            return View(ref_plant_notification);
        }
        // GET: plantNotification/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_plant_notification ref_plant_notification = _Notificatione.Get((int)id);
            if (ref_plant_notification == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlanMaintenanceOrderList = new SelectList(_planMaintenanceOrder.GetAll(), "plan_maintenance_order_id", "order_no");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(229), "document_numbring_id", "category");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.employeelist = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.notificationtypelist = new SelectList(_Generic.GetNotificationType(), "NOTIFICATION_ID", "NOTIFICATION_TYPE");
            //ViewBag.machinelist = new SelectList(_Generic.GetMachineList(1), "machine_id", "machine_code");
            //ViewBag.machinedesc = new SelectList(_Generic.GetMachineList(1), "machine_id", "machine_name");
            return View(ref_plant_notification);
        }

        // POST: Notification/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_plant_notification ref_plant_notification)
        {
            if (ModelState.IsValid)
            {
                var s = _Notificatione.Add(ref_plant_notification);
                if (s != "Error")
                {
                    TempData["saved"] = s;
                    return RedirectToAction("Index");
                }
            }
            ViewBag.PlanMaintenanceOrderList = new SelectList(_planMaintenanceOrder.GetAll(), "plan_maintenance_order_id", "order_no");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(229), "document_numbring_id", "category");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.employeelist = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.notificationtypelist = new SelectList(_Generic.GetNotificationType(), "NOTIFICATION_ID", "NOTIFICATION_TYPE");
            ViewBag.machinelist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.machinedesc = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_name");
            return View(ref_plant_notification);
        }

        // GET: PlantNotification/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_plant_notification ref_plant_notification = _Notificatione.Get((int)id);
            if (ref_plant_notification == null)
            {
                return HttpNotFound();
            }
            return View(ref_plant_notification);
        }

        // POST: Notification/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //ref_pm_notification ref_pm_notification = db.ref_pm_notification.Find(id);
            //db.ref_pm_notification.Remove(ref_pm_notification);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _Notificatione.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}