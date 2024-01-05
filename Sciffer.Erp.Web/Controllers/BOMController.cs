using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Sciffer.Erp.Web.Controllers
{
    public class BOMController : Controller
    {
        private readonly IBomService _bomservice;
        private readonly IGenericService _Generic;
        private readonly IUOMService _uOMService;
        private readonly IItemCategoryService _itemcat;

        public BOMController(IBomService BomService, IGenericService Generic, IUOMService UOMService, IItemCategoryService Itemcat)
        {
            _bomservice = BomService;
            _Generic = Generic;
            _uOMService = UOMService;
            _itemcat=Itemcat;           
        }
        [CustomAuthorizeAttribute("BM")]
        public ActionResult Index()
        {
            ViewBag.num = TempData["data"];
            ViewBag.DataSource = _bomservice.getall();
            return View();
        }
        [CustomAuthorizeAttribute("BM")]
        // GET: Create
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.CategoryList = new SelectList(_Generic.GetCategoryList(94), "document_numbring_id", "category");
            ViewBag.ItemList= new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.UnitList = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.GetItemCatList = new SelectList(_Generic.GetItemCategoryList(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_mfg_bom_VM vm)
        {
            if (ModelState.IsValid)
            {
                var isValid = _bomservice.Add(vm);
                if (isValid != "Error")
                {
                    TempData["data"] = isValid;
                    return RedirectToAction("Index", TempData["data"]);
                }
                ViewBag.error = "Something went wrong !";
            }

            ViewBag.CategoryList = new SelectList(_Generic.GetCategoryList(94), "document_numbring_id", "category");
            ViewBag.ItemList = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.UnitList = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.GetItemCatList = new SelectList(_Generic.GetItemCategoryList(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            return View(vm);
        }
        [CustomAuthorizeAttribute("BM")]
        //Get: Edit
        public ActionResult Edit(int id)
        {
            ref_mfg_bom_VM bomvm = _bomservice.Get(id);

            if (bomvm == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryList = new SelectList(_Generic.GetCategoryList(94), "document_numbring_id", "category");
            ViewBag.ItemList = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.UnitList = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");            
            ViewBag.GetItemCatList = new SelectList(_Generic.GetItemCategoryList(), "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            return View(bomvm);
        }

        [HttpPost]
        public ActionResult Edit(ref_mfg_bom_VM vm)
        {
            bool result = _bomservice.Update(vm);
            if (result == true)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]

        public ActionResult GetItemList(string itemGroupid)
        {            
            int ItemGroupID = Convert.ToInt32(itemGroupid);            
            var lstitem = _Generic.GetCatWiesItemList(ItemGroupID);
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string result = javaScriptSerializer.Serialize(lstitem);
            return Json(result, JsonRequestBehavior.AllowGet);            
        }

    }
}