using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly ITaskService _taskservice;
        private readonly ILoginService _login;
        public TaskController( IGenericService genericService, ITaskService taskservice, ILoginService login)
        {
            _Generic = genericService;
            _taskservice = taskservice;
            _login = login;
        }
        // GET: Task
        public ActionResult Index()
        {
            int create_user = int.Parse(Session["User_Id"].ToString());
            var open_cnt = _login.GetOpentaskcount(create_user);
            var overdue_cnt = _login.GetOverduetask(create_user);
            Session["open_count"] = open_cnt;
            Session["overdue_count"] = overdue_cnt;
            ViewBag.doc = TempData["saved"];
           // ViewBag.DataSource = _taskservice.GetAll();        
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryListByModule("MCAL"), "document_numbring_id", "category");
            ViewBag.Tasktypelist = new SelectList( _Generic.GetTaskTypeList(), "task_type_id", "task_type");
            ViewBag.Custlist = _Generic.GetEmployeeCode();
            ViewBag.Periodiclist = new SelectList(_Generic.GetPeriodicitylist(), "task_periodicity_id", "task_periodicity_name");
            return View();
        }
        [HttpPost]
        public ActionResult Create(ref_task_vm task, FormCollection fc)
        {
           var issaved = _taskservice.Add(task);
            if (issaved.Contains("Saved"))
            {
                if (task.task_id==0)
                {
                    TempData["saved"] = issaved.Split('~')[1] + " Saved Successfully.";

                }
                else
                {
                    TempData["saved"] = issaved.Split('~')[1] + " Updated Successfully.";

                }
                return RedirectToAction("Index");
            }
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryListByModule("MCAL"), "document_numbring_id", "category");
            ViewBag.Tasktypelist = new SelectList(_Generic.GetTaskTypeList(), "task_type_id", "task_type");
            ViewBag.Custlist = _Generic.GetEmployeeCode();

            //ViewBag.Custlist = new SelectList(_Generic.GetEmployeeCode(), "employee_id", "employee_code");
            // ViewBag.Custlist1 = new SelectList(_Generic.GetEmployeeCode(), "employee_id", "employee_code");

            ViewBag.Periodiclist = new SelectList(_Generic.GetPeriodicitylist(), "task_periodicity_id", "task_periodicity_name");
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = _taskservice.Get((int)id);
            
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryListByModule("MCAL"), "document_numbring_id", "category");
            ViewBag.Tasktypelist = new SelectList(_Generic.GetTaskTypeList(), "task_type_id", "task_type");
            ViewBag.Custlist = new SelectList(_Generic.GetEmployeeCode(), "employee_id", "employee_code");        

            ViewBag.Periodiclist = new SelectList(_Generic.GetPeriodicitylist(), "task_periodicity_id", "task_periodicity_name");
            ViewBag.StatusList = new SelectList(_Generic.GetStatusList("MCAL"), "status_id", "status_name");
            return View(data);
        }
       
        public FileResult Download(string controller_name, int id)
        {
            var notification = _taskservice.Get(id).attachment;
            if (notification == "No File")
            {
                return null;
            }
            else
            {


                var file = notification.Split('/').Last();
                var paths = "~/Files/" + controller_name + "/" + file;
                var path = System.Web.HttpContext.Current.Server.MapPath("~/Files/" + controller_name + "/" + file);
                byte[] fileBytes = System.IO.File.ReadAllBytes(path);

                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, file);
            }
        }
       
        public FileResult GridDownload(string controller_name, int id)
        {
            var notification = _taskservice.Getattachment(id).oroginal_attachment;
            if(notification== "No File")
            {
                notification = _taskservice.Getattachment(id).new_attachment;
            }
            if (notification == "No File"|| notification==null)
            {
                return null;
            }
            else
            {


                var file = notification.Split('/').Last();
                var paths = "~/Files/" + controller_name + "/" + file;
                var path = System.Web.HttpContext.Current.Server.MapPath("~/Files/" + controller_name + "/" + file);
                byte[] fileBytes = System.IO.File.ReadAllBytes(path);

                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, file);
            }
        }

        public FileResult Download1(string controller_name, int id)
        {
            var notification = _taskservice.Get(id).new_attachment;
            if (notification == "No File" || notification == null)
            {
                return null;
            }
            else
            {
                var file = notification.Split('/').Last();
                var paths = "~/Files/" + controller_name + "/" + file;
                var path = System.Web.HttpContext.Current.Server.MapPath("~/Files/" + controller_name + "/" + file);
                byte[] fileBytes = System.IO.File.ReadAllBytes(path);

                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, file);
            }
        }

        public FileResult GridDownload1(string controller_name, int id)
        {
            var notification = _taskservice.Getattachment(id).new_attachment;
            if (notification == "No File")
            {
                return null;
            }
            else
            {
                var file = notification.Split('/').Last();
                var paths = "~/Files/" + controller_name + "/" + file;
                var path = System.Web.HttpContext.Current.Server.MapPath("~/Files/" + controller_name + "/" + file);
                byte[] fileBytes = System.IO.File.ReadAllBytes(path);

                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, file);
            }
        }
    }
}