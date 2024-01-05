using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using System.Web;
using System.Linq;
using System.IO;
using Excel;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using Sciffer.Erp.Web.CustomFilters;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Web.Controllers
{
    public class FinancialTemplateController : Controller
    {
        private readonly IGeneralLedgerService _general;
        private readonly IGLAccountTypeService _gl;
        private readonly IGenericService _Generic;
        private readonly IFinancialTemplateService _FinancialTemplate;
        string[] error = new string[30000];
        string errorMessage = "";
        int errorList = 0;
        string Message = "";
        // GET: FinancialTemplate
        public FinancialTemplateController(IGeneralLedgerService general, IGenericService gen, IGLAccountTypeService gl, IFinancialTemplateService FinancialTemplate)
        {
            _general = general;
            _Generic = gen;
            _gl = gl;
            _FinancialTemplate = FinancialTemplate;

        }
        public ActionResult Index()
        {
            ViewBag.num = TempData["data"];
            ViewBag.DataSource = _FinancialTemplate.getall();
            return View();
        }
        //public ActionResult Index1(int? bs_pl, string searchstring)
        //{
        //    {
        //        if (bs_pl == null)
        //        {
        //            bs_pl = 1;
        //        }
        //        //var res = _FinancialTemplate.GetTreeVeiwList(bs_pl);
        //        ViewBag.getparent = new SelectList(_Generic.GetFinTemplateParentList(), "template_detail_id", "group_name");
        //        if (!String.IsNullOrEmpty(searchstring))
        //        {

        //        }
        //        ViewBag.ActiveTabId = searchstring;
        //        //ViewBag.datasource = res;
        //        return View();
        //    }
        //}
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult FillParentList(int id)
        {
            var paymentService = _FinancialTemplate.Get_Parent_Ledger(id);
            return Json(paymentService, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Create(ref_fin_template_vm value)
        {
            if (ModelState.IsValid)
            {
                var isValid = _FinancialTemplate.Add(value);
                if (isValid == true)
                {
                    return RedirectToAction("Index");
                }
            }
            //ViewBag.getparent = new SelectList(_Generic.GetFinTemplateParentList(), "template_detail_id", "group_name");
            return RedirectToAction("Index");
        }

        public ActionResult ExportData()
        {

            var query = _FinancialTemplate.GetExport();
            var grid = new GridView();
            grid.DataSource = query;
            grid.DataBind();
            if (grid.Rows.Count > 1)
            {
                for (int i = 0; i <= grid.Rows.Count - 1; i++)
                {
                    grid.Rows[i].Cells[0].Text = (i + 1).ToString();
                }
            }
            Response.ClearContent();
            Response.ContentType = "application/vnd.openxmlformats-     officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=FinancialTemplate.xls");
            //Response.AddHeader("content-disposition", "attachement; filename=GeneralLedger.xlsx");
            //Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("Index", "ExportData");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ref_fin_template_vm journal_entry = _FinancialTemplate.GetTreeVeiwList(id);

            ViewBag.journal_entry = journal_entry;

            if (journal_entry == null)
            {
                return HttpNotFound();
            }
            return View(journal_entry);
        }
        [HttpPost]
        public ActionResult Edit(ref_fin_template_detail vm)
        {
            
            //ViewBag.getparent = new SelectList(_Generic.GetFinTemplateParentList(), "template_detail_id", "group_name");
            var isValid = _FinancialTemplate.Update(vm);
            if (isValid == true)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
