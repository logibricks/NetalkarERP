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
    public class ItemGroupController : Controller
    {
        private readonly IItemGroupService _itemService;
        private readonly IGenericService _Generic;
        public ItemGroupController(IItemGroupService itemService, IGenericService gen)
        {
            _itemService = itemService;
            _Generic = gen;
        }

        // GET: ItemGroup
        [CustomAuthorizeAttribute("ITMGP")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _itemService.GetAll();
            return View();
        }
       

        public ActionResult InlineDelete(int key)
        {
            var res = _itemService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(REF_ITEM_GROUP value)
        {

            var add = _Generic.CheckDuplicate(value.ITEM_GROUP_NAME, value.ITEM_GROUP_DESCRIPTION,"", "itemgroup", value.ITEM_GROUP_ID);
            if (add == 0)
            {
                if (value.ITEM_GROUP_ID == 0)
                {
                    var data1 = _itemService.Add(value);
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _itemService.Update(value);
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { text = "duplicate" }, JsonRequestBehavior.AllowGet);

            }
        }
       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _itemService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
