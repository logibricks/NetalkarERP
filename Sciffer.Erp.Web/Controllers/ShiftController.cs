using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using Syncfusion.JavaScript.Models;
using Syncfusion.EJ.Export;
using System.Web.Script.Serialization;
using System.Collections;
using Syncfusion.XlsIO;
using System.Reflection;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class ShiftController : Controller
    {
        private readonly IShiftService _shiftService;
        private readonly IPlantService _plantService;
        private readonly IGenericService _Generic;
        public ShiftController(IShiftService ShiftService, IPlantService PlantService, IGenericService gen)
        {
            _shiftService = ShiftService;
            _plantService = PlantService;
            _Generic = gen;
        }

        // GET: Shift
        [CustomAuthorizeAttribute("SHFT")]
        public ActionResult Index()
        {
            ViewBag.plant_list = new SelectList(_plantService.GetPlant(), "PLANT_ID", "PLANT_NAME");
            ViewBag.Datasource = _shiftService.GetShiftList();
            return View();
        }

       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _shiftService.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult InlineInsert(shift value)
        {

            var add = _Generic.CheckDuplicate(value.from_time.ToString()+","+value.to_time.ToString()+","+value.shift_code,value.plant_id.ToString(),"", "shift", value.shift_id);
            if (add == 0)
            {
                if (value.shift_id == 0)
                {
                    var data1 = _shiftService.Add(value);
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _shiftService.Update(value);
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
