using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class CapitalizationController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly ICapitalizationService _cap;

        public CapitalizationController(IGenericService GenericService, ICapitalizationService cap)
        {
           
            _Generic = GenericService;
            _cap = cap;
        }

        // GET: Capitalization
        public ActionResult Index()
        {
            ViewBag.num = "";
            ViewBag.DataSource = _cap.GetAll();
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.assetcodeList = new SelectList(_Generic.GetAssetMasterDataForDropDown(), "asset_master_data_id", "asset_master_data_desc");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryListByModule("Capitalization"), "document_numbring_id", "category");
            ViewBag.entity_type_list = new SelectList(_Generic.GetEntityList(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");
            return View();
        }


        public ActionResult Save(fin_ledger_capitalization_vm Capdata, List<fin_ledger_capitalization_detail_vm> DepParaArr)
        {
            var Cap = _cap.Add(Capdata, DepParaArr);
            if(Cap.Contains("Duplicate"))
            {

                TempData["doc_num"] = Cap;
                return Json(Cap, JsonRequestBehavior.AllowGet);
            }else if (Cap.Contains("Saved"))
            {

                TempData["doc_num"] = Cap;
                return Json(Cap, JsonRequestBehavior.AllowGet);
            }else if (Cap.Contains("Error"))
            {

                TempData["doc_num"] = Cap;
                return Json(Cap, JsonRequestBehavior.AllowGet);
            }

            ViewBag.assetcodeList = _Generic.GetAssetMasterData().Select(a => new { a.asset_master_data_id, a.asset_master_data_code  }).ToList();
            ViewBag.category_list = new SelectList(_Generic.GetCategoryListByModule("Capitalization"), "document_numbring_id", "category");
            ViewBag.entity_type_list = new SelectList(_Generic.GetEntityList(), "ENTITY_TYPE_ID", "ENTITY_TYPE_NAME");
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fin_ledger_capitalization_vm vm = _cap.Get((int)id);
            if (vm == null)
            {
                return HttpNotFound();
            }

            ViewBag.assetcodeList = new SelectList(_Generic.GetAssetMasterDataForDropDown(), "asset_master_data_id", "asset_master_data_desc");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryListByModule("Capitalization"), "document_numbring_id", "category");
            ViewBag.entity_type_list = new SelectList(_Generic.GetGLForSearchingDropdown(), "gl_ledger_id", "gl_ledger_code");
            return View(vm);
        }

        public ActionResult DeleteConfirmed(int id, string cancellation_remarks)
        {
            var isValid = _cap.Delete(id, cancellation_remarks);
            if (isValid.Contains("Cancelled"))
            {
                return Json(isValid);
            }
            return Json(isValid);
        }
    }
}