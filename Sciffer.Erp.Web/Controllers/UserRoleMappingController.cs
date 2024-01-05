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
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class UserRoleMappingController : Controller
    {
        private readonly IUserRoleMappingService _mapping;
        private readonly IUserManagementService _user;
        private readonly IUserManagementRoleService _role;

        public UserRoleMappingController(IUserRoleMappingService mapping, IUserManagementService user, IUserManagementRoleService role)
        {
            _mapping = mapping;
            _user = user;
            _role = role;

        }
        [CustomAuthorizeAttribute("USROM")]
        // GET: Machine
        public ActionResult Index()
        {
            ViewBag.DataSource = _mapping.GetAll();
            return View();
        }

        // GET: Machine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_user_role_mapping_VM ref_user_role_mapping_VM = _mapping.Get((int)id);
            if (ref_user_role_mapping_VM == null)
            {
                return HttpNotFound();
            }

            ViewBag.rolelist = new SelectList(_role.GetAll(), "role_id", "role_name");
            ViewBag.userlist = new SelectList(_user.GetAll(), "user_id", "user_name");

            return View(ref_user_role_mapping_VM);
        }

        // GET: Machine/Create
        public ActionResult Create()
        {
            ViewBag.rolelist = new SelectList(_role.GetAll(), "role_id", "role_name");
            ViewBag.userlist = new SelectList(_user.GetAll(), "user_id", "user_name");
            return View();
        }


        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_user_role_mapping_VM ref_User_role_mapping_VM, FormCollection fc)
        {

            if (ModelState.IsValid)
            {
                var issaved = _mapping.Add(ref_User_role_mapping_VM);
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
            ViewBag.rolelist = new SelectList(_role.GetAll(), "role_id", "role_name");
            ViewBag.userlist = new SelectList(_user.GetAll(), "user_id", "user_name");
            return View(ref_User_role_mapping_VM);


        }


        // GET: Machine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_user_role_mapping_VM ref_User_role_mapping_VM = _mapping.Get((int)id);
            if (ref_User_role_mapping_VM == null)
            {
                return HttpNotFound();
            }
            ViewBag.rolelist = new SelectList(_role.GetAll(), "role_id", "role_name");
            ViewBag.userlist = new SelectList(_user.GetAll(), "user_id", "user_name");

            return View(ref_User_role_mapping_VM);
        }

        // POST: Machine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_user_role_mapping_VM ref_User_role_mapping_VM, FormCollection fc)
        {

            string products, deleteids;
            products = fc["productdetail"];
            deleteids = fc["deleteids"];
            string[] emptyStringArray = new string[0];

            try
            {
                emptyStringArray = products.Split(new string[] { "~" }, StringSplitOptions.None);

            }
            catch (Exception e)
            {

            }
            if (ModelState.IsValid)
            {
                var isedited = _mapping.Update(ref_User_role_mapping_VM);
                if (isedited == true)
                    return RedirectToAction("Index");
            }
            ViewBag.rolelist = new SelectList(_role.GetAll(), "role_id", "role_name");
            ViewBag.userlist = new SelectList(_user.GetAll(), "user_id", "user_name");
            return View(ref_User_role_mapping_VM);
        }


        // GET: Machine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_user_role_mapping_VM ref_mapping = _mapping.Get((int)id);
            if (ref_mapping == null)
            {
                return HttpNotFound();
            }
            return View(ref_mapping);
        }


        // POST: Machine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bool isdeleted = _mapping.Delete(id);
            if (isdeleted == true)
            {
                return RedirectToAction("Index");
            }
            ref_user_role_mapping_VM ref_mapping = _mapping.Get((int)id);
            if (ref_mapping == null)
            {
                return HttpNotFound();
            }
            return View(ref_mapping);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _mapping.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

