using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using System;
using Syncfusion.JavaScript.Models;
using Syncfusion.EJ.Export;
using System.Web.Script.Serialization;
using System.Collections;
using System.Collections.Generic;
using Syncfusion.XlsIO;
using System.Reflection;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
    {
        public class UserManagementRoleController : Controller
        {
            private readonly IUserManagementRoleService _userservice;
            private readonly IGenericService _Generic;
            public UserManagementRoleController(IUserManagementRoleService userservice, IGenericService Generic)
            {
              _userservice = userservice;
                _Generic = Generic;
            }

        [CustomAuthorizeAttribute("ROPF")]
        public ActionResult Index()
            {
                ViewBag.DataSource = _userservice.GetAll();
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
            public ActionResult InlineInsert(ref_user_management_role value)
            {

                var add = _Generic.CheckDuplicate(value.role_code, "", "", "customercategory", value.role_id);
                if (add == 0)
                {
                    if (value.role_id == 0)
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
        }
    }








