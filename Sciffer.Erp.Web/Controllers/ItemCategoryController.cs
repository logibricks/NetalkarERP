using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class ItemCategoryController : Controller
    {
        private readonly IItemCategoryService _itemService;
        private readonly IGenericService _Generic;
        private readonly IGeneralLedgerService __generalLedgerService;
        private readonly IItemService _itemservice;
        private readonly IItemTypeService _ItemTypeService;
        public ItemCategoryController(IItemCategoryService itemService, IGenericService gen, IGeneralLedgerService GeneralLedgerService,
            IItemService ItemService, IItemTypeService ItemTypeService)
        {
            _itemService = itemService;
            _Generic = gen;
            __generalLedgerService = GeneralLedgerService;
            _itemservice = ItemService;
            _ItemTypeService = ItemTypeService;
        }

        // GET: ItemCategory
        [CustomAuthorizeAttribute("ITMCT")]
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


        // GET: ItemCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_ITEM_CATEGORYVM rEF_ITEM_CATEGORY = _itemService.Get((int)id);
            if (rEF_ITEM_CATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(rEF_ITEM_CATEGORY);
        }

        // GET: ItemCategory/Create
        [CustomAuthorizeAttribute("ITMCT")]
        public ActionResult Create()
        {
            ViewBag.generalleder = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.item_type_list = new SelectList(_ItemTypeService.GetAll(), "item_type_id", "item_type_name");
            return View();
        }

        // POST: ItemCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(REF_ITEM_CATEGORYVM rEF_ITEM_CATEGORY, FormCollection fc)
        {
            string ledgeraccounttype;
            ledgeraccounttype = fc["ledgeraccounttype"];
            rEF_ITEM_CATEGORY.ledgeraccounttype = ledgeraccounttype;
            if (ModelState.IsValid)
            {
                var isAdded = _itemService.Add(rEF_ITEM_CATEGORY);
                return RedirectToAction("Index");
            }
            ViewBag.generalleder = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.item_type_list = new SelectList(_ItemTypeService.GetAll(), "item_type_id", "item_type_name");
            return View(rEF_ITEM_CATEGORY);
        }

        // GET: ItemCategory/Edit/5
        [CustomAuthorizeAttribute("ITMCT")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_ITEM_CATEGORYVM rEF_ITEM_CATEGORY = _itemService.Get((int)id);
            if (rEF_ITEM_CATEGORY == null)
            {
                return HttpNotFound();
            }
            ViewBag.generalleder = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.item_type_list = new SelectList(_ItemTypeService.GetAll(), "item_type_id", "item_type_name");
            return View(rEF_ITEM_CATEGORY);
        }

        // POST: ItemCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(REF_ITEM_CATEGORYVM rEF_ITEM_CATEGORY, FormCollection fc)
        {
            string ledgeraccounttype;
            ledgeraccounttype = fc["ledgeraccounttype"];
            rEF_ITEM_CATEGORY.ledgeraccounttype = ledgeraccounttype;
            if (ModelState.IsValid)
            {
                var isAdded = _itemService.Add(rEF_ITEM_CATEGORY);
                if (isAdded == true)
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.generalleder = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.item_type_list = new SelectList(_ItemTypeService.GetAll(), "item_type_id", "item_type_name");
            return View(rEF_ITEM_CATEGORY);
        }

        // GET: ItemCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_ITEM_CATEGORYVM rEF_ITEM_CATEGORY = _itemService.Get((int)id);
            if (rEF_ITEM_CATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(rEF_ITEM_CATEGORY);
        }

        // POST: ItemCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var isDeleted = _itemService.Delete(id);
            if (isDeleted)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult GetLedgerAccountTypeByItem(int entity_type_id, int item_category_id,int? item_type_id)
        {
            var paymentService = _Generic.GetLedgerAccountTypeByItem(entity_type_id, item_category_id, item_type_id);
            return Json(paymentService, JsonRequestBehavior.AllowGet);
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
