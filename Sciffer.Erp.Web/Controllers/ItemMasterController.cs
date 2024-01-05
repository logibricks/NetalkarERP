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
    public class ItemMasterController : Controller
    {
        private readonly IItemService _item_service;
        private readonly IUOMService _uomService;
        private readonly IVendorService _VendorService;
        private readonly IUserService _UserService;
        private readonly IPriorityService _Priority;
        private readonly IItemValuationService _ItemValuation;
        private readonly IBrandService _Brand;
        private readonly IExciseCategoryService _ExciseService;
        private readonly IItemAccountingService _ItemAccounting;
        private readonly IItemCategoryService _ItemCategory;
        private readonly IItemGroupService _ItemGroup;
        private readonly IItemTypeService _ItemTypeService;
        private readonly IGenericService _genericservice;
        private readonly IGeneralLedgerService _generalLedgerService;
        private readonly IShelfLifeService _shelflifeservice;
        private readonly IPlantService _plantService;
       
        string[] error = new string[30000];
        string errorMessage = "";
        int errorList = 0;
       
        public ItemMasterController(IPlantService plantservice, IGenericService GenericService, IItemTypeService ItemTypeService, ItemService ScifferContext, IUOMService uomService, VendorService VendorService, UserService UserService,
            PriorityService Priority, ItemValuationService ItemValuation, BrandService BrandService, ExciseCategoryService ExciseService, ItemAccountingService accounting,
            ItemCategoryService ItemCategory, ItemGroupService ItemGroup, IGeneralLedgerService GeneralLedgerService, IShelfLifeService shelfLifeService)
        {
            
            _plantService = plantservice;
            _item_service = ScifferContext;
            _uomService = uomService;
            _VendorService = VendorService;
            _UserService = UserService;
            _Priority = Priority;
            _ItemValuation = ItemValuation;
            _Brand = BrandService;
            _ExciseService = ExciseService;
            _ItemAccounting = accounting;
            _ItemCategory = ItemCategory;
            _ItemGroup = ItemGroup;
            _ItemTypeService = ItemTypeService;
            _genericservice = GenericService;
            _generalLedgerService = GeneralLedgerService;
            _shelflifeservice = shelfLifeService;
        }

        // GET: Items
        [CustomAuthorizeAttribute("IM")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _item_service.GetItems();
            ViewBag.doc_no = TempData["doc_num"];
            //var res = _item_service.GetItems();
            //var jsonResult = Json(res, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = Int32.MaxValue;
            //ViewBag.DataSource = jsonResult;
            return View();
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_ITEM = _item_service.Get((int)id);

            if (rEF_ITEM == null)
            {
                return HttpNotFound();
            }
            var uom = _uomService.GetAll();
            var vendor = _VendorService.GetAll();
            var user = _UserService.GetAll();
            var priority = _genericservice.GetPriorityByForm(3);
            var itemvaluation = _ItemValuation.GetAll();
            var brand = _Brand.GetAll();
            var excisecategory = _ExciseService.GetAll();
            var itemaccounting = _ItemAccounting.GetAll();
            var itemcategory = _ItemCategory.GetAll();
            var itemgroup = _ItemGroup.GetAll();
            var shelfLife = _shelflifeservice.GetAll();
            ViewBag.ShelfLifeList = new SelectList(shelfLife, "shelf_life_id", "shelf_life_name");
            ViewBag.generalleder = new SelectList(_genericservice.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.brandlist = new SelectList(brand, "BRAND_ID", "BRAND_NAME");
            ViewBag.excisecategorylist = new SelectList(excisecategory, "EXCISE_CATEGORY_ID", "EXCISE_CATEGORY_NAME");
            ViewBag.itemaccountinglist = new SelectList(itemaccounting, "ITEM_ACCOUNTING_ID", "ITEM_ACCOUNTING_NAME");
            ViewBag.itemcategorylist = new SelectList(itemcategory, "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME", rEF_ITEM.ITEM_GROUP_ID);
            ViewBag.itemgrouplist = new SelectList(itemgroup, "ITEM_GROUP_ID", "ITEM_GROUP_NAME");
            ViewBag.itemvaluationlist = new SelectList(itemvaluation, "ITEM_VALUATION_ID", "ITEM_VALUATION_NAME");
            ViewBag.prioritilist = new SelectList(priority, "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.uomlist = new SelectList(uom, "UOM_ID", "UOM_NAME");
            ViewBag.itemlengthuomlist = new SelectList(uom, "UOM_ID", "UOM_NAME");
            ViewBag.itemwidthuomlist = new SelectList(uom, "UOM_ID", "UOM_NAME");
            ViewBag.itemheightuomlist = new SelectList(uom, "UOM_ID", "UOM_NAME");
            ViewBag.itemvolumeuomlist = new SelectList(uom, "UOM_ID", "UOM_NAME");
            ViewBag.itemweightuomlist = new SelectList(uom, "UOM_ID", "UOM_NAME");
            ViewBag.selflifeuomlist = new SelectList(uom, "UOM_ID", "UOM_NAME");
            ViewBag.createdbylist = new SelectList(user, "USER_ID", "USER_NAME");
            ViewBag.vendorlist = new SelectList(vendor, "VENDOR_ID", "VENDOR_CODE");
            ViewBag.item_type_list = new SelectList(_ItemTypeService.GetAll(), "item_type_id", "item_type_name");
            ViewBag.hsncodelist = new SelectList(_genericservice.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.GSTApplicability = new SelectList(_genericservice.GetGSTApplicability(), "gst_applicability_id", "gst_applicability_name");
            ViewBag.SacList = new SelectList(_genericservice.GetSACList(), "sac_id", "sac_code");
            return View(rEF_ITEM);
        }

        // GET: Items/Create
        [CustomAuthorizeAttribute("IM")]
        public ActionResult Create()
        {
            var uom = _uomService.GetAll();
            var vendor = _VendorService.GetAll();
            var user = _UserService.GetAll();
            var priority = _genericservice.GetPriorityByForm(3);
            var itemvaluation = _ItemValuation.GetAll();
            var brand = _Brand.GetAll();
            var excisecategory = _ExciseService.GetAll();
            var itemaccounting = _ItemAccounting.GetAll();
            var itemcategory = _ItemCategory.GetAll();
            var itemgroup = _ItemGroup.GetAll();
            var shelfLife = _shelflifeservice.GetAll();
            ViewBag.error = "";
            ViewBag.ShelfLifeList = new SelectList(shelfLife, "shelf_life_id", "shelf_life_name");
            ViewBag.generalleder = new SelectList(_genericservice.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.BRAND_ID = new SelectList(brand, "BRAND_ID", "BRAND_NAME");
            ViewBag.EXCISE_CATEGORY_ID = new SelectList(excisecategory, "EXCISE_CATEGORY_ID", "EXCISE_CATEGORY_NAME");
            ViewBag.ITEM_ACCOUNTING_ID = new SelectList(itemaccounting, "ITEM_ACCOUNTING_ID", "ITEM_ACCOUNTING_NAME");
            ViewBag.ITEM_CATEGORY_ID = new SelectList(itemcategory, "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.ITEM_CATEGORY_prefix_sufix_ID = _ItemCategory.GetAll();
            ViewBag.ITEM_GROUP_ID = new SelectList(itemgroup, "ITEM_GROUP_ID", "ITEM_GROUP_NAME");
            ViewBag.ITEM_VALUATION_ID = new SelectList(itemvaluation, "ITEM_VALUATION_ID", "ITEM_VALUATION_NAME");
            ViewBag.PRIORITY_ID = new SelectList(priority, "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.uomlist = new SelectList(uom, "UOM_ID", "UOM_NAME");
            ViewBag.CREATED_BY = new SelectList(user, "USER_ID", "USER_NAME");
            ViewBag.PREFERRED_VENDOR_ID = new SelectList(vendor, "VENDOR_ID", "VENDOR_CODE");
            ViewBag.item_type_list = new SelectList(_ItemTypeService.GetAll(), "item_type_id", "item_type_name");
            ViewBag.hsncodelist = new SelectList(_genericservice.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.GSTApplicability = new SelectList(_genericservice.GetGSTApplicability(), "gst_applicability_id", "gst_applicability_name");
            ViewBag.SacList = new SelectList(_genericservice.GetSACList(), "sac_id", "sac_code");
            ViewBag.item_id = new SelectList(_genericservice.get_childitem_list("create_item",0), "ITEM_ID", "ITEM_NAME");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ref_item_VM rEF_ITEM, FormCollection fc)
        {
            rEF_ITEM.CREATED_ON = DateTime.Now;
            string products;
            products = fc["productdetail"];
            string[] emptyStringArray = new string[0];
            try
            {
                emptyStringArray = products.Split(new string[] { "~" }, StringSplitOptions.None);
            }
            catch
            {

            }

            List<ref_item_alternate_UOM> journal_list = new List<ref_item_alternate_UOM>();
            for (int i = 0; i < emptyStringArray.Count() - 1; i++)
            {
                ref_item_alternate_UOM journal_item = new ref_item_alternate_UOM();
                journal_item.uom_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[1]);
                journal_item.conversion_rate = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[4]);
                journal_list.Add(journal_item);
            }
            rEF_ITEM.ref_item_alternate_UOM = journal_list;
            rEF_ITEM.ledgeraccounttype = fc["ledgeraccounttype"];

            string parameters;
            parameters = fc["parameterdetail"];
            string[] emptyStringArray2 = new string[0];
            try
            {
                emptyStringArray2 = parameters.Split(new string[] { "~" }, StringSplitOptions.None);
            }
            catch
            {

            }

            List<ref_item_parameter> parameter_list = new List<ref_item_parameter>();
            for (int i = 0; i < emptyStringArray2.Count() - 1; i++)
            {
                ref_item_parameter parameter_item = new ref_item_parameter();
                //   parameter_item.parameter_id = int.Parse(emptyStringArray2[i].Split(new char[] { ',' })[0]==""?"0": emptyStringArray2[i].Split(new char[] { ',' })[0]);
                parameter_item.parameter_name = emptyStringArray2[i].Split(new char[] { ',' })[0];
                parameter_item.parameter_range = emptyStringArray2[i].Split(new char[] { ',' })[1];
                parameter_list.Add(parameter_item);
            }
            rEF_ITEM.ref_item_parameter = parameter_list;
            var isValid = "";
            if (ModelState.IsValid)
            {
                isValid = _item_service.Add(rEF_ITEM);
                if (isValid == "Saved")
                {
                    TempData["doc_num"] = "Saved Successfully.";
                    return RedirectToAction("Index");
                }
            }


            ViewBag.error = isValid;
            var uom = _uomService.GetAll();
            var vendor = _VendorService.GetAll();
            var user = _UserService.GetAll();
            var priority = _genericservice.GetPriorityByForm(3);
            var itemvaluation = _ItemValuation.GetAll();
            var brand = _Brand.GetAll();
            var excisecategory = _ExciseService.GetAll();
            var itemaccounting = _ItemAccounting.GetAll();
            var itemcategory = _ItemCategory.GetAll();
            var itemgroup = _ItemGroup.GetAll();
            var shelfLife = _shelflifeservice.GetAll();
            ViewBag.ShelfLifeList = new SelectList(shelfLife, "shelf_life_id", "shelf_life_name");
            ViewBag.generalleder = new SelectList(_genericservice.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.BRAND_ID = new SelectList(brand, "BRAND_ID", "BRAND_NAME");
            ViewBag.EXCISE_CATEGORY_ID = new SelectList(excisecategory, "EXCISE_CATEGORY_ID", "EXCISE_CATEGORY_NAME");
            ViewBag.ITEM_ACCOUNTING_ID = new SelectList(itemaccounting, "ITEM_ACCOUNTING_ID", "ITEM_ACCOUNTING_NAME");
            ViewBag.ITEM_CATEGORY_ID = new SelectList(itemcategory, "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME");
            ViewBag.ITEM_GROUP_ID = new SelectList(itemgroup, "ITEM_GROUP_ID", "ITEM_GROUP_NAME");
            ViewBag.ITEM_VALUATION_ID = new SelectList(itemvaluation, "ITEM_VALUATION_ID", "ITEM_VALUATION_NAME");
            ViewBag.PRIORITY_ID = new SelectList(priority, "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.uomlist = new SelectList(uom, "UOM_ID", "UOM_NAME");
            ViewBag.CREATED_BY = new SelectList(user, "USER_ID", "USER_NAME");
            ViewBag.PREFERRED_VENDOR_ID = new SelectList(vendor, "VENDOR_ID", "VENDOR_CODE");
            ViewBag.item_type_list = new SelectList(_ItemTypeService.GetAll(), "item_type_id", "item_type_name");
            ViewBag.hsncodelist = new SelectList(_genericservice.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.GSTApplicability = new SelectList(_genericservice.GetGSTApplicability(), "gst_applicability_id", "gst_applicability_name");
            ViewBag.SacList = new SelectList(_genericservice.GetSACList(), "sac_id", "sac_code");
            return View(rEF_ITEM);
        }

        // GET: Items/Edit/5
        [CustomAuthorizeAttribute("IM")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_ITEM = _item_service.Get((int)id);
            if (rEF_ITEM == null)
            {
                return HttpNotFound();
            }
            ViewBag.error = "";
            var uom = _uomService.GetAll();
            var vendor = _VendorService.GetAll();
            var user = _UserService.GetAll();
            var priority = _genericservice.GetPriorityByForm(3);
            var itemvaluation = _ItemValuation.GetAll();
            var brand = _Brand.GetAll();
            var excisecategory = _ExciseService.GetAll();
            var itemaccounting = _ItemAccounting.GetAll();
            var itemcategory = _ItemCategory.GetAll();
            var itemgroup = _ItemGroup.GetAll();
            var shelfLife = _shelflifeservice.GetAll();
            ViewBag.ShelfLifeList = new SelectList(shelfLife, "shelf_life_id", "shelf_life_name");
            ViewBag.generalleder = new SelectList(_genericservice.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.brandlist = new SelectList(brand, "BRAND_ID", "BRAND_NAME");
            ViewBag.excisecategorylist = new SelectList(excisecategory, "EXCISE_CATEGORY_ID", "EXCISE_CATEGORY_NAME");
            ViewBag.itemaccountinglist = new SelectList(itemaccounting, "ITEM_ACCOUNTING_ID", "ITEM_ACCOUNTING_NAME");
            ViewBag.itemcategorylist = new SelectList(itemcategory, "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME", rEF_ITEM.ITEM_GROUP_ID);
            ViewBag.itemgrouplist = new SelectList(itemgroup, "ITEM_GROUP_ID", "ITEM_GROUP_NAME");
            ViewBag.itemvaluationlist = new SelectList(itemvaluation, "ITEM_VALUATION_ID", "ITEM_VALUATION_NAME");
            ViewBag.prioritilist = new SelectList(priority, "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.uomlist = new SelectList(uom, "UOM_ID", "UOM_NAME");
            ViewBag.createdbylist = new SelectList(user, "USER_ID", "USER_NAME");
            ViewBag.vendorlist = new SelectList(vendor, "VENDOR_ID", "VENDOR_CODE");
            ViewBag.item_type_list = new SelectList(_ItemTypeService.GetAll(), "item_type_id", "item_type_name");
            ViewBag.hsncodelist = new SelectList(_genericservice.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.GSTApplicability = new SelectList(_genericservice.GetGSTApplicability(), "gst_applicability_id", "gst_applicability_name");
            ViewBag.SacList = new SelectList(_genericservice.GetSACList(), "sac_id", "sac_code");
            ViewBag.item_id = new SelectList(_genericservice.get_childitem_list("edit_item", (int)id), "ITEM_ID", "ITEM_NAME");
            return View(rEF_ITEM);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ref_item_VM rEF_ITEM, FormCollection fc)
        {
            rEF_ITEM.CREATED_ON = DateTime.Now;
            rEF_ITEM.CREATED_BY = 1;
            string products;
            products = fc["productdetail"];
            rEF_ITEM.deleteparameter = fc["deleteparameter"];
            //var vendor = _vendor.GetAll();
            //ViewBag.Vendor = new SelectList(vendor, "VENDOR_ID", "VENDOR_NAME");
            string[] emptyStringArray = new string[0];
            try
            {
                emptyStringArray = products.Split(new string[] { "~" }, StringSplitOptions.None);
            }
            catch
            {

            }

            List<ref_item_alternate_UOM> journal_list = new List<ref_item_alternate_UOM>();
            for (int i = 0; i < emptyStringArray.Count() - 1; i++)
            {
                ref_item_alternate_UOM item = new ref_item_alternate_UOM();
                item.uom_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[1]);
                item.conversion_rate = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[4]);

                if (emptyStringArray[i].Split(new char[] { ',' })[0] == "")
                {
                    item.alternate_uom_id = 0;
                }
                else
                {
                    item.alternate_uom_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[0]);
                }
                journal_list.Add(item);
            }
            rEF_ITEM.ref_item_alternate_UOM = journal_list;
            rEF_ITEM.ledgeraccounttype = fc["ledgeraccounttype"];

            string parameters;
            parameters = fc["parameterdetail"];
            string[] emptyStringArray2 = new string[0];
            try
            {
                emptyStringArray2 = parameters.Split(new string[] { "~" }, StringSplitOptions.None);
            }
            catch
            {

            }

            List<ref_item_parameter> parameter_list = new List<ref_item_parameter>();
            for (int i = 0; i < emptyStringArray2.Count() - 1; i++)
            {
                ref_item_parameter parameter_item = new ref_item_parameter();
                parameter_item.parameter_id = int.Parse(emptyStringArray2[i].Split(new char[] { ',' })[0] == "" ? "0" : emptyStringArray2[i].Split(new char[] { ',' })[0]);
                parameter_item.parameter_name = emptyStringArray2[i].Split(new char[] { ',' })[1];
                parameter_item.parameter_range = emptyStringArray2[i].Split(new char[] { ',' })[2];
                parameter_list.Add(parameter_item);
            }
            rEF_ITEM.ref_item_parameter = parameter_list;
            var isValid = "";
            if (ModelState.IsValid)
            {
                isValid = _item_service.Add(rEF_ITEM);
                if (isValid == "Saved")
                {
                    TempData["doc_num"] = "Updated Successfully.";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = isValid;
            var uom = _uomService.GetAll();
            var vendor = _VendorService.GetAll();
            var user = _UserService.GetAll();
            var priority = _genericservice.GetPriorityByForm(3);
            var itemvaluation = _ItemValuation.GetAll();
            var brand = _Brand.GetAll();
            var excisecategory = _ExciseService.GetAll();
            var itemaccounting = _ItemAccounting.GetAll();
            var itemcategory = _ItemCategory.GetAll();
            var itemgroup = _ItemGroup.GetAll();
            var shelfLife = _shelflifeservice.GetAll();
            ViewBag.ShelfLifeList = new SelectList(shelfLife, "shelf_life_id", "shelf_life_name");
            ViewBag.generalleder = new SelectList(_genericservice.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.brandlist = new SelectList(brand, "BRAND_ID", "BRAND_NAME");
            ViewBag.excisecategorylist = new SelectList(excisecategory, "EXCISE_CATEGORY_ID", "EXCISE_CATEGORY_NAME");
            ViewBag.itemaccountinglist = new SelectList(itemaccounting, "ITEM_ACCOUNTING_ID", "ITEM_ACCOUNTING_NAME");
            ViewBag.itemcategorylist = new SelectList(itemcategory, "ITEM_CATEGORY_ID", "ITEM_CATEGORY_NAME", rEF_ITEM.ITEM_GROUP_ID);
            ViewBag.itemgrouplist = new SelectList(itemgroup, "ITEM_GROUP_ID", "ITEM_GROUP_NAME");
            ViewBag.itemvaluationlist = new SelectList(itemvaluation, "ITEM_VALUATION_ID", "ITEM_VALUATION_NAME");
            ViewBag.prioritilist = new SelectList(priority, "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.uomlist = new SelectList(uom, "UOM_ID", "UOM_NAME");
            ViewBag.itemlengthuomlist = new SelectList(uom, "UOM_ID", "UOM_NAME");
            ViewBag.itemwidthuomlist = new SelectList(uom, "UOM_ID", "UOM_NAME");
            ViewBag.itemheightuomlist = new SelectList(uom, "UOM_ID", "UOM_NAME");
            ViewBag.itemvolumeuomlist = new SelectList(uom, "UOM_ID", "UOM_NAME");
            ViewBag.itemweightuomlist = new SelectList(uom, "UOM_ID", "UOM_NAME");
            ViewBag.selflifeuomlist = new SelectList(uom, "UOM_ID", "UOM_NAME");
            ViewBag.createdbylist = new SelectList(user, "USER_ID", "USER_NAME");
            ViewBag.vendorlist = new SelectList(vendor, "VENDOR_ID", "VENDOR_CODE");
            ViewBag.item_type_list = new SelectList(_ItemTypeService.GetAll(), "item_type_id", "item_type_name");
            ViewBag.hsncodelist = new SelectList(_genericservice.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.GSTApplicability = new SelectList(_genericservice.GetGSTApplicability(), "gst_applicability_id", "gst_applicability_name");
            ViewBag.SacList = new SelectList(_genericservice.GetSACList(), "sac_id", "sac_code");
            return View(rEF_ITEM);
        }

        // GET: Items/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var rEF_ITEM = _scifferContext.Get((int)id);
        //    if (rEF_ITEM == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(rEF_ITEM);
        //}

        //// POST: Items/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    var isdelete = _scifferContext.Delete(id);
        //    if (isdelete)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return View();

        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _item_service.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Delete(int key)
        {
            var data = _item_service.Delete(key);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLedgerAccountTypeByItem(int entity_type_id, int item_category_id,int? item_type_id)
        {
            var paymentService = _genericservice.GetLedgerAccountTypeByItem(entity_type_id, item_category_id, item_type_id);
            return Json(paymentService, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetItemPlantValuation(int id)
        {
            var paymentService = _item_service.GetItemPlantValuation(id);
            return Json(paymentService, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLedgerAccountType(int entity_type_id, int entity_id,int? item_type_id)
        {
            var paymentService = _genericservice.GetLedgerAccountType(entity_type_id, entity_id, item_type_id);
            return Json(paymentService, JsonRequestBehavior.AllowGet);
        }

        public Boolean ValidateItemsExcelColumns(string[] ItemColumnArray)
        {
            if (ItemColumnArray.Contains("SrNo") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("ItemCategory") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("ItemName") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("ItemCode") == false)
            {
                return false;
            }

            if (ItemColumnArray.Contains("ItemType") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("ItemGroup") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("Brand") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("UoM") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("ItemLength") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("ItemLeghthUoM") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("ItemWidth") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("ItemWidthUoM") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("ItemHeight") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("ItemHeightUoM") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("ItemVolume") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("ItemVolumeUoM") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("ItemWeight") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("ItemWeightUoM") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("Priority") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("QualityManaged") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("BatchManaged") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("ShelfLife") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("ShelfLifeUoM") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("PreferredVendor") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("VendorPartNumber") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("MinimumLevel") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("MaximumLevel") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("ReorderLevel") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("ReorderQuality") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("MRP") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("ItemValuation") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("ItemAccounting") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("HSNCatagory") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("HSN") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("AdditionalInfo") == false)
            {
                return false;
            }
            if (ItemColumnArray.Contains("Blocked") == false)
            {
                return false;
            }
            return true;
        }

        public Boolean ValidateUoMExcelColumns(string[] ContactColumnArray)
        {
            if (ContactColumnArray.Contains("SrNo") == false)
            {
                return false;
            }
            if (ContactColumnArray.Contains("ItemCode") == false)
            {
                return false;
            }
            if (ContactColumnArray.Contains("UoM") == false)
            {
                return false;
            }
            if (ContactColumnArray.Contains("ConversionRate") == false)
            {
                return false;
            }
            return true;
        }

        public Boolean ValidateGLExcelColumns(string[] GLColumnArray)
        {
            if (GLColumnArray.Contains("SrNo") == false)
            {
                return false;
            }
            if (GLColumnArray.Contains("ItemCode") == false)
            {
                return false;
            }
            if (GLColumnArray.Contains("LedgerAccountType") == false)
            {
                return false;
            }
            if (GLColumnArray.Contains("GeneralLedgerCode") == false)
            {
                return false;
            }
            return true;
        }
        public Boolean ValidateStandardCost(string[] StandardCostArray)
        {
            if (StandardCostArray.Contains("SrNo") == false)
            {
                return false;
            }
            if (StandardCostArray.Contains("ItemCode") == false)
            {
                return false;
            }
            if (StandardCostArray.Contains("PlantName") == false)
            {
                return false;
            }
            if (StandardCostArray.Contains("Value") == false)
            {
                return false;
            }
            return true;
        }
        public Boolean ValidateParamExcelColumns(string[] ParamColumnArray)
        {
            if (ParamColumnArray.Contains("SrNo") == false)
            {
                return false;
            }
            if (ParamColumnArray.Contains("ItemCode") == false)
            {
                return false;
            }
            if (ParamColumnArray.Contains("ParameterName") == false)
            {
                return false;
            }
            if (ParamColumnArray.Contains("ParameterRange") == false)
            {
                return false;
            }
            return true;
        }
        [HttpPost]
        public ActionResult UploadFiles()
        {
            for (int m = 0; m < Request.Files.Count; m++)
            {
                HttpPostedFileBase file = Request.Files[m];
                if (file.ContentLength > 0)
                {
                    string extension = System.IO.Path.GetExtension(file.FileName);
                    string path1 = string.Format("{0}/{1}", Server.MapPath("~/Uploads"), file.FileName);
                    if (System.IO.File.Exists(path1))
                        System.IO.File.Delete(path1);

                    file.SaveAs(path1);
                    FileStream stream = System.IO.File.Open(path1, FileMode.Open, FileAccess.Read);
                    IExcelDataReader excelReader;
                    if (extension == ".xls")
                    {
                        excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else
                    {
                        excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }

                    excelReader.IsFirstRowAsColumnNames = true;
                    System.Data.DataSet result = excelReader.AsDataSet();
                    int contcol = 0, custcol = 0, glcol = 0, prmcol = 0, stccol = 0;
                    string uploadtype = Request.Params[0];
                    List<item_list> item_list = new List<item_list>();
                    List<Uom_Excel> Uom_Excel = new List<Uom_Excel>();
                    List<item_category_gl_Excel> item_category_gl_Excel = new List<item_category_gl_Excel>();
                    List<parameter_Excel> parameter_Excel = new List<parameter_Excel>();
                    List<duplicateGLExcel> duplicateGLExcel = new List<duplicateGLExcel>();
                    List<REF_PLANT_VM> REF_PLANT_VM = new List<REF_PLANT_VM>();
                    List<ref_item_plant_vm> ref_item_plant_vm = new List<ref_item_plant_vm>();
                    List<ref_item_parameter> ref_item_parameter = new List<ref_item_parameter>();
                    List<qality_batch> qality_batch = new List<qality_batch>();
                    if (result.Tables.Count == 0)
                    {
                        errorList++;
                        error[error.Length - 1] = "File is Empty!";
                        errorMessage = "File is Empty!";
                    }
                    else
                    {
                        foreach (DataTable sheet in result.Tables)
                        {

                            if (sheet.TableName == "ItemDetails")
                            {
                                string[] ItemColumnArray = new string[sheet.Columns.Count];
                                var TempSheetColumn = from DataColumn Tempcolumn in sheet.Columns select Tempcolumn;
                                foreach (var ary1 in TempSheetColumn)
                                {
                                    ItemColumnArray[custcol] = ary1.ToString();
                                    custcol = custcol + 1;
                                }

                                if (sheet != null)
                                {
                                    var TempSheetRows = from DataRow TempRow in sheet.Rows select TempRow;
                                    foreach (var ary in TempSheetRows)
                                    {
                                        var a = ary.ItemArray;
                                        if (ValidateItemsExcelColumns(ItemColumnArray) == true)
                                        {
                                            string sr_no = a[Array.IndexOf(ItemColumnArray, "SrNo")].ToString().Trim();
                                            if (sr_no != "")
                                            {

                                                if (item_list.Count != 0)
                                                {
                                                    item_list IDVM = new item_list();
                                                    var item_category = a[Array.IndexOf(ItemColumnArray, "ItemCategory")].ToString().Trim();

                                                    if (item_category == "")
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = item_category.ToString();
                                                        errorMessage = "Add item category.";
                                                    }
                                                    else
                                                    {
                                                        var item_category_id = _genericservice.GetItemCategoryId(item_category);
                                                        if (item_category_id == 0)
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = item_category.ToString();
                                                            errorMessage = item_category + " item_category not found.";
                                                        }
                                                        else
                                                        {
                                                            IDVM.ITEM_CATEGORY_ID = item_category_id;
                                                        }
                                                    }
                                                    IDVM.ITEM_NAME = a[Array.IndexOf(ItemColumnArray, "ItemName")].ToString().Trim();
                                                    IDVM.CREATED_BY = 1;
                                                    IDVM.CREATED_ON = DateTime.Now;
                                                    var ItemCode = a[Array.IndexOf(ItemColumnArray, "ItemCode")].ToString().Trim();
                                                    var ItemDBCode = _genericservice.GetItemId(ItemCode);
                                                    if (ItemDBCode == 0)
                                                    {
                                                        var itemCode = item_list.Where(x => x.ITEM_CODE == ItemCode).FirstOrDefault();
                                                        if (itemCode == null)
                                                        {
                                                            IDVM.ITEM_CODE = ItemCode;
                                                            var item_type = a[Array.IndexOf(ItemColumnArray, "ItemType")].ToString().Trim();
                                                            var item_type_id = _genericservice.GetItemTypeId(item_type);
                                                            if (item_type_id == 0)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = item_type;
                                                                errorMessage = "item_type  not found.";
                                                            }
                                                            else
                                                            {
                                                                IDVM.item_type_id = item_type_id;
                                                            }
                                                            var ItemGroup = a[Array.IndexOf(ItemColumnArray, "ItemGroup")].ToString().Trim();
                                                            if (ItemGroup == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = ItemGroup;
                                                                errorMessage = "ItemGroup not found.";
                                                            }
                                                            else
                                                            {
                                                                var item_group_id = _genericservice.GetItemGroupId(ItemGroup);
                                                                if (item_group_id == 0)
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = ItemGroup;
                                                                    errorMessage = ItemGroup + " ItemGroup not found.";
                                                                }
                                                                else
                                                                {
                                                                    IDVM.ITEM_GROUP_ID = item_group_id;
                                                                }
                                                            }
                                                            var Brand = a[Array.IndexOf(ItemColumnArray, "Brand")].ToString().Trim();
                                                            if (Brand == "")
                                                            {
                                                                IDVM.BRAND_ID = 0;
                                                            }
                                                            else
                                                            {
                                                                var Brand_Id = _genericservice.GetBrandId(Brand);
                                                                if (Brand_Id == 0)
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = Brand;
                                                                    errorMessage = Brand + " Brand not found.";
                                                                }
                                                                else
                                                                {
                                                                    IDVM.BRAND_ID = Brand_Id;
                                                                }

                                                            }
                                                            var UoM = a[Array.IndexOf(ItemColumnArray, "UoM")].ToString().Trim();
                                                            if (UoM == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = UoM;
                                                                errorMessage = "UoM not found.";
                                                            }
                                                            else
                                                            {
                                                                var Uom_id = _genericservice.GetUoMId(UoM);
                                                                if (Uom_id == 0)
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = UoM;
                                                                    errorMessage = UoM + " UoM not found.";
                                                                }
                                                                else
                                                                {
                                                                    IDVM.UOM_ID = Uom_id;
                                                                }
                                                            }
                                                            var ItemLength = a[Array.IndexOf(ItemColumnArray, "ItemLength")].ToString().Trim();
                                                            if (ItemLength == "")
                                                            {
                                                                IDVM.ITEM_LENGTH = 0;
                                                            }
                                                            else
                                                            {
                                                                IDVM.ITEM_LENGTH = Double.Parse(ItemLength);
                                                                var ItemLeghthUoM = a[Array.IndexOf(ItemColumnArray, "ItemLeghthUoM")].ToString().Trim();
                                                                if (ItemLeghthUoM == "")
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = ItemLeghthUoM;
                                                                    errorMessage = "ItemLeghthUoM is blank";
                                                                }
                                                                else
                                                                {
                                                                    var ItemLeghthUoMid = _genericservice.GetUoMId(ItemLeghthUoM);
                                                                    if (ItemLeghthUoMid == 0)
                                                                    {
                                                                        errorList++;
                                                                        error[error.Length - 1] = ItemLeghthUoM.ToString();
                                                                        errorMessage = ItemLeghthUoM + " ItemLeghthUoM not found.";
                                                                    }
                                                                    else
                                                                    {
                                                                        IDVM.ITEM_LENGHT_UOM_ID = ItemLeghthUoMid;
                                                                    }
                                                                }
                                                            }

                                                            var ItemWidth = a[Array.IndexOf(ItemColumnArray, "ItemWidth")].ToString().Trim();
                                                            if (ItemWidth == "")
                                                            {
                                                                IDVM.ITEM_WIDTH = 0;
                                                            }
                                                            else
                                                            {
                                                                IDVM.ITEM_WIDTH = Double.Parse(ItemWidth);
                                                                var ItemWidthUoM = a[Array.IndexOf(ItemColumnArray, "ItemWidthUoM")].ToString().Trim();
                                                                if (ItemWidthUoM == "")
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = ItemWidthUoM;
                                                                    errorMessage = "ItemWidthUoM is blank";
                                                                }
                                                                else
                                                                {
                                                                    var ItemWidthUoMid = _genericservice.GetUoMId(ItemWidthUoM);
                                                                    if (ItemWidthUoMid == 0)
                                                                    {
                                                                        errorList++;
                                                                        error[error.Length - 1] = ItemWidthUoM.ToString();
                                                                        errorMessage = ItemWidthUoM + " ItemWidthUoM not found.";
                                                                    }
                                                                    else
                                                                    {
                                                                        IDVM.ITEM_WIDTH_UOM_ID = ItemWidthUoMid;
                                                                    }
                                                                }
                                                            }

                                                            var ItemHeight = a[Array.IndexOf(ItemColumnArray, "ItemHeight")].ToString().Trim();
                                                            if (ItemHeight == "")
                                                            {
                                                                IDVM.ITEM_HEIGHT = 0;
                                                            }
                                                            else
                                                            {
                                                                IDVM.ITEM_HEIGHT = Double.Parse(ItemHeight);
                                                                var ItemHeightUoM = a[Array.IndexOf(ItemColumnArray, "ItemHeightUoM")].ToString().Trim();
                                                                if (ItemHeightUoM == "")
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = ItemHeightUoM;
                                                                    errorMessage = " ItemHeightUoM is blank";
                                                                }
                                                                else
                                                                {
                                                                    var ItemHeightUoMid = _genericservice.GetUoMId(ItemHeightUoM);
                                                                    if (ItemHeightUoMid == 0)
                                                                    {
                                                                        errorList++;
                                                                        error[error.Length - 1] = ItemHeightUoM.ToString();
                                                                        errorMessage = ItemHeightUoM + " ItemHeightUoM not found.";
                                                                    }
                                                                    else
                                                                    {
                                                                        IDVM.ITEM_HEIGHT_UOM_ID = ItemHeightUoMid;
                                                                    }
                                                                }
                                                            }

                                                            var ItemVolume = a[Array.IndexOf(ItemColumnArray, "ItemVolume")].ToString().Trim();
                                                            if (ItemVolume == "")
                                                            {
                                                                IDVM.ITEM_VOLUME = 0;
                                                            }
                                                            else
                                                            {
                                                                IDVM.ITEM_VOLUME = Double.Parse(ItemVolume);
                                                                var ItemVolumeUoM = a[Array.IndexOf(ItemColumnArray, "ItemVolumeUoM")].ToString().Trim();
                                                                if (ItemVolumeUoM == "")
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = ItemVolumeUoM;
                                                                    errorMessage = "ItemVolumeUoM is blank";
                                                                }
                                                                else
                                                                {
                                                                    var ItemVolumeUoMid = _genericservice.GetUoMId(ItemVolumeUoM);
                                                                    if (ItemVolumeUoMid == 0)
                                                                    {
                                                                        errorList++;
                                                                        error[error.Length - 1] = ItemVolumeUoM.ToString();
                                                                        errorMessage = ItemVolumeUoM + " ItemVolumeUoM not found.";
                                                                    }
                                                                    else
                                                                    {
                                                                        IDVM.ITEM_VOLUME_UOM_ID = ItemVolumeUoMid;
                                                                    }
                                                                }
                                                            }

                                                            var ItemWeight = a[Array.IndexOf(ItemColumnArray, "ItemWeight")].ToString().Trim();
                                                            if (ItemWeight == "")
                                                            {
                                                                IDVM.ITEM_WEIGHT = 0;
                                                            }
                                                            else
                                                            {
                                                                IDVM.ITEM_WEIGHT = Double.Parse(ItemWeight);
                                                                var ItemWeightUoM = a[Array.IndexOf(ItemColumnArray, "ItemWeightUoM")].ToString().Trim();
                                                                if (ItemWeightUoM == "")
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = ItemWeightUoM;
                                                                    errorMessage = "ItemWeightUoM is blank";
                                                                }
                                                                else
                                                                {
                                                                    var ItemWeightUoMid = _genericservice.GetUoMId(ItemWeightUoM);
                                                                    if (ItemWeightUoMid == 0)
                                                                    {
                                                                        errorList++;
                                                                        error[error.Length - 1] = ItemWeightUoM.ToString();
                                                                        errorMessage = ItemWeightUoM + " ItemWeightUoM not found.";
                                                                    }
                                                                    else
                                                                    {
                                                                        IDVM.ITEM_WEIGHT_UOM_ID = ItemWeightUoMid;
                                                                    }
                                                                }
                                                            }

                                                            var Priority = a[Array.IndexOf(ItemColumnArray, "Priority")].ToString().Trim();
                                                            if (Priority == "")
                                                            {
                                                                IDVM.PRIORITY_ID = 0;
                                                            }
                                                            else
                                                            {
                                                                var priorityId = _genericservice.GetPriorityId(Priority);
                                                                if (priorityId == 0)
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = Priority;
                                                                    errorMessage = Priority + " Priority not found!";
                                                                }
                                                                else
                                                                {
                                                                    IDVM.PRIORITY_ID = priorityId;
                                                                }
                                                            }
                                                            var QualityManaged = a[Array.IndexOf(ItemColumnArray, "QualityManaged")].ToString().Trim();
                                                            if (QualityManaged.ToLower() == "yes")
                                                            {
                                                                IDVM.QUALITY_MANAGED = true;
                                                                qality_batch qb = new qality_batch();
                                                                qb.item_code = IDVM.ITEM_CODE;
                                                                qality_batch.Add(qb);
                                                            }
                                                            else if (QualityManaged.ToLower() == "no" || QualityManaged == "")
                                                            {
                                                                IDVM.QUALITY_MANAGED = false;
                                                            }
                                                            else
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = QualityManaged;
                                                                errorMessage = QualityManaged + "Add yes/no only!";
                                                            }
                                                            var BatchManaged = a[Array.IndexOf(ItemColumnArray, "BatchManaged")].ToString().Trim();
                                                            if (BatchManaged.ToLower() == "yes")
                                                            {
                                                                IDVM.BATCH_MANAGED = true;
                                                                var auto_batch = a[Array.IndexOf(ItemColumnArray, "AutoBatch")].ToString().Trim();
                                                                if (auto_batch.ToLower() == "auto")
                                                                {
                                                                    IDVM.auto_batch = 1;
                                                                }
                                                                else if (auto_batch.ToLower() == "manual")
                                                                {
                                                                    IDVM.auto_batch = 2;
                                                                }
                                                                else
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = " Add Auto Or Batch!";
                                                                    errorMessage = "Add Auto Or Batch!";
                                                                }
                                                            }
                                                            else if (BatchManaged.ToLower() == "no" || BatchManaged == "")
                                                            {
                                                                IDVM.BATCH_MANAGED = false;
                                                            }
                                                            else
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = BatchManaged;
                                                                errorMessage = BatchManaged + "Add yes/no only!";
                                                            }
                                                            if (IDVM.BATCH_MANAGED == true)
                                                            {
                                                                if (IDVM.QUALITY_MANAGED == true)
                                                                {
                                                                    var ShelfLife = a[Array.IndexOf(ItemColumnArray, "ShelfLife")].ToString().Trim();
                                                                    if (ShelfLife == "")
                                                                    {
                                                                        errorList++;
                                                                        error[error.Length - 1] = ShelfLife;
                                                                        errorMessage = "ShelfLife not found!";
                                                                    }
                                                                    else
                                                                    {
                                                                        IDVM.SHELF_LIFE = int.Parse(ShelfLife);
                                                                        var ShelfLifeUoM = a[Array.IndexOf(ItemColumnArray, "ShelfLifeUoM")].ToString().Trim();
                                                                        if (ShelfLifeUoM == "")
                                                                        {
                                                                            errorList++;
                                                                            error[error.Length - 1] = ShelfLifeUoM;
                                                                            errorMessage = "ShelfLifeUoM is blank!";
                                                                        }
                                                                        else
                                                                        {
                                                                            var ShelfLifeUoMid = _genericservice.GetShelfLifeId(ShelfLifeUoM);
                                                                            if (ShelfLifeUoMid == 0)
                                                                            {
                                                                                errorList++;
                                                                                error[error.Length - 1] = ShelfLifeUoM;
                                                                                errorMessage = ShelfLifeUoM + " ShelfLifeUoM not found! ";
                                                                            }
                                                                            else
                                                                            {
                                                                                IDVM.SHELF_LIFE_UOM_ID = 1;
                                                                            }
                                                                        }

                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    IDVM.SHELF_LIFE_UOM_ID = 0;
                                                                    IDVM.SHELF_LIFE = 0;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                IDVM.SHELF_LIFE = 0;
                                                                IDVM.SHELF_LIFE_UOM_ID = 0;
                                                            }

                                                            var PreferredVendor = a[Array.IndexOf(ItemColumnArray, "PreferredVendor")].ToString().Trim();
                                                            if (PreferredVendor == "" || PreferredVendor == " ")
                                                            {
                                                                IDVM.PREFERRED_VENDOR_ID = 0;
                                                            }
                                                            else
                                                            {
                                                                var PreferredVendorid = _genericservice.GetVendorId(PreferredVendor);
                                                                if (PreferredVendorid == 0)
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = PreferredVendor;
                                                                    errorMessage = PreferredVendor + " PreferredVendor not found!";
                                                                }
                                                                else
                                                                {
                                                                    IDVM.PREFERRED_VENDOR_ID = PreferredVendorid;
                                                                }
                                                            }
                                                            IDVM.VENDOR_PART_NUMBER = a[Array.IndexOf(ItemColumnArray, "VendorPartNumber")].ToString().Trim();

                                                            var MinimumLevel = a[Array.IndexOf(ItemColumnArray, "MinimumLevel")].ToString().Trim();
                                                            if (MinimumLevel == "")
                                                            {
                                                                IDVM.MINIMUM_LEVEL = 0;
                                                            }
                                                            else
                                                            {
                                                                IDVM.MINIMUM_LEVEL = int.Parse(MinimumLevel);
                                                            }
                                                            var MaximumLevel = a[Array.IndexOf(ItemColumnArray, "MaximumLevel")].ToString().Trim();
                                                            if (MaximumLevel == "")
                                                            {
                                                                IDVM.MAXIMUM_LEVEL = 0;
                                                            }
                                                            else
                                                            {
                                                                IDVM.MAXIMUM_LEVEL = int.Parse(MaximumLevel);
                                                            }
                                                            var ReorderLevel = a[Array.IndexOf(ItemColumnArray, "ReorderLevel")].ToString().Trim();
                                                            if (ReorderLevel == "")
                                                            {
                                                                IDVM.REORDER_LEVEL = 0;
                                                            }
                                                            else
                                                            {
                                                                IDVM.REORDER_LEVEL = int.Parse(ReorderLevel);
                                                            }
                                                            var ReorderQuality = a[Array.IndexOf(ItemColumnArray, "ReorderQuality")].ToString().Trim();
                                                            if (ReorderQuality == "")
                                                            {
                                                                IDVM.REORDER_QUANTITY = 0;
                                                            }
                                                            else
                                                            {
                                                                IDVM.REORDER_QUANTITY = Double.Parse(ReorderQuality);
                                                            }
                                                            var mrp = a[Array.IndexOf(ItemColumnArray, "MRP")].ToString().Trim();
                                                            if (mrp.ToLower() == "yes")
                                                            {
                                                                IDVM.MRP = true;
                                                            }
                                                            else if (mrp.ToLower() == "no" || mrp == "")
                                                            {
                                                                IDVM.MRP = false;
                                                            }
                                                            else
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = mrp;
                                                                errorMessage = mrp + "Add yes/no only!";
                                                            }
                                                            var ItemValuation = a[Array.IndexOf(ItemColumnArray, "ItemValuation")].ToString().Trim();
                                                            if (ItemValuation == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = ItemValuation;
                                                                errorMessage = "ItemValuation is blank!";
                                                            }
                                                            else
                                                            {
                                                                var ItemValuationid = _genericservice.GetItemValuationId(ItemValuation);
                                                                if (ItemValuationid == 0)
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = ItemValuation;
                                                                    errorMessage = ItemValuation + " ItemValuation not found";
                                                                }
                                                                else if (ItemValuationid == 1)
                                                                {
                                                                    IDVM.ITEM_VALUATION_ID = ItemValuationid;
                                                                }
                                                                else
                                                                {
                                                                    IDVM.ITEM_VALUATION_ID = ItemValuationid;
                                                                    var plantList = _plantService.GetPlant();
                                                                    for (int i = 0; i < plantList.Count; i++)
                                                                    {
                                                                        REF_PLANT_VM plant = new REF_PLANT_VM();
                                                                        plant.item_code = ItemCode;
                                                                        // plant.PLANT_CODE = plantList[i].PLANT_CODE;
                                                                        REF_PLANT_VM.Add(plant);
                                                                    }

                                                                }
                                                            }
                                                            var ItemAccounting = a[Array.IndexOf(ItemColumnArray, "ItemAccounting")].ToString().Trim();
                                                            if (ItemAccounting == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = ItemAccounting;
                                                                errorMessage = "ItemAccounting not found";
                                                            }
                                                            else
                                                            {
                                                                var ItemAccountingid = _genericservice.GetItemAccountingId(ItemAccounting);
                                                                if (ItemAccountingid == 0)
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = ItemAccounting;
                                                                    errorMessage = ItemAccounting + " ItemAccounting not found";
                                                                }
                                                                else
                                                                {
                                                                    IDVM.ITEM_ACCOUNTING_ID = ItemAccountingid;
                                                                    if (IDVM.ITEM_ACCOUNTING_ID == 2)
                                                                    {
                                                                        var category_length = _genericservice.GetLedgerAccountTypeByItem(4, IDVM.ITEM_CATEGORY_ID, IDVM.item_type_id);
                                                                        foreach (var x in category_length)
                                                                        //for (var i = 0; i < category_length.Count; i++)
                                                                        {
                                                                            duplicateGLExcel dupli = new duplicateGLExcel();
                                                                            dupli.itemCode = IDVM.ITEM_CODE;
                                                                            duplicateGLExcel.Add(dupli);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        var itemAccounting = _genericservice.GetLedgerAccountTypeByItem(4, IDVM.ITEM_CATEGORY_ID, IDVM.item_type_id);
                                                                        foreach (var x in itemAccounting)
                                                                        {
                                                                            item_category_gl_Excel GE1 = new item_category_gl_Excel();
                                                                            GE1.itemCode = IDVM.ITEM_CODE;
                                                                            GE1.ledger_account_type_id = x.ledger_account_type_id;
                                                                            GE1.gl_ledger_id = x.gl_ledger_id;
                                                                            item_category_gl_Excel.Add(GE1);

                                                                        }

                                                                    }
                                                                }
                                                            }
                                                            var ExciseCatagory = a[Array.IndexOf(ItemColumnArray, "HSNCatagory")].ToString().Trim();
                                                            if (ExciseCatagory == "")
                                                            {
                                                                IDVM.EXCISE_CATEGORY_ID = 0;
                                                            }
                                                            else
                                                            {
                                                                var ExciseCatagoryid = _genericservice.GetExcisecategoryid(ExciseCatagory);
                                                                if (ExciseCatagoryid == 0)
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = ExciseCatagory;
                                                                    errorMessage = ExciseCatagory + " ExciseCatagory not found";
                                                                }
                                                                else
                                                                {
                                                                    IDVM.EXCISE_CATEGORY_ID = ExciseCatagoryid;
                                                                }
                                                            }
                                                            IDVM.EXCISE_CHAPTER_NO = a[Array.IndexOf(ItemColumnArray, "HSN")].ToString().Trim();

                                                            IDVM.ADDITIONAL_INFORMATION = a[Array.IndexOf(ItemColumnArray, "AdditionalInfo")].ToString().Trim();
                                                            var blocked = a[Array.IndexOf(ItemColumnArray, "Blocked")].ToString();
                                                            if (blocked.ToLower() == "yes")
                                                            {
                                                                IDVM.IS_BLOCKED = true;
                                                            }
                                                            else if (blocked.ToLower() == "no" || blocked == "")
                                                            {
                                                                IDVM.IS_BLOCKED = false;
                                                            }
                                                            else
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = blocked;
                                                                errorMessage = "Add only yes or no blocked .";
                                                            }
                                                            item_list.Add(IDVM);

                                                        }
                                                        else
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = ItemCode;
                                                            errorMessage = "item Code is duplicate!";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = ItemCode;
                                                        errorMessage = "item Code already exist!";
                                                    }

                                                }
                                                else
                                                {
                                                    item_list IDVM = new item_list();
                                                    IDVM.CREATED_BY = 1;
                                                    IDVM.CREATED_ON = DateTime.Now;
                                                    var item_category = a[Array.IndexOf(ItemColumnArray, "ItemCategory")].ToString().Trim();
                                                    if (item_category == "")
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = item_category.ToString();
                                                        errorMessage = "Add item category.";
                                                    }
                                                    else
                                                    {
                                                        var item_category_id = _genericservice.GetItemCategoryId(item_category);
                                                        if (item_category_id == 0)
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = item_category.ToString();
                                                            errorMessage = item_category + " item_category not found.";
                                                        }
                                                        else
                                                        {
                                                            IDVM.ITEM_CATEGORY_ID = item_category_id;
                                                        }
                                                    }
                                                    IDVM.ITEM_NAME = a[Array.IndexOf(ItemColumnArray, "ItemName")].ToString().Trim();
                                                    var itemCode = a[Array.IndexOf(ItemColumnArray, "ItemCode")].ToString().Trim();
                                                    var ItemDBCode = _genericservice.GetItemId(itemCode);
                                                    if (ItemDBCode == 0)
                                                    {
                                                        IDVM.ITEM_CODE = itemCode;
                                                        var item_type = a[Array.IndexOf(ItemColumnArray, "ItemType")].ToString().Trim();
                                                        var item_type_id = _genericservice.GetItemTypeId(item_type);
                                                        if (item_type_id == 0)
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = item_type;
                                                            errorMessage = item_type + " item_type not found.";
                                                        }
                                                        else
                                                        {
                                                            IDVM.item_type_id = item_type_id;
                                                        }
                                                        var ItemGroup = a[Array.IndexOf(ItemColumnArray, "ItemGroup")].ToString().Trim();
                                                        if (ItemGroup == "")
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = ItemGroup;
                                                            errorMessage = "ItemGroup not found.";
                                                        }
                                                        else
                                                        {
                                                            var item_group_id = _genericservice.GetItemGroupId(ItemGroup);
                                                            if (item_group_id == 0)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = ItemGroup;
                                                                errorMessage = ItemGroup + " ItemGroup not found.";
                                                            }
                                                            else
                                                            {
                                                                IDVM.ITEM_GROUP_ID = item_group_id;
                                                            }
                                                        }
                                                        var Brand = a[Array.IndexOf(ItemColumnArray, "Brand")].ToString().Trim();
                                                        if (Brand == "")
                                                        {
                                                            IDVM.BRAND_ID = 0;
                                                        }
                                                        else
                                                        {
                                                            var Brand_Id = _genericservice.GetBrandId(Brand);
                                                            if (Brand_Id == 0)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = Brand;
                                                                errorMessage = "Brand not found.";
                                                            }
                                                            else
                                                            {
                                                                IDVM.BRAND_ID = Brand_Id;
                                                            }

                                                        }
                                                        var UoM = a[Array.IndexOf(ItemColumnArray, "UoM")].ToString().Trim();
                                                        if (UoM == "")
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = UoM;
                                                            errorMessage = "UoM not found.";
                                                        }
                                                        else
                                                        {
                                                            var Uom_id = _genericservice.GetUoMId(UoM);
                                                            if (Uom_id == 0)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = UoM;
                                                                errorMessage = "UoM not found.";
                                                            }
                                                            else
                                                            {
                                                                IDVM.UOM_ID = Uom_id;
                                                            }
                                                        }

                                                        var ItemLength = a[Array.IndexOf(ItemColumnArray, "ItemLength")].ToString().Trim();
                                                        if (ItemLength == "")
                                                        {
                                                            IDVM.ITEM_LENGTH = 0;
                                                        }
                                                        else
                                                        {
                                                            IDVM.ITEM_LENGTH = Double.Parse(ItemLength);
                                                            var ItemLeghthUoM = a[Array.IndexOf(ItemColumnArray, "ItemLeghthUoM")].ToString().Trim();
                                                            if (ItemLeghthUoM == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = ItemLeghthUoM;
                                                                errorMessage = "ItemLeghthUoM is blank";
                                                            }
                                                            else
                                                            {
                                                                var ItemLeghthUoMid = _genericservice.GetUoMId(ItemLeghthUoM);
                                                                if (ItemLeghthUoMid == 0)
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = ItemLeghthUoM.ToString();
                                                                    errorMessage = ItemLeghthUoM + " ItemLeghthUoM not found.";
                                                                }
                                                                else
                                                                {
                                                                    IDVM.ITEM_LENGHT_UOM_ID = ItemLeghthUoMid;
                                                                }
                                                            }
                                                        }

                                                        var ItemWidth = a[Array.IndexOf(ItemColumnArray, "ItemWidth")].ToString().Trim();
                                                        if (ItemWidth == "")
                                                        {
                                                            IDVM.ITEM_WIDTH = 0;
                                                        }
                                                        else
                                                        {
                                                            IDVM.ITEM_WIDTH = Double.Parse(ItemWidth);
                                                            var ItemWidthUoM = a[Array.IndexOf(ItemColumnArray, "ItemWidthUoM")].ToString().Trim();
                                                            if (ItemWidthUoM == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = ItemWidthUoM;
                                                                errorMessage = "ItemWidthUoM is blank";
                                                            }
                                                            else
                                                            {
                                                                var ItemWidthUoMid = _genericservice.GetUoMId(ItemWidthUoM);
                                                                if (ItemWidthUoMid == 0)
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = ItemWidthUoM.ToString();
                                                                    errorMessage = ItemWidthUoM + " ItemWidthUoM not found.";
                                                                }
                                                                else
                                                                {
                                                                    IDVM.ITEM_WIDTH_UOM_ID = ItemWidthUoMid;
                                                                }
                                                            }
                                                        }

                                                        var ItemHeight = a[Array.IndexOf(ItemColumnArray, "ItemHeight")].ToString().Trim();
                                                        if (ItemHeight == "")
                                                        {
                                                            IDVM.ITEM_HEIGHT = 0;
                                                        }
                                                        else
                                                        {
                                                            IDVM.ITEM_HEIGHT = Double.Parse(ItemHeight);
                                                            var ItemHeightUoM = a[Array.IndexOf(ItemColumnArray, "ItemHeightUoM")].ToString().Trim();
                                                            if (ItemHeightUoM == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = ItemHeightUoM;
                                                                errorMessage = " ItemHeightUoM is blank";
                                                            }
                                                            else
                                                            {
                                                                var ItemHeightUoMid = _genericservice.GetUoMId(ItemHeightUoM);
                                                                if (ItemHeightUoMid == 0)
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = ItemHeightUoM.ToString();
                                                                    errorMessage = ItemHeightUoM + " ItemHeightUoM not found.";
                                                                }
                                                                else
                                                                {
                                                                    IDVM.ITEM_HEIGHT_UOM_ID = ItemHeightUoMid;
                                                                }
                                                            }
                                                        }

                                                        var ItemVolume = a[Array.IndexOf(ItemColumnArray, "ItemVolume")].ToString().Trim();
                                                        if (ItemVolume == "")
                                                        {
                                                            IDVM.ITEM_VOLUME = 0;
                                                        }
                                                        else
                                                        {
                                                            IDVM.ITEM_VOLUME = Double.Parse(ItemVolume);
                                                            var ItemVolumeUoM = a[Array.IndexOf(ItemColumnArray, "ItemVolumeUoM")].ToString().Trim();
                                                            if (ItemVolumeUoM == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = ItemVolumeUoM;
                                                                errorMessage = "ItemVolumeUoM is blank";
                                                            }
                                                            else
                                                            {
                                                                var ItemVolumeUoMid = _genericservice.GetUoMId(ItemVolumeUoM);
                                                                if (ItemVolumeUoMid == 0)
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = ItemVolumeUoM.ToString();
                                                                    errorMessage = ItemVolumeUoM + " ItemVolumeUoM not found.";
                                                                }
                                                                else
                                                                {
                                                                    IDVM.ITEM_VOLUME_UOM_ID = ItemVolumeUoMid;
                                                                }
                                                            }
                                                        }

                                                        var ItemWeight = a[Array.IndexOf(ItemColumnArray, "ItemWeight")].ToString().Trim();
                                                        if (ItemWeight == "")
                                                        {
                                                            IDVM.ITEM_WEIGHT = 0;
                                                        }
                                                        else
                                                        {
                                                            IDVM.ITEM_WEIGHT = Double.Parse(ItemWeight);
                                                            var ItemWeightUoM = a[Array.IndexOf(ItemColumnArray, "ItemWeightUoM")].ToString().Trim();
                                                            if (ItemWeightUoM == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = ItemWeightUoM;
                                                                errorMessage = "ItemWeightUoM is blank";
                                                            }
                                                            else
                                                            {
                                                                var ItemWeightUoMid = _genericservice.GetUoMId(ItemWeightUoM);
                                                                if (ItemWeightUoMid == 0)
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = ItemWeightUoM.ToString();
                                                                    errorMessage = ItemWeightUoM + " ItemWeightUoM not found.";
                                                                }
                                                                else
                                                                {
                                                                    IDVM.ITEM_WEIGHT_UOM_ID = ItemWeightUoMid;
                                                                }
                                                            }
                                                        }
                                                        var Priority = a[Array.IndexOf(ItemColumnArray, "Priority")].ToString().Trim();
                                                        if (Priority == "")
                                                        {
                                                            IDVM.PRIORITY_ID = 0;
                                                        }
                                                        else
                                                        {
                                                            var priorityId = _genericservice.GetPriorityId(Priority);
                                                            if (priorityId == 0)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = Priority;
                                                                errorMessage = Priority + " Priority not found!";
                                                            }
                                                            else
                                                            {
                                                                IDVM.PRIORITY_ID = priorityId;
                                                            }
                                                        }
                                                        var QualityManaged = a[Array.IndexOf(ItemColumnArray, "QualityManaged")].ToString().Trim();
                                                        if (QualityManaged.ToLower() == "yes")
                                                        {
                                                            IDVM.QUALITY_MANAGED = true;
                                                            qality_batch qb = new qality_batch();
                                                            qb.item_code = IDVM.ITEM_CODE;
                                                            qality_batch.Add(qb);
                                                        }
                                                        else if (QualityManaged.ToLower() == "no" || QualityManaged == "")
                                                        {
                                                            IDVM.QUALITY_MANAGED = false;
                                                        }
                                                        else
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = QualityManaged;
                                                            errorMessage = QualityManaged + "Add yes/no only!";
                                                        }
                                                        var BatchManaged = a[Array.IndexOf(ItemColumnArray, "BatchManaged")].ToString().Trim();
                                                        if (BatchManaged.ToLower() == "yes")
                                                        {
                                                            IDVM.BATCH_MANAGED = true;
                                                            var auto_batch = a[Array.IndexOf(ItemColumnArray, "AutoBatch")].ToString().Trim();
                                                            if (auto_batch.ToLower() == "auto")
                                                            {
                                                                IDVM.auto_batch = 1;
                                                            }
                                                            else if (auto_batch.ToLower() == "manual")
                                                            {
                                                                IDVM.auto_batch = 2;
                                                            }
                                                            else
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = " Add Auto Or Batch!";
                                                                errorMessage = "Add Auto Or Batch!";
                                                            }
                                                        }
                                                        else if (BatchManaged.ToLower() == "no" || BatchManaged == "")
                                                        {
                                                            IDVM.BATCH_MANAGED = false;
                                                        }
                                                        else
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = BatchManaged;
                                                            errorMessage = BatchManaged + "Add yes/no only!";
                                                        }
                                                        if (IDVM.BATCH_MANAGED == true)
                                                        {
                                                            if (IDVM.QUALITY_MANAGED == true)
                                                            {
                                                                var ShelfLife = a[Array.IndexOf(ItemColumnArray, "ShelfLife")].ToString().Trim();
                                                                if (ShelfLife == "")
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = ShelfLife;
                                                                    errorMessage = "ShelfLife not found!";
                                                                }
                                                                else
                                                                {
                                                                    IDVM.SHELF_LIFE = int.Parse(ShelfLife);
                                                                    var ShelfLifeUoM = a[Array.IndexOf(ItemColumnArray, "ShelfLifeUoM")].ToString().Trim();
                                                                    if (ShelfLifeUoM == "")
                                                                    {
                                                                        errorList++;
                                                                        error[error.Length - 1] = ShelfLifeUoM;
                                                                        errorMessage = "ShelfLifeUoM is blank!";
                                                                    }
                                                                    else
                                                                    {
                                                                        var ShelfLifeUoMid = _genericservice.GetShelfLifeId(ShelfLifeUoM);
                                                                        if (ShelfLifeUoMid == 0)
                                                                        {
                                                                            errorList++;
                                                                            error[error.Length - 1] = ShelfLifeUoM;
                                                                            errorMessage = ShelfLifeUoM + " ShelfLifeUoM not found! ";
                                                                        }
                                                                        else
                                                                        {
                                                                            IDVM.SHELF_LIFE_UOM_ID = 1;
                                                                        }
                                                                    }

                                                                }
                                                            }
                                                            else
                                                            {
                                                                IDVM.SHELF_LIFE_UOM_ID = 0;
                                                                IDVM.SHELF_LIFE = 0;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            IDVM.SHELF_LIFE = 0;
                                                            IDVM.SHELF_LIFE_UOM_ID = 0;
                                                        }
                                                        var PreferredVendor = a[Array.IndexOf(ItemColumnArray, "PreferredVendor")].ToString().Trim();
                                                        if (PreferredVendor == "" || PreferredVendor == " ")
                                                        {
                                                            IDVM.PREFERRED_VENDOR_ID = 0;
                                                        }
                                                        else
                                                        {
                                                            var PreferredVendorid = _genericservice.GetVendorId(PreferredVendor);
                                                            if (PreferredVendorid == 0)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = PreferredVendor;
                                                                errorMessage = PreferredVendor + " PreferredVendor not found!";
                                                            }
                                                            else
                                                            {
                                                                IDVM.PREFERRED_VENDOR_ID = PreferredVendorid;
                                                            }
                                                        }
                                                        IDVM.VENDOR_PART_NUMBER = a[Array.IndexOf(ItemColumnArray, "VendorPartNumber")].ToString().Trim();

                                                        var MinimumLevel = a[Array.IndexOf(ItemColumnArray, "MinimumLevel")].ToString().Trim();
                                                        if (MinimumLevel == "")
                                                        {
                                                            IDVM.MINIMUM_LEVEL = 0;
                                                        }
                                                        else
                                                        {
                                                            IDVM.MINIMUM_LEVEL = int.Parse(MinimumLevel);
                                                        }
                                                        var MaximumLevel = a[Array.IndexOf(ItemColumnArray, "MaximumLevel")].ToString().Trim();
                                                        if (MaximumLevel == "")
                                                        {
                                                            IDVM.MAXIMUM_LEVEL = 0;
                                                        }
                                                        else
                                                        {
                                                            IDVM.MAXIMUM_LEVEL = int.Parse(MaximumLevel);
                                                        }
                                                        var ReorderLevel = a[Array.IndexOf(ItemColumnArray, "ReorderLevel")].ToString().Trim();
                                                        if (ReorderLevel == "")
                                                        {
                                                            IDVM.REORDER_LEVEL = 0;
                                                        }
                                                        else
                                                        {
                                                            IDVM.REORDER_LEVEL = int.Parse(ReorderLevel);
                                                        }
                                                        var ReorderQuality = a[Array.IndexOf(ItemColumnArray, "ReorderQuality")].ToString().Trim();
                                                        if (ReorderQuality == "")
                                                        {
                                                            IDVM.REORDER_QUANTITY = 0;
                                                        }
                                                        else
                                                        {
                                                            IDVM.REORDER_QUANTITY = Double.Parse(ReorderQuality);
                                                        }
                                                        var mrp = a[Array.IndexOf(ItemColumnArray, "MRP")].ToString().Trim();
                                                        if (mrp.ToLower() == "yes")
                                                        {
                                                            IDVM.MRP = true;
                                                        }
                                                        else if (mrp.ToLower() == "no" || mrp == "")
                                                        {
                                                            IDVM.MRP = false;
                                                        }
                                                        else
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = mrp;
                                                            errorMessage = mrp + "Add yes/no only!";
                                                        }
                                                        var ItemValuation = a[Array.IndexOf(ItemColumnArray, "ItemValuation")].ToString().Trim();
                                                        if (ItemValuation == "")
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = ItemValuation;
                                                            errorMessage = "ItemValuation is blank!";
                                                        }
                                                        else
                                                        {
                                                            var ItemValuationid = _genericservice.GetItemValuationId(ItemValuation);
                                                            if (ItemValuationid == 0)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = ItemValuation;
                                                                errorMessage = ItemValuation + " ItemValuation not found";
                                                            }
                                                            else if (ItemValuationid == 1)
                                                            {
                                                                IDVM.ITEM_VALUATION_ID = ItemValuationid;
                                                            }
                                                            else
                                                            {
                                                                IDVM.ITEM_VALUATION_ID = ItemValuationid;
                                                                var plantList = _plantService.GetPlant();
                                                                for (int i = 0; i < plantList.Count; i++)
                                                                {
                                                                    REF_PLANT_VM plant = new REF_PLANT_VM();
                                                                    plant.item_code = itemCode;
                                                                    REF_PLANT_VM.Add(plant);
                                                                }

                                                            }
                                                        }
                                                        var ItemAccounting = a[Array.IndexOf(ItemColumnArray, "ItemAccounting")].ToString().Trim();
                                                        if (ItemAccounting == "")
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = ItemAccounting;
                                                            errorMessage = "ItemAccounting not found";
                                                        }
                                                        else
                                                        {
                                                            var ItemAccountingid = _genericservice.GetItemAccountingId(ItemAccounting);
                                                            if (ItemAccountingid == 0)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = ItemAccounting;
                                                                errorMessage = ItemAccounting + " ItemAccounting not found";
                                                            }
                                                            else
                                                            {
                                                                IDVM.ITEM_ACCOUNTING_ID = ItemAccountingid;
                                                                if (IDVM.ITEM_ACCOUNTING_ID == 2)
                                                                {
                                                                    var category_length = _genericservice.GetLedgerAccountTypeByItem(4, IDVM.ITEM_CATEGORY_ID, IDVM.item_type_id);
                                                                    foreach (var x in category_length)
                                                                    //for (var i = 0; i < category_length.Count; i++)
                                                                    {
                                                                        duplicateGLExcel dupli = new duplicateGLExcel();
                                                                        dupli.itemCode = IDVM.ITEM_CODE;
                                                                        duplicateGLExcel.Add(dupli);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    var itemAccounting = _genericservice.GetLedgerAccountTypeByItem(4, IDVM.ITEM_CATEGORY_ID, IDVM.item_type_id);
                                                                    foreach (var x in itemAccounting)
                                                                    {
                                                                        item_category_gl_Excel GE1 = new item_category_gl_Excel();
                                                                        GE1.itemCode = IDVM.ITEM_CODE;
                                                                        GE1.ledger_account_type_id = x.ledger_account_type_id;
                                                                        GE1.gl_ledger_id = x.gl_ledger_id;
                                                                        item_category_gl_Excel.Add(GE1);

                                                                    }

                                                                }
                                                            }
                                                        }
                                                        var ExciseCatagory = a[Array.IndexOf(ItemColumnArray, "HSNCatagory")].ToString().Trim();
                                                        if (ExciseCatagory == "")
                                                        {
                                                            IDVM.EXCISE_CATEGORY_ID = 0;
                                                        }
                                                        else
                                                        {
                                                            var ExciseCatagoryid = _genericservice.GetExcisecategoryid(ExciseCatagory);
                                                            if (ExciseCatagoryid == 0)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = ExciseCatagory;
                                                                errorMessage = ExciseCatagory + " ExciseCatagory not found";
                                                            }
                                                            else
                                                            {
                                                                IDVM.EXCISE_CATEGORY_ID = ExciseCatagoryid;
                                                            }
                                                        }
                                                        IDVM.EXCISE_CHAPTER_NO = a[Array.IndexOf(ItemColumnArray, "HSN")].ToString().Trim();

                                                        IDVM.ADDITIONAL_INFORMATION = a[Array.IndexOf(ItemColumnArray, "AdditionalInfo")].ToString().Trim();
                                                        var blocked = a[Array.IndexOf(ItemColumnArray, "Blocked")].ToString().Trim();
                                                        if (blocked.ToLower() == "yes")
                                                        {
                                                            IDVM.IS_BLOCKED = true;
                                                        }
                                                        else if (blocked.ToLower() == "no" || blocked == "")
                                                        {
                                                            IDVM.IS_BLOCKED = false;
                                                        }
                                                        else
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = blocked;
                                                            errorMessage = "Add only yes or no blocked .";
                                                        }
                                                        item_list.Add(IDVM);
                                                    }
                                                    else
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = itemCode;
                                                        errorMessage = "item Code already exist!";
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            errorList++;
                                            error[error.Length - 1] = "Check Headers Name!";
                                            errorMessage = "Check Headers Name!";
                                        }


                                    }
                                }

                            }
                            else if (sheet.TableName == "UomDetails" && errorMessage == "")
                            {
                                string[] UomColumnArray = new string[sheet.Columns.Count];
                                var TempSheetColumn = from DataColumn Tempcolumn in sheet.Columns select Tempcolumn;
                                foreach (var ary1 in TempSheetColumn)
                                {
                                    UomColumnArray[contcol] = ary1.ToString();
                                    contcol = contcol + 1;
                                }

                                if (sheet != null)
                                {
                                    var TempSheetRows = from DataRow TempRow in sheet.Rows select TempRow;
                                    foreach (var ary in TempSheetRows)
                                    {
                                        var a = ary.ItemArray;
                                        if (ValidateUoMExcelColumns(UomColumnArray) == true)
                                        {
                                            string sr_no = a[Array.IndexOf(UomColumnArray, "SrNo")].ToString().Trim();
                                            if (sr_no != "")
                                            {
                                                if (Uom_Excel.Count != 0)
                                                {
                                                    Uom_Excel CE = new Uom_Excel();
                                                    var item_CODE = a[Array.IndexOf(UomColumnArray, "ItemCode")].ToString().Trim();
                                                    var itemCode = item_list.Where(x => x.ITEM_CODE == item_CODE).FirstOrDefault();
                                                    if (itemCode != null)
                                                    {
                                                        CE.itemCode = item_CODE;
                                                        var UoM = a[Array.IndexOf(UomColumnArray, "UoM")].ToString().Trim();
                                                        if (UoM == "")
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = UoM;
                                                            errorMessage = "UoM is blank!";
                                                        }
                                                        else
                                                        {
                                                            var uom_id = _genericservice.GetUoMId(UoM);
                                                            if (uom_id == 0)
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = UoM;
                                                                errorMessage = UoM + " UoM not found!";
                                                            }
                                                            else
                                                            {
                                                                CE.uom_id = uom_id;
                                                            }
                                                        }
                                                        var ConversionRate = a[Array.IndexOf(UomColumnArray, "ConversionRate")].ToString().Trim();
                                                        if (ConversionRate == "")
                                                        {
                                                            CE.conversion_rate = 0;
                                                        }
                                                        else
                                                        {
                                                            CE.conversion_rate = Double.Parse(ConversionRate);
                                                        }
                                                        Uom_Excel.Add(CE);
                                                    }
                                                    else
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = item_CODE;
                                                        errorMessage = item_CODE + "item_CODE not found!";
                                                    }
                                                }
                                                else
                                                {
                                                    Uom_Excel CE = new Uom_Excel();
                                                    CE.itemCode = a[Array.IndexOf(UomColumnArray, "ItemCode")].ToString().Trim();
                                                    var UoM = a[Array.IndexOf(UomColumnArray, "UoM")].ToString().Trim();
                                                    if (UoM == "")
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = UoM;
                                                        errorMessage = "UoM is blank!";
                                                    }
                                                    else
                                                    {
                                                        var uom_id = _genericservice.GetUoMId(UoM);
                                                        if (uom_id == 0)
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = UoM;
                                                            errorMessage = UoM + " UoM not found!";
                                                        }
                                                        else
                                                        {
                                                            CE.uom_id = uom_id;
                                                        }
                                                    }
                                                    var ConversionRate = a[Array.IndexOf(UomColumnArray, "ConversionRate")].ToString().Trim();
                                                    if (ConversionRate == "")
                                                    {
                                                        CE.conversion_rate = 0;
                                                    }
                                                    else
                                                    {
                                                        CE.conversion_rate = Double.Parse(ConversionRate);
                                                    }
                                                    Uom_Excel.Add(CE
                                                        );
                                                }

                                            }

                                        }
                                        else
                                        {
                                            errorList++;
                                            errorMessage = "Check Headers Name.";
                                            error[error.Length - 1] = "Check Headers Name.";
                                        }
                                    }
                                }
                            }
                            else if (sheet.TableName == "GLAccounts" && errorMessage == "")
                            {
                                string[] GLColumnArray = new string[sheet.Columns.Count];
                                var itemCodeCount = item_list.Count;
                                var TempSheetColumn = from DataColumn Tempcolumn in sheet.Columns select Tempcolumn;
                                foreach (var ary1 in TempSheetColumn)
                                {
                                    GLColumnArray[glcol] = ary1.ToString();
                                    glcol = glcol + 1;
                                }
                                if (sheet != null)
                                {
                                    var TempSheetRows = from DataRow TempRow in sheet.Rows select TempRow;
                                    foreach (var ary in TempSheetRows)
                                    {
                                        var a = ary.ItemArray;
                                        if (ValidateGLExcelColumns(GLColumnArray) == true)
                                        {
                                            string sr_no = a[Array.IndexOf(GLColumnArray, "SrNo")].ToString().Trim();
                                            if (sr_no != "")

                                                if (item_category_gl_Excel.Count != 0)
                                                {
                                                    item_category_gl_Excel GE = new item_category_gl_Excel();
                                                    var Item_Code = a[Array.IndexOf(GLColumnArray, "ItemCode")].ToString().Trim();
                                                    // var ItemCode = item_list.Where(x => x.ITEM_CODE == Item_Code).FirstOrDefault();
                                                    if (Item_Code == null)
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = Item_Code + " not found";
                                                        errorMessage = Item_Code + " Item_Code not found!";
                                                    }
                                                    else
                                                    {
                                                        var ItemCode = item_list.Where(x => x.ITEM_CODE == Item_Code).FirstOrDefault();
                                                        if (ItemCode == null)
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = Item_Code + " not found";
                                                            errorMessage = Item_Code + " Item_Code not found!";
                                                        }
                                                        else
                                                        {
                                                            if (ItemCode.ITEM_ACCOUNTING_ID == 2)
                                                            {
                                                                GE.itemCode = Item_Code;
                                                                var gl_account_type = a[Array.IndexOf(GLColumnArray, "LedgerAccountType")].ToString().Trim();

                                                                if (gl_account_type == "")
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = gl_account_type;
                                                                    errorMessage = "Add gl_account type!";
                                                                }
                                                                else
                                                                {
                                                                    var gl_account_type_id = _genericservice.GetLedgerAccountTypeId(gl_account_type, 4);
                                                                    if (gl_account_type_id != 0)
                                                                    {
                                                                        var glAccountTypeDupli = item_category_gl_Excel.Where(x => x.ledger_account_type_id == gl_account_type_id);
                                                                        if (glAccountTypeDupli != null)
                                                                        {
                                                                            GE.ledger_account_type_id = gl_account_type_id;
                                                                            var removedupli = duplicateGLExcel.Where(x => x.itemCode == GE.itemCode).FirstOrDefault();
                                                                            duplicateGLExcel.Remove(removedupli);

                                                                        }
                                                                        else
                                                                        {
                                                                            errorList++;
                                                                            error[error.Length - 1] = gl_account_type + " duplicate!";
                                                                            errorMessage = gl_account_type + " duplicate!";
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        errorList++;
                                                                        error[error.Length - 1] = gl_account_type;
                                                                        errorMessage = gl_account_type + " gl_account_type not found!";
                                                                    }
                                                                }
                                                                var GeneralLedger = a[Array.IndexOf(GLColumnArray, "GeneralLedgerCode")].ToString().Trim();
                                                                if (GeneralLedger == "")
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = GeneralLedger;
                                                                    errorMessage = "Add Ledger Code.";
                                                                }
                                                                else
                                                                {
                                                                    var ledger_code_id = _genericservice.GetGLId(GeneralLedger);
                                                                    if (ledger_code_id == 0)
                                                                    {
                                                                        errorList++;
                                                                        error[error.Length - 1] = GeneralLedger;
                                                                        errorMessage = GeneralLedger + " GeneralLedger not found!";
                                                                    }
                                                                    else
                                                                    {
                                                                        GE.gl_ledger_id = (int)ledger_code_id;
                                                                    }
                                                                }
                                                                item_category_gl_Excel.Add(GE);
                                                            }
                                                        }



                                                    }
                                                }
                                                else
                                                {

                                                    item_category_gl_Excel GE = new item_category_gl_Excel();
                                                    var Item_Code = a[Array.IndexOf(GLColumnArray, "ItemCode")].ToString();
                                                    var ItemCode = item_list.Where(x => x.ITEM_CODE == Item_Code).FirstOrDefault();
                                                    if (Item_Code == null)
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = Item_Code + " not found";
                                                        errorMessage = Item_Code + " Item_Code not found!";
                                                    }
                                                    else
                                                    {
                                                        if (ItemCode.ITEM_ACCOUNTING_ID == 2)
                                                        {
                                                            GE.itemCode = Item_Code;
                                                            var gl_account_type = a[Array.IndexOf(GLColumnArray, "LedgerAccountType")].ToString().Trim();
                                                            if (gl_account_type == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = gl_account_type;
                                                                errorMessage = "Add gl_account type!";
                                                            }
                                                            else
                                                            {
                                                                var gl_account_type_id = _genericservice.GetLedgerAccountTypeId(gl_account_type, 4);
                                                                if (gl_account_type_id != 0)
                                                                {
                                                                    GE.ledger_account_type_id = gl_account_type_id;
                                                                }
                                                                else
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = gl_account_type;
                                                                    errorMessage = gl_account_type + " gl_account_type not found!";
                                                                }
                                                            }
                                                            var GeneralLedger = a[Array.IndexOf(GLColumnArray, "GeneralLedgerCode")].ToString().Trim();
                                                            if (GeneralLedger == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = GeneralLedger;
                                                                errorMessage = "Add Ledger Code.";
                                                            }
                                                            else
                                                            {
                                                                var ledger_code_id = _genericservice.GetGLId(GeneralLedger);
                                                                if (ledger_code_id == 0)
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = GeneralLedger;
                                                                    errorMessage = GeneralLedger + " GeneralLedger not found!";
                                                                }
                                                                else
                                                                {
                                                                    GE.gl_ledger_id = (int)ledger_code_id;
                                                                }
                                                            }
                                                            item_category_gl_Excel.Add(GE);
                                                            var removedupli = duplicateGLExcel.Where(x => x.itemCode == GE.itemCode).FirstOrDefault();
                                                            duplicateGLExcel.Remove(removedupli);
                                                        }
                                                    }
                                                }

                                        }
                                    }
                                }
                            }
                            else if (sheet.TableName == "Parameter" && errorMessage == "")
                            {
                                string[] ParamColumnArray = new string[sheet.Columns.Count];
                                var itemCodeCount = item_list.Count;
                                var TempSheetColumn = from DataColumn Tempcolumn in sheet.Columns select Tempcolumn;
                                foreach (var ary1 in TempSheetColumn)
                                {
                                    ParamColumnArray[prmcol] = ary1.ToString();
                                    prmcol = prmcol + 1;
                                }
                                if (sheet != null)
                                {
                                    var TempSheetRows = from DataRow TempRow in sheet.Rows select TempRow;
                                    foreach (var ary in TempSheetRows)
                                    {
                                        var a = ary.ItemArray;
                                        if (ValidateParamExcelColumns(ParamColumnArray) == true)
                                        {
                                            string sr_no = a[Array.IndexOf(ParamColumnArray, "SrNo")].ToString().Trim();
                                            if (sr_no != "")
                                            {
                                                parameter_Excel prm = new parameter_Excel();
                                                prm.itemCode = a[Array.IndexOf(ParamColumnArray, "ItemCode")].ToString().Trim();
                                                prm.parameter_name = a[Array.IndexOf(ParamColumnArray, "ParameterName")].ToString();
                                                prm.parameter_range = a[Array.IndexOf(ParamColumnArray, "ParameterRange")].ToString();
                                                parameter_Excel.Add(prm);
                                            }
                                        }
                                    }
                                }
                            }
                            else if (sheet.TableName == "StandardCost" && errorMessage == "")
                            {
                                string[] StandardCostArray = new string[sheet.Columns.Count];
                                var itemCodeCount = item_list.Count;
                                var TempSheetColumn = from DataColumn Tempcolumn in sheet.Columns select Tempcolumn;
                                foreach (var ary1 in TempSheetColumn)
                                {
                                    StandardCostArray[stccol] = ary1.ToString();
                                    stccol = stccol + 1;
                                }
                                if (sheet != null)
                                {
                                    var TempSheetRows = from DataRow TempRow in sheet.Rows select TempRow;
                                    foreach (var ary in TempSheetRows)
                                    {
                                        var a = ary.ItemArray;
                                        if (ValidateStandardCost(StandardCostArray) == true)
                                        {
                                            string sr_no = a[Array.IndexOf(StandardCostArray, "SrNo")].ToString();
                                            if (sr_no != "")
                                            {
                                                if (REF_PLANT_VM.Count != 0)
                                                {
                                                    ref_item_plant_vm prm = new ref_item_plant_vm();
                                                    var Item_Code = a[Array.IndexOf(StandardCostArray, "ItemCode")].ToString().Trim();
                                                    // var ItemCode = item_list.Where(x => x.ITEM_CODE == Item_Code).FirstOrDefault();
                                                    if (Item_Code == "")
                                                    {
                                                        errorList++;
                                                        error[error.Length - 1] = Item_Code + " ItemCode is Blank";
                                                        errorMessage = Item_Code + " ItemCode is Blank!";
                                                    }
                                                    else
                                                    {
                                                        var ItemCode = item_list.Where(x => x.ITEM_CODE == Item_Code).FirstOrDefault();
                                                        if (ItemCode == null)
                                                        {
                                                            errorList++;
                                                            error[error.Length - 1] = Item_Code + " not found";
                                                            errorMessage = Item_Code + " Item_Code not found!";
                                                        }
                                                        else
                                                        {
                                                            prm.itemCode = Item_Code;
                                                            var plant_name = a[Array.IndexOf(StandardCostArray, "PlantName")].ToString().Trim();
                                                            if (prm.plant_name == "")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = plant_name + " plant name is blank!";
                                                                errorMessage = plant_name + " plant name is blank!";
                                                            }
                                                            else
                                                            {
                                                                prm.plant_name = plant_name;
                                                                var plant_id = _genericservice.GetPlantID(prm.plant_name);
                                                                if (plant_id == 0)
                                                                {
                                                                    errorList++;
                                                                    error[error.Length - 1] = plant_name + " not found";
                                                                    errorMessage = "plant_name not found!";
                                                                }
                                                                else
                                                                {
                                                                    prm.plant_id = plant_id;
                                                                }
                                                            }
                                                            var item_value = a[Array.IndexOf(StandardCostArray, "Value")].ToString().Trim();
                                                            if (item_value == "" || item_value == "0")
                                                            {
                                                                errorList++;
                                                                error[error.Length - 1] = item_value + " not found";
                                                                errorMessage = "item_value not found!";
                                                            }
                                                            else
                                                            {
                                                                prm.item_value = Double.Parse(item_value);
                                                            }
                                                            ref_item_plant_vm.Add(prm);
                                                            var removeplant = REF_PLANT_VM.Where(x => x.item_code == Item_Code).FirstOrDefault();
                                                            REF_PLANT_VM.Remove(removeplant);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    excelReader.Close();
                    if (errorMessage == "")
                    {
                        foreach (var item in item_list)
                        {
                            if (item.QUALITY_MANAGED == true)
                            {
                                if (parameter_Excel.Count == 0)
                                {
                                    errorList++;
                                    error[error.Length - 1] = "Add Parameter list for " + item.ITEM_CODE;
                                    errorMessage = "Add Parameter list for " + item.ITEM_CODE;
                                }
                                else
                                {
                                    var prm = parameter_Excel.Where(x => x.itemCode == item.ITEM_CODE).FirstOrDefault();
                                    if (prm != null)
                                    {
                                        var remove = qality_batch.Where(x => x.item_code == item.ITEM_CODE).FirstOrDefault();
                                        qality_batch.Remove(remove);
                                    }
                                    else
                                    {
                                        errorList++;
                                        error[error.Length - 1] = "Parameter list for " + item.ITEM_CODE + " not found";
                                        errorMessage = "Parameter list for " + item.ITEM_CODE + " not found";
                                    }
                                    //qb.item_code = IDVM.ITEM_CODE;
                                    //qality_batch.Add(qb);
                                }
                            }
                        }
                    }

                    if (errorMessage == "")
                    {
                        if (duplicateGLExcel.Count == 0)
                        {
                            if (REF_PLANT_VM.Count == 0)
                            {
                                if (qality_batch.Count == 0)
                                {
                                    var isSucess = _item_service.AddExcel(item_list, Uom_Excel, item_category_gl_Excel, parameter_Excel, ref_item_plant_vm);
                                    if (isSucess == "Saved")
                                    {
                                        errorMessage = "success";
                                        return Json(errorMessage, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        errorMessage = "Failed";
                                    }
                                }
                                else
                                {
                                    errorMessage = "Add Parameter list for Item Code not found";
                                }

                            }
                            else
                            {
                                errorMessage = "Add Item value for all Plant in StandardCost sheet!";
                            }
                        }
                        else
                        {
                            errorList++;
                            error[error.Length - 1] = "Add All GL Item! ";
                            errorMessage = "Add All GL Item!";
                        }
                    }

                }
                else
                {
                    errorList++;
                    error[error.Length - 1] = "Select File to Upload.";
                    errorMessage = "Select File to Upload.";
                }
            }
            //return Json(new { Status = Message, text = errorList, error = error, errorMessage = errorMessage }, JsonRequestBehavior.AllowGet);
            return Json(errorList + " - " + errorMessage, JsonRequestBehavior.AllowGet);
        }
    }
}
