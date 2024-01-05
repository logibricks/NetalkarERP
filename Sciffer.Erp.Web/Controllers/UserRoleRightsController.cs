using System;
using Sciffer.Erp.Service.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class UserRoleRightsController : Controller
    {
        private readonly IUserRoleRightsService _rights;
        private  readonly IUserManagementRoleService _roles;

        public UserRoleRightsController(IUserRoleRightsService rights, IUserManagementRoleService roles)
        {
            _rights = rights;
            _roles = roles;
        }
        // GET: ProductionOrderNew
        [CustomAuthorizeAttribute("RORIG")]
        public ActionResult Index(string msg="")
        {
            var role = _roles.GetAll();
            ViewBag.rolelist = role;
            ViewBag.msg = msg;

            return View();
        }
       public ActionResult GetAllFromModules(int role_id)
        {
            var data = _rights.GetAllFromModules(role_id);
            return Json(data);

        }

        public ActionResult UpdateRecords(ref_user_role_rights_VM vm)
        {
            var saved = _rights.UpdateRecords(vm);
            if(saved == true)
            {
                return RedirectToAction("Index", new { msg = "saved" });
            }
            else
            {
                return RedirectToAction("Index",new { msg = "failed" });
            }
            
        }

        //public ActionResult Getrolerights(int pid)
        //{
        //    var data = _rights.Getrolerights(pid);
        //    return Json(data);
        //}

        //public double CreateOrder(int role_id)
        //{
        //    var a = _rights.CreateOrder(role_id);
        //    return a;
        //}
    }
}