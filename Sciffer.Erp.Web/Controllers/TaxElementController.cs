using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System.Linq;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class TaxElementController : Controller
    {
        private readonly ITaxElementService _taxElementService;
        private readonly ITaxTypeService _taxTypeService;
        private readonly IGenericService _Generic;
        private readonly IGeneralLedgerService _generalLedgerService;
        public TaxElementController(IGeneralLedgerService GeneralLedgerService, ITaxElementService TaxElementService, ITaxTypeService TaxTypeService, IGenericService gen)
        {
            _taxElementService = TaxElementService;
            _taxTypeService = TaxTypeService;
            _Generic = gen;
            _generalLedgerService = GeneralLedgerService;
        }

        // GET: TaxElement
        [CustomAuthorizeAttribute("TAXEL")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _taxElementService.getall();
            return View();
        }

        public ActionResult Delete(int key)
        {
            var res = _taxElementService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }


        // GET: TaxElement/Details/5
        [CustomAuthorizeAttribute("TAXEL")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tax_elementVM ref_tax_element = _taxElementService.Get((int)id);
            if (ref_tax_element == null)
            {
                return HttpNotFound();
            }
            ViewBag.tax_type_list = new SelectList(_taxTypeService.GetAll(), "tax_type_id", "tax_type_name");            
            ViewBag.sales_gllist = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            return View(ref_tax_element);
        }

        // GET: TaxElement/Create
        [CustomAuthorizeAttribute("TAXEL")]
        public ActionResult Create()
        {
            ViewBag.tax_type_list = new SelectList(_taxTypeService.GetAll(), "tax_type_id", "tax_type_name");
            ViewBag.sales_gllist = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.no_setoff_gllist = new SelectList(_generalLedgerService.GetAccountGeneral(), "gl_ledger_id", "gl_ledger_name");
            return View();
        }

        // POST: TaxElement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tax_elementVM ref_tax_element, FormCollection fc)
        {
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

            List<ref_tax_element_detail> element_list = new List<ref_tax_element_detail>();
            for (int i = 0; i < emptyStringArray.Count() - 1; i++)
            {
                ref_tax_element_detail element = new ref_tax_element_detail();       
                      
                element.effective_from = DateTime.Parse(emptyStringArray[i].Split(new char[] { ',' })[2]);                         
                element.rate = double.Parse(emptyStringArray[i].Split(new char[] { ',' })[3]);
                element.no_setoff = emptyStringArray[i].Split(new char[] { ',' })[4]==""?0:double.Parse(emptyStringArray[i].Split(new char[] { ',' })[4]);
                element.on_hold = emptyStringArray[i].Split(new char[] { ',' })[5] == "" ? 0 : double.Parse(emptyStringArray[i].Split(new char[] { ',' })[5]);   
                element.is_active = true;
                element_list.Add(element);
            }
            ref_tax_element.ref_tax_element_detail = element_list;
            if (ModelState.IsValid)
            {
                var isValid = _taxElementService.Add(ref_tax_element);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.tax_type_list = new SelectList(_taxTypeService.GetAll(), "tax_type_id", "tax_type_name");
            ViewBag.sales_gllist = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.no_setoff_gllist = new SelectList(_generalLedgerService.GetAccountGeneral(), "gl_ledger_id", "gl_ledger_name");
            return View(ref_tax_element);
        }

        // GET: TaxElement/Edit/5
        [CustomAuthorizeAttribute("TAXEL")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tax_elementVM ref_tax_element = _taxElementService.Get((int)id);
            if (ref_tax_element == null)
            {
                return HttpNotFound();
            }
            ViewBag.tax_type_list = new SelectList(_taxTypeService.GetAll(), "tax_type_id", "tax_type_name");
            ViewBag.sales_gllist = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.no_setoff_gllist = new SelectList(_generalLedgerService.GetAccountGeneral(), "gl_ledger_id", "gl_ledger_name");

            return View(ref_tax_element);
        }

        // POST: TaxElement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tax_elementVM ref_tax_element, FormCollection fc)
        {
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
            List<ref_tax_element_detail> element_list = new List<ref_tax_element_detail>();
            for (int i = 0; i < emptyStringArray.Count() - 1; i++)
            {
                ref_tax_element_detail element = new ref_tax_element_detail();

                element.effective_from = DateTime.Parse(emptyStringArray[i].Split(new char[] { ',' })[2]);
                element.rate = double.Parse(emptyStringArray[i].Split(new char[] { ',' })[3]);
                element.no_setoff = emptyStringArray[i].Split(new char[] { ',' })[4] == "" ? 0 : double.Parse(emptyStringArray[i].Split(new char[] { ',' })[4]);
                element.on_hold = emptyStringArray[i].Split(new char[] { ',' })[5] == "" ? 0 : double.Parse(emptyStringArray[i].Split(new char[] { ',' })[5]);
                element.is_active = true;
                element.tax_element_detail_id= emptyStringArray[i].Split(new char[] { ',' })[0] == "" ? 0 : int.Parse(emptyStringArray[i].Split(new char[] { ',' })[0]);
                element_list.Add(element);
            }
            ref_tax_element.ref_tax_element_detail = element_list;
            if (ModelState.IsValid)
            {
                var isValid = _taxElementService.Add(ref_tax_element);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.tax_type_list = new SelectList(_taxTypeService.GetAll(), "tax_type_id", "tax_type_name");
            ViewBag.sales_gllist = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.no_setoff_gllist = new SelectList(_generalLedgerService.GetAccountGeneral(), "gl_ledger_id", "gl_ledger_name");
            return View(ref_tax_element);
        }

        // GET: TaxElement/Delete/5
      

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _taxElementService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
