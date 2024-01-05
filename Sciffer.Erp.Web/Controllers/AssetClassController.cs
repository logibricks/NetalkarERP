using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Implementation;
using Sciffer.Erp.Domain.ViewModel;
using Newtonsoft.Json;
using Sciffer.Erp.Service.Interface;
using System.IO;
using Excel;
using Sciffer.Erp.Web.CustomFilters;
using System.Web.Script.Serialization;
using Syncfusion.JavaScript;

namespace Sciffer.Erp.Web.Controllers
{
    public class AssetClassController : Controller
    {
        private readonly IGenericService _genericservice;
        private readonly IAssetClassService _assetclassservice;
        public AssetClassController( IGenericService GenericService, IAssetClassService AssetClassService)
        {
            _genericservice = GenericService;
            _assetclassservice = AssetClassService;
        }

        // GET: AssetClass
        public ActionResult Index()
        {
            ViewBag.num = TempData["doc_num"];
            ViewBag.DataSource = _assetclassservice.getall();
            return View();
        }
        public ActionResult Create()
        {
            ViewBag.deparea = new SelectList(_genericservice.GetDepList(), "dep_area_id", "dep_area_code");
            ViewBag.deptype = new SelectList(_genericservice.GETDEPRECIATIONAREA(), "dep_type_id", "dep_type_code");
            ViewBag.generalleder = new SelectList(_genericservice.GetLedgerAccountOnlyActive(2), "gl_ledger_id", "gl_ledger_name");
            return View();
        }
        public ActionResult GetLedgerAccountTypeName()
        {
            var paymentService = _genericservice.GetLedgerAccountTypeName();
            return Json(paymentService, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GETDEPRECIATIONAREA()
        {
            var paymentService = _genericservice.GETDEPRECIATIONAREA();
            return Json(paymentService, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Create(ref_asset_class_vm fin_map, List<ref_asset_class_gl_vm> fin_detail,List<ref_asset_class_depreciation_vm> dip_detail)
        {
            var add = _genericservice.CheckDuplicate(fin_map.asset_class_code, "", "", "AssetClass", fin_map.asset_class_id);
            if (add == 0)
            {
                var isValid = _assetclassservice.Add(fin_map, fin_detail, dip_detail);
                if (isValid != "error")
                {
                   // TempData["doc_num"] = isValid;
                    return Json(isValid, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var msg = fin_map.asset_class_code + " already exists!!";
               // TempData["doc_num"] = msg;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            ViewBag.generalleder = new SelectList(_genericservice.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sonvm = _assetclassservice.Get((int)id);
            ViewBag.generalleder = new SelectList(_genericservice.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            return View(sonvm);
        }
    }
}