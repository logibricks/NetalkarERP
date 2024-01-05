using Sciffer.Erp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Security.Policy;

namespace Sciffer.Erp.Web.CustomFilters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        ScifferContext context = new ScifferContext(); // my entity  
        private readonly string[] allowedmodule;

        public string View { get; set; }
        public CustomAuthorizeAttribute(params string[] modulename)
        {
            View = "AuthorizeFailed";
            this.allowedmodule = modulename;

        }
        public static string GetCurrentWebsiteRoot()
        {
            return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

             bool authorize = false;
            if (HttpContext.Current.Session["User_Id"] == null)
            {
                HttpContext ctx = HttpContext.Current;
                UrlHelper url = new UrlHelper();
                string s = GetCurrentWebsiteRoot();
                ctx.Response.Redirect(s + "/LogibricksERP/Login?msg=expired");
            }
            else
            {
                var ss = HttpContext.Current.Session["User_Id"].ToString();

                var user_id = Convert.ToInt32(ss);
                //For now do not check and allow all

                foreach (var module in allowedmodule)
                {
                    var role_id = context.ref_user_role_mapping.Where(x => x.user_id == user_id).Select(z => z.role_id).ToList();

                    if (module == "Home")
                    {
                        return true;
                    }
                    else if (module == "OPTR_BLOCK")
                    {
                        foreach (var r in role_id)
                        {
                            var role_code = context.ref_user_management_role.FirstOrDefault(x => x.role_id == r).role_code;
                            if (role_code == "PROD_OP")
                            {
                                authorize = false;
                            }
                            else
                            {
                                authorize = true;
                            }
                        }
                    }
                    else if (module == "QA_OP_BLOCK")
                    {
                        foreach (var r in role_id)
                        {
                            var role_code = context.ref_user_management_role.FirstOrDefault(x => x.role_id == r).role_code;
                            if (role_code == "QA_OP")
                            {
                                authorize = false;
                            }
                            else
                            {
                                authorize = true;
                            }
                        }
                    }
                    else
                    {
                        var module_form_id = context.ref_module_form.Where(x => x.module_form_code == module).FirstOrDefault();

                        var rd = httpContext.Request.RequestContext.RouteData;

                        string currentAction = rd.GetRequiredString("action");

                        foreach (var r in role_id)
                        {

                            var role_code = context.ref_user_management_role.FirstOrDefault(x => x.role_id == r).role_code;
                            if (role_code == "TOP_MGMT")
                            {
                                return true;
                            }
                            try
                            {
                                var rights = context.ref_user_role_rights.Where(x => x.role_id == r && x.module_form_id == module_form_id.module_form_id).FirstOrDefault();

                                if (currentAction == "Index")
                                {
                                    if (rights.view_rights)
                                        authorize = true;
                                }
                                if (currentAction == "Edit")
                                {
                                    if (rights.edit_rights)
                                        authorize = true;
                                }
                                if (currentAction == "Create")
                                {
                                    if (rights.create_rights)
                                        authorize = true;
                                }
                                if (currentAction == "Details")
                                {
                                    if (rights.view_rights)
                                        authorize = true;
                                }
                                if (currentAction == "approvedpurchaseorder")
                                {
                                    if (rights.view_rights && rights.create_rights && rights.edit_rights)
                                        authorize = true;
                                }
                                if (currentAction == "ApprovedPurchaseReq")
                                {
                                    if (rights.view_rights && rights.create_rights && rights.edit_rights)
                                        authorize = true;
                                }

                                if (currentAction == "OperatorChangeRequestApproval")
                                {
                                    if (rights.view_rights && rights.create_rights && rights.edit_rights)
                                        authorize = true;
                                }
                                if (currentAction == "SaveLevel")
                                {
                                    if (rights.create_rights && rights.edit_rights)
                                        authorize = true;
                                }if (currentAction == "SaveOperatorLevel")
                                {
                                    if (rights.create_rights && rights.edit_rights)
                                        authorize = true;
                                }
                                if (currentAction == "SaveMachineLevel")
                                {
                                    if (rights.create_rights && rights.edit_rights)
                                        authorize = true;
                                }
                                if (currentAction == "UpdateOperatorOperationMapping")
                                {
                                    if (rights.create_rights && rights.edit_rights)
                                        authorize = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                authorize = false;
                            }

                        }
                    }
                }

            }
            return authorize;

        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var ss = HttpContext.Current.Session["User_Id"].ToString();
            var user_id = Convert.ToInt32(ss);

            var vr = new ViewResult();
            vr.ViewName = View;

            ViewDataDictionary dict = new ViewDataDictionary();
            dict.Add("Message", "Sorry you are not Authorized to Perform this Action");

            var role_id = context.ref_user_management_role.FirstOrDefault(x => x.role_code == "PROD_OP").role_id;
            var role = context.ref_user_role_mapping.Where(x => x.user_id == user_id && x.role_id == role_id).FirstOrDefault();
            if (role != null)
            {
                dict.Add("OperatorCheck", "1");
            }
            else
            {
                dict.Add("OperatorCheck", "0");
            }

            vr.ViewData = dict;
            var result = vr;

            filterContext.Result = result;

        }

    }
}