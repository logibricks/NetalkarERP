using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.Model;

namespace Sciffer.Erp.Web.Controllers
{
    public class SACMasterController : Controller
    {
        // GET: SAC
        private readonly ISACService _SacService;
        private readonly IGenericService _Generic;
        public SACMasterController(ISACService sac, IGenericService gen)
        {
            _SacService = sac;
            _Generic = gen;
        }
        public ActionResult Index()
        {
            ViewBag.datasource = _SacService.GetAll();
            return View();
        }
        public ActionResult InlineDelete(int key)
        {
            var res = _SacService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(ref_sac value)
        {
            var add = _Generic.CheckDuplicate(value.sac_code, value.sac_description, "", "SAC", value.sac_id);
            if (add == 0)
            {
                if (value.sac_id == 0)
                {
                    var data1 = _SacService.Add(value);                    
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _SacService.Update(value);                   
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
                _SacService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}