using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System.Linq;

namespace Sciffer.Erp.Web.Controllers
{
    public class TDSCodeController : Controller
    {
        private readonly ITdsCodeService _tdsCodeService;
        private readonly IGenericService _Generic;
        private readonly IGeneralLedgerService _generalLedgerService;
        public TDSCodeController(IGeneralLedgerService GeneralLedgerService, ITdsCodeService TdsCodeService, IGenericService gen)
        {
            _tdsCodeService = TdsCodeService;
            _Generic = gen;
            _generalLedgerService = GeneralLedgerService;
        }

        // GET: TDSCode
        public ActionResult Index()
        {
            ViewBag.DataSource = _tdsCodeService.getall();
            return View();
        }
        public ActionResult Delete(int key)
        {
            var res = _tdsCodeService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
      
       

        // GET: TDSCode/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tds_codeVM ref_tds_code = _tdsCodeService.Get((int)id);
            if (ref_tds_code == null)
            {
                return HttpNotFound();
            }
         
             ViewBag.credit_list = _Generic.GetLedgerAccount(2);
            return View(ref_tds_code);
        }

        // GET: TDSCode/Create
        public ActionResult Create()
        {
            var gl = _Generic.GetLedgerAccount(2);
            ViewBag.credit_list = new SelectList(gl, "gl_ledger_id", "gl_ledger_name");
            ViewBag.creditorgl = new SelectList(gl, "gl_ledger_id", "gl_ledger_name");
            return View();
        }

        // POST: TDSCode/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tds_codeVM ref_tax_element, FormCollection fc)
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

            List<ref_tds_code_detail> element_list = new List<ref_tds_code_detail>();
            for (int i = 0; i < emptyStringArray.Count() - 1; i++)
            {
                ref_tds_code_detail element = new ref_tds_code_detail();
                element.effective_from = DateTime.Parse(emptyStringArray[i].Split(new char[] { ',' })[2]);               
                element.rate = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[3]);
                element.is_active = true;
                element_list.Add(element);
            }
            ref_tax_element.ref_tds_code_detail = element_list;
            if (ModelState.IsValid)
            {
                var isValid = _tdsCodeService.Add(ref_tax_element);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }
            var gl = _Generic.GetLedgerAccount(2);
            ViewBag.credit_list = new SelectList(gl, "gl_ledger_id", "gl_ledger_name");
            ViewBag.creditorgl = new SelectList(gl, "gl_ledger_id", "gl_ledger_name");
            return View(ref_tax_element);
        }

        // GET: TDSCode/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tds_codeVM ref_tds_code = _tdsCodeService.Get((int)id);
            if (ref_tds_code == null)
            {
                return HttpNotFound();
            }
            var gl = _Generic.GetLedgerAccount(2);
            ViewBag.credit_list = new SelectList(gl, "gl_ledger_id", "gl_ledger_name");
            ViewBag.creditorgl = new SelectList(gl, "gl_ledger_id", "gl_ledger_name");
            return View(ref_tds_code);
        }

        // POST: TDSCode/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tds_codeVM ref_tax_element, FormCollection fc)
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

            List<ref_tds_code_detail> element_list = new List<ref_tds_code_detail>();
            for (int i = 0; i < emptyStringArray.Count() - 1; i++)
            {
                ref_tds_code_detail element = new ref_tds_code_detail();
                element.effective_from = DateTime.Parse(emptyStringArray[i].Split(new char[] { ',' })[2]);
                var eto = emptyStringArray[i].Split(new char[] { ',' })[3];
                element.rate = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[3]);
                element.is_active = true;
                if (emptyStringArray[i].Split(new char[] { ',' })[0] == "")
                {
                    element.tds_code_detail_id = 0;
                }
                else
                {
                    element.tds_code_detail_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[0]);
                }
                element_list.Add(element);
            }
            ref_tax_element.ref_tds_code_detail = element_list;
            if (ModelState.IsValid)
            {
                var isValid = _tdsCodeService.Add(ref_tax_element);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }
            var gl = _Generic.GetLedgerAccount(2);
            ViewBag.credit_list = new SelectList(gl, "gl_ledger_id", "gl_ledger_name");
            ViewBag.creditorgl = new SelectList(gl, "gl_ledger_id", "gl_ledger_name");
            return View(ref_tax_element);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tdsCodeService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
