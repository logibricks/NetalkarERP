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
    public class ToolMasterController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly IToolMasterService _ToolMaster;

        public ToolMasterController(IGenericService generic, IToolMasterService toolmaster)
        {
            _Generic = generic;
            _ToolMaster = toolmaster;
        }
        // GET: ToolMaster
        [CustomAuthorizeAttribute("TM")]
        public ActionResult Index()
        {
            ViewBag.Datasource = _ToolMaster.GetAll();
            ViewBag.ItemList = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            return View();
        }

        public ActionResult InlineDelete(int key)
        {
            var res = _ToolMaster.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(ref_tool_VM value)
        {
            if (value.tool_id == 0)
            {
                var data1 = _ToolMaster.Add(value);
                if (data1.tool_id == -1)
                {
                    return Json(new { text = "duplicate" }, JsonRequestBehavior.AllowGet);
                }
                return Json(data1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data1 = _ToolMaster.Update(value);
                return Json(data1, JsonRequestBehavior.AllowGet);
            }
        }

    }
}