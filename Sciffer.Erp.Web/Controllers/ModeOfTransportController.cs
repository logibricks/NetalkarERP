using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class ModeOfTransportController : Controller
    {
        private readonly IModeOfTransportService _Transport;
        private readonly IGenericService _Generic;
        public ModeOfTransportController(IModeOfTransportService Transport, IGenericService gen)
        {
            _Transport = Transport;
            _Generic = gen;
        }
        // GET: ModeOfTransport
        public ActionResult Index()
        {
            ViewBag.datasource = _Transport.GetAll();
            return View();
        }
        public ActionResult InlineDelete(int key)
        {
            var res = _Transport.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InlineInsert(ref_mode_of_transport value)
        {

            var add = _Generic.CheckDuplicate(value.mode_of_transport_name, value.mode_of_transport_name, "", "mode_of_transport", value.mode_of_transport_id);
            if (add == 0)
            {
                if (value.mode_of_transport_id == 0)
                {
                    var data1 = _Transport.Add(value);
                    //var data1 = _countryService.GetAll();
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _Transport.Update(value);
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
                _Transport.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}