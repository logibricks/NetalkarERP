using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using System;
using Syncfusion.JavaScript.Models;
using Syncfusion.EJ.Export;
using System.Web.Script.Serialization;
using System.Collections;
using System.Collections.Generic;
using Syncfusion.XlsIO;
using System.Reflection;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class DocumentNumberingController : Controller
    {
        private readonly IDocumentNumbringService _documentService;
        private readonly IFinancialYearService _financialService;
        private readonly ICategoryService _Category;
        private readonly IModuleService _module;
        private readonly IModuleFormService _moduleForm;
        private readonly IGenericService _ge;
        public DocumentNumberingController(IDocumentNumbringService documentService, IFinancialYearService financialService, ICategoryService salesCategory, IModuleService module,
            IModuleFormService moduleform, IGenericService ge )
        {
            _documentService = documentService;
            _financialService = financialService;
            _Category = salesCategory;
            _module = module;
            _moduleForm = moduleform;
            _ge = ge;
        }

        // GET: DocumentNumbring
        [CustomAuthorizeAttribute("DOCN")]
        public ActionResult Index()
        {
            var finance = _financialService.GetAll();
            var category = _Category.GetAll();
            ViewBag.modulelist = new SelectList(_module.GetAll(), "module_id", "module_name");
            ViewBag.moduleformlist = new SelectList(_moduleForm.GetAll(), "module_form_id", "module_form_name");
            ViewBag.financelist = new SelectList(finance, "FINANCIAL_YEAR_ID", "FINANCIAL_YEAR_NAME");
            ViewBag.categorylist = new SelectList(category, "CATEGORY_ID", "CATEGORY_NAME");
            ViewBag.Datasource = _documentService.GetDocumentNumbering();
            ViewBag.PlantList = new SelectList(_ge.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            return View();
        }
      
      protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _documentService.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult InlineInsert(document_numbring value)
        {

            var add = _ge.CheckDuplicate(value.module_form_id.ToString(), value.category,value.financial_year_id.ToString(), "documentnumbering", value.document_numbring_id);
         //   var add = 0;
            if (add == 0)
            {
                if (value.document_numbring_id == 0)
                {
                    var data1 = _documentService.Add(value);
                    //var data1 = _countryService.GetAll();
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _documentService.Update(value);
                    // var data1 = _countryService.GetAll();
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { text = "duplicate" }, JsonRequestBehavior.AllowGet);

            }
        }
    }
}
