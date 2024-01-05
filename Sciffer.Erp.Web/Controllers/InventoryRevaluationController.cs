using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class InventoryRevaluationController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IUOMService _uOMService;
        private readonly IGeneralLedgerService _generalLedgerService;
        private readonly ICategoryService _categoryService;
        private readonly IRevaluationService _revaluationService;
        private readonly IGenericService _Generic;
        public InventoryRevaluationController(IGenericService gen, IRevaluationService RevaluationService, IGeneralLedgerService GeneralLedgerService,  ICategoryService CategoryService,   IUOMService UOMService,  IItemService ItemService)
        {
            _revaluationService = RevaluationService;
              _itemService = ItemService;
            _uOMService = UOMService;
            _generalLedgerService = GeneralLedgerService;
            _categoryService = CategoryService;
            _Generic = gen;
        }

        [CustomAuthorizeAttribute("INVRV")]
        public ActionResult Index()
        {
            ViewBag.doc = TempData["reavil_no"];
            ViewBag.DataSource = _revaluationService.GetAll();
            return View();
        }
       
      
        // GET: InventoryRevaluation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inv_revaluation_vm rEF_INVENTORY_REVALUATION = _revaluationService.Get(id);
            if (rEF_INVENTORY_REVALUATION == null)
            {
                return HttpNotFound();
            }
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.category_list = new SelectList(_categoryService.GetAll(), "CATEGORY_ID", "CATEGORY_NAME");
            ViewBag.Item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.gl_list = _generalLedgerService.GetAll();
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(83), "document_numbring_id", "category");
            return View(rEF_INVENTORY_REVALUATION);
        }

        // GET: InventoryRevaluation/Create
        [CustomAuthorizeAttribute("INVRV")]
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.Item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.gl_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(83), "document_numbring_id", "category");
            return View();
        }

        // POST: InventoryRevaluation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(inv_revaluation_vm revaluation)
        {
            var issaved = "";
            if (ModelState.IsValid)
            {
                issaved = _revaluationService.Add(revaluation);
                if (issaved.Contains("Saved"))
                {
                    TempData["reavil_no"] = issaved.Split('~')[1];
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = issaved;
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.category_list = new SelectList(_categoryService.GetAll(), "CATEGORY_ID", "CATEGORY_NAME");
            ViewBag.Item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.gl_list = new SelectList(_generalLedgerService.GetAll(), "gl_ledger_id", "gl_ledger_code");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(83), "document_numbring_id", "category");
            return View(revaluation);
        }

        // GET: InventoryRevaluation/Edit/5
        [CustomAuthorizeAttribute("INVRV")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inv_revaluation_vm rEF_INVENTORY_REVALUATION = _revaluationService.Get(id);
            if (rEF_INVENTORY_REVALUATION == null)
            {
                return HttpNotFound();
            }
            ViewBag.error = "";
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.Item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.gl_list = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(83), "document_numbring_id", "category");
            return View(rEF_INVENTORY_REVALUATION);
        }

        // POST: InventoryRevaluation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(inv_revaluation_vm revaluation)
        {
            var issaved = "";
            if (ModelState.IsValid)
            {
                issaved = _revaluationService.Add(revaluation);
                if (issaved.Contains("Saved"))
                {
                    TempData["reavil_no"] = issaved.Split('~')[1];
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = issaved;
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.category_list = new SelectList(_categoryService.GetAll(), "CATEGORY_ID", "CATEGORY_NAME");
            ViewBag.Item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.gl_list = _generalLedgerService.GetAll();
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(83), "document_numbring_id", "category");
            return View(revaluation);
        }    
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _revaluationService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
