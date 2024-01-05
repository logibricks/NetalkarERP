using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class ToolRenewTypeController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly IToolRenewTypeService _ToolRenewType;

        public ToolRenewTypeController(IGenericService generic, IToolRenewTypeService toolrenewtype)
        {
            _Generic = generic;
            _ToolRenewType = toolrenewtype;
        }
        // GET: ToolRenewType
        [CustomAuthorizeAttribute("TRTP")]
        public ActionResult Index()
        {
            ViewBag.Datasource = _ToolRenewType.GetAll();
            return View();
        }

        public ActionResult InlineDelete(int key)
        {
            var res = _ToolRenewType.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(ref_tool_renew_type value)
        {
            if (value.tool_renew_type_id == 0)
            {
                var data1 = _ToolRenewType.Add(value);
                if (data1.tool_renew_type_id == -1)
                {
                    return Json(new { text = "duplicate" }, JsonRequestBehavior.AllowGet);
                }
                return Json(data1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data1 = _ToolRenewType.Update(value);
                return Json(data1, JsonRequestBehavior.AllowGet);
            }
        }
    }
}