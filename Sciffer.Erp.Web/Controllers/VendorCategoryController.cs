using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class VendorCategoryController : Controller
    {
        private readonly IVendorCategoryService _vendorService;
        private readonly IGenericService _Generic;
        public VendorCategoryController(IVendorCategoryService countryService, IGenericService gen)
        {
            _vendorService = countryService;
            _Generic = gen;
        }

        // GET: Vendor_Category
        [CustomAuthorizeAttribute("VNDRC")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _vendorService.GetAll();
            return View();
        }
       
     
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _vendorService.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult InlineInsert(REF_VENDOR_CATEGORY value)
        {         
            var add = _Generic.CheckDuplicate(value.VENDOR_CATEGORY_NAME, "","", "vendorcategory", value.VENDOR_CATEGORY_ID);
            if (add == 0)
            {
                if (value.VENDOR_CATEGORY_ID == 0)
                {
                    var data1 = _vendorService.Add(value);
                    //var data1 = _countryService.GetAll();
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = _vendorService.Update(value);
                    // var data1 = _countryService.GetAll();
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { text = "duplicate" }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult InlineDelete(int key)
        {
            var res = _vendorService.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}
