using System;
using Sciffer.Erp.Service.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Web.CustomFilters;
using Sciffer.Erp.Domain.Model;

namespace Sciffer.Erp.Web.Controllers
{
    public class RoleDashboardController : Controller
    {
        private readonly IRoleDashboardService _RoleDashboard;
        private readonly IUserManagementRoleService _roles;
        public RoleDashboardController(IRoleDashboardService rights, IUserManagementRoleService roles)
        {
            _RoleDashboard = rights;
            _roles = roles;
        }
        [CustomAuthorizeAttribute("RDASH")]
        public ActionResult Index()
        {
            var role = _roles.GetAll();
            ViewBag.msg = TempData["data"];
            ViewBag.rolelist = role;
            return View();
        }
        public ActionResult GetRoleDashboard(int role_id)
        {
            var data = _RoleDashboard.GetRoleDashboard(role_id);
            return Json(data);

        }
        public ActionResult UpdateRecords(ref_role_dashboard_mapping_vm vm)
        {
            var saved = _RoleDashboard.UpdateRecords(vm);
            if (saved == true)
            {
                TempData["data"] = " Saved Successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["data"] ="Some Thing went wrong.";
                return RedirectToAction("Index");
                
            }

        }
    }
}