using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly IUserManagementService _userservice;
        private readonly IGenericService _Generic;

        private readonly IEmployeeService _employee;
        private readonly IDepartmentService _department;
        private readonly IBranchService _branch;

        public UserManagementController(IDepartmentService department, IBranchService branch, IUserManagementService userservice, IGenericService Generic, IEmployeeService employee)
        {
            _userservice = userservice;
            _Generic = Generic;

            _employee = employee;
            _branch = branch;
            _department = department;
        }

        [CustomAuthorizeAttribute("UPF")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _userservice.GetAll();
            ViewBag.employeelist = _employee.GetAll();
            ViewBag.branchlist = _branch.GetAll();
            ViewBag.deptlist = _department.GetAll();

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _userservice.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult InlineInsert(ref_user_management_VM value)
        {
            var add = _Generic.CheckDuplicate(value.user_code, "", "", "UserManagement", value.user_id);
            if (add == 0)
            {
                if (value.user_id == 0)
                {
                    var data1 = _userservice.Add(value);
                    //var data1 = _countryService.GetAll();
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _userservice.Update(value);
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { text = "duplicate" }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult GetEmployeeList(int id)
        {
            var list = _Generic.GetEmployeeList(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}





