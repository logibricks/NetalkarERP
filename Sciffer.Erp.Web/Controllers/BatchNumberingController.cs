using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using Syncfusion.EJ.Export;
using Syncfusion.JavaScript.Models;
using Syncfusion.XlsIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Sciffer.Erp.Web.Controllers
{
    public class BatchNumberingController : Controller
    {
        private readonly IBatchNumberingService _batchNumbering;
        private readonly IPlantService _plant;
        private readonly IItemCategoryService _itemCategory;
        private readonly IGenericService _Generic;
        private readonly IFinancialYearService _financialYearService;
        public BatchNumberingController(IFinancialYearService FinancialYearService,IGenericService Generic,IBatchNumberingService batchNumbering, IPlantService plant, IItemCategoryService itemCategory)
        {
            _batchNumbering = batchNumbering;
            _plant = plant;
            _itemCategory = itemCategory;
            _Generic = Generic;
            _financialYearService = FinancialYearService;
        }
        // GET: BatchNumbering
        [CustomAuthorizeAttribute("BTCHN")]
        public ActionResult Index()
        {
            ViewBag.financelist = new SelectList(_financialYearService.GetAll(), "FINANCIAL_YEAR_ID", "FINANCIAL_YEAR_NAME");
            ViewBag.DataSource = _batchNumbering.GetAll();
            var itemCategory = _Generic.GetItemCategoryList();
            ViewBag.ItemCategory = itemCategory;
            var plant = _Generic.GetPlantList();
            ViewBag.Plant = plant;
            return View();
        } 
        public ActionResult InlineDelete(int key)
        {
            var data = _batchNumbering.Delete(key);
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult InlineInsert(batch_numbering_VM value)
        {

            var add = _Generic.CheckDuplicate(value.plant_id.ToString(), value.item_category_id.ToString(),"", "batchnumbering", value.batch_no_id);
            if (add == 0)
            {
                if (value.batch_no_id == 0)
                {
                    var data1 = _batchNumbering.Add(value);                  
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _batchNumbering.Update(value);                   
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