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

namespace Sciffer.Erp.Web.Controllers
{
    public class WorkFlowController : Controller
    {
        private ScifferContext db = new ScifferContext();
        private readonly IDocumentTypeService _documentTypeService;
        private readonly IWorkflowService _workflowService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;

        public WorkFlowController(IUserService UserService,IDocumentTypeService DocumentTypeService, IWorkflowService WorkflowService, ICategoryService CategoryService)
        {
            _documentTypeService = DocumentTypeService;
            _workflowService = WorkflowService;
            _categoryService = CategoryService;
            _userService = UserService;
        }
        // GET: WorkFlow
        public ActionResult Index()
        {
            var ref_workflow = db.ref_workflow.Include(r => r.REF_CATEGORY).Include(r => r.ref_document_type);
            return View(ref_workflow.ToList());
        }
       
        // GET: WorkFlow/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_workflow ref_workflow = db.ref_workflow.Find(id);
            if (ref_workflow == null)
            {
                return HttpNotFound();
            }
            return View(ref_workflow);
        }

        // GET: WorkFlow/Create
        public ActionResult Create()
        {
            ViewBag.user_list = new SelectList(_userService.GetAll(), "USER_ID", "USER_NAME");
            ViewBag.category_list = new SelectList(_categoryService.GetAll(), "CATEGORY_ID", "CATEGORY_NAME");
            ViewBag.document_type_list = new SelectList(_documentTypeService.GetAll(), "document_type_id", "document_type_name");
            return View();
        }

        // POST: WorkFlow/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(workflowVM workflow,FormCollection fc)
        {
            string  setname, userid;
            setname = fc["setName"];
            userid = fc["userId"];
            var user1 = userid.Split(',');
            var setname1 = setname.Split(',');
            List<ref_workflow_approval> app = new List<ref_workflow_approval>();
          
            foreach (var ar1 in user1)
            {
                var ar2 = ar1.Split('~');
                foreach (var ar3 in ar2)
                {
                    ref_workflow_approval a = new ref_workflow_approval();
                    var ar4 = ar3.Split('-');
                    var check = ar4[1];
                    if (check == "~")
                    {
                        continue;
                    }
                    else
                    {
                        a.user_id = Convert.ToInt32(ar4[1]);
                        app.Add(a);
                    }
                }
            }
            workflow.ref_workflow_approval = app;
            List<ref_workflow_detail> detail = new List<ref_workflow_detail>();
            foreach (var ar1 in setname1)
            {
                var ar2 = ar1.Split('~');
                ref_workflow_detail d = new ref_workflow_detail();
                d.approval_set_no = Convert.ToInt32(ar2[0]);
                d.approval_set_name = ar2[0];
                detail.Add(d);
            }
            workflow.ref_workflow_detail = detail;
            if (ModelState.IsValid)
            {
               
                return RedirectToAction("Index");
            }

            ViewBag.user_list = new SelectList(_userService.GetAll(), "USER_ID", "USER_NAME");
            ViewBag.category_list = new SelectList(_categoryService.GetAll(), "CATEGORY_ID", "CATEGORY_NAME");
            ViewBag.document_type_list = new SelectList(_documentTypeService.GetAll(), "document_type_id", "document_type_name");
            return View(workflow);
        }

        // GET: WorkFlow/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_workflow ref_workflow = db.ref_workflow.Find(id);
            if (ref_workflow == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.REF_CATEGORY, "CATEGORY_ID", "CATEGORY_NAME", ref_workflow.category_id);
            ViewBag.document_type_id = new SelectList(db.ref_document_type, "document_type_id", "document_type_name", ref_workflow.document_type_id);
            return View(ref_workflow);
        }

        // POST: WorkFlow/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "workflow_id,document_type_id,category_id,has_value,value_from,value_to,no_of_approval,is_active")] ref_workflow ref_workflow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ref_workflow).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_id = new SelectList(db.REF_CATEGORY, "CATEGORY_ID", "CATEGORY_NAME", ref_workflow.category_id);
            ViewBag.document_type_id = new SelectList(db.ref_document_type, "document_type_id", "document_type_name", ref_workflow.document_type_id);
            return View(ref_workflow);
        }

        // GET: WorkFlow/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_workflow ref_workflow = db.ref_workflow.Find(id);
            if (ref_workflow == null)
            {
                return HttpNotFound();
            }
            return View(ref_workflow);
        }

        // POST: WorkFlow/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ref_workflow ref_workflow = db.ref_workflow.Find(id);
            db.ref_workflow.Remove(ref_workflow);
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
    }
}
