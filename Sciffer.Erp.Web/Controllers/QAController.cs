using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using System;
using System.Net;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class QAController : Controller
    {
        private readonly IQAService _QAService;
        private readonly IDocumentTypeService _DocumentType;
        private readonly IItemService _itemService;
        private readonly IStatusService _statusService;
        private readonly IGenericService _generic;
        private readonly ISalesCategoryService _salecategoryService;
        private readonly IGrnService _grnService;
        public QAController(IQAService QAService,IDocumentTypeService documentType, IItemService itemService, IStatusService statusService,
            IGenericService generic,ISalesCategoryService salecategoryService, IGrnService grnService)
        {
            _QAService = QAService;
            _DocumentType = documentType;
            _itemService = itemService;
            _statusService = statusService;
            _generic = generic;
            _salecategoryService = salecategoryService;
            _grnService = grnService;
        }
        [CustomAuthorizeAttribute("ONQA")]
        // GET: QA
        public ActionResult Index()
        {
            ViewBag.doc = TempData["doc_num"];
            ViewBag.DataSource = _QAService.GetAll();
            return View();
        }

        // GET: QA/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pur_qa_VM pur_qa = _QAService.Get(id);
            if (pur_qa == null)
            {
                return HttpNotFound();
            }
            return View(pur_qa);
        }

        [CustomAuthorizeAttribute("ONQA")]
        // GET: QA/Create
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.itemList = new SelectList(_itemService.GetItemList(), "ITEM_ID", "ITEM_CODE");
            ViewBag.statusList = new SelectList(_generic.GetStatusList("QA"), "status_id", "status_name");
            ViewBag.Parameter = new SelectList(_generic.GetParameter(), "parameter_id", "parameter_name");
            ViewBag.categorylist = new SelectList(_generic.GetCategoryList(77), "document_numbring_id", "category");
            ViewBag.date = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.Grn = new SelectList(_QAService.GetSourceDocument(), "document_id", "document_number");
            ViewBag.DocumentTypeCode = new SelectList(_DocumentType.GetAll(), "document_type_code", "document_type_name");
            ViewBag.plantlist = new SelectList(_generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.slocList = new SelectList(_generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            return View();
        }

        // POST: QA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(pur_qa_VM vm)
        {
            var issaved = "";
            if (ModelState.IsValid)
            {
                issaved = _QAService.Add(vm);
                if (issaved.Contains("Saved"))
                {
                    TempData["doc_num"] = issaved.Split('~')[1];
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = issaved;
            ViewBag.itemList = new SelectList(_itemService.GetItemList(), "ITEM_ID", "ITEM_CODE");
            ViewBag.statusList = new SelectList(_generic.GetStatusList("QA"), "status_id", "status_name");
            ViewBag.Parameter = new SelectList(_generic.GetParameter(), "parameter_id", "parameter_name");
            ViewBag.categorylist = new SelectList(_generic.GetCategoryList(77), "document_numbring_id", "category");
            ViewBag.date = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.Grn = new SelectList(_QAService.GetSourceDocument(), "document_id", "document_number");
            ViewBag.DocumentTypeCode = new SelectList(_DocumentType.GetAll(), "document_type_code", "document_type_name");
            ViewBag.plantlist = new SelectList(_generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.slocList = new SelectList(_generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            return View(vm);
        }

        [CustomAuthorizeAttribute("ONQA")]
        // GET: QA/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pur_qa_VM pur_qa = _QAService.Get(id);
            if (pur_qa == null)
            {
                return HttpNotFound();
            }
            ViewBag.error = "";
            ViewBag.itemList = new SelectList(_itemService.GetItemList(), "ITEM_ID", "ITEM_CODE");
            ViewBag.statusList= new SelectList(_generic.GetStatusList("QA"), "status_id", "status_name");
            ViewBag.Parameter = new SelectList(_generic.GetParameter(), "parameter_id", "parameter_name");
            ViewBag.categorylist = new SelectList(_generic.GetCategoryList(77), "document_numbring_id", "category");
            ViewBag.date = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.Grn = new SelectList(_QAService.GetSourceDocument(), "document_id", "document_number");
            ViewBag.DocumentTypeCode = new SelectList(_DocumentType.GetAll(), "document_type_code", "document_type_name");
            ViewBag.plantlist = new SelectList(_generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.slocList = new SelectList(_generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            return View(pur_qa);
        }

        // POST: QA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(pur_qa_VM vm)
        {
            var issaved = "";
            if (ModelState.IsValid)
            {
                issaved = _QAService.Add(vm);
                if (issaved.Contains("Saved"))
                {
                    TempData["doc_num"] = issaved.Split('~')[1] + " Updated Successfully";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = issaved;
            ViewBag.itemList = new SelectList(_itemService.GetItemList(), "ITEM_ID", "ITEM_CODE");
            ViewBag.statusList = new SelectList(_generic.GetStatusList("QA"), "status_id", "status_name");
            ViewBag.Parameter = new SelectList(_generic.GetParameter(), "parameter_id", "parameter_name");
            ViewBag.categorylist = new SelectList(_generic.GetCategoryList(77), "document_numbring_id", "category");
            ViewBag.date = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.Grn = new SelectList(_QAService.GetSourceDocument(), "document_id", "document_number");
            ViewBag.DocumentTypeCode = new SelectList(_DocumentType.GetAll(), "document_type_code", "document_type_name");
            ViewBag.plantlist = new SelectList(_generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.slocList = new SelectList(_generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            return View(vm);
        }

     

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        qa.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
