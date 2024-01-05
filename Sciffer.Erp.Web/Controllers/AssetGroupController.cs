using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Web.CustomFilters;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Web.Controllers
{
    public class AssetGroupController : Controller
    {
        private readonly IGenericService _genericservice;
        private readonly IAssetGroupService _assetgroup;

        public AssetGroupController(IGenericService GenericService, IAssetGroupService assetgroup)
        {
            _genericservice = GenericService;
            _assetgroup = assetgroup;

        }

        // GET: AssetGroup
        public ActionResult Index()
        {
            ViewBag.datasource = _assetgroup.GetAll();
            ViewBag.assetclass = _genericservice.GetAssetClass().Select(a => new { a.asset_class_id, asset_class_code = a.asset_class_code +"/"+ a.asset_class_des }).ToList();
            return View();
        }

        public ActionResult InlineInsert(ref_asset_group value)
        {

            var add = _genericservice.CheckDuplicate(value.asset_group_code, "", "", "AssetGroup", value.asset_group_id);
            if (add == 0)
            {
                if (value.asset_group_id == 0)
                {
                    var data1 = _assetgroup.Add(value);
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _assetgroup.Update(value);
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