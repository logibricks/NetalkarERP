using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using Syncfusion.EJ.Export;
using Syncfusion.JavaScript.Models;
using Syncfusion.XlsIO;
using AutoMapper;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Web.Controllers
{
    public class NotificationController : Controller


    {
        //private ScifferContext db = new ScifferContext();
        private readonly INotificationService _Notificatione;
        private readonly IGenericService _Generic;
        private readonly ILoginService _login;
        private readonly ISMSService _SMS;
        private readonly ScifferContext _scifferContext;
        private readonly IPlanBreakdownOrderService _breakdownOrder;

        public NotificationController(INotificationService Notificatione, IGenericService gen, ILoginService login, ISMSService SMS, ScifferContext scifferContext, IPlanBreakdownOrderService breakdownOrder)
        {
            _Notificatione = Notificatione;
            _Generic = gen;
            _login = login;
            _SMS = SMS;
            _scifferContext = scifferContext;
            _breakdownOrder = breakdownOrder;

        }

        // GET: Notification
        public ActionResult Index(int? machine_id)
        {
            if (machine_id == null)
            {
                machine_id = 0;
            }
            ViewBag.doc = TempData["saved"];
            //ViewBag.DataSource = _Notificatione.GetAll(machine_id);
            var user_id = int.Parse(Session["User_Id"].ToString());
            var checkoperator = _login.CheckOperatorLogin(user_id, "PROD_OP");
            ViewBag.loginuser = user_id;
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

        public string UpdateNotificationtStatus(int[] notifications_ids, int status)
        {
            var messege = _Notificatione.UpdateNotificationtStatus(notifications_ids, status);
            return messege;
        }

        public ActionResult CloseOpenItem()
        {
            var user_id = int.Parse(Session["User_Id"].ToString());
            ViewBag.DataSource = _Notificatione.get_mal_notification(user_id);
            return View();
        }

        // GET: Notification/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_pm_notification ref_pm_notification = _Notificatione.Get((int)id);
            if (ref_pm_notification == null)
            {
                return HttpNotFound();
            }
            return View(ref_pm_notification);
        }


        // GET: Notification/Create
        public ActionResult Create(int? machine_id)
        {
            var user_id = int.Parse(Session["User_Id"].ToString());
            machine_id = machine_id == null ? 0 : machine_id;
            ViewBag.loginuser = user_id;
            var checkoperator = _login.CheckOperatorLogin(user_id, "PROD_OP");
            if (checkoperator == true)
            {
                ViewBag.role = "Operator";
            }
            else
            {
                ViewBag.role = "Not Operator";
            }
            //ViewBag.categorylist = new SelectList(_Generic.GetCategoryListByModule("MALNOTIFICATION"), "document_numbring_id", "category");
            //ViewBag.categorylist1 = new SelectList(_Generic.GetCategoryListByPlant("), "document_numbring_id", "category");

            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.employeelist = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.notificationtypelist = new SelectList(_Generic.GetNotificationType(), "NOTIFICATION_ID", "NOTIFICATION_TYPE");
            var machine_list = _Generic.GetMachineList((int)machine_id);
            foreach (var item in machine_list)
            {
              
                var plant_name  = _Generic.GetPlantList().Where(x => x.PLANT_ID == item.plant_id).Select(x => x.PLANT_NAME).FirstOrDefault();
                item.plant_name = plant_name;
            }
            var machine_list2 = _Generic.GetMachineList((int)machine_id).FirstOrDefault();
            var plantlist2 = _Generic.GetPlantList().Where(a => a.PLANT_ID == machine_list2.plant_id).ToList();
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryListByPlant(226,machine_list2.plant_id), "document_numbring_id", "category");

            ViewBag.plantlist21 = new SelectList(plantlist2, "PLANT_ID", "PLANT_NAME");   
            ViewBag.machineLists = machine_list;
            ViewBag.machinelist = new SelectList(machine_list, "machine_id", "machine_code");
            //ViewBag.machinelist = new SelectList(_Generic.GetMachineList((int)machine_id), "machine_id", "machine_code");
            ViewBag.categorylist1 = _Generic.GetCategoryListByPlant1(226).ToList();


            ViewBag.allmachinelist = _Generic.GetallMachineList();

            return View();
        }

        // POST: Notification/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(ref_pm_notification ref_pm_notification)
        {
            if (ModelState.IsValid)
            {
                 
                var s = _Notificatione.Add(ref_pm_notification);
                var userid = int.Parse(Session["User_Id"].ToString());
                if (s != "Error")
                {
                    string message = "";
                    string breakdown_start_date = ref_pm_notification.start_date.ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
                    //var breakdown_start_date = ref_pm_notification.start_date;
                    var breakdown_start_time = ref_pm_notification.start_time;
                    var machine = _Generic.GetMachineList(ref_pm_notification.machine_id);
                    var Operator = _Generic.GetEMpOperator(ref_pm_notification.employee_id);
                    var shift = _Generic.Get_PlantShift(ref_pm_notification.plant_id,ref_pm_notification.start_time);
                    var moblie_number = _Generic.Get_UserMobileNumber(machine[0].plant_id);
                    message = "M/C Malfunction Notification%nM/C : "+ machine[0].machine_name +" %nMalfunction raised Date %26 Time: " + breakdown_start_date + " " + breakdown_start_time + " %nPlant %26 Shift: " + shift + " %nOperator: " + Operator;
                    //"M/C Malfunction Notification " + System.Environment.NewLine + "M/C : " + machine[0].machine_name + System.Environment.NewLine + "Malfunction raised Date & Time : " + breakdown_start_date + " " + breakdown_start_time + System.Environment.NewLine + "Plant & Shift : " + shift + System.Environment.NewLine + "Operator : " + Operator ;
                     //var moblie_number = "8390159186,8275517261";
                    var reconcile = _SMS.sendSMS(moblie_number, message);
                    TempData["saved"] = s;
                    return RedirectToAction("Index");
                }
            }
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
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(226), "document_numbring_id", "category");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.plantlist21 = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");

            ViewBag.employeelist = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.notificationtypelist = new SelectList(_Generic.GetNotificationType(), "NOTIFICATION_ID", "NOTIFICATION_TYPE");
            ViewBag.machinelist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.machinedesc = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_name");
            ViewBag.statuslist = new SelectList(_Generic.GetStatusList("MALNOTIFICATION").Where(x => x.status_id == ref_pm_notification.status_id).ToList(), "status_id", "status_name");
            ViewBag.opertorlist = new SelectList(_Generic.GetOpertorList(0), "employee_id", "employee_code");
            var userlist1 = _Generic.GetUserList().Where(a => a.user_id == user_id).ToList();
            ViewBag.userlist1 = new SelectList(userlist1, "user_id", "user_name");
            //var plantlist2 = _Generic.GetPlantList().Where(a => a.PLANT_ID == machine_list2.plant_id).ToList();

            //ViewBag.plantlist21 = new SelectList(plantlist2, "PLANT_ID", "PLANT_NAME");

            return View(ref_pm_notification);
        }

        // GET: Notification/Edit/5
        public ActionResult Edit(int? id)
        {

            var stattusid = _scifferContext.ref_pm_notification.FirstOrDefault(c => c.notification_id == id);
            ViewBag.currentstatuslist = new SelectList(_Generic.GetStatusList("MALNOTIFICATION").Where(x => x.status_id == stattusid.status_id).ToList(), "status_id", "status_name");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user_id = int.Parse(Session["User_Id"].ToString());

            ref_pm_notification ref_pm_notification = _Notificatione.Get((int)id);
            if(ref_pm_notification.under_taken_by_id!=null)
            {
                ref_pm_notification.under_taken_by_id = ref_pm_notification.under_taken_by_id;
                var userlist1 = _Generic.GetUserList().Where(a => a.user_id == ref_pm_notification.under_taken_by_id).ToList();
                ViewBag.userlist1 = new SelectList(userlist1, "user_id", "user_name");
            }
            else
            {
                //var user_id = int.Parse(Session["User_Id"].ToString());
                var userlist1 = _Generic.GetUserList().Where(a => a.user_id == user_id).ToList();
                ViewBag.userlist1 = new SelectList(userlist1, "user_id", "user_name");
            }
            if (ref_pm_notification == null)
            {
                return HttpNotFound();
            }

            var checkoperator = _login.CheckOperatorLogin(user_id, "PROD_OP");
            if (checkoperator == true)
            {
                ViewBag.role = "Operator";
            }
            else
            {
                ViewBag.role = "Not Operator";
            }
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryListByModule("MALNOTIFICATION"), "document_numbring_id", "category");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.employeelist = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.notificationtypelist = new SelectList(_Generic.GetNotificationType(), "NOTIFICATION_ID", "NOTIFICATION_TYPE");
            ViewBag.machinelist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.machinedesc = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_name");
            ViewBag.userlist = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            //ViewBag.userlist = _Generic.GetUserList().Select(a => new { a.user_id, a.user_name }).Where(a => a.user_id != user_id);

            //ViewBag.userlist1 = _Generic.GetUserList().Where(a => a.user_id == user_id).Select(a => new { a.user_id, a.user_name }).FirstOrDefault();
          
            ViewBag.statuslist = new SelectList(_Generic.GetStatusList("MALNOTIFICATION").Where(x=>x.status_id== ref_pm_notification.status_id).ToList(), "status_id", "status_name");
            ViewBag.opertorlist= new SelectList(_Generic.GetOpertorList(0), "employee_id", "employee_code");

            return View(ref_pm_notification);
        }

        // POST: Notification/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_pm_notification ref_pm_notification)
        {
            if (ModelState.IsValid)
            {
                var s = "";
                if (ref_pm_notification.status_id==2099)
                {
                    if (ref_pm_notification.detail_problem == null)
                    {
                        TempData["saved"] = "Please Enter Detail Problem..!" + "$"+ ref_pm_notification.notification_id;
                        return RedirectToAction("Index");

                    }
                    else if (ref_pm_notification.reviewed_by_id == null)
                    {
                        TempData["saved"] = "Please select  Reviewed By..!" + "$" + ref_pm_notification.notification_id;
                        return RedirectToAction("Index");
                    }

                }
                s = _Notificatione.Add(ref_pm_notification);
                var userid = int.Parse(Session["User_Id"].ToString());
                var NotificationType = _scifferContext.REF_NOTIFICATION_TYPE.Where(x => x.NOTIFICATION_ID == ref_pm_notification.notification_type).FirstOrDefault();

                var status = _scifferContext.ref_status.Where(x => x.status_id == ref_pm_notification.status_id).FirstOrDefault();
                if (s != "Error")
                {
                    if(status.status_name == "Under Service")
                    {
                        string message = "";
                        String breakdown_start_date = Convert.ToDateTime(ref_pm_notification.attending_date).ToString("dd/MMM/yyyy");
                        var breakdown_start_time = ref_pm_notification.attending_time;
                        var machine = _Generic.GetMachineList(ref_pm_notification.machine_id);
                        int undertakenbyid = (int)ref_pm_notification.under_taken_by_id;
                        var Operator = _Generic.GetOperator(undertakenbyid);
                        var shift = _Generic.Get_PlantShift(ref_pm_notification.plant_id, ref_pm_notification.breakdown_end_time);
                        var moblie_number = _Generic.Get_UserMobileNumber(machine[0].plant_id);
                        NotificationType = _scifferContext.REF_NOTIFICATION_TYPE.Where(x => x.NOTIFICATION_ID == ref_pm_notification.notification_type).FirstOrDefault();
                        message = "M/C Under Service %nM/C: " + machine[0].machine_name + " %nNotification Type: " + NotificationType.NOTIFICATION_TYPE + " %nM/C Undertaken By: " + Operator + " %nAttending Date %26 Time: "+ breakdown_start_date + " " + breakdown_start_time;
                                               //"M/C Under Service%nM/C: " + machine[0].machine_name + "%nNotification Type: " + NotificationType.NOTIFICATION_TYPE + "%nM/C Undertaken By: " + Operator + "%nAttending Date %26 Time: " + breakdown_start_date + " " + breakdown_start_time;
                                               //var moblie_number = "8390159186,8275517261";
                        var reconcile = _SMS.sendSMS(moblie_number, message);
                        TempData["saved"] = s;
                        return RedirectToAction("Index");
                    } 
                    else if (status.status_name == "Malfunction Close")
                    {
                        string message = "";
                        String breakdown_start_date = Convert.ToDateTime(ref_pm_notification.malfunction_closure_date).ToString("dd/MMM/yyyy");
                        var breakdown_start_time = ref_pm_notification.malfunction_closure_time;
                        var machine = _Generic.GetMachineList(ref_pm_notification.machine_id);
                        var attendedby_id = ref_pm_notification.attended_by_id;
                        var Attended = _Generic.GetMulOperator(attendedby_id);
                        var Operator = _Generic.GetEMpOperator((int)ref_pm_notification.operator_id);
                        var shift = _Generic.Get_PlantShift(ref_pm_notification.plant_id,(TimeSpan)ref_pm_notification.malfunction_closure_time);
                        var diff = _Generic.Get_DateDiff_for_malfunction(ref_pm_notification.notification_id);
                        var moblie_number = _Generic.Get_UserMobileNumber(machine[0].plant_id);
                         NotificationType = _scifferContext.REF_NOTIFICATION_TYPE.Where(x => x.NOTIFICATION_ID == ref_pm_notification.notification_type).FirstOrDefault();
                        message = "M/C Malfunction Closure%nM/C: "+ machine[0].machine_name +"%nMalfunction Closure Date %26 Time: " + breakdown_start_date + " " + breakdown_start_time + "%nAttended By: "+ Attended +"%nOperator Name: " + Operator + "%nPlant %26 Shift: "+ shift + "%nTotal Time taken for closure: "+ diff;
                        //"M/C Malfunction Closure " + System.Environment.NewLine + "M/C : " + machine[0].machine_name + System.Environment.NewLine + "Malfunction Closure Date & Time : " + breakdown_start_date + " " + breakdown_start_time + System.Environment.NewLine + "Attended By: " + Attended + System.Environment.NewLine + "Operator Name: " + Operator + System.Environment.NewLine + "Plant & Shift: " + shift + System.Environment.NewLine + "Total Time taken for closure: " + diff;
                        //var moblie_number = "8080231184";
                        var reconcile = _SMS.sendSMS(moblie_number, message);
                        TempData["saved"] = s;
                        return RedirectToAction("Index");
                    }

                    else if(status.status_name=="Close" && NotificationType.NOTIFICATION_TYPE == "Breakdown")
                           // else if (status.status_name == "Close")

                            {
                               

                                ref_plan_breakdown_order_VM VM = new ref_plan_breakdown_order_VM();

                        //  ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(227), "document_numbring_id", "category");

                         var categoryid1 = _scifferContext.ref_document_numbring.FirstOrDefault(x => x.module_form_id == 227);

                        var query = (from d in _scifferContext.ref_document_numbring
                                         // join pl in _scifferContext.REF_PLANT on d.plant_id equals pl.PLANT_ID
                                     join pl in _scifferContext.REF_PLANT.Where(x=>x.PLANT_ID==ref_pm_notification.plant_id) on d.plant_id equals pl.PLANT_ID

                                     join mo in _scifferContext.ref_module_form.Where(x => x.module_form_id == 227) on d.module_form_id equals mo.module_form_id
                                     join f in _scifferContext.REF_FINANCIAL_YEAR.Where(x=> x.FINANCIAL_YEAR_FROM <= DateTime.Now && x.FINANCIAL_YEAR_TO >= DateTime.Now) on d.financial_year_id equals f.FINANCIAL_YEAR_ID
                                     select new document_numbring
                                     {
                                         document_numbring_id = d.document_numbring_id,
                                         category = d.category,
                                         set_default = d.set_default,
                                     }).OrderByDescending(x => x.set_default).ToList()
                                      .Select(d => new ref_document_numbring()
                                {
                                     document_numbring_id = d.document_numbring_id,
                                     category = d.category,
                                     set_default = d.set_default,
                             }).ToList();
                    
                        VM.creation_date = DateTime.Now;
                        VM.plant_id = ref_pm_notification.plant_id;
                        var ss= ref_pm_notification.machine_id.ToString();
                        VM.machine_id_selected = ss;
                        VM.actual_start_date = ref_pm_notification.start_date;
                        VM.actual_start_time= ref_pm_notification.start_time;
                        VM.actual_finish_date = ref_pm_notification.malfunction_closure_date;
                        VM.actual_end_time = ref_pm_notification.malfunction_closure_time;
                        VM.created_by = ((int) ref_pm_notification.under_taken_by_id);
                        VM.employee_id= ((int) ref_pm_notification.under_taken_by_id);
                          VM.category_id = query.Select(x=>x.document_numbring_id).FirstOrDefault();
                        //VM.category_id = 20149;
                        VM.machine_id = ref_pm_notification.machine_id.ToString();
                        VM.remarks = "";
                        VM.maintenance_type_id = ref_pm_notification.machine_id;
                        VM.notification_description = ref_pm_notification.notification_description;
                        VM.notification_id = ref_pm_notification.notification_id;

                        var issaved = _breakdownOrder.Add(VM);

                        if (issaved != "Error")
                        {
                            TempData["doc_num"] = issaved;
                            return RedirectToAction("Index");
                        }
                        ViewBag.error = issaved;

                        TempData["saved"] = s;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["saved"] = s;
                        return RedirectToAction("Index");
                    }
                }
            }
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
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryListByModule("MALNOTIFICATION"), "document_numbring_id", "category");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.employeelist = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.notificationtypelist = new SelectList(_Generic.GetNotificationType(), "NOTIFICATION_ID", "NOTIFICATION_TYPE");
            ViewBag.machinelist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.machinedesc = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_name");
            ViewBag.statuslist = new SelectList(_Generic.GetStatusList("MALNOTIFICATION").Where(x => x.status_id == ref_pm_notification.status_id).ToList(), "status_id", "status_name");
            ViewBag.opertorlist = new SelectList(_Generic.GetOpertorList(0), "employee_id", "employee_code");
            var userlist1 = _Generic.GetUserList().Where(a => a.user_id == user_id).ToList();
            ViewBag.userlist1 = new SelectList(userlist1, "user_id", "user_name");
            ViewBag.userlist = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View(ref_pm_notification);
        }

        // GET: Notification/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_pm_notification ref_pm_notification = _Notificatione.Get((int)id);
            if (ref_pm_notification == null)
            {
                return HttpNotFound();
            }
            return View(ref_pm_notification);
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


        private GridProperties ConvertGridObject(string gridProperty)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            IEnumerable div = (IEnumerable)serializer.Deserialize(gridProperty, typeof(IEnumerable));
            GridProperties gridProp = new GridProperties();
            foreach (KeyValuePair<string, object> ds in div)
            {
                var property = gridProp.GetType().GetProperty(ds.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (property != null)
                {
                    Type type = property.PropertyType;
                    string serialize = serializer.Serialize(ds.Value);
                    object value = serializer.Deserialize(serialize, type);
                    property.SetValue(gridProp, value, null);
                }
            }
            return gridProp;
        }

        public void get_mal_notification(string GridModel)
        {
            ExcelExport exp = new ExcelExport();
            var user_id = int.Parse(Session["User_Id"].ToString());
            var DataSource = _Notificatione.get_mal_notification(user_id);
            GridProperties obj = ConvertGridObject(GridModel);
            exp.Export(obj, DataSource, "Export.xlsx", ExcelVersion.Excel2010, false, false, "bootstrap-theme");
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
