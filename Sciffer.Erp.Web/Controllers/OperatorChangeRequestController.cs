using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.ViewModel;
using System.Net;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class OperatorChangeRequestController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly ISkillMatrixService _SkillMatrix;
        private readonly IOperatorChangeRequestService _opchangreq;
        private readonly ILoginService _login;

        public OperatorChangeRequestController(IGenericService gen, ISkillMatrixService SkillMatrix, IOperatorChangeRequestService opchangreq, ILoginService login)
        {
            _Generic = gen;
            _SkillMatrix = SkillMatrix;
            _opchangreq = opchangreq;
            _login = login;
        }
        // GET: OperatorChangeRequest
        public ActionResult Index()
        {
            ViewBag.num = TempData["data"];
            ViewBag.DataSource = _SkillMatrix.GetAll("operator_change_req_index_list",0);
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.category_list = new SelectList(_Generic.GetCategoryListByModule("OPCREQ"), "document_numbring_id", "category");
            ViewBag.operator_list = new SelectList(_SkillMatrix.GetAll("get_operator_drpdwn",0), "user_id", "user_code");
            ViewBag.level_list = new SelectList(_Generic.GetLevelList(), "level_id", "level_code");
            return View();
        }

        [HttpPost]
        
        public ActionResult Create(operator_change_req_vm vm)
        {
            if (ModelState.IsValid)
            {
                var issaved = _opchangreq.Add(vm);
                if (issaved != "Error")
                {
                    TempData["data"] = issaved;
                    return RedirectToAction("Index", TempData["data"]);
                }
                ViewBag.error = "Something went wrong !";
            }
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    var er = error.ErrorMessage;
                }
            }

            ViewBag.category_list = new SelectList(_Generic.GetCategoryListByModule("OPCREQ"), "document_numbring_id", "category");
            ViewBag.operator_list = new SelectList(_SkillMatrix.GetAll("get_operator_drpdwn", 0), "user_id", "user_code");
            ViewBag.level_list = new SelectList(_Generic.GetLevelList(), "level_id", "level_code");
            return View(vm);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            operator_change_req_vm edit_list = _opchangreq.Get(id);

            if (edit_list == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_list = new SelectList(_Generic.GetCategoryListByModule("OPCREQ"), "document_numbring_id", "category");
            ViewBag.operator_list = new SelectList(_SkillMatrix.GetAll("get_operator_drpdwn", 0), "user_id", "user_code");
            ViewBag.level_list = new SelectList(_Generic.GetLevelList(), "level_id", "level_code");
            return View(edit_list);
        }

        [CustomAuthorizeAttribute("OPCREQA")]
        public ActionResult OperatorChangeRequestApproval()
        {            

            ViewBag.DataSource = _opchangreq.GetAll("getOpChangeReqApprovalList", 0) ;
            ViewBag.status_list =_opchangreq.GetAll("get_statuslist_ocr",0);
            return View();
        }
        public ActionResult GetApprovedStatus(int id)
        {
            List<SelectListItem> items = new List<SelectListItem>();
         
            var data = _opchangreq.GetAll("getOpChangeReqApprovalList",0);
            ViewBag.datasource = data;
            ViewBag.status_list = _opchangreq.GetAll("get_statuslist_ocr", 0); ;
            return PartialView("Partial_ApprovalStatus", data);
        }


        [HttpPost]
        public ActionResult ChangeApprovedStatus(operator_change_req_vm value)
        {
            var change = _opchangreq.ChangeApprovedStatus("approval_status", value.operator_change_request_detail_id, value.approval_status_id, value.approval_comments);
            return RedirectToAction("Index");
        }
    }
}