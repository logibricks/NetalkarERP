using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class TerritoryController : Controller
    {
        private readonly ITerritoryService _countryService;
        private readonly IGenericService _Generic;
        public TerritoryController(ITerritoryService countryService, IGenericService gen)
        {
            _countryService = countryService;
            _Generic = gen;
        }

        // GET: Territory
        [CustomAuthorizeAttribute("TRTY")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _countryService.GetAll();
            return View();
        }
        public ActionResult InlineDelete(int key)
        {
            var delete= _countryService.Delete(key);
            return Json(delete, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _countryService.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult InlineInsert(REF_TERRITORY value)
        {

            var add = _Generic.CheckDuplicate(value.TERRITORY_NAME,"", "", "territory", value.TERRITORY_ID);
            if (add == 0)
            {
                if (value.TERRITORY_ID == 0)
                {
                    var data1 = _countryService.Add(value);
                    //var data1 = _countryService.GetAll();
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _countryService.Update(value);
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
