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
    public class StorageLocationController : Controller
    {
        private readonly IStorageLocation _storage;
        private readonly IGenericService _Generic;
        private readonly IPlantService _Plant;
        public StorageLocationController(IStorageLocation storage, IGenericService gen, IPlantService plant)
        {
            _storage = storage;
            _Generic = gen;
            _Plant = plant;
        }


        // GET: StorageLocation
        [CustomAuthorizeAttribute("SLOC")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _storage.getstoragelist();
            ViewBag.plant = _Plant.GetPlant();
            return View();
        }
        public ActionResult InlineDelete(int key)
        {
            var res = _storage.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(storage_vm value)
        {

            var add = _Generic.CheckDuplicate(value.storage_location_name + "," + value.description, value.plant_id.ToString(), "", "storagelocation", value.storage_location_id);
            if (add == 0)
            {
                if (value.storage_location_id == 0)
                {
                    var data1 = _storage.Add(value);
                    //var data1 = _countryService.GetAll();
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _storage.Update(value);
                    // var data1 = _countryService.GetAll();
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { text = "duplicate" }, JsonRequestBehavior.AllowGet);

            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _storage.Dispose();
            }
            base.Dispose(disposing);
        }           

        public ActionResult getstoragelistUsingDocumentId(int docId)
        {
            var result = _storage.getstoragelistUsingDocumentId(docId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
