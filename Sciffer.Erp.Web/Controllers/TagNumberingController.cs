using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Implementation;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class TagNumberingController : Controller
    {
        private readonly TagNumberingService _tagNumberingService;
        private readonly IGenericService _Generic;
        private readonly IFinancialYearService _financialService;

        public TagNumberingController(TagNumberingService tagNumberingService, IGenericService gen, IFinancialYearService FinancialService)
        {
            _tagNumberingService = tagNumberingService;
            _Generic = gen;
            _financialService = FinancialService;
        }

        public ActionResult InlineDelete(int key)
        {
            var res = _tagNumberingService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorizeAttribute("TAG-NUM")]
        public ActionResult InlineInsert(mfg_tag_numbering_VM value)
        {

            if (value.tag_numbering_id == 0)
            {
                var data1 = _tagNumberingService.Add(value);

                return Json(data1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data1 = _tagNumberingService.Update(value);

                return Json(data1, JsonRequestBehavior.AllowGet);
            }
        }

        [CustomAuthorizeAttribute("TAG-NUM")]
        // GET: Currency
        public ActionResult Index()
        {
            var finance = _financialService.GetAll();
            ViewBag.financelist = new SelectList(finance, "FINANCIAL_YEAR_ID", "FINANCIAL_YEAR_NAME");
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.DataSource = _tagNumberingService.GetTagNumbering();
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tagNumberingService.Dispose();
            }
            base.Dispose(disposing);
        }

        [CustomAuthorizeAttribute("TAG-NUM")]
        // GET: QualityParameter/Create
        public ActionResult Create()
        {
            var finance = _financialService.GetAll();
            ViewBag.financelist = new SelectList(finance, "FINANCIAL_YEAR_ID", "FINANCIAL_YEAR_NAME");
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View();
        }

        // POST: QualityParameter/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(mfg_tag_numbering_VM value)
        {
            mfg_tag_numbering_VM result = null;

            result = _tagNumberingService.Add(value);
            if (result != null)
            {
                return RedirectToAction("Index");
            }

            var finance = _financialService.GetAll();
            ViewBag.financelist = new SelectList(finance, "FINANCIAL_YEAR_ID", "FINANCIAL_YEAR_NAME");
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View(result);
        }

        [CustomAuthorizeAttribute("TAG-NUM")]
        // GET: QualityParameter/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            mfg_tag_numbering_VM mfg_tag_numbering_vm = _tagNumberingService.Get(id);
            if (mfg_tag_numbering_vm == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemList = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.MachineList = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.OperationList = new SelectList(_Generic.GetOperationList(), "process_id", "process_description");
            return View(mfg_tag_numbering_vm);
        }

        // POST: QualityParameter/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(mfg_tag_numbering_VM vm)
        {
            mfg_tag_numbering_VM result = null;

            result = _tagNumberingService.Update(vm);
            if (result != null)
            {
                return RedirectToAction("Index");
            }

            var finance = _financialService.GetAll();
            ViewBag.financelist = new SelectList(finance, "FINANCIAL_YEAR_ID", "FINANCIAL_YEAR_NAME");
            ViewBag.item_list = new SelectList(_Generic.GetCatWiesItemList(3), "ITEM_ID", "ITEM_NAME");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View(result);
        }
    }
}